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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MCUpdateCheck
{
    public partial class MainForm : Form
    {
        private string currentMinecraftVersion;
        public static string newMinecraftVersion;
        bool isSelfUpdate = false;
        bool isMinecraftUpdate = false;
        string currentSelfVersion = "1.0.1.2";
        string selfDownload = string.Empty;
        string minecraftDownload = string.Empty;
        string updateCheckURL = "http://mclauncherw.sinaapp.com/MCLauncherW.xml";

        public MainForm()
        {
            InitializeComponent();
            refreshSettings();
            if (minecraftCheck())
            {
                selfUpdate();
            }
            else
            {
                MessageBox.Show("Can not found Minecraft.exe! Please download it from http://minecraft.net/", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        public void refreshSettings()
        {
            currentMinecraftVersion = Properties.Settings.Default.minecraftVersion;
        }

        public bool minecraftCheck()
        {
            return File.Exists("minecraft.exe");
        }

        public void checkUpdate()
        {
            string newSelfVersion = string.Empty;
            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(updateCheckURL);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);
                XmlNode list = xmlDoc.SelectSingleNode("Update");
                foreach (XmlNode node in list)
                {
                    if (node.Name == "Soft" && node.Attributes["Name"].Value.ToLower() == "MCUpdateCheck".ToLower())
                    {
                        foreach (XmlNode xml in node)
                        {
                            if (xml.Name == "Version")
                                newSelfVersion = xml.InnerText;
                            else
                                selfDownload = xml.InnerText;
                        }
                    }
                    if (node.Name == "Soft" && node.Attributes["Name"].Value.ToLower() == "Minecraft".ToLower())
                    {
                        foreach (XmlNode xml in node)
                        {
                            if (xml.Name == "Version")
                                newMinecraftVersion = xml.InnerText;
                            else
                                minecraftDownload = xml.InnerText;
                        }
                    }
                }

                Version ver = new Version(newSelfVersion);
                Version verson = new Version(currentSelfVersion);
                int tm = verson.CompareTo(ver);

                if (tm >= 0)
                    isSelfUpdate = false;
                else
                    isSelfUpdate = true;

                ver = new Version(newMinecraftVersion);
                verson = new Version(currentMinecraftVersion);
                tm = verson.CompareTo(ver);

                if (tm >= 0)
                    isMinecraftUpdate = false;
                else
                    isMinecraftUpdate = true;
            }
            catch (Exception ex)
            {
                throw new Exception("networkerror");
            }
        }

        private void selfUpdate()
        {
            try
            {
                checkUpdate();
            }
            catch (Exception ex)
            {
                if (ex.Message == "networkerror")
                {
                    //MessageBox.Show("Network Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ProcessStartInfo info = new ProcessStartInfo("minecraft.exe");
                    Process.Start(info);
                    Environment.Exit(0);
                }
            }
            if (isSelfUpdate)
            {
                if (MessageBox.Show("New Launcher Updates Available!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        this.Hide();
                        GetUpdate updateForm = new GetUpdate();
                        updateForm.Show();
                        updateForm.setParent(this);
                        updateForm.setDownload(selfDownload);
                        updateForm.selfUpdateStart();
                        updateForm.Show();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "networkerror")
                        {
                            MessageBox.Show("Network Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        minecraftUpdate();
                    }
                }
                else
                    minecraftUpdate();
            }
            else
                minecraftUpdate();
        }

        public void minecraftUpdate()
        {
            if (isMinecraftUpdate)
            {
                if (MessageBox.Show("New Minecraft Updates Available!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        this.Hide();
                        GetUpdate updateForm = new GetUpdate();
                        updateForm.Show();
                        updateForm.setParent(this);
                        updateForm.setDownload(minecraftDownload);
                        updateForm.minecraftUpdateStart();
                        updateForm.Show();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "networkerror")
                        {
                            MessageBox.Show("Network Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        ProcessStartInfo info = new ProcessStartInfo("minecraft.exe");
                        Process.Start(info);
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                ProcessStartInfo info = new ProcessStartInfo("minecraft.exe");
                Process.Start(info);
                Environment.Exit(0);
            }
        }
    }
}
