namespace Comet.Structure
{
    #region Namespace

    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml.Linq;

    #endregion

    public class Logger
    {
        #region Variables

        private string _filePath;
        private WriteMode _writeMode;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Logger" /> class.
        /// </summary>
        /// <param name="logSettings">The logger settings.</param>
        public Logger(LogSettings logSettings)
        {
            _filePath = logSettings.FilePath;
            _writeMode = logSettings.WriteMode;
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
        ///     The file path.
        /// </summary>
        public string FilePath
        {
            get
            {
                return _filePath;
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
        /// <param name="logSettings">The log settings.</param>
        /// <param name="message">The message.</param>
        public static void Log(LogSettings logSettings, string message)
        {
            switch (logSettings.WriteMode)
            {
                case WriteMode.Text:
                    {
                        LogText(logSettings, message);
                        break;
                    }

                case WriteMode.XML:
                    {
                        LogXML(logSettings, message);
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
        /// <param name="logSettings">The log settings.</param>
        /// <param name="exception">The exception.</param>
        public static void LogException(LogSettings logSettings, Exception exception)
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

                WriteLine(logSettings, _xmlEntry);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     Write using text mode.
        /// </summary>
        /// <param name="logSettings">The log Settings.</param>
        /// <param name="message">The message.</param>
        private static void LogText(LogSettings logSettings, string message)
        {
            string _layout = $"{GetTimeFormatString()} : {message}";

            try
            {
                WriteLine(logSettings, _layout);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Write using XML mode.</summary>
        /// <param name="logSettings">The log Settings.</param>
        /// <param name="message">The message.</param>
        private static void LogXML(LogSettings logSettings, string message)
        {
            try
            {
                XElement _xmlEntryLog = new XElement(
                    "Entry",
                    new XElement("Date", GetTimeFormatString()),
                    new XElement("Message", message));

                WriteLine(logSettings, _xmlEntryLog);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     Write line to log.
        /// </summary>
        /// <param name="logSettings">The log settings.</param>
        /// <param name="message">The message.</param>
        private static void WriteLine(LogSettings logSettings, object message)
        {
            try
            {
                StreamWriter _streamWriter = new StreamWriter(logSettings.FilePath, true);
                _streamWriter.WriteLine(message);
                _streamWriter.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Methods

        public struct LogSettings
        {
            public WriteMode WriteMode { get; set; }

            public string FilePath { get; set; }
        }

        #endregion
    }
}