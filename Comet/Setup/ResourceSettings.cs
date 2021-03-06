﻿namespace Comet
{
    #region Namespace

    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Text;
    using System.Windows.Forms;

    #endregion

    public class ResourceSettings
    {
        #region Variables

        private string _downloadFolder;
        private string _executablePath;
        private string _installDirectory;
        private string _installFilesFolder;
        private bool _logging;
        private string _productName;
        private bool _restartApplicationAfterInstall;
        private string _workingFolder;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceSettings" /> class.
        /// </summary>
        public ResourceSettings()
        {
            LoadSettings();
        }

        #endregion

        #region Properties

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

        public bool Logging
        {
            get
            {
                return _logging;
            }
        }

        public string ProductName
        {
            get
            {
                return _productName;
            }
        }

        public bool RestartApplicationAfterInstall
        {
            get
            {
                return _restartApplicationAfterInstall;
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
        ///     Loads the resource settings.
        /// </summary>
        public void LoadSettings()
        {
            try
            {
                ConsoleManager.DrawLine();
                Console.WriteLine(@"Loading settings...");
                ConsoleManager.DrawLine();

                _logging = LoadInstallerSetting<bool>("Logging");
                _executablePath = LoadInstallerSetting<string>("ExecutablePath");
                _installDirectory = LoadInstallerSetting<string>("InstallDirectory");
                _productName = LoadInstallerSetting<string>("ProductName");
                _restartApplicationAfterInstall = LoadInstallerSetting<bool>("RestartApplicationAfterInstall");

                ConsoleManager.DrawLine();
                Console.WriteLine(@"Initializing");
                ConsoleManager.DrawLine();

                _workingFolder = Path.GetTempPath() + _productName + @"\Updater\";
                Console.WriteLine(@"Working Folder: " + _workingFolder);
                ConsoleManager.DrawLine();

                _downloadFolder = _workingFolder + @"Download\";
                _installFilesFolder = _workingFolder + @"InstallFiles\";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>Loads the installer setting from the resource.</summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="ignoreCase">Indicates whether the case of the specified name should be ignored.</param>
        /// <returns>
        ///     <see cref="object" />
        /// </returns>
        internal static T LoadInstallerSetting<T>(string name, bool ignoreCase = false)
        {
            ResourceManager _settingsResourceManager = new ResourceManager("CometSettings", Assembly.GetExecutingAssembly());
            ResourceSet _settingsResourceSet = _settingsResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            object _settingValue = _settingsResourceSet.GetObject(name, ignoreCase);
            T _settingType = (T)Convert.ChangeType(_settingValue, typeof(T));

            StringBuilder _loadedSetting = new StringBuilder();
            _loadedSetting.AppendLine("Name: " + name);
            _loadedSetting.AppendLine("Value: " + _settingValue);
            _loadedSetting.AppendLine("Type: " + typeof(T));
            Console.WriteLine(_loadedSetting.ToString());

            return (T)_settingValue;
        }

        #endregion
    }
}