namespace PackageManager.Forms
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml.Linq;

    using Comet;
    using Comet.Events;
    using Comet.Managers;
    using Comet.Structure;

    using PackageManager.Managers;
    using PackageManager.UserControls;

    #endregion

    /// <summary>The main.</summary>
    public partial class Main : Form
    {
        #region Variables

        private DownloadSites _downloadSites;
        private HistoryManager _historyManager;
        private CometUpdater _updater;

        #endregion

        #region Constructors

        public Main()
        {
            InitializeComponent();

            ControlPanel.DefaultSettings = new Logger.LogSettings
                {
                    FilePath = @"Logs\Log.xml",
                    WriteMode = Logger.WriteMode.XML
                };

            ControlPanel.SettingsManager = new SettingsManager(@"Logs\Settings.xml");
            ControlPanel.SettingsManager.Load();

            ControlPanel.FileHistoryLocation = @"Logs\History.xml";
            ControlPanel.PackageFileTypes = @"Package File|*.package";
            ControlPanel.MaxRecentProjects = 10;
            ControlPanel.InstallerPath = "Installer.exe";

            Logger.Log(ControlPanel.DefaultSettings, $"Started {Application.ProductName}");

            ControlPanel.UpdatePackageUrl = @"https://raw.githubusercontent.com/DarkByte7/Comet/master/PackageManager/Update.package";

            _updater = new CometUpdater(new Uri(ControlPanel.UpdatePackageUrl), Assembly.GetExecutingAssembly().Location, ControlPanel.SettingsManager.Settings.AutoUpdate, ControlPanel.SettingsManager.Settings.DisplayWelcomePage);

            _updater.CheckingForUpdate += CometUpdater_CheckingForUpdate;
            _updater.CheckForUpdate();

            string _source = ResourcesManager.ReadResource(Application.StartupPath + @"\Comet.dll", "Comet.Setup.MainEntryPoint.cs");
            tbSource.Text = _source;

            InitializeDownloadSitesTabPage();
        }

        #endregion

        #region Events

        /// <summary>The about tool strip menu item.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About _aboutForm = new About
                {
                    StartPosition = FormStartPosition.CenterParent
                };

            _aboutForm.ShowDialog();
        }

        /// <summary>The about tool strip menu item.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void CheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _updater.CheckForUpdate();
            _updater.DisplayWelcomePage = ControlPanel.SettingsManager.Settings.DisplayWelcomePage;

            if (_updater.UpdateAvailable)
            {
                _updater.ShowProgressDialog();
            }
        }

        /// <summary>Close Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string _changeLog = string.Empty;
            string _downloadLink = string.Empty;
            string _fileName = string.Empty;
            string _productName = string.Empty;
            tabControlMain.TabPages[1].Text = string.Empty;
            DateTime _dateTime = new DateTime(2000, 1, 1);
            Version _version = new Version(0, 0, 0, 0);
            Package _newPackage = new Package(_changeLog, _fileName, _productName, _dateTime.ToString(CultureInfo.CurrentCulture), _version, new List<Uri>());
            UpdatePackage(_newPackage);
            closeToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        ///     The updater state changed.
        /// </summary>
        /// <param name="e">The event args.</param>
        private void CometUpdater_CheckingForUpdate(UpdaterStateEventArgs e)
        {
            string _updateStatus = e.ToString();

            if (TbUpdaterDetails.InvokeRequired)
            {
                TbUpdaterDetails.BeginInvoke((MethodInvoker)delegate
                    {
                        TbUpdaterDetails.Text = _updateStatus;
                    });
            }
            else
            {
                TbUpdaterDetails.Text = _updateStatus;
            }
        }

        /// <summary>DateTime release value changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void DateTimePickerRelease_ValueChanged(object sender, EventArgs e)
        {
            ControlPanel.FileSaved = false;
            ToggleSaveOption();
        }

        /// <summary>Exit Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>Get the package version.</summary>
        /// <returns>
        ///     <see cref="Version" />
        /// </returns>
        private Version GetPackageVersion()
        {
            // TODO: input UpDownControl/s
            int _major = Convert.ToInt32(nudMajor.Value);
            int _minor = Convert.ToInt32(nudMinor.Value);
            int _build = Convert.ToInt32(nudBuild.Value);
            int _revision = Convert.ToInt32(nudRevision.Value);

            return new Version(_major, _minor, _build, _revision);
        }

        /// <summary>
        ///     Initializes the download sites tab page.
        /// </summary>
        private void InitializeDownloadSitesTabPage()
        {
            TabPage _downloadSitesTabPage = new TabPage("Download Sites");

            _downloadSites = new DownloadSites
                {
                    BackColor = Color.White,
                    Dock = DockStyle.Fill
                };

            _downloadSitesTabPage.Controls.Add(_downloadSites);
            tabControlCreator.TabPages.Add(_downloadSitesTabPage);
        }

        /// <summary>The main load.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void Main_Load(object sender, EventArgs e)
        {
            NewToolStripMenuItem.PerformClick();

            lvErrorList.Columns[0].Width = 50;
            lvErrorList.Columns[1].Width = 230;
            lvErrorList.Columns[2].Width = 150;
            lvErrorList.Columns[3].Width = 50;
            lvErrorList.Columns[4].Width = 50;

            _historyManager = new HistoryManager(recentHistoryToolStripMenuItem, ControlPanel.FileHistoryLocation);
            _historyManager.ToolStripMenuItemClicked += RecentHistory_Click;
        }

        /// <summary>New Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeToolStripMenuItem.Enabled = true;
            ControlPanel.FileName = "Untitled.package";
            ControlPanel.FullPath = string.Empty;
            ControlPanel.FileSaved = false;
            tabControlMain.TabPages[1].Text = ControlPanel.FileName;

            const string ChangeLog = "Initial release";

            // const string DownloadLink = "www.example.com/";
            const string FileName = "Filename.exe";
            const string PackageProductName = "ProductName";
            DateTime _dateTime = DateTime.Today;
            Version _version = new Version(1, 0, 0, 0);

            UpdatePackage(new Package(ChangeLog, FileName, PackageProductName, _dateTime.ToString(CultureInfo.CurrentCulture), _version, new List<Uri>()));
        }

        /// <summary>Package version value changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void NudPackage_ValueChanged(object sender, EventArgs e)
        {
            ControlPanel.FileSaved = false;
            ToggleSaveOption();
        }

        /// <summary>Open the project file.</summary>
        /// <param name="path">The file path.</param>
        private void OpenFile(string path)
        {
            UpdatePackage(new Package(path));

            try
            {
                using (StreamReader _streamReader = new StreamReader(path))
                {
                    string _line = _streamReader.ReadToEnd();
                    ControlPanel.FileSaved = true;
                    ControlPanel.FileName = Path.GetFileName(path);
                    ControlPanel.FullPath = path;

                    _historyManager.Add(path);
                    _historyManager.UpdateMenu();
                    _historyManager.Save();

                    tabControlMain.TabPages[1].Text = ControlPanel.FileName;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Open Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog _openFileDialog = new OpenFileDialog())
            {
                _openFileDialog.Title = @"Open package...";
                _openFileDialog.Filter = ControlPanel.PackageFileTypes;

                if (_openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                OpenFile(_openFileDialog.FileName);
                closeToolStripMenuItem.Enabled = true;
            }
        }

        /// <summary>
        ///     Occurs when the options form is closed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OptionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // TODO: Reload settings.
            // _updater.AutoUpdate = ControlPanel.SettingsManager.Settings.AutoUpdate;
            // _updater.DisplayWelcomePage = ControlPanel.SettingsManager.Settings.DisplayWelcomePage;
            // _updater.NotifyUpdateReadyToInstall = ControlPanel.SettingsManager.Settings.NotifyBeforeInstallUpdates;
        }

        /// <summary>
        ///     Displays the options form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options _optionsForm = new Options();
            _optionsForm.FormClosed += OptionsForm_FormClosed;
            _optionsForm.ShowDialog();
        }

        /// <summary>
        ///     The tool strip menu item click event for a recent history entry.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void RecentHistory_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItemClicked = (ToolStripMenuItem)sender;
            string _clickedFilePath = _toolStripMenuItemClicked.Tag.ToString();
            OpenFile(_clickedFilePath);
        }

        /// <summary>Report A Problem Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ReportAProblemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/DarkByte7/Comet/issues");
        }

        /// <summary>SaveAs Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog _saveFileDialog = new SaveFileDialog())
            {
                _saveFileDialog.Title = @"Save package";
                _saveFileDialog.Filter = ControlPanel.PackageFileTypes;
                _saveFileDialog.FileName = ControlPanel.FileName;

                if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SavePackage(_saveFileDialog.FileName);

                    ControlPanel.FullPath = _saveFileDialog.FileName;
                    ControlPanel.FileName = Path.GetFileName(_saveFileDialog.FileName);
                    ControlPanel.FileSaved = true;
                    tabControlMain.TabPages[1].Text = ControlPanel.FileName;
                    saveToolStripMenuItem.Enabled = false;
                }
            }
        }

        /// <summary>Saves the package to a file.</summary>
        /// <param name="path">The output path.</param>
        private void SavePackage(string path)
        {
            Package _package = new Package
                {
                    ChangeLog = tbPackageChangeLog.Text,
                    Downloads = _downloadSites.DownloadsList,
                    Filename = tbPackageFilename.Text,
                    Name = tbPackageName.Text,
                    Release = dtpRelease.Value,
                    Version = GetPackageVersion()
                };

            _package.Save(path, SaveOptions.None);
        }

        /// <summary>Save Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ControlPanel.FileSaved)
            {
                if (File.Exists(ControlPanel.FullPath))
                {
                    SavePackage(ControlPanel.FullPath);
                    _historyManager.UpdateMenu();
                    ControlPanel.FileSaved = true;
                }
                else
                {
                    saveAsToolStripMenuItem.PerformClick();
                }
            }
        }

        /// <summary>
        ///     The status bar tool strip menu item clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusbarToolStripMenuItem.Checked)
            {
                statusbarToolStripMenuItem.Checked = false;
                statusStripMain.Visible = false;
            }
            else
            {
                statusbarToolStripMenuItem.Checked = true;
                statusStripMain.Visible = true;
            }
        }

        /// <summary>Text box data value changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void TbPackage_TextChanged(object sender, EventArgs e)
        {
            ControlPanel.FileSaved = false;
            ToggleSaveOption();
        }

        /// <summary>Toggles the save menu option.</summary>
        private void ToggleSaveOption()
        {
            if (File.Exists(ControlPanel.FullPath))
            {
                saveToolStripMenuItem.Enabled = true;
            }
            else
            {
                saveToolStripMenuItem.Enabled = false;
            }
        }

        /// <summary>Update the package data fields.</summary>
        /// <param name="package">The package data.</param>
        private void UpdatePackage(Package package)
        {
            DateTime _dateTime = Convert.ToDateTime(package.Release);

            tbPackageChangeLog.Text = package.ChangeLog;

            var _downloadsList = package.Downloads;

            _downloadSites.ImportPackageDownloads(_downloadsList);

            tbPackageFilename.Text = package.Filename;
            tbPackageName.Text = package.Name;
            dtpRelease.Value = package.Release;
            UpdateVersion(package.Version);
        }

        /// <summary>Update package data version.</summary>
        /// <param name="version">The version.</param>
        private void UpdateVersion(Version version)
        {
            nudMajor.Value = Convert.ToDecimal(version.Major);
            nudMinor.Value = Convert.ToDecimal(version.Minor);
            nudBuild.Value = Convert.ToDecimal(version.Build);
            nudRevision.Value = Convert.ToDecimal(version.Revision);
        }

        #endregion
    }
}