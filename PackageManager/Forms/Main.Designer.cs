﻿namespace PackageManager.Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.lvArchive = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsArchive = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.addFilesToArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.extractFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToTheSpecifiedFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbSource = new System.Windows.Forms.TextBox();
            this.lvErrorList = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nudRevision = new System.Windows.Forms.NumericUpDown();
            this.nudBuild = new System.Windows.Forms.NumericUpDown();
            this.nudMinor = new System.Windows.Forms.NumericUpDown();
            this.nudMajor = new System.Windows.Forms.NumericUpDown();
            this.dtpRelease = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPackageName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPackageFilename = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPackageDownloadLink = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPackageChangeLog = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.recentHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.RunAfterBuildtoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.loadEntryPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadInstallerScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportAProblemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.CbUrlScheme = new System.Windows.Forms.ComboBox();
            this.PbDownloadLinkConnection = new System.Windows.Forms.PictureBox();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LUpdateStats = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.TUpdate = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cometUpdater1 = new Comet.CometUpdater(this.components);
            this.cmsArchive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRevision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuild)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMajor)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbDownloadLinkConnection)).BeginInit();
            this.tabPage9.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvArchive
            // 
            this.lvArchive.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvArchive.ContextMenuStrip = this.cmsArchive;
            this.lvArchive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvArchive.FullRowSelect = true;
            this.lvArchive.GridLines = true;
            this.lvArchive.Location = new System.Drawing.Point(3, 3);
            this.lvArchive.Name = "lvArchive";
            this.lvArchive.Size = new System.Drawing.Size(586, 407);
            this.lvArchive.TabIndex = 4;
            this.lvArchive.UseCompatibleStateImageBehavior = false;
            this.lvArchive.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Modified";
            // 
            // cmsArchive
            // 
            this.cmsArchive.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newArchiveToolStripMenuItem,
            this.loadArchiveToolStripMenuItem,
            this.toolStripSeparator12,
            this.addFilesToArchiveToolStripMenuItem,
            this.toolStripSeparator9,
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator11,
            this.extractFileToolStripMenuItem,
            this.extractToTheSpecifiedFolderToolStripMenuItem,
            this.toolStripSeparator10,
            this.deleteFileToolStripMenuItem});
            this.cmsArchive.Name = "cmsArchive";
            this.cmsArchive.Size = new System.Drawing.Size(228, 182);
            // 
            // newArchiveToolStripMenuItem
            // 
            this.newArchiveToolStripMenuItem.Name = "newArchiveToolStripMenuItem";
            this.newArchiveToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.newArchiveToolStripMenuItem.Text = "New archive...";
            this.newArchiveToolStripMenuItem.Click += new System.EventHandler(this.NewArchiveToolStripMenuItem_Click);
            // 
            // loadArchiveToolStripMenuItem
            // 
            this.loadArchiveToolStripMenuItem.Name = "loadArchiveToolStripMenuItem";
            this.loadArchiveToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.loadArchiveToolStripMenuItem.Text = "Load archive...";
            this.loadArchiveToolStripMenuItem.Click += new System.EventHandler(this.LoadArchiveToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(224, 6);
            // 
            // addFilesToArchiveToolStripMenuItem
            // 
            this.addFilesToArchiveToolStripMenuItem.Enabled = false;
            this.addFilesToArchiveToolStripMenuItem.Name = "addFilesToArchiveToolStripMenuItem";
            this.addFilesToArchiveToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.addFilesToArchiveToolStripMenuItem.Text = "Add files to archive";
            this.addFilesToArchiveToolStripMenuItem.Click += new System.EventHandler(this.AddFilesToArchiveToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(224, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Enabled = false;
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.selectAllToolStripMenuItem.Text = "Select all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(224, 6);
            // 
            // extractFileToolStripMenuItem
            // 
            this.extractFileToolStripMenuItem.Enabled = false;
            this.extractFileToolStripMenuItem.Name = "extractFileToolStripMenuItem";
            this.extractFileToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.extractFileToolStripMenuItem.Text = "Extract file";
            this.extractFileToolStripMenuItem.Click += new System.EventHandler(this.ExtractFileToolStripMenuItem_Click);
            // 
            // extractToTheSpecifiedFolderToolStripMenuItem
            // 
            this.extractToTheSpecifiedFolderToolStripMenuItem.Enabled = false;
            this.extractToTheSpecifiedFolderToolStripMenuItem.Name = "extractToTheSpecifiedFolderToolStripMenuItem";
            this.extractToTheSpecifiedFolderToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.extractToTheSpecifiedFolderToolStripMenuItem.Text = "Extract to the specified folder";
            this.extractToTheSpecifiedFolderToolStripMenuItem.Click += new System.EventHandler(this.ExtractToTheSpecifiedFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(224, 6);
            // 
            // deleteFileToolStripMenuItem
            // 
            this.deleteFileToolStripMenuItem.Enabled = false;
            this.deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            this.deleteFileToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.deleteFileToolStripMenuItem.Text = "Delete file";
            this.deleteFileToolStripMenuItem.Click += new System.EventHandler(this.DeleteFileToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbSource);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvErrorList);
            this.splitContainer1.Size = new System.Drawing.Size(586, 407);
            this.splitContainer1.SplitterDistance = 286;
            this.splitContainer1.TabIndex = 6;
            // 
            // tbSource
            // 
            this.tbSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSource.Location = new System.Drawing.Point(0, 0);
            this.tbSource.Multiline = true;
            this.tbSource.Name = "tbSource";
            this.tbSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbSource.Size = new System.Drawing.Size(586, 286);
            this.tbSource.TabIndex = 0;
            this.tbSource.WordWrap = false;
            // 
            // lvErrorList
            // 
            this.lvErrorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lvErrorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvErrorList.FullRowSelect = true;
            this.lvErrorList.GridLines = true;
            this.lvErrorList.Location = new System.Drawing.Point(0, 0);
            this.lvErrorList.Name = "lvErrorList";
            this.lvErrorList.Size = new System.Drawing.Size(586, 117);
            this.lvErrorList.TabIndex = 5;
            this.lvErrorList.UseCompatibleStateImageBehavior = false;
            this.lvErrorList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Code";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Description";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "File";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Line";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Column";
            // 
            // nudRevision
            // 
            this.nudRevision.Location = new System.Drawing.Point(274, 148);
            this.nudRevision.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudRevision.Name = "nudRevision";
            this.nudRevision.Size = new System.Drawing.Size(53, 20);
            this.nudRevision.TabIndex = 22;
            this.nudRevision.ValueChanged += new System.EventHandler(this.NudPackage_ValueChanged);
            // 
            // nudBuild
            // 
            this.nudBuild.Location = new System.Drawing.Point(215, 148);
            this.nudBuild.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudBuild.Name = "nudBuild";
            this.nudBuild.Size = new System.Drawing.Size(53, 20);
            this.nudBuild.TabIndex = 21;
            this.nudBuild.ValueChanged += new System.EventHandler(this.NudPackage_ValueChanged);
            // 
            // nudMinor
            // 
            this.nudMinor.Location = new System.Drawing.Point(156, 148);
            this.nudMinor.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMinor.Name = "nudMinor";
            this.nudMinor.Size = new System.Drawing.Size(53, 20);
            this.nudMinor.TabIndex = 20;
            this.nudMinor.ValueChanged += new System.EventHandler(this.NudPackage_ValueChanged);
            // 
            // nudMajor
            // 
            this.nudMajor.Location = new System.Drawing.Point(97, 147);
            this.nudMajor.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMajor.Name = "nudMajor";
            this.nudMajor.Size = new System.Drawing.Size(53, 20);
            this.nudMajor.TabIndex = 19;
            this.nudMajor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMajor.ValueChanged += new System.EventHandler(this.NudPackage_ValueChanged);
            // 
            // dtpRelease
            // 
            this.dtpRelease.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpRelease.Location = new System.Drawing.Point(97, 121);
            this.dtpRelease.Name = "dtpRelease";
            this.dtpRelease.Size = new System.Drawing.Size(454, 20);
            this.dtpRelease.TabIndex = 18;
            this.dtpRelease.ValueChanged += new System.EventHandler(this.DateTimePickerRelease_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Version:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Release:";
            // 
            // tbPackageName
            // 
            this.tbPackageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPackageName.Location = new System.Drawing.Point(97, 95);
            this.tbPackageName.Name = "tbPackageName";
            this.tbPackageName.Size = new System.Drawing.Size(456, 20);
            this.tbPackageName.TabIndex = 13;
            this.tbPackageName.TextChanged += new System.EventHandler(this.TbPackage_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Name:";
            // 
            // tbPackageFilename
            // 
            this.tbPackageFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPackageFilename.Location = new System.Drawing.Point(97, 69);
            this.tbPackageFilename.Name = "tbPackageFilename";
            this.tbPackageFilename.Size = new System.Drawing.Size(456, 20);
            this.tbPackageFilename.TabIndex = 11;
            this.tbPackageFilename.TextChanged += new System.EventHandler(this.TbPackage_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Filename:";
            // 
            // tbPackageDownloadLink
            // 
            this.tbPackageDownloadLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPackageDownloadLink.Location = new System.Drawing.Point(165, 43);
            this.tbPackageDownloadLink.Name = "tbPackageDownloadLink";
            this.tbPackageDownloadLink.Size = new System.Drawing.Size(388, 20);
            this.tbPackageDownloadLink.TabIndex = 9;
            this.tbPackageDownloadLink.TextChanged += new System.EventHandler(this.TbPackage_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Download Link:";
            // 
            // tbPackageChangeLog
            // 
            this.tbPackageChangeLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPackageChangeLog.Location = new System.Drawing.Point(97, 17);
            this.tbPackageChangeLog.Name = "tbPackageChangeLog";
            this.tbPackageChangeLog.Size = new System.Drawing.Size(456, 20);
            this.tbPackageChangeLog.TabIndex = 7;
            this.tbPackageChangeLog.TextChanged += new System.EventHandler(this.TbPackage_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Change Log:";
            // 
            // statusStripMain
            // 
            this.statusStripMain.Location = new System.Drawing.Point(0, 495);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(614, 22);
            this.statusStripMain.TabIndex = 4;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.toolStripSeparator1,
            this.openToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator6,
            this.closeToolStripMenuItem,
            this.toolStripSeparator3,
            this.recentHistoryToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.NewToolStripMenuItem.Text = "New";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(183, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(183, 6);
            // 
            // recentHistoryToolStripMenuItem
            // 
            this.recentHistoryToolStripMenuItem.Name = "recentHistoryToolStripMenuItem";
            this.recentHistoryToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.recentHistoryToolStripMenuItem.Text = "Recent History";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(183, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildToolStripMenuItem,
            this.toolStripSeparator5,
            this.RunAfterBuildtoolStripMenuItem1,
            this.toolStripSeparator8,
            this.loadEntryPointToolStripMenuItem,
            this.loadInstallerScriptToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.buildToolStripMenuItem.Text = "Build";
            this.buildToolStripMenuItem.Click += new System.EventHandler(this.BuildToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(174, 6);
            // 
            // RunAfterBuildtoolStripMenuItem1
            // 
            this.RunAfterBuildtoolStripMenuItem1.Checked = true;
            this.RunAfterBuildtoolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RunAfterBuildtoolStripMenuItem1.Name = "RunAfterBuildtoolStripMenuItem1";
            this.RunAfterBuildtoolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
            this.RunAfterBuildtoolStripMenuItem1.Text = "Run after Build";
            this.RunAfterBuildtoolStripMenuItem1.Click += new System.EventHandler(this.RunAfterBuildToolStripMenuItem1_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(174, 6);
            // 
            // loadEntryPointToolStripMenuItem
            // 
            this.loadEntryPointToolStripMenuItem.Name = "loadEntryPointToolStripMenuItem";
            this.loadEntryPointToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.loadEntryPointToolStripMenuItem.Text = "Load Entry Point";
            this.loadEntryPointToolStripMenuItem.Click += new System.EventHandler(this.LoadEntryPointToolStripMenuItem_Click);
            // 
            // loadInstallerScriptToolStripMenuItem
            // 
            this.loadInstallerScriptToolStripMenuItem.Name = "loadInstallerScriptToolStripMenuItem";
            this.loadInstallerScriptToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.loadInstallerScriptToolStripMenuItem.Text = "Load Installer Script";
            this.loadInstallerScriptToolStripMenuItem.Click += new System.EventHandler(this.LoadInstallerScriptToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportAProblemToolStripMenuItem,
            this.toolStripSeparator7,
            this.checkForUpdatesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // reportAProblemToolStripMenuItem
            // 
            this.reportAProblemToolStripMenuItem.Name = "reportAProblemToolStripMenuItem";
            this.reportAProblemToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.reportAProblemToolStripMenuItem.Text = "Report a problem...";
            this.reportAProblemToolStripMenuItem.Click += new System.EventHandler(this.ReportAProblemToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(172, 6);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdatesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Controls.Add(this.tabPage10);
            this.tabControl2.Controls.Add(this.tabPage11);
            this.tabControl2.Controls.Add(this.tabPage12);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(600, 439);
            this.tabControl2.TabIndex = 6;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(592, 413);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "General";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(592, 413);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Style";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(592, 413);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "Language";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.CbUrlScheme);
            this.tabPage8.Controls.Add(this.PbDownloadLinkConnection);
            this.tabPage8.Controls.Add(this.nudRevision);
            this.tabPage8.Controls.Add(this.label3);
            this.tabPage8.Controls.Add(this.nudBuild);
            this.tabPage8.Controls.Add(this.tbPackageChangeLog);
            this.tabPage8.Controls.Add(this.nudMinor);
            this.tabPage8.Controls.Add(this.label4);
            this.tabPage8.Controls.Add(this.nudMajor);
            this.tabPage8.Controls.Add(this.tbPackageDownloadLink);
            this.tabPage8.Controls.Add(this.dtpRelease);
            this.tabPage8.Controls.Add(this.label5);
            this.tabPage8.Controls.Add(this.label8);
            this.tabPage8.Controls.Add(this.tbPackageFilename);
            this.tabPage8.Controls.Add(this.label7);
            this.tabPage8.Controls.Add(this.label6);
            this.tabPage8.Controls.Add(this.tbPackageName);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(592, 413);
            this.tabPage8.TabIndex = 3;
            this.tabPage8.Text = "Update Information";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // CbUrlScheme
            // 
            this.CbUrlScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbUrlScheme.FormattingEnabled = true;
            this.CbUrlScheme.Items.AddRange(new object[] {
            "http://",
            "https://"});
            this.CbUrlScheme.Location = new System.Drawing.Point(97, 42);
            this.CbUrlScheme.Name = "CbUrlScheme";
            this.CbUrlScheme.Size = new System.Drawing.Size(62, 21);
            this.CbUrlScheme.TabIndex = 25;
            // 
            // PbDownloadLinkConnection
            // 
            this.PbDownloadLinkConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PbDownloadLinkConnection.Image = ((System.Drawing.Image)(resources.GetObject("PbDownloadLinkConnection.Image")));
            this.PbDownloadLinkConnection.Location = new System.Drawing.Point(557, 38);
            this.PbDownloadLinkConnection.Name = "PbDownloadLinkConnection";
            this.PbDownloadLinkConnection.Size = new System.Drawing.Size(32, 32);
            this.PbDownloadLinkConnection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PbDownloadLinkConnection.TabIndex = 23;
            this.PbDownloadLinkConnection.TabStop = false;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.lvArchive);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(592, 413);
            this.tabPage9.TabIndex = 4;
            this.tabPage9.Text = "Files and Folders";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(592, 413);
            this.tabPage10.TabIndex = 5;
            this.tabPage10.Text = "Registry";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // tabPage11
            // 
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(592, 413);
            this.tabPage11.TabIndex = 6;
            this.tabPage11.Text = "Download Sites";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.splitContainer1);
            this.tabPage12.Location = new System.Drawing.Point(4, 22);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(592, 413);
            this.tabPage12.TabIndex = 7;
            this.tabPage12.Text = "Build";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(614, 471);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.LUpdateStats);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(606, 445);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Start Page";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // LUpdateStats
            // 
            this.LUpdateStats.AutoSize = true;
            this.LUpdateStats.Location = new System.Drawing.Point(8, 3);
            this.LUpdateStats.Name = "LUpdateStats";
            this.LUpdateStats.Size = new System.Drawing.Size(141, 13);
            this.LUpdateStats.TabIndex = 0;
            this.LUpdateStats.Text = "Update Status: NotChecked";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(606, 445);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Untitled";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.MdiWindowListItem = this.fileToolStripMenuItem;
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(614, 24);
            this.menuStripMain.TabIndex = 5;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(111, 6);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(111, 6);
            // 
            // TUpdate
            // 
            this.TUpdate.Enabled = true;
            this.TUpdate.Interval = 3000;
            this.TUpdate.Tick += new System.EventHandler(this.TUpdate_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Other";
            // 
            // cometUpdater1
            // 
            this.cometUpdater1.AutoUpdate = false;
            this.cometUpdater1.NotifyUpdateAvailable = true;
            this.cometUpdater1.NotifyUpdateReadyToInstall = true;
            this.cometUpdater1.NotifyUser = true;
            this.cometUpdater1.PackagePath = null;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 517);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.statusStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "Main";
            this.Text = "[Comet] Package Manager";
            this.Load += new System.EventHandler(this.Main_Load);
            this.cmsArchive.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudRevision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuild)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMajor)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbDownloadLinkConnection)).EndInit();
            this.tabPage9.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView lvArchive;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbPackageName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbPackageFilename;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPackageDownloadLink;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPackageChangeLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpRelease;
        private System.Windows.Forms.NumericUpDown nudRevision;
        private System.Windows.Forms.NumericUpDown nudBuild;
        private System.Windows.Forms.NumericUpDown nudMinor;
        private System.Windows.Forms.NumericUpDown nudMajor;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportAProblemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox tbSource;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ListView lvErrorList;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem loadEntryPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RunAfterBuildtoolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem loadInstallerScriptToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsArchive;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem addFilesToArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractToTheSpecifiedFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem extractFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.Label LUpdateStats;
        private System.Windows.Forms.PictureBox PbDownloadLinkConnection;
        private System.Windows.Forms.Timer TUpdate;
        private System.Windows.Forms.ComboBox CbUrlScheme;
        private System.Windows.Forms.Label label1;
        private Comet.CometUpdater cometUpdater1;
    }
}

