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

            CbDisplayWelcomePage.Checked = ControlPanel.SettingsManager.Settings.DisplayWelcomePage;
            CbAutoUpdate.Checked = ControlPanel.SettingsManager.Settings.AutoUpdate;
            CbNotifyBeforeInstallingUpdates.Checked = ControlPanel.SettingsManager.Settings.NotifyUpdateReadyToInstall;
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

        private void UpdateSettings()
        {
            UpdaterSettings _settings = new UpdaterSettings
            {
                    AutoUpdate = CbAutoUpdate.Checked,
                    DisplayWelcomePage = CbDisplayWelcomePage.Checked,
                    NotifyUpdateReadyToInstall = CbNotifyBeforeInstallingUpdates.Checked
                };

            ControlPanel.SettingsManager.Settings = _settings;
        }

        #endregion
    }
}