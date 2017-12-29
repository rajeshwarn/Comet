namespace Comet
{
    #region Namespace

    using System;
    using System.IO;

    #endregion

    internal class Installer
    {
        #region Events

        /// <summary>Cleanup the temporary install files.</summary>
        /// <param name="resourceSettings">The resource Settings.</param>
        public static void Cleanup(ResourceSettings resourceSettings)
        {
            Console.WriteLine(@"Cleaning up...");
            ConsoleManager.DrawLine();

            if (Directory.Exists(resourceSettings.DownloadFolder))
            {
                Directory.Delete(resourceSettings.DownloadFolder, true);
            }

            if (Directory.Exists(resourceSettings.InstallFilesFolder))
            {
                Directory.Delete(resourceSettings.InstallFilesFolder, true);
            }
        }

        /// <summary>Install the data.</summary>
        /// <param name="resourceSettings">The resource Settings.</param>
        public static void InstallData(ResourceSettings resourceSettings)
        {
            Console.WriteLine(@"Installing...");
            ConsoleManager.DrawLine();

            foreach (string _file in Directory.GetFiles(resourceSettings.InstallFilesFolder, "*.*", SearchOption.AllDirectories))
            {
                string _destination = _file.Replace(resourceSettings.InstallFilesFolder, resourceSettings.InstallDirectory);

                Console.WriteLine(@"Copying file: " + _file);
                Console.WriteLine(@"To: " + _destination);
                ConsoleManager.DrawLine();
                File.Copy(_file, _destination, true);
            }

            Cleanup(resourceSettings);
        }

        #endregion
    }
}