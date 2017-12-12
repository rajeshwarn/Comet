namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.Text;

    #endregion

    public class StringManager
    {
        #region Events

        /// <summary>Create exception string.</summary>
        /// <param name="e">The e.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string ExceptionString(Exception e)
        {
            StringBuilder _exception = new StringBuilder();
            _exception.AppendLine("An unhandled exception has occurred in a component in your application.");
            _exception.Append(Environment.NewLine);
            _exception.AppendLine("Message: " + e.Message);
            _exception.Append(Environment.NewLine);
            _exception.AppendLine("Stack Trace: " + e.StackTrace);
            _exception.Append(Environment.NewLine);
            _exception.AppendLine("Help Link: " + e.HelpLink);
            _exception.AppendLine("HResult: " + e.HResult);
            _exception.AppendLine("Source: " + e.Source);
            _exception.AppendLine("Target Site: " + e.TargetSite);
            return _exception.ToString();
        }

        /// <summary>Create file not found string.</summary>
        /// <param name="path">The package path.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string FileNotFound(string path)
        {
            StringBuilder _fileNotFound = new StringBuilder();
            _fileNotFound.AppendLine("Unable to locate the file using the following path.");
            _fileNotFound.Append(Environment.NewLine);
            _fileNotFound.AppendLine("Path: " + path);
            return _fileNotFound.ToString();
        }

        /// <summary>Create is null or empty string.</summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string IsNull(object value)
        {
            StringBuilder _isNullOrEmpty = new StringBuilder();
            _isNullOrEmpty.AppendLine("The object is null.");
            _isNullOrEmpty.Append(Environment.NewLine);
            _isNullOrEmpty.AppendLine("Object: " + nameof(value));
            _isNullOrEmpty.AppendLine("Type: " + value.GetType());
            return _isNullOrEmpty.ToString();
        }

        /// <summary>Create is null or empty string.</summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string IsNullOrEmpty(string text)
        {
            StringBuilder _isNullOrEmpty = new StringBuilder();
            _isNullOrEmpty.AppendLine("The string is null or empty.");
            _isNullOrEmpty.Append(Environment.NewLine);
            _isNullOrEmpty.AppendLine("String: " + nameof(text));
            return _isNullOrEmpty.ToString();
        }

        /// <summary>Create package not found string.</summary>
        /// <param name="path">The package path.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string PackageNotFound(string path)
        {
            StringBuilder _packageNotFound = new StringBuilder();
            _packageNotFound.AppendLine("Unable to locate the package using the following path.");
            _packageNotFound.Append(Environment.NewLine);
            _packageNotFound.AppendLine("Path: " + path);
            return _packageNotFound.ToString();
        }

        /// <summary>Create remote file not found string.</summary>
        /// <param name="url">The url.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string RemoteFileNotFound(string url)
        {
            StringBuilder _remoteFileNotFound = new StringBuilder();
            _remoteFileNotFound.AppendLine("Unable to locate the remote file using the following URL.");
            _remoteFileNotFound.Append(Environment.NewLine);
            _remoteFileNotFound.AppendLine("URL: " + url);
            return _remoteFileNotFound.ToString();
        }

        /// <summary>Create URL is not well formatted string.</summary>
        /// <param name="url">The url.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string UrlNotWellFormatted(string url)
        {
            StringBuilder _urlNotWellFormatted = new StringBuilder();
            _urlNotWellFormatted.AppendLine("The URL is not well formatted.");
            _urlNotWellFormatted.Append(Environment.NewLine);
            _urlNotWellFormatted.AppendLine("URL: " + url);
            return _urlNotWellFormatted.ToString();
        }

        #endregion
    }
}