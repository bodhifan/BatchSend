using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Windows.Forms;

namespace BatchSend
{
    public class CopyDirectoryFactory
    {
        public static void CopyDirectory(string sourceDirectory, string destDirectory,string appLocation)
        {
            ProgressFrm frm = new ProgressFrm();
            CopyDirectory dire = new CopyDirectory(frm);
            frm.Show();
            Thread th = new Thread(new ThreadStart(delegate
            {
                frm.SetMainLabel("关闭模拟器.....");
                AdbOperation.KillBlueStacks();

                string tempPath = sourceDirectory + "\\Android";
                string tempTPath = destDirectory + "\\Android";

                dire.copyDirectory(tempPath, tempTPath,false);

                tempPath = sourceDirectory + "\\locales";
                tempTPath = destDirectory + "\\locales";
                dire.copyDirectory(tempPath, tempTPath, false);

                tempPath = sourceDirectory + "\\UserData";
                tempTPath = destDirectory + "\\UserData";
                dire.copyDirectory(tempPath, tempTPath, true);

                if (appLocation != null)
                {
                    AdbOperation.StartApp(appLocation);
                }

            }));
            th.Start();
        }
    }
    class CopyDirectory
    {
        private const short FILE_ATTRIBUTE_NORMAL = 0x80;
        private const short INVALID_HANDLE_VALUE = -1;
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint CREATE_NEW = 1;
        private const uint CREATE_ALWAYS = 2;
        private const uint OPEN_EXISTING = 3;
        private const uint FILE_FLAG_NO_BUFFERING = 0x20000000;
        private const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;
        private const uint FILE_FLAG_NORMAL = 0x00000080;
        private const uint FILE_SHARE_READ = 0x00000001;
        private const uint FILE_SHARE_WRITE = 0x00000002;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle CreateFile(string IpFileName, uint dwDesiredAccess,
            uint dwShareMode, IntPtr IpSecurityAttributes, uint dwCreationDisposition,
            uint dwFlagsAndAttributes, IntPtr hTemplateFile);
        private int _ThreadNum;
        private Thread[] CopyThread;
        private long ReadBufferSize = 1024 * 1024 * 32;
        public long TotalReadCount = 0;
        public long AverageCopySpeed;
        public int ProgressBarValue = 0;
        private DateTime start;
        private FileInfo SourceFileInfo;
        private string _DestFileName;
        private string _SourceFileName;
        private bool _IsUsingSystemBuff;
        public delegate void CopyFinished(string IsFinish);
        private bool[] isfirst;
        public event CopyFinished CopyF;
        private bool WaitingEnd = true;
        private DateTime WaitTime;
        private int ThreadExitCout = 0;
        private object ThreadLock = new object();
        private ProgressFrm progressFrm;

        public CopyDirectory(ProgressFrm frm)
        {
            progressFrm = frm;
        }
        public void copyDirectory(string sourceDirectory, string destDirectory,bool isRoot)
        {
            //判断源目录和目标目录是否存在，如果不存在，则创建一个目录
            if (!Directory.Exists(sourceDirectory))
            {
                Directory.CreateDirectory(sourceDirectory);
            }
            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            //拷贝文件
            copyFile(sourceDirectory, destDirectory);

            //拷贝子目录
            //获取所有子目录名称
            string[] directionName = Directory.GetDirectories(sourceDirectory);
            
            foreach (string directionPath in directionName)
            {
                //根据每个子目录名称生成对应的目标子目录名称
                string directionPathTemp = destDirectory + "\\" + directionPath.Substring(sourceDirectory.Length + 1);

                //递归下去
                copyDirectory(directionPath, directionPathTemp,false);
            }

            if (isRoot)
            {
                progressFrm.CloseMe();
            }
        }
        public void copyFile(string sourceDirectory, string destDirectory)
        {

            //获取所有文件名称
            string[] fileName = Directory.GetFiles(sourceDirectory);
            progressFrm.SetMainProgressMax(fileName.Length);
            int i=0;
            foreach (string filePath in fileName)
            {
                progressFrm.SetMainLabel("当前拷贝文件：" + filePath);
               // MessageBox.Show("当前拷贝文件：" + filePath);
                progressFrm.SetMainProgress(i++);
                //根据每个文件名称生成对应的目标文件名称
                string filePathTemp = destDirectory + "\\" + filePath.Substring(sourceDirectory.Length + 1);

                //若不存在，直接复制文件；若存在，覆盖复制
                CopyFile(filePath, filePathTemp);
            }
        }
        public void CopyFile(string source, string dest)
        {
            progressFrm.SetSubProgressMax(100);
            using (FileStream sourceStream = new FileStream(source, FileMode.Open))
            {
                byte[] buffer = new byte[64 * 1024]; // Change to suitable size after testing performance
                using (FileStream destStream = new FileStream(dest, FileMode.Create))
                {
                    int i;
                    while ((i = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        destStream.Write(buffer, 0, i);
                       // OnProgress(sourceStream.Position, sourceStream.Length);
                        int procgress = (int)(((double)sourceStream.Position / sourceStream.Length) * 100);

                        progressFrm.SetSubLabel("当前进度：（" + procgress.ToString() + "%)");
                        progressFrm.SetSubProgress(procgress);
                    }
                }
            }
        }
        public void copyFileStream(string sourceFile, string destFile)
        {
          //  progressFrm.SetSubProgressMax(100);
            bool useBuffer = false;
            SafeFileHandle fr = CreateFile(sourceFile, GENERIC_READ, FILE_SHARE_READ, IntPtr.Zero, OPEN_EXISTING, useBuffer ? 0 : FILE_FLAG_NO_BUFFERING, IntPtr.Zero);
            SafeFileHandle fw = CreateFile(destFile, GENERIC_WRITE, FILE_SHARE_READ, IntPtr.Zero, CREATE_ALWAYS, FILE_FLAG_NORMAL, IntPtr.Zero);

            int bufferSize = useBuffer ? 1024 * 1024 * 32 : 1024 * 1024 * 32;

            FileStream fsr = new FileStream(fr, FileAccess.Read,bufferSize,false);
            FileStream fsw = new FileStream(fw, FileAccess.Write,bufferSize,false);

            BinaryReader br = new BinaryReader(fsr);
            BinaryWriter bw = new BinaryWriter(fsw);

            byte[] buffer = new byte[bufferSize];
            Int64 len = fsr.Length;
            DateTime start = DateTime.Now;
            TimeSpan ts;
            while (fsr.Position < fsr.Length)
            {
                int readCount = br.Read(buffer, 0, bufferSize);
                bw.Write(buffer, 0, readCount);
                ts = DateTime.Now.Subtract(start);
                double speed = (double)fsr.Position / ts.TotalMilliseconds * 1000 / (1024 * 1024);
                double progress = (double)fsr.Position / len * 100;
           //     progressFrm.SetSubLabel("当前拷贝文件：" + sourceFile + " 速度：" + speed);
          //      progressFrm.SetSubProgress((int)progress);
            }
            br.Close();
            bw.Close();
        }
    }
}
