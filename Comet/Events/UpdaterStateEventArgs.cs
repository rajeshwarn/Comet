namespace Comet.Events
{
    #region Namespace

    using System;
    using System.Reflection;

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
        public UpdaterStateEventArgs(Assembly assembly, InstallOptions installOptions, string packagePath, UpdaterState state)
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
        public string PackagePath { get; }

        /// <summary>The update state.</summary>
        public UpdaterState State { get; }

        /// <summary>Gets the update available state.</summary>
        public bool UpdateAvailable { get; }

        /// <summary>Gets the assembly version.</summary>
        public Version Version { get; }

        #endregion
    }
}