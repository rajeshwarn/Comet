namespace Comet
{
    #region Namespace

    using System;
    using System.IO;
    using System.Windows.Forms;

    using Comet.Managers;

    #endregion

    internal class ControlPanel
    {
        #region Variables

        public bool UpdateAvailable;

        #endregion

        #region Properties

        public static string DownloadFolder { get; set; }

        public static string InstallFilesFolder { get; set; }

        public static string TemporaryFolder { get; set; }

        public static string WorkingFolder { get; set; } // Temp + AppName + \Updater\

        #endregion

        #region Events

        public static string SettingsFilePath = WorkingFolder + "Comet.cfg";

        public const string UpdaterFolderName = "CometUpdater";

        [Obsolete]
        /// <summary>Checks if new version is available for download.</summary>
        /// <returns>The <see cref="bool" />.</returns>
        public bool CheckForUpdate()
        {
            // Higher version available
            UpdateAvailable = ApplicationManager.CompareVersion(Application.ExecutablePath, "online/local source in here");
            return UpdateAvailable;
        }

        /// <summary>Handles directory creation.</summary>
        internal void CreateDirectory()
        {
            // Comet Temp Folder
            if (!Directory.Exists(DownloadFolder))
            {
                Directory.CreateDirectory(DownloadFolder);
            }

            // Temp/Temp downloaded files
            if (!Directory.Exists(TemporaryFolder))
            {
                Directory.CreateDirectory(TemporaryFolder);
            }
        }

        #endregion
    }
}