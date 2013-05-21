using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace MCUpdateCheck
{
    public partial class GetUpdate : Form
    {

        private MainForm win;
        private string download;
        WebClient wc = new WebClient();

        public GetUpdate()
        {
            InitializeComponent();
            this.downloadingLable.Text = "Downloading 0%";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            wc.CancelAsync();
        }

        public void setParent(MainForm par)
        {
            this.win = par;
        }

        public void setDownload(string down)
        {
            this.download = down;
        }

        public void selfUpdateStart()
        {
            try
            {
                wc.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
                wc.Proxy = WebRequest.DefaultWebProxy;

                string filename = "new.exe";
                wc.DownloadFileAsync(new Uri(download), filename);
                //wc.DownloadFile(download, filename);

                wc.Dispose();
            }
            catch
            {
                throw new Exception("networkerror");
            }
        }

        public void minecraftUpdateStart()
        {
            try
            {
                wc.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
                wc.Proxy = WebRequest.DefaultWebProxy;

                string filename = "update.zip";
                wc.DownloadFileAsync(new Uri(download), filename);
                //wc.DownloadFile(download, filename);

                wc.Dispose();
            }
            catch
            {
                throw new Exception("networkerror");
            }
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Downloading Canceled!");
                if (download.Contains("MCAutoUpdate.exe"))
                {
                    this.win.minecraftCheck();
                }
                else
                {
                    this.Dispose();
                    this.Close();
                    Environment.Exit(0);
                }
            }
            else
            {
                MessageBox.Show("Downloading Finished!");
                if (download.Contains("MCUpdateCheck.exe"))
                {
                    string filename = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "killmyself.bat");
                    using (StreamWriter bat = File.CreateText(filename))
                    {
                        // 自升级
                        bat.WriteLine(string.Format(@"
@echo off
:selfkill
attrib -a -r -s -h ""{0}""
del ""{0}""
if exist ""{0}"" goto selfkill
copy /y ""new.exe"" ""{0}""
del ""new.exe""
""{0}""

del %0
", AppDomain.CurrentDomain.FriendlyName));
                    }

                    // 启动自删除批处理文件
                    ProcessStartInfo info = new ProcessStartInfo(filename);
                    info.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(info);

                    // 强制关闭当前进程
                    this.Dispose();
                    this.Close();
                    Environment.Exit(0);
                }
                else
                {
                    String path = System.Environment.GetEnvironmentVariable("appdata") + "\\.minecraft"; ;
                    path = path.Substring(0, path.Length - 18);
                    MCUpdateCheck.Update.updateFile("update.zip", path);
                    Properties.Settings.Default.minecraftVersion = MainForm.newMinecraftVersion;
                    Properties.Settings.Default.Save();
                    ProcessStartInfo info = new ProcessStartInfo("minecraft.exe");
                    Process.Start(info);
                    this.Dispose();
                    this.Close();
                    Environment.Exit(0);
                }
            }
        }

        void wc_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            this.win.Hide();
            downloadingProcess.Value = e.ProgressPercentage;
            this.downloadingLable.Text = "Downloading: " + downloadingProcess.Value.ToString() + "%";
        }

        private void GetUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            wc.CancelAsync();
        }
    }
}
