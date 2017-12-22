namespace Comet.Structure
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Text;
    using System.Threading;

    using Comet.Controls;
    using Comet.Exceptions;
    using Comet.Managers;

    #endregion

    [Description("Downloads and resumes files from HTTP, HTTPS, FTP, and File (file://) URLs.")]
    public class Downloader
    {
        #region Variables

        internal WebClient _client;

        #endregion

        #region Variables

        private bool _downloadComplete;
        private string _downloadDirectory;
        private List<string> _downloads;
        private List<string> _urlList;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="Downloader" /> class.</summary>
        /// <param name="urlList">The url List.</param>
        /// <param name="downloadDirectory">The download directory.</param>
        public Downloader(List<string> urlList, string downloadDirectory)
        {
            _downloadDirectory = downloadDirectory;

            VerifyURL(urlList);
            _urlList = urlList;

            _client = new WebClient();

            // _client.DownloadProgressChanged += DownloadProgressChanged;
            _client.DownloadFileCompleted += DownloadFileCompleted;

            _downloads = new List<string>();
        }

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

        public int Count
        {
            get
            {
                return _urlList.Count;
            }
        }

        public bool DownloadComplete
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
        }

        public List<string> Downloads
        {
            get
            {
                return _downloads;
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Tries to retrieve the file size.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>
        ///     <see cref="long" />
        /// </returns>
        public static long TryGetFileSize(string url)
        {
            WebRequest _webRequest = GetRequest(url);
            WebResponse _webResponse;

            try
            {
                // Get the file name for FTP files.
                if (_webRequest is FtpWebRequest)
                {
                    _webRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                    _webResponse = _webRequest.GetResponse();
                }
                else
                {
                    _webResponse = _webRequest.GetResponse();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error downloading \"{url}\": {e.Message}", e);
            }

            long _size;

            try
            {
                _size = _webResponse.ContentLength;
            }
            catch
            {
                // File size couldn't be determined
                _size = -1;
            }

            return _size;
        }

        /// <summary>
        ///     Tries to retrieve the file name from the url.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string TryGetName(string url)
        {
            var _invalidFilenameChars = new List<char>(Path.GetInvalidFileNameChars());

            WebRequest _webRequest = GetRequest(url);
            WebResponse _webResponse = _webRequest.GetResponse();

            // Take the name of the file given to use from the web server.
            string _fileName = _webRequest.Headers["Content-Disposition"];

            if (_fileName != null)
            {
                int _fileLoc = _fileName.IndexOf("filename=", StringComparison.OrdinalIgnoreCase);

                if (_fileLoc != -1)
                {
                    // Move past "filename=".
                    _fileLoc += 9;

                    if (_fileName.Length > _fileLoc)
                    {
                        // Trim off an ending semicolon if it exits.
                        int _end = _fileName.IndexOf(';', _fileLoc);

                        if (_end == -1)
                        {
                            _end = _fileName.Length - _fileLoc;
                        }
                        else
                        {
                            _end -= _fileLoc;
                        }

                        _fileName = _fileName.Substring(_fileLoc, _end).Trim();
                    }
                    else
                    {
                        _fileName = null;
                    }
                }
                else
                {
                    _fileName = null;
                }
            }

            if (string.IsNullOrEmpty(_fileName))
            {
                // Brute force the filename from the url.
                _fileName = Path.GetFileName(_webResponse.ResponseUri.LocalPath);
            }

            // Trim out non-standard file name characters. 
            if (!string.IsNullOrEmpty(_fileName) && (_fileName.IndexOfAny(_invalidFilenameChars.ToArray()) != -1))
            {
                // Make a new string builder (with at least one bad character).
                StringBuilder _newText = new StringBuilder(_fileName.Length - 1);

                // Remove bad characters.
                for (var i = 0; i < _fileName.Length; i++)
                {
                    if (_invalidFilenameChars.IndexOf(_fileName[i]) == 1)
                    {
                        _newText.Append(i);
                    }
                }

                _fileName = _newText.ToString().Trim();
            }

            // If filename *still* is null or empty, then generate some random temp filename
            if (string.IsNullOrEmpty(_fileName))
            {
                _fileName = Path.GetFileName(Path.GetTempFileName());
            }

            return _fileName;
        }

        /// <summary>
        ///     Verify the url source file/s exist.
        /// </summary>
        /// <param name="urlList">The url list.</param>
        public static void VerifyURL(IEnumerable<string> urlList)
        {
            foreach (string _url in urlList)
            {
                if (!NetworkManager.SourceExists(_url))
                {
                    VisualExceptionDialog.Show(new RemoteSourceNotFoundException(StringManager.RemoteFileNotFound(_url)));
                }
            }
        }

        /// <summary>
        ///     Download the file/s.
        /// </summary>
        public void Download()
        {
            FileManager.CreateDirectory(_downloadDirectory);

            foreach (string _url in _urlList)
            {
                string _output = $"{_downloadDirectory}{TryGetName(_url)}";

                // TODO: How do I know which file is being downloaded?
                // TODO: If file already partially exists. Resume downloading if possible. Or delete file and restart.
                Thread thread = new Thread(() =>
                    {
                        _client.DownloadFileAsync(new Uri(_url), _output);
                    });

                thread.Start();

                _downloads.Add(_output);
            }
        }

        private static WebRequest GetRequest(string url)
        {
            UriBuilder uri = new UriBuilder(url);
            bool hasCredentials = !string.IsNullOrEmpty(uri.UserName) && !string.IsNullOrEmpty(uri.Password);
            if (hasCredentials && ((uri.Scheme == Uri.UriSchemeHttp) || (uri.Scheme == Uri.UriSchemeHttps)))
            {
                // get the URL without user/password
                url = new UriBuilder(uri.Scheme, uri.Host, uri.Port, uri.Path, uri.Fragment).ToString();
            }

            WebRequest request = WebRequest.Create(url);
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

            if (request is HttpWebRequest)
            {
                request.Credentials = hasCredentials ? new NetworkCredential(uri.UserName, uri.Password) : CredentialCache.DefaultCredentials;

                // Some servers explode if the user agent is missing.
                // Some servers explode if the user agent is "non-standard" (e.g. "wyUpdate / " + VersionTools.FromExecutingAssembly())

                // Thus we're forced to mimic IE 9 User agent
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 6.1; en-US; Comet)";
            }
            else if (request is FtpWebRequest)
            {
                // set to binary mode (should fix crummy servers that need this spelled out to them)
                // namely ProFTPd that chokes if you request the file size without first setting "TYPE I" (binary mode)
                (request as FtpWebRequest).UseBinary = true;
            }

            return request;
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _downloadComplete = true;
        }

        #endregion
    }
}