using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BatchSend
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
//             if (!Library.Content.InitContent())
//             {
//                 MessageBox.Show("软件暂时不支持未注册用户!");
//                 Application.Exit();
//             }
//             else
            Application.Run(new Form1());
        }
    }
}
