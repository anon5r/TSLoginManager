using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TricksterTools.Plugins.UIEditor
{
    public partial class UIEditor_ChatUI : Form
    {
        public UIEditor_ChatUI()
        {
            InitializeComponent();
        }

        private void UIEditorMainForm_Load(object sender, EventArgs e)
        {
            int[] size = UIEdit.loadChatUISize();
            this.Width = size[0];
            this.Height = size[1];

            int h, w;
            //ディスプレイの作業領域の高さ
            h = System.Windows.Forms.Screen.GetWorkingArea(this).Height;
            //ディスプレイの作業領域の幅
            w = System.Windows.Forms.Screen.GetWorkingArea(this).Width;

            this.DesktopLocation = new Point(0, h - this.Height);
        }

        private void ToolStripMenuItem_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UIEditorMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Width;
            //this.Height;
            UIEdit.generateChatUI(this.Width, this.Height);
        }

        private void UIEditorMainForm_Resize(object sender, EventArgs e)
        {
            //pictureBox_ChatUI_Center.BackgroundImageLayout = ImageLayout.Tile;
            pictureBox_ChatUI_Center.Width = (this.Width - 90);
        }
    }
}