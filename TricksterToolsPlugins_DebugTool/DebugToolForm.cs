using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TricksterTools.Plugins.DebugTool
{
    public partial class DebugToolForm : Form
    {
        public DebugToolForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new D36u9T00lzException("���̗�O�G���[��DebugTool.D36u9 T00lz�ɂ���ĈӐ}�I�ɔ������ꂽ���̂ł��B");
        }
    }

    public class D36u9T00lzException : Exception
    {
        public D36u9T00lzException() {}
        public D36u9T00lzException(string Message) : base(Message) { }
        public D36u9T00lzException(string Message, Exception inngerException) : base(Message, inngerException) { }
    }
}