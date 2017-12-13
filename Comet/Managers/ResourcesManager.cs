namespace Comet.Managers
{
    #region Namespace

    using System.Resources;

    #endregion

    public class ResourcesManager
    {
        #region Events

        public static void CreateSettingsResource(string filename)
        {
            using (ResourceWriter _resourceWriter = new ResourceWriter(filename))
            {
                _resourceWriter.AddResource("Logging", false); // TODO: On true it won't display the contents like ExtractFolder in the console.
                _resourceWriter.AddResource("ProductName", "AppName");
                _resourceWriter.AddResource("InstallFolder", "%AppInstallationFolder%");

                _resourceWriter.Generate();
                _resourceWriter.Close();
            }
        }

        #endregion
    }
}