namespace Comet.UserControls
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Forms;

    using Comet.Events;
    using Comet.Managers;
    using Comet.Structure;

    #endregion

    /// <summary>The Download User Panel.</summary>
    [ToolboxItem(false)]
    public partial class DownloadPanel : UserControl
    {
        #region Variables

        private int _downloadedFilesCount;
        private DownloadsManager _downloadManager;
        private InstallOptions _installOptions;
        private Package _package;
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
            _package = package;
            _updater = updater;

            StringBuilder _downText = new StringBuilder();
            _downText.AppendLine($"Comet is downloading updates for {package.Name}. This process could take a few minutes.");

            LDown.Text = _downText.ToString();

            _downloadManager = new DownloadsManager(package.Downloads, installOptions.DownloadFolder);
            _downloadManager.ProgressChanged += DownloadManager_ProgressChanged;
            _downloadManager.DownloadsCompleted += DownloadManager_DownloadsCompleted;
            LDownloadFiles.Text = $@"Download File/s: {_downloadManager.DownloadedFilesCount} of {package.Downloads.Count}";

            _downloadManager.Download();
        }

        #endregion

        #region Properties

        public DownloadsManager DownloadManager
        {
            get
            {
                return _downloadManager;
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///     The download manager downloads completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void DownloadManager_DownloadsCompleted(object sender, EventArgs e)
        {
            _installOptions.DownloadedFiles = _downloadManager.DownloadedFiles;
        }

        private void DownloadManager_ProgressChanged(DownloaderEventArgs e)
        {
            // double _bytesReceived = double.Parse(e.BytesReceived.ToString());
            // double _totalBytesToReceive = double.Parse(e.TotalBytesToReceive.ToString());
            /// double _percentage = (_bytesReceived / _totalBytesToReceive) * 100;
            
            _downloadedFilesCount = _downloadManager.DownloadedFilesCount;
            string _downloadedFilesString = $@"Download File/s: {_downloadedFilesCount} of {_package.Downloads.Count}";
            if (LDownloadFiles.InvokeRequired)
            {
                LDownloadFiles.BeginInvoke((MethodInvoker)delegate
                    {
                        LDownloadFiles.Text = _downloadedFilesString;
                    });
            }
            else
            {
                LDownloadFiles.Text = _downloadedFilesString;
            }

            // int _value = int.Parse(Math.Truncate(_percentage).ToString(CultureInfo.CurrentCulture));
            int _value = e.PercentDone;

            // if (progressBarFileDownload.InvokeRequired)
            // {
            // progressBarFileDownload.BeginInvoke((MethodInvoker)delegate
            // {
            // progressBarFileDownload.Value = _value;
            // });
            // }
            // else
            // {
            // progressBarFileDownload.Value = _value;
            // }
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

            // Bytes _bytesReceivedType = new Bytes(long.Parse(e.BytesReceived.ToString()))
            // {
            // Abbreviated = true
            // };
            // Bytes _totalBytesToReceiveType = new Bytes(long.Parse(e.TotalBytesToReceive.ToString()))
            // {
            // Abbreviated = true
            // };

            // string _receivedString = @"Received: " + _bytesReceivedType;
            // string _totalSizeString = @"Total Size: " + _totalBytesToReceiveType;

            // if (LBytesReceived.InvokeRequired)
            // {
            // LBytesReceived.BeginInvoke((MethodInvoker)delegate
            // {
            // LBytesReceived.Text = _receivedString;
            // });
            // }
            // else
            // {
            // LBytesReceived.Text = _receivedString;
            // }

            // if (LBytesTotalSize.InvokeRequired)
            // {
            // LBytesTotalSize.BeginInvoke((MethodInvoker)delegate
            // {
            // LBytesTotalSize.Text = _totalSizeString;
            // });
            // }
            // else
            // {
            // LBytesTotalSize.Text = _totalSizeString;
            // }
        }

        #endregion
    }
}