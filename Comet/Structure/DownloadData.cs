namespace Comet.Structure
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Text;

    #endregion

    public class DownloadData
    {
        #region Variables

        private string _destinationDirectory;
        private string _outputFileName;
        private string _outputFilePath;
        private long _size;
        private long _start;
        private Stream _stream;
        private Uri _url;
        private WebRequest _webRequest;
        private WebResponse _webResponse;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="DownloadData" /> class.</summary>
        /// <param name="url">The url.</param>
        /// <param name="destinationDirectory">The destination Directory.</param>
        public DownloadData(Uri url, string destinationDirectory)
        {
            _url = url;
            _destinationDirectory = destinationDirectory;
        }

        #endregion

        #region Properties

        // public long BytesReceived { get; set; }
        public string DestinationDirectory
        {
            get
            {
                return _destinationDirectory;
            }

            set
            {
                _destinationDirectory = value;
            }
        }

        // public bool DownloadComplete { get; set; }
        public Stream DownloadStream
        {
            get
            {
                if (StartPoint == _size)
                {
                    return Stream.Null;
                }

                return _stream ?? (_stream = _webResponse.GetResponseStream());
            }
        }

        // public string FileName { get; set; }
        public bool IsProgressKnown
        {
            get
            {
                // If the size of the remote url is '-1', then we can't determine it so we don't know progress information.
                return _size > -1;
            }
        }

        public string OutputFileName
        {
            get
            {
                return _outputFileName;
            }
        }

        public string OutputFilePath
        {
            get
            {
                return _outputFilePath;
            }
        }

        public int PercentDone
        {
            get
            {
                if (_size > 0)
                {
                    return (int)((StartPoint * 100) / _size);
                }

                return 0;
            }
        }

        public WebResponse Response
        {
            get
            {
                return _webResponse;
            }

            set
            {
                _webResponse = value;
            }
        }

        public long StartPoint
        {
            get
            {
                return _start;
            }

            set
            {
                _start = value;
            }
        }

        public long TotalDownloadSize
        {
            get
            {
                return _size;
            }
        }

        public Uri Url
        {
            get
            {
                return _url;
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
        public static long TryGetFileSize(Uri url)
        {
            WebRequest _webRequest = GetRequest(url.OriginalString);
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
                throw new Exception($"Error downloading \"{url.OriginalString}\": {e.Message}", e);
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

            _webResponse.Close();
            return _size;
        }

        /// <summary>
        ///     Tries to retrieve the file name from the url.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string TryGetName(Uri url)
        {
            var _invalidFilenameChars = new List<char>(Path.GetInvalidFileNameChars());

            WebRequest _webRequest = GetRequest(url.OriginalString);
            WebResponse _webResponse;

            _webResponse = _webRequest.GetResponse();

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

            _webResponse.Close();
            return _fileName;
        }

        /// <summary>
        ///     Close the download data.
        /// </summary>
        public void Close()
        {
            _webResponse.Close();
        }

        /// <summary>
        ///     Create the data.
        /// </summary>
        public void Create()
        {
            _webRequest = GetRequest(_url.OriginalString);

            try
            {
                if (_webRequest is FtpWebRequest)
                {
                    // Get the file size for ftp files.
                    _webRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                    _webResponse = _webRequest.GetResponse();
                    GetFileSize();

                    // New request for downloading the FTP file
                    _webRequest = GetRequest(_url.OriginalString);
                    _webResponse = _webRequest.GetResponse();
                }
                else
                {
                    _webResponse = _webRequest.GetResponse();
                    GetFileSize();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error downloading \"{_url.OriginalString}\": {e.Message}", e);
            }

            ValidateResponse(_webResponse, _url);

            _outputFileName = TryGetName(_url);
            _outputFilePath = _destinationDirectory + _outputFileName;

            // If we don't know how big the file is supposed to be, we can't resume, so delete what we already have if something is on disk already.
            if (!IsProgressKnown && File.Exists(_outputFilePath))
            {
                File.Delete(_outputFilePath);
            }

            if (IsProgressKnown && File.Exists(_outputFilePath))
            {
                // Resume on HTTP requests support
                if (!(_webResponse is HttpWebResponse))
                {
                    File.Delete(_outputFilePath);
                }
                else
                {
                    // Try and start where the file on disk left off.
                    StartPoint = new FileInfo(_outputFilePath).Length;

                    // If we have a file that's bigger than what is online, then something strange happened.
                    if (StartPoint > _size)
                    {
                        File.Delete(_outputFilePath);
                    }
                    else if (StartPoint < _size)
                    {
                        // Try and resume by creating a new request and with a new start position.
                        _webResponse.Close();
                        _webRequest = GetRequest(_url.OriginalString);

                        ((HttpWebRequest)_webRequest).AddRange((int)StartPoint);
                        _webResponse = _webRequest.GetResponse();

                        if (((HttpWebResponse)Response).StatusCode != HttpStatusCode.PartialContent)
                        {
                            // Resume request not supported.
                            File.Delete(_outputFilePath);
                            StartPoint = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Creates a web request.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>
        ///     <see cref="WebRequest" />
        /// </returns>
        private static WebRequest GetRequest(string url)
        {
            UriBuilder _uriBuilder = new UriBuilder(url);
            bool hasCredentials = !string.IsNullOrEmpty(_uriBuilder.UserName) && !string.IsNullOrEmpty(_uriBuilder.Password);
            if (hasCredentials && ((_uriBuilder.Scheme == Uri.UriSchemeHttp) || (_uriBuilder.Scheme == Uri.UriSchemeHttps)))
            {
                // get the URL without user/password
                url = new UriBuilder(_uriBuilder.Scheme, _uriBuilder.Host, _uriBuilder.Port, _uriBuilder.Path, _uriBuilder.Fragment).ToString();
            }

            WebRequest _webRequest = WebRequest.Create(url);
            _webRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

            if (_webRequest is HttpWebRequest)
            {
                _webRequest.Credentials = hasCredentials ? new NetworkCredential(_uriBuilder.UserName, _uriBuilder.Password) : CredentialCache.DefaultCredentials;

                // Some servers explode if the user agent is missing.
                // Some servers explode if the user agent is "non-standard"

                // Thus we're forced to mimic IE 9 User agent
                ((HttpWebRequest)_webRequest).UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 6.1; en-US; Comet)";
            }
            else if (_webRequest is FtpWebRequest)
            {
                // set to binary mode (should fix crummy servers that need this spelled out to them)
                // namely ProFTPd that chokes if you request the file size without first setting "TYPE I" (binary mode)
                (_webRequest as FtpWebRequest).UseBinary = true;
            }

            return _webRequest;
        }

        /// <summary>
        ///     Validate the web response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="url">The url.</param>
        private static void ValidateResponse(WebResponse response, Uri url)
        {
            if (response is HttpWebResponse)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)response;

                // If it's an HTML page, it's probably an error page.
                if ((httpResponse.StatusCode == HttpStatusCode.NotFound) || httpResponse.ContentType.Contains("text/html"))
                {
                    throw new Exception(
                        string.Format("Could not download \"{0}\" - a web page was returned from the web server.", url.OriginalString));
                }
            }
            else if (response is FtpWebResponse)
            {
                if (((FtpWebResponse)response).StatusCode == FtpStatusCode.ConnectionClosed)
                {
                    throw new Exception(string.Format("Could not download \"{0}\" - FTP server closed the connection.", url.OriginalString));
                }
            }
        }

        /// <summary>
        ///     Get the file size.
        /// </summary>
        private void GetFileSize()
        {
            if (_webResponse != null)
            {
                try
                {
                    _size = _webResponse.ContentLength;
                }
                catch
                {
                    // File size couldn't be determined
                    _size = -1;
                }
            }
        }

        #endregion
    }
}