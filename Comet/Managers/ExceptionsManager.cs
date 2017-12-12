namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.Data;
    using System.IO;
    using System.Windows.Forms;

    #endregion

    public class ExceptionsManager
    {
        #region Events

        /// <summary>Throws a <see cref="ArgumentException" />.</summary>
        /// <param name="stream">The file stream.</param>
        public static void CanSeek(Stream stream)
        {
            if (!stream.CanSeek)
            {
                throw new ArgumentException(@"Stream is not seek able.", nameof(stream));
            }
        }

        /// <summary>Displays the exception.</summary>
        /// <param name="e">The exception.</param>
        public static void DisplayException(Exception e)
        {
            MessageBox.Show(e.Message, @"Comet", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>Throws a <see cref="FileNotFoundException" />.</summary>
        /// <param name="path">The file path.</param>
        public static void FileExists(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(StringManager.FileNotFound(path));
            }
        }

        /// <summary>Throws a <see cref="NoNullAllowedException" />.</summary>
        /// <param name="value">The value.</param>
        public static void IsNull(object value)
        {
            if (value == null)
            {
                throw new NoNullAllowedException(StringManager.IsNull(value));
            }
        }

        /// <summary>Throws a <see cref="NoNullAllowedException" />.</summary>
        /// <param name="value">The value.</param>
        public static void IsNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new NoNullAllowedException(StringManager.IsNullOrEmpty(value));
            }
        }

        /// <summary>Throws a <see cref="ObjectDisposedException" />.</summary>
        /// <param name="value">The value.</param>
        /// <param name="disposed">The disposed toggle.</param>
        public static void ObjectDisposed(object value, bool disposed)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(value));
            }
        }

        #endregion
    }
}