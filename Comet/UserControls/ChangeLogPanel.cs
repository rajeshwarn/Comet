﻿namespace Comet.UserControls
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Forms;

    using Comet.Structure;

    #endregion

    /// <summary>The change log.</summary>
    [ToolboxItem(false)]
    public partial class ChangeLogPanel : UserControl
    {
        #region Constructors

        public ChangeLogPanel(InstallOptions installOptions, Package package, Version currentVersion)
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