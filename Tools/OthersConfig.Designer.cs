namespace BatchSend
{
    partial class OthersConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ceScanAccount = new DevExpress.XtraEditors.CheckEdit();
            this.ceAutoExport = new DevExpress.XtraEditors.CheckEdit();
            this.cb_sendImg = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbImgCnt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbShowAllLogs = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.teGapCnt = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.teAccount = new DevExpress.XtraEditors.TextEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceScanAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceAutoExport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teGapCnt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAccount.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ceScanAccount);
            this.groupBox1.Controls.Add(this.ceAutoExport);
            this.groupBox1.Controls.Add(this.cb_sendImg);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbImgCnt);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbShowAllLogs);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(422, 223);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // ceScanAccount
            // 
            this.ceScanAccount.Location = new System.Drawing.Point(256, 20);
            this.ceScanAccount.Name = "ceScanAccount";
            this.ceScanAccount.Properties.Caption = "开启扫号";
            this.ceScanAccount.Size = new System.Drawing.Size(81, 19);
            this.ceScanAccount.TabIndex = 27;
            // 
            // ceAutoExport
            // 
            this.ceAutoExport.EditValue = true;
            this.ceAutoExport.Location = new System.Drawing.Point(256, 47);
            this.ceAutoExport.Name = "ceAutoExport";
            this.ceAutoExport.Properties.Caption = "自动导出成功登录账号";
            this.ceAutoExport.Size = new System.Drawing.Size(145, 19);
            this.ceAutoExport.TabIndex = 26;
            // 
            // cb_sendImg
            // 
            this.cb_sendImg.AutoSize = true;
            this.cb_sendImg.Location = new System.Drawing.Point(25, 88);
            this.cb_sendImg.Name = "cb_sendImg";
            this.cb_sendImg.Size = new System.Drawing.Size(72, 16);
            this.cb_sendImg.TabIndex = 17;
            this.cb_sendImg.Text = "发送图片";
            this.cb_sendImg.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(113, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "个店铺切换图片";
            // 
            // tbImgCnt
            // 
            this.tbImgCnt.Location = new System.Drawing.Point(58, 52);
            this.tbImgCnt.Name = "tbImgCnt";
            this.tbImgCnt.Size = new System.Drawing.Size(40, 21);
            this.tbImgCnt.TabIndex = 21;
            this.tbImgCnt.Text = "4";
            this.tbImgCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "发送";
            // 
            // cbShowAllLogs
            // 
            this.cbShowAllLogs.AutoSize = true;
            this.cbShowAllLogs.Checked = true;
            this.cbShowAllLogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowAllLogs.Location = new System.Drawing.Point(23, 22);
            this.cbShowAllLogs.Name = "cbShowAllLogs";
            this.cbShowAllLogs.Size = new System.Drawing.Size(96, 16);
            this.cbShowAllLogs.TabIndex = 4;
            this.cbShowAllLogs.Text = "显示全部日志";
            this.cbShowAllLogs.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(51, 251);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 35);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(319, 251);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(228, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "查询间隔";
            // 
            // teGapCnt
            // 
            this.teGapCnt.EditValue = "50";
            this.teGapCnt.Location = new System.Drawing.Point(301, 25);
            this.teGapCnt.Name = "teGapCnt";
            this.teGapCnt.Size = new System.Drawing.Size(100, 21);
            this.teGapCnt.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "小号账号";
            // 
            // teAccount
            // 
            this.teAccount.Location = new System.Drawing.Point(82, 25);
            this.teAccount.Name = "teAccount";
            this.teAccount.Size = new System.Drawing.Size(100, 21);
            this.teAccount.TabIndex = 20;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.teGapCnt);
            this.groupBox2.Controls.Add(this.teAccount);
            this.groupBox2.Location = new System.Drawing.Point(18, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 62);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "信息回测";
            // 
            // OthersConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 296);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Name = "OthersConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "其他配置";
            this.Load += new System.EventHandler(this.OthersConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceScanAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceAutoExport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teGapCnt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAccount.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.CheckEdit ceScanAccount;
        private DevExpress.XtraEditors.CheckEdit ceAutoExport;
        private System.Windows.Forms.CheckBox cb_sendImg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbImgCnt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbShowAllLogs;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.TextEdit teGapCnt;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit teAccount;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}