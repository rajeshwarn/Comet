namespace Comet
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Windows.Forms;

    #endregion

    // TODO: Download data
    // TODO: extract data, and install.
    // TODO: Restart app.
    public class MainEntryPoint
    {
        #region Properties

        private static string DownloadLink { get; set; }

        private static string InstallFolder { get; set; }

        private static bool Logging { get; set; }

        private static List<string> ProcessList { get; set; }

        private static ProcessWindowStyle ProcessWindowStyle { get; set; }

        private static bool RunMainFile { get; set; }

        private static bool RunWindowNormal { get; set; }

        private static string SubFolder { get; set; }

        #endregion

        #region Events

        private static void Cleanup()
        {
        }

        private static void DownloadData()
        {
//            Downloader.DownloadData();
        }

        private static void ExtractData()
        {
        }

        private static void InstallData()
        {
        }

        /// <summary>Load the settings.</summary>
        private static void LoadSettings()
        {
            try
            {
                Console.WriteLine(@"Loading resource settings...");
                ResourceManager _settingsResourceManager = new ResourceManager("CometSettings", Assembly.GetExecutingAssembly());
                ResourceSet _settingsResourceSet = _settingsResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
                Console.WriteLine(@"-----------------------------");

                Logging = (bool)_settingsResourceSet.GetObject("Logging");

                if (Logging)
                {
                    FileStream _fileStreamLogger = new FileStream("Fusion.log", FileMode.Create);
                    StreamWriter _streamWriterLogger = new StreamWriter(_fileStreamLogger)
                        {
                            AutoFlush = true
                        };

                    Console.SetOut(_streamWriterLogger);
                    Console.SetError(_streamWriterLogger);
                }

                DownloadLink = _settingsResourceSet.GetString("DownloadLink");
                Console.WriteLine(@"Download Link: " + DownloadLink);
                Console.WriteLine(@"-----------------------------");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            // MainExecutable = _settingsResourceSet.GetString("MainExecutable");
            // Console.WriteLine(@"Main executable: " + MainExecutable);

            // SubFolder = _settingsResourceSet.GetString("InstallFolder");
            // Console.WriteLine(@"Install Sub-Folder: " + SubFolder);

            // InstallFolder = Path.GetTempPath() + SubFolder + @"\";
            // Console.WriteLine(@"Install folder: " + InstallFolder);

            // RunMainFile = (bool)_settingsResourceSet.GetObject("RunMainFile");
            // Console.WriteLine(@"Run main file: " + RunMainFile);

            // RunWindowNormal = (bool)_settingsResourceSet.GetObject("RunWindowNormal");

            // ProcessWindowStyle = RunWindowNormal ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden;
            // Console.WriteLine(@"Process Window Style: " + ProcessWindowStyle);
        }

        /// <summary>The main application entry point.</summary>
        static void Main()
        {
            Console.Title = "Comet";

            try
            {
                LoadSettings();
                NetworkCheckup();

                // if (!Directory.Exists(InstallFolder))
                // {
                // Directory.CreateDirectory(InstallFolder);
                // Console.WriteLine(@"Created install folder: " + InstallFolder);
                // Console.WriteLine(@"-----------------------------");
                // }

                //DownloadData();
                //ExtractData();
                //InstallData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static void NetworkCheckup()
        {
            if (Downloader.SourceExists(DownloadLink))
            {
                Console.WriteLine(@"The source file exists.");
            }
            else
            {
                Console.WriteLine(@"The source does not exist!");
            }
        }

        /// <summary>Start the executable application.</summary>
        /// <param name="filename">The filename.</param>
        /// <param name="processWindowStyle">The process window style.</param>
        private static void StartExecutable(string filename, ProcessWindowStyle processWindowStyle)
        {
            Process _process = new Process
                {
                    StartInfo =
                        {
                            FileName = filename,
                            WindowStyle = processWindowStyle
                        }
                };

            Console.WriteLine(@"Starting process ({0}): {1}", processWindowStyle.ToString(), filename);
            Console.WriteLine(@"-----------------------------");
            _process.Start();
        }

        #endregion
    }
}