namespace Comet.UserControls
{
    partial class ChangeLogPanel
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
            this.TbChangeLog = new System.Windows.Forms.TextBox();
            this.LVersion = new System.Windows.Forms.Label();
            this.LChanges = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // TbChangeLog
            // 
            this.TbChangeLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbChangeLog.Location = new System.Drawing.Point(0, 49);
            this.TbChangeLog.Multiline = true;
            this.TbChangeLog.Name = "TbChangeLog";
            this.TbChangeLog.ReadOnly = true;
            this.TbChangeLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TbChangeLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TbChangeLog.Size = new System.Drawing.Size(406, 183);
            this.TbChangeLog.TabIndex = 1;
            // 
            // LVersion
            // 
            this.LVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LVersion.Location = new System.Drawing.Point(3, 0);
            this.LVersion.Name = "LVersion";
            this.LVersion.Size = new System.Drawing.Size(400, 31);
            this.LVersion.TabIndex = 2;
            this.LVersion.Text = "The version of ProductName installed on this computer is: v#.#.#.#. \r\nThe latest " +
    "version is: v#.#.#.#.";
            // 
            // LChanges
            // 
            this.LChanges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LChanges.Location = new System.Drawing.Point(3, 31);
            this.LChanges.Name = "LChanges";
            this.LChanges.Size = new System.Drawing.Size(400, 13);
            this.LChanges.TabIndex = 4;
            this.LChanges.Text = "Listed below are the changes and improvements:";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.LVersion);
            this.panelHeader.Controls.Add(this.LChanges);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(406, 49);
            this.panelHeader.TabIndex = 5;
            // 
            // ChangeLogPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.TbChangeLog);
            this.Controls.Add(this.panelHeader);
            this.Name = "ChangeLogPanel";
            this.Size = new System.Drawing.Size(406, 232);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TbChangeLog;
        private System.Windows.Forms.Label LVersion;
        private System.Windows.Forms.Label LChanges;
        private System.Windows.Forms.Panel panelHeader;
    }
}
