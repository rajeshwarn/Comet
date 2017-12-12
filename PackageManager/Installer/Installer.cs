namespace Comet
{
    #region Namespace

    using System;
    using System.Net;

    #endregion

    public class Installer
    {
        #region Events

        public static void DownloadData()
        {
            // Verify internet connection

            // Todo: Download file or multiple packages?
        }

        /// <summary>Get the url status code.</summary>
        /// <param name="url">The url.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>The <see cref="HttpStatusCode" />.</returns>
        public static HttpStatusCode GetStatusCode(string url, int timeout = 5000)
        {
            if (IsURLFormatted(url))
            {
                HttpWebResponse _response = null;
                HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(url);
                _request.Method = "HEAD";
                _request.Timeout = timeout;
                _request.AllowAutoRedirect = false;

                try
                {
                    _response = (HttpWebResponse)_request.GetResponse();
                    return _response.StatusCode;
                }
                catch
                {
                    return HttpStatusCode.Conflict;
                }
                finally
                {
                    if (_response != null)
                    {
                        _response.Close();
                    }
                }
            }
            else
            {
                return HttpStatusCode.Conflict;
            }
        }

        /// <summary>Determines whether the url is well formatted.</summary>
        /// <param name="url">The url.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsURLFormatted(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }

        /// <summary>Determines whether the url source is ok.</summary>
        /// <param name="url">The url.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool SourceExists(string url, int timeout = 5000)
        {
            return GetStatusCode(url, timeout) == HttpStatusCode.OK;
        }

        #endregion
    }
}