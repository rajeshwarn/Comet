namespace PackageManager
{
    #region Namespace

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    #endregion

    internal class Settings
    {
        #region Properties

        public static string ArchiveFileTypes { get; set; }

        public static string ArchivePath { get; set; }

        public static string FileName { get; set; }

        public static bool FileSaved { get; set; }

        public static string FullPath { get; set; }

        public static int MaxRecentProjects { get; set; }

        public static string PackageFileTypes { get; set; }

        #endregion

        #region Events

        public static List<string> FileHistory = new List<string>();

        /// <summary>Updates the History Log file.</summary>
        /// <param name="write">Write new log history to file.</param>
        public static void ManageHistoryLog(bool write = false)
        {
            // Create a history log file if it don't exist
            if (!File.Exists(FileHistoryLocation))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FileHistoryLocation));
                File.Create(FileHistoryLocation).Close();
            }

            // Check for duplicates
            if (FileHistory.Count != 0)
            {
                FileHistory = FileHistory.Distinct().ToList();
            }

            // Write from memory to history file
            if (write)
            {
                // Hold a history max count to prevent memory leaks
                if (File.ReadAllLines(FileHistoryLocation).Length <= MaxRecentProjects)
                {
                    // Add file path to memory
                    FileHistory.Add(FullPath);

                    // Overwrite the file
                    File.WriteAllLines(FileHistoryLocation, FileHistory);
                }
                else
                {
                    // Truncate first line when limit reached
                    while (FileHistory.Count > MaxRecentProjects)
                    {
                        File.WriteAllLines(
                            FileHistoryLocation,
                            File.ReadAllLines(FileHistoryLocation).Skip(1).ToArray());
                    }

                    // Add file path to memory
                    FileHistory.Add(FullPath);

                    // Overwrite the file
                    File.WriteAllLines(FileHistoryLocation, FileHistory);
                }
            }
            else
            {
                // Read the settings only once from file, to prevent adding multiple items to list
                if (readSettings)
                {
                    readSettings = false;

                    var lines = File.ReadLines(FileHistoryLocation);
                    foreach (string line in lines)
                    {
                        FileHistory.Add(line);
                    }
                }
            }
        }

        private const string FileHistoryLocation = @"Logs\History.log";

        private static bool readSettings = true;

        #endregion
    }
}