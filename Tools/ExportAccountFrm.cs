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
    public partial class ExportAccountFrm : Form
    {
        public ExportAccountFrm()
        {
            InitializeComponent();
        }
        private string configFile = "";
        Dictionary<string, int> sendCntMap = new Dictionary<string, int>();
        // 级别分割符
        string levelSplitSymbol = " <--------> ";


        private void ExportAccountFrm_Load(object sender, EventArgs e)
        {
            sendCntMap.Clear();
            configFile = Configurations.getInstance().configFile;
            btnExport.Text = Application.StartupPath + "/" + "整理后的小号.txt";
            labelerror.Text = Application.StartupPath + "/" + "账号密码不正确小号.txt";

            teLevelIdx.Text = OperateIniFile.ReadIniData("TOOL", "LevelIndex", "4", configFile);
            teSendCount.Text = OperateIniFile.ReadIniData("TOOL", "SendCount", "5", configFile);
            String LevelMap = OperateIniFile.ReadIniData("TOOL", "LevelMapping", "", configFile);
            teSplitSymbol.Text = OperateIniFile.ReadIniData("TOOL", "SplitSymbol", "----", configFile);
            ceLowLimit.Checked = OperateIniFile.ReadIniData("TOOL", "LowLevel", "true", configFile).ToLower().Equals("true");

            if (teSplitSymbol.Text.Equals(""))
            {
                teSplitSymbol.Text = " ";
            }

            teExportIdxs.Text = OperateIniFile.ReadIniData("TOOL", "ExportIndex", "", configFile);

            if (LevelMap != "")
            {
                string[] levelList = LevelMap.Split(new string[] { "|" }, StringSplitOptions.None);
                listBox1.Items.Clear();
                foreach (string level in levelList)
                {
                    if (level.Trim() != "")
                    {
                        string[] cols = level.Split(new string[] { levelSplitSymbol }, StringSplitOptions.None);
                        sendCntMap.Add(cols[0], Convert.ToInt32(cols[1]));
                        listBox1.Items.Add(level);
                    }

                }
            }
        }

        /************************************************************************/
        /* 保存工具tab的配置                                                    */
        /************************************************************************/
        private void SaveToolsConfig()
        {
            // OperateIniFile.WriteIniData("IMAGE", "IsRandomFont", chRandomFont.Checked.ToString(), configFile);
            OperateIniFile.WriteIniData("TOOL", "LevelIndex", teLevelIdx.Text.Trim(), configFile);
            OperateIniFile.WriteIniData("TOOL", "SendCount", teSendCount.Text.Trim(), configFile);
            OperateIniFile.WriteIniData("TOOL", "SplitSymbol", teSplitSymbol.Text, configFile);

            OperateIniFile.WriteIniData("TOOL", "LowLevel", ceLowLimit.Checked.ToString(), configFile);
            OperateIniFile.WriteIniData("TOOL", "ExportIndex", teExportIdxs.Text.Trim(), configFile);

            string levelMap = "";
            foreach (object obj in listBox1.Items)
            {
                levelMap += obj.ToString() + "|";
            }
            OperateIniFile.WriteIniData("TOOL", "LevelMapping", levelMap, configFile);
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            if (btnImport.Text == null
               || btnImport.Text == "")
            {
                MessageBox.Show("请选择需要整理的小号文件");
                return;
            }

            if (btnExport.Text == null
                || btnExport.Text == "")
            {
                MessageBox.Show("请选择导出文件的位置");
                return;
            }

            if (teSplitSymbol.Text == "" ||
                teExportIdxs.Text == ""
                || teExportSymbol.Text == "")
            {
                MessageBox.Show("信息输入不完整");
                return;
            }

            StreamWriter writer = new StreamWriter(btnExport.Text);
            StreamWriter errWriter = new StreamWriter(labelerror.Text);
            errWriter.AutoFlush = true;
            StreamReader reader = new StreamReader(btnImport.Text, Encoding.GetEncoding("GB2312"));

            string line = null;
            string importSplitSymbol = teSplitSymbol.Text;
            string[] exportIdxs = teExportIdxs.Text.Trim().Split(',');
            string exportSplitSymbol = teExportSymbol.Text;
            int allInfoCnts = 0;

            bool isExportSuc = true;
            while ((line = reader.ReadLine()) != null && isExportSuc)
            {
                if (line.Trim() == "")
                {
                    continue;
                }

                // 用分割符号分割行
                string[] allInfos = line.Split(new string[] { importSplitSymbol }, StringSplitOptions.None);
                if (allInfoCnts == 0)
                {
                    allInfoCnts = allInfos.Length;
                }

                List<string> tempInfos = new List<string>();

                foreach (string str in allInfos)
                {
                    if (str.Trim() != "")
                    {
                        tempInfos.Add(str);
                    }

                }

                if (!ceLowLimit.Checked)
                {
                    if (tempInfos.Count < allInfoCnts)
                    {
                        // 这是一条错误的账号信息
                        errWriter.WriteLine(line);
                        continue;
                    }

                    string level = tempInfos[Convert.ToInt32(teLevelIdx.Text.Trim()) - 1];
                    if (!sendCntMap.ContainsKey(level))
                    {
                        errWriter.WriteLine(line);
                        continue;
                    }
                }


                for (int i = 0; i < exportIdxs.Length - 1; i++)
                {
                    int idx = Convert.ToInt32(exportIdxs[i]) - 1;
                    if (idx >= allInfoCnts)
                    {
                        MessageBox.Show("导出索引错误! 目标个数：" + allInfoCnts.ToString() + " 当前索引：" + idx.ToString());
                        isExportSuc = false;
                        break;
                    }
                    if (tempInfos[idx].Trim() == "")
                    {
                        errWriter.WriteLine(line);
                        break;
                    }
                    writer.Write(tempInfos[idx]);
                    writer.Write(exportSplitSymbol);
                }

                if (isExportSuc)
                {
                    writer.Write(tempInfos[Convert.ToInt32(exportIdxs[exportIdxs.Length - 1]) - 1]);
                }

                if (isExportSuc && teLevelIdx.Text.Trim() != "")
                {
                    string level = tempInfos[Convert.ToInt32(teLevelIdx.Text.Trim()) - 1];
                    string sendCnt = Configurations.getInstance().imageSendCnt.ToString();
                    if (sendCntMap.ContainsKey(level))
                    {
                        sendCnt = sendCntMap[level].ToString();
                    }
                    writer.Write(" " + sendCnt);
                }
                writer.WriteLine();
            }
            writer.Flush();
            errWriter.Flush();
            errWriter.Close();
            writer.Close();
            reader.Close();
            SaveToolsConfig();
            if (isExportSuc)
            {
                MessageBox.Show("导出成功!");
            }
        }

        /************************************************************************/
        /* 获取账号级别                                                         */
        /************************************************************************/
        private List<string> getAccountLevel(string filePath, int idx)
        {
            StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("GB2312"));

            string line = null;
            string importSplitSymbol = teSplitSymbol.Text;
            List<string> rntList = new List<string>();
            while ((line = reader.ReadLine()) != null)
            {
                string[] cols = line.Split(new string[] { importSplitSymbol }, StringSplitOptions.None);
                if (cols.Length >= idx && !rntList.Contains(cols[idx - 1]))
                {
                    rntList.Add(cols[idx - 1]);
                }
            }
            rntList.Sort();
            return rntList;
        }

        private void btnAddLevelItem_Click(object sender, EventArgs e)
        {
            if (teSendCount.Text.Trim() == "")
            {
                MessageBox.Show("请设置 发送数量");
                return;
            }

            string level = cbLevel.SelectedItem.ToString();
            int sendCnt = Convert.ToInt32(teSendCount.Text.Trim());

            if (sendCntMap.ContainsKey(level))
            {
                sendCntMap.Remove(level);
                foreach (object obj in listBox1.Items)
                {
                    if (obj.ToString().Contains(level))
                    {
                        listBox1.Items.Remove(obj);
                        break;
                    }
                }
            }
            sendCntMap.Add(level, sendCnt);
            listBox1.Items.Add(level + levelSplitSymbol + sendCnt.ToString());
        }

        private void teLevelIdx_Leave(object sender, EventArgs e)
        {
            if (teLevelIdx.Text.Trim() == "")
            {
                return;
            }
            List<string> accountLevel = getAccountLevel(btnImport.Text, Convert.ToInt32(teLevelIdx.Text.Trim()));
            cbLevel.Items.Clear();
            foreach (string str in accountLevel)
            {
                cbLevel.Items.Add(str);
            }
        }
        /************************************************************************/
        /* 获取 级别 与 发送数量的字典                                          */
        /************************************************************************/
        private void getLevelMap()
        {
            sendCntMap.Clear();

            foreach (object obj in listBox1.Items)
            {
                string[] cols = obj.ToString().Split(new string[] { levelSplitSymbol }, StringSplitOptions.None);
                if (cols.Length == 2)
                {
                    sendCntMap.Add(cols[0].Trim(), Convert.ToInt32(cols[1].Trim()));
                }
            }
        }

        private void btnImport_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = BatchSend.Tools.Utilities.CreateOpenFileDialog(null);
            openFileDialog1.InitialDirectory = Application.StartupPath;
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                btnImport.Text = openFileDialog1.FileName;
            }
        }
    }
}
