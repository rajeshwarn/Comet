namespace Comet
{
    #region Namespace

    using System;
    using System.Data;
    using System.Diagnostics;
    using System.IO;

    using Comet.Commands;
    using Comet.Managers;

    #endregion

    /// <summary>The <see cref="UpdateManager" />.</summary>
    public class UpdateManager
    {
        #region Variables

        private string _downloadPath;
        private string _executablePath;
        private Uri _packagePath;
        private bool _restart;
        private UpdateState _state;
        private bool _working;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="UpdateManager" /> class.</summary>
        /// <param name="packagePath">The package url.</param>
        /// <param name="downloadPath">The download folder path.</param>
        /// <param name="executablePath">The destination executable Path.</param>
        /// <param name="restart">Restart the application after update.</param>
        public UpdateManager(Uri packagePath, string downloadPath, string executablePath, bool restart) : this()
        {
            Initialize(packagePath, downloadPath, executablePath, restart);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateManager" /> class.</summary>
        /// <param name="packagePath">The package url.</param>
        /// <param name="restart">Restart the application after update.</param>
        public UpdateManager(Uri packagePath, bool restart) : this()
        {
            Initialize(packagePath, FileManager.CreateTempPath("Update"), GetMainModuleFileName(), restart);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateManager" /> class.</summary>
        public UpdateManager()
        {
            _packagePath = null;
            _downloadPath = string.Empty;
            Restart = false;
            ExecutablePath = string.Empty;
            _working = false;
            _state = UpdateState.NotChecked;
        }

        /// <summary>The update state.</summary>
        [Serializable]
        public enum UpdateState
        {
            /// <summary>The not checked.</summary>
            NotChecked,

            /// <summary>The checked.</summary>
            Checked,

            /// <summary>The prepared.</summary>
            Prepared
        }

        #endregion

        #region Properties

        /// <summary>The download path.</summary>
        public string DownloadPath
        {
            get
            {
                return _downloadPath;
            }

            set
            {
                _downloadPath = value;
            }
        }

        /// <summary>The executable path.</summary>
        public string ExecutablePath
        {
            get
            {
                return _executablePath;
            }

            set
            {
                _executablePath = value;
            }
        }

        /// <summary>The source url.</summary>
        public Uri PackagePath
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

        /// <summary>The restart toggle.</summary>
        public bool Restart
        {
            get
            {
                return _restart;
            }

            set
            {
                _restart = value;
            }
        }

        /// <summary>The update state.</summary>
        public UpdateState State
        {
            get
            {
                return _state;
            }
        }

        #endregion

        #region Events

        /// <summary>Checks for updates.</summary>
        public void CheckForUpdate()
        {
            if (_working)
            {
                throw new InvalidOperationException("Another update process is already in progress.");
            }
            else if (_packagePath == null)
            {
                throw new InvalidOperationException("The source must be set before checking for updates.");
            }
            else if (!NetworkManager.IsURLFormatted(_packagePath.ToString()))
            {
                throw new UriFormatException("The source uri is not well formatted.");
            }
            else if (!NetworkManager.SourceExists(_packagePath.ToString()))
            {
                throw new FileNotFoundException("The remote source file is not found.");
            }
            else if (string.IsNullOrWhiteSpace(_downloadPath))
            {
                throw new NoNullAllowedException("The path is null or whitespace.");
            }

            if (_state != UpdateState.NotChecked)
            {
                throw new InvalidOperationException("Already checked for updates.");
            }

            if (UpdateRequired())
            {
                _state = UpdateState.Prepared;
            }
            else
            {
                _state = UpdateState.Checked;
            }

            DefaultCommands.Check(_executablePath, _packagePath.ToString());
        }

        /// <summary>Cleanup temporary files.</summary>
        public void Cleanup()
        {
            FileManager.DeleteDirectory(_downloadPath);
        }

        public void Download()
        {
        }

        /// <summary>Get the main module file name.</summary>
        /// <returns><see cref="string" />.</returns>
        public string GetMainModuleFileName()
        {
            return Process.GetCurrentProcess().MainModule.FileName;
        }

        public void Install()
        {
            // create setup script and run it after closing application.

            // setup script should extract all the file/s?
            // Then it should replace them and start the main executable.
        }

        /// <summary>Initializes the <see cref="UpdateManager" />.</summary>
        /// <param name="source">The source url.</param>
        /// <param name="downloadPath">The folder path.</param>
        /// <param name="installPath">The install Path.</param>
        /// <param name="restart">The restart.</param>
        private void Initialize(Uri source, string downloadPath, string installPath, bool restart)
        {
            _packagePath = source;
            _downloadPath = downloadPath;
            _executablePath = installPath;
            _restart = restart;

            CheckForUpdate();

            // TODO: Prepare for update.
            // TODO: Download.
            // TODO: Extract and install.
            // TODO: Restart
            // TODO: Success? Cleanup then.
        }

        private void PrepareUpdate()
        {
            FileManager.CreateDirectory(_downloadPath);
        }

        /// <summary>Check if update required.</summary>
        /// <returns><see cref="bool" />.</returns>
        private bool UpdateRequired()
        {
            return ApplicationManager.CheckForUpdate(_executablePath, _packagePath.ToString());
        }

        #endregion
    }
}