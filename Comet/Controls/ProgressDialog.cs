namespace Comet.Controls
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    using Comet.Enums;
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

        private Button _cancelButton;
        private ChangeLog _changeLog;
        private Label _comet;
        private Download _downData;
        private Package _package;
        private Separator _separator;
        private Button _updateButton;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="ProgressDialog" /> class.</summary>
        /// <param name="installOptions">The install Options.</param>
        /// <param name="package">The package.</param>
        /// <param name="currentVersion">The current Version.</param>
        public ProgressDialog(InstallOptions installOptions, Package package, Version currentVersion)
        {
            MaximizeBox = false;
            MinimizeBox = true;
            ShowIcon = true;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Padding = new Padding(10);
            Size = new Size(480, 420);
            BackColor = SystemColors.Control;
            Text = @"Comet";
            Icon = Resources.Comet;

            _package = package;

            InitializeButtons();

            _changeLog = new ChangeLog(installOptions, package, currentVersion);
            _changeLog.Location = new Point(40, 60);
            _changeLog.Size = new Size(400, 280);
            Controls.Add(_changeLog);
        }

        #endregion

        #region Events

        public void UpdateDisplayMode(UpdaterState state)
        {
            switch (state)
            {
                case UpdaterState.NotChecked:
                    break;
                case UpdaterState.Checking:
                    break;
                case UpdaterState.Updated:
                    break;
                case UpdaterState.Outdated:
                    break;
                case UpdaterState.Downloading:
                    Controls.Remove(_changeLog);

                    _downData = new Download
                        {
                            Location = new Point(40, 60),
                            Size = new Size(400, 280)
                        };

                    Controls.Add(_downData);
                    break;
                case UpdaterState.NoConnection:
                    break;
                case UpdaterState.PackageNotFound:
                    break;
                case UpdaterState.PackageDataNotFound:
                    break;
                case UpdaterState.Downloaded:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        /// <summary>
        ///     The on paint event.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Banner.DrawBanner(e.Graphics, "Update Information", "Changes in the latest version of " + _package.Name, Padding);
        }

        /// <summary>The cancel button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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
        private void UpdateButtonClick(object sender, EventArgs e)
        {
            _changeLog.Visible = false;
            UpdateDisplayMode(UpdaterState.Downloading);
            DialogResult = DialogResult.OK;
        }

        #endregion
    }
}