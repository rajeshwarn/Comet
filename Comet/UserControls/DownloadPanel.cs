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

    /// <summary>The download.</summary>
    [ToolboxItem(false)]
    public partial class DownloadPanel : UserControl
    {
        #region Variables

        internal BetterDownloader _downloader;

        #endregion

        #region Variables

        // private string _downloadedFile;
        private InstallOptions _installOptions;

        private CometUpdater _updater;

        #endregion

        #region Constructors

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

            LCount.Text = @"Count: " + _urls.Count;

            _downloader = new BetterDownloader(_urls, installOptions.DownloadFolder);
            _downloader._client.DownloadProgressChanged += DownloadProgressChanged;
            _downloader._client.DownloadFileCompleted += DownloadFileCompleted;

            new Thread(() =>
                {
                    BeginDownload(installOptions, package);
                }).Start();
        }

        #endregion

        #region Events

        private void BeginDownload(InstallOptions installOptions, Package package)
        {
            Logger.Log(new Logger("Logs", ".xml", "Log", Logger.WriteMode.XML), "Started Download.");

            _downloader.Download();

            while (!_downloader.DownloadComplete)
            {
                // Wait for downloader to finish.
                Thread.Sleep(100);
            }

            _installOptions.DownloadedFile = _downloader.Downloads[0];

            // _installOptions.DownloadedFile = _downloader.DownloadingTo;
            Logger.Log(new Logger("Logs", ".xml", "Log", Logger.WriteMode.XML), $"Downloaded file: {_downloader.Downloads[0]}");
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (_updater.AutoUpdate)
            {
            }
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double _receive = double.Parse(e.BytesReceived.ToString());
            double _fileSize = double.Parse(e.TotalBytesToReceive.ToString());

            double _percentage = (_receive / _fileSize) * 100;

            string _status = $"Downloaded {string.Format("0:0.##", _percentage)}";
            int _value = int.Parse(Math.Truncate(_percentage).ToString(CultureInfo.CurrentCulture));

            if (progressBar1.InvokeRequired)
            {
                progressBar1.BeginInvoke((MethodInvoker)delegate
                    {
                        progressBar1.Value = _value;
                    });
            }
            else
            {
                progressBar1.Value = _value;
            }

            Bytes _receive1 = new Bytes(long.Parse(e.BytesReceived.ToString()))
                {
                    Abbreviated = true
                };

            Bytes _fileSize1 = new Bytes(long.Parse(e.TotalBytesToReceive.ToString()))
                {
                    Abbreviated = true
                };

            if (label1.InvokeRequired)
            {
                label1.BeginInvoke((MethodInvoker)delegate
                    {
                        label1.Text = @"Receive: " + _receive1;
                    });
            }
            else
            {
                label1.Text = @"Receive: " + _receive1;
            }

            if (label2.InvokeRequired)
            {
                label2.BeginInvoke((MethodInvoker)delegate
                    {
                        label2.Text = @"File size: " + _fileSize1;
                    });
            }
            else
            {
                label2.Text = @"File size: " + _fileSize1;
            }
        }

        #endregion
    }
}