namespace Comet.Controls
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

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

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="ProgressDialog" /> class.</summary>
        public ProgressDialog()
        {
            MaximizeBox = false;
            MinimizeBox = true;
            ShowIcon = false;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Padding = new Padding(10);
            Size = new Size(440, 410);
            BackColor = SystemColors.Control;
            Text = @"Comet";
        }

        #endregion

        #region Events

        /// <summary>The OK button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void CancelButtonClick(object sender, EventArgs e)
        {
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
                    Location = new Point(0, 0),
                    TabIndex = 0
                };

            _cancelButton.Click += CancelButtonClick;

            Controls.Add(_cancelButton);
        }

        #endregion
    }
}