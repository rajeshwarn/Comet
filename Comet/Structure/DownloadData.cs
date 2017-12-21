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

        private static readonly List<char> invalidFilenameChars = new List<char>(Path.GetInvalidFileNameChars());

        private string _fileName;

        private long _size;

        private long _start;

        private Stream _stream;

        private WebResponse _webResponse;

        #endregion

        #region Properties

        public Stream DownloadStream
        {
            get
            {
                if (_start == _size)
                {
                    return Stream.Null;
                }

                return _stream ?? (_stream = _webResponse.GetResponseStream());
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        public bool IsProgressKnown
        {
            get
            {
                // If the size of the remote url is '-1', then we can't determine it so we don't know progress information.
                return _size > -1;
            }
        }

        public int PercentDone
        {
            get
            {
                if (_size > 0)
                {
                    return (int)((_start * 100) / _size);
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

        #endregion

        #region Events

        public static DownloadData Create(string url, string destinationDirectory)
        {
            DownloadData downloadData = new DownloadData();
            WebRequest req = GetRequest(url);

            try
            {
                if (req is FtpWebRequest)
                {
                    // get the file size for FTP files

                    // new request for downloading the FTP file
                    req = GetRequest(url);
                    downloadData._webResponse = req.GetResponse();
                }
                else
                {
                    downloadData._webResponse = req.GetResponse();
                    downloadData.GetFileSize();
                }

                // downloadData._size = GetFileSize(downloadData._webResponse);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error downloading \"{0}\": {1}", url, e.Message), e);
            }

            // Check to make sure the response isn't an error. If it is this method
            // will throw exceptions.
            ValidateResponse(downloadData._webResponse, url);

            // Take the name of the file given to use from the web server.
            string fileName = downloadData._webResponse.Headers["Content-Disposition"];

            if (fileName != null)
            {
                int fileLoc = fileName.IndexOf("filename=", StringComparison.OrdinalIgnoreCase);

                if (fileLoc != -1)
                {
                    // go past "filename="
                    fileLoc += 9;

                    if (fileName.Length > fileLoc)
                    {
                        // trim off an ending semicolon if it exists
                        int end = fileName.IndexOf(';', fileLoc);

                        if (end == -1)
                        {
                            end = fileName.Length - fileLoc;
                        }
                        else
                        {
                            end -= fileLoc;
                        }

                        fileName = fileName.Substring(fileLoc, end).Trim();
                    }
                    else
                    {
                        fileName = null;
                    }
                }
                else
                {
                    fileName = null;
                }
            }

            if (string.IsNullOrEmpty(fileName))
            {
                // brute force the filename from the url
                fileName = Path.GetFileName(downloadData._webResponse.ResponseUri.LocalPath);
            }

            // trim out non-standard filename characters
            if (!string.IsNullOrEmpty(fileName) && (fileName.IndexOfAny(invalidFilenameChars.ToArray()) != -1))
            {
                // make a new string builder (with at least one bad character)
                StringBuilder newText = new StringBuilder(fileName.Length - 1);

                // remove the bad characters
                for (var i = 0; i < fileName.Length; i++)
                {
                    if (invalidFilenameChars.IndexOf(fileName[i]) == -1)
                    {
                        newText.Append(fileName[i]);
                    }
                }

                fileName = newText.ToString().Trim();
            }

            // if filename *still* is null or empty, then generate some random temp filename
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Path.GetFileName(Path.GetTempFileName());
            }

            string downloadTo = Path.Combine(destinationDirectory, fileName);

            downloadData._fileName = downloadTo;

            // If we don't know how big the file is supposed to be,
            // we can't resume, so delete what we already have if something is on disk already.
            if (!downloadData.IsProgressKnown && File.Exists(downloadTo))
            {
                File.Delete(downloadTo);
            }

            if (downloadData.IsProgressKnown && File.Exists(downloadTo))
            {
                // We only support resuming on http requests
                if (!(downloadData.Response is HttpWebResponse))
                {
                    File.Delete(downloadTo);
                }
                else
                {
                    // Try and start where the file on disk left off
                    downloadData._start = new FileInfo(downloadTo).Length;

                    // If we have a file that's bigger than what is online, then something 
                    // strange happened. Delete it and start again.
                    if (downloadData._start > downloadData._size)
                    {
                        File.Delete(downloadTo);
                    }
                    else if (downloadData._start < downloadData._size)
                    {
                        // Try and resume by creating a new request with a new start position
                        downloadData._webResponse.Close();
                        req = GetRequest(url);
                        ((HttpWebRequest)req).AddRange((int)downloadData._start);
                        downloadData._webResponse = req.GetResponse();

                        if (((HttpWebResponse)downloadData.Response).StatusCode != HttpStatusCode.PartialContent)
                        {
                            // They didn't support our resume request. 
                            File.Delete(downloadTo);
                            downloadData._start = 0;
                        }
                    }
                }
            }

            return downloadData;
        }

        /// <summary>
        ///     Get the file size.
        /// </summary>
        /// <param name="response">The web response.</param>
        /// <returns>
        ///     <see cref="long" />
        /// </returns>
        public static long GetFileSize(WebResponse response)
        {
            long _size = -1;

            if (response == null)
            {
                return _size;
            }

            try
            {
                _size = response.ContentLength;
            }
            catch (Exception e)
            {
                _size = -1;
            }

            return _size;
        }

        public void Close()
        {
            _webResponse.Close();
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

        // Checks whether a WebResponse is an error.
        private static void ValidateResponse(WebResponse response, string url)
        {
            if (response is HttpWebResponse)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)response;

                // If it's an HTML page, it's probably an error page.
                if ((httpResponse.StatusCode == HttpStatusCode.NotFound) || httpResponse.ContentType.Contains("text/html"))
                {
                    throw new Exception(
                        string.Format("Could not download \"{0}\" - a web page was returned from the web server.", url));
                }
            }
            else if (response is FtpWebResponse)
            {
                if (((FtpWebResponse)response).StatusCode == FtpStatusCode.ConnectionClosed)
                {
                    throw new Exception(string.Format("Could not download \"{0}\" - FTP server closed the connection.", url));
                }
            }

            // FileWebResponse doesn't have a status code to check.
        }

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