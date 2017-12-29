namespace PackageManager.Structure
{
    #region Namespace

    using System;
    using System.IO;

    #endregion

    public class HistoryLogEntry : ICloneable
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

        public DateTime DateModified
        {
            get
            {
                return _dateModified;
            }

            set
            {
                _dateModified = value;
            }
        }

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

        #endregion

        #region Events

        public object Clone()
        {
            return this;
        }

        public override bool Equals(object obj)
        {
            HistoryLogEntry _historyLogEntryObject = (HistoryLogEntry)obj;

            if (_historyLogEntryObject == null)
            {
                return false;
            }

            return _filePath == _historyLogEntryObject.FilePath;
        }

        public override int GetHashCode()
        {
            int _hashedFilePath = FilePath == null ? 0 : FilePath.GetHashCode();

            return _hashedFilePath;
        }

        #endregion
    }
}