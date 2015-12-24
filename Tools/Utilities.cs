using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BatchSend.Tools
{
    public class Utilities
    {
        public static OpenFileDialog CreateOpenFileDialog(string title)
        {
            OpenFileDialog select = new OpenFileDialog();
            select.InitialDirectory = Application.StartupPath;
            if (title != null && title != "")
            {
                select.Title = title;
            }
            select.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            return select;
        }
    }
}
