namespace BatchSend
{
    partial class ProgressFrm
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
            this.MainProgressBar = new System.Windows.Forms.ProgressBar();
            this.SubProgressBar = new System.Windows.Forms.ProgressBar();
            this.mainLabel = new System.Windows.Forms.Label();
            this.subLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MainProgressBar
            // 
            this.MainProgressBar.Location = new System.Drawing.Point(12, 32);
            this.MainProgressBar.Name = "MainProgressBar";
            this.MainProgressBar.Size = new System.Drawing.Size(437, 20);
            this.MainProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.MainProgressBar.TabIndex = 0;
            // 
            // SubProgressBar
            // 
            this.SubProgressBar.Location = new System.Drawing.Point(12, 86);
            this.SubProgressBar.Name = "SubProgressBar";
            this.SubProgressBar.Size = new System.Drawing.Size(437, 18);
            this.SubProgressBar.TabIndex = 1;
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Location = new System.Drawing.Point(13, 11);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(65, 12);
            this.mainLabel.TabIndex = 2;
            this.mainLabel.Text = "当前文件夹";
            // 
            // subLabel
            // 
            this.subLabel.AutoSize = true;
            this.subLabel.Location = new System.Drawing.Point(13, 65);
            this.subLabel.Name = "subLabel";
            this.subLabel.Size = new System.Drawing.Size(65, 12);
            this.subLabel.TabIndex = 3;
            this.subLabel.Text = "文件进度：";
            // 
            // ProgressFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 124);
            this.Controls.Add(this.subLabel);
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.SubProgressBar);
            this.Controls.Add(this.MainProgressBar);
            this.Name = "ProgressFrm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "替换内部文件";
            this.Load += new System.EventHandler(this.ProgressFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar MainProgressBar;
        private System.Windows.Forms.ProgressBar SubProgressBar;
        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.Label subLabel;
    }
}