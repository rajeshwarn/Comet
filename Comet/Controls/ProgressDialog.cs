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

        public Button _cancelButton;
        public Button _installButton;
        public Button _updateButton;

        #endregion

        #region Variables

        private string _bannerText;
        private string _bannerTitle;
        private ChangeLog _changeLogPanel;
        private Label _comet;

        private Version _currentVersion;
        private DownloadPanel _downloadPanel;

        private InstallOptions _installOptions;
        private Point _location = new Point(40, 60);
        private Package _package;
        private Separator _separator;
        private Size _size = new Size(400, 280);

        private UpdateMode _updateMode;
        private CometUpdater _updater;

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
            Padding = new Padding(10);
            Size = new Size(480, 420);
            BackColor = SystemColors.Control;
            Text = Application.ProductName + @" Update";
            Icon = Resources.Comet;
            StartPosition = FormStartPosition.CenterScreen;

            _currentVersion = currentVersion;
            _updateMode = UpdateMode.Changes;

            _installOptions = installOptions;
            _package = package;
            _updater = updater;

            InitializeButtons();

            _changeLogPanel = new ChangeLog(_installOptions, _package, _currentVersion)
                {
                    Location = _location,
                    Size = _size,
                    Name = "ChangeLogPanel"
                };

            Controls.Add(_changeLogPanel);
            UpdateDisplayMode(_updateMode);
        }

        public enum UpdateMode
        {
            /// <summary>The changes mode.</summary>
            Changes,

            /// <summary>The downloading mode.</summary>
            Download,

            /// <summary>The installing mode.</summary>
            Installing
        }

        #endregion

        #region Events

        /// <summary>
        ///     The on paint event.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(new SolidBrush(Color.LightSteelBlue), new Rectangle(0, 0, Width, 50));
            Banner.DrawBanner(e.Graphics, _bannerTitle, _bannerText, Padding);
        }

        /// <summary>The cancel button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void CancelButtonClick(object sender, EventArgs e)
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
        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
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
            var _buttonSpacing = 7;
            Size _buttonSize = new Size(75, 23);

            _cancelButton = new Button
                {
                    BackColor = SystemColors.Control,
                    Text = @"Cancel",
                    Size = _buttonSize,
                    Location = new Point(Padding.Right + 356, 350),
                    TabIndex = 1
                };

            _cancelButton.Click += CancelButtonClick;

            Controls.Add(_cancelButton);

            _updateButton = new Button
                {
                    BackColor = SystemColors.Control,
                    Text = @"Update",
                    Size = _buttonSize,
                    Location = new Point(_cancelButton.Left - _buttonSize.Width - 5, _cancelButton.Location.Y),
                    TabIndex = 0
                };

            _updateButton.Click += UpdateButtonClick;

            Controls.Add(_updateButton);

            _installButton = new Button
                {
                    BackColor = SystemColors.Control,
                    Text = @"Install",
                    Size = _buttonSize,
                    Location = new Point(_cancelButton.Left - _buttonSize.Width - 5, _cancelButton.Location.Y),
                    TabIndex = 0,
                    Enabled = false
                };

            _installButton.Click += InstallButtonClick;

            _comet = new Label
                {
                    Location = new Point(0, _cancelButton.Location.Y - 10),
                    Size = new Size(39, 20),
                    Text = @"Comet",
                    ForeColor = Color.DarkGray
                };

            Controls.Add(_comet);

            _separator = new Separator
                {
                    Orientation = Orientation.Horizontal,
                    Location = new Point(_comet.Right, _comet.Location.Y + 5),
                    Size = new Size(Width, 0),
                    Line = Color.FromArgb(224, 222, 220),
                    Shadow = Color.FromArgb(250, 249, 249),
                    ShadowVisible = true
                };

            Controls.Add(_separator);
        }

        /// <summary>The update button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void InstallButtonClick(object sender, EventArgs e)
        {
            UpdateDisplayMode(UpdateMode.Installing);
        }

        /// <summary>
        ///     Install the update.
        /// </summary>
        private void InstallUpdate()
        {
            FileManager.CreateDirectory(_installOptions.InstallFilesFolder);
            Archive.ExtractToDirectory(new Archive(_installOptions.DownloadedFile), _installOptions.InstallFilesFolder);
            CompileInstaller(_installOptions);
        }

        /// <summary>
        ///     Updates the banner.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        private void UpdateBanner(string title, string text)
        {
            _bannerTitle = title;
            _bannerText = text;
            Invalidate();
        }

        /// <summary>The update button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void UpdateButtonClick(object sender, EventArgs e)
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
                        UpdateBanner("Update Information", $"Changes in the latest version of {_package.Name}.");
                        break;
                    }

                case UpdateMode.Download:
                    {
                        UpdateBanner("Downloading", $"The latest v.{_package.Version} of {_package.Name}.");

                        Controls.Remove(_changeLogPanel);
                        Controls.Remove(_updateButton);
                        Controls.Add(_installButton);

                        _downloadPanel = new DownloadPanel(_installOptions, _package, _updater)
                            {
                                Location = _location,
                                Size = _size
                            };
                        Controls.Add(_downloadPanel);

                        _downloadPanel.Downloader._client.DownloadFileCompleted += DownloadFileCompleted;
                        break;
                    }

                case UpdateMode.Installing:
                    {
                        UpdateBanner("Installing", $"Updating {_package.Name} to the v.{_package.Version}.");
                        Controls.Remove(_downloadPanel);
                        Controls.Remove(_installButton);
                        InstallUpdate();
                        Close();
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