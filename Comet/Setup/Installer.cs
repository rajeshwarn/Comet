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
            DeleteDirectory(resourceSettings.DownloadFolder);
            // DeleteDirectory(resourceSettings.WorkingFolder);
        }

        /// <summary>Install the data.</summary>
        /// <param name="resourceSettings">The resource Settings.</param>
        public static void InstallData(ResourceSettings resourceSettings)
        {
            Console.WriteLine(@"Installing...");
            ConsoleManager.DrawLine();
            Console.WriteLine("... TODO!");

            // Verify process closed before data overwrite.
            // File.Copy(InstallFiles, InstallFolder, true);

            Cleanup(resourceSettings);
        }

        /// <summary>Delete a directory.</summary>
        /// <param name="directory">The directory path.</param>
        private static void DeleteDirectory(string directory)
        {
            var _files = Directory.GetFiles(directory);
            var _directories = Directory.GetDirectories(directory);

            foreach (string file in _files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string subDirectory in _directories)
            {
                DeleteDirectory(subDirectory);
            }

            File.SetAttributes(directory, FileAttributes.Normal);
            Directory.Delete(directory, false);
        }

        #endregion
    }
}