﻿namespace Comet.Managers
{
    #region Namespace

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;

    using Comet.Structure;

    #endregion

    public class ResourcesManager
    {
        #region Events

        /// <summary>
        ///     Create the resource settings.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="installOptions">The install options.</param>
        public static void CreateSettingsResource(string filename, InstallOptions installOptions)
        {
            using (ResourceWriter _resourceWriter = new ResourceWriter(filename))
            {
                _resourceWriter.AddResource("Logging", false); // TODO: On true it won't display the contents like ExtractFolder in the console.
                _resourceWriter.AddResource("ProductName", installOptions.ProductName);
                _resourceWriter.AddResource("InstallFolder", installOptions.InstallPath);

                _resourceWriter.Generate();
                _resourceWriter.Close();
            }
        }

        /// <summary>Retrieve the resource names from the file.</summary>
        /// <param name="file">The file path.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static List<string> GetResourceNames(string file)
        {
            Assembly _assembly = ApplicationManager.LoadAssembly(file);
            return _assembly.GetManifestResourceNames().ToList();
        }

        /// <summary>
        ///     Read the resource from the file.
        /// </summary>
        /// <param name="file">The file path.</param>
        /// <param name="resource">The resource name.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string ReadResource(string file, string resource)
        {
            Assembly _assembly = ApplicationManager.LoadAssembly(file);
            string result;
            using (Stream stream = _assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        #endregion
    }
}