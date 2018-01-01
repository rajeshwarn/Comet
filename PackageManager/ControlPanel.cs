namespace PackageManager
{
    #region Namespace

    using Comet.Structure;

    using PackageManager.Managers;

    #endregion

    internal class ControlPanel
    {
        #region Properties

        public static Logger.LogSettings DefaultSettings { get; set; }

        public static string FileHistoryLocation { get; set; }

        public static string FileName { get; set; }

        public static bool FileSaved { get; set; }

        public static string FullPath { get; set; }

        public static string InstallerPath { get; set; }

        public static string PackageFileTypes { get; set; }

        public static string ResourceSettingsPath { get; set; }

        public static SettingsManager SettingsManager { get; set; }

        #endregion
    }
}