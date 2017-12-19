namespace Comet
{
    #region Namespace

    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Text;
    using System.Windows.Forms;

    #endregion

    // TODO: Install app.
    // TODO: Restart app.
    public class CometInstaller
    {
        #region Properties

        private static bool Installed { get; set; }

        private static string InstallFiles { get; set; }

        private static string InstallFolder { get; set; }

        private static bool Logging { get; set; }

        private static ProcessWindowStyle ProcessWindowStyle { get; set; }

        private static string ProductName { get; set; }

        private static string WorkingFolder { get; set; }

        #endregion

        #region Events



        /// <summary>Install the data.</summary>
        private static void InstallData()
        {
            Console.WriteLine(@"Installing...");
            ConsoleManager.DrawLine();
            
            Installer.InstallData();

            // Verify process closed before data overwrite.
            // File.Copy(InstallFiles, InstallFolder, true);
        }

        /// <summary>Loads the installer setting from the resource.</summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="ignoreCase">Indicates whether the case of the specified name should be ignored.</param>
        /// <returns>
        ///     <see cref="object" />
        /// </returns>
        private static T LoadInstallerSetting<T>(string name, bool ignoreCase = false)
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

        /// <summary>Load the installer settings.</summary>
        private static void LoadSettings()
        {
            Installed = false;

            try
            {
                ConsoleManager.DrawLine();
                Console.WriteLine(@"Loading settings...");
                ConsoleManager.DrawLine();

                Logging = LoadInstallerSetting<bool>("Logging");
                InstallFolder = LoadInstallerSetting<string>("InstallFolder");
                ProductName = LoadInstallerSetting<string>("ProductName");

                ConsoleManager.DrawLine();
                Console.WriteLine(@"Initializing");
                ConsoleManager.DrawLine();

                WorkingFolder = Path.GetTempPath() + ProductName + @"\Updater\";
                Console.WriteLine(@"Working Folder: " + WorkingFolder);

                InstallFiles = WorkingFolder + @"InstallFiles\";
                Console.WriteLine(@"Install Files: " + InstallFiles);

                ConsoleManager.DrawLine();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>The main application entry point.</summary>
        private static void Main()
        {
            Console.Title = @"Comet";

            ConsoleManager.DrawLine();
            Console.WriteLine(@"Comet Installer");

            try
            {
                LoadSettings();
                InstallData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
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