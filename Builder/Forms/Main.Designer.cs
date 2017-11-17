namespace Builder.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnArchiveExtractFile = new System.Windows.Forms.Button();
            this.btnArchiveNew = new System.Windows.Forms.Button();
            this.btnArchiveRemove = new System.Windows.Forms.Button();
            this.btnArchiveExtractToDirectory = new System.Windows.Forms.Button();
            this.lvArchive = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnArchiveAdd = new System.Windows.Forms.Button();
            this.btnArchiveLoad = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.recentHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyRecentFilesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.file0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportAProblemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRevision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuild)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMajor)).BeginInit();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(455, 352);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnArchiveExtractFile);
            this.tabPage2.Controls.Add(this.btnArchiveNew);
            this.tabPage2.Controls.Add(this.btnArchiveRemove);
            this.tabPage2.Controls.Add(this.btnArchiveExtractToDirectory);
            this.tabPage2.Controls.Add(this.lvArchive);
            this.tabPage2.Controls.Add(this.btnArchiveAdd);
            this.tabPage2.Controls.Add(this.btnArchiveLoad);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(447, 326);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Archive Editor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnArchiveExtractFile
            // 
            this.btnArchiveExtractFile.Enabled = false;
            this.btnArchiveExtractFile.Location = new System.Drawing.Point(3, 32);
            this.btnArchiveExtractFile.Name = "btnArchiveExtractFile";
            this.btnArchiveExtractFile.Size = new System.Drawing.Size(112, 23);
            this.btnArchiveExtractFile.TabIndex = 8;
            this.btnArchiveExtractFile.Text = "Extract To File";
            this.btnArchiveExtractFile.UseVisualStyleBackColor = true;
            this.btnArchiveExtractFile.Click += new System.EventHandler(this.BtnArchiveExtractFile_Click);
            // 
            // btnArchiveNew
            // 
            this.btnArchiveNew.Location = new System.Drawing.Point(3, 6);
            this.btnArchiveNew.Name = "btnArchiveNew";
            this.btnArchiveNew.Size = new System.Drawing.Size(75, 23);
            this.btnArchiveNew.TabIndex = 7;
            this.btnArchiveNew.Text = "New";
            this.btnArchiveNew.UseVisualStyleBackColor = true;
            this.btnArchiveNew.Click += new System.EventHandler(this.BtnArchiveNew_Click);
            // 
            // btnArchiveRemove
            // 
            this.btnArchiveRemove.Enabled = false;
            this.btnArchiveRemove.Location = new System.Drawing.Point(246, 6);
            this.btnArchiveRemove.Name = "btnArchiveRemove";
            this.btnArchiveRemove.Size = new System.Drawing.Size(75, 23);
            this.btnArchiveRemove.TabIndex = 6;
            this.btnArchiveRemove.Text = "Remove";
            this.btnArchiveRemove.UseVisualStyleBackColor = true;
            this.btnArchiveRemove.Click += new System.EventHandler(this.BtnArchiveRemove_Click);
            // 
            // btnArchiveExtractToDirectory
            // 
            this.btnArchiveExtractToDirectory.Enabled = false;
            this.btnArchiveExtractToDirectory.Location = new System.Drawing.Point(121, 32);
            this.btnArchiveExtractToDirectory.Name = "btnArchiveExtractToDirectory";
            this.btnArchiveExtractToDirectory.Size = new System.Drawing.Size(112, 23);
            this.btnArchiveExtractToDirectory.TabIndex = 5;
            this.btnArchiveExtractToDirectory.Text = "Extract To Directory";
            this.btnArchiveExtractToDirectory.UseVisualStyleBackColor = true;
            this.btnArchiveExtractToDirectory.Click += new System.EventHandler(this.BtnArchiveExtractToDirectory_Click);
            // 
            // lvArchive
            // 
            this.lvArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvArchive.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvArchive.FullRowSelect = true;
            this.lvArchive.GridLines = true;
            this.lvArchive.Location = new System.Drawing.Point(6, 61);
            this.lvArchive.Name = "lvArchive";
            this.lvArchive.Size = new System.Drawing.Size(435, 259);
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
            // btnArchiveAdd
            // 
            this.btnArchiveAdd.Enabled = false;
            this.btnArchiveAdd.Location = new System.Drawing.Point(165, 6);
            this.btnArchiveAdd.Name = "btnArchiveAdd";
            this.btnArchiveAdd.Size = new System.Drawing.Size(75, 23);
            this.btnArchiveAdd.TabIndex = 3;
            this.btnArchiveAdd.Text = "Add";
            this.btnArchiveAdd.UseVisualStyleBackColor = true;
            this.btnArchiveAdd.Click += new System.EventHandler(this.BtnArchiveAdd_Click);
            // 
            // btnArchiveLoad
            // 
            this.btnArchiveLoad.Location = new System.Drawing.Point(84, 6);
            this.btnArchiveLoad.Name = "btnArchiveLoad";
            this.btnArchiveLoad.Size = new System.Drawing.Size(75, 23);
            this.btnArchiveLoad.TabIndex = 0;
            this.btnArchiveLoad.Text = "Load";
            this.btnArchiveLoad.UseVisualStyleBackColor = true;
            this.btnArchiveLoad.Click += new System.EventHandler(this.BtnArchiveLoad_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.nudRevision);
            this.tabPage1.Controls.Add(this.nudBuild);
            this.tabPage1.Controls.Add(this.nudMinor);
            this.tabPage1.Controls.Add(this.nudMajor);
            this.tabPage1.Controls.Add(this.dtpRelease);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.tbPackageName);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.tbPackageFilename);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.tbPackageDownloadLink);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.tbPackageChangeLog);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(447, 326);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Package Editor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // nudRevision
            // 
            this.nudRevision.Location = new System.Drawing.Point(270, 137);
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
            this.nudBuild.Location = new System.Drawing.Point(211, 137);
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
            this.nudMinor.Location = new System.Drawing.Point(152, 137);
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
            this.nudMajor.Location = new System.Drawing.Point(93, 136);
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
            this.dtpRelease.Location = new System.Drawing.Point(93, 110);
            this.dtpRelease.Name = "dtpRelease";
            this.dtpRelease.Size = new System.Drawing.Size(346, 20);
            this.dtpRelease.TabIndex = 18;
            this.dtpRelease.ValueChanged += new System.EventHandler(this.DateTimePickerRelease_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Version:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Release:";
            // 
            // tbPackageName
            // 
            this.tbPackageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPackageName.Location = new System.Drawing.Point(93, 84);
            this.tbPackageName.Name = "tbPackageName";
            this.tbPackageName.Size = new System.Drawing.Size(348, 20);
            this.tbPackageName.TabIndex = 13;
            this.tbPackageName.TextChanged += new System.EventHandler(this.TbPackage_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Name:";
            // 
            // tbPackageFilename
            // 
            this.tbPackageFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPackageFilename.Location = new System.Drawing.Point(93, 58);
            this.tbPackageFilename.Name = "tbPackageFilename";
            this.tbPackageFilename.Size = new System.Drawing.Size(348, 20);
            this.tbPackageFilename.TabIndex = 11;
            this.tbPackageFilename.TextChanged += new System.EventHandler(this.TbPackage_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Filename:";
            // 
            // tbPackageDownloadLink
            // 
            this.tbPackageDownloadLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPackageDownloadLink.Location = new System.Drawing.Point(93, 32);
            this.tbPackageDownloadLink.Name = "tbPackageDownloadLink";
            this.tbPackageDownloadLink.Size = new System.Drawing.Size(348, 20);
            this.tbPackageDownloadLink.TabIndex = 9;
            this.tbPackageDownloadLink.TextChanged += new System.EventHandler(this.TbPackage_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Download Link:";
            // 
            // tbPackageChangeLog
            // 
            this.tbPackageChangeLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPackageChangeLog.Location = new System.Drawing.Point(93, 6);
            this.tbPackageChangeLog.Name = "tbPackageChangeLog";
            this.tbPackageChangeLog.Size = new System.Drawing.Size(348, 20);
            this.tbPackageChangeLog.TabIndex = 7;
            this.tbPackageChangeLog.TextChanged += new System.EventHandler(this.TbPackage_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "ChangeLog:";
            // 
            // statusStripMain
            // 
            this.statusStripMain.Location = new System.Drawing.Point(0, 376);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(455, 22);
            this.statusStripMain.TabIndex = 4;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(455, 24);
            this.menuStripMain.TabIndex = 5;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
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
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
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
            this.recentHistoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyRecentFilesListToolStripMenuItem,
            this.toolStripSeparator5,
            this.file0ToolStripMenuItem});
            this.recentHistoryToolStripMenuItem.Name = "recentHistoryToolStripMenuItem";
            this.recentHistoryToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.recentHistoryToolStripMenuItem.Text = "Recent History";
            // 
            // emptyRecentFilesListToolStripMenuItem
            // 
            this.emptyRecentFilesListToolStripMenuItem.Name = "emptyRecentFilesListToolStripMenuItem";
            this.emptyRecentFilesListToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.emptyRecentFilesListToolStripMenuItem.Text = "Empty Recent Files List";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(191, 6);
            // 
            // file0ToolStripMenuItem
            // 
            this.file0ToolStripMenuItem.Name = "file0ToolStripMenuItem";
            this.file0ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.file0ToolStripMenuItem.Text = "file0";
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
            this.viewToolStripMenuItem.Text = "View";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportAProblemToolStripMenuItem,
            this.toolStripSeparator7,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
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
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 398);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.statusStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "Main";
            this.Text = "Comet - Builder";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRevision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuild)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMajor)).EndInit();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnArchiveLoad;
        private System.Windows.Forms.Button btnArchiveAdd;
        private System.Windows.Forms.ListView lvArchive;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnArchiveExtractToDirectory;
        private System.Windows.Forms.Button btnArchiveRemove;
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
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem emptyRecentFilesListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem file0ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportAProblemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnArchiveNew;
        private System.Windows.Forms.Button btnArchiveExtractFile;
    }
}

