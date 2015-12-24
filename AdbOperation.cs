using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace BatchSend
{
    public class AdbOperation
    {
        public static void RefreshDevices()
        {
            string killCmm = "netstat -ano | findstr \":5037\"";
            string allListenning = execAndWait("cmd.exe", killCmm);
            string[] lines = allListenning.Split(new string[] { "\r\n" },StringSplitOptions.None);

            for (int i = 1; i < lines.Length;i++)
            {
                if (lines[i].Contains("LISTENING"))
                {
                    string[] cols = lines[i].Split(new string[] { "\t", "      ", "     ", "    ", "   ", "  ", " " }, StringSplitOptions.None);

                    string pid = "";
                    int cnt = 0;
                    foreach (string str in cols)
                    {
                        if (str != "")
                        {
                            cnt++;
                            if (cnt == 5)
                            {
                                pid = str;
                            }
                        }
                    }
                    execAndWait("cmd.exe", "taskkill /f /pid "+pid);
                }
            }
            execAndWait("cmd.exe", "\"" + getAdbPath() + "\" kill-server");
            execAndWait("cmd.exe", "\"" + getAdbPath() + "\" start-server");
        }

        public static void KillBlueStacks()
        {
            string killCmm = "tasklist | findstr \"HD\"";
            string allListenning = execAndWait("cmd.exe", killCmm);
            string[] lines = allListenning.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Contains(".exe"))
                {
                    string[] cols = lines[i].Split(new string[] { "\t", "      ", "     ", "    ", "   ", "  ", " " }, StringSplitOptions.None);

                    string pid = "";
                    int cnt = 0;
                    foreach (string str in cols)
                    {
                        if (str != "")
                        {
                            cnt++;
                            if (cnt == 2)
                            {
                                pid = str;
                            }
                        }
                    }
                    execAndWait("cmd.exe", "taskkill /f /pid " + pid);
                }
            }
        }

        public static void StartApp(string appName)
        {
            Process.Start(appName);
        }
        /************************************************************************/
        /* 获取所有连接的模拟器                                                 */
        /************************************************************************/
        public static List<string> getDevices()
        {
            List<string> rntList = new List<string>();
            string cmmString = execAndWait("cmd.exe", "\"" + getAdbPath() + "\" devices");
            string[] alls = cmmString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < alls.Length;i++)
            {
                if (alls[i].Trim() == "List of devices attached")
                {
                    string deviceLine = alls[++i];
                    while (deviceLine != null && deviceLine !="" )
                    {
                        string[] deviceInfo = deviceLine.Split(new string[] { "\t" },StringSplitOptions.None);
                        if (deviceInfo.Length == 2 
                            && deviceInfo[1].Trim() == "device")
                        {
                            rntList.Add(deviceInfo[0]);
                        }
                        deviceLine = alls[++i];
                    }
                    break;
                }
            }
            return rntList;
        }

        private static string getAdbPath()
        {
            return Application.StartupPath + "\\" + "adb\\adb.exe";
        }   
        public static void pushFile(string filePath,string targetPath,string emulatorSerial)
        {
            if (emulatorSerial == null)
            {
                execAndWait("cmd.exe", "\"" + getAdbPath() + "\" push " + filePath + " " + targetPath);
            }
            else
            {
                execAndWait("cmd.exe", "\"" + getAdbPath() + "\" -s " + emulatorSerial + " push " + filePath + " " + targetPath);
            }

        }

        /************************************************************************/
        /* 执行命令                                                             */
        /************************************************************************/
        public static string execAndWait(string exePath, string cmdLines)
        {

            Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = exePath;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            process.StandardInput.WriteLine(cmdLines);
            process.StandardInput.WriteLine("exit");
            process.StandardInput.AutoFlush = true;
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            return output;
        }
    }
}
