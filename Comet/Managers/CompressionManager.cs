namespace Comet.Managers
{
    #region Namespace

    using System.IO.Compression;

    #endregion

    /// <summary>The <see cref="CompressionManager" />.</summary>
    public class CompressionManager
    {
        #region Events

        /// <summary>Compress directory to an archive.</summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="output">The output file name.</param>
        /// <param name="compressionLevel">The compression level.</param>
        public static void CompressDirectory(string directoryPath, string output, CompressionLevel compressionLevel)
        {
            ZipFile.CreateFromDirectory(directoryPath, output, compressionLevel, false);
        }

        /// <summary>Extract the archive to a directory.</summary>
        /// <param name="archivePath">The archive path.</param>
        /// <param name="output">The directory output.</param>
        public static void ExtractToDirectory(string archivePath, string output)
        {
            ZipFile.ExtractToDirectory(archivePath, output);
        }

        #endregion
    }
}