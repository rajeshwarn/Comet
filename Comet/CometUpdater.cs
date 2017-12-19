namespace Comet
{
    #region Namespace

    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
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

    // TODO: Catch timeout to package connection exception.
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

        private BackgroundWorker _bw;
        private Downloader _downloader;
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

            InitializeProgressDialog();

            _bw = new BackgroundWorker();
            _bw.WorkerSupportsCancellation = true;
            _bw.DoWork += BW_DoWork;
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
            if (!_bw.IsBusy)
            {
                _bw.RunWorkerAsync();
            }
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
                if (NetworkManager.SourceExists(Package.Download))
                {
                    OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, _installOptions, _packagePath, UpdaterState.Downloading));
                }
                else
                {
                    OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, _installOptions, _packagePath, UpdaterState.PackageDataNotFound));
                }
            }
            else
            {
                OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, _installOptions, _packagePath, UpdaterState.Updated));
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
                OnUpdaterStateChanged(new UpdaterStateEventArgs(e.Assembly, _installOptions, e.PackagePath, UpdaterState.Outdated));
            }
            else
            {
                OnUpdaterStateChanged(new UpdaterStateEventArgs(e.Assembly, _installOptions, e.PackagePath, UpdaterState.Updated));
            }
        }

        /// <summary>The on download update event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnDownloadUpdate(UpdaterStateEventArgs e)
        {
            // TODO: Move to progress dialog to do tasks there to call.
            var _urls = new List<string>
                {
                    e.Package.Download
                };

            _downloader = new Downloader(_urls, e.InstallOptions.DownloadFolder);

            // _downloader.ProgressChanged += Downloader_ProgressChanged;
            _downloader.Download();
            DownloadingUpdate?.Invoke(e);

            while (!_downloader.DownloadComplete)
            {
                // Wait for downloader to finish.
                Thread.Sleep(1000);
            }

            _installOptions.DownloadedFile = _downloader.DownloadingTo;

            OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, _installOptions, _packagePath, UpdaterState.Downloaded));
        }

        /// <summary>The on updater state changed event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnUpdaterStateChanged(UpdaterStateEventArgs e)
        {
            _state = e.State;
            UpdaterStateChanged?.Invoke(e);

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
                        _updateAvailable = false;
                        _bw.CancelAsync();
                        break;
                    }

                case UpdaterState.Outdated:
                    {
                        NotificationUpdateAvailable();
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
                        _bw.CancelAsync();
                        break;
                    }

                case UpdaterState.PackageNotFound:
                    {
                        _updateAvailable = false;
                        _bw.CancelAsync();
                        break;
                    }

                case UpdaterState.Downloaded:
                    {
                        NotificationUpdateReadyToInstall();
                        break;
                    }

                case UpdaterState.PackageDataNotFound:
                    {
                        _updateAvailable = false;
                        _bw.CancelAsync();
                        VisualExceptionDialog.Show(new FileNotFoundException(StringManager.RemoteFileNotFound(Package.Download)));
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }
        }

        private void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            CheckForUpdate();

            _installOptions.Verify();

            if (NetworkManager.InternetAvailable)
            {
                if (NetworkManager.SourceExists(_packagePath))
                {
                    OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, _installOptions, _packagePath, UpdaterState.Checking));
                }
                else
                {
                    OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, _installOptions, _packagePath, UpdaterState.PackageNotFound));
                }
            }
            else
            {
                OnUpdaterStateChanged(new UpdaterStateEventArgs(GetEntryAssembly, _installOptions, _packagePath, UpdaterState.NoConnection));
            }
        }

        /// <summary>Cleanup the temporary install files.</summary>
        /// <param name="installOptions">The install Options.</param>
        private void Cleanup(InstallOptions installOptions)
        {
            FileManager.DeleteDirectory(installOptions.DownloadFolder);

            // FileManager.DeleteDirectory(installOptions.InstallFilesFolder);
            // FileManager.DeleteDirectory(installOptions.WorkingFolder);
        }

        /// <summary>
        ///     Compile the installer.
        /// </summary>
        /// <param name="installOptions"></param>
        private void CompileInstaller(InstallOptions installOptions)
        {
            // TODO: Set resource install folder option and compile with it.

            // Ask to close and restart to update files with installer
            // var _references = new List<string>
            // {
            // "System.dll",
            // "System.Windows.Forms.dll"
            // };

            // ResourcesManager.CreateSettingsResource(ControlPanel.ResourceSettingsPath);

            // var _resources = new List<string>
            // {
            // ControlPanel.ResourceSettingsPath
            // };

            // string[] _sources;
            // _sources = new[] { Resources.MainEntryPoint, Resources.Installer };

            // CompilerResults _results = CodeDomCompiler.Build(_references, _sources, ControlPanel.InstallerPath, _resources);
            Cleanup(installOptions);
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

        /// <summary>
        ///     Initializes the progress dialog.
        /// </summary>
        private void InitializeProgressDialog()
        {
            _progressDialog = new ProgressDialog(_installOptions, Package, CurrentVersion)
                {
                    StartPosition = FormStartPosition.CenterParent,
                    Text = Application.ProductName + @" Update"
                };
        }

        /// <summary>
        ///     Install the update.
        /// </summary>
        /// <param name="installOptions">The install options.</param>
        private void InstallUpdate(InstallOptions installOptions)
        {
            Archive.ExtractToDirectory(new Archive(installOptions.DownloadedFile), installOptions.InstallFilesFolder);

            CompileInstaller(_installOptions);
        }

        /// <summary>
        ///     Notify the user an update is available.
        /// </summary>
        private void NotificationUpdateAvailable()
        {
            if (_notifyUpdateAvailable)
            {
                StringBuilder _updateAvailableString = new StringBuilder();
                _updateAvailableString.AppendLine($"The update (v.{GetLatestVersion}) is available for download.");
                Notification.DisplayNotification(Resources.Comet, "Update Available", _updateAvailableString.ToString(), ToolTipIcon.Info);
            }

            if (!_autoUpdate)
            {
                InitializeProgressDialog();

                if (_progressDialog.ShowDialog() == DialogResult.OK)
                {
                    DownloadUpdate();
                }
            }
        }

        /// <summary>
        ///     Notify the user the update is ready to install.
        /// </summary>
        private void NotificationUpdateReadyToInstall()
        {
            if (_notifyUpdateReadyToInstall)
            {
                StringBuilder _updateReadyToInstall = new StringBuilder();
                _updateReadyToInstall.AppendLine($"The update (v.{GetLatestVersion}) is ready to install.");
                Notification.DisplayNotification(Resources.Comet, "Update Ready", _updateReadyToInstall.ToString(), ToolTipIcon.Info);
            }

            if (!_autoUpdate)
            {
                StringBuilder _askToInstall = new StringBuilder();
                _askToInstall.AppendLine($"The update (v.{GetLatestVersion}) is ready to install.");
                _askToInstall.Append(Environment.NewLine);
                _askToInstall.AppendLine($"Would you like to install it now?");

                InitializeProgressDialog();
                if (_progressDialog.ShowDialog() == DialogResult.OK)
                {
                    InstallUpdate(_installOptions);
                }
            }
            else
            {
                InstallUpdate(_installOptions);
            }
        }

        #endregion
    }
}