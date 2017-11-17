namespace Builder.Forms
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml.Linq;

    using Comet.Managers;
    using Comet.Structure;

    #endregion

    public partial class Main : Form
    {
        #region Constructors

        public Main()
        {
            InitializeComponent();
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
                CompressionManager.AddFile(Settings.ArchivePath, _openFileDialog.FileName);
                UpdateArchive(Settings.ArchivePath);
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
                    CompressionManager.CreateArchive(_saveFileDialog.FileName);
                    Settings.ArchivePath = _saveFileDialog.FileName;
                }
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
                        CompressionManager.ExtractToFile(Settings.ArchivePath, _fileToExtract, _saveFileDialog.FileName);
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

                CompressionManager.ExtractToDirectory(Settings.ArchivePath, _folderBrowserDialog.SelectedPath);
                MessageBox.Show(@"The archive has been extracted!" + Environment.NewLine + @"Output: " + _folderBrowserDialog.SelectedPath);
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
                CompressionManager.RemoveFile(Settings.ArchivePath, _fileToRemove);
                UpdateArchive(Settings.ArchivePath);
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
            DateTime _dateTime = DateTime.Today;
            Version _version = new Version(0, 0, 0, 0);
            Package _newPackage = new Package(_changeLog, _downloadLink, _fileName, _productName, _dateTime.ToString(CultureInfo.CurrentCulture), _version);
            UpdatePackage(_newPackage);
            closeToolStripMenuItem.Enabled = false;
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
            int _major = Convert.ToInt32(nudMajor.Value);
            int _minor = Convert.ToInt32(nudMinor.Value);
            int _build = Convert.ToInt32(nudBuild.Value);
            int _revision = Convert.ToInt32(nudRevision.Value);

            return new Version(_major, _minor, _build, _revision);
        }

        /// <summary>The main load.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void Main_Load(object sender, EventArgs e)
        {
            Settings.ArchiveFileTypes = @"ZIP File|*.zip";
            Settings.PackageFileTypes = @"Package File|*.package";
            
            newToolStripMenuItem.PerformClick();

            lvArchive.Columns[0].Width = 100;
            lvArchive.Columns[1].Width = 100;
            lvArchive.Columns[2].Width = 100;
            lvArchive.Columns[3].Width = 100;
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
                    // string _line = _streamReader.ReadToEnd();
                    Settings.FileSaved = true;
                    Settings.FileName = Path.GetFileName(path);
                    Settings.FullPath = path;
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

            List<ArchiveData> _archiveList = CompressionManager.Open(path);

            foreach (ArchiveData _entry in _archiveList)
            {
                ListViewItem _listViewItem = new ListViewItem
                    {
                        Text = _entry.FullName
                    };

                _listViewItem.SubItems.Add(_entry.Size.ToString());
                _listViewItem.SubItems.Add(_entry.Type);
                _listViewItem.SubItems.Add(_entry.LastWriteTime.ToString());

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
    }
}