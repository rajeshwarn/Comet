namespace Comet.Managers
{
    #region Namespace

    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.AccessControl;
    using System.Security.Principal;

    #endregion

    /// <summary>The <see cref="FileManager" />.</summary>
    public class FileManager
    {
        #region Events

        /// <summary>Copy the file access control rules.</summary>
        /// <param name="source">The source file.</param>
        /// <param name="destination">The destination file.</param>
        public static void CopyAccessControl(FileInfo source, FileInfo destination)
        {
            FileSecurity _fileSecurity = source.GetAccessControl();

            bool _hasInheritanceRules = _fileSecurity.GetAccessRules(false, true, typeof(SecurityIdentifier)).Count > 0;

            if (_hasInheritanceRules)
            {
                _fileSecurity.SetAccessRuleProtection(false, false);
            }
            else
            {
                _fileSecurity.SetAccessRuleProtection(true, true);
            }

            destination.SetAccessControl(_fileSecurity);
        }

        /// <summary>Create a directory.</summary>
        /// <param name="directory">The folder directory.</param>
        public static void CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>Create temp folder.</summary>
        /// <param name="path">The folder path.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string CreateTempPath(string path = "")
        {
            return Path.GetTempPath() + path;
        }

        /// <summary>Delete a directory.</summary>
        /// <param name="directory">The directory path.</param>
        public static void DeleteDirectory(string directory)
        {
            var _files = Directory.GetFiles(directory);
            var _directories = Directory.GetDirectories(directory);

            foreach (string file in _files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string subDirectory in _directories)
            {
                DeleteDirectory(subDirectory);
            }

            File.SetAttributes(directory, FileAttributes.Normal);
            Directory.Delete(directory, false);
        }

        /// <summary>Get the files <see cref="IEnumerable" />.</summary>
        /// <param name="path">The directory path.</param>
        /// <param name="searchPatterns">The search patterns.</param>
        /// <param name="option">The search option.</param>
        /// <returns><see cref="IEnumerable" />.</returns>
        public static IEnumerable GetFiles(string path, string searchPatterns, SearchOption option)
        {
            var _patterns = searchPatterns.Split('|');
            var _files = new List<string>();

            foreach (string searchPattern in _patterns)
            {
                _files.AddRange(Directory.GetFiles(path, searchPattern, option));
            }

            return _files;
        }

        /// <summary>Have write permissions for file or folder.</summary>
        /// <param name="path">The path.</param>
        /// <returns><see cref="bool" />.</returns>
        public static bool HaveWritePermissionsForFileOrFolder(string path)
        {
            SecurityIdentifier _securityIdentifier = WindowsIdentity.GetCurrent().User;
            if (_securityIdentifier == null)
            {
                return false;
            }

            string _currentUser = _securityIdentifier.Value;

            IdentityReferenceCollection _groups = WindowsIdentity.GetCurrent().Groups;
            AuthorizationRuleCollection _rules = Directory.GetAccessControl(path).GetAccessRules(true, true, typeof(SecurityIdentifier));
            var _allowWrite = false;
            var _denyWrite = false;

            foreach (FileSystemAccessRule rule in _rules)
            {
                if ((rule.AccessControlType == AccessControlType.Deny) && ((rule.FileSystemRights & FileSystemRights.WriteData) == FileSystemRights.WriteData) &&
                    (_groups.Contains(rule.IdentityReference) || (rule.IdentityReference.Value == _currentUser)))
                {
                    _denyWrite = true;
                }

                if ((rule.AccessControlType == AccessControlType.Allow) && ((rule.FileSystemRights & FileSystemRights.WriteData) == FileSystemRights.WriteData) &&
                    (_groups.Contains(rule.IdentityReference) || (rule.IdentityReference.Value == _currentUser)))
                {
                    _allowWrite = true;
                }
            }

            return _allowWrite && !_denyWrite;
        }

        /// <summary>Have write permissions for folder.</summary>
        /// <param name="path">The path.</param>
        /// <returns><see cref="bool" />.</returns>
        public static bool HaveWritePermissionsForFolder(string path)
        {
            string _folder = IsDirectory(path) ? path : Path.GetDirectoryName(path);
            return HaveWritePermissionsForFileOrFolder(_folder);
        }

        /// <summary>Determines whether the path is a directory.</summary>
        /// <param name="path">The path.</param>
        /// <returns><see cref="bool" />.</returns>
        public static bool IsDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }

            FileAttributes _fileAttributes = File.GetAttributes(path);
            return (_fileAttributes & FileAttributes.Directory) == FileAttributes.Directory;
        }

        /// <summary>Returns a <see cref="bool" /> indicating whether the file is locked.</summary>
        /// <param name="file">The file.</param>
        /// <returns><see cref="bool" />.</returns>
        public static bool IsFileLocked(FileInfo file)
        {
            FileStream _stream = null;

            try
            {
                _stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                // The file is unavailable:
                // - Being written to.
                // - Being processed by another thread.
                // - Does not exist (has already been processed).
                return true;
            }
            finally
            {
                _stream?.Close();
            }

            return false;
        }

        #endregion
    }
}