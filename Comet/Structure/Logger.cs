namespace Comet.Structure
{
    #region Namespace

    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml.Linq;

    #endregion

    public class Logger
    {
        #region Variables

        private string _directory;
        private string _extension;
        private string _fileName;
        private WriteMode _writeMode;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="Logger" /> class.</summary>
        /// <param name="directory">The directory name.</param>
        /// <param name="extension">The file extension.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="writeMode">The write mode.</param>
        public Logger(string directory = null, string extension = ".log", string fileName = "Log", WriteMode writeMode = WriteMode.Text)
        {
            _fileName = fileName;
            _directory = directory;
            _extension = extension;
            _writeMode = writeMode;
        }

        /// <summary>Initializes a new instance of the <see cref="Logger" /> class.</summary>
        /// <param name="extension">The extension.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="writeMode">The write mode.</param>
        public Logger(string extension = ".log", string fileName = "Log", WriteMode writeMode = WriteMode.Text)
        {
            _directory = string.Empty;
            _extension = extension;
            _fileName = fileName;
            _writeMode = writeMode;
        }

        public enum WriteMode
        {
            /// <summary>Text mode.</summary>
            Text,

            /// <summary>XML Mode.</summary>
            XML
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The directory path.
        /// </summary>
        public string DirectoryPath
        {
            get
            {
                string _folder;
                if (string.IsNullOrEmpty(_directory))
                {
                    _folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                }
                else
                {
                    _folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + _directory;
                }

                return $"{_folder}\\";
            }
        }

        /// <summary>
        ///     The file extension.
        /// </summary>
        public string Extension
        {
            get
            {
                return _extension;
            }
        }

        /// <summary>
        ///     The file name.
        /// </summary>
        public string FileName
        {
            get
            {
                return $"{_fileName}{_extension}";
            }
        }

        /// <summary>
        ///     The full log path.
        /// </summary>
        public string LogFullPath
        {
            get
            {
                return $"{DirectoryPath}{FileName}";
            }
        }

        /// <summary>
        ///     The write mode.
        /// </summary>
        public WriteMode Mode
        {
            get
            {
                return _writeMode;
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Returns the long time format string.
        /// </summary>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string GetTimeFormatString()
        {
            return DateTime.Now.ToLongTimeString();
        }

        /// <summary>Write the log entry to file.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        public static void Log(Logger logger, string message)
        {
            switch (logger.Mode)
            {
                case WriteMode.Text:
                    {
                        LogText(logger, message);
                        break;
                    }

                case WriteMode.XML:
                    {
                        LogXML(logger, message);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }
        }

        /// <summary>
        ///     Write exception log.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        public static void LogException(Logger logger, Exception exception)
        {
            try
            {
                XElement _xmlEntry = new XElement(
                    "Entry",
                    new XElement("Date", GetTimeFormatString()),
                    new XElement("Exception",
                        new XElement("Message", exception.Message),
                        new XElement("Type", exception.GetType().FullName),
                        new XElement("Stack Trace", exception.StackTrace),
                        new XElement("Help Link", exception.HelpLink),
                        new XElement("HResult", exception.HResult),
                        new XElement("Source", exception.Source),
                        new XElement("Target Site", exception.TargetSite)));

                if (exception.InnerException != null)
                {
                    _xmlEntry.Element("Exception").Add(new XElement("InnerException",
                        new XElement("Source", exception.InnerException.Source),
                        new XElement("Message", exception.InnerException.Message),
                        new XElement("Stack", exception.InnerException.StackTrace),
                        new XElement("Type", exception.InnerException.GetType().FullName)));
                }

                WriteLine(logger, _xmlEntry);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     Write using text mode.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        private static void LogText(Logger logger, string message)
        {
            string _layout = $"{GetTimeFormatString()} : {message}";

            try
            {
                WriteLine(logger, _layout);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     Write using XML mode.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        private static void LogXML(Logger logger, string message)
        {
            try
            {
                XElement _xmlEntryLog = new XElement(
                    "Entry",
                    new XElement("Date", GetTimeFormatString()),
                    new XElement("Message", message));

                WriteLine(logger, _xmlEntryLog);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     Write line to log.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        private static void WriteLine(Logger logger, object message)
        {
            try
            {
                StreamWriter _streamWriter = new StreamWriter(logger.LogFullPath, true);
                _streamWriter.WriteLine(message);
                _streamWriter.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}