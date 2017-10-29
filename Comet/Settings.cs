namespace Comet
{
    #region Namespace

    using System;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    using Comet.Managers;

    #endregion

    internal class Settings
    {
        #region Variables

        public bool UpdateAvailable;

        #endregion

        #region Properties

        public static ConsoleColor BackgroundColor { get; set; }

        public static string BatchFullPath { get; set; }

        public static string ConfigFullPath { get; set; }

        public static ConnectionState Connection { get; set; }

        public static Version CurrentVersion { get; set; }

        public static string DownloadFolder { get; set; }

        public static char ErrorCharacter { get; set; }

        public static ConsoleColor ErrorColor { get; set; }

        public static ConsoleColor ErrorTextColor { get; set; }

        public static string ExecutingAssemblyLocation { get; set; }

        public static string ExecutingDirectory { get; set; }

        public static char InputCharacter { get; set; }

        public static ConsoleColor InputColor { get; set; }

        public static ConsoleColor OutputColor { get; set; }

        public static string PackageFullPath { get; set; }

        public static string ProductName { get; set; }

        public static string TemporaryFolder { get; set; }

        public static ConsoleColor TextColor { get; set; }

        #endregion

        #region Events

        /// <summary>Initializes the settings.</summary>
        public static void Initialize()
        {
            LoadSettings();
            InitializeConsoleWindow();
        }

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

        /// <summary>Initializes the console window.</summary>
        private static void InitializeConsoleWindow()
        {
            Console.Title = Application.ProductName;
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = TextColor;
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

            string _productName = ProductName;
            StringManager.DrawCenterText(_productName);

            string _version = "v." + CurrentVersion;
            StringManager.DrawCenterText(_version);

            string _website = versionInfo.LegalTrademarks;
            StringManager.DrawCenterText(_website);

            string _updateState = "Update state: " + (true ? "Updated." : "Update available.");
            StringManager.DrawCenterText(_updateState);

            Console.Write(Environment.NewLine);

            var _tip = "Type: 'Help' - Provides Help information for Comet commands.";
            StringManager.DrawCenterText(_tip);

            Console.Write(Environment.NewLine);
        }

        /// <summary>Load the settings.</summary>
        private static void LoadSettings()
        {
            BackgroundColor = ConsoleColor.Black;
            ErrorCharacter = '>';
            ErrorColor = ConsoleColor.Red;
            OutputColor = ConsoleColor.Blue;
            ErrorTextColor = ConsoleColor.White;
            TextColor = ConsoleColor.White;
            InputCharacter = '#';
            InputColor = ConsoleColor.Green;

            Connection = ConnectionState.Connecting;

            // CurrentVersion = ApplicationManager.GetFileVersion(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            CurrentVersion = AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).Version;

            ProductName = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductName;

            ExecutingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            ExecutingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            BatchFullPath = ExecutingDirectory + @"\Update.bat";
            ConfigFullPath = ExecutingDirectory + @"\" + ProductName + @"\Temp\config.xml";
            PackageFullPath = ExecutingDirectory + @"\" + ProductName + @"\Package.zip";
            DownloadFolder = ExecutingDirectory + @"\" + ProductName;
            TemporaryFolder = ExecutingDirectory + @"\" + ProductName + @"\Temp\";
        }

        #endregion
    }
}