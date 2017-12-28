namespace PackageManager.Structure
{
    #region Namespace

    using System;
    using System.IO;

    #endregion

    public class HistoryLogEntry
    {
        #region Variables

        private DateTime _dateModified;

        private string _fileName;
        private string _filePath;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HistoryLogEntry" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public HistoryLogEntry(string filePath)
        {
            _filePath = filePath;
            _fileName = Path.GetFileName(_filePath);
        }

        #endregion

        #region Properties

        public DateTime DateModified { get; set; }

        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        public string FilePath
        {
            get
            {
                return _filePath;
            }
        }

        public int ID { get; set; }

        #endregion

        #region Events

        public bool Equals(HistoryLogEntry other)
        {
            if (FilePath == other.FilePath)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hashFirstName = FilePath == null ? 0 : FilePath.GetHashCode();

            // int hashLastName = DateModified == null ? 0 : DateModified.GetHashCode();
            return hashFirstName;
        }

        #endregion
    }
}