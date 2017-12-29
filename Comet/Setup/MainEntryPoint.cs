namespace Comet
{
    #region Namespace

    using System;
    using System.Diagnostics;
    using System.IO;

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
                TerminateRunningProcess(_resourceSettings);
                Installer.InstallData(_resourceSettings);

                if (_resourceSettings.RestartApplicationAfterInstall)
                {
                    StartExecutable(_resourceSettings.ExecutablePath);
                }
                else
                {
                    // TODO: Exit
                }
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
        private static void StartExecutable(string filename, ProcessWindowStyle processWindowStyle = ProcessWindowStyle.Normal)
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
            ConsoleManager.DrawLine();
            _process.Start();
        }

        /// <summary>
        ///     Terminate the running process.
        /// </summary>
        /// <param name="resourceSettings">The resource settings.</param>
        private static void TerminateRunningProcess(ResourceSettings resourceSettings)
        {
            string _processName = Path.GetFileNameWithoutExtension(resourceSettings.ExecutablePath);

            foreach (Process _process in Process.GetProcesses())
            {
                if (_process.ProcessName == _processName)
                {
                    Console.WriteLine(@"Terminating process: " + _processName);
                    ConsoleManager.DrawLine();
                    _process.Kill();
                }
            }
        }

        #endregion
    }
}