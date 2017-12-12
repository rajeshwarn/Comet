namespace Comet.Structure
{
    #region Namespace

    using System.IO;

    using Comet.Controls;
    using Comet.Managers;

    #endregion

    public class InstallerOptions
    {
        #region Properties

        public string InstallFilesFolder { get; set; }

        public string InstallFolder { get; set; }

        public string ProductName { get; set; }

        public string WorkingFolder { get; set; }

        #endregion

        #region Events

        public void Initialize()
        {
            string _subFolder = @"\" + ProductName + @"\Updater\";

            WorkingFolder = Path.GetTempPath() + _subFolder;

            Verify();
        }

        public void Verify()
        {
            FileManager.CreateDirectory(WorkingFolder);

            if (!Directory.Exists(InstallFilesFolder))
            {
                VisualExceptionDialog.Show(new DirectoryNotFoundException("The install files directory cannot be found. " + InstallFilesFolder));
            }

            if (!Directory.Exists(InstallFolder))
            {
                VisualExceptionDialog.Show(new DirectoryNotFoundException("The install files directory cannot be found. " + InstallFolder));
            }
        }

        #endregion
    }
}