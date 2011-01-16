using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TricksterTools.Plugins.DebugWindow
{
    public partial class DebugWindowForm : Form
    {

        public DebugWindowForm()
        {
            InitializeComponent();
            
        }

        private void textBox_Console_TextChanged(object sender, EventArgs e)
        {
            Label label = new Label(); //ラベル作成
            label.Font = textBox_Console.Font; //フォントを合わせる
            label.Text = textBox_Console.Text; //テキストをセット

            //文字幅がテキストBOXを超えて、まだスクロールバーが表示されていないなら
            if (textBox_Console.Width < label.PreferredWidth && textBox_Console.ScrollBars == ScrollBars.None)
            {
                textBox_Console.ScrollBars = ScrollBars.Horizontal; //スクロールバーセット
                //水平スクロールバーの高さをプラス
                textBox_Console.Height += SystemInformation.HorizontalScrollBarHeight;
            }
            //上記の逆処理
            else if (textBox_Console.Width >= label.PreferredWidth && textBox_Console.ScrollBars != ScrollBars.None)
            {
                textBox_Console.Height -= SystemInformation.HorizontalScrollBarHeight;
                textBox_Console.ScrollBars = ScrollBars.None;
            }
        }

        public void appendLog(string text)
        {
            /*
            Label label = new Label(); //ラベル作成
            label.Font = textBox_Console.Font; //フォントを合わせる
            label.Text = textBox_Console.Text; //テキストをセット

            //文字幅がテキストBOXを超えて、まだスクロールバーが表示されていないなら
            if (textBox_Console.Width < label.PreferredWidth && textBox_Console.ScrollBars == ScrollBars.None)
            {
                textBox_Console.ScrollBars = ScrollBars.Horizontal; //スクロールバーセット
                //水平スクロールバーの高さをプラス
                textBox_Console.Height += SystemInformation.HorizontalScrollBarHeight;
            }
            //上記の逆処理
            else if (textBox_Console.Width >= label.PreferredWidth && textBox_Console.ScrollBars != ScrollBars.None)
            {
                textBox_Console.Height -= SystemInformation.HorizontalScrollBarHeight;
                textBox_Console.ScrollBars = ScrollBars.None;
            }
            */
            textBox_Console.Text += text;
        }
    }
}