namespace PackageManager
{
    #region Namespace

    using System.Windows.Forms;

    using Comet.Structure;

    using PackageManager.Managers;

    #endregion

    internal class ControlPanel
    {
        #region Properties

        public static SettingsManager SettingsManager { get; set; }

        public static Logger.LogSettings DefaultSettings { get; set; }

        public static string FileHistoryLocation { get; set; }

        public static string FileName { get; set; }

        public static bool FileSaved { get; set; }

        public static string FullPath { get; set; }

        public static string InstallerPath { get; set; }

        public static int MaxRecentProjects { get; set; }

        public static string PackageFileTypes { get; set; }

        public static string UpdatePackageUrl { get; set; }

        #endregion

        #region Events

        public static string ResourceSettingsPath = Application.StartupPath + @"\\CometSettings.resources";

        #endregion
    }
}