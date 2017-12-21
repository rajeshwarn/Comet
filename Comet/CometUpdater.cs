﻿namespace Comet
{
    #region Namespace

    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    using Comet.Controls;
    using Comet.Enums;
    using Comet.Events;
    using Comet.Managers;
    using Comet.Properties;
    using Comet.Structure;

    #endregion

    // TODO: Handle timeout to package connection exception.
    #endregion

    /// <summary>The <see cref="CometUpdater" />.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Description("The Comet Updater")]
    [ToolboxBitmap(typeof(CometUpdater), "Resources.Comet.bmp")]
    [ToolboxItem(true)]
    public class CometUpdater : Component
    {
        #region Variables

        private bool _autoUpdate;

        private BackgroundWorker _backgroundUpdateChecker;
        private InstallOptions _installOptions;
        private bool _notifyUpdateAvailable;
        private bool _notifyUpdateReadyToInstall;
        private string _packagePath;
        private ProgressDialog _progressDialog;
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
        /// <param name="packagePath">The package path.</param>
        /// <param name="installPath">The install Path.</param>
        public CometUpdater(string packagePath, string installPath) : this()
        {
            _packagePath = packagePath;
            _installOptions = new InstallOptions(installPath);
        }

        /// <summary>Initializes a new instance of the <see cref="CometUpdater" /> class.</summary>
        /// <param name="packagePath">The package path.</param>
        /// <param name="installPath">The install Path.</param>
        /// <param name="autoUpdate">Auto update the application.</param>
        public CometUpdater(string packagePath, string installPath, bool autoUpdate) : this()
        {
            _autoUpdate = autoUpdate;
            _packagePath = packagePath;
            _installOptions = new InstallOptions(installPath);
        }

        /// <summary>Initializes a new instance of the <see cref="CometUpdater" /> class.</summary>
        public CometUpdater()
        {
            _packagePath = null;
            _autoUpdate = false;
            _notifyUpdateAvailable = true;
            _notifyUpdateReadyToInstall = true;
            _state = UpdaterState.NotChecked;
            _installOptions = new InstallOptions(string.Empty);

            _backgroundUpdateChecker = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true
                };

            _backgroundUpdateChecker.DoWork += BackgroundUpdateCheckerDoWork;
            _backgroundUpdateChecker.RunWorkerCompleted += BackgroundUpdateChecker_Completed;
        }

        [Category("UpdaterState")]
        [Description("The update check event.")]
        public event Delegates.UpdaterStateChangedEventHandler CheckingForUpdate;

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

        /// <summary>
        ///     verify the connection state.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool Connected
        {
            get
            {
                bool _connected;

                if ((_state == UpdaterState.NotChecked) | (_state == UpdaterState.NoConnection) | (_state == UpdaterState.PackageNotFound) | (_state == UpdaterState.PackageDataNotFound))
                {
                    _connected = false;
                }
                else
                {
                    _connected = true;
                }

                return _connected;
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
                return _installOptions.DownloadFolder;
            }
        }

        /// <summary>Gets the change log.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string GetChangeLog
        {
            get
            {
                if (Connected)
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
                if (Connected)
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

        /// <summary>Gets the install files path.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string InstallFilesFolder
        {
            get
            {
                return _installOptions.InstallFilesFolder;
            }
        }

        /// <summary>Gets the current install options.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public InstallOptions InstallOptions
        {
            get
            {
                return _installOptions;
            }
        }

        /// <summary>Gets the install path.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string InstallPath
        {
            get
            {
                return _installOptions.InstallPath;
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

        /// <summary>Gets the package.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Package Package
        {
            get
            {
                if (Connected)
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
        public string WorkingPath
        {
            get
            {
                return _installOptions.WorkingFolder;
            }
        }

        #endregion

        #region Events

        /// <summary>Checks the application for updates.</summary>
        public void CheckForUpdate()
        {
            if (!_backgroundUpdateChecker.IsBusy)
            {
                _backgroundUpdateChecker.RunWorkerAsync();
            }
        }

        /// <summary>
        ///     Notify the user an update is available.
        /// </summary>
        public void NotificationUpdateAvailable()
        {
            if (!_notifyUpdateAvailable)
            {
                return;
            }

            StringBuilder _updateAvailableString = new StringBuilder();
            _updateAvailableString.AppendLine($"The update (v.{GetLatestVersion}) is available for download.");
            Notification.DisplayNotification(Resources.Comet, "Update Available", _updateAvailableString.ToString(), ToolTipIcon.Info);
        }

        /// <summary>
        ///     Notify the user the update is ready to install.
        /// </summary>
        public void NotificationUpdateReadyToInstall()
        {
            if (!_notifyUpdateReadyToInstall)
            {
                return;
            }

            StringBuilder _updateReadyToInstall = new StringBuilder();
            _updateReadyToInstall.AppendLine($"The update (v.{GetLatestVersion}) is ready to install.");
            Notification.DisplayNotification(Resources.Comet, "Update Ready", _updateReadyToInstall.ToString(), ToolTipIcon.Info);
        }

        /// <summary>
        ///     Checking for update.
        /// </summary>
        /// <param name="e">The sender.</param>
        protected virtual void OnCheckingForUpdate(UpdaterStateEventArgs e)
        {
            CheckingForUpdate?.Invoke(e);

            if (NetworkManager.InternetAvailable)
            {
                if (NetworkManager.SourceExists(_packagePath))
                {
                    if (ApplicationManager.CheckForUpdate(AssemblyLocation, _packagePath))
                    {
                        _updateAvailable = true;
                        _state = UpdaterState.Outdated;
                        CheckingForUpdate?.Invoke(e);
                    }
                    else
                    {
                        _updateAvailable = false;
                        _state = UpdaterState.Updated;
                        CheckingForUpdate?.Invoke(e);
                    }
                }
                else
                {
                    _state = UpdaterState.PackageNotFound;
                    _updateAvailable = false;
                    CheckingForUpdate?.Invoke(e);
                    VisualExceptionDialog.Show(new FileNotFoundException(StringManager.RemoteFileNotFound(Package.Download)));
                }
            }
            else
            {
                _state = UpdaterState.NoConnection;
                _updateAvailable = false;
                CheckingForUpdate?.Invoke(e);
            }

            _backgroundUpdateChecker.CancelAsync();
        }

        /// <summary>Checking for update complete.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void BackgroundUpdateChecker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_autoUpdate)
            {
                // TODO: Automatically continue updating.
                _progressDialog = new ProgressDialog(_installOptions, Package, CurrentVersion, this);
                _progressDialog.Show();
            }
            else
            {
                _progressDialog = new ProgressDialog(_installOptions, Package, CurrentVersion, this);

                // TODO: Prevent opening multiple progress dialogs.
                _progressDialog.Show();
            }
        }

        /// <summary>
        ///     Background worker checks for updates.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void BackgroundUpdateCheckerDoWork(object sender, DoWorkEventArgs e)
        {
            OnCheckingForUpdate(new UpdaterStateEventArgs(GetEntryAssembly, _installOptions, _packagePath, _state));
        }

        #endregion
    }
}