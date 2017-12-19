namespace Comet
{
    #region Namespace

    using System;

    #endregion

    internal class Installer
    {
        #region Events

        /// <summary>Install the data.</summary>
        /// <param name="resourceSettings">The resource Settings.</param>
        public static void InstallData(ResourceSettings resourceSettings)
        {
            Console.WriteLine(@"Installing...");
            ConsoleManager.DrawLine();
            Console.WriteLine("... TODO!");

            // Verify process closed before data overwrite.
            // File.Copy(InstallFiles, InstallFolder, true);
        }

        #endregion
    }
}