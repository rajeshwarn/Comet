namespace Comet.Events
{
    #region Namespace

    using System;
    using System.Reflection;
    using System.Text;

    using Comet.Enums;
    using Comet.Managers;
    using Comet.Structure;

    #endregion

    public class UpdaterStateEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="UpdaterStateEventArgs" /> class.</summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="installOptions">The install Options.</param>
        /// <param name="packagePath">The package path.</param>
        /// <param name="state">The update state.</param>
        public UpdaterStateEventArgs(Assembly assembly, InstallOptions installOptions, Uri packagePath, UpdaterState state)
        {
            Assembly = assembly;
            AssemblyLocation = assembly.Location;
            InstallOptions = installOptions;
            PackagePath = packagePath;
            Package = new Package(packagePath);
            State = state;
            UpdateAvailable = ApplicationManager.CheckForUpdate(assembly, packagePath);
            Version = assembly.GetName().Version;
        }

        #endregion

        #region Properties

        /// <summary>Gets the assembly.</summary>
        public Assembly Assembly { get; }

        /// <summary>Gets the assembly location.</summary>
        public string AssemblyLocation { get; }

        /// <summary>Gets the install options.</summary>
        public InstallOptions InstallOptions { get; }

        /// <summary>Gets the package.</summary>
        public Package Package { get; }

        /// <summary>The package uri source.</summary>
        public Uri PackagePath { get; }

        /// <summary>The update state.</summary>
        public UpdaterState State { get; }

        /// <summary>Gets the update available state.</summary>
        public bool UpdateAvailable { get; }

        /// <summary>Gets the assembly version.</summary>
        public Version Version { get; }

        #endregion

        #region Events

        /// <summary>
        ///     Override the ToString().
        /// </summary>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public override string ToString()
        {
            StringBuilder _updaterStateEventArgsString = new StringBuilder();
            _updaterStateEventArgsString.AppendLine("Assembly: " + Assembly);
            _updaterStateEventArgsString.AppendLine("Location: " + AssemblyLocation);
            _updaterStateEventArgsString.AppendLine("Package Path: " + PackagePath);
            _updaterStateEventArgsString.AppendLine("State: " + State);
            _updaterStateEventArgsString.AppendLine("Version: " + Version);
            _updaterStateEventArgsString.AppendLine("Update Available: " + UpdateAvailable);
            _updaterStateEventArgsString.AppendLine("Install options:");
            _updaterStateEventArgsString.AppendLine("Download Folder: " + InstallOptions.DownloadFolder);

            foreach (string _download in InstallOptions.DownloadedFiles)
            {
                _updaterStateEventArgsString.AppendLine("Download File: " + _download);
            }

            _updaterStateEventArgsString.AppendLine("Executable Path: " + InstallOptions.ExecutablePath);
            _updaterStateEventArgsString.AppendLine("Install Directory: " + InstallOptions.InstallDirectory);
            _updaterStateEventArgsString.AppendLine("Install Files Folder: " + InstallOptions.InstallFilesFolder);
            _updaterStateEventArgsString.AppendLine("Product Name: " + InstallOptions.ProductName);
            _updaterStateEventArgsString.AppendLine("Restart After Install: " + InstallOptions.RestartApplicationAfterInstall);
            _updaterStateEventArgsString.AppendLine("Working Folder: " + InstallOptions.WorkingFolder);
            _updaterStateEventArgsString.AppendLine("Package:");
            _updaterStateEventArgsString.AppendLine("Change Log: " + Package.ChangeLog);

            _updaterStateEventArgsString.AppendLine("Downloads: ");

            if (Package.Downloads != null)
            {
                foreach (Uri _download in Package.Downloads)
                {
                    _updaterStateEventArgsString.AppendLine("Link: " + _download.OriginalString);
                }
            }

            _updaterStateEventArgsString.AppendLine("Filename: " + Package.Filename);
            _updaterStateEventArgsString.AppendLine("Name: " + Package.Name);
            _updaterStateEventArgsString.AppendLine("Release: " + Package.Release);
            _updaterStateEventArgsString.AppendLine("Version: " + Package.Version);
            return _updaterStateEventArgsString.ToString();
        }

        #endregion
    }
}