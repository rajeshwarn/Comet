namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Net;
    using System.Threading;

    using Comet.Structure;

    #endregion

    /// <summary>The <see cref="UpdateManager" />.</summary>
    public class UpdateManager
    {
        #region Events

        /// <summary>Download file.</summary>
        /// <param name="uri">The URI.</param>
        /// <param name="fileName">The filename output.</param>
        public static void Download(Uri uri, string fileName)
        {
            try
            {
                using (WebClient _client = new WebClient())
                {
                    _client.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
                    _client.Proxy = null;
                    _client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    _client.DownloadFileCompleted += Client_DownloadFileCompleted;

                    // _client.DownloadFileAsync(uri, fileName);
                    object _syncObject = new object();
                    lock (_syncObject)
                    {
                        _client.DownloadFileAsync(uri, fileName, _syncObject);

                        while (_client.IsBusy)
                        {
                        }

                        // This would block the thread until download completes
                        Monitor.Wait(_syncObject);
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionManager.WriteException(e.Message);
            }
        }

        private static void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            lock (e.UserState)
            {
                // Releases blocked thread.
                Monitor.Pulse(e.UserState);
            }

            ConsoleManager.WriteOutput(e.Error?.Message ?? "The file download has completed.");
        }

        private static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                long _bytesReceived = e.BytesReceived;
                long _totalBytes = e.TotalBytesToReceive;

                Bytes _received = new Bytes(_bytesReceived) { Abbreviated = true };
                Bytes _total = new Bytes(_totalBytes) { Abbreviated = true };

                if (_totalBytes == -1)
                {
                    // _total = "?";
                }

                ConsoleManager.WriteOutput("(" + e.ProgressPercentage.ToString("0") + "%) " + _received.ToString + " / " + _total.ToString);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteException(ex.Message);
            }
        }

        #endregion
    }
}