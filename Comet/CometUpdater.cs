namespace Comet
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    using Comet.Controls;
    using Comet.Enums;
    using Comet.Events;
    using Comet.Managers;
    using Comet.Properties;
    using Comet.Structure;

    #endregion

    /// <summary>The <see cref="CometUpdater" />.</summary>
    public class CometUpdater : Component
    {
        #region Variables

        private bool _autoUpdate;
        private bool _notifyUpdateAvailable;
        private bool _notifyUpdateReadyToInstall;
        private bool _notifyUser;
        private string _packagePath;
        private UpdaterState _state;
        private bool _updateAvailable;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="CometUpdater" /> class.</summary>
        /// <param name="container">The container.</param>
        public CometUpdater(IContainer container) : this()
        {
            container.Add(this);
        }

        /// <summary>Initializes a new instance of the <see cref="CometUpdater" /> class.</summary>
        /// <param name="autoUpdate">The auto update toggle.</param>
        public CometUpdater(bool autoUpdate)
        {
            _autoUpdate = autoUpdate;
        }

        /// <summary>Initializes a new instance of the <see cref="CometUpdater" /> class.</summary>
        /// <param name="packagePath">The package path.</param>
        /// <param name="autoUpdate">Auto update the application.</param>
        public CometUpdater(string packagePath, bool autoUpdate) : this()
        {
            _autoUpdate = autoUpdate;
            _packagePath = packagePath;
        }

        /// <summary>Initializes a new instance of the <see cref="CometUpdater" /> class.</summary>
        /// <param name="packagePath">The package path.</param>
        public CometUpdater(string packagePath) : this()
        {
            _packagePath = packagePath;
        }

        /// <summary>Initializes a new instance of the <see cref="CometUpdater" /> class.</summary>
        public CometUpdater()
        {
            _packagePath = null;
            _autoUpdate = false;
            _notifyUpdateAvailable = true;
            _notifyUpdateReadyToInstall = true;
            _notifyUser = false;
            _state = UpdaterState.NotChecked;
        }

        [Category("UpdaterState")]
        [Description("The update check event.")]
        public event Delegates.UpdaterStateChangedEventHandler CheckingForUpdate;

        [Category("UpdaterState")]
        [Description("The download event.")]
        public event Delegates.UpdaterStateChangedEventHandler DownloadingUpdate;

        [Category("UpdaterState")]
        [Description("The updater state change event.")]
        public event Delegates.UpdaterStateChangedEventHandler UpdaterStateChanged;

        #endregion

        #region Properties

        /// <summary>Gets the current assembly location.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string AssemblyLocation
        {
            get
            {
                return GetEntryAssembly.Location;
            }
        }

        /// <summary>Gets or sets the auto update.</summary>
        [Browsable(true)]
        [Category("Status")]
        [Description("Gets or sets the auto update.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool AutoUpdate
        {
            get
            {
                return _autoUpdate;
            }

            set
            {
                _autoUpdate = value;
            }
        }

        /// <summary>Gets the current version.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Version CurrentVersion
        {
            get
            {
                return GetEntryAssembly.GetName().Version;
            }
        }

        /// <summary>Gets the download path.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string DownloadPath
        {
            get
            {
                return UpdaterPath + @"Download\";
            }
        }

        /// <summary>Gets the extracted path.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string ExtractedPath
        {
            get
            {
                return UpdaterPath + @"Extracted\";
            }
        }

        /// <summary>Gets the change log.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string GetChangeLog
        {
            get
            {
                if (ConnectionSynchronized())
                {
                    if (Package.ChangeLog != null)
                    {
                        return Package.ChangeLog;
                    }
                    else
                    {
                        return @"No change log to load.";
                    }
                }
                else
                {
                    return @"No change log to load.";
                }
            }
        }

        /// <summary>Gets the entry assembly.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Assembly GetEntryAssembly
        {
            get
            {
                return Assembly.GetEntryAssembly();
            }
        }

        /// <summary>Gets the latest version.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Version GetLatestVersion
        {
            get
            {
                if (ConnectionSynchronized())
                {
                    if (Package.Version != null)
                    {
                        return Package.Version;
                    }
                    else
                    {
                        return new Version(0, 0, 0, 0);
                    }
                }
                else
                {
                    return new Version(0, 0, 0, 0);
                }
            }
        }

        /// <summary>Gets or sets the update available notification.</summary>
        [Browsable(true)]
        [Category("Status")]
        [Description("Gets or sets the update available notification.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool NotifyUpdateAvailable
        {
            get
            {
                return _notifyUpdateAvailable;
            }

            set
            {
                _notifyUpdateAvailable = value;
            }
        }

        /// <summary>Gets or sets the update ready to install notification.</summary>
        [Browsable(true)]
        [Category("Status")]
        [Description("Gets or sets the update ready to install notification.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool NotifyUpdateReadyToInstall
        {
            get
            {
                return _notifyUpdateReadyToInstall;
            }

            set
            {
                _notifyUpdateReadyToInstall = value;
            }
        }

        /// <summary>Gets or sets the update notify to ask user notification.</summary>
        [Browsable(true)]
        [Category("Status")]
        [Description("Gets or sets the update notify to ask user notification.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool NotifyUser
        {
            get
            {
                return _notifyUser;
            }

            set
            {
                _notifyUser = value;
            }
        }

        /// <summary>Gets the package.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Package Package
        {
            get
            {
                if (ConnectionSynchronized())
                {
                    if (!string.IsNullOrWhiteSpace(_packagePath))
                    {
                        return new Package(_packagePath);
                    }
                    else
                    {
                        return new Package();
                    }
                }
                else
                {
                    return new Package();
                }
            }
        }

        /// <summary>Gets or sets the package uri.</summary>
        [Browsable(true)]
        [Category("Status")]
        [Description("Gets or sets the package uri.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string PackagePath
        {
            get
            {
                return _packagePath;
            }

            set
            {
                _packagePath = value;
            }
        }

        /// <summary>Gets the product name.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string ProductName
        {
            get
            {
                return GetEntryAssembly.GetName().Name;
            }
        }

        /// <summary>Gets the updater state.</summary>
        [Browsable(true)]
        [Category("Status")]
        [Description("Gets the updater state")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public UpdaterState State
        {
            get
            {
                return _state;
            }
        }

        /// <summary>Gets the update available state.</summary>
        [Browsable(true)]
        [Category("Status")]
        [Description("Gets the update available state")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool UpdateAvailable
        {
            get
            {
                return _updateAvailable;
            }
        }

        /// <summary>Gets the updater path.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string UpdaterPath
        {
            get
            {
                return Path.GetTempPath() + ProductName + @"\" + ControlPanel.UpdaterFolderName + @"\";
            }
        }

        #endregion

        #region Events

        /// <summary>Checks the application for updates.</summary>
        public void CheckForUpdate()
        {
            if (NetworkManager.InternetAvailable)
            {
                if (NetworkManager.SourceExists(_packagePath))
                {
                    OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, DownloadPath, _packagePath, UpdaterState.Checking));
                }
                else
                {
                    OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, DownloadPath, _packagePath, UpdaterState.PackageNotFound));
                }
            }
            else
            {
                OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, DownloadPath, _packagePath, UpdaterState.NoConnection));
            }
        }

        /// <summary>Cleanup temporary files.</summary>
        public void Cleanup()
        {
            FileManager.DeleteDirectory(UpdaterPath);
        }

        /// <summary>Download the update package.</summary>
        public void DownloadUpdate()
        {
            // TODO: Bug: This method seems to be run twice.
            if (!ConnectionSynchronized())
            {
                CheckForUpdate();
            }

            if (_updateAvailable)
            {
                // TODO: Apparently not finding the file?
                if (NetworkManager.SourceExists(Package.Download))
                {
                    OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, DownloadPath, _packagePath, UpdaterState.Downloading));
                }
                else
                {
                    OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, DownloadPath, _packagePath, UpdaterState.PackageDataNotFound));
                }
            }
            else
            {
                OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, DownloadPath, _packagePath, UpdaterState.Updated));
            }
        }

        /// <summary>The on download update event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnCheckingForUpdate(UpdaterStateEventArgs e)
        {
            CheckingForUpdate?.Invoke(e);

            _updateAvailable = ApplicationManager.CheckForUpdate(e.AssemblyLocation, e.PackagePath);

            if (_updateAvailable)
            {
                OnUpdaterStateChanged(new UpdaterStateEventArgs(e.Assembly, e.DownloadLocation, e.PackagePath, UpdaterState.Outdated));
            }
            else
            {
                OnUpdaterStateChanged(new UpdaterStateEventArgs(e.Assembly, e.DownloadLocation, e.PackagePath, UpdaterState.Updated));
            }
        }

        /// <summary>The on download update event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnDownloadUpdate(UpdaterStateEventArgs e)
        {
            FileManager.CreateDirectory(e.DownloadLocation);

            string _contentsDownloadLink = e.Package.Download;

            var _urls = new List<string>();
            _urls.Add(e.Package.Download);

            // NetworkManager.Download(new Uri(e.DownloadLocation), e.PackagePath);
            Downloader _d = new Downloader(_urls, e.DownloadLocation);
            _d.Download();

            // TODO: Download 
            DownloadingUpdate?.Invoke(e);

            // TODO: OnCompleteDownload
            // OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, DownloadPath, _packagePath, UpdaterState.UpdateReady));

            // TODO: Notify user before installing update!
        }

        /// <summary>The on updater state changed event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnUpdaterStateChanged(UpdaterStateEventArgs e)
        {
            _state = e.State;

            switch (e.State)
            {
                case UpdaterState.NotChecked:
                    {
                        break;
                    }

                case UpdaterState.Checking:
                    {
                        OnCheckingForUpdate(e);
                        break;
                    }

                case UpdaterState.Updated:
                    {
                        break;
                    }

                case UpdaterState.Outdated:
                    {
                        if (_autoUpdate)
                        {
                            // DownloadUpdate();

                            // Download and install later.
                            // Download and install.
                            // Manually update.
                        }
                        else
                        {
                            NotificationUpdateAvailable();

                            if (_notifyUser)
                            {
                                StringBuilder _askToUpdateString = new StringBuilder();
                                _askToUpdateString.AppendLine($"A new version ({GetLatestVersion}) is available for download.");
                                _askToUpdateString.Append(Environment.NewLine);
                                _askToUpdateString.AppendLine($"Would you like to download it now?");

                                new Thread(() =>
                                    {
                                        Thread.CurrentThread.IsBackground = true;

                                        DialogResult _result = MessageBox.Show(_askToUpdateString.ToString(), Application.ProductName + @" Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                        if (_result == DialogResult.Yes)
                                        {
                                            DownloadUpdate();
                                        }
                                    }).Start();
                            }
                        }

                        break;
                    }

                case UpdaterState.Downloading:
                    {
                        OnDownloadUpdate(e);
                        break;
                    }

                case UpdaterState.NoConnection:
                    {
                        _updateAvailable = false;
                        break;
                    }

                case UpdaterState.PackageNotFound:
                    {
                        _updateAvailable = false;
                        break;
                    }

                case UpdaterState.UpdateReady:
                    {
                        if (_notifyUpdateReadyToInstall)
                        {
                            StringBuilder _updateReadyToInstall = new StringBuilder();
                            _updateReadyToInstall.AppendLine($"The update (v.{GetLatestVersion}) is ready to install.");
                            Notification.DisplayNotification(Resources.Comet, "Update Ready", _updateReadyToInstall.ToString(), ToolTipIcon.Info);
                        }

                        // TODO: Install update.
                        break;
                    }

                case UpdaterState.PackageDataNotFound:
                    {
                        _updateAvailable = false;
                        ExceptionsManager.DisplayException(new FileNotFoundException(StringManager.RemoteFileNotFound(Package.Download)));
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            if (UpdaterStateChanged != null)
            {
                UpdaterStateChanged.Invoke(e);
            }
        }

        /// <summary>Verify the connection.</summary>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        private bool ConnectionSynchronized()
        {
            if ((_state == UpdaterState.NotChecked) | (_state == UpdaterState.NoConnection) | (_state == UpdaterState.PackageNotFound))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void NotificationUpdateAvailable()
        {
            if (_notifyUpdateAvailable)
            {
                StringBuilder _updateAvailableString = new StringBuilder();
                _updateAvailableString.AppendLine($"The update (v.{GetLatestVersion}) is available for download.");
                Notification.DisplayNotification(Resources.Comet, "Update Available", _updateAvailableString.ToString(), ToolTipIcon.Info);
            }
        }

        #endregion
    }
}