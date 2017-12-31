namespace PackageManager.Forms
{
    partial class Options
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
            this.CbDisplayWelcomePage = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CbNotifyBeforeInstallingUpdates = new System.Windows.Forms.CheckBox();
            this.CbAutoUpdate = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.LMaximumRecentProjects = new System.Windows.Forms.Label();
            this.NudMaximumRecentProjects = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudMaximumRecentProjects)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CbDisplayWelcomePage
            // 
            this.CbDisplayWelcomePage.AutoSize = true;
            this.CbDisplayWelcomePage.Location = new System.Drawing.Point(6, 19);
            this.CbDisplayWelcomePage.Name = "CbDisplayWelcomePage";
            this.CbDisplayWelcomePage.Size = new System.Drawing.Size(136, 17);
            this.CbDisplayWelcomePage.TabIndex = 0;
            this.CbDisplayWelcomePage.Text = "Display Welcome Page";
            this.CbDisplayWelcomePage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CbNotifyBeforeInstallingUpdates);
            this.groupBox1.Controls.Add(this.CbAutoUpdate);
            this.groupBox1.Controls.Add(this.CbDisplayWelcomePage);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 114);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Updater Options";
            // 
            // CbNotifyBeforeInstallingUpdates
            // 
            this.CbNotifyBeforeInstallingUpdates.AutoSize = true;
            this.CbNotifyBeforeInstallingUpdates.Location = new System.Drawing.Point(6, 65);
            this.CbNotifyBeforeInstallingUpdates.Name = "CbNotifyBeforeInstallingUpdates";
            this.CbNotifyBeforeInstallingUpdates.Size = new System.Drawing.Size(170, 17);
            this.CbNotifyBeforeInstallingUpdates.TabIndex = 3;
            this.CbNotifyBeforeInstallingUpdates.Text = "Notify before installing updates";
            this.CbNotifyBeforeInstallingUpdates.UseVisualStyleBackColor = true;
            // 
            // CbAutoUpdate
            // 
            this.CbAutoUpdate.AutoSize = true;
            this.CbAutoUpdate.Location = new System.Drawing.Point(6, 42);
            this.CbAutoUpdate.Name = "CbAutoUpdate";
            this.CbAutoUpdate.Size = new System.Drawing.Size(158, 17);
            this.CbAutoUpdate.TabIndex = 2;
            this.CbAutoUpdate.Text = "Automatically install updates";
            this.CbAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Location = new System.Drawing.Point(224, 185);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.Location = new System.Drawing.Point(143, 185);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 3;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(6, 88);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(220, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "User option: Allow custom install directory";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // LMaximumRecentProjects
            // 
            this.LMaximumRecentProjects.AutoSize = true;
            this.LMaximumRecentProjects.Location = new System.Drawing.Point(6, 16);
            this.LMaximumRecentProjects.Name = "LMaximumRecentProjects";
            this.LMaximumRecentProjects.Size = new System.Drawing.Size(133, 13);
            this.LMaximumRecentProjects.TabIndex = 4;
            this.LMaximumRecentProjects.Text = "Maximum Recent Projects:";
            // 
            // NudMaximumRecentProjects
            // 
            this.NudMaximumRecentProjects.Location = new System.Drawing.Point(145, 14);
            this.NudMaximumRecentProjects.Name = "NudMaximumRecentProjects";
            this.NudMaximumRecentProjects.Size = new System.Drawing.Size(130, 20);
            this.NudMaximumRecentProjects.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LMaximumRecentProjects);
            this.groupBox2.Controls.Add(this.NudMaximumRecentProjects);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 43);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 220);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Options";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudMaximumRecentProjects)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox CbDisplayWelcomePage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox CbNotifyBeforeInstallingUpdates;
        private System.Windows.Forms.CheckBox CbAutoUpdate;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label LMaximumRecentProjects;
        private System.Windows.Forms.NumericUpDown NudMaximumRecentProjects;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}