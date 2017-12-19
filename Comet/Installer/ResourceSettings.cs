namespace Comet
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

        private string _installFiles;
        private string _installFolder;
        private bool _logging;
        private string _productName;
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

        public string InstallFiles
        {
            get
            {
                return _installFiles;
            }
        }

        public string InstallFolder
        {
            get
            {
                return _installFolder;
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
                _installFolder = LoadInstallerSetting<string>("InstallFolder");
                _productName = LoadInstallerSetting<string>("ProductName");

                ConsoleManager.DrawLine();
                Console.WriteLine(@"Initializing");
                ConsoleManager.DrawLine();

                _workingFolder = Path.GetTempPath() + ProductName + @"\Updater\";
                Console.WriteLine(@"Working Folder: " + WorkingFolder);

                _installFiles = WorkingFolder + @"InstallFiles\";
                Console.WriteLine(@"Install Files: " + InstallFiles);

                ConsoleManager.DrawLine();

                _downloadFolder = _workingFolder + @"Download\";
                _installFilesFolder = _workingFolder + @"InstallFiles\";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private string _installFilesFolder;
        public string InstallFilesFolder
        {
            get
            {
                return _installFilesFolder;
            }
        }

        private string _downloadFolder;
        public string DownloadFolder
        {
            get
            {
                return _downloadFolder;
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