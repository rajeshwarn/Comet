namespace Comet.UserControls
{
    #region Namespace

    using System;
    using System.Text;
    using System.Windows.Forms;

    using Comet.Structure;

    #endregion

    /// <summary>The change log.</summary>
    public partial class ChangeLog : UserControl
    {
        #region Constructors

        public ChangeLog(InstallOptions installOptions, Package package, Version currentVersion)
        {
            InitializeComponent();
            
            StringBuilder _versionInfo = new StringBuilder();
            _versionInfo.AppendLine(@"The version of " + package.Name + @" installed on this computer is: v" + currentVersion);
            _versionInfo.AppendLine(@"The latest version is: v" + package.Version);

            LVersion.Text = _versionInfo.ToString();
            TbChangeLog.Text = package.ChangeLog;
        }

        #endregion
    }
}