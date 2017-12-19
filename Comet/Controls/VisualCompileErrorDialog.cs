namespace Comet.Controls
{
    #region Namespace

    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    #endregion

    /// <summary>The <see cref="VisualCompileErrorDialog" />.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Description("The Visual Error Dialog")]
    [ToolboxBitmap(typeof(CometUpdater), "Resources.VisualErrorDialog.bmp")]
    [ToolboxItem(false)]
    public class VisualCompileErrorDialog : Form
    {
        #region Variables

        private CompilerResults _compilerResults;
        private Button _copyButton;
        private Label _errorLabel;
        private int _imageSpacing = 10;
        private Button _okButton;
        private PictureBox _pictureBoxImage;
        private Button _saveButton;
        private TextBox _textBoxError;
        private int _textheight = 16;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="VisualCompileErrorDialog" /> class.</summary>
        /// <param name="compilerResults">The compiler Results.</param>
        public VisualCompileErrorDialog(CompilerResults compilerResults)
        {
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Padding = new Padding(10);
            Size = new Size(440, 410);
            BackColor = Color.White;
            Text = @"Comet Compile Error Results";

            _compilerResults = compilerResults;

            var _defaultWidth = 360;

            InitializePictureBoxImage();
            InitializeCompileErrorResults(_defaultWidth);
            InitializeButtons();
        }

        #endregion

        #region Events

        /// <summary>Show the exception dialog.</summary>
        /// <param name="compilerResults">The compiler results.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="dialogWindow">The dialog Window.</param>
        public static void Show(CompilerResults compilerResults, string caption = "Compile Error Dialog", bool dialogWindow = true)
        {
            VisualCompileErrorDialog compileErrorDialog = new VisualCompileErrorDialog(compilerResults)
                {
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };

            if (dialogWindow)
            {
                compileErrorDialog.ShowDialog();
            }
            else
            {
                compileErrorDialog.Show();
            }
        }

        /// <summary>Copy the log to the clipboard.</summary>
        public void CopyLogToClipboard()
        {
            Clipboard.SetText(CreateLog());
        }

        /// <summary>Create a log entry.</summary>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public string CreateLog()
        {
            StringBuilder _errorLog = new StringBuilder();
            foreach (CompilerError _compileError in _compilerResults.Errors)
            {
                _errorLog.AppendLine("Error Code: " + _compileError.ErrorNumber);
                _errorLog.Append(Environment.NewLine);

                _errorLog.AppendLine("Line: " + _compileError.Line + ", Column: " + _compileError.Column);
                _errorLog.Append(Environment.NewLine);

                _errorLog.AppendLine("Error Text:");
                _errorLog.AppendLine(_compileError.ErrorText);
                _errorLog.Append(Environment.NewLine);

                _errorLog.AppendLine("Filename:");
                _errorLog.Append(_compileError.FileName);
                _errorLog.Append(Environment.NewLine);
            }

            return _errorLog.ToString();
        }

        /// <summary>Saves the log to a file.</summary>
        /// <param name="filePath">The file Path.</param>
        public void SaveLog(string filePath)
        {
            File.WriteAllText(filePath, CreateLog());
        }

        /// <summary>The Copy button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void CopyButton_Click(object sender, EventArgs e)
        {
            CopyLogToClipboard();
        }

        /// <summary>Initializes the ok button.</summary>
        private void InitializeButtons()
        {
            var _buttonSpacing = 7;
            Size _buttonSize = new Size(75, 23);

            _okButton = new Button
                {
                    BackColor = SystemColors.Control,
                    Text = @"OK",
                    Size = _buttonSize,
                    Location = new Point(_textBoxError.Right - _buttonSize.Width, _textBoxError.Bottom + 10),
                    TabIndex = 0
                };

            _okButton.Click += OkButton_Click;

            Controls.Add(_okButton);

            _saveButton = new Button
                {
                    BackColor = SystemColors.Control,
                    Text = @"Save",
                    Size = _buttonSize,
                    Location = new Point(_okButton.Left - _buttonSpacing - _buttonSize.Width, _textBoxError.Bottom + 10),
                    TabIndex = 1
                };

            _saveButton.Click += SaveButton_Click;

            Controls.Add(_saveButton);

            _copyButton = new Button
                {
                    BackColor = SystemColors.Control,
                    Text = @"Copy",
                    Size = _buttonSize,
                    Location = new Point(_saveButton.Left - _buttonSpacing - _buttonSize.Width, _textBoxError.Bottom + 10),
                    TabIndex = 2
                };

            _copyButton.Click += CopyButton_Click;

            Controls.Add(_copyButton);
        }

        /// <summary>Initializes the text box stack trace.</summary>
        /// <param name="width">The width.</param>
        private void InitializeCompileErrorResults(int width)
        {
            _errorLabel = new Label
                {
                    Text = $@"Compile Error/s: {_compilerResults.Errors.Count}",
                    Location = new Point(_pictureBoxImage.Right + _imageSpacing, Padding.Top),
                    Size = new Size(width, _textheight),
                    BorderStyle = BorderStyle.None
                };

            Controls.Add(_errorLabel);

            _textBoxError = new TextBox
                {
                    BackColor = BackColor,
                    Text = CreateLog(),
                    ReadOnly = true,
                    Multiline = true,
                    Location = new Point(_errorLabel.Location.X, _errorLabel.Bottom),
                    Size = new Size(width, 300),
                    ScrollBars = ScrollBars.Both
                };

            Controls.Add(_textBoxError);
        }

        /// <summary>Initializes the picture box image.</summary>
        private void InitializePictureBoxImage()
        {
            _pictureBoxImage = new PictureBox
                {
                    Image = SystemIcons.Error.ToBitmap(),
                    Location = new Point(Padding.Left, Padding.Top),
                    Size = new Size(32, 32),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

            Controls.Add(_pictureBoxImage);
        }

        /// <summary>The OK button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>The Copy button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog _saveFileDialog = new SaveFileDialog
                {
                    Title = @"Save exception log...",
                    Filter = @"Text Files|*.log;*.txt|All Files|*.*"
                };

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveLog(_saveFileDialog.FileName);
            }
        }

        #endregion
    }
}