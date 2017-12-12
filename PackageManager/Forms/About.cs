namespace PackageManager.Forms
{
    #region Namespace

    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    #endregion

    /// <summary>The about form.</summary>
    public partial class About : Form
    {
        #region Constructors

        public About()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>The about form load.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void About_Load(object sender, EventArgs e)
        {
        }

        /// <summary>Close button.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>Web page button.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void BtnWebPage_Click(object sender, EventArgs e)
        {
            Process.Start("https://darkbyte7.github.io/Comet/");
        }

        #endregion
    }
}