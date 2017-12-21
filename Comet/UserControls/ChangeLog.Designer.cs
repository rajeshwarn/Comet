namespace Comet.UserControls
{
    partial class ChangeLog
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
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TbChangeLog
            // 
            this.TbChangeLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TbChangeLog.Location = new System.Drawing.Point(0, 51);
            this.TbChangeLog.Multiline = true;
            this.TbChangeLog.Name = "TbChangeLog";
            this.TbChangeLog.ReadOnly = true;
            this.TbChangeLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TbChangeLog.Size = new System.Drawing.Size(547, 232);
            this.TbChangeLog.TabIndex = 1;
            // 
            // LVersion
            // 
            this.LVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.LVersion.Location = new System.Drawing.Point(0, 0);
            this.LVersion.Name = "LVersion";
            this.LVersion.Size = new System.Drawing.Size(547, 31);
            this.LVersion.TabIndex = 2;
            this.LVersion.Text = "The version of ProductName installed on this computer is: v#.#.#.#. \r\nThe latest " +
    "version is: v#.#.#.#.";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Listed below are the changes and improvements:";
            // 
            // ChangeLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LVersion);
            this.Controls.Add(this.TbChangeLog);
            this.Name = "ChangeLog";
            this.Size = new System.Drawing.Size(547, 283);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TbChangeLog;
        private System.Windows.Forms.Label LVersion;
        private System.Windows.Forms.Label label3;
    }
}
