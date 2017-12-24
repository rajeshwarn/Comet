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

    using Comet.Controls;
    using Comet.Events;
    using Comet.Structure;

    #endregion

    public class DownloadsManager
    {
        #region Variables

        public long Adler32;
        public string PublicSignKey;
        public byte[] SignedSHA1Hash;
        public bool UseRelativeProgress;

        #endregion

        #region Variables

        private readonly BackgroundWorker _backgroundDownloader;
        private int _bufferSize;
        private int _currentDownload;
        private Uri _currentUrl;
        private WebProxy _customProxy;
        private bool _downloadComplete;
        private string _downloadDirectory;

        private List<string> _downloadedFiles;
        private Adler32 _downloaderAdler32;
        private List<Uri> _downloadList;
        private string _downloadSpeed;
        private long _sentSinceLastCalc;
        private Stopwatch _stopWatch;
        private int _totalCount;
        private bool _waitingForResponse;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DownloadsManager" /> class.
        /// </summary>
        /// <param name="downloadList">The download list.</param>
        /// <param name="downloadDirectory">The download path.</param>
        public DownloadsManager(List<Uri> downloadList, string downloadDirectory)
        {
            _downloaderAdler32 = new Adler32();
            _downloadedFiles = new List<string>();
            _downloadList = downloadList;
            _downloadDirectory = downloadDirectory;
            _totalCount = downloadList.Count;

            ServicePointManager.DefaultConnectionLimit = _totalCount;

            _bufferSize = 4096;
            _currentDownload = 0;
            _stopWatch = new Stopwatch();

            _backgroundDownloader = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };

            _backgroundDownloader.DoWork += BackgroundDownloader_DoWork;
            _backgroundDownloader.ProgressChanged += BackgroundDownloader_ProgressChanged;
            _backgroundDownloader.RunWorkerCompleted += BackgroundDownloader_RunWorkerCompleted;
        }

        public event EventHandler DownloadsCompleted;

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

        public int CurrentDownload
        {
            get
            {
                return _currentDownload;
            }
        }

        public bool DownloadCompleted
        {
            get
            {
                return _downloadComplete;
            }
        }

        public string DownloadDirectory
        {
            get
            {
                return _downloadDirectory;
            }

            set
            {
                _downloadDirectory = value;
            }
        }

        public List<string> DownloadedFiles
        {
            get
            {
                return _downloadedFiles;
            }
        }

        public string DownloadingTo { get; set; }

        public int TotalDownloads
        {
            get
            {
                return _totalCount;
            }
        }

        #endregion

        #region Events

        public static int GetRelativeProgress(int stepOn, int stepProgress)
        {
            return ((stepOn * 100) / TotalUpdateSteps) + (stepProgress / TotalUpdateSteps);
        }

        /// <summary>
        ///     Setup sane download options.
        /// </summary>
        public static void SetupSaneDownloadOptions()
        {
            ServicePointManager.Expect100Continue = false;
        }

        public const int TotalUpdateSteps = 7;

        /// <summary>
        ///     Cancel all the downloads.
        /// </summary>
        public void Cancel()
        {
            _backgroundDownloader.CancelAsync();
        }

        /// <summary>
        ///     Begin downloading the files.
        /// </summary>
        public void Download()
        {
            // Check if the PublicSignKey exists and the update isn't signed then just don't bother downloading.
            if ((PublicSignKey != null) && (SignedSHA1Hash == null))
            {
                // Un-register background worker.
                BackgroundDownloader_RunWorkerCompleted(null, null);

                // Notify user that updates must be signed.
                // BackgroundDownloader_ProgressChanged(new DownloaderEventArgs(-1, -1, string.Empty, Downloader.ProgressStatus.Failure, new Exception("The update is not signed. All updates must be signed in order to be installed.")));
            }
            else
            {
                _backgroundDownloader.RunWorkerAsync();
            }
        }

        /// <summary>
        ///     The background downloader do work event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void BackgroundDownloader_DoWork(object sender, DoWorkEventArgs e)
        {
            _downloadComplete = false;

            // Validate input
            if ((_downloadList == null) || (_downloadList.Count == 0))
            {
                if (string.IsNullOrEmpty(_currentUrl.OriginalString))
                {
                    // No sites specified!
                    if (!_backgroundDownloader.CancellationPending)
                    {
                        _backgroundDownloader.ReportProgress(0, new object[] { -1, -1, string.Empty, ProgressStatus.Failure, new Exception("No download urls are specified.") });
                    }

                    return;
                }

                _downloadList = new List<Uri> { _currentUrl };
            }

            InitializeProxy();

            // Try each url in the list until one succeeds.
            var _allFailedWaitingForResponse = true;
            Exception ex = null;
            foreach (Uri _url in _downloadList)
            {
                ex = null;
                try
                {
                    _currentUrl = _url;
                    BeginDownload();
                    ValidateDownload();
                }
                catch (Exception exception)
                {
                    ex = exception;

                    if (!_waitingForResponse)
                    {
                        _allFailedWaitingForResponse = false;
                    }
                }

                // If we got through that without an exception, we found a good url
                if ((ex == null) || _backgroundDownloader.CancellationPending)
                {
                    _allFailedWaitingForResponse = false;

                    // break;
                }
            }

            // If all the sites failed (Before response received):
            // The internet connection is shot,
            // The Proxy is shot.
            // Try downloading without the proxy.
            if (_allFailedWaitingForResponse && (WebRequest.DefaultWebProxy != null))
            {
                WebRequest.DefaultWebProxy = null;

                foreach (Uri _url in _downloadList)
                {
                    ex = null;
                    try
                    {
                        _currentUrl = _url;
                        BeginDownload();
                        ValidateDownload();
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }

                    // If we got through that without an exception, we found a good url
                    if ((ex == null) || _backgroundDownloader.CancellationPending)
                    {
                        // break;
                    }
                }
            }

            // Process complete (Successful or failed downloads) report back.
            if (_backgroundDownloader.CancellationPending || (ex != null))
            {
                _backgroundDownloader.ReportProgress(0, new object[] { -1, -1, string.Empty, ProgressStatus.Failure, ex });
            }
            else
            {
                _backgroundDownloader.ReportProgress(0, new object[] { -1, -1, string.Empty, ProgressStatus.Success, null });
            }
        }

        /// <summary>
        ///     The background downloader worker progress changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void BackgroundDownloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var arr = (object[])e.UserState;
            DownloaderEventArgs _event = new DownloaderEventArgs((int)arr[0], (int)arr[1], (string)arr[2], (ProgressStatus)arr[3], arr[4]);
            ProgressChanged?.Invoke(_event);
        }

        /// <summary>
        ///     The background downloader worker completed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void BackgroundDownloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _downloadComplete = true;

            _backgroundDownloader.DoWork -= BackgroundDownloader_DoWork;
            _backgroundDownloader.ProgressChanged -= BackgroundDownloader_ProgressChanged;
            _backgroundDownloader.RunWorkerCompleted -= BackgroundDownloader_RunWorkerCompleted;

            DownloadsCompleted?.Invoke(this, new EventArgs());
        }

        /// <summary>
        ///     Begins downloading the files.
        /// </summary>
        private void BeginDownload()
        {
            DownloadData _downloadData = null;
            FileStream _fileStream = null;

            try
            {
                FileManager.CreateDirectory(_downloadDirectory);

                // Start the stop stop watch for speed calculation.
                _stopWatch.Start();

                // Receive download details
                _waitingForResponse = true;

                _downloadData = new DownloadData(_currentUrl, _downloadDirectory);
                _downloadData.Create();

                _waitingForResponse = false;

                // Reset the adler
                _downloaderAdler32.Reset();

                DownloadingTo = _downloadData.OutputFilePath;
                _downloadedFiles.Add(DownloadingTo);

                if (!File.Exists(DownloadingTo))
                {
                    // Create the file.
                    _fileStream = File.Open(DownloadingTo, FileMode.Create, FileAccess.Write);
                }
                else
                {
                    // Read in the existing data to calculate the Adler32
                    if (Adler32 != 0)
                    {
                        GetAdler32(DownloadingTo);
                    }

                    // Append to an existing file (resume the download)
                    _fileStream = File.Open(DownloadingTo, FileMode.Append, FileAccess.Write);
                }

                var _buffer = new byte[_bufferSize];
                int _readCount;

                // Update how many bytes have already been read.
                _sentSinceLastCalc = _downloadData.StartPoint; // For BPS (bytes/per second) calculation

                // Only increment once for each %.
                var _lastProgress = 0;
                while ((_readCount = _downloadData.DownloadStream.Read(_buffer, 0, _bufferSize)) > 0)
                {
                    // Break on cancel
                    if (_backgroundDownloader.CancellationPending)
                    {
                        _downloadData.Close();
                        _fileStream.Close();
                        break;
                    }

                    // Update total bytes read
                    _downloadData.StartPoint += _readCount;

                    // Update the Adler32 value
                    if (Adler32 != 0)
                    {
                        _downloaderAdler32.Update(_buffer, 0, _readCount);
                    }

                    // Save block to end of file
                    _fileStream.Write(_buffer, 0, _readCount);

                    CalculateBps(_downloadData.StartPoint, _downloadData.TotalDownloadSize);

                    // Send progress info.
                    if (!_backgroundDownloader.CancellationPending && (_downloadData.PercentDone > _lastProgress))
                    {
                        _backgroundDownloader.ReportProgress(0, new object[]
                            {
                                // use the relative or the raw progress.
                                UseRelativeProgress ? GetRelativeProgress(0, _downloadData.PercentDone) : _downloadData.PercentDone,

                                // unweighted percent
                                _downloadData.PercentDone,
                                _downloadSpeed, ProgressStatus.None, null
                            });

                        _lastProgress = _downloadData.PercentDone;
                    }

                    // Break on cancel
                    if (_backgroundDownloader.CancellationPending)
                    {
                        _downloadData.Close();
                        _fileStream.Close();
                        break;
                    }
                }
            }
            catch (UriFormatException e)
            {
                VisualExceptionDialog.Show(new Exception($"Could not parse the URL \"{_currentUrl}\" - it's either malformed or is an unknown protocol.", e));
            }
            catch (Exception e)
            {
                if (string.IsNullOrEmpty(DownloadingTo))
                {
                    VisualExceptionDialog.Show(new Exception($"Error trying to save file: {e.Message}", e));
                }
                else
                {
                    VisualExceptionDialog.Show(new Exception($"Error trying to save file \"{DownloadingTo}\": {e.Message}", e));
                }
            }
            finally
            {
                _downloadData?.Close();
                _fileStream?.Close();
            }
        }

        private void CalculateBps(long bytesReceived, long totalBytes)
        {
            if (_stopWatch.Elapsed > TimeSpan.FromSeconds(2))
            {
                return;
            }

            _stopWatch.Stop();

            // Calculate transfer speed.
            long _bytes = bytesReceived - _sentSinceLastCalc;
            double _bps = (_bytes * 1000.0) / _stopWatch.Elapsed.TotalMilliseconds;
            _downloadSpeed = bytesReceived + " / " + (totalBytes == 0 ? "unknown" : totalBytes + " (" + _bps + "/sec)");

            // Estimated seconds remaining based on the current transfer speed.
            // secondsRemaining = (int)((e.TotalBytesToReceive - e.BytesReceived) / bps);

            // Restart stopwatch for next second.
            _sentSinceLastCalc = bytesReceived;
            _stopWatch.Reset();
            _stopWatch.Start();
        }

        /// <summary>
        ///     Get the Adler32.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        private void GetAdler32(string fileName)
        {
            var _buffer = new byte[_bufferSize];

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                int _sourceBytes;

                do
                {
                    _sourceBytes = fs.Read(_buffer, 0, _buffer.Length);
                    _downloaderAdler32.Update(_buffer, 0, _sourceBytes);

                    // Break on cancel
                    if (_backgroundDownloader.CancellationPending)
                    {
                        break;
                    }
                }
                while (_sourceBytes > 0);
            }
        }

        /// <summary>
        ///     Initialize the system web proxy.
        /// </summary>
        private void InitializeProxy()
        {
            if (_customProxy != null)
            {
                WebRequest.DefaultWebProxy = _customProxy;
            }
            else
            {
                IWebProxy _proxy = WebRequest.GetSystemWebProxy();

                if (_proxy.Credentials == null)
                {
                    _proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                }

                WebRequest.DefaultWebProxy = _proxy;
            }
        }

        private void ValidateDownload()
        {
            // if an Adler32 checksum is provided, check the file
            if (!_backgroundDownloader.CancellationPending)
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
                        using (FileStream _fileStream = new FileStream(DownloadingTo, FileMode.Open, FileAccess.Read))
                        using (SHA1CryptoServiceProvider _sha1 = new SHA1CryptoServiceProvider())
                        {
                            hash = _sha1.ComputeHash(_fileStream);
                        }

                        RSACryptoServiceProvider _rsa = new RSACryptoServiceProvider();
                        _rsa.FromXmlString(PublicSignKey);

                        RSAPKCS1SignatureDeformatter _rsaDeformatter = new RSAPKCS1SignatureDeformatter(_rsa);
                        _rsaDeformatter.SetHashAlgorithm("SHA1");

                        // Verify signed hash
                        if (!_rsaDeformatter.VerifySignature(hash, SignedSHA1Hash))
                        {
                            // The signature is not valid.
                            throw new Exception("Verification failed.");
                        }

                        _downloadComplete = true;
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