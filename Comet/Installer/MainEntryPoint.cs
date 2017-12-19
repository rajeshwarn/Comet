namespace Comet
{
    #region Namespace

    using System;
    using System.Diagnostics;

    #endregion

    /// <summary>
    ///     The comet installer.
    /// </summary>
    public class CometInstaller
    {
        #region Events

        private static ResourceSettings _resourceSettings;

        /// <summary>The main application entry point.</summary>
        [STAThread]
        private static void Main()
        {
            Console.Title = @"Comet";

            ConsoleManager.DrawLine();
            Console.WriteLine(@"Comet Installer");

            try
            {
                _resourceSettings = new ResourceSettings();
                Installer.InstallData(_resourceSettings);
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