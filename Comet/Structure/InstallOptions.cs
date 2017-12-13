namespace Comet.Structure
{
    #region Namespace

    using System.IO;
    using System.Windows.Forms;

    using Comet.Controls;
    using Comet.Managers;

    #endregion

    public class InstallOptions
    {
        #region Variables

        private string _downloadFolder;
        private string _installFilesFolder;
        private string _installPath;
        private string _workingFolder;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="InstallOptions" /> class.
        /// </summary>
        /// <param name="installFolder">The install folder.</param>
        public InstallOptions(string installFolder)
        {
            UpdateOptions(installFolder);
        }

        #endregion

        #region Properties

        public string DownloadedFile { get; set; }

        public string DownloadFolder
        {
            get
            {
                return _downloadFolder;
            }
        }

        public string InstallFilesFolder
        {
            get
            {
                return _installFilesFolder;
            }
        }

        public string InstallPath
        {
            get
            {
                return _installPath;
            }

            set
            {
                if (_installPath == value)
                {
                    return;
                }

                _installPath = value;
                UpdateOptions(_installPath);
            }
        }

        public string WorkingFolder
        {
            get
            {
                return _workingFolder;
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Verify the directories.
        /// </summary>
        public void Verify()
        {
            FileManager.CreateDirectory(_workingFolder);
            FileManager.CreateDirectory(_downloadFolder);
            FileManager.CreateDirectory(_installFilesFolder);
        }

        /// <summary>
        ///     Verify the install files folder exists before extracting.
        /// </summary>
        public void VerifyExtract()
        {
            if (!Directory.Exists(_installFilesFolder))
            {
                VisualExceptionDialog.Show(new DirectoryNotFoundException("The install files path cannot be found. " + _installFilesFolder));
            }

            if (!File.Exists(DownloadedFile))
            {
                VisualExceptionDialog.Show(new DirectoryNotFoundException("The downloaded file doesn't exist. " + DownloadedFile));
            }
        }

        /// <summary>
        ///     Verify the install folder exists before installing.
        /// </summary>
        public void VerifyInstall()
        {
            if (!Directory.Exists(_installPath))
            {
                VisualExceptionDialog.Show(new DirectoryNotFoundException("The install path cannot be found. " + _installPath));
            }
        }

        /// <summary>
        ///     Updates the options.
        /// </summary>
        /// <param name="installPath">The install path.</param>
        private void UpdateOptions(string installPath)
        {
            _installPath = installPath;

            _workingFolder = Path.GetTempPath() + Application.ProductName + @"\Updater\"; // TODO : Allow custom product name
            _downloadFolder = _workingFolder + @"Download\";
            _installFilesFolder = _workingFolder + @"InstallFiles\";

            Verify();
        }

        #endregion
    }
}