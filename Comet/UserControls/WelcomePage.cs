namespace Comet.UserControls
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Forms;

    using Comet.Structure;

    #endregion

    /// <summary>The welcome page.</summary>
    [ToolboxItem(false)]
    public partial class WelcomePage : UserControl
    {
        #region Constructors

        public WelcomePage(InstallOptions installOptions, Package package, Version currentVersion)
        {
            InitializeComponent();

            LTitle.Text = $@"Welcome to the {package.Name} Update Wizard";

            StringBuilder _headerText = new StringBuilder();
            _headerText.AppendLine($@"This wizard will guide you through the update of {package.Name}.");
            _headerText.Append(Environment.NewLine);
            _headerText.AppendLine($@"It is recommended that you close all other applications before continuing.");
            _headerText.Append(Environment.NewLine);
            _headerText.AppendLine($@"Click Next to continue, or Cancel to exit the update.");
            LHeader.Text = _headerText.ToString();
        }

        #endregion
    }
}