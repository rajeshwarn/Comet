namespace Comet
{
    #region Namespace

    using System;
    using System.Data;
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

        public static string BatchFullPath { get; set; }

        public static string ConfigFullPath { get; set; }

        public static ConnectionState Connection { get; set; }

        public static Version CurrentVersion { get; set; }

        public static string DownloadFolder { get; set; }

        public static char ErrorCharacter { get; set; }

        public static string ExecutingAssemblyLocation { get; set; }

        public static string ExecutingDirectory { get; set; }

        public static char InputCharacter { get; set; }

        public static string PackageFullPath { get; set; }

        public static string ProductName { get; set; }

        public static string TemporaryFolder { get; set; }

        #endregion

        #region Events

        public const string UpdaterFolderName = "CometUpdater";

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