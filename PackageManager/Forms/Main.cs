namespace PackageManager.Forms
{
    #region Namespace

    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml.Linq;

    using Comet;
    using Comet.Events;
    using Comet.Managers;
    using Comet.Structure;

    using PackageManager.UserControls;

    #endregion

    /// <summary>The main.</summary>
    public partial class Main : Form
    {
        #region Constructors

        public Main()
        {
            InitializeComponent();

            ControlPanel.ArchiveFileTypes = @"ZIP File|*.zip";
            ControlPanel.PackageFileTypes = @"Package File|*.package";
            ControlPanel.MaxRecentProjects = 10;
            ControlPanel.InstallerPath = "Installer.exe";

            ControlPanel.WriteLog($"Started {Application.ProductName}");

            CbUrlScheme.SelectedIndex = 0;

            ControlPanel.UpdatePackageUrl = @"https://raw.githubusercontent.com/DarkByte7/Comet/master/PackageManager/Update.package";

            _updater = new CometUpdater(ControlPanel.UpdatePackageUrl, Assembly.GetExecutingAssembly().Location)
                {
                    AutoUpdate = false
                };

            _updater.CheckingForUpdate += CometUpdater_CheckingForUpdate;

            // cometUpdater1.CheckForUpdate();
            string _asm = Application.StartupPath + @"\Comet.dll";

            string _source = ResourcesManager.ReadResource(_asm, "Comet.Installer.MainEntryPoint.cs");
            tbSource.Text = _source;

            TabPage _downloadSitesTabPage = new TabPage("Download Sites");

            DownloadSites _downloadSites = new DownloadSites
                {
                    BackColor = Color.White,
                    Dock = DockStyle.Fill
                };

            _downloadSitesTabPage.Controls.Add(_downloadSites);

            tabControlCreator.TabPages.Add(_downloadSitesTabPage);
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

        /// <summary>Add files to archive Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void AddFilesToArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog
                {
                    Title = @"Browse for a file to add",
                    Filter = @"All Files|*.*",
                    Multiselect = true
                };

            if (_openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (string file in _openFileDialog.FileNames)
            {
                Archive.CreateEntryFromFile(new Archive(ControlPanel.ArchivePath), file, Path.GetFileName(file), CompressionLevel.Fastest);
            }

            UpdateArchive(ControlPanel.ArchivePath);
        }

        /// <summary>The about tool strip menu item.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void CheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _updater.CheckForUpdate();
        }

        /// <summary>Clears the recent files from history.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ClearRecentFilesHistory_Click(object sender, EventArgs e)
        {
            recentHistoryToolStripMenuItem.DropDownItems.Clear();
            ConstructClearRecentFilesMenuItem();
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
            DateTime _dateTime = new DateTime(2000, 1, 1);
            Version _version = new Version(0, 0, 0, 0);
            Package _newPackage = new Package(_changeLog, _downloadLink, _fileName, _productName, _dateTime.ToString(CultureInfo.CurrentCulture), _version);
            UpdatePackage(_newPackage);
            closeToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        ///     The updater state changed.
        /// </summary>
        /// <param name="e">The event args.</param>
        private void CometUpdater_CheckingForUpdate(UpdaterStateEventArgs e)
        {
            string _updateStatus = @"Update status: " + _updater.State;

            if (LUpdateStats.InvokeRequired)
            {
                LUpdateStats.BeginInvoke((MethodInvoker)delegate
                    {
                        LUpdateStats.Text = _updateStatus;
                    });
            }
            else
            {
                LUpdateStats.Text = _updateStatus;
            }

            string _s = e.ToString();

            if (label1.InvokeRequired)
            {
                label1.BeginInvoke((MethodInvoker)delegate
                    {
                        label1.Text = _s;
                    });
            }
            else
            {
                label1.Text = _s;
            }
        }

        /// <summary>CreateInstallerCode clear recent files menu item.</summary>
        private void ConstructClearRecentFilesMenuItem()
        {
            ToolStripMenuItem _clearRecentFilesHistory = new ToolStripMenuItem
                {
                    Name = "ClearRecentFilesHistoryMenuItem",
                    Text = @"Empty Recent Files List"
                };

            _clearRecentFilesHistory.Click += ClearRecentFilesHistory_Click;
            recentHistoryToolStripMenuItem.DropDownItems.Add(_clearRecentFilesHistory);

            ToolStripSeparator _toolStripSeparator = new ToolStripSeparator();
            recentHistoryToolStripMenuItem.DropDownItems.Add(_toolStripSeparator);
        }

        /// <summary>Constructs the recent files menu strip.</summary>
        private void ConstructRecentFilesMenuStrip()
        {
            recentHistoryToolStripMenuItem.DropDownItems.Clear();
            ConstructClearRecentFilesMenuItem();
            PopulateRecentFilesHistory();
        }

        /// <summary>DateTime release value changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void DateTimePickerRelease_ValueChanged(object sender, EventArgs e)
        {
            ControlPanel.FileSaved = false;
            ToggleSaveOption();
        }

        /// <summary>Delete file Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void DeleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvArchive.SelectedItems.Count > 0)
            {
                string _fileToRemove = lvArchive.SelectedItems[0].Text;

                Archive.DeleteEntry(new Archive(ControlPanel.ArchivePath), _fileToRemove);
                UpdateArchive(ControlPanel.ArchivePath);
            }
        }

        /// <summary>Exit Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>Extract File Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ExtractFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvArchive.SelectedItems.Count > 0)
            {
                string _fileToExtract = lvArchive.SelectedItems[0].Text;

                using (SaveFileDialog _saveFileDialog = new SaveFileDialog())
                {
                    _saveFileDialog.Title = @"Save extracted file";
                    _saveFileDialog.Filter = @"All Files|*.*";
                    _saveFileDialog.FileName = Path.GetFileName(_fileToExtract);

                    if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Archive.ExtractToFile(new Archive(ControlPanel.ArchivePath), _fileToExtract, _saveFileDialog.FileName);
                    }
                }
            }
        }

        /// <summary>Extract To Specified Folder Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void ExtractToTheSpecifiedFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvArchive.Items.Count > 0)
            {
                FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog
                    {
                        ShowNewFolderButton = true
                    };

                if (_folderBrowserDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                Archive.ExtractToDirectory(new Archive(ControlPanel.ArchivePath), _folderBrowserDialog.SelectedPath);
                MessageBox.Show(@"The archive has been extracted!" + Environment.NewLine + @"Output: " + _folderBrowserDialog.SelectedPath);
            }
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

        /// <summary>Load archive Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void LoadArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog
                {
                    Title = @"Browse for an archive.",
                    Filter = ControlPanel.ArchiveFileTypes
                };

            if (_openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ControlPanel.ArchivePath = _openFileDialog.FileName;
            UpdateArchive(_openFileDialog.FileName);
        }

        /// <summary>The main load.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void Main_Load(object sender, EventArgs e)
        {
            NewToolStripMenuItem.PerformClick();

            lvArchive.Columns[0].Width = 230;
            lvArchive.Columns[1].Width = 100;
            lvArchive.Columns[2].Width = 100;
            lvArchive.Columns[3].Width = 100;

            lvErrorList.Columns[0].Width = 50;
            lvErrorList.Columns[1].Width = 230;
            lvErrorList.Columns[2].Width = 150;
            lvErrorList.Columns[3].Width = 50;
            lvErrorList.Columns[4].Width = 50;

            ConstructRecentFilesMenuStrip();
        }

        /// <summary>New Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void NewArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog _saveFileDialog = new SaveFileDialog())
            {
                _saveFileDialog.Title = @"Save new archive";
                _saveFileDialog.Filter = ControlPanel.ArchiveFileTypes;
                _saveFileDialog.FileName = string.Empty;

                if (_saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                Archive.CreateEmptyArchive(_saveFileDialog.FileName);
                ControlPanel.ArchivePath = _saveFileDialog.FileName;

                UpdateArchive(ControlPanel.ArchivePath);
            }
        }

        /// <summary>New Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeToolStripMenuItem.Enabled = true;
            ControlPanel.FileName = string.Empty;
            ControlPanel.FullPath = string.Empty;
            ControlPanel.FileSaved = false;

            const string ChangeLog = "Initial release";
            const string DownloadLink = "www.example.com/";
            const string FileName = "Filename.exe";
            const string PackageProductName = "ProductName";
            DateTime _dateTime = DateTime.Today;
            Version _version = new Version(1, 0, 0, 0);

            UpdatePackage(new Package(ChangeLog, DownloadLink, FileName, PackageProductName, _dateTime.ToString(CultureInfo.CurrentCulture), _version));
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
                    ControlPanel.ManageHistoryLog(true);

                    ConstructRecentFilesMenuStrip();
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

        /// <summary>Populate the recent files history from the log.</summary>
        private void PopulateRecentFilesHistory()
        {
            // Check for recent file updates
            ControlPanel.ManageHistoryLog();

            for (var i = 0; i < ControlPanel.FileHistory.Count; i++)
            {
                // CreateInstallerCode sub-item
                ToolStripMenuItem _recentFileItem = new ToolStripMenuItem
                    {
                        Name = @"RecentFilesListToolStripMenuItem" + i,
                        Tag = ControlPanel.FileHistory[i],
                        Text = i + @": " + ControlPanel.FileHistory[i]
                    };

                _recentFileItem.Click += RecentHistoryItem_Click;
                recentHistoryToolStripMenuItem.DropDownItems.Add(_recentFileItem);
            }
        }

        /// <summary>Recent History Item Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void RecentHistoryItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _recentHistoryItemClicked = (ToolStripMenuItem)sender;
            OpenFile(_recentHistoryItemClicked.Text);
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

                if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SavePackage(_saveFileDialog.FileName);

                    ControlPanel.FullPath = _saveFileDialog.FileName;
                    ControlPanel.FileName = Path.GetFileName(_saveFileDialog.FileName);
                    ControlPanel.FileSaved = true;

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
                    Download = tbPackageDownloadLink.Text,
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

                    ConstructRecentFilesMenuStrip();

                    ControlPanel.FileSaved = true;
                }
                else
                {
                    saveAsToolStripMenuItem.PerformClick();
                }
            }
        }

        /// <summary>Select all Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvArchive.Items)
            {
                item.Selected = true;
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

        /// <summary>
        ///     Tick Update
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void TUpdate_Tick(object sender, EventArgs e)
        {
            if (tbPackageDownloadLink.TextLength >= 1)
            {
                string _source = CbUrlScheme.Text + tbPackageDownloadLink.Text;

                if (NetworkManager.IsURLFormatted(_source))
                {
                    if (NetworkManager.SourceExists(_source))
                    {
                        PbDownloadLinkConnection.Visible = false;
                    }
                    else
                    {
                        PbDownloadLinkConnection.Visible = true;
                    }
                }
                else
                {
                    PbDownloadLinkConnection.Visible = true;
                }
            }
        }

        /// <summary>Update the archive file/s list.</summary>
        /// <param name="path">The archive file path.</param>
        private void UpdateArchive(string path)
        {
            lvArchive.Items.Clear();

            Archive _archive = new Archive(path);
            _archive.Read();

            for (var i = 0; i < _archive.Count; i++)
            {
                ListViewItem _listViewItem = new ListViewItem
                    {
                        Text = _archive[i].FullName
                    };

                Bytes _size = new Bytes(_archive[i].Length);

                string _extension = Path.GetExtension(_archive[i].FullName);
                if (string.IsNullOrEmpty(_extension))
                {
                    _extension = "Folder";
                }

                _listViewItem.SubItems.Add(_size.ToString());
                _listViewItem.SubItems.Add(_extension);

                _listViewItem.SubItems.Add(_archive[i].LastWriteTime.ToString());

                lvArchive.Items.Add(_listViewItem);
            }

            addFilesToArchiveToolStripMenuItem.Enabled = true;

            if (lvArchive.Items.Count > 0)
            {
                deleteFileToolStripMenuItem.Enabled = true;
                extractFileToolStripMenuItem.Enabled = true;
                extractToTheSpecifiedFolderToolStripMenuItem.Enabled = true;
                selectAllToolStripMenuItem.Enabled = true;
            }
            else
            {
                deleteFileToolStripMenuItem.Enabled = false;
                extractFileToolStripMenuItem.Enabled = false;
                extractToTheSpecifiedFolderToolStripMenuItem.Enabled = false;
                selectAllToolStripMenuItem.Enabled = false;
            }
        }

        /// <summary>Update the package data fields.</summary>
        /// <param name="package">The package data.</param>
        private void UpdatePackage(Package package)
        {
            DateTime _dateTime = Convert.ToDateTime(package.Release);
            UpdatePackage(package.ChangeLog, package.Download, package.Filename, package.Name, _dateTime, package.Version);
        }

        /// <summary>Updates the package data fields.</summary>
        /// <param name="changeLog">The change log.</param>
        /// <param name="downloadLink">The download link.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="name">The name.</param>
        /// <param name="release">The release date.</param>
        /// <param name="version">The version.</param>
        private void UpdatePackage(string changeLog, string downloadLink, string fileName, string name, DateTime release, Version version)
        {
            tbPackageChangeLog.Text = changeLog;
            tbPackageDownloadLink.Text = downloadLink;
            tbPackageFilename.Text = fileName;
            tbPackageName.Text = name;
            dtpRelease.Value = release;
            UpdateVersion(version);
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