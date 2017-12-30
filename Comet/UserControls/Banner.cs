namespace Comet.UserControls
{
    #region Namespace

    using System.ComponentModel;
    using System.Windows.Forms;

    #endregion

    /// <summary>The banner page.</summary>
    [ToolboxItem(false)]
    public partial class Banner : UserControl
    {
        #region Constructors

        public Banner()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        public void UpdateBanner(string header, string description)
        {
            LHeader.Text = header;
            LDescription.Text = description;
        }

        #endregion
    }
}