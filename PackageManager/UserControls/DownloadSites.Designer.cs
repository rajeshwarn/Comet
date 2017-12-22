namespace PackageManager.UserControls
{
    partial class DownloadSites
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainerDownloadSites = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LUrl = new System.Windows.Forms.Label();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.CbUrlScheme = new System.Windows.Forms.ComboBox();
            this.TextBoxUrl = new System.Windows.Forms.TextBox();
            this.ListViewUrlList = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CmsDownloadSites = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDownloadSites)).BeginInit();
            this.splitContainerDownloadSites.Panel1.SuspendLayout();
            this.splitContainerDownloadSites.Panel2.SuspendLayout();
            this.splitContainerDownloadSites.SuspendLayout();
            this.panel1.SuspendLayout();
            this.CmsDownloadSites.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerDownloadSites
            // 
            this.splitContainerDownloadSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerDownloadSites.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerDownloadSites.IsSplitterFixed = true;
            this.splitContainerDownloadSites.Location = new System.Drawing.Point(0, 0);
            this.splitContainerDownloadSites.Name = "splitContainerDownloadSites";
            this.splitContainerDownloadSites.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerDownloadSites.Panel1
            // 
            this.splitContainerDownloadSites.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainerDownloadSites.Panel2
            // 
            this.splitContainerDownloadSites.Panel2.Controls.Add(this.ListViewUrlList);
            this.splitContainerDownloadSites.Size = new System.Drawing.Size(529, 316);
            this.splitContainerDownloadSites.SplitterDistance = 30;
            this.splitContainerDownloadSites.TabIndex = 29;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LUrl);
            this.panel1.Controls.Add(this.BtnAdd);
            this.panel1.Controls.Add(this.CbUrlScheme);
            this.panel1.Controls.Add(this.TextBoxUrl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 30);
            this.panel1.TabIndex = 29;
            // 
            // LUrl
            // 
            this.LUrl.AutoSize = true;
            this.LUrl.Location = new System.Drawing.Point(4, 7);
            this.LUrl.Name = "LUrl";
            this.LUrl.Size = new System.Drawing.Size(32, 13);
            this.LUrl.TabIndex = 0;
            this.LUrl.Text = "URL:";
            // 
            // BtnAdd
            // 
            this.BtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAdd.Location = new System.Drawing.Point(451, 3);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // CbUrlScheme
            // 
            this.CbUrlScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbUrlScheme.FormattingEnabled = true;
            this.CbUrlScheme.Items.AddRange(new object[] {
            "http://",
            "https://"});
            this.CbUrlScheme.Location = new System.Drawing.Point(42, 3);
            this.CbUrlScheme.Name = "CbUrlScheme";
            this.CbUrlScheme.Size = new System.Drawing.Size(62, 21);
            this.CbUrlScheme.TabIndex = 27;
            // 
            // TextBoxUrl
            // 
            this.TextBoxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxUrl.Location = new System.Drawing.Point(110, 4);
            this.TextBoxUrl.Name = "TextBoxUrl";
            this.TextBoxUrl.Size = new System.Drawing.Size(335, 20);
            this.TextBoxUrl.TabIndex = 26;
            // 
            // ListViewUrlList
            // 
            this.ListViewUrlList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11});
            this.ListViewUrlList.ContextMenuStrip = this.CmsDownloadSites;
            this.ListViewUrlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewUrlList.FullRowSelect = true;
            this.ListViewUrlList.GridLines = true;
            this.ListViewUrlList.Location = new System.Drawing.Point(0, 0);
            this.ListViewUrlList.Name = "ListViewUrlList";
            this.ListViewUrlList.Size = new System.Drawing.Size(529, 282);
            this.ListViewUrlList.TabIndex = 3;
            this.ListViewUrlList.UseCompatibleStateImageBehavior = false;
            this.ListViewUrlList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "URL";
            this.columnHeader10.Width = 257;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Available";
            this.columnHeader11.Width = 136;
            // 
            // CmsDownloadSites
            // 
            this.CmsDownloadSites.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem1,
            this.toolStripSeparator5,
            this.refreshToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.CmsDownloadSites.Name = "cmsDownloadSites";
            this.CmsDownloadSites.Size = new System.Drawing.Size(123, 76);
            // 
            // selectAllToolStripMenuItem1
            // 
            this.selectAllToolStripMenuItem1.Name = "selectAllToolStripMenuItem1";
            this.selectAllToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem1.Text = "Select All";
            this.selectAllToolStripMenuItem1.Click += new System.EventHandler(this.SelectAllToolStripMenuItem1_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(119, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // DownloadSites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerDownloadSites);
            this.Name = "DownloadSites";
            this.Size = new System.Drawing.Size(529, 316);
            this.splitContainerDownloadSites.Panel1.ResumeLayout(false);
            this.splitContainerDownloadSites.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDownloadSites)).EndInit();
            this.splitContainerDownloadSites.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.CmsDownloadSites.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerDownloadSites;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LUrl;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.ComboBox CbUrlScheme;
        private System.Windows.Forms.TextBox TextBoxUrl;
        private System.Windows.Forms.ListView ListViewUrlList;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ContextMenuStrip CmsDownloadSites;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    }
}
