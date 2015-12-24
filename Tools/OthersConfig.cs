using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatchSend
{
    public partial class OthersConfig : Form
    {
        public OthersConfig()
        {
            InitializeComponent();
        }

        private void OthersConfig_Load(object sender, EventArgs e)
        {
            tbImgCnt.Text = Configurations.getInstance().imageSendCnt.ToString();// OperateIniFile.ReadIniData("SEND", "ImageCount", "4", configFile);
            ceAutoExport.Checked = Configurations.getInstance().isAutoExportAccount;// OperateIniFile.ReadIniData("SEND", "AutoExport", "true", configFile).ToLower().Equals("true");
            cb_sendImg.Checked = Configurations.getInstance().isSendImg;
            cbShowAllLogs.Checked = Configurations.getInstance().showAllLogs;
            teAccount.Text = Configurations.getInstance().checkAccount;
            teGapCnt.Text = Configurations.getInstance().checkGap.ToString();

          //  cbShowAllLogs.Checked = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Configurations.getInstance().imageSendCnt = Convert.ToInt32(tbImgCnt.Text);
            Configurations.getInstance().isAutoExportAccount = ceAutoExport.Checked;
            Configurations.getInstance().isSendImg = cb_sendImg.Checked;
            Configurations.getInstance().checkAccount = teAccount.Text;
            Configurations.getInstance().checkGap = Convert.ToInt32(teGapCnt.Text.Trim());
            Configurations.getInstance().showAllLogs = cbShowAllLogs.Checked;
            Configurations.getInstance().Save();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
