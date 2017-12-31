namespace PackageManager.Forms
{
    #region Namespace

    using System;
    using System.Windows.Forms;

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
            CbNotifyBeforeInstallingUpdates.Checked = ControlPanel.SettingsManager.Settings.NotifyBeforeInstallUpdates;
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
            Settings _settings = new Settings
                {
                    AutoUpdate = CbAutoUpdate.Checked,
                    DisplayWelcomePage = CbDisplayWelcomePage.Checked,
                    NotifyBeforeInstallUpdates = CbNotifyBeforeInstallingUpdates.Checked
                };

            ControlPanel.SettingsManager.Settings = _settings;
        }

        #endregion
    }
}