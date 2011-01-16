using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TSLoginManager
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            this.label_AppVersion.Text = "ver. " + Application.ProductVersion;
            this.progressBar.Value = 0;
            this.label_Status.Text = "";
            this.Activate();
        }

        public void statusUpdate(string message, int value)
        {
            this.label_Status.Text = message;
            if ((this.progressBar.Value + value) > this.progressBar.Maximum)
            {
                this.progressBar.Value = this.progressBar.Maximum;
            }
            else
            {
                this.progressBar.Value += value;
            }
            this.Activate();
            this.Refresh();
        }
    }
}