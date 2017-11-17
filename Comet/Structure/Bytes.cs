namespace Comet.Structure
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.InteropServices;

    #endregion

    /// <summary>The Bytes structure.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [Description("The Bytes structure allows you to easily represent file sizes.")]
    [DesignerCategory("code")]
    public sealed class Bytes
    {
        #region Variables

        private bool _abbreviated;
        private Abbreviations _abbreviations;
        private FileSizeTypes _fileSizeTypes;
        private bool _formatted;
        private long _formattedSize;
        private long _totalSize;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="Bytes" /> class.</summary>
        /// <param name="value">The total value size.</param>
        public Bytes(long value) : this()
        {
            Update(value);
        }

        /// <summary>Initializes a new instance of the <see cref="Bytes" /> class.</summary>
        /// <param name="file">The file.</param>
        public Bytes(string file) : this()
        {
            Update(GetFileSize(file));
        }

        /// <summary>Initializes a new instance of the <see cref="Bytes" /> class.</summary>
        public Bytes()
        {
            _abbreviated = false;
            _abbreviations = Abbreviations.B;
            _fileSizeTypes = FileSizeTypes.Bytes;
            _formatted = true;
            _formattedSize = 0;
            _totalSize = 0;

            Update(_totalSize);
        }

        /// <summary>The <see cref="Abbreviations" />.</summary>
        [DefaultValue(B)]
        public enum Abbreviations
        {
            /// <summary>The bytes.</summary>
            B = 0,

            /// <summary>The kilobytes.</summary>
            KB = 1,

            /// <summary>The megabytes.</summary>
            MB = 2,

            /// <summary>The gigabytes.</summary>
            GB = 3,

            /// <summary>The terabytes.</summary>
            TB = 4,

            /// <summary>The petabytes.</summary>
            PB = 5
        }

        /// <summary>The <see cref="FileSizeTypes" />.</summary>
        [DefaultValue(Bytes)]
        public enum FileSizeTypes
        {
            /// <summary>The bytes.</summary>
            Bytes = 0,

            /// <summary>The kilobytes.</summary>
            Kilobytes = 1,

            /// <summary>The megabytes.</summary>
            Megabytes = 2,

            /// <summary>The gigabytes.</summary>
            Gigabytes = 3,

            /// <summary>The terabytes.</summary>
            Terabytes = 4,

            /// <summary>The petabytes.</summary>
            Petabytes = 5
        }

        #endregion

        #region Properties

        /// <summary>Determines whether the <see cref="string"></see> is abbreviated.</summary>
        [DefaultValue(false)]
        public bool Abbreviated
        {
            get
            {
                return _abbreviated;
            }

            set
            {
                if (_abbreviated == value)
                {
                    return;
                }

                _abbreviated = value;
                Update(_totalSize);
            }
        }

        /// <summary>The <see cref="Abbreviation" />.</summary>
        [DefaultValue(typeof(FileSizeTypes), "B")]
        public Abbreviations Abbreviation
        {
            get
            {
                return _abbreviations;
            }
        }

        /// <summary>Determines whether the <see cref="Bytes"></see> is formatted.</summary>
        [DefaultValue(true)]
        public bool Formatted
        {
            get
            {
                return _formatted;
            }

            set
            {
                if (_formatted == value)
                {
                    return;
                }

                _formatted = value;
                Update(_totalSize);
            }
        }

        /// <summary>The amount of <see cref="long"></see> in the <see cref="Bytes"></see>.</summary>
        [DefaultValue(0)]
        public long FormattedSize
        {
            get
            {
                return _formattedSize;
            }
        }

        /// <summary>The <see cref="SizeType" />.</summary>
        [DefaultValue(typeof(FileSizeTypes), "Bytes")]
        public FileSizeTypes SizeType
        {
            get
            {
                return _fileSizeTypes;
            }
        }

        /// <summary>The total amount of <see cref="long"></see> in the <see cref="Bytes"></see>.</summary>
        [DefaultValue(0)]
        public long TotalSize
        {
            get
            {
                return _totalSize;
            }

            set
            {
                if (_totalSize == value)
                {
                    return;
                }

                _totalSize = value;
                Update(_totalSize);
            }
        }

        #endregion

        #region Events

        /// <summary>Get the abbreviation of the size type.</summary>
        /// <param name="fileSizeTypes">The size type.</param>
        /// <returns>The <see cref="Abbreviations" />.</returns>
        public static Abbreviations ConvertToAbbreviation(FileSizeTypes fileSizeTypes)
        {
            Abbreviations _abbreviations;

            switch (fileSizeTypes)
            {
                case FileSizeTypes.Bytes:
                    {
                        _abbreviations = Abbreviations.B;
                        break;
                    }

                case FileSizeTypes.Kilobytes:
                    {
                        _abbreviations = Abbreviations.KB;
                        break;
                    }

                case FileSizeTypes.Megabytes:
                    {
                        _abbreviations = Abbreviations.MB;
                        break;
                    }

                case FileSizeTypes.Gigabytes:
                    {
                        _abbreviations = Abbreviations.GB;
                        break;
                    }

                case FileSizeTypes.Terabytes:
                    {
                        _abbreviations = Abbreviations.TB;
                        break;
                    }

                case FileSizeTypes.Petabytes:
                    {
                        _abbreviations = Abbreviations.PB;
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(fileSizeTypes), fileSizeTypes, null);
                    }
            }

            return _abbreviations;
        }

        /// <summary>Get the full size type.</summary>
        /// <param name="abbreviations">The abbreviation.</param>
        /// <returns>The <see cref="FileSizeTypes" />.</returns>
        public static FileSizeTypes ConvertToSizeType(Abbreviations abbreviations)
        {
            FileSizeTypes _fileSizeTypes;

            switch (abbreviations)
            {
                case Abbreviations.B:
                    {
                        _fileSizeTypes = FileSizeTypes.Bytes;
                        break;
                    }

                case Abbreviations.KB:
                    {
                        _fileSizeTypes = FileSizeTypes.Kilobytes;
                        break;
                    }

                case Abbreviations.MB:
                    {
                        _fileSizeTypes = FileSizeTypes.Megabytes;
                        break;
                    }

                case Abbreviations.GB:
                    {
                        _fileSizeTypes = FileSizeTypes.Gigabytes;
                        break;
                    }

                case Abbreviations.TB:
                    {
                        _fileSizeTypes = FileSizeTypes.Terabytes;
                        break;
                    }

                case Abbreviations.PB:
                    {
                        _fileSizeTypes = FileSizeTypes.Petabytes;
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(abbreviations), abbreviations, null);
                    }
            }

            return _fileSizeTypes;
        }

        /// <summary>Format bytes.</summary>
        /// <param name="value">The value.</param>
        /// <param name="division">The division.</param>
        /// <returns>The <see cref="long" />.</returns>
        public static long FormatBytes(long value, long division = 1000)
        {
            var _count = 0;

            while ((value >= division) && (_count + 1 < Enum.GetNames(typeof(FileSizeTypes)).Length))
            {
                _count++;
                value = value / division;
            }

            return value;
        }

        /// <summary>Retrieves the Abbreviations.</summary>
        /// <param name="value">The value.</param>
        /// <param name="formatted">The formatting toggle.</param>
        /// <returns>The <see cref="Abbreviations" />.</returns>
        public static Abbreviations GetAbbreviations(long value, bool formatted)
        {
            int _count = formatted ? GetStepCount(value) : 0;
            Abbreviations _abbreviations = (Abbreviations)_count;
            return _abbreviations;
        }

        /// <summary>Gets the size, in bytes, of the current file.</summary>
        /// <param name="file">The fully qualified name of the file, or the relative file name.</param>
        /// <returns>The <see cref="long" />.</returns>
        public static long GetFileSize(string file)
        {
            if (File.Exists(file))
            {
                FileInfo _fileInfo = new FileInfo(file);
                return _fileInfo.Length;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>Retrieves the FileSizeType.</summary>
        /// <param name="value">The value.</param>
        /// <param name="formatted">The formatting toggle.</param>
        /// <returns>The <see cref="FileSizeTypes" />.</returns>
        public static FileSizeTypes GetFileSizeType(long value, bool formatted)
        {
            int _count = formatted ? GetStepCount(value) : 0;
            FileSizeTypes _fileSizeTypes = (FileSizeTypes)_count;
            return _fileSizeTypes;
        }

        /// <summary>Retrieve the step count from the value.</summary>
        /// <param name="value">The total bytes value.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int GetStepCount(long value)
        {
            var _count = 0;

            while ((value >= 1000) && (_count + 1 < Enum.GetNames(typeof(FileSizeTypes)).Length))
            {
                _count++;
                value = value / 1000;
            }

            return _count;
        }

        /// <inheritdoc />
        /// <returns>The <see cref="string" />.</returns>
        public override string ToString()
        {
            long _bytesValue = _formatted ? FormatBytes(_totalSize) : _totalSize;
            string _byteExtension = _abbreviated ? GetAbbreviations(_totalSize, _formatted).ToString() : GetFileSizeType(_totalSize, _formatted).ToString();
            return _bytesValue.ToString("0.##") + " " + _byteExtension;
        }

        /// <summary>Update the structure.</summary>
        /// <param name="value">The total bytes value.</param>
        private void Update(long value)
        {
            _formattedSize = value;
            _totalSize = value;

            if (_formatted)
            {
                _formattedSize = FormatBytes(_totalSize);
            }

            _fileSizeTypes = GetFileSizeType(_totalSize, _formatted);
            _abbreviations = ConvertToAbbreviation(_fileSizeTypes);
        }

        #endregion
    }
}