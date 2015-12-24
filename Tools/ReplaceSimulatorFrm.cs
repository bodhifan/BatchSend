using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BatchSend.Tools
{
    public partial class ReplaceSimulatorFrm : Form
    {
        public ReplaceSimulatorFrm()
        {
            InitializeComponent();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(beSourceFolder.Text.Trim()))
            {
                MessageBox.Show("输入正确的源文件夹位置");
                return;
            }
            if (!Directory.Exists(beTargetFolder.Text.Trim()))
            {
                MessageBox.Show("输入正确的目标文件夹位置");
                return;
            }

            string targetPath = beTargetFolder.Text.Trim();
            string sourcePath = beSourceFolder.Text.Trim();

            OperateIniFile.WriteIniData("SOFTWARE", "TargetFolder", beTargetFolder.Text.Trim(), Configurations.getInstance().configFile);
            OperateIniFile.WriteIniData("SOFTWARE", "SourceFolder", beSourceFolder.Text.Trim(), Configurations.getInstance().configFile);
            OperateIniFile.WriteIniData("SOFTWARE", "StartAppLocation", beStartAppLocation.Text.Trim(), Configurations.getInstance().configFile);

            string appLocation = null;
            if (File.Exists(beStartAppLocation.Text.Trim()))
            {
                OperateIniFile.WriteIniData("SOFTWARE", "StartAppLocation", beStartAppLocation.Text.Trim(), Configurations.getInstance().configFile);
                appLocation = beStartAppLocation.Text.Trim();
            }
            CopyDirectoryFactory.CopyDirectory(sourcePath, targetPath, appLocation);
        }

        private void beTargetFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog folderSelect = new FolderBrowserDialog();
            if (folderSelect.ShowDialog() == DialogResult.OK)
            {
                beTargetFolder.Text = folderSelect.SelectedPath;
            }
        }

        private void beSourceFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog folderSelect = new FolderBrowserDialog();
            if (folderSelect.ShowDialog() == DialogResult.OK)
            {
                beSourceFolder.Text = folderSelect.SelectedPath;
            }
        }

        private void beStartAppLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dail = new OpenFileDialog();

            if (dail.ShowDialog() == DialogResult.OK)
            {
                beStartAppLocation.Text = dail.FileName;
            }
        }

        private void ReplaceSimulatorFrm_Load(object sender, EventArgs e)
        {
            beTargetFolder.Text = Configurations.getInstance().targetFilePath;
            beSourceFolder.Text = Configurations.getInstance().sourceFilePath;
            beStartAppLocation.Text = Configurations.getInstance().startAppLocation;
        }
    }
}
