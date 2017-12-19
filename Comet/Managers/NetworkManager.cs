namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Net;
    using System.Net.Cache;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    using Comet.PInvoke;

    #endregion

    /// <summary>The <see cref="NetworkManager" />.</summary>
    public class NetworkManager
    {
        #region Properties

        /// <summary>Get the connection state using wininet.dll API.</summary>
        /// <returns>The <see cref="bool" />.</returns>
        [Description("Indicates whether any internet connection is available.")]
        public static bool InternetAvailable
        {
            get
            {
                try
                {
                    int flags;
                    return Wininet.InternetGetConnectedState(out flags, 0);
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>Get the connection state of the local network.</summary>
        /// <returns>The <see cref="bool" />.</returns>
        [Description("Indicates whether any network connection is available.")]
        public static bool NetworkAvailable
        {
            get
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
        }

        #endregion

        #region Events

        /// <summary>Gets the connection state using DNS.</summary>
        /// <param name="hostname">The hostname.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool GetConnectionState(string hostname)
        {
            try
            {
                Dns.GetHostEntry(hostname);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Get connection state using TCP.</summary>
        /// <param name="hostname">The hostname.</param>
        /// <param name="port">The port.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool GetConnectionState(string hostname, int port)
        {
            try
            {
                TcpClient _client = new TcpClient(hostname, port);
                _client.Close();
                return true;
            }
            catch
            {
                return false;
            }
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
                HttpWebRequest _request = (HttpWebRequest)CreateWebRequest(url);
                _request.Method = "HEAD";
                _request.Timeout = timeout;
                _request.AllowAutoRedirect = false;
                _request.UserAgent = UserAgent;

                try
                {
                    _response = (HttpWebResponse)_request.GetResponse();
                    return _response.StatusCode;
                }
                catch (Exception)
                {
                    return HttpStatusCode.Conflict;
                }
                finally
                {
                    _response?.Close();
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
            if (string.IsNullOrEmpty(url))
            {
                throw new NoNullAllowedException("The URL is null or empty.");
            }

            bool _formatted;

            try
            {
                Uri _uri = new Uri(url, UriKind.Absolute);

                // _uri.Scheme == Uri.UriSchemeHttp || _uri.Scheme == Uri.UriSchemeHttps;
                if (_uri.Scheme != Uri.UriSchemeHttps)
                {
                    // TODO: Notify user not recommended to download from such urls ask to continue.
                    // throw new SecurityException("Using unsafe connections to update from is not allowed.");
                }

                _formatted = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            }
            catch (UriFormatException)
            {
                _formatted = false;
            }

            return _formatted;
        }

        /// <summary>Determines whether the url source is ok.</summary>
        /// <param name="url">The url.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool SourceExists(string url, int timeout = 5000)
        {
            return GetStatusCode(url, timeout) == HttpStatusCode.OK;
        }

        /// <summary>
        ///     Create a web request.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>
        ///     <see cref="WebRequest" />
        /// </returns>
        private static WebRequest CreateWebRequest(string url)
        {
            UriBuilder _uriBuilder = new UriBuilder(url);
            bool _hasCredentials = !string.IsNullOrEmpty(_uriBuilder.UserName) && !string.IsNullOrEmpty(_uriBuilder.Password);
            if (_hasCredentials && ((_uriBuilder.Scheme == Uri.UriSchemeHttp) || _uriBuilder.Scheme == Uri.UriSchemeHttps))
            {
                // Receive the URL without user/password
                url = new UriBuilder(_uriBuilder.Scheme, _uriBuilder.Host, _uriBuilder.Port, _uriBuilder.Path, _uriBuilder.Fragment).ToString();
            }

            WebRequest request = WebRequest.Create(url);
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

            if (request is HttpWebRequest)
            {
                request.Credentials = _hasCredentials ? new NetworkCredential(_uriBuilder.UserName, _uriBuilder.Password) : CredentialCache.DefaultCredentials;

                // TODO: Some servers explode if the user agent is missing.
                // TODO: Some servers explode if the user agent is "non-standard"
                ((HttpWebRequest)request).UserAgent = UserAgent;
            }
            else if (request is FtpWebRequest)
            {
                (request as FtpWebRequest).UseBinary = true;
            }

            return request;
        }

        private static string UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 6.1; en-US; Comet)";

        /// <summary>
        ///     Validates the http response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="url">The url.</param>
        private static void ValidateHTTPResponse(WebResponse response, string url)
        {
            if (response is HttpWebResponse)
            {
                HttpWebResponse _httpResponse = (HttpWebResponse)response;

                if (_httpResponse.StatusCode == HttpStatusCode.NotFound || _httpResponse.ContentType.Contains("text/html"))
                {
                    throw new Exception($"Could not download \"{url}\"- a web page was returned from the web server.");
                }
            }
            else if (response is FtpWebResponse)
            {
                if (((FtpWebResponse)response).StatusCode == FtpStatusCode.ConnectionClosed)
                {
                    throw new Exception($"Could not download \"{url}\" - FTP server closed the connection.");
                }
            }
        }

        #endregion
    }
}