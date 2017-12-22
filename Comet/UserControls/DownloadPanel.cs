namespace Comet.UserControls
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    using Comet.Structure;

    #endregion

    /// <summary>The Download User Panel.</summary>
    [ToolboxItem(false)]
    public partial class DownloadPanel : UserControl
    {
        #region Variables

        internal Downloader Downloader;

        #endregion

        #region Variables

        private InstallOptions _installOptions;
        private CometUpdater _updater;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DownloadPanel" /> class.
        /// </summary>
        /// <param name="installOptions">The install options.</param>
        /// <param name="package">The package.</param>
        /// <param name="updater">The updater.</param>
        public DownloadPanel(InstallOptions installOptions, Package package, CometUpdater updater)
        {
            InitializeComponent();

            _installOptions = installOptions;
            _updater = updater;

            StringBuilder _downText = new StringBuilder();
            _downText.AppendLine($"Comet is downloading updates for {package.Name}. This process could take a few minutes.");

            LDown.Text = _downText.ToString();

            var _urls = new List<string>
                {
                    package.Download
                };

            LDownloadFiles.Text = $@"Download File/s: {_urls.Count} of {_urls.Count}";

            Downloader = new Downloader(_urls, installOptions.DownloadFolder);
            Downloader._client.DownloadProgressChanged += DownloadProgressChanged;
            Downloader._client.DownloadFileCompleted += DownloadFileCompleted;

            new Thread(BeginDownload).Start();
        }

        #endregion

        #region Events

        /// <summary>The file download begin.</summary>
        private void BeginDownload()
        {
            Downloader.Download();

            while (!Downloader.DownloadComplete)
            {
                // Interval to repeat Wait for downloader to finish.
                Thread.Sleep(100);
            }

            _installOptions.DownloadedFile = Downloader.Downloads[0];
        }

        /// <summary>
        ///     The file download completed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (_updater.AutoUpdate)
            {
                // Do nothing here.
            }
        }

        /// <summary>
        ///     The file download progress changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double _bytesReceived = double.Parse(e.BytesReceived.ToString());
            double _totalBytesToReceive = double.Parse(e.TotalBytesToReceive.ToString());
            double _percentage = (_bytesReceived / _totalBytesToReceive) * 100;

            int _value = int.Parse(Math.Truncate(_percentage).ToString(CultureInfo.CurrentCulture));
            if (progressBarFileDownload.InvokeRequired)
            {
                progressBarFileDownload.BeginInvoke((MethodInvoker)delegate
                    {
                        progressBarFileDownload.Value = _value;
                    });
            }
            else
            {
                progressBarFileDownload.Value = _value;
            }

            string _progressString = $@"Progress: {_value}%";
            if (LProgress.InvokeRequired)
            {
                LProgress.BeginInvoke((MethodInvoker)delegate
                    {
                        LProgress.Text = _progressString;
                    });
            }
            else
            {
                LProgress.Text = _progressString;
            }

            Bytes _bytesReceivedType = new Bytes(long.Parse(e.BytesReceived.ToString()))
                {
                    Abbreviated = true
                };
            Bytes _totalBytesToReceiveType = new Bytes(long.Parse(e.TotalBytesToReceive.ToString()))
                {
                    Abbreviated = true
                };

            string _receivedString = @"Received: " + _bytesReceivedType;
            string _totalSizeString = @"Total Size: " + _totalBytesToReceiveType;

            if (LBytesReceived.InvokeRequired)
            {
                LBytesReceived.BeginInvoke((MethodInvoker)delegate
                    {
                        LBytesReceived.Text = _receivedString;
                    });
            }
            else
            {
                LBytesReceived.Text = _receivedString;
            }

            if (LBytesTotalSize.InvokeRequired)
            {
                LBytesTotalSize.BeginInvoke((MethodInvoker)delegate
                    {
                        LBytesTotalSize.Text = _totalSizeString;
                    });
            }
            else
            {
                LBytesTotalSize.Text = _totalSizeString;
            }
        }

        #endregion
    }
}