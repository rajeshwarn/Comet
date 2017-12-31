namespace PackageManager.Forms
{
    #region Namespace

    using System;
    using System.Windows.Forms;

    using Comet.Structure;

    using PackageManager.Structure;

    #endregion

    /// <summary>The options form.</summary>
    public partial class Options : Form
    {
        #region Constructors

        public Options()
        {
            InitializeComponent();
            NudMaximumRecentProjects.Value = ControlPanel.SettingsManager.ApplicationSettings.MaxRecentProjects;
            CbDisplayWelcomePage.Checked = ControlPanel.SettingsManager.UpdaterSettings.DisplayWelcomePage;
            CbAutoUpdate.Checked = ControlPanel.SettingsManager.UpdaterSettings.AutoUpdate;
            CbNotifyBeforeInstallingUpdates.Checked = ControlPanel.SettingsManager.UpdaterSettings.NotifyUpdateReadyToInstall;
        }

        #endregion

        #region Events

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            UpdateSettings();
            ControlPanel.SettingsManager.Save();
            Close();
        }

        /// <summary>
        ///     Updates the settings manager with the latest options.
        /// </summary>
        private void UpdateSettings()
        {
            ApplicationSettings _applicationSettings = new ApplicationSettings
                {
                    MaxRecentProjects = Convert.ToInt32(NudMaximumRecentProjects.Value)
                };

            UpdaterSettings _updaterSettings = new UpdaterSettings
                {
                    AutoUpdate = CbAutoUpdate.Checked,
                    DisplayWelcomePage = CbDisplayWelcomePage.Checked,
                    NotifyUpdateReadyToInstall = CbNotifyBeforeInstallingUpdates.Checked
                };

            ControlPanel.SettingsManager.ApplicationSettings = _applicationSettings;
            ControlPanel.SettingsManager.UpdaterSettings = _updaterSettings;
        }

        #endregion
    }
}