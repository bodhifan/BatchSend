namespace BatchSend.Tools
{
    partial class ExportAccountFrm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.exportBtn = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.ButtonEdit();
            this.labelerror = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.ceLowLimit = new DevExpress.XtraEditors.CheckEdit();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.teSendCount = new DevExpress.XtraEditors.TextEdit();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.btnAddLevelItem = new DevExpress.XtraEditors.SimpleButton();
            this.teLevelIdx = new DevExpress.XtraEditors.TextEdit();
            this.label27 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.teExportSymbol = new DevExpress.XtraEditors.TextEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.btnImport = new DevExpress.XtraEditors.ButtonEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.teSplitSymbol = new DevExpress.XtraEditors.TextEdit();
            this.teExportIdxs = new DevExpress.XtraEditors.TextEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport.Properties)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceLowLimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSendCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teLevelIdx.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teExportSymbol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSplitSymbol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teExportIdxs.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.groupBox17);
            this.groupBox3.Controls.Add(this.groupBox8);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.teExportSymbol);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.btnImport);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.teSplitSymbol);
            this.groupBox3.Controls.Add(this.teExportIdxs);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(564, 428);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "初始化工具";
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.label16);
            this.groupBox17.Controls.Add(this.exportBtn);
            this.groupBox17.Controls.Add(this.btnExport);
            this.groupBox17.Controls.Add(this.labelerror);
            this.groupBox17.Location = new System.Drawing.Point(6, 270);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(538, 145);
            this.groupBox17.TabIndex = 15;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "导出";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 12);
            this.label16.TabIndex = 12;
            this.label16.Text = "导出文件位置";
            // 
            // exportBtn
            // 
            this.exportBtn.Location = new System.Drawing.Point(207, 84);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(118, 55);
            this.exportBtn.TabIndex = 15;
            this.exportBtn.Text = "导出";
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(110, 25);
            this.btnExport.Name = "btnExport";
            this.btnExport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnExport.Size = new System.Drawing.Size(273, 21);
            this.btnExport.TabIndex = 13;
            // 
            // labelerror
            // 
            this.labelerror.AutoSize = true;
            this.labelerror.ForeColor = System.Drawing.Color.Red;
            this.labelerror.Location = new System.Drawing.Point(10, 54);
            this.labelerror.Name = "labelerror";
            this.labelerror.Size = new System.Drawing.Size(77, 12);
            this.labelerror.TabIndex = 14;
            this.labelerror.Text = "导出文件位置";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.ceLowLimit);
            this.groupBox8.Controls.Add(this.listBox1);
            this.groupBox8.Controls.Add(this.cbLevel);
            this.groupBox8.Controls.Add(this.teSendCount);
            this.groupBox8.Controls.Add(this.label29);
            this.groupBox8.Controls.Add(this.label28);
            this.groupBox8.Controls.Add(this.btnAddLevelItem);
            this.groupBox8.Controls.Add(this.teLevelIdx);
            this.groupBox8.Controls.Add(this.label27);
            this.groupBox8.Location = new System.Drawing.Point(6, 111);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(538, 153);
            this.groupBox8.TabIndex = 19;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "级别设置";
            // 
            // ceLowLimit
            // 
            this.ceLowLimit.EditValue = true;
            this.ceLowLimit.Location = new System.Drawing.Point(96, 112);
            this.ceLowLimit.Name = "ceLowLimit";
            this.ceLowLimit.Properties.Caption = "弱性格式";
            this.ceLowLimit.Size = new System.Drawing.Size(105, 19);
            this.ceLowLimit.TabIndex = 30;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(299, 26);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(217, 104);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 26;
            // 
            // cbLevel
            // 
            this.cbLevel.FormattingEnabled = true;
            this.cbLevel.Location = new System.Drawing.Point(98, 49);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(100, 20);
            this.cbLevel.TabIndex = 25;
            // 
            // teSendCount
            // 
            this.teSendCount.EditValue = "5";
            this.teSendCount.Location = new System.Drawing.Point(98, 85);
            this.teSendCount.Name = "teSendCount";
            this.teSendCount.Size = new System.Drawing.Size(100, 21);
            this.teSendCount.TabIndex = 22;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(34, 90);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(53, 12);
            this.label29.TabIndex = 23;
            this.label29.Text = "发送数量";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(33, 57);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(53, 12);
            this.label28.TabIndex = 21;
            this.label28.Text = "级别名称";
            // 
            // btnAddLevelItem
            // 
            this.btnAddLevelItem.Location = new System.Drawing.Point(207, 20);
            this.btnAddLevelItem.Name = "btnAddLevelItem";
            this.btnAddLevelItem.Size = new System.Drawing.Size(78, 111);
            this.btnAddLevelItem.TabIndex = 19;
            this.btnAddLevelItem.Text = "》";
            this.btnAddLevelItem.Click += new System.EventHandler(this.btnAddLevelItem_Click);
            // 
            // teLevelIdx
            // 
            this.teLevelIdx.Location = new System.Drawing.Point(98, 22);
            this.teLevelIdx.Name = "teLevelIdx";
            this.teLevelIdx.Size = new System.Drawing.Size(100, 21);
            this.teLevelIdx.TabIndex = 16;
            this.teLevelIdx.Leave += new System.EventHandler(this.teLevelIdx_Leave);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(33, 26);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 17;
            this.label27.Text = "级别索引";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(284, 86);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 10;
            this.label15.Text = "导出元素分隔符";
            // 
            // teExportSymbol
            // 
            this.teExportSymbol.EditValue = " ";
            this.teExportSymbol.Location = new System.Drawing.Point(422, 81);
            this.teExportSymbol.Name = "teExportSymbol";
            this.teExportSymbol.Size = new System.Drawing.Size(100, 21);
            this.teExportSymbol.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 7;
            this.label11.Text = "导出元素索引";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(99, 23);
            this.btnImport.Name = "btnImport";
            this.btnImport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnImport.Size = new System.Drawing.Size(423, 21);
            this.btnImport.TabIndex = 6;
            this.btnImport.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnImport_ButtonClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(40, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "分割符号";
            // 
            // teSplitSymbol
            // 
            this.teSplitSymbol.EditValue = "----";
            this.teSplitSymbol.Location = new System.Drawing.Point(100, 51);
            this.teSplitSymbol.Name = "teSplitSymbol";
            this.teSplitSymbol.Size = new System.Drawing.Size(100, 21);
            this.teSplitSymbol.TabIndex = 4;
            // 
            // teExportIdxs
            // 
            this.teExportIdxs.Location = new System.Drawing.Point(100, 81);
            this.teExportIdxs.Name = "teExportIdxs";
            this.teExportIdxs.Size = new System.Drawing.Size(100, 21);
            this.teExportIdxs.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(40, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "文件位置";
            // 
            // ExportAccountFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 450);
            this.Controls.Add(this.groupBox3);
            this.Name = "ExportAccountFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "小号整理工具";
            this.Load += new System.EventHandler(this.ExportAccountFrm_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport.Properties)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceLowLimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSendCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teLevelIdx.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teExportSymbol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSplitSymbol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teExportIdxs.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox8;
        private DevExpress.XtraEditors.CheckEdit ceLowLimit;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox cbLevel;
        private DevExpress.XtraEditors.TextEdit teSendCount;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private DevExpress.XtraEditors.SimpleButton btnAddLevelItem;
        private DevExpress.XtraEditors.TextEdit teLevelIdx;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label15;
        private DevExpress.XtraEditors.TextEdit teExportSymbol;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.ButtonEdit btnImport;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TextEdit teSplitSymbol;
        private DevExpress.XtraEditors.TextEdit teExportIdxs;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Label label16;
        private DevExpress.XtraEditors.SimpleButton exportBtn;
        private DevExpress.XtraEditors.ButtonEdit btnExport;
        private System.Windows.Forms.Label labelerror;
    }
}