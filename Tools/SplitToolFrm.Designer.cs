namespace BatchSend.Tools
{
    partial class SplitToolFrm
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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            this.beFilesPath = new DevExpress.XtraEditors.ButtonEdit();
            this.label30 = new System.Windows.Forms.Label();
            this.teSplitLineNum = new DevExpress.XtraEditors.TextEdit();
            this.label31 = new System.Windows.Forms.Label();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beFilesPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSplitLineNum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.checkBox2);
            this.groupBox7.Controls.Add(this.simpleButton7);
            this.groupBox7.Controls.Add(this.beFilesPath);
            this.groupBox7.Controls.Add(this.label30);
            this.groupBox7.Controls.Add(this.teSplitLineNum);
            this.groupBox7.Controls.Add(this.label31);
            this.groupBox7.Location = new System.Drawing.Point(12, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(549, 135);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "分号工具";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(449, 52);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(84, 16);
            this.checkBox2.TabIndex = 16;
            this.checkBox2.Text = "过滤重复号";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // simpleButton7
            // 
            this.simpleButton7.Location = new System.Drawing.Point(209, 84);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(123, 45);
            this.simpleButton7.TabIndex = 15;
            this.simpleButton7.Text = "开始分割";
            this.simpleButton7.Click += new System.EventHandler(this.simpleButton7_Click);
            // 
            // beFilesPath
            // 
            this.beFilesPath.Location = new System.Drawing.Point(73, 23);
            this.beFilesPath.Name = "beFilesPath";
            this.beFilesPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beFilesPath.Size = new System.Drawing.Size(460, 21);
            this.beFilesPath.TabIndex = 6;
            this.beFilesPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beFilesPath_ButtonClick);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(14, 56);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(53, 12);
            this.label30.TabIndex = 5;
            this.label30.Text = "分割数量";
            // 
            // teSplitLineNum
            // 
            this.teSplitLineNum.EditValue = "200";
            this.teSplitLineNum.Location = new System.Drawing.Point(74, 51);
            this.teSplitLineNum.Name = "teSplitLineNum";
            this.teSplitLineNum.Size = new System.Drawing.Size(100, 21);
            this.teSplitLineNum.TabIndex = 4;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(14, 26);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(53, 12);
            this.label31.TabIndex = 1;
            this.label31.Text = "文件位置";
            // 
            // SplitToolFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 161);
            this.Controls.Add(this.groupBox7);
            this.Name = "SplitToolFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分隔小号";
            this.Load += new System.EventHandler(this.SplitToolFrm_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beFilesPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSplitLineNum.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox checkBox2;
        private DevExpress.XtraEditors.SimpleButton simpleButton7;
        private DevExpress.XtraEditors.ButtonEdit beFilesPath;
        private System.Windows.Forms.Label label30;
        private DevExpress.XtraEditors.TextEdit teSplitLineNum;
        private System.Windows.Forms.Label label31;
    }
}