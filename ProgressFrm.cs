using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatchSend
{
    public partial class ProgressFrm : Form
    {
        public ProgressFrm()
        {
            InitializeComponent();
        }

        private void ProgressFrm_Load(object sender, EventArgs e)
        {

        }

        // 设置主进度条的
        public void SetMainLabel(string txt)
        {
            this.Invoke(new EventHandler(delegate
            {

                mainLabel.Text = txt;

            }));
        }
        // 设置子进度条的
        public void SetSubLabel(string txt)
        {
            this.Invoke(new EventHandler(delegate
            {

                subLabel.Text = txt;

            }));
        }

        // 设置主进度条的
        public void SetMainProgress(int degree)
        {
            this.Invoke(new EventHandler(delegate
            {

                MainProgressBar.Value = degree;

            }));
        }

        // 设置主进度条的
        public void SetSubProgress(int degree)
        {
            this.Invoke(new EventHandler(delegate
            {

                SubProgressBar.Value = degree;

            }));
        }

        // 设置主进度条的
        public void SetSubProgressMax(int maxValue)
        {
            this.Invoke(new EventHandler(delegate
            {

                SubProgressBar.Value = 0;
                SubProgressBar.Maximum = maxValue;

            }));
        }

        // 设置主进度条的
        public void SetMainProgressMax(int maxValue)
        {
            this.Invoke(new EventHandler(delegate
            {

                MainProgressBar.Maximum = maxValue;
                MainProgressBar.Value = 0;

            }));
        }

        public void CloseMe()
        {
            this.Invoke(new EventHandler(delegate
            {

                this.Close();

            }));
        }
    }
}
