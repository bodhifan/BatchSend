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
    public partial class FilterDuplicateAccount : Form
    {
        public FilterDuplicateAccount()
        {
            InitializeComponent();
        }

        private void beFile1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dail = BatchSend.Tools.Utilities.CreateOpenFileDialog(null);
            dail.InitialDirectory = Application.StartupPath;

            if (dail.ShowDialog() == DialogResult.OK)
            {
                beFile1.Text = dail.FileName;

                string dir = beFile1.Text.Substring(0, beFile1.Text.LastIndexOf("\\") + 1) + "过滤后的号.txt";
                beFileTarget.Text = dir;
            }
        }

        private void beFile2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dail = BatchSend.Tools.Utilities.CreateOpenFileDialog(null);
            dail.InitialDirectory = Application.StartupPath;

            if (dail.ShowDialog() == DialogResult.OK)
            {
                beFile2.Text = dail.FileName;
            }
        }

        private void beFileTarget_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (beFileTarget.Text.Trim() == "")
            {
                MessageBox.Show("选择过滤后的目标文件");
                return;
            }

            int lines = 0;
         //   int splitAccount = Convert.ToInt32(teSplitLineNum.Text.Trim());
            StreamReader reader;
            string line;
            List<string> allLines = new List<string>(5000);

            if (beFile2.Text.Trim() != "")
            {
                reader = new StreamReader(beFile2.Text.Trim(), Encoding.GetEncoding("GB2312"));
                while ((line = reader.ReadLine()) != null)
                {
                    allLines.Add(line);
                }
                reader.Close();
            }
            reader = new StreamReader(beFile1.Text.Trim(), Encoding.GetEncoding("GB2312"));
            StreamWriter writer = new StreamWriter(beFileTarget.Text.Trim());
            while ((line = reader.ReadLine()) != null)
            {
                if (allLines.Contains(line))
                {
                    continue;
                }
                allLines.Add(line);
                lines++;
                writer.WriteLine(line);

            }
            try
            {
                writer.Flush();
                writer.Close();
            }
            catch (System.Exception ex)
            {

            }

            MessageBox.Show("过滤成功! 文件参见 " + beFileTarget.Text);
        }
    }
}
