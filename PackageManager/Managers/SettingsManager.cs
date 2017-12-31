namespace PackageManager.Managers
{
    #region Namespace

    using System;
    using System.Data;
    using System.IO;
    using System.Xml.Linq;

    using Comet.Controls;
    using Comet.Managers;
    using Comet.Structure;

    #endregion

    public class SettingsManager
    {
        #region Variables

        private string _filePath;
        private UpdaterSettings _updaterSettings;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="SettingsManager" /> class. </summary>
        /// <param name="filePath">The settings file Path.</param>
        public SettingsManager(string filePath) : this()
        {
            _filePath = filePath;
        }

        /// <summary>Initializes a new instance of the <see cref="SettingsManager" /> class. </summary>
        /// <param name="filePath">The settings file Path.</param>
        /// <param name="settings">The settings.</param>
        public SettingsManager(string filePath, UpdaterSettings settings) : this()
        {
            _filePath = filePath;
            _updaterSettings = settings;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SettingsManager" /> class.
        /// </summary>
        public SettingsManager()
        {
            _filePath = string.Empty;
            _updaterSettings = new UpdaterSettings();
        }

        #endregion

        #region Properties

        public string FilePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                _filePath = value;
            }
        }

        public UpdaterSettings Settings
        {
            get
            {
                return _updaterSettings;
            }

            set
            {
                _updaterSettings = value;
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Loads the settings.
        /// </summary>
        public void Load()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                throw new NoNullAllowedException(StringManager.IsNullOrEmpty(FilePath));
            }

            try
            {
                if (File.Exists(FilePath))
                {
                    XDocument _historyLogFile = XDocument.Load(FilePath);
                    Deserialize(_historyLogFile);
                }
                else
                {
                    Save();
                }
            }
            catch (Exception e)
            {
                VisualExceptionDialog.Show(e);
            }
        }

        /// <summary>
        ///     Saves the settings to file.
        /// </summary>
        public void Save()
        {
            try
            {
                XDocument _settingsDocument = new XDocument();
                XElement _headerElement = new XElement("Settings");
                _settingsDocument.Add(_headerElement);

                _headerElement.Add(new XElement("AutoUpdate", _updaterSettings.AutoUpdate));
                _headerElement.Add(new XElement("NotifyUpdateAvailable", _updaterSettings.NotifyUpdateAvailable));
                _headerElement.Add(new XElement("NotifyBeforeInstallingUpdates", _updaterSettings.NotifyUpdateReadyToInstall));
                _headerElement.Add(new XElement("DisplayWelcomePage", _updaterSettings.DisplayWelcomePage));

                _settingsDocument.Save(FilePath, SaveOptions.None);
            }
            catch (Exception e)
            {
                VisualExceptionDialog.Show(e);
            }
        }

        /// <summary>
        ///     Deserialize the settings file.
        /// </summary>
        /// <param name="settingsFile">The settings document.</param>
        private void Deserialize(XContainer settingsFile)
        {
            try
            {
                _updaterSettings.AutoUpdate = Convert.ToBoolean(string.Concat(settingsFile.Descendants("AutoUpdate").Nodes()));
                _updaterSettings.DisplayWelcomePage = Convert.ToBoolean(string.Concat(settingsFile.Descendants("DisplayWelcomePage").Nodes()));
                _updaterSettings.NotifyUpdateAvailable = Convert.ToBoolean(string.Concat(settingsFile.Descendants("NotifyUpdateAvailable").Nodes()));
                _updaterSettings.NotifyUpdateReadyToInstall = Convert.ToBoolean(string.Concat(settingsFile.Descendants("NotifyBeforeInstallingUpdates").Nodes()));
            }
            catch (Exception e)
            {
                VisualExceptionDialog.Show(e);
            }
        }

        #endregion
    }
}