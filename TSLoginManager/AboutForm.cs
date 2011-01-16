using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using TricksterTools.CommonXmlStructure;
using TricksterTools.API.Controller;
using TricksterTools.Library.LoginManager;

namespace TSLoginManager
{
    partial class AboutForm : Form
    {
        #region アセンブリ属性アクセサ

        public string AssemblyTitle
        {
            get
            {
                // このアセンブリ上のタイトル属性をすべて取得します
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // 少なくとも 1 つのタイトル属性がある場合
                if (attributes.Length > 0)
                {
                    // 最初の項目を選択します
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // 空の文字列の場合、その項目を返します
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // タイトル属性がないか、またはタイトル属性が空の文字列の場合、.exe 名を返します
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                // このアセンブリ上の説明属性をすべて取得します
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // 説明属性がない場合、空の文字列を返します
                if (attributes.Length == 0)
                    return "";
                // 説明属性がある場合、その値を返します
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // このアセンブリ上の製品属性をすべて取得します
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // 製品属性がない場合、空の文字列を返します
                if (attributes.Length == 0)
                    return "";
                // 製品属性がある場合、その値を返します
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // このアセンブリ上の著作権属性をすべて取得します
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // 著作権属性がない場合、空の文字列を返します
                if (attributes.Length == 0)
                    return "";
                // 著作権属性がある場合、その値を返します
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                // このアセンブリ上の会社属性をすべて取得します
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // 会社属性がない場合、空の文字列を返します
                if (attributes.Length == 0)
                    return "";
                // 会社属性がある場合、その値を返します
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
        

        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            //pictureBox_Icon.Image = this.Icon.ToBitmap();
            pictureBox_Icon.Image = Properties.Resources.AppIcon;
            labelAppVersion.Text = "version " + Application.ProductVersion.ToString();
            if (Common.isInstalled())
            {
                label_TS_Client_Ver.Text = "version " + Common.getClientVersion();
            }
            else
            {
                label_TS_Client_Ver.Text = "NOT INSTALLED.";
            }
        }

        private void AboutForm_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://trickster.anoncom.net/");
        }

        private void linkLabel_Forum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://trickster.anoncom.net/forum/viewforum.php?f=4");
        }

        private void linkLabel_SupportMail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:anon.jp@gmail.com");
        }

        private void AboutForm_Shown(object sender, EventArgs e)
        {
            this.Opacity = 0;
            this.Visible = true;
            for (int i = 0; i <= 100; i++)
            {
                this.Opacity = (double)i / 100;
                this.Refresh();
            }
        }

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 100; i >= 0; i--)
            {
                this.Opacity = (double)i / 100;
                this.Refresh();
            }
        }
    }
}
