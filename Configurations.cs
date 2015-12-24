using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BatchSend
{
    public class Configurations
    {
        public int imageSendCnt; //图片发送数量
        public string configFile = Application.StartupPath + "/config.ini"; //配置文件位置
        public bool showAllLogs = true; // 是否显示所有日志
        public bool isScanAccount = false; // 扫描账号
        public bool isAutoExportAccount = false;// 自动导出账号
        public bool isSendImg = true; //是否发送图片
        public bool needRestart = false;

        private static Configurations m_Instance =null;

        // 图片验证码账号
        public string imageAccount = "";
        public string imagePwd = "";


        // 回测小号
        public string checkAccount = "";
        public int checkGap = 50;

        // 分隔小号
        public int splitNum = 200;
         

        // 模拟器替换工具
        public string sourceFilePath = ""; // 源文件路径
        public string targetFilePath = ""; // 目标文件路径
        public string startAppLocation = "";// 启动程序

        public static Configurations getInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new Configurations();
            }
            return m_Instance;
        }

        public Configurations()
        {
            imageSendCnt = Convert.ToInt32(OperateIniFile.ReadIniData("SEND", "ImageCount", "4", configFile));
            isAutoExportAccount = OperateIniFile.ReadIniData("SEND", "AutoExport", "true", configFile).ToLower().Equals("true");
            isSendImg = OperateIniFile.ReadIniData("SEND", "IsSendImage", "true", configFile).ToLower().Equals("true");

            imageAccount = OperateIniFile.ReadIniData("ACCOUNT", "ImageUser", "", configFile);
            imagePwd = OperateIniFile.ReadIniData("ACCOUNT", "ImagePwd", "", configFile);

            checkAccount = OperateIniFile.ReadIniData("ACCOUNT", "CheckUser", "", configFile);
            checkGap = Convert.ToInt32(OperateIniFile.ReadIniData("ACCOUNT", "CheckGap", "50", configFile));
            splitNum = Convert.ToInt32(OperateIniFile.ReadIniData("TOOLS", "SplitNum", "200", configFile));

            // 模拟器替换工具
            targetFilePath = OperateIniFile.ReadIniData("SOFTWARE", "TargetFolder", @"C:\ProgramData\BlueStacks", configFile);
            sourceFilePath = OperateIniFile.ReadIniData("SOFTWARE", "SourceFolder", "", configFile);

            startAppLocation=OperateIniFile.ReadIniData("SOFTWARE", "StartAppLocation", @"C:\Program Files (x86)\BlueStacks\HD-StartLauncher.exe", configFile);
            if (!File.Exists(startAppLocation))
            {
                startAppLocation = @"C:\Program Files\BlueStacks\HD-StartLauncher.exe";
                if (!File.Exists(startAppLocation))
                {
                    startAppLocation = "";
                }
            }
        }

        public void Save()
        {
            OperateIniFile.WriteIniData("SEND", "ImageCount", imageSendCnt.ToString(), configFile);
            OperateIniFile.WriteIniData("SEND", "AutoExport", isAutoExportAccount.ToString().ToLower(), configFile);
            OperateIniFile.WriteIniData("SEND", "IsSendImage", isSendImg.ToString().ToLower(), configFile);

            OperateIniFile.WriteIniData("ACCOUNT", "ImageUser", imageAccount, configFile);
            OperateIniFile.WriteIniData("ACCOUNT", "ImagePwd", imagePwd, configFile);

             OperateIniFile.WriteIniData("ACCOUNT", "CheckUser", checkAccount, configFile);
             OperateIniFile.WriteIniData("ACCOUNT", "CheckGap", checkGap.ToString(), configFile);
             OperateIniFile.WriteIniData("TOOLS", "SplitNum", splitNum.ToString(), configFile);
        }
    }
}
