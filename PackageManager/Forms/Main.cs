namespace PackageManager.Forms
{
    #region Namespace

    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Windows.Forms;
    using System.Xml.Linq;

    using Comet.Compiler;
    using Comet.Structure;

    #endregion

    /// <summary>The main.</summary>
    public partial class Main : Form
    {
        #region Constructors

        public Main()
        {
            InitializeComponent();

            Settings.ArchiveFileTypes = @"ZIP File|*.zip";
            Settings.PackageFileTypes = @"Package File|*.package";
            Settings.MaxRecentProjects = 10;
            Settings.InstallerPath = "Installer.exe";

            loadInstallerScriptToolStripMenuItem.PerformClick();
        }

        #endregion

        #region Events

        /// <summary>Add file for archive.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void BtnArchiveAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog
                {
                    Title = @"Browse for a file to add",
                    Filter = @"All Files|*.*"
                };

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Archive.CreateEntryFromFile(new Archive(Settings.ArchivePath), _openFileDialog.FileName, Path.GetFileName(_openFileDialog.FileName), CompressionLevel.Fastest);
                UpdateArchive(Settings.ArchivePath);
            }
        }

        /// <summary>Extract file from archive.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void BtnArchiveExtractFile_Click(object sender, EventArgs e)
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
                        Archive.ExtractToFile(new Archive(Settings.ArchivePath), _fileToExtract, _saveFileDialog.FileName);
                    }
                }
            }
        }

        /// <summary>Extract archive to directory.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void BtnArchiveExtractToDirectory_Click(object sender, EventArgs e)
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

                Archive.ExtractToDirectory(new Archive(Settings.ArchivePath), _folderBrowserDialog.SelectedPath);
                MessageBox.Show(@"The archive has been extracted!" + Environment.NewLine + @"Output: " + _folderBrowserDialog.SelectedPath);
            }
        }

        /// <summary>Browse file archive.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void BtnArchiveLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog
                {
                    Title = @"Browse for an archive.",
                    Filter = Settings.ArchiveFileTypes
                };

            if (_openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Settings.ArchivePath = _openFileDialog.FileName;
            UpdateArchive(_openFileDialog.FileName);
        }

        /// <summary>Create an archive.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void BtnArchiveNew_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog _saveFileDialog = new SaveFileDialog())
            {
                _saveFileDialog.Title = @"Save new archive";
                _saveFileDialog.Filter = Settings.ArchiveFileTypes;
                _saveFileDialog.FileName = string.Empty;

                if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Archive.CreateEmptyArchive(_saveFileDialog.FileName);
                    Settings.ArchivePath = _saveFileDialog.FileName;
                }
            }
        }

        /// <summary>Remove file from archive.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void BtnArchiveRemove_Click(object sender, EventArgs e)
        {
            if (lvArchive.SelectedItems.Count > 0)
            {
                string _fileToRemove = lvArchive.SelectedItems[0].Text;

                Archive.DeleteEntry(new Archive(Settings.ArchivePath), _fileToRemove);
                UpdateArchive(Settings.ArchivePath);
            }
        }

        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _references = new List<string> { "System.dll", "Comet.dll" };

            CompilerResults _results = CodeDomCompiler.Build(_references, tbSource.Text, Settings.InstallerPath);

            ProcessCompilerResults(_results);
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
            Settings.FileSaved = false;
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

        private void loadEntryPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeCompileUnit _mainEntryPointUnit = CompileUnits.CreateMainEntryPoint();
            tbSource.Text = CodeDomCompiler.GenerateSource(_mainEntryPointUnit);
        }

        /// <summary>The main load.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void Main_Load(object sender, EventArgs e)
        {
            newToolStripMenuItem.PerformClick();

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
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeToolStripMenuItem.Enabled = true;
            Settings.FileName = string.Empty;
            Settings.FullPath = string.Empty;
            Settings.FileSaved = false;

            const string ChangeLog = "Initial release";
            const string DownloadLink = "https://www.example.com/link/";
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
            Settings.FileSaved = false;
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
                    Settings.FileSaved = true;
                    Settings.FileName = Path.GetFileName(path);
                    Settings.FullPath = path;
                    Settings.ManageHistoryLog(true);

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
                _openFileDialog.Filter = Settings.PackageFileTypes;

                if (_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(_openFileDialog.FileName);
                    closeToolStripMenuItem.Enabled = true;
                }
            }
        }

        /// <summary>Populate the recent files history from the log.</summary>
        private void PopulateRecentFilesHistory()
        {
            // Check for recent file updates
            Settings.ManageHistoryLog();

            for (var i = 0; i < Settings.FileHistory.Count; i++)
            {
                // CreateInstallerCode sub-item
                ToolStripMenuItem _recentFileItem = new ToolStripMenuItem
                    {
                        Name = @"RecentFilesListToolStripMenuItem" + i,
                        Tag = Settings.FileHistory[i],
                        Text = i + @": " + Settings.FileHistory[i]
                    };

                _recentFileItem.Click += RecentHistoryItem_Click;
                recentHistoryToolStripMenuItem.DropDownItems.Add(_recentFileItem);
            }
        }

        /// <summary>Process the compiler results.</summary>
        /// <param name="compilerResults">The compiler results.</param>
        private void ProcessCompilerResults(CompilerResults compilerResults)
        {
            lvErrorList.Items.Clear();

            if (compilerResults.Errors.Count > 0)
            {
                foreach (CompilerError _compileError in compilerResults.Errors)
                {
                    ListViewItem _listViewItem = new ListViewItem(_compileError.ErrorNumber);

                    _listViewItem.SubItems.Add(_compileError.ErrorText);
                    _listViewItem.SubItems.Add(_compileError.FileName);
                    _listViewItem.SubItems.Add(_compileError.Line.ToString());
                    _listViewItem.SubItems.Add(_compileError.Column.ToString());

                    lvErrorList.Items.Add(_listViewItem);
                }
            }
            else
            {
                if (RunAfterBuildtoolStripMenuItem1.Checked)
                {
                    Process.Start(Settings.InstallerPath);
                }
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

        private void RunAfterBuildtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RunAfterBuildtoolStripMenuItem1.Checked = !RunAfterBuildtoolStripMenuItem1.Checked;
        }

        /// <summary>SaveAs Tool Strip Menu Item Click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog _saveFileDialog = new SaveFileDialog())
            {
                _saveFileDialog.Title = @"Save package";
                _saveFileDialog.Filter = Settings.PackageFileTypes;

                if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SavePackage(_saveFileDialog.FileName);

                    Settings.FullPath = _saveFileDialog.FileName;
                    Settings.FileName = Path.GetFileName(_saveFileDialog.FileName);
                    Settings.FileSaved = true;

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
            if (!Settings.FileSaved)
            {
                if (File.Exists(Settings.FullPath))
                {
                    SavePackage(Settings.FullPath);

                    ConstructRecentFilesMenuStrip();

                    Settings.FileSaved = true;
                }
                else
                {
                    saveAsToolStripMenuItem.PerformClick();
                }
            }
        }

        /// <summary>Text box data value changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void TbPackage_TextChanged(object sender, EventArgs e)
        {
            Settings.FileSaved = false;
            ToggleSaveOption();
        }

        /// <summary>Toggles the save menu option.</summary>
        private void ToggleSaveOption()
        {
            if (File.Exists(Settings.FullPath))
            {
                saveToolStripMenuItem.Enabled = true;
            }
            else
            {
                saveToolStripMenuItem.Enabled = false;
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

            btnArchiveAdd.Enabled = true;

            if (lvArchive.Items.Count > 0)
            {
                btnArchiveRemove.Enabled = true;
                btnArchiveExtractFile.Enabled = true;
                btnArchiveExtractToDirectory.Enabled = true;
            }
            else
            {
                btnArchiveRemove.Enabled = false;
                btnArchiveExtractFile.Enabled = false;
                btnArchiveExtractToDirectory.Enabled = false;
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

        private void loadInstallerScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeCompileUnit _mainEntryPointUnit = CompileUnits.CreateInstallerCode();
            tbSource.Text = CodeDomCompiler.GenerateSource(_mainEntryPointUnit);
        }
    }
}