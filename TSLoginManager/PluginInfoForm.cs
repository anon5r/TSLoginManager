using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TricksterTools.Plugins;

namespace TSLoginManager
{
    public partial class PluginInfoForm : Form
    {
        IPlugin plugin;

        private static bool ViewClassName = false;

        public PluginInfoForm(IPlugin plugin)
        {
            InitializeComponent();
            this.plugin = plugin;
        }

        private void PluginInfoForm_Load(object sender, EventArgs e)
        {
            this.Text = this.plugin.Name + " - ƒvƒ‰ƒOƒCƒ“î•ñ";
            this.label_PluginName.Text = this.plugin.Name;
            this.label_Author.Text = this.plugin.Author;
            this.label_Version.Text = this.plugin.Version;
            this.textBox_Descriptions.Text = this.plugin.Description;
            this.linkLabel_Support.Text = "";
            if (this.plugin.URL != "")
            {
                if (this.plugin.URL.Length > 35)
                {
                    this.linkLabel_Support.Text = this.plugin.URL.Substring(0, 30) + "...";
                }
                else
                {
                    this.linkLabel_Support.Text = this.plugin.URL;
                }
                this.linkLabel_Support.LinkClicked += delegate
                {
                    System.Diagnostics.Process.Start(this.plugin.URL);
                };
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PluginInfoForm_Shown(object sender, EventArgs e)
        {
            this.button_OK.Focus();
            this.Opacity = 0;
            this.Visible = true;
            for (int i = 0; i <= 100; i++)
            {
                this.Opacity = Double.Parse(i.ToString()) / 100;
                this.Refresh();
            }
        }

        private void PluginInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 100; i >= 0; i--)
            {
                this.Opacity = Double.Parse(i.ToString()) / 100;
                this.Refresh();
            }
        }

        private void label_PluginName_DoubleClick(object sender, EventArgs e)
        {
            if (ViewClassName)
            {
                this.label_PluginName.Text = this.plugin.Name;
                ViewClassName = false;
            }
            else
            {
                this.label_PluginName.Text = this.plugin.GetType().Name;
                ViewClassName = true;
            }
        }

    }
}