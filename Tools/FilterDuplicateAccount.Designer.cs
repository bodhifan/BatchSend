namespace BatchSend.Tools
{
    partial class FilterDuplicateAccount
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
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.beFileTarget = new DevExpress.XtraEditors.ButtonEdit();
            this.label39 = new System.Windows.Forms.Label();
            this.beFile2 = new DevExpress.XtraEditors.ButtonEdit();
            this.label37 = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.CheckBox();
            this.btnFilter = new DevExpress.XtraEditors.SimpleButton();
            this.beFile1 = new DevExpress.XtraEditors.ButtonEdit();
            this.label38 = new System.Windows.Forms.Label();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beFileTarget.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beFile2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beFile1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.beFileTarget);
            this.groupBox11.Controls.Add(this.label39);
            this.groupBox11.Controls.Add(this.beFile2);
            this.groupBox11.Controls.Add(this.label37);
            this.groupBox11.Controls.Add(this.cbFilter);
            this.groupBox11.Controls.Add(this.btnFilter);
            this.groupBox11.Controls.Add(this.beFile1);
            this.groupBox11.Controls.Add(this.label38);
            this.groupBox11.Location = new System.Drawing.Point(22, 21);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(540, 124);
            this.groupBox11.TabIndex = 7;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "过滤重复号";
            // 
            // beFileTarget
            // 
            this.beFileTarget.Location = new System.Drawing.Point(96, 92);
            this.beFileTarget.Name = "beFileTarget";
            this.beFileTarget.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beFileTarget.Size = new System.Drawing.Size(283, 21);
            this.beFileTarget.TabIndex = 26;
            this.beFileTarget.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beFileTarget_ButtonClick);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(13, 97);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(77, 12);
            this.label39.TabIndex = 25;
            this.label39.Text = "目标文件位置";
            // 
            // beFile2
            // 
            this.beFile2.Location = new System.Drawing.Point(96, 58);
            this.beFile2.Name = "beFile2";
            this.beFile2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beFile2.Size = new System.Drawing.Size(283, 21);
            this.beFile2.TabIndex = 24;
            this.beFile2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beFile2_ButtonClick);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(31, 61);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(59, 12);
            this.label37.TabIndex = 23;
            this.label37.Text = "文件位置2";
            // 
            // cbFilter
            // 
            this.cbFilter.AutoSize = true;
            this.cbFilter.Checked = true;
            this.cbFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilter.Location = new System.Drawing.Point(404, 20);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(84, 16);
            this.cbFilter.TabIndex = 22;
            this.cbFilter.Text = "过滤重复号";
            this.cbFilter.UseVisualStyleBackColor = true;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(404, 64);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(123, 45);
            this.btnFilter.TabIndex = 21;
            this.btnFilter.Text = "开始过滤";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // beFile1
            // 
            this.beFile1.Location = new System.Drawing.Point(96, 20);
            this.beFile1.Name = "beFile1";
            this.beFile1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beFile1.Size = new System.Drawing.Size(283, 21);
            this.beFile1.TabIndex = 20;
            this.beFile1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beFile1_ButtonClick);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(37, 24);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(53, 12);
            this.label38.TabIndex = 17;
            this.label38.Text = "文件位置";
            // 
            // FilterDuplicateAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 172);
            this.Controls.Add(this.groupBox11);
            this.Name = "FilterDuplicateAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "过滤重复号";
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beFileTarget.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beFile2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beFile1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox11;
        private DevExpress.XtraEditors.ButtonEdit beFileTarget;
        private System.Windows.Forms.Label label39;
        private DevExpress.XtraEditors.ButtonEdit beFile2;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.CheckBox cbFilter;
        private DevExpress.XtraEditors.SimpleButton btnFilter;
        private DevExpress.XtraEditors.ButtonEdit beFile1;
        private System.Windows.Forms.Label label38;
    }
}