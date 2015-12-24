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
    public partial class SplitToolFrm : Form
    {
        public SplitToolFrm()
        {
            InitializeComponent();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            string dir = Application.StartupPath + "\\分割文件夹";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            int lines = 0;
            int currentFileIdx = 1;
            int splitAccount = Convert.ToInt32(teSplitLineNum.Text.Trim());
            StreamReader reader = new StreamReader(beFilesPath.Text.Trim(), Encoding.GetEncoding("UTF-8"));
            string line;
            string path = dir + "\\" + currentFileIdx.ToString() + ".txt";
            if (splitAccount <= 0)
            {
                path = dir + "\\过滤后的号.txt";
            }

            StreamWriter writer = new StreamWriter(path);

            string path1 = dir + "\\重复记录.txt";
            StreamWriter writer1 = new StreamWriter(path1);
            List<string> allLines = new List<string>(5000);
            while ((line = reader.ReadLine()) != null)
            {
                if (allLines.Contains(line))
                {
                    writer1.WriteLine(line);
                    continue;
                }
                allLines.Add(line);
                lines++;
                if (splitAccount > 0 && lines > splitAccount)
                {
                    lines = 1;
                    currentFileIdx++;
                    path = dir + "\\" + currentFileIdx.ToString() + ".txt";
                    writer.Flush();
                    writer.Close();
                    writer = new StreamWriter(path);
                }
                writer.WriteLine(line);

            }
            try
            {
                writer.Flush();
                writer.Close();

                writer1.Flush();
                writer1.Close();
            }
            catch (System.Exception ex)
            {

            }
            if (!Configurations.getInstance().splitNum.ToString().Equals(teSplitLineNum.Text.Trim()))
            {
                Configurations.getInstance().splitNum = Convert.ToInt32(teSplitLineNum.Text.Trim());
                Configurations.getInstance().Save();
            }
            
            MessageBox.Show("分割成功! 文件参见 " + dir);
        }

        private void beFilesPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = BatchSend.Tools.Utilities.CreateOpenFileDialog(null);
            openFileDialog1.InitialDirectory = Application.StartupPath;
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                beFilesPath.Text = openFileDialog1.FileName;
            }
        }

        private void SplitToolFrm_Load(object sender, EventArgs e)
        {
            teSplitLineNum.Text = Configurations.getInstance().splitNum.ToString();
        }
    }
}
