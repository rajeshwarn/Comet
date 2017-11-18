namespace Comet.Structure
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.CompilerServices;

    using Comet.Managers;

    #endregion

    [Description("Easily manage a ZIP file archive.")]
    public class Archive : IDisposable
    {
        #region Variables

        private CompressionLevel _compression;
        private string _directory;
        private string _extension;
        private bool _isDisposed;
        private bool _isNewArchive;
        private string _name;
        private ZipArchiveEntry[] _zipEntries;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="Archive" /> class.</summary>
        /// <param name="fileStream">The file stream.</param>
        public Archive(FileStream fileStream) : this()
        {
            ExceptionsManager.IsNull(fileStream);
            ExceptionsManager.CanSeek(fileStream);

            OpenArchiveStream(fileStream);

            try
            {
                Read();
            }
            catch (Exception e)
            {
                DisposeInternal(true);
                throw new Exception(e.Message);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="Archive" /> class.</summary>
        /// <param name="archivePath">Read an archive.</param>
        public Archive(string archivePath) : this()
        {
            ExceptionsManager.IsNull(archivePath);
            ExceptionsManager.FileExists(archivePath);

            OpenArchiveStream(new FileStream(archivePath, FileMode.Open));

            try
            {
                Read();
            }
            catch (Exception e)
            {
                DisposeInternal(true);
                throw new Exception(e.Message);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="Archive" /> class.</summary>
        public Archive()
        {
            _compression = CompressionLevel.Optimal;
            _isNewArchive = true;
            _zipEntries = null;
            _isDisposed = true;
            _directory = string.Empty;
            _extension = ".zip";
            _name = string.Empty;
        }

        /// <summary>Finalizes an instance of the <see cref="Archive" /> class.</summary>
        ~Archive()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets the compression level.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public CompressionLevel Compression
        {
            get
            {
                return _compression;
            }

            set
            {
                _compression = value;
            }
        }

        /// <summary>Gets a value indicating the amount of entries.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int Count
        {
            get
            {
                return _zipEntries.Length;
            }
        }

        /// <summary>Gets or sets the directory for the archive.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string Directory
        {
            get
            {
                return _directory;
            }

            set
            {
                _directory = value;
            }
        }

        /// <summary>Gets the archived entries.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ZipArchiveEntry[] Entries
        {
            get
            {
                return _zipEntries;
            }
        }

        /// <summary>Gets or sets the extension for the archive.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string Extension
        {
            get
            {
                return _extension;
            }

            set
            {
                _extension = value;
            }
        }

        /// <summary>Get the full archive path.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string GetFullPath
        {
            get
            {
                return _directory + _name + _extension;
            }
        }

        /// <summary>Gets a value indicating whether the archive is empty.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsEmpty
        {
            get
            {
                if (_zipEntries[0] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>Get a value indicating that this archive is a new one.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsNewArchive
        {
            get
            {
                return _isNewArchive;
            }
        }

        /// <summary>Indexer property for <see cref="ZipArchiveEntry" />.</summary>
        /// <param name="index">The entry index.</param>
        /// <returns>
        ///     <see cref="ZipArchiveEntry" />
        /// </returns>
        [IndexerName("EntryByIndex")]
        public ZipArchiveEntry this[int index]
        {
            get
            {
                if (IsEmpty)
                {
                    throw new ArgumentNullException($"The archive is empty.");
                }
                else
                {
                    return _zipEntries[index];
                }
            }
        }

        /// <summary>Gets or sets the filename without the extension for the archive.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        #endregion

        #region Events

        /// <summary>Compress directory to an archive.</summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="archiveOutput">The output file name.</param>
        /// <param name="compressionLevel">The compression level.</param>
        public static void CompressDirectory(string directoryPath, string archiveOutput, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            ZipFile.CreateFromDirectory(directoryPath, archiveOutput, compressionLevel, false);
        }

        /// <summary>Creates an archive from the file.</summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="archiveOutput">The archive output.</param>
        /// <param name="compressionLevel">The compression level.</param>
        public static void CompressFile(string filePath, string archiveOutput, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            ExceptionsManager.IsNullOrEmpty(filePath);
            ExceptionsManager.FileExists(filePath);
            ExceptionsManager.IsNullOrEmpty(archiveOutput);
            ExceptionsManager.IsNull(compressionLevel);

            try
            {
                using (FileStream _zipFile = new FileStream(archiveOutput, FileMode.CreateNew))
                {
                    using (ZipArchive _archive = new ZipArchive(_zipFile, ZipArchiveMode.Create))
                    {
                        _archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath), compressionLevel);
                    }
                }
            }
            catch (IOException e)
            {
                throw new IOException(nameof(archiveOutput), e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Creates a empty archive file.</summary>
        /// <param name="archiveOutput">The output file.</param>
        public static void CreateEmptyArchive(string archiveOutput)
        {
            ExceptionsManager.IsNullOrEmpty(archiveOutput);
            const string EmptyEntry = "Empty";

            try
            {
                using (FileStream _fileStream = new FileStream(archiveOutput, FileMode.CreateNew))
                {
                    using (ZipArchive _archive = new ZipArchive(_fileStream, ZipArchiveMode.Create))
                    {
                        _archive.CreateEntry(EmptyEntry, CompressionLevel.Optimal);
                    }
                }

                DeleteEntry(new Archive(archiveOutput), EmptyEntry);
            }
            catch (IOException e)
            {
                throw new IOException(nameof(GetFullPath), e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Creates an entry in the archive.</summary>
        /// <param name="archive">The archive.</param>
        /// <param name="entryName">The entry name.</param>
        /// <param name="compressionLevel">The compression level.</param>
        /// <param name="overwrite">The overwrite toggle.</param>
        public static void CreateEntry(Archive archive, string entryName, CompressionLevel compressionLevel = CompressionLevel.Optimal, bool overwrite = true)
        {
            ExceptionsManager.IsNull(archive);
            ExceptionsManager.IsNullOrEmpty(entryName);
            ExceptionsManager.IsNull(compressionLevel);

            Overwrite(archive, entryName, overwrite);

            try
            {
                using (FileStream _zipFile = new FileStream(archive.GetFullPath, FileMode.Open))
                {
                    using (ZipArchive _archive = new ZipArchive(_zipFile, ZipArchiveMode.Update))
                    {
                        _archive.CreateEntry(entryName, compressionLevel);
                    }
                }
            }
            catch (IOException e)
            {
                throw new IOException(nameof(archive.GetFullPath), e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Creates an entry from a file stream in the archive.</summary>
        /// <param name="archive">The archive.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="compressionLevel">The compression level.</param>
        /// <param name="overwrite">The overwrite toggle.</param>
        public static void CreateEntryFromFile(Archive archive, FileStream fileStream, CompressionLevel compressionLevel = CompressionLevel.Optimal, bool overwrite = true)
        {
            CreateEntryFromFile(archive, fileStream.Name, Path.GetFileName(fileStream.Name), compressionLevel, overwrite);
        }

        /// <summary>Creates an entry from a file in the archive.</summary>
        /// <param name="archive">The archive.</param>
        /// <param name="sourceFileName">The source File Name.</param>
        /// <param name="entryName">The entry name.</param>
        /// <param name="compressionLevel">The compression level.</param>
        /// <param name="overwrite">The overwrite toggle.</param>
        public static void CreateEntryFromFile(Archive archive, string sourceFileName, string entryName, CompressionLevel compressionLevel = CompressionLevel.Optimal, bool overwrite = true)
        {
            ExceptionsManager.IsNull(archive);
            ExceptionsManager.IsNullOrEmpty(entryName);
            ExceptionsManager.IsNullOrEmpty(sourceFileName);
            ExceptionsManager.FileExists(sourceFileName);
            ExceptionsManager.IsNull(compressionLevel);

            Overwrite(archive, entryName, overwrite);

            try
            {
                using (FileStream _zipFile = new FileStream(archive.GetFullPath, FileMode.Open))
                {
                    using (ZipArchive _archive = new ZipArchive(_zipFile, ZipArchiveMode.Update))
                    {
                        _archive.CreateEntryFromFile(sourceFileName, entryName, compressionLevel);
                    }
                }
            }
            catch (IOException e)
            {
                throw new IOException(nameof(GetFullPath), e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Deletes an entry from the archive.</summary>
        /// <param name="archive">The archive.</param>
        /// <param name="entryName">The entry to delete.</param>
        public static void DeleteEntry(Archive archive, string entryName)
        {
            ExceptionsManager.IsNull(archive);
            ExceptionsManager.IsNullOrEmpty(entryName);

            if (!FileExists(archive, entryName))
            {
                throw new FileNotFoundException("The " + nameof(entryName) + " was not found in the " + nameof(archive));
            }

            try
            {
                using (FileStream _zipFile = new FileStream(archive.GetFullPath, FileMode.Open))
                {
                    using (ZipArchive _archive = new ZipArchive(_zipFile, ZipArchiveMode.Update))
                    {
                        foreach (ZipArchiveEntry _entry in _archive.Entries)
                        {
                            if (_entry.FullName == entryName)
                            {
                                _entry.Delete();
                            }
                        }
                    }
                }
            }
            catch (IOException e)
            {
                throw new IOException(nameof(GetFullPath), e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Extract the archive to a directory.</summary>
        /// <param name="archive">The archive.</param>
        /// <param name="directory">The directory output.</param>
        public static void ExtractToDirectory(Archive archive, string directory)
        {
            ZipFile.ExtractToDirectory(archive.GetFullPath, directory);
        }

        /// <summary>Extract file from the archive.</summary>
        /// <param name="archive">The archive.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="output">The output.</param>
        /// <param name="overwrite">The overwrite toggle.</param>
        public static void ExtractToFile(Archive archive, string fileName, string output, bool overwrite = true)
        {
            ExceptionsManager.IsNullOrEmpty(archive.GetFullPath);
            ExceptionsManager.FileExists(archive.GetFullPath);
            ExceptionsManager.IsNullOrEmpty(fileName);
            ExceptionsManager.IsNullOrEmpty(output);

            if (!overwrite)
            {
                ExceptionsManager.FileExists(output);
            }

            if (!FileExists(archive, fileName))
            {
                throw new FileNotFoundException(nameof(fileName) + " was not found in the archive.");
            }

            try
            {
                using (FileStream _fileStream = new FileStream(archive.GetFullPath, FileMode.Open))
                {
                    using (ZipArchive _archive = new ZipArchive(_fileStream, ZipArchiveMode.Read))
                    {
                        foreach (ZipArchiveEntry _entry in _archive.Entries)
                        {
                            if (_entry.FullName == fileName)
                            {
                                _entry.ExtractToFile(output, overwrite);
                            }
                        }
                    }
                }
            }
            catch (IOException e)
            {
                throw new IOException(nameof(output), e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Check if the archive contains the file name.</summary>
        /// <param name="archive">The archive.</param>
        /// <param name="fileName">The file name.</param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        public static bool FileExists(Archive archive, string fileName)
        {
            ExceptionsManager.IsNull(archive);
            ExceptionsManager.FileExists(archive.GetFullPath);

            try
            {
                using (FileStream _fileStream = new FileStream(archive.GetFullPath, FileMode.Open))
                {
                    using (ZipArchive _archive = new ZipArchive(_fileStream, ZipArchiveMode.Read))
                    {
                        foreach (ZipArchiveEntry _entry in _archive.Entries)
                        {
                            if (_entry.FullName == fileName)
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

        /// <summary>Get the entry count from the archive.</summary>
        /// <param name="archive">The archive.</param>
        /// <returns>
        ///     <see cref="int" />
        /// </returns>
        public static int GetCount(Archive archive)
        {
            return archive.Entries.Length;
        }

        /// <summary>Gets the entry count from the archive.</summary>
        /// <param name="archivePath">The archive path.</param>
        /// <returns>
        ///     <see cref="int" />
        /// </returns>
        public static int GetCount(string archivePath)
        {
            ExceptionsManager.IsNullOrEmpty(archivePath);
            ExceptionsManager.FileExists(archivePath);

            using (ZipArchive _archive = ZipFile.OpenRead(archivePath))
            {
                return _archive.Entries.Count;
            }
        }

        /// <summary>Reads the archive entries.</summary>
        /// <param name="archivePath">The archive path.</param>
        /// <returns>
        ///     <see cref="ZipArchiveEntry" />
        /// </returns>
        public static ZipArchiveEntry[] ReadEntries(string archivePath)
        {
            ExceptionsManager.IsNullOrEmpty(archivePath);
            ExceptionsManager.FileExists(archivePath);

            var _entries = new ZipArchiveEntry[GetCount(archivePath)];

            try
            {
                using (ZipArchive _archive = ZipFile.OpenRead(archivePath))
                {
                    for (var i = 0; i < _archive.Entries.Count; i++)
                    {
                        ZipArchiveEntry _archiveEntry = _archive.Entries[i];

                        _entries[i] = _archiveEntry;
                    }

                    return _entries;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>Closes the archive.</summary>
        public void Close()
        {
            DisposeInternal(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Disposes this instance.</summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>Dispose internals.</summary>
        /// <param name="disposing">Disposing toggle.</param>
        public void DisposeInternal(bool disposing)
        {
            if (!_isDisposed)
            {
                _zipEntries = null;
                _isDisposed = true;
            }
        }

        /// <summary>Read the archive file filling the entries array.</summary>
        public void Read()
        {
            ExceptionsManager.IsNullOrEmpty(GetFullPath);
            ExceptionsManager.FileExists(GetFullPath);

            _zipEntries = new ZipArchiveEntry[GetCount(GetFullPath)];
            _zipEntries = ReadEntries(GetFullPath);

            _isNewArchive = Count <= 0;
        }

        /// <summary>Saves the archive to file.</summary>
        /// <param name="output">The file path output.</param>
        public void Save(string output)
        {
            ExceptionsManager.IsNullOrEmpty(output);
            ExceptionsManager.IsNullOrEmpty(_directory);
            ExceptionsManager.IsNullOrEmpty(_name);
            ExceptionsManager.IsNullOrEmpty(_extension);

            string _archiveOutputPath = _directory + _name + _extension;

            if (_isNewArchive)
            {
                CreateEmptyArchive(_archiveOutputPath);
                _isNewArchive = false;
            }
            else
            {
                SaveArchiveContents(_archiveOutputPath);
            }
        }

        /// <summary>Displays the archive ToString.</summary>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public override string ToString()
        {
            return GetFullPath;
        }

        /// <summary>Releases the un-managed resources used by the this instance and optionally releases the managed resources.</summary>
        /// <param name="disposing">
        ///     true to release both managed and un-managed resources false to release only un-managed
        ///     resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            DisposeInternal(disposing);
        }

        /// <summary>Overwrite the file.</summary>
        /// <param name="archive">The archive.</param>
        /// <param name="entryName">The entry name.</param>
        /// <param name="overwrite">The overwrite toggle.</param>
        private static void Overwrite(Archive archive, string entryName, bool overwrite)
        {
            if (!FileExists(archive, entryName))
            {
                return;
            }

            if (overwrite)
            {
                DeleteEntry(archive, entryName);
            }
            else
            {
                throw new IOException("The " + nameof(entryName) + " already exists in the " + nameof(archive) + ".");
            }
        }

        /// <summary>Disposable Dispose.</summary>
        void IDisposable.Dispose()
        {
            Close();
        }

        /// <summary>Opens an entry to a stream.</summary>
        /// <param name="entryName">The entry name.</param>
        /// <returns>
        ///     <see cref="Stream" />
        /// </returns>
        private Stream EntryToStream(string entryName)
        {
            try
            {
                using (FileStream _zipFile = new FileStream(GetFullPath, FileMode.Open))
                {
                    using (ZipArchive _archive = new ZipArchive(_zipFile, ZipArchiveMode.Update))
                    {
                        foreach (ZipArchiveEntry _entry in _archive.Entries)
                        {
                            if (_entry.FullName == entryName)
                            {
                                return _entry.Open();
                            }
                        }
                    }
                }
            }
            catch (IOException e)
            {
                throw new IOException(nameof(GetFullPath), e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        /// <summary>Opens the archive file stream.</summary>
        /// <param name="fileStream">The archive file stream.</param>
        private void OpenArchiveStream(FileStream fileStream)
        {
            _name = Path.GetFileNameWithoutExtension(fileStream.Name);
            _directory = Path.GetDirectoryName(fileStream.Name) + @"\";
            _extension = Path.GetExtension(fileStream.Name);
            fileStream.Close();
        }

        /// <summary>Save archive contents to a file.</summary>
        /// <param name="output">The output path.</param>
        private void SaveArchiveContents(string output)
        {
            try
            {
                using (FileStream _fileStream = new FileStream(output, FileMode.CreateNew))
                {
                    using (ZipArchive _archive = new ZipArchive(_fileStream, ZipArchiveMode.Create))
                    {
                        if (_zipEntries.Length > 0)
                        {
                            foreach (ZipArchiveEntry _entry in _zipEntries)
                            {
                                _archive.CreateEntryFromFile(_entry.FullName, _entry.Name, _compression);
                            }

                            _isNewArchive = false;
                        }
                        else
                        {
                            _isNewArchive = true;
                        }
                    }
                }
            }
            catch (IOException e)
            {
                throw new IOException(nameof(GetFullPath), e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }
}