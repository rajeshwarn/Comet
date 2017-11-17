namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.IO.Compression;

    using Comet.Structure;

    #endregion

    /// <summary>The <see cref="CompressionManager" />.</summary>
    public class CompressionManager
    {
        #region Events

        /// <summary>Add a file to the archive.</summary>
        /// <param name="archivePath">The archive path.</param>
        /// <param name="filePath">The file to add.</param>
        public static void AddFile(string archivePath, string filePath)
        {
            if (string.IsNullOrEmpty(archivePath))
            {
                throw new NoNullAllowedException(nameof(archivePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(nameof(filePath));
            }

            try
            {
                using (FileStream zipToOpen = new FileStream(archivePath, FileMode.Open))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                    {
                        archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                    }
                }
            }
            catch (IOException e)
            {
                throw new IOException(nameof(archivePath), e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Compress directory to an archive.</summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="output">The output file name.</param>
        /// <param name="compressionLevel">The compression level.</param>
        public static void CompressDirectory(string directoryPath, string output, CompressionLevel compressionLevel)
        {
            ZipFile.CreateFromDirectory(directoryPath, output, compressionLevel, false);
        }

        /// <summary>Checks if the archive contains the file.</summary>
        /// <param name="archivePath">The archive path.</param>
        /// <param name="file">The file to search.</param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        public static bool ContainsFile(string archivePath, string file)
        {
            if (string.IsNullOrEmpty(archivePath))
            {
                throw new NoNullAllowedException(nameof(archivePath));
            }

            try
            {
                using (FileStream _fileStream = new FileStream(archivePath, FileMode.Open))
                {
                    using (ZipArchive _archive = new ZipArchive(_fileStream, ZipArchiveMode.Update))
                    {
                        foreach (ZipArchiveEntry _entry in _archive.Entries)
                        {
                            if (_entry.FullName == file)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }

        /// <summary>Creates an archive.</summary>
        /// <param name="outputPath">The path to save the archive to.</param>
        public static void CreateArchive(string outputPath)
        {
            if (string.IsNullOrEmpty(outputPath))
            {
                throw new NoNullAllowedException(nameof(outputPath));
            }

            try
            {
                FileStream _fileStream = new FileStream(outputPath, FileMode.CreateNew);

                using (ZipArchive _archive = new ZipArchive(_fileStream, ZipArchiveMode.Create))
                {
                    _archive.CreateEntry("empty", CompressionLevel.Optimal);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Extract the archive to a directory.</summary>
        /// <param name="archivePath">The archive path.</param>
        /// <param name="output">The directory output.</param>
        public static void ExtractToDirectory(string archivePath, string output)
        {
            ZipFile.ExtractToDirectory(archivePath, output);
        }

        /// <summary>Extract file from archive.</summary>
        /// <param name="archivePath">The archive path.</param>
        /// <param name="file">The file To Extract.</param>
        /// <param name="outputPath">The extracted destination file path.</param>
        /// <param name="overwrite">The overwrite toggle.</param>
        public static void ExtractToFile(string archivePath, string file, string outputPath, bool overwrite = true)
        {
            if (string.IsNullOrEmpty(archivePath))
            {
                throw new NoNullAllowedException(nameof(archivePath));
            }

            if (!ContainsFile(archivePath, file))
            {
                throw new FileNotFoundException(nameof(file));
            }

            try
            {
                FileStream _fileStream = new FileStream(archivePath, FileMode.Open);

                using (ZipArchive _archive = new ZipArchive(_fileStream, ZipArchiveMode.Update))
                {
                    foreach (ZipArchiveEntry _entry in _archive.Entries)
                    {
                        if (_entry.FullName == file)
                        {
                            _entry.ExtractToFile(outputPath, overwrite);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Open an archive to list.</summary>
        /// <param name="archivePath">The archive path.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static List<ArchiveData> Open(string archivePath)
        {
            if (string.IsNullOrEmpty(archivePath))
            {
                throw new NoNullAllowedException(nameof(archivePath));
            }

            if (!File.Exists(archivePath))
            {
                throw new FileNotFoundException(nameof(archivePath));
            }

            var _entries = new List<ArchiveData>();

            using (ZipArchive _archive = ZipFile.OpenRead(archivePath))
            {
                foreach (ZipArchiveEntry entry in _archive.Entries)
                {
                    ArchiveData data = new ArchiveData
                        {
                            CompressedSize = new Bytes(entry.CompressedLength),
                            Name = entry.FullName,
                            FullName = entry.FullName,
                            Size = new Bytes(entry.Length),
                            LastWriteTime = entry.LastWriteTime,
                            Type = Path.GetExtension(entry.FullName)
                        };

                    _entries.Add(data);
                }
            }

            return _entries;
        }

        /// <summary>Removes a file from the archive.</summary>
        /// <param name="archivePath">The archive path.</param>
        /// <param name="file">The file entry to remove.</param>
        public static void RemoveFile(string archivePath, string file)
        {
            if (string.IsNullOrEmpty(archivePath))
            {
                throw new NoNullAllowedException(nameof(archivePath));
            }

            try
            {
                FileStream _fileStream = new FileStream(archivePath, FileMode.Open);

                using (ZipArchive _archive = new ZipArchive(_fileStream, ZipArchiveMode.Update))
                {
                    foreach (ZipArchiveEntry _entry in _archive.Entries)
                    {
                        if (_entry.FullName == file)
                        {
                            _entry.Delete();
                            ConsoleManager.WriteOutput("The entry was deleted.");
                            ConsoleManager.WriteOutput("Entry: " + _entry.FullName);
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }
}