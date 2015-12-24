namespace BatchSend.Tools
{
    partial class ReplaceSimulatorFrm
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
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.beStartAppLocation = new DevExpress.XtraEditors.ButtonEdit();
            this.label35 = new System.Windows.Forms.Label();
            this.beTargetFolder = new DevExpress.XtraEditors.ButtonEdit();
            this.label33 = new System.Windows.Forms.Label();
            this.simpleButton9 = new DevExpress.XtraEditors.SimpleButton();
            this.beSourceFolder = new DevExpress.XtraEditors.ButtonEdit();
            this.label34 = new System.Windows.Forms.Label();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beStartAppLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beTargetFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beSourceFolder.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.beStartAppLocation);
            this.groupBox10.Controls.Add(this.label35);
            this.groupBox10.Controls.Add(this.beTargetFolder);
            this.groupBox10.Controls.Add(this.label33);
            this.groupBox10.Controls.Add(this.simpleButton9);
            this.groupBox10.Controls.Add(this.beSourceFolder);
            this.groupBox10.Controls.Add(this.label34);
            this.groupBox10.Location = new System.Drawing.Point(12, 12);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(555, 176);
            this.groupBox10.TabIndex = 17;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "内部文件";
            // 
            // beStartAppLocation
            // 
            this.beStartAppLocation.Location = new System.Drawing.Point(99, 88);
            this.beStartAppLocation.Name = "beStartAppLocation";
            this.beStartAppLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beStartAppLocation.Size = new System.Drawing.Size(433, 21);
            this.beStartAppLocation.TabIndex = 19;
            this.beStartAppLocation.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beStartAppLocation_ButtonClick);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(5, 93);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(89, 12);
            this.label35.TabIndex = 18;
            this.label35.Text = "模拟器启动位置";
            // 
            // beTargetFolder
            // 
            this.beTargetFolder.Location = new System.Drawing.Point(99, 19);
            this.beTargetFolder.Name = "beTargetFolder";
            this.beTargetFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beTargetFolder.Size = new System.Drawing.Size(433, 21);
            this.beTargetFolder.TabIndex = 17;
            this.beTargetFolder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beTargetFolder_ButtonClick);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 24);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(89, 12);
            this.label33.TabIndex = 16;
            this.label33.Text = "目标文件夹位置";
            // 
            // simpleButton9
            // 
            this.simpleButton9.Location = new System.Drawing.Point(190, 125);
            this.simpleButton9.Name = "simpleButton9";
            this.simpleButton9.Size = new System.Drawing.Size(123, 45);
            this.simpleButton9.TabIndex = 15;
            this.simpleButton9.Text = "开始替换";
            this.simpleButton9.Click += new System.EventHandler(this.simpleButton9_Click);
            // 
            // beSourceFolder
            // 
            this.beSourceFolder.Location = new System.Drawing.Point(99, 55);
            this.beSourceFolder.Name = "beSourceFolder";
            this.beSourceFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beSourceFolder.Size = new System.Drawing.Size(433, 21);
            this.beSourceFolder.TabIndex = 6;
            this.beSourceFolder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beSourceFolder_ButtonClick);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(5, 57);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(89, 12);
            this.label34.TabIndex = 1;
            this.label34.Text = "内部文件夹位置";
            // 
            // ReplaceSimulatorFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 203);
            this.Controls.Add(this.groupBox10);
            this.Name = "ReplaceSimulatorFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模拟器替换工具";
            this.Load += new System.EventHandler(this.ReplaceSimulatorFrm_Load);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beStartAppLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beTargetFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beSourceFolder.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox10;
        private DevExpress.XtraEditors.ButtonEdit beStartAppLocation;
        private System.Windows.Forms.Label label35;
        private DevExpress.XtraEditors.ButtonEdit beTargetFolder;
        private System.Windows.Forms.Label label33;
        private DevExpress.XtraEditors.SimpleButton simpleButton9;
        private DevExpress.XtraEditors.ButtonEdit beSourceFolder;
        private System.Windows.Forms.Label label34;
    }
}