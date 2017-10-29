namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Net;
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