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
                _resourceWriter.AddResource("Logging", false); // TODO: On true it won't display the contents like download link in the console.
                _resourceWriter.AddResource("DownloadLink", "https://raw.githubusercontent.com/DarkByte7/Comet/master/Comet/Update.package");
                _resourceWriter.AddResource("ExtractFolder", "DATA");

                _resourceWriter.Generate();
                _resourceWriter.Close();
            }
        }

        #endregion
    }
}