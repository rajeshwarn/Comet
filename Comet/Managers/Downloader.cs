namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;

    using Comet.Events;
    using Comet.Structure;

    #endregion

    [Description("Downloads and resumes files from HTTP, HTTPS, FTP, and File (file://) URLs.")]
    public class Downloader
    {
        #region Variables

        public long Adler32;

        public string PublicSignKey;

        public byte[] SignedSHA1Hash;

        public bool UseRelativeProgress;

        #endregion

        #region Variables

        private readonly BackgroundWorker _backgroundWorker;

        private readonly Adler32 _downloaderAdler32;

        private readonly Stopwatch _stopWatch;

        private string _currentUrl;

        private bool _downloadComplete;

        private string _downloadFolder;

        private string _downloadSpeed;

        private int _progressPercentage;

        private long _sentSinceLastCalc;

        private Bytes _units;

        private List<string> _urlList;

        private bool _waitingForResponse;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Downloader" /> class.
        /// </summary>
        /// <param name="url">The url list.</param>
        /// <param name="downloadFolder">The download folder.</param>
        public Downloader(List<string> url, string downloadFolder)
        {
            _downloadComplete = false;
            _downloaderAdler32 = new Adler32();
            _stopWatch = new Stopwatch();

            _urlList = url;
            _downloadFolder = downloadFolder;

            _backgroundWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };

            _backgroundWorker.DoWork += BackgroundWorkerDoWork;
            _backgroundWorker.ProgressChanged += BackgroundWorkerProgressChanged;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerRunWorkerCompleted;
        }

        public event Delegates.DownloaderProgressChangedEventHandler ProgressChanged;

        public enum ProgressStatus
        {
            /// <summary>The none.</summary>
            None,

            /// <summary>The success.</summary>
            Success,

            /// <summary>The failure.</summary>
            Failure,

            /// <summary>The sharing violation.</summary>
            SharingViolation
        }

        #endregion

        #region Properties

        public string CurrentUrl
        {
            get
            {
                return _currentUrl;
            }
        }

        public bool DownloadComplete
        {
            get
            {
                return _downloadComplete;
            }
        }

        public string DownloadFolder
        {
            get
            {
                return _downloadFolder;
            }

            set
            {
                _downloadFolder = value;
            }
        }

        public string DownloadingTo { get; private set; }

        public int ProgressPercentage
        {
            get
            {
                return _progressPercentage;
            }
        }

        public List<string> UrlList
        {
            get
            {
                return _urlList;
            }

            set
            {
                _urlList = value;
            }
        }

        #endregion

        #region Events

        public const int BufferSize = 4096;

        public static WebProxy CustomProxy;

        public static void SetupSaneDownloadOptions()
        {
            ServicePointManager.Expect100Continue = false;
        }

        public void Cancel()
        {
            _backgroundWorker.CancelAsync();
        }

        public void Download()
        {
            // Check if the PublicSignKey exists and the update isn't signed then just don't bother downloading.
            if ((PublicSignKey != null) && (SignedSHA1Hash == null))
            {
                // Un-register background worker.
                BackgroundWorkerRunWorkerCompleted(null, null);

                // Notify user that updates must be signed.
                ProgressChanged(new DownloaderEventArgs(-1, -1, string.Empty, ProgressStatus.Failure, new Exception("The update is not signed. All updates must be signed in order to be installed.")));
            }
            else
            {
                _backgroundWorker.RunWorkerAsync();
            }
        }

        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            _downloadComplete = false;

            // validate input
            if ((_urlList == null) || (_urlList.Count == 0))
            {
                if (string.IsNullOrEmpty(_currentUrl))
                {
                    // no sites specified, bail out
                    if (!_backgroundWorker.CancellationPending)
                    {
                        _backgroundWorker.ReportProgress(0, new object[] { -1, -1, string.Empty, ProgressStatus.Failure, new Exception("No download urls are specified.") });
                    }

                    return;
                }

                // single site specified, add it to the list
                _urlList = new List<string> { _currentUrl };
            }

            // use the custom proxy if provided
            if (CustomProxy != null)
            {
                WebRequest.DefaultWebProxy = CustomProxy;
            }
            else
            {
                IWebProxy proxy = WebRequest.GetSystemWebProxy();

                if (proxy.Credentials == null)
                {
                    proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                }

                WebRequest.DefaultWebProxy = proxy;
            }

            // try each url in the list until one succeeds
            var allFailedWaitingForResponse = true;
            Exception ex = null;
            foreach (string s in _urlList)
            {
                ex = null;
                try
                {
                    _currentUrl = s;
                    BeginDownload();
                    ValidateDownload();
                }
                catch (Exception except)
                {
                    ex = except;

                    if (!_waitingForResponse)
                    {
                        allFailedWaitingForResponse = false;
                    }
                }

                // If we got through that without an exception, we found a good url
                if ((ex == null) || _backgroundWorker.CancellationPending)
                {
                    allFailedWaitingForResponse = false;
                    break;
                }
            }

            /*
             If all the sites failed before a response was received then either the 
             internet connection is shot, or the Proxy is shot. Either way it can't 
             hurt to try downloading without the proxy:
            */
            if (allFailedWaitingForResponse && (WebRequest.DefaultWebProxy != null))
            {
                // try the sites again without a proxy
                WebRequest.DefaultWebProxy = null;

                foreach (string s in _urlList)
                {
                    ex = null;
                    try
                    {
                        _currentUrl = s;
                        BeginDownload();
                        ValidateDownload();
                    }
                    catch (Exception except)
                    {
                        ex = except;
                    }

                    // If we got through that without an exception, we found a good url
                    if ((ex == null) || _backgroundWorker.CancellationPending)
                    {
                        break;
                    }
                }
            }

            // Process complete (either successfully or failed), report back
            if (_backgroundWorker.CancellationPending || (ex != null))
            {
                _backgroundWorker.ReportProgress(0, new object[] { -1, -1, string.Empty, ProgressStatus.Failure, ex });
            }
            else
            {
                _backgroundWorker.ReportProgress(0, new object[] { -1, -1, string.Empty, ProgressStatus.Success, null });
            }
        }
       
        /// <summary>
        /// The background worker progress changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var _percentDone = (object[])e.UserState;
            var _unWeightPercent = (object[])e.UserState;
            var _extraStatus = (object[])e.UserState;
            var _progressStatus = (object[])e.UserState;
            var _userStateData = (object[])e.UserState;

            if (ProgressChanged != null)
            {
                ProgressChanged(new DownloaderEventArgs((int)_percentDone[0], (int)_unWeightPercent[1], (string)_extraStatus[2], (ProgressStatus)_progressStatus[3], _userStateData[4]));
            }
            
           // _progressPercentage = e.ProgressPercentage;
        }

        private void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _downloadComplete = true;

            _backgroundWorker.DoWork -= BackgroundWorkerDoWork;
            _backgroundWorker.ProgressChanged -= BackgroundWorkerProgressChanged;
            _backgroundWorker.RunWorkerCompleted -= BackgroundWorkerRunWorkerCompleted;
        }

        private void BeginDownload()
        {
            DownloadData data = null;
            FileStream fs = null;

            try
            {
                // start the stopwatch for speed calc
                _stopWatch.Start();

                // get download details 
                _waitingForResponse = true;
                data = DownloadData.Create(_currentUrl, _downloadFolder);
                _waitingForResponse = false;

                // reset the adler
                _downloaderAdler32.Reset();

                DownloadingTo = data.FileName;

                if (!File.Exists(DownloadingTo))
                {
                    // create the file
                    fs = File.Open(DownloadingTo, FileMode.Create, FileAccess.Write);
                }
                else
                {
                    // read in the existing data to calculate the adler32
                    if (Adler32 != 0)
                    {
                        GetAdler32(DownloadingTo);
                    }

                    // Append to an existing file (resume)
                    fs = File.Open(DownloadingTo, FileMode.Append, FileAccess.Write);
                }

                // create the download buffer
                var buffer = new byte[BufferSize];

                int readCount;

                // update how many bytes have already been read
                _sentSinceLastCalc = data.StartPoint; // for BPS calculation

                // Only increment once for each %
                var LastProgress = 0;
                while ((readCount = data.DownloadStream.Read(buffer, 0, BufferSize)) > 0)
                {
                    // break on cancel
                    if (_backgroundWorker.CancellationPending)
                    {
                        data.Close();
                        fs.Close();
                        break;
                    }

                    // update total bytes read
                    data.StartPoint += readCount;

                    // update the adler32 value
                    if (Adler32 != 0)
                    {
                        _downloaderAdler32.Update(buffer, 0, readCount);
                    }

                    // save block to end of file
                    fs.Write(buffer, 0, readCount);

                    // calculate download speed
                    CalculateBps(data.StartPoint, data.TotalDownloadSize);

                    // send progress info
                    if (!_backgroundWorker.CancellationPending && (data.PercentDone > LastProgress))
                    {
                        _backgroundWorker.ReportProgress(0, new object[]
                            {
                                // use the realtive progress or the raw progress
                                // UseRelativeProgress ? InstallUpdate.GetRelativeProgess(0, data.PercentDone) : data.PercentDone,

                                // unweighted percent
                                data.PercentDone,
                                _downloadSpeed, ProgressStatus.None, null
                            });

                        LastProgress = data.PercentDone;
                    }

                    // break on cancel
                    if (_backgroundWorker.CancellationPending)
                    {
                        data.Close();
                        fs.Close();
                        break;
                    }
                }
            }
            catch (UriFormatException e)
            {
                throw new Exception(string.Format("Could not parse the URL \"{0}\" - it's either malformed or is an unknown protocol.", _currentUrl), e);
            }
            catch (Exception e)
            {
                if (string.IsNullOrEmpty(DownloadingTo))
                {
                    throw new Exception(string.Format("Error trying to save file: {0}", e.Message), e);
                }
                else
                {
                    throw new Exception(string.Format("Error trying to save file \"{0}\": {1}", DownloadingTo, e.Message), e);
                }
            }
            finally
            {
                if (data != null)
                {
                    data.Close();
                }

                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        private void CalculateBps(long BytesReceived, long TotalBytes)
        {
            if (_stopWatch.Elapsed < TimeSpan.FromSeconds(2))
            {
                return;
            }

            _stopWatch.Stop();

            // Calculcate transfer speed.
            long bytes = BytesReceived - _sentSinceLastCalc;
            double bps = (bytes * 1000.0) / _stopWatch.Elapsed.TotalMilliseconds;
            _downloadSpeed = BytesReceived + " / " + (TotalBytes == 0 ? "unknown" : TotalBytes + "   (" + bps + "/sec)");

            // Estimated seconds remaining based on the current transfer speed.
            // secondsRemaining = (int)((e.TotalBytesToReceive - e.BytesReceived) / bps);

            // Restart stopwatch for next second.
            _sentSinceLastCalc = BytesReceived;
            _stopWatch.Reset();
            _stopWatch.Start();
        }

        private void GetAdler32(string fileName)
        {
            var buffer = new byte[BufferSize];

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                int sourceBytes;

                do
                {
                    sourceBytes = fs.Read(buffer, 0, buffer.Length);

                    _downloaderAdler32.Update(buffer, 0, sourceBytes);

                    // break on cancel
                    if (_backgroundWorker.CancellationPending)
                    {
                        break;
                    }
                }
                while (sourceBytes > 0);
            }
        }

        private void ValidateDownload()
        {
            // if an Adler32 checksum is provided, check the file
            if (!_backgroundWorker.CancellationPending)
            {
                if ((Adler32 != 0) && (Adler32 != _downloaderAdler32.Value))
                {
                    // file failed to vaildate, throw an error
                    throw new Exception("The downloaded file \"" + Path.GetFileName(DownloadingTo) + "\" failed the Adler32 validation.");
                }

                if (PublicSignKey != null)
                {
                    if (SignedSHA1Hash == null)
                    {
                        throw new Exception("The downloaded file \"" + Path.GetFileName(DownloadingTo) + "\" is not signed.");
                    }

                    byte[] hash = null;

                    try
                    {
                        using (FileStream fs = new FileStream(DownloadingTo, FileMode.Open, FileAccess.Read))
                        using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                        {
                            hash = sha1.ComputeHash(fs);
                        }

                        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                        RSA.FromXmlString(PublicSignKey);

                        RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);
                        RSADeformatter.SetHashAlgorithm("SHA1");

                        // verify signed hash
                        if (!RSADeformatter.VerifySignature(hash, SignedSHA1Hash))
                        {
                            // The signature is not valid.
                            throw new Exception("Verification failed.");
                        }
                    }
                    catch (Exception ex)
                    {
                        string msg = "The downloaded file \"" + Path.GetFileName(DownloadingTo) +
                                     "\" failed the signature validation: " + ex.Message;

                        long sizeInBytes = new FileInfo(DownloadingTo).Length;

                        msg += "\r\n\r\nThis error is likely caused by a download that ended prematurely. Total size of the downloaded file: " + sizeInBytes;

                        // show the size in bytes only if the size displayed isn't already in bytes
                        if (sizeInBytes >= 0.9 * 1024)
                        {
                            msg += " (" + sizeInBytes + " bytes).";
                        }

                        if (hash != null)
                        {
                            msg += "\r\n\r\nComputed SHA1 hash of the downloaded file: " + BitConverter.ToString(hash);
                        }

                        throw new Exception(msg);
                    }
                }
            }
        }

        #endregion
    }
}