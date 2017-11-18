namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.Data;
    using System.IO;

    using Comet.Structure;

    #endregion

    internal class ExceptionsManager
    {
        #region Events

        /// <summary>Throws a <see cref="ArgumentException" />.</summary>
        /// <param name="stream">The file stream.</param>
        public static void CanSeek(Stream stream)
        {
            if (!stream.CanSeek)
            {
                throw new ArgumentException("Stream is not seek able.", nameof(stream));
            }
        }

        /// <summary>Creates an exception command not recognized message.</summary>
        /// <param name="command">The command.</param>
        public static void CommandNotRecognized(ConsoleCommand command)
        {
            WriteException($"\'{command.Name}\' is not recognized as a command. ");
            Console.WriteLine();
        }

        /// <summary>Throws a <see cref="ObjectDisposedException"/>.</summary>
        /// <param name="value">The value.</param>
        /// <param name="disposed">The disposed toggle.</param>
        public static void ObjectDisposed(object value, bool disposed)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(value));
            }
        }

        /// <summary>Throws a <see cref="FileNotFoundException" />.</summary>
        /// <param name="path">The file path.</param>
        public static void FileExists(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(nameof(path));
            }
        }

        /// <summary>Throws a <see cref="NoNullAllowedException" />.</summary>
        /// <param name="value">The value.</param>
        public static void IsNull(object value)
        {
            if (value == null)
            {
                throw new NoNullAllowedException(nameof(value));
            }
        }

        /// <summary>Throws a <see cref="NoNullAllowedException" />.</summary>
        /// <param name="value">The value.</param>
        public static void IsNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new NoNullAllowedException(nameof(value));
            }
        }

        /// <summary>Creates an file not found exception.</summary>
        /// <param name="file">The path to the file.</param>
        public static void ShowFileNotFoundException(string file)
        {
            WriteException("The system cannot find the specified file.");
            WriteException("Path: '" + file + "'");
        }

        /// <summary>Shows an exception null or empty exception message.</summary>
        /// <param name="field">The field.</param>
        public static void ShowNullOrEmpty(string field)
        {
            Console.ForegroundColor = Settings.ErrorColor;
            Console.Write(Settings.ErrorCharacter + " ");
            Console.ForegroundColor = Settings.ErrorTextColor;
            Console.Write("The value is null or empty. The field '" + field + "' must contain a value.");
        }

        /// <summary>Creates a web exception file not found on remote server.</summary>
        /// <param name="file">The path to the file.</param>
        public static void ShowSourceNotFoundException(string file)
        {
            WriteException("The remote server cannot find the specified file.");
            WriteException("Path: '" + file + "'");
        }

        /// <summary>Creates an exception message.</summary>
        /// <param name="message">The message.</param>
        public static void WriteException(string message)
        {
            Console.ForegroundColor = Settings.ErrorColor;
            Console.Write(Settings.ErrorCharacter + " ");
            Console.ForegroundColor = Settings.ErrorTextColor;
            Console.WriteLine(message);
        }

        #endregion
    }
}