namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows.Forms;

    #endregion

    /// <summary>The <see cref="ApplicationManager" />.</summary>
    public class ApplicationManager
    {
        #region Events

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
            return AssemblyName.GetAssemblyName(fileName).Version;
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