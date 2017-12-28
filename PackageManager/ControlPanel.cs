namespace PackageManager
{
    #region Namespace

    using System.Windows.Forms;

    using Comet.Structure;

    #endregion

    internal class ControlPanel
    {
        #region Properties

        public static string ArchiveFileTypes { get; set; }

        public static string ArchivePath { get; set; }

        public static string FileName { get; set; }

        public static bool FileSaved { get; set; }

        public static string FullPath { get; set; }

        public static string InstallerPath { get; set; }

        public static int MaxRecentProjects { get; set; }

        public static string PackageFileTypes { get; set; }

        public static string UpdatePackageUrl { get; set; }

        #endregion

        #region Events

        // TODO: Create logger settings
        public static string LogDirectory = "Logs";

        public static string LogExtension = ".xml";
        public static string LogFile = "Log";

        public static string ResourceSettingsPath = Application.StartupPath + @"\\CometSettings.resources";

        /// <summary>
        ///     Write entry to log file.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLog(string message)
        {
            Logger.Log(new Logger(LogDirectory, LogExtension, LogFile, WriteMode), message);
        }

        public static Logger.WriteMode WriteMode = Logger.WriteMode.XML;

        internal static string FileHistoryLocation;

        #endregion
    }
}