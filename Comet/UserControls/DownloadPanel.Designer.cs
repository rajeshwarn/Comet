namespace Comet.UserControls
{
    partial class DownloadPanel
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
            this.LDown = new System.Windows.Forms.Label();
            this.progressBarFileDownload = new System.Windows.Forms.ProgressBar();
            this.LBytesTotalSize = new System.Windows.Forms.Label();
            this.LBytesReceived = new System.Windows.Forms.Label();
            this.LDownloadFiles = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LProgress = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LDown
            // 
            this.LDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.LDown.Location = new System.Drawing.Point(0, 0);
            this.LDown.Name = "LDown";
            this.LDown.Size = new System.Drawing.Size(446, 31);
            this.LDown.TabIndex = 3;
            this.LDown.Text = "Comet is downloading updates for {package.Name}. This process could take a few mi" +
    "nutes.";
            // 
            // progressBarFileDownload
            // 
            this.progressBarFileDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarFileDownload.Location = new System.Drawing.Point(3, 110);
            this.progressBarFileDownload.Name = "progressBarFileDownload";
            this.progressBarFileDownload.Size = new System.Drawing.Size(440, 23);
            this.progressBarFileDownload.TabIndex = 4;
            // 
            // LBytesTotalSize
            // 
            this.LBytesTotalSize.AutoSize = true;
            this.LBytesTotalSize.Location = new System.Drawing.Point(6, 29);
            this.LBytesTotalSize.Name = "LBytesTotalSize";
            this.LBytesTotalSize.Size = new System.Drawing.Size(77, 13);
            this.LBytesTotalSize.TabIndex = 5;
            this.LBytesTotalSize.Text = "Total Size: # B";
            // 
            // LBytesReceived
            // 
            this.LBytesReceived.AutoSize = true;
            this.LBytesReceived.Location = new System.Drawing.Point(6, 16);
            this.LBytesReceived.Name = "LBytesReceived";
            this.LBytesReceived.Size = new System.Drawing.Size(76, 13);
            this.LBytesReceived.TabIndex = 6;
            this.LBytesReceived.Text = "Received: # B";
            // 
            // LDownloadFiles
            // 
            this.LDownloadFiles.AutoSize = true;
            this.LDownloadFiles.Location = new System.Drawing.Point(0, 31);
            this.LDownloadFiles.Name = "LDownloadFiles";
            this.LDownloadFiles.Size = new System.Drawing.Size(119, 13);
            this.LDownloadFiles.TabIndex = 7;
            this.LDownloadFiles.Text = "Download File/s: # of #";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.LBytesReceived);
            this.groupBox1.Controls.Add(this.LBytesTotalSize);
            this.groupBox1.Location = new System.Drawing.Point(3, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 47);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // LProgress
            // 
            this.LProgress.AutoSize = true;
            this.LProgress.Location = new System.Drawing.Point(0, 44);
            this.LProgress.Name = "LProgress";
            this.LProgress.Size = new System.Drawing.Size(68, 13);
            this.LProgress.TabIndex = 9;
            this.LProgress.Text = "Progress: 0%";
            // 
            // DownloadPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LProgress);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LDownloadFiles);
            this.Controls.Add(this.progressBarFileDownload);
            this.Controls.Add(this.LDown);
            this.Name = "DownloadPanel";
            this.Size = new System.Drawing.Size(446, 237);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LDown;
        private System.Windows.Forms.ProgressBar progressBarFileDownload;
        private System.Windows.Forms.Label LBytesTotalSize;
        private System.Windows.Forms.Label LBytesReceived;
        private System.Windows.Forms.Label LDownloadFiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label LProgress;
    }
}
