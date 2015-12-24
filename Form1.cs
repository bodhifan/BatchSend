using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using DevExpress.Utils.Menu;
using System.Drawing.Text;
using System.Collections;
using System.Threading;
using System.Xml;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Runtime.Serialization.Formatters.Binary;

namespace BatchSend
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        static extern bool GenerateConsoleCtrlEvent(int dwCtrlEvent, int dwProcessGroupId);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(HandlerRoutine handlerRoutine, bool add);

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        public delegate bool HandlerRoutine(CtrlTypes CtrlType);


        bool isVirtualMobileActivity = true;
        // 账号列表
        List<User> usersList = new List<User>(1000);
        Dictionary<string, User> totalUsersList = new Dictionary<string, User>(1000);
        Dictionary<string, User> totalTargetsList = new Dictionary<string, User>(10000);
        List<User> targetsList = new List<User>(5000);
        List<SendResult> resultList = new List<SendResult>(50000);

        string startPath = null;
        // reg
        Regex firstReg = null;
        Regex secondReg = null;
        bool isGreatThan2 = false;

        // 图片索引
        int imageIdx = 0;
        int totalImgs = 1;
        // 当前接收数量
        int nReceive = 0;
        int everyReceive = 10;

        // 是否为第一次点击发送
        bool isFisrtTimeClick = true;
        // 配置文件
        string configFile = Configurations.getInstance().configFile;
        // 日志文件
        string logFile = Application.StartupPath + "/日志.txt";
        StreamWriter logWrite;

        string DIR_LOGIN_NAME = "导出登录账号";
        string DIR_UNUSE_NAME = "导出未使用账号";

        /************************************************************************/
        /* 发送小号变量                                                         */
        /************************************************************************/
        int totalSendCnt = 0;
        int totalReceviorCnt = 0;
        int totalSendUsedCnt = 0;
        int totalRecevierUsedCnt = 0;

        string currentDevice = "";

        bool isOnceOver = true;// 是否发送一次完整的数据
        int loopNums = 1;// 小号循环次数
        int curLoops = 0;// 当前循环次数

        int i = 0;
        bool isBegin = false;
        int usersRemainder = 0;
        int targetsRemainder = 0;

        Advertise currentAdv;

        bool isStop = false;

        string userBackupFile = @"userslist.bat";
        string targetBackupFile = @"targetslist.bat";

        // An enumerated type for the control messages
        // sent to the handler routine.
        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            firstReg = new Regex("TAG:([\\d]+)###");
            secondReg = new Regex("SEND USER:([\\d]+)  TARGET:([-\\d]+)  STATUS:(.*)");

            // 绑定数据
            gridControl1.DataSource = resultList;
            gridControl1.MainView.RefreshData();

            // 初始化
            Dictionary<string,string> configs = parseConfig("msg.list");
           // sendMsgBox.Text = configs.ContainsKey("MessageContent")?configs["MessageContent"]:"";
            tbCount.Text = configs.ContainsKey("SendCount")?configs["SendCount"]:"";
            addFriendFirst.Checked = configs.ContainsKey("FriendFirst") ? configs["FriendFirst"].Equals("true") :false ;
            tbFriend.Text = configs.ContainsKey("FriendCount")? configs["FriendCount"]:"";
          //  Configurations.getInstance().checkAccount = configs.ContainsKey("QueryAccount")?configs["QueryAccount"]:"";
            tbTimeout.Text = configs.ContainsKey("Timeouts") ? configs["Timeouts"] : "";
          //  tbImgCnt.Text =OperateIniFile.ReadIniData("SEND", "ImageCount", "4", configFile);
          //  ceAutoExport.Checked = OperateIniFile.ReadIniData("SEND", "AutoExport", "true", configFile).ToLower().Equals("true");
          //  Configurations.getInstance().checkGap = Convert.ToInt32(OperateIniFile.ReadIniData("QUERY", "QueryCount", "50", configFile));
            
            chSendOnline.Checked = OperateIniFile.ReadIniData("SEND", "SendOnline", "true", configFile).ToLower().Equals("true");
            chLoopAccountsNum.Checked = OperateIniFile.ReadIniData("SEND", "IsLoopAccount", "false", configFile).ToLower().Equals("true");
            cbLoopsNum.SelectedIndex =Convert.ToInt32(OperateIniFile.ReadIniData("SEND", "LoopNums", "3", configFile))-1;

            tbImageCheck.Text = OperateIniFile.ReadIniData("SEND", "ImageCheckCnt","2", configFile);
            String sendMode = OperateIniFile.ReadIniData("SEND", "SendMode", "1", configFile);

            initSendMode(sendMode);

           // cb_sendImg.Checked = configs["SendImage"].Equals("true");
            ceStartSmartSendMode.Checked = OperateIniFile.ReadIniData("SEND", "SendSmartMode", "true", configFile).ToLower().Equals("true");
            
            startPath = Application.StartupPath+"\\images";

            if (!Directory.Exists(startPath))
            {
                Directory.CreateDirectory(startPath);
            }
            totalImgs = Directory.GetFiles(startPath).Length;


            // 图片账号初始化
            tbImageCheckAccount.Text = Configurations.getInstance().imageAccount;
            tbImageCheckPwd.Text = Configurations.getInstance().imagePwd;
            if (tbImageCheckAccount.Text!=null&&
                tbImageCheckAccount.Text != "")
            {
                checkImageAccount();
            }

            List<string> devices = AdbOperation.getDevices();
            foreach (string device in devices)
            {
                cbDevices.Items.Add(device);
            }
            if (devices.Count>0)
            {
                cbDevices.SelectedIndex = 0;
            }

            // 日志
            logWrite  = new StreamWriter(logFile);

            // 初试化广告语
            initAdvertiesPanel();

            if(AdvertiseList.getInstance().myAdvs.Count > 0)
            {
                currentAdv = AdvertiseList.getInstance().next();
            }
            else
            {
                currentAdv = new Advertise();
            }

            // 软件到期时间
            tspDeadline.Text = "软件已注册，到期时间为："+ Library.Utility.GetInpiredDate(); 
        }

        private void initAdvertiesPanel()
        {
            gridControl2.DataSource = AdvertiseList.getInstance().myAdvs;
        }

        private void initSendMode(string sendMode)
        {
            switch (sendMode)
            {
                case "2": rbImageMode.Select(); break;
                case "3": rbTextMode.Select(); break;
                default: rbMixMode.Select(); break;
           }
        }

        private Dictionary<string,string> parseConfig(string path)
        {
            StreamReader reader = new StreamReader(path);
            string line =null;
            Dictionary<string, string> rntMap = new Dictionary<string, string>();
            while ((line=reader.ReadLine()) != null)
            {
                int idx = line.IndexOf('=');
                if (idx < 0)
                {
                    continue;
                }
                string key = line.Substring(0, idx);
                string value = line.Substring(idx + 1);
                rntMap.Add(key, value);
            }
            reader.Close();
            return rntMap;
        }
        Process process;
        private void sendBtn_Click(object sender, EventArgs e)
        {
            
        }
        private bool OnHandlerRoutine(CtrlTypes CtrlType)
        {
            statusBox.AppendText("===========CTRL + C ==========");
            return true;
        }
        private void writeMsg()
        {
            if (currentAdv.UsedNum >= currentAdv.TotalNum)
            {
                if (!AdvertiseList.getInstance().hasNext())
                {
                    OutMsg(statusBox, "没有可发送的内容", Color.Red);
                }
                else
                {
                    currentAdv = AdvertiseList.getInstance().next();
                }
            }
            string msgtxt = currentAdv.MsgContent;
            StreamWriter write = new StreamWriter("msg.list");
            msgtxt = msgtxt.Replace("\r\n"," ");
            write.WriteLine("MessageContent="+msgtxt);
            write.WriteLine("SendCount=" + tbCount.Text.Trim());
            string sendMode = getSendMode();
            write.WriteLine("FriendFirst=" + addFriendFirst.Checked.ToString().ToLower());
            write.WriteLine("FriendCount=" + tbFriend.Text);
            write.WriteLine("SendMode=" + sendMode);
            write.WriteLine("QueryAccount=" + Configurations.getInstance().checkAccount);
            write.WriteLine("Timeouts=" + tbTimeout.Text.Trim());
            write.WriteLine("IsScanAccount=" + Configurations.getInstance().isScanAccount.ToString());//ceScanAccount.Checked.ToString().ToLower());
            write.WriteLine("SendOnline=" + chSendOnline.Checked.ToString().ToLower());
            write.WriteLine("SendSmartMode=" + ceStartSmartSendMode.Checked.ToString().ToLower());
            write.WriteLine("ImageCheckCnt=" + tbImageCheck.Text.Trim());

            // 图片验证账号和密码
            write.WriteLine("ImageAccount=" + Configurations.getInstance().imageAccount);
            write.WriteLine("ImagePwd=" + Configurations.getInstance().imagePwd);
            write.WriteLine("IsStoped=" +isStop.ToString().ToLower());
            write.Flush();
            write.Close();
        }

        private void pushMsgToPhone()
        {
            writeMsg();
            execAndWait("cmd.exe", "\"" + getAdbPath() + "\" -s " + currentDevice + " push msg.list data/local/tmp");
        }

        private string getSendMode()
        {
            string rnt = "1";
            if (rbImageMode.Checked)
            {
                rnt = "2";
            }
            else if (rbTextMode.Checked)
            {
                rnt = "3";
            }

            return rnt;
                 
        }

        public string execAndWait(string exePath, string cmdLines)
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

        public Process exec(string exePath, string cmdLines)
        {
            Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = exePath;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            process.StandardInput.WriteLine(cmdLines + "&exit");
            process.BeginOutputReadLine();
           
            process.OutputDataReceived += new DataReceivedEventHandler(processOutputDataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceived);
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(process_Exited);

            isProcessEnd = false;
            return process;
        }

        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            string msg = e.Data.ToString();
            statusBox.ForeColor = Color.Red;//设置输入字体颜色
            statusBox.AppendText(msg+"\r\n");//需要以此种颜色显示的字
        }

        void process_Exited(object sender, EventArgs e)
        {
          //  statusBox.AppendText("发送完毕\r\n");
        }


        private void OutMsg(RichTextBox rtb, string msg, Color color)
        {
            rtb.Invoke(new EventHandler(delegate
            {
                rtb.SelectionStart = rtb.Text.Length;//设置插入符位置为文本框末
                rtb.SelectionColor = color;//设置文本颜色
                string msgCxt = DateTime.Now.ToString()+": " +msg;
                rtb.AppendText(msgCxt + "\r\n");//输出文本，换行
                rtb.ScrollToCaret();//滚动条滚到到最新插入行

                // 保存到日志文件
                logWrite.WriteLine(msgCxt);
                logWrite.Flush();

            }));
        }
        private void processOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e == null || e.Data == null)
            {
                return;
            }
            string msg = e.Data.ToString();
            

          //  OutMsg(statusBox, msg, Color.Red);
            Match first = firstReg.Match(msg);
            if (first.Success)
            {
                // 检查是否重启
                if (cbAutoRestart.Checked)
                {
                    isReceiveMsg = true;
                    needContinueSend = true;
                    invokeTimer();
                }

                msg = msg.Substring(first.Groups[0].Length);
                if (!isBegin && msg.Contains("begin"))
                {
                    isBegin = true;
                    OutMsg(statusBox, "===========================开始输出日志===========================", Color.Black);

                }
                if (msg.Contains("end."))
                {
                    OutMsg(statusBox, "===========================程序执行完毕===========================", Color.Black);
                    this.Invoke(new EventHandler(delegate
                    {
                        sendBtn.Enabled = true;
                        stopBtn.Enabled = false;
                    }));
                    timer1.Stop();

                    if (targetsRemainder>0 && usersRemainder > 0 && cbAutoRestart.Checked && !isStop)
                    {
                        OutMsg(statusBox, "还有未发送的账号" + usersRemainder.ToString() + "，重新启动", Color.Red);
                        Thread th = new Thread(new ThreadStart(ReSend));
                        th.Start();
                    }

                    isBegin = false;
                }
                if (first.Groups[1].Value == "30")
                {
                    Regex reg = new Regex("still has:([\\d]+)");
                    Match senderMath = reg.Match(msg.Trim());
                    if (senderMath.Success)
                    {
                        usersRemainder = Convert.ToInt32(senderMath.Groups[1].Value);
                        OutMsg(statusBox, "还剩下" + usersRemainder.ToString() + " 小号未使用", Color.Red);
                        if (usersRemainder <= 0)
                        {
                            needContinueSend = false;
                        }
                    }
                }
                if (first.Groups[1].Value == "31")
                {
                    Regex reg = new Regex("still has:([\\d]+)");
                    Match senderMath = reg.Match(msg.Trim());

                    if (senderMath.Success && needContinueSend)
                    {
                        targetsRemainder = Convert.ToInt32(senderMath.Groups[1].Value);
                        OutMsg(statusBox, "还剩下" + targetsRemainder.ToString() + " 店铺号未发送", Color.Red);
                        if (targetsRemainder <= 0)
                        {
                            needContinueSend = false;
                        }
                    }

                    if (usersRemainder > 0 && targetsRemainder > 0)
                    {

                        if (cbAutoRestart.Checked && !isStop)
                        {
                            OutMsg(statusBox, "程序异常终止,启用继续发送机制... 等待时间：1 分钟后重启", Color.Red);
                            needContinueSend = true;
                        }
                        else
                        {
                            OutMsg(statusBox, "程序异常终止！", Color.Red);
                        }
                    }
                    else
                    {
                        if (Configurations.getInstance().isAutoExportAccount)
                        {
                            try
                            {
                                exportSucAccount();
                                exportFailedAccount();
                            }
                            catch (System.Exception ex)
                            {
                                MessageBox.Show("未能成功导出账号，请手动导出登录成功&失败账号!");
                            }

                        }
                        if (++curLoops < loopNums && chLoopAccountsNum.Checked)
                        {
                            OutMsg(statusBox, "第 " + curLoops.ToString() + " 次重复发送数据", Color.Green);
                            isOnceOver = true;
                            Thread th = new Thread(new ThreadStart(ReSend));
                            th.Start();
                        }
                        else
                            OutMsg(statusBox, "数据全部发送完毕!", Color.Green);
                    }

                }
            }
            if (!first.Success)
            {
                return;
            }

            if (Configurations.getInstance().showAllLogs)
            {
                if (isBegin)
                {
                    OutMsg(statusBox, i++.ToString() + ":" + msg, Color.Black);
                }
            }

            Match second = secondReg.Match(msg);
            if (second.Success)
            {
                SendResult rnt = new SendResult();
                int userIdx = Convert.ToInt32(second.Groups[1].Value);
                rnt.userName = usersList[userIdx].name;
                rnt.password = usersList[userIdx].pwd;

                int targetIdx = 0;

                string errMsg = "";
                targetIdx = Convert.ToInt32(second.Groups[2].Value);
                if (targetIdx >= 0)
                {
                    rnt.target = targetsList[targetIdx].name;
                }
                else
                {
                    errMsg = getErrMsg(targetIdx);
                }
                switch(first.Groups[1].Value)
                {
                    case "21":
                        rnt.isSucc = true;
                        rnt.status = SendResult.STATUS_SUCC;
                        usersList[userIdx].isUsed = true;
                        usersList[userIdx].statusCode = StatusCode.UserLoginSuc;
                        targetsList[targetIdx].isUsed = true;
                        targetsList[targetIdx].statusCode = StatusCode.UserLoginSuc;

                        totalRecevierUsedCnt++;
                        UpdateRecevierUsed(totalRecevierUsedCnt);

                        // 更新发送状态
                        currentAdv.UsedNum++;
                        pushMsgToPhone();
                        break;
                    case "11":
                        usersList[userIdx].isUsed = true;
                        usersList[userIdx].statusCode = StatusCode.UserLoginFailed;
                        rnt.status = SendResult.STATUS_LOGIN_FAIL;
                        rnt.target = errMsg;

                        // 标记登录失败
                        totalUsersList[usersList[userIdx].name].statusCode = StatusCode.UserLoginFailed;
                        totalSendUsedCnt++;

                        // 登录失败
                        UpdateUsedSender(totalSendUsedCnt);
                        break;
                    case "12":
                        targetsList[targetIdx].isUsed = true;
                        targetsList[targetIdx].statusCode = StatusCode.TargetSearchFailed;
                        rnt.status = SendResult.STATUS_SEARCH_FAIL;

                        totalRecevierUsedCnt++;
                        UpdateRecevierUsed(totalRecevierUsedCnt);
                        break;
                    case "13":
                        targetsList[targetIdx].isUsed = true;
                        targetsList[targetIdx].statusCode = StatusCode.TargetNotOnline;
                        rnt.status = SendResult.STATUS_NOT_ONLINE;

                        totalRecevierUsedCnt++;
                        UpdateRecevierUsed(totalRecevierUsedCnt);
                        break;
                    case "22":
                        nReceive++;
                        if (nReceive >= everyReceive)
                        {
                            if (totalImgs > 0)
                            {
                                imageIdx = (++imageIdx) % totalImgs;
                                nReceive = 0;
                                execAndWait("cmd.exe", "\"" + getAdbPath() + "\" -s " + currentDevice + " push \"" + getCurrentImg() + "\" /storage/sdcard/wandoujia/image/a.jpg");
                            }
                            
                        }
                        break;
                    case "23":
                        usersList[userIdx].isUsed = true;
                        rnt.status = SendResult.STATUS_LOGIN_SUC;
                        rnt.target = errMsg;
                        totalSendUsedCnt++;
                        UpdateUsedSender(totalSendUsedCnt);
                        break;
                    case "41":
                        usersList[userIdx].isUsed = true;
                        rnt.status = errMsg;
                        break;
                    case "1000":

                        break;
                }
                
                if (first.Groups[1].Value == "22")
                {
                    return;
                }

                OutMsg(statusBox, rnt.userName + "->" + rnt.target + " " + rnt.status, Color.Green);
                resultList.Add(rnt);
            }

            isReceiveMsg = false;
            
        }

        private void UpdateRecevierUsed(int totalRecevierUsedCnt)
        {
            this.Invoke(new EventHandler(delegate
            {
                labelUsedRecevier.Text = string.Format("已使用数：{0}", totalRecevierUsedCnt);
                labelTotalReceiver.Text = string.Format("总数：{0}  可使用数：{1}", targetsList.Count, targetsList.Count - totalRecevierUsedCnt);
            }));
        }

        private void UpdateUsedSender(int totalSendUsedCnt)
        {
            this.Invoke(new EventHandler(delegate
            {
                labelUsedSender.Text = string.Format("已使用数：{0}", totalSendUsedCnt);
                labelTotalSendCnt.Text = string.Format("总数：{0}  可使用数：{1}", usersList.Count, usersList.Count - totalSendUsedCnt);
            }));
        }

        private string getErrMsg(int targetIdx)
        {
            string rtnStr = "其他错误";
            if (targetIdx > 0)
            {
                rtnStr = "";
            }
            switch (targetIdx)
            {
                case -101:
                    rtnStr = "淘宝网没有这个账号";
                    break;
                case -102:
                    rtnStr = "您的密码错误！";
                    break;
                case -103:
                    rtnStr = "非常抱歉，您的账号限制登录";
                    break;
                case -105:
                    rtnStr = "账户受保护";
                    break;
                case -201:
                    rtnStr = "当日已经无发送数量";
                    break;
                case -202:
                    rtnStr = "";
                    break;
                default:
                    break;
            }

            return rtnStr;
        }

        private void invokeTimer()
        {
            this.Invoke(new EventHandler(delegate
            {
                timer1.Enabled = true;
                timer1.Interval = 1000*40;
                timer1.Stop();
                timer1.Start();
               
            }));
        }
        bool isProcessEnd = false;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否确定退出程序？", "警告", MessageBoxButtons.YesNo))
            {
                e.Cancel = true;
                return;
            }
            try
            {
                if (process != null &&
    !process.HasExited)
                {
                    if (!isProcessEnd && (DialogResult.OK == MessageBox.Show("正在发送消息，是否退出？")))
                    {
                        stopSend();
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            saveConfig();
            dump(userBackupFile, true);
            dump(targetBackupFile, false);

            if (logWrite != null)
            {
                 logWrite.Flush();
                 logWrite.Close();
            }

        }

        private void saveConfig()
        {
            string sendMode = getSendMode();
            OperateIniFile.WriteIniData("SEND", "ImageCount", Configurations.getInstance().imageSendCnt.ToString(), configFile);
            OperateIniFile.WriteIniData("SEND", "AutoExport", Configurations.getInstance().isAutoExportAccount.ToString(), configFile);
            OperateIniFile.WriteIniData("QUERY", "QueryCount", Configurations.getInstance().checkGap.ToString(), configFile);
            OperateIniFile.WriteIniData("SEND", "SendOnline", chSendOnline.Checked.ToString(), configFile);
            OperateIniFile.WriteIniData("SEND", "SendSmartMode", ceStartSmartSendMode.Checked.ToString(), configFile);
            OperateIniFile.WriteIniData("SEND", "ImageCheckCnt", tbImageCheck.Text.Trim(), configFile);

            OperateIniFile.WriteIniData("SEND", "IsLoopAccount", chLoopAccountsNum.Checked.ToString(), configFile);
            OperateIniFile.WriteIniData("SEND", "LoopNums", (cbLoopsNum.SelectedIndex + 1).ToString(), configFile);

            OperateIniFile.WriteIniData("SEND", "SendMode", sendMode, configFile);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stopBtn.Enabled = false;
            stopSend();
            
        }

        private void stopSend()
        {
            this.Invoke(new EventHandler(delegate{
                OutMsg(statusBox, "正在关闭....\r\n", Color.Red);
                pushMsgToPhone();
                while (isBegin)
                {
                    Application.DoEvents();
                
                }
                isBegin = false;
                try
                {
                    if (process != null && !process.HasExited)
                    {
                        process.StandardInput.Close();
                        process.Kill();
                        process.Close();
                    }
                }
                catch (System.Exception ex)
                {
            	
                }
                isProcessEnd = true;
                OutMsg(statusBox, "已经关闭\r\n", Color.Red);
                }));
            }

            private void forceStop()
            {
                this.Invoke(new EventHandler(delegate{
                sendBtn.Enabled = true;
                stopBtn.Enabled = false;
                isBegin = false;
                 try
                {
                    if (process != null && !process.HasExited)
                    {
                        process.StandardInput.Close();
                    }
                    isProcessEnd = true;
                    }
               catch (System.Exception ex)
               {
                    	
               }
                OutMsg(statusBox, "强制关闭\r\n", Color.Red);
            }));
        }

        // 是否使用查询账号
        bool isQueryAccount = true;

        // 指示是否还有账号,或店铺可以继续发送
        // 接受模拟器传送的数值判断
        bool needContinueSend = true;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (cbDevices.SelectedItem == null)
            {
                MessageBox.Show("请选择模拟器设备");
                return;
            }

            currentDevice = cbDevices.SelectedItem.ToString();
            if (isFisrtTimeClick)
            {
                if (currentDevice==null
                    || currentDevice=="")
                {
                    currentDevice = "emulator-5554";
                }
               // parserTarget();
               //  paserUser();
                isFisrtTimeClick = false;
            }
            if (totalSendCnt <= 0)
            {
                MessageBox.Show("请添加发送小号账号!");
                return;
            }
            if (totalReceviorCnt <= 0)
            {
                MessageBox.Show("请添加接受消息账号!");
                return;
            }
            if (!imageAccountAvaiable)
            {
                MessageBox.Show("尚未设置图片验证码,请设置图片验证账号");
                return;
            }
            
            
            loopNums = 1;
            if (chLoopAccountsNum.Checked)
            {
                loopNums = cbLoopsNum.SelectedIndex+1;
            }

            stopBtn.Enabled = true;
            sendBtn.Enabled = false;
            Thread th = new Thread(new ThreadStart(BeginSend));
            th.Start();
        }
        private void BeginSend()
        {
            writeMsg();
            getAdbPath();
            isStop = false;
                if (Configurations.getInstance().checkAccount == null
                   || Configurations.getInstance().checkAccount.Trim() == "")
                {
                    if (DialogResult.Cancel == MessageBox.Show("你没有设置查询账号，如果忽略请点击\"是\"，返回设置点击\"否\"", "提示", MessageBoxButtons.OKCancel))
                    {
                        OutMsg(statusBox, "请在 设置 中设置查询账号!", Color.Red);
                        return;
                    }
                    isQueryAccount = false;
                }
                init();
                totalRecevierUsedCnt = 0;
                totalSendUsedCnt = 0;
                // write msg.list
                isVirtualMobileActivity = true;
                isOnceOver = false;
                everyReceive = Convert.ToInt32(Configurations.getInstance().imageSendCnt);

                isVirtualMobileActivity = isDevicesDetached();
                // process.WaitForExit();
                int cnt = 1;
                while (!isVirtualMobileActivity && cnt > 0)
                {
                    execAndWait("cmd.exe", "\""+getAdbPath()+"\" kill-server");
                    execAndWait("cmd.exe", "\"" + getAdbPath() + "\" start-server");
                    isVirtualMobileActivity = isDevicesDetached();
                    cnt--;
                }
                if (!isVirtualMobileActivity)
                {
                    statusBox.AppendText("尝试连接虚拟机失败，请手动启动虚拟机!\r\n");
                    return;
                }
                if (isVirtualMobileActivity)
                {
                    String cmd = "\"" + getAdbPath() + "\" -s " + currentDevice + " push UIAutomation.jar data/local/tmp";
                    execAndWait("cmd.exe", cmd);

                    // 推送目标用户
                    execAndWait("cmd.exe", "\"" + getAdbPath() + "\" -s " + currentDevice + " push user.list.temp data/local/tmp/user.list");

                    // 推送发送用户
                    execAndWait("cmd.exe", "\"" + getAdbPath() + "\" -s " + currentDevice + " push target.list.temp data/local/tmp/target.list");

                    // 推送发送消息
                    execAndWait("cmd.exe", "\"" + getAdbPath() + "\" -s " + currentDevice + " push msg.list data/local/tmp");

                    // 如果图片存在,则推送图片
                    if (Configurations.getInstance().isSendImg)
                    {
                        execAndWait("cmd.exe", "\"" + getAdbPath() + "\" -s " + currentDevice + " push \"" + getCurrentImg() + "\" /storage/sdcardt/wandoujia/image/a.jpg");
                    }

                    process = exec("cmd.exe", "\"" + getAdbPath() + "\" -s " + currentDevice + " shell uiautomator runtest UIAutomation.jar -c com.Runner");

                    AttachConsole(process.Id);
                    HandlerRoutine handle = new HandlerRoutine(OnHandlerRoutine);
                    SetConsoleCtrlHandler(handle, true);

                    this.Invoke(new EventHandler(delegate
                    {
                        stopBtn.Enabled = true;
                        sendBtn.Enabled = false;
                    }));
                }
        }

        private string getAdbPath()
        {
            return Application.StartupPath + "\\" + "adb\\adb.exe";
        }

        private string getCurrentImg()
        {
            string imgPath = startPath + "\\" + imageIdx + ".jpg";
           // OutMsg(statusBox, "当前图片位置：" + imgPath + "\r\n", Color.Black);
            return imgPath;
        }

        private bool isDevicesDetached()
        {
            return AdbOperation.getDevices().Count > 0;
        }

        private void init()
        {
            dumpTargets(Application.StartupPath + "/target.list.temp");
            dumpUsers(Application.StartupPath + "/user.list.temp");
            if (!isGreatThan2)
            {
                gridControl1.Refresh();
            }
            isGreatThan2 = true;
        }

        private void parserTarget()
        {
            targetsList.Clear();
            parserTarget("target.list");
        }
        private int parserTarget(string filepath,bool showConsole = false)
        {
            int cnt = 0;
            StreamReader reader = new StreamReader(filepath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                
                if (line.Trim().Equals(""))
                {
                    continue;
                }

                cnt++;
                User use = new User();
                use.name = line.Trim();
                if (!totalTargetsList.ContainsKey(use.name))
                {
                    totalTargetsList.Add(use.name, use);
                    targetsList.Add(use);
                    if (showConsole)
                    {
                        tbReceiveor.AppendText(use.name + "\r\n");
                    }
                }
            }
            reader.Close();
            return cnt;
        }
        private void paserUser()
        {
            usersList.Clear();
            paserUser("user.list");

        }
        private int paserUser(string filePath,bool showConsole = false)
        {
            int num = 0;
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                
                string[] namePwd = line.Split(new string[]{" ","|"},StringSplitOptions.None);
                if (!(namePwd.Length == 2 || namePwd.Length == 3))
                {
                    continue;
                }
                num++;
                User user = new User();
                user.name = namePwd[0];
                user.pwd = namePwd[1];
                if (namePwd.Length == 3)
                {
                    try
                    {
                        user.sendCnt = Convert.ToInt32(namePwd[2]);

                    }
                    catch
                    {
                       
                    }
                }

                if (!totalUsersList.ContainsKey(user.name))
                {
                    totalUsersList.Add(user.name, user);
                    usersList.Add(user);
                    if (showConsole)
                    {
                        tbSendUsers.AppendText(user.name + " " + user.pwd + "\r\n");
                    }
                }
                
            }
            reader.Close();
            return num;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            stopBtn.Enabled = false;
            isStop = true;

            InvokeStopTimer();
            stopSend();

        }
        private void InvokeStopTimer()
        {
            this.Invoke(new EventHandler(delegate
            {
                OutMsg(statusBox, "唤醒终止计时器", Color.Red);
                timer2.Interval = 1000 * 60;
                timer2.Enabled = true;
                timer2.Start();               
            }));


        }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            gridControl1.MainView.RefreshData();
            gridControl1.Refresh();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            //string date = DateTime.Now.ToFileTime().ToString();
            string path = Application.StartupPath + "/" + DIR_UNUSE_NAME;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += "/未使用小号.txt"; 
            if (dumpUsers(path) > 0)
            {
                MessageBox.Show("账号 导出成功，文件位置：" + path);
            }
            else
            {
                File.Delete(path);
                MessageBox.Show("没有剩余账号可以导出");
            }
        }

        private int dumpUsers(string path)
        {
            if (isOnceOver)
            {
                RecoverUnused();
            }
            List<User> tempUser = new List<User>(usersList.Count);
            StreamWriter writer = new StreamWriter(path);
            int outputCnt = 0;
            for (int i = 0; i < usersList.Count; i++)
            {
                if (!usersList[i].isUsed)
                {

                    outputCnt++;
                    writer.WriteLine(usersList[i].ToString());
                    tempUser.Add(usersList[i]);
                }
            }
            writer.Flush();
            writer.Close();
            usersList = tempUser;
            return outputCnt;
        }

        private void RecoverUnused()
        {
            if (totalUsersList.Count == 0)
            {
                parse(userBackupFile, true);
            }
            usersList.Clear();
            tbSendUsers.Text = "";
            usersList = new List<User>(totalUsersList.Count);
            foreach (string name in totalUsersList.Keys)
            {
                User u = totalUsersList[name];
                if (u.statusCode == StatusCode.UserLoginFailed)
                {
                    continue;
                }
                u.isUsed = false;
                usersList.Add(u);
                tbSendUsers.AppendText(u.name + " " + u.pwd+"\r\n");
            }
            totalSendCnt = usersList.Count;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToFileTime().ToString();
            string path = Application.StartupPath + "/"+DIR_UNUSE_NAME;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += "/未使用店铺.txt";

            if (dumpTargets(path) > 0)
            {
                MessageBox.Show("店铺 导出成功，文件位置：" + path);
            }
            else
            {
                File.Delete(path);
                MessageBox.Show("没有剩余店铺可以导出");
            }
        }
        private string ExportReceiveByCondition(string condition)
        {
           // "所有 已经使用 没有使用"
            string dir = Application.StartupPath + "\\" + DIR_UNUSE_NAME;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = dir + "\\" + condition + "_" + DateTime.Now.ToLongTimeString().Replace(":", "_") + ".txt";
            StreamWriter writer = new StreamWriter(path);

            List<User> tempTargets = new List<User>(targetsList.Count);
            for (int i = 0; i < targetsList.Count; i++)
            {
                if (condition.Contains("已经使用") &&
                   targetsList[i].isUsed == true)
                {
                    tempTargets.Add(targetsList[i]);
                    writer.WriteLine(targetsList[i].name);
                }
                else if (condition.Contains("没有使用")&&
                    targetsList[i].isUsed == false)
                {
                    tempTargets.Add(targetsList[i]);
                    writer.WriteLine(targetsList[i].name);
                }
                else if (condition.Contains("没有在线") && 
                         targetsList[i].statusCode == StatusCode.TargetNotOnline)
                {
                    tempTargets.Add(targetsList[i]);
                    writer.WriteLine(targetsList[i].name);
                }
                else if (condition.Contains("搜索失败")&&
                    targetsList[i].statusCode == StatusCode.TargetSearchFailed)
                {
                    tempTargets.Add(targetsList[i]);
                    writer.WriteLine(targetsList[i].name);
                }
            }
            writer.Flush();
            writer.Close();
            return path;
        }
        private int dumpTargets(string path)
        {
          //  OutMsg(statusBox, path, Color.Black);
            int gapCount = Configurations.getInstance().checkGap;
            string queryAccount = Configurations.getInstance().checkAccount;
            StreamWriter writer = new StreamWriter(path);
            int outputCnt = 1;
            User u = new User();
            u.name = queryAccount;

            List<User> tempTargets = new List<User>(targetsList.Count);
            for (int i = 0; i < targetsList.Count; i++)
            {
                if (targetsList[i].isUsed)
                {
                    continue;
                }
                if (targetsList[i].name == queryAccount)
                {
                    continue;
                }

                    tempTargets.Add(targetsList[i]);
                   
                    if (isQueryAccount && (outputCnt%gapCount)==0)
                    {
                        outputCnt++;
                        tempTargets.Add(u);
                        writer.WriteLine(queryAccount);
                    }
                    writer.WriteLine(targetsList[i].name);
                    outputCnt++;
                
            }
            writer.Flush();
            writer.Close();
            targetsList = tempTargets;
            return outputCnt;
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
              int hand = e.RowHandle;
            if (hand < 0) return;
            DataRow dr = this.gridView1.GetDataRow(hand);
            if (dr== null) return;

            MessageBox.Show(dr[2].ToString() + "##########22");
            MessageBox.Show(dr[3].ToString() + "##########33");

            if (dr[2].ToString() == "发送成功")
            {
                e.Appearance.ForeColor = Color.Black;// 改变行字体颜色
                e.Appearance.BackColor = Color.Green;// 改变行背景颜色
                e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色

            }
        }

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.TabPages[xtraTabControl1.SelectedTabPageIndex].Tag == null)
            {
                return;
            }
            switch(xtraTabControl1.TabPages[xtraTabControl1.SelectedTabPageIndex].Tag.ToString())
            {
                case "SENDRESULT":
                    gridControl1.MainView.RefreshData();
                    break;
                case "IMAGECONFIG":
                    InitImageTab();
                    break;
                case "TEXTCONFIG":
                    gridControl2.MainView.RefreshData();
                    break;
            }
        }

        private void InitTextTab()
        {
            throw new NotImplementedException();
        }
        List<string> fontList = new List<string>();
        private void InitImageTab()
        {
            // 获取系统字体
            InstalledFontCollection MyFont = new InstalledFontCollection();
            FontFamily[] MyFontFamilies = MyFont.Families;
            fontList.Clear();
            int Count = MyFontFamilies.Length;
            for (int i = 0; i < Count; i++)
            {
                string FontName = MyFontFamilies[i].Name;
                if (FontName[0] > 127)
                {
                    fontList.Add(FontName);
                    cbFont.Items.Add(FontName);
                }

            }
            cbFont.SelectedIndex = 0;
            readToolsConfig();
        }

        private void InitToolTab()
        {
            
           
        }


      
        private void exportBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void btnImport_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
 
        /***
         *  bkOptions 1 表示使用背景
         *            2 表示不使用背景,且纯色渲染
         *            3 表示不使用背景,随机渲染
         */
        private int bkOptions = 1;
        string imagePath = Application.StartupPath + "/生成图片";
        string imageUsePath = Application.StartupPath + "/images";

        Watermark wholeWatermark = new Watermark();

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            string bkImage = null;
            if (bkOptions == 1)
            {
                bkImage = btnImagePath.Text;
            }
            if (!File.Exists(btnImagePath.Text))
            {
                OutMsg(statusBox, "背景图片不存在，请检查其位置", Color.Red);
                return;
            }
            int imageCnt = Convert.ToInt32(teImageCount.Text.Trim());

            int THREAD_MOD = 5;

            int threadCnt = imageCnt / THREAD_MOD;

            if (Directory.Exists(imagePath))
            {
                Directory.Delete(imagePath,true);
            }

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            initImageParamters();
            Thread[] allThread = new Thread[threadCnt];
            for (int i = 0; i < threadCnt; i++)
            {
                allThread[i] = new Thread(new ParameterizedThreadStart(generatgeImages));
                GImage imgs = new GImage();
                imgs.bkImage = bkImage;
                imgs.startInt = i * THREAD_MOD;
                imgs.endInt = (i + 1) * THREAD_MOD;
                allThread[i].Start(imgs);
            }
            Thread thread = null;
            if (imageCnt % THREAD_MOD != 0)
            {
                thread = new Thread(new ParameterizedThreadStart(generatgeImages));
                GImage imgs1 = new GImage();
                imgs1.bkImage = bkImage;
                imgs1.startInt = threadCnt * THREAD_MOD;
                imgs1.endInt = imageCnt;
                thread.Start(imgs1);
            }
            for (int i = 0; i < allThread.Length;i++ )
            {
                allThread[i].Join();
            }
            if (thread != null)
            {
                thread.Join();
            }

            dumpConfig();

            OutMsg(statusBox, "生成图片完成,查看:\r\n"+imagePath, Color.Green);
        }

        //初始化化图片参数
        private void initImageParamters()
        {
            wholeWatermark.FontFamily = cbFont.SelectedItem.ToString();
            wholeWatermark.FontSize = Convert.ToInt32(teFontSize.Text.Trim());
            wholeWatermark.Adaptable = false;
            wholeWatermark.Text = imageText.Text;
            wholeWatermark.Width = Convert.ToInt32(teImageWidth.Text);
            wholeWatermark.Height = Convert.ToInt32(teImageHeight.Text);

            wholeWatermark.BgColor = colorEdit1.Color;

            wholeWatermark._isRandomFontSize = chRandomFontSize.Checked;
            wholeWatermark._isRandomFont = chRandomFont.Checked;
            wholeWatermark.MaxLineHeight = Convert.ToInt32(teMaxLineHeight.Text.Trim());
            wholeWatermark.MinLineHeight = Convert.ToInt32(teMinLineHeight.Text.Trim());
            wholeWatermark.FontFamilyList = fontList;
            wholeWatermark._imageBorder = Convert.ToInt32(teImageBorder.Text.Trim());
        }
        
        private void generatgeImages(object obj)
        {
            GImage imgs = (GImage)obj;
            for (int i = imgs.startInt; i < imgs.endInt; i++)
            {
                CreateImage(imgs.bkImage,imagePath+"/d" + i.ToString() + ".jpg");
            }
        }

        private void readToolsConfig()
        {
            imageText.Text = OperateIniFile.ReadIniData("IMAGE", "Content","", configFile);
            teFontSize.Text = OperateIniFile.ReadIniData("IMAGE", "FontSize", "20", configFile);
          //  chIsLoopBKImg.Checked = Convert.ToBoolean(OperateIniFile.ReadIniData("IMAGE", "UseBackgourdImage", "false", configFile));
            teImageWidth.Text = OperateIniFile.ReadIniData("IMAGE", "ImageWidth", "439", configFile);
            teImageHeight.Text = OperateIniFile.ReadIniData("IMAGE", "ImageHeight", "454", configFile);
            teImageCount.Text = OperateIniFile.ReadIniData("IMAGE", "ImageCount", "50", configFile);

            teMaxLineHeight.Text = OperateIniFile.ReadIniData("IMAGE", "MaxLineHeight","40", configFile);
            teMinLineHeight.Text = OperateIniFile.ReadIniData("IMAGE", "MinLineHeight","20", configFile);
            chRandomFontSize.Checked = OperateIniFile.ReadIniData("IMAGE", "IsRandomFontSize","true", configFile).ToLower().Equals("true");
            chRandomFont.Checked = OperateIniFile.ReadIniData("IMAGE", "IsRandomFont", "true", configFile).ToLower().Equals("true");
            btnImagePath.Text = OperateIniFile.ReadIniData("IMAGE", "ImagePath", Application.StartupPath + "/background/b0.jpg", configFile);
            teImageBorder.Text = OperateIniFile.ReadIniData("IMAGE", "ImageBorder","20", configFile);

        }
        private void dumpConfig()
        {
            OperateIniFile.WriteIniData("IMAGE", "Content", imageText.Text, configFile);
            OperateIniFile.WriteIniData("IMAGE", "FontSize", teFontSize.Text, configFile);
          //  OperateIniFile.WriteIniData("IMAGE", "UseBackgourdImage", chIsLoopBKImg.Checked.ToString(), configFile);
            OperateIniFile.WriteIniData("IMAGE", "ImageWidth", teImageWidth.Text.Trim(), configFile);
            OperateIniFile.WriteIniData("IMAGE", "ImageHeight", teImageHeight.Text.Trim(), configFile);
            OperateIniFile.WriteIniData("IMAGE", "ImageCount", teImageCount.Text.Trim(), configFile);
            OperateIniFile.WriteIniData("IMAGE", "MaxLineHeight", teMaxLineHeight.Text.Trim(), configFile);
            OperateIniFile.WriteIniData("IMAGE", "MinLineHeight", teMinLineHeight.Text.Trim(), configFile);
            OperateIniFile.WriteIniData("IMAGE", "IsRandomFontSize", chRandomFontSize.Checked.ToString(), configFile);
            OperateIniFile.WriteIniData("IMAGE", "IsRandomFont", chRandomFont.Checked.ToString(), configFile);
            OperateIniFile.WriteIniData("IMAGE", "ImagePath", btnImagePath.Text, configFile);
            OperateIniFile.WriteIniData("IMAGE", "ImageBorder", teImageBorder.Text.Trim(), configFile);
        }

        private void CreateImage(string bkImagePath, string targetPath)
        {

                Watermark watermark = new Watermark();
                if (bkImagePath != null)
                {
                    watermark.BackgroundImage = bkImagePath;
                }
                watermark.FontFamily = wholeWatermark.FontFamily;
                watermark.FontSize = wholeWatermark.FontSize;
                watermark.Adaptable = false;
                watermark.ResultImage = targetPath;
                watermark.Text = wholeWatermark.Text;// imageText.Text;
                watermark.Width = wholeWatermark.Width;//.ToInt32(teImageWidth.Text);
                watermark.Height = wholeWatermark.Height;//Convert.ToInt32(teImageHeight.Text);

                watermark.BgColor = colorEdit1.Color;

                watermark._isRandomFontSize = wholeWatermark._isRandomFontSize;// chRandomFontSize.Checked;
                watermark._isRandomFont = wholeWatermark._isRandomFont; //chRandomFont.Checked;
                watermark.MaxLineHeight = wholeWatermark.MaxLineHeight;// Convert.ToInt32(teMaxLineHeight.Text.Trim());
                watermark.MinLineHeight = wholeWatermark.MinLineHeight;// Convert.ToInt32(teMinLineHeight.Text.Trim());
                watermark.FontFamilyList = fontList;
                watermark._imageBorder = wholeWatermark._imageBorder;// Convert.ToInt32(teImageBorder.Text.Trim());
                watermark.Create();
        }

        private void cbBkOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            bkOptions = cbBkOptions.SelectedIndex + 1;
        }

        private void btnReorder_Click(object sender, EventArgs e)
        {
            Directory.Delete(imageUsePath, true);
            Directory.CreateDirectory(imageUsePath);

            string[] allImages = Directory.GetFiles(imagePath); 
            for(int i = 0; i < allImages.Length; i++)
            {
                string imgPath = allImages[i];
                string targetPath = imgPath.Substring(0,imgPath.LastIndexOf("\\"));
                if (chCopyToImages.Checked)
                {
                    targetPath = imageUsePath;
                }
                string newName = targetPath + "/" + i.ToString() + ".jpg";
                File.Move(imgPath, newName);
            }

            OutMsg(statusBox, "图片准备就绪，一共 " + allImages.Length + " 张",Color.Green);
//             if (chCopyToImages.Checked)
//             {
//                 Directory.Delete(imageUsePath, true);
//                 Directory.CreateDirectory(imageUsePath);
// 
//                 ProgressFrm frm = new ProgressFrm();
//                 CopyDirectory copy = new CopyDirectory(frm);
//                 frm.Show();
//                 copy.copyDirectory(imagePath, imageUsePath, true);
//             }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
         
        private void startApp()
        {
            string devices = execAndWait("cmd.exe", "\"" + getAdbPath() + "\" -s " + currentDevice + " shell monkey -p com.alibaba.mobileim -c android.intent.category.LAUNCHER 1");
            Thread.Sleep(5000);
        }

        private void killApp()
        {
            OutMsg(statusBox, "kill app....", Color.Red);
            string devices = execAndWait("cmd.exe", "\"" + getAdbPath() + "\" -s " + currentDevice + " shell am force-stop com.alibaba.mobileim");
            devices = execAndWait("cmd.exe", "\"" + getAdbPath() + "\" -s " + currentDevice + " shell pm clear com.alibaba.mobileim");
            Thread.Sleep(2000);
        }

        bool isReceiveMsg = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (cbAutoRestart.Checked &&!isReceiveMsg && needContinueSend)
            {
                Thread th = new Thread(new ThreadStart(ReSend));
                th.Start();
            }
        }

        private void ReSend()
        {
            usersRemainder = 0;
            targetsRemainder = 0;
            forceStop();
            killApp();
            startApp();
            BeginSend();
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 5000;
            //   timer1.Stop();
            timer1.Start();
            statusBox.AppendText("##########checking###########\r\n");
        }

        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            gridControl1.MainView.RefreshData();
            gridControl1.Refresh();
            exportSucAccount();
        }
        private void exportSucAccount()
        {
            int row = resultList.Count;
            Dictionary<string, User> tempUserList = new Dictionary<string, User>();
            for (int i = 0; i < row; i++ )
            {
                SendResult dt = resultList[i];
                if (dt.status == SendResult.STATUS_LOGIN_SUC)
                {
                    if (!tempUserList.ContainsKey(dt.userName.Trim()))
                    {
                        User usr = totalUsersList[dt.userName];
                        tempUserList.Add(usr.name.Trim(),usr);
                    }
                }
            }

            //导出
            string dir = Application.StartupPath + "\\" + DIR_LOGIN_NAME;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = dir + "\\登录成功_" + DateTime.Now.ToLongTimeString().Replace(":", "_")+ ".txt";
            StreamWriter writer = new StreamWriter(path);
            Dictionary<string, User>.Enumerator itor = tempUserList.GetEnumerator();

            while (itor.MoveNext())
            {
                User u = (User)itor.Current.Value;
                writer.WriteLine(u.ToString());
            }
            writer.Flush();
            writer.Close();

            OutMsg(statusBox, "登录成功记录已经导出成功!请查看" + path, Color.Blue);
        }

        private void buttonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            
        }

        private void btnFailedAccount_Click(object sender, EventArgs e)
        {
            gridControl1.MainView.RefreshData();
            gridControl1.Refresh();
            exportFailedAccount();
        }
        private void exportFailedAccount()
        {

            int row = resultList.Count;
            Dictionary<string, User> tempUserList = new Dictionary<string, User>();
            for (int i = 0; i < row; i++)
            {
                SendResult dt = resultList[i];
                if (dt.status == SendResult.STATUS_LOGIN_FAIL)
                {
                    User usr = totalUsersList[dt.userName];
                    usr.sendStatus = dt.target;
                    if (usr != null && !tempUserList.ContainsKey(usr.name.Trim()))
                    {
                       tempUserList.Add(usr.name.Trim(),usr);
                    }
                }
            }

            //导出
            string dir = Application.StartupPath + "\\" + DIR_LOGIN_NAME;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = dir + "\\登录失败_" + DateTime.Now.ToLongTimeString().Replace(":", "_") + ".txt";
            StreamWriter writer = new StreamWriter(path);
            Dictionary<string, User>.Enumerator itor = tempUserList.GetEnumerator();

            while (itor.MoveNext())
            {
                User u = (User)itor.Current.Value;
                writer.WriteLine(u.name + " " + u.pwd+" " + u.sendStatus);
            }
            writer.Flush();
            writer.Close();

            OutMsg(statusBox,"登录失败记录已经导出成功!请查看" + path,Color.Blue);
        }

        private void textEdit1_Leave(object sender, EventArgs e)
        {

        }

        

        private void btnAddLevelItem_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonEdit1_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                btnImagePath.Text = openFileDialog1.FileName;
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(refreshDevices));
            th.Start();
        }

        private void refreshDevices()
        {
            AdbOperation.RefreshDevices();

            List<string> devices = AdbOperation.getDevices();

            this.Invoke(new EventHandler(delegate
            {

                cbDevices.Items.Clear();


                foreach (string device in devices)
                {
                    cbDevices.Items.Add(device);
                }

                if (devices.Count > 0)
                {
                    cbDevices.SelectedIndex = 0;
                    OutMsg(statusBox, "刷新设备 成功！", Color.Green);
                }
                else
                {
                    OutMsg(statusBox, "刷新设备 失败！", Color.Red);
                }

            }));

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            if (sendBtn.Enabled == true)
            {
                return;
            }
            OutMsg(statusBox, (timer2.Interval / 1000) + "秒钟后没停止,强制停止....",Color.Red);
            killApp();
            sendBtn.Enabled = true;
            stopBtn.Enabled = false;
            if (process != null && !process.HasExited)
            {
                process.StandardInput.Close();
                process.Kill();
                process.Close();
            }
            isBegin = false;
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {



        }

        private void buttonEdit2_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void buttonEdit1_ButtonClick_2(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void beStartAppLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void buttonEdit1_ButtonClick_3(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void beFile2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
           
        }

        private void beSelectSenderFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog select = BatchSend.Tools.Utilities.CreateOpenFileDialog("选择小号文件");
            if (select.ShowDialog() == DialogResult.OK)
            {
                beSelectSenderFile.Text = select.FileName;
                int curNum = paserUser(beSelectSenderFile.Text, true);
                totalSendCnt += curNum;

                labelTotalSendCnt.Text = string.Format("总数：{0}  可使用数：{1}", totalSendCnt, usersList.Count);

                this.Focus();
            }


        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            beSelectSenderFile.Text = "";
            clearAllSenderUser();
        }

        private void clearAllSenderUser()
        {
            tbSendUsers.Clear();
            usersList.Clear();
            totalUsersList.Clear();
            labelTotalSendCnt.Text = string.Format("总数：{0}  可使用数：{1}", 0, 0);
            totalSendCnt = 0;
            totalSendUsedCnt = 0;
            labelUsedSender.Text = string.Format("已使用数：{0}", 0);

        }

        private void beSelectRecevier_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog select = BatchSend.Tools.Utilities.CreateOpenFileDialog("选择接收消息账号文件");

            if (select.ShowDialog() == DialogResult.OK)
            {
                beSelectRecevier.Text = select.FileName;
                int curNum = parserTarget(beSelectRecevier.Text, true);
                totalReceviorCnt += curNum;

                labelTotalReceiver.Text = string.Format("总数：{0}  可使用数：{1}", totalReceviorCnt, targetsList.Count);

                this.Focus();
            }


        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            beSelectRecevier.Text = "";
            clearAllReceiver();
        }

        private void clearAllReceiver()
        {
            tbReceiveor.Clear();
            targetsList.Clear();
            totalTargetsList.Clear();
            labelTotalReceiver.Text = string.Format("总数：{0}  可使用数：{1}", 0, 0);
            totalReceviorCnt = 0;
        }


        private bool imageAccountAvaiable = false; // 图片账号是否可用

        private void checkImageAccount()
        {
            Dictionary<object, object> param = new Dictionary<object, object>
                    {        
                        {"username",tbImageCheckAccount.Text},
                        {"password",tbImageCheckPwd.Text}
                    };
            Thread t = new Thread(new ThreadStart(delegate
            {
                //提交服务器
                string httpResult = RuoKuaiHttp.Post("http://api.ruokuai.com/info.xml", param);
                imageAccountAvaiable = false;
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(httpResult);
                }
                catch
                {
 
                }

                XmlNode scoreNode = xmlDoc.SelectSingleNode("Root/Score");
                XmlNode historyScoreNode = xmlDoc.SelectSingleNode("Root/HistoryScore");
                XmlNode totalTopicNode = xmlDoc.SelectSingleNode("Root/TotalTopic");

                XmlNode errorNode = xmlDoc.SelectSingleNode("Root/Error");

                if (scoreNode != null && historyScoreNode != null && totalTopicNode != null)
                {
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        string msgContent = string.Format("剩余快豆：{0}\r\n历史快豆：{1}\r\n答题总数：{2}\r\n", scoreNode.InnerText, historyScoreNode.InnerText, totalTopicNode.InnerText);
                        Configurations.getInstance().imageAccount = tbImageCheckAccount.Text.Trim();
                        Configurations.getInstance().imagePwd = tbImageCheckPwd.Text.Trim();
                        Configurations.getInstance().Save();
                        imageAccountAvaiable = true;
                    }));
                }
                else if (errorNode != null)
                {
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        string msgContent = string.Format("错误：{0}", errorNode.InnerText);
                    }));
                }
                else
                {
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        string msgContent = string.Format("未知问题");
                    }));
                }

                btnLoginImageCheck.Enabled = true;
            }));
            t.IsBackground = true;
            t.Start();
        }
        private void simpleButton16_Click(object sender, EventArgs e)
        {
            btnLoginImageCheck.Enabled = false;
            if (tbImageCheckAccount.Text==""||tbImageCheckPwd.Text=="")
            {
                MessageBox.Show("请输入账号或密码");
                return;
            }
            Dictionary<object, object> param = new Dictionary<object, object>
                    {        
                        {"username",tbImageCheckAccount.Text},
                        {"password",tbImageCheckPwd.Text}
                    };
            Thread t = new Thread(new ThreadStart(delegate
            {
                imageAccountAvaiable = false;
                //提交服务器
                string httpResult = RuoKuaiHttp.Post("http://api.ruokuai.com/info.xml", param);
               
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(httpResult);
                }
                catch
                {
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        MessageBox.Show("账号或密码错误");
                        
                    }));
                }


                XmlNode scoreNode = xmlDoc.SelectSingleNode("Root/Score");
                XmlNode historyScoreNode = xmlDoc.SelectSingleNode("Root/HistoryScore");
                XmlNode totalTopicNode = xmlDoc.SelectSingleNode("Root/TotalTopic");

                XmlNode errorNode = xmlDoc.SelectSingleNode("Root/Error");

                if (scoreNode != null && historyScoreNode != null && totalTopicNode != null)
                {
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        string msgContent = string.Format("剩余快豆：{0}\r\n历史快豆：{1}\r\n答题总数：{2}\r\n", scoreNode.InnerText, historyScoreNode.InnerText, totalTopicNode.InnerText);
                        MessageBox.Show(msgContent, "图片打码信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Configurations.getInstance().imageAccount = tbImageCheckAccount.Text.Trim();
                        Configurations.getInstance().imagePwd = tbImageCheckPwd.Text.Trim();
                        Configurations.getInstance().Save();
                        imageAccountAvaiable = true;
                    }));
                }
                else if (errorNode != null)
                {
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        string msgContent = string.Format("错误：{0}", errorNode.InnerText);
                        MessageBox.Show(msgContent, "图片打码信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
                else
                {
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        string msgContent = string.Format("未知问题");
                        MessageBox.Show(msgContent, "图片打码信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }));
                }

                btnLoginImageCheck.Enabled = true;
            }));
            t.IsBackground = true;
            t.Start();
        }

        private void 其他ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OthersConfig configFrm = new OthersConfig();
            configFrm.ShowDialog();
        }

        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Aboutus aboutFrm = new Aboutus();
            aboutFrm.ShowDialog();
        }

        private void 过滤重复号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BatchSend.Tools.FilterDuplicateAccount filterFrm = new BatchSend.Tools.FilterDuplicateAccount();
            filterFrm.Show();
        }

        private void 分隔号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BatchSend.Tools.SplitToolFrm splitFrm = new BatchSend.Tools.SplitToolFrm();
            splitFrm.Show();
        }

        private void 模拟器替换工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BatchSend.Tools.ReplaceSimulatorFrm replaceFrm = new BatchSend.Tools.ReplaceSimulatorFrm();
            replaceFrm.Show();
        }

        private void 整理小号工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BatchSend.Tools.ExportAccountFrm exportFrm = new BatchSend.Tools.ExportAccountFrm();
            exportFrm.Show();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (chLoopAccountsNum.Checked)
            {
                cbLoopsNum.Enabled = true;
            }
            else
            {
                cbLoopsNum.Enabled = false;
            }
            if (isBegin)
            {
                curLoops = 1;
            }
            else
                curLoops = 0;
            
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            curLoops = 0;
            RecoverUnused();

            labelTotalSendCnt.Text = string.Format("总数：{0}  可使用数：{1}", totalUsersList.Count, usersList.Count);
        }

        private void btnExportSendUsers_Click(object sender, EventArgs e)
        {
            string condition = cbSendCondition.Items[cbSendCondition.SelectedIndex].ToString();
           string path = ExportUsersByCondition(condition);

            OutMsg(statusBox, condition + " 记录已经导出成功!请查看" + path, Color.Blue);

        }

        private string ExportUsersByCondition(string condition)
        {
            int row = resultList.Count;
            Dictionary<string, User> tempUserList = new Dictionary<string, User>();
            for (int i = 0; i < row; i++)
            {
                SendResult dt = resultList[i];
                if (dt.status == condition)//SendResult.STATUS_LOGIN_SUC)
                {
                    if (!tempUserList.ContainsKey(dt.userName.Trim()))
                    {
                        User usr = totalUsersList[dt.userName];
                        tempUserList.Add(usr.name.Trim(), usr);
                    }
                }
            }

            if (condition.Equals("所有") || condition.Contains(SendResult.STATUS_LOGIN_SUC))
            {
                //包含所有
                foreach (string name in totalUsersList.Keys)
                {
                    if (totalUsersList[name].statusCode == StatusCode.UserLoginSuc && !tempUserList.ContainsKey(name))
                    {
                        tempUserList.Add(name, totalUsersList[name]);
                    }
                }
            }

            //导出
            string dir = Application.StartupPath + "\\" + DIR_LOGIN_NAME;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = dir + "\\"+condition+"_" + DateTime.Now.ToLongTimeString().Replace(":", "_") + ".txt";
            StreamWriter writer = new StreamWriter(path);
            Dictionary<string, User>.Enumerator itor = tempUserList.GetEnumerator();

            while (itor.MoveNext())
            {
                User u = (User)itor.Current.Value;
                writer.WriteLine(u.ToString());
            }
            writer.Flush();
            writer.Close();

            return path;
        }

        private void btnExportRecevies_Click(object sender, EventArgs e)
        {
            string condition = cbReceiveCondition.Items[cbReceiveCondition.SelectedIndex].ToString();
            string path = ExportReceiveByCondition(condition);
            OutMsg(statusBox, condition + " 记录已经导出成功!请查看" + path, Color.Blue);
        }


        private void btnAddAds_Click(object sender, EventArgs e)
        {
            int rowIdx = -1;
            if (downHitInfo!=null)
            {
                rowIdx = downHitInfo.RowHandle;
            }
            if (rowIdx < 0)
            {
                Advertise adv = new Advertise();
                adv.TotalNum = Convert.ToInt32(tbTotalNum.Text.Trim());
                adv.CheckMsg = tbCheckMsg.Text;
                adv.MsgContent = sendMsgBox.Text;

                AdvertiseList.getInstance().myAdvs.Add(adv);
                gridControl2.MainView.RefreshData();

                AdvertiseList.getInstance().dump();
            }
            else
            {
                Advertise adv = AdvertiseList.getInstance().myAdvs[rowIdx];
                adv.TotalNum = Convert.ToInt32(tbTotalNum.Text.Trim());
                adv.CheckMsg = tbCheckMsg.Text;
                adv.MsgContent = sendMsgBox.Text;
                gridControl2.MainView.RefreshData();
                AdvertiseList.getInstance().dump();
            }
           
        }

        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            Advertise adv = AdvertiseList.getInstance().myAdvs[e.RowHandle];
            UpdateAdverties(adv);
        }

        private void UpdateAdverties(Advertise adv)
        {
            tbTotalNum.Text = adv.TotalNum.ToString();
            tbUsedNum.Text = adv.UsedNum.ToString();
            tbCheckMsg.Text = adv.CheckMsg;
            sendMsgBox.Text = adv.MsgContent;

        }

        private void btnDeleteAdverties_Click(object sender, EventArgs e)
        {
            int rowIdx = downHitInfo.RowHandle;
            if (rowIdx > 0)
            {
                AdvertiseList.getInstance().myAdvs.RemoveAt(rowIdx);                
                gridControl2.MainView.RefreshData();
                AdvertiseList.getInstance().dump();
            }
        }
        GridHitInfo downHitInfo;
        private void gridControl2_MouseClick(object sender, MouseEventArgs e)
        {
            GridView view = gridControl2.MainView as GridView;
            downHitInfo = null;
            downHitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                return;
            }
            sendMsgBox.AppendText(comboBox1.Items[comboBox1.SelectedIndex].ToString());
            comboBox1.SelectedIndex = 0;
        }

        private void cbAutoRestart_Click(object sender, EventArgs e)
        {
            
        }
        public void dump(string filePath,bool isUserList)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            if (isUserList)
            {
                bf.Serialize(fs, totalUsersList);
            }
            else
            {
                bf.Serialize(fs, totalTargetsList);
            }
            fs.Close();
        }
        public void parse(string filePath, bool isUserList)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            FileStream fs = new FileStream(filePath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            if (isUserList)
            {
                totalUsersList = bf.Deserialize(fs) as Dictionary<string, User>;
            }
            else
            {
                totalTargetsList = bf.Deserialize(fs) as Dictionary<string, User>;
            }
            fs.Close();
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            curLoops = 0;
            RecoverUnusedTarget();
            labelTotalReceiver.Text = string.Format("总数：{0}  可使用数：{1}", totalTargetsList.Count, targetsList.Count);
        }

        private void RecoverUnusedTarget()
        {
            if (totalTargetsList.Count == 0)
            {
                parse(targetBackupFile, false);
            }
            targetsList.Clear();
            tbReceiveor.Text = "";
            targetsList = new List<User>(totalTargetsList.Count);
            foreach (string name in totalTargetsList.Keys)
            {
                User u = totalTargetsList[name];
                if (u.isUsed)
                {
                    continue;
                }
                u.isUsed = false;
                targetsList.Add(u);
                tbReceiveor.AppendText(u.name + "\r\n");
            }
            totalReceviorCnt = targetsList.Count;
        }

        private void cbLoopsNum_SelectedValueChanged(object sender, EventArgs e)
        {
            loopNums = cbLoopsNum.SelectedIndex + 1;
            if (isBegin)
            {
                curLoops = 1;
            }
            else
                curLoops = 0;
        }



    }
    class GImage
    {
       public int startInt = 0;
       public int endInt = 0;
       public string bkImage = null;
    }
}
