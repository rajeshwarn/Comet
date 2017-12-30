namespace Comet.Controls
{
    #region Namespace

    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    using Comet.Compiler;
    using Comet.Managers;
    using Comet.Properties;
    using Comet.Structure;
    using Comet.UserControls;

    #endregion

    /// <summary>The <see cref="VisualExceptionDialog" />.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Description("The Visual Exception Dialog")]
    [ToolboxBitmap(typeof(CometUpdater), "Resources.VisualExceptionDialog.bmp")]
    [ToolboxItem(false)]
    public class ProgressDialog : Form
    {
        #region Variables

        private Banner _banner;
        private Size _buttonSize;
        private int _buttonSpacing;
        private Panel _buttonsPanel;
        private Button _cancelButton0;
        private ChangeLogPanel _changeLogPanel;
        private Label _comet;
        private Panel _contentPanel;
        private Version _currentVersion;
        private DownloadPanel _downloadPanel;
        private Button _installButton;
        private InstallOptions _installOptions;
        private Button _nextButton;
        private Package _package;
        private Separator _separator;
        private TableLayoutPanel _tableLayoutPanel;
        private Button _updateButton;
        private UpdateMode _updateMode;
        private CometUpdater _updater;
        private WelcomePage _welcomePage;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="ProgressDialog" /> class.</summary>
        /// <param name="installOptions">The install Options.</param>
        /// <param name="package">The package.</param>
        /// <param name="currentVersion">The current Version.</param>
        /// <param name="updater">The updater.</param>
        public ProgressDialog(InstallOptions installOptions, Package package, Version currentVersion, CometUpdater updater)
        {
            MaximizeBox = false;
            MinimizeBox = true;
            ShowIcon = true;
            ShowInTaskbar = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Padding = new Padding(0);
            Size = new Size(530, 400);
            BackColor = SystemColors.Control;
            Text = $@"Update - {Application.ProductName}";
            Icon = Resources.Comet;
            StartPosition = FormStartPosition.CenterScreen;

            _buttonSize = new Size(75, 23);
            _currentVersion = currentVersion;
            _updateMode = UpdateMode.Welcome;
            _buttonSpacing = 7;
            _installOptions = installOptions;
            _package = package;
            _updater = updater;

            InitializeTableLayoutPanels();
            InitializeWelcomePage();

            InitializeButtons();

            UpdateDisplayMode(_updateMode);

            if (_updater.AutoUpdate)
            {
                _nextButton.PerformClick();
                _updateButton.PerformClick();

                // TODO: Install update
            }
        }

        public enum UpdateMode
        {
            /// <summary>
            ///     The welcome page.
            /// </summary>
            Welcome,

            /// <summary>The changes mode.</summary>
            Changes,

            /// <summary>The downloading mode.</summary>
            Download,

            /// <summary>The installing mode.</summary>
            Installing
        }

        #endregion

        #region Events

        /// <summary>The cancel button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Compile the installer.
        /// </summary>
        /// <param name="installOptions">The install options.</param>
        private void CompileInstaller(InstallOptions installOptions)
        {
            // Ask to close and restart to update files with installer
            var _references = new List<string>
                {
                    "System.dll",
                    "System.Windows.Forms.dll"
                };

            ResourcesManager.CreateSettingsResource(installOptions.ResourceSettingsPath, installOptions);

            var _resources = new List<string>
                {
                    installOptions.ResourceSettingsPath
                };

            var _sources = new[] { Resources.MainEntryPoint, Resources.ConsoleManager, Resources.Installer, Resources.ResourceSettings };

            string _updaterFileName = Application.StartupPath + @"\\Updater.exe";

            CompilerResults _results = CodeDomCompiler.Build(_references, _sources, _updaterFileName, _resources);

            if (_results.Errors.Count > 0)
            {
                VisualCompileErrorDialog.Show(_results);
            }
            else
            {
                Process.Start(_updaterFileName);
            }

            File.Delete(installOptions.ResourceSettingsPath);
        }

        /// <summary>
        ///     The file download completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void DownloadFileCompleted(object sender, EventArgs e)
        {
            _updater.NotificationUpdateReadyToInstall();

            if (_installButton.InvokeRequired)
            {
                _installButton.BeginInvoke((MethodInvoker)delegate
                    {
                        _installButton.Enabled = true;
                    });
            }
            else
            {
                _installButton.Enabled = true;
            }

            if (_updater.AutoUpdate)
            {
                InstallUpdate();
            }
        }

        /// <summary>Initializes the ok button.</summary>
        private void InitializeButtons()
        {
            _cancelButton0 = new Button
                {
                    BackColor = SystemColors.Control,
                    Text = @"Cancel",
                    Size = _buttonSize,
                    Location = new Point(_buttonsPanel.ClientSize.Width - _buttonSize.Width - _buttonsPanel.Padding.Right, _buttonsPanel.Padding.Top),
                    TabIndex = 1
                };

            _cancelButton0.Click += CancelButton_Click;

            _buttonsPanel.Controls.Add(_cancelButton0);

            _nextButton = new Button
                {
                    BackColor = SystemColors.Control,
                    Text = @"Next >",
                    Size = _buttonSize,
                    Location = new Point(_cancelButton0.Left - _buttonSize.Width - 5, _cancelButton0.Location.Y),
                    TabIndex = 0
                };

            _nextButton.Click += NextButton_Click;

            _buttonsPanel.Controls.Add(_nextButton);

            _installButton = new Button
                {
                    BackColor = SystemColors.Control,
                    Text = @"Install",
                    Size = _buttonSize,
                    Location = new Point(_cancelButton0.Left - _buttonSize.Width - 5, _cancelButton0.Location.Y),
                    TabIndex = 0,
                    Enabled = false
                };

            _installButton.Click += InstallButton_Click;

            _comet = new Label
                {
                    Location = new Point(0, _cancelButton0.Location.Y - 10),
                    Size = new Size(39, 20),
                    Text = @"Comet",
                    ForeColor = Color.DarkGray
                };

            // _buttonsPanel.Controls.Add(_comet);
            _separator = new Separator
                {
                    Orientation = Orientation.Horizontal,
                    Location = new Point(_comet.Right, _comet.Location.Y + 5),
                    Size = new Size(Width, 0),
                    Line = Color.FromArgb(224, 222, 220),
                    Shadow = Color.FromArgb(250, 249, 249),
                    ShadowVisible = true
                };

            // _buttonsPanel.Controls.Add(_separator);
        }

        /// <summary>
        ///     Initializes the table layout panels.
        /// </summary>
        private void InitializeTableLayoutPanels()
        {
            _tableLayoutPanel = new TableLayoutPanel
                {
                    BackColor = Color.White,
                    Dock = DockStyle.Fill,
                    ColumnCount = 0,
                    RowCount = 1,
                    Padding = new Padding(0)
                };

            _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 320F));

            Controls.Add(_tableLayoutPanel);

            _contentPanel = new Panel
                {
                    BackColor = Color.White,
                    Dock = DockStyle.Fill,
                    Padding = new Padding(0),
                    Margin = new Padding(0)
                };

            _tableLayoutPanel.Controls.Add(_contentPanel);

            _buttonsPanel = new Panel
                {
                    BackColor = SystemColors.Control,
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10),
                    Margin = new Padding(0)
                };

            _tableLayoutPanel.Controls.Add(_buttonsPanel);
        }

        /// <summary>
        ///     Initializes the welcome page.
        /// </summary>
        private void InitializeWelcomePage()
        {
            _welcomePage = new WelcomePage(_installOptions, _package, _currentVersion)
                {
                    Name = "WelcomePage",
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    Size = new Size(0, 0)
                };

            _contentPanel.Controls.Add(_welcomePage);
        }

        /// <summary>The update button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void InstallButton_Click(object sender, EventArgs e)
        {
            UpdateDisplayMode(UpdateMode.Installing);
        }

        /// <summary>
        ///     Install the update.
        /// </summary>
        private void InstallUpdate()
        {
            if (Directory.Exists(_installOptions.InstallFilesFolder))
            {
                Directory.Delete(_installOptions.InstallFilesFolder, true);
            }

            FileManager.CreateDirectory(_installOptions.InstallFilesFolder);

            foreach (string _archive in _installOptions.DownloadedFiles)
            {
                ZipFile.ExtractToDirectory(_archive, _installOptions.InstallFilesFolder);
            }

            CompileInstaller(_installOptions);
        }

        /// <summary>The update button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void NextButton_Click(object sender, EventArgs e)
        {
            UpdateDisplayMode(UpdateMode.Changes);
        }

        /// <summary>The update button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateDisplayMode(UpdateMode.Download);
        }

        /// <summary>
        ///     Update the display mode on the progress dialog.
        /// </summary>
        /// <param name="updateMode">The update mode.</param>
        private void UpdateDisplayMode(UpdateMode updateMode)
        {
            switch (updateMode)
            {
                case UpdateMode.Changes:
                    {
                        _banner = new Banner
                            {
                                Size = new Size(_contentPanel.Width, 42)
                            };

                        _changeLogPanel = new ChangeLogPanel(_installOptions, _package, _currentVersion)
                            {
                                Name = "ChangesPage",
                                Location = new Point(0, _banner.Size.Height),
                                Size = new Size(_contentPanel.Width, _contentPanel.Height - _banner.Size.Height)
                            };

                        _updateButton = new Button
                            {
                                BackColor = SystemColors.Control,
                                Text = @"Update",
                                Size = _buttonSize,
                                Location = new Point(_cancelButton0.Left - _buttonSize.Width - 5, _cancelButton0.Location.Y),
                                TabIndex = 0
                            };

                        _updateButton.Click += UpdateButton_Click;

                        _contentPanel.Controls.Add(_banner);
                        _banner.UpdateBanner("Update Information", $"Changes in the latest version of {_package.Name}.");

                        _contentPanel.Controls.Remove(_welcomePage);
                        _buttonsPanel.Controls.Remove(_nextButton);

                        _contentPanel.Controls.Add(_changeLogPanel);
                        _buttonsPanel.Controls.Add(_updateButton);
                        break;
                    }

                case UpdateMode.Download:
                    {
                        _banner.UpdateBanner("Downloading", $"The latest v.{_package.Version} of {_package.Name}.");

                        _contentPanel.Controls.Remove(_changeLogPanel);
                        _buttonsPanel.Controls.Remove(_updateButton);

                        _buttonsPanel.Controls.Add(_installButton);

                        _downloadPanel = new DownloadPanel(_installOptions, _package, _updater)
                            {
                                Location = new Point(0, _banner.Size.Height),
                                Size = new Size(_contentPanel.Width, _contentPanel.Height - _banner.Size.Height)
                            };

                        _contentPanel.Controls.Add(_downloadPanel);

                        _downloadPanel.DownloadManager.DownloadsCompleted += DownloadFileCompleted;
                        break;
                    }

                case UpdateMode.Installing:
                    {
                        _banner.UpdateBanner("Installing", $"Updating {_package.Name} to the v.{_package.Version}.");
                        _contentPanel.Controls.Remove(_downloadPanel);
                        _buttonsPanel.Controls.Remove(_installButton);
                        InstallUpdate();
                        Close();
                        break;
                    }

                case UpdateMode.Welcome:
                    {
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(updateMode), updateMode, null);
                    }
            }
        }

        #endregion
    }
}