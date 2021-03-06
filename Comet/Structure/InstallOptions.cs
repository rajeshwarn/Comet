﻿namespace Comet.Structure
{
    #region Namespace

    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    using Comet.Controls;
    using Comet.Managers;

    #endregion

    public class InstallOptions
    {
        #region Variables

        private bool _displayWelcomePage;
        private string _downloadFolder;
        private string _executablePath;
        private string _installDirectory;
        private string _installFilesFolder;
        private string _productName;
        private string _resourceSettingsPath;
        private bool _restartApplicationAfterInstall;
        private string _workingFolder;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="InstallOptions" /> class.</summary>
        /// <param name="executablePath">The executable path.</param>
        /// <param name="restartApplicationAfterInstall">The restart Application After Install.</param>
        /// <param name="displayWelcomePage">The display welcome page.</param>
        public InstallOptions(string executablePath, bool restartApplicationAfterInstall, bool displayWelcomePage)
        {
            _executablePath = executablePath;
            _displayWelcomePage = displayWelcomePage;
            _restartApplicationAfterInstall = restartApplicationAfterInstall;

            if (string.IsNullOrEmpty(_executablePath))
            {
                _installDirectory = string.Empty;
            }
            else
            {
                _installDirectory = Path.GetDirectoryName(_executablePath) + @"\";
            }

            _productName = Application.ProductName;
            _workingFolder = Path.GetTempPath() + _productName + @"\Updater\";
            _downloadFolder = _workingFolder + @"Download\";
            _installFilesFolder = _workingFolder + @"InstallFiles\";
            _resourceSettingsPath = _workingFolder + @"\\CometSettings.resources";
        }

        #endregion

        #region Properties

        public bool DisplayWelcomePage
        {
            get
            {
                return _displayWelcomePage;
            }

            set
            {
                _displayWelcomePage = value;
            }
        }

        public List<string> DownloadedFiles { get; set; } = new List<string>();

        public string DownloadFolder
        {
            get
            {
                return _downloadFolder;
            }
        }

        public string ExecutablePath
        {
            get
            {
                return _executablePath;
            }
        }

        public string InstallDirectory
        {
            get
            {
                return _installDirectory;
            }
        }

        public string InstallFilesFolder
        {
            get
            {
                return _installFilesFolder;
            }
        }

        public string ProductName
        {
            get
            {
                return _productName;
            }
        }

        public string ResourceSettingsPath
        {
            get
            {
                return _resourceSettingsPath;
            }
        }

        public bool RestartApplicationAfterInstall
        {
            get
            {
                return _restartApplicationAfterInstall;
            }

            set
            {
                _restartApplicationAfterInstall = value;
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
        }

        ///// <summary>
        /////     Verify the install files folder exists before extracting.
        ///// </summary>
        // public void VerifyExtract()
        // {
        // if (!Directory.Exists(_installFilesFolder))
        // {
        // VisualExceptionDialog.Show(new DirectoryNotFoundException("The install files path cannot be found. " + _installFilesFolder));
        // }

        // if (!File.Exists(DownloadedFile))
        // {
        // VisualExceptionDialog.Show(new DirectoryNotFoundException("The downloaded file doesn't exist. " + DownloadedFile));
        // }
        // }

        /// <summary>
        ///     Verify the install folder exists before installing.
        /// </summary>
        public void VerifyInstall()
        {
            if (!Directory.Exists(_executablePath))
            {
                VisualExceptionDialog.Show(new DirectoryNotFoundException("The install path cannot be found. " + _executablePath));
            }
        }

        #endregion
    }
}