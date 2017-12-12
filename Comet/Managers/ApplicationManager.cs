namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    using Comet.Structure;

    #endregion

    /// <summary>The <see cref="ApplicationManager" />.</summary>
    public class ApplicationManager
    {
        #region Events

        /// <summary>Checks for update.</summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="url">The url to the package.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool CheckForUpdate(Assembly assembly, string url)
        {
            if (!File.Exists(assembly.Location))
            {
                throw new FileNotFoundException("The executable file doesn't exist.");
            }

            return CompareVersion(assembly.Location, new Package(url));
        }

        /// <summary>Checks for update.</summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="url">The url to the package.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool CheckForUpdate(string assemblyPath, string url)
        {
            Assembly _assembly = Assembly.LoadFile(assemblyPath);
            return CheckForUpdate(_assembly, url);
        }

        /// <summary>Compares the source version with the comparison.</summary>
        /// <param name="file">The file.</param>
        /// <param name="package">The package.</param>
        /// <returns>The <see cref="bool" />.</returns>
        /// <exception cref="FileNotFoundException">The file doesn't exist.</exception>
        /// <exception cref="NoNullAllowedException">The package cannot be empty or null.</exception>
        public static bool CompareVersion(string file, Package package)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("The file was not found.");
            }

            if (package == null)
            {
                throw new NoNullAllowedException("The package cannot be empty or null.");
            }

            Version _fileVersion = AssemblyName.GetAssemblyName(file).Version;
            return CompareVersion(_fileVersion, package.Version);
        }

        /// <summary>Compares the source version with the comparison.</summary>
        /// <param name="source">The source.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool CompareVersion(Version source, Version comparison)
        {
            if (source.CompareTo(comparison) > 0)
            {
                return false;
            }

            return source.CompareTo(comparison) < 0;
        }

        /// <summary>Compares the file version with the comparison file.</summary>
        /// <param name="sourceFile">The source File.</param>
        /// <param name="comparisonFile">The comparison File.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool CompareVersion(string sourceFile, string comparisonFile)
        {
            return CompareVersion(AssemblyName.GetAssemblyName(sourceFile).Version, AssemblyName.GetAssemblyName(sourceFile).Version);
        }

        /// <summary>Gets the file version.</summary>
        /// <param name="fileName">The file to retrieve the version from.</param>
        /// <returns>The <see cref="Version" />.</returns>
        public static Version GetFileVersion(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(StringManager.FileNotFound(fileName));
            }

            Version _version = null;

            try
            {
                _version = AssemblyName.GetAssemblyName(fileName).Version;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return _version;
        }

        /// <summary>Get the main module file name.</summary>
        /// <returns><see cref="string" />.</returns>
        public static string GetMainModuleFileName()
        {
            return Process.GetCurrentProcess().MainModule.FileName;
        }

        /// <summary>Restarts the application.</summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="createNoWindow">Create no window.</param>
        /// <param name="fileName">The filename.</param>
        /// <param name="processWindowStyle">The process window style.</param>
        public static void Restart(string arguments, bool createNoWindow, string fileName, ProcessWindowStyle processWindowStyle)
        {
            // processWindowStyle = ProcessWindowStyle.Hidden;
            // fileName = "cmd.exe";
            // createNoWindow = true;
            // arguments = "/C ping 127.0.0.1 -n 2 && \"" + Application.ExecutablePath + "\"";
            ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    Arguments = arguments,
                    WindowStyle = processWindowStyle,
                    CreateNoWindow = createNoWindow,
                    FileName = fileName
                };

            Process.Start(processStartInfo);
            Application.Exit();
        }

        #endregion
    }
}