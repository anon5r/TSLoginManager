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
            Label label = new Label(); //���x���쐬
            label.Font = textBox_Console.Font; //�t�H���g�����킹��
            label.Text = textBox_Console.Text; //�e�L�X�g���Z�b�g

            //���������e�L�X�gBOX�𒴂��āA�܂��X�N���[���o�[���\������Ă��Ȃ��Ȃ�
            if (textBox_Console.Width < label.PreferredWidth && textBox_Console.ScrollBars == ScrollBars.None)
            {
                textBox_Console.ScrollBars = ScrollBars.Horizontal; //�X�N���[���o�[�Z�b�g
                //�����X�N���[���o�[�̍������v���X
                textBox_Console.Height += SystemInformation.HorizontalScrollBarHeight;
            }
            //��L�̋t����
            else if (textBox_Console.Width >= label.PreferredWidth && textBox_Console.ScrollBars != ScrollBars.None)
            {
                textBox_Console.Height -= SystemInformation.HorizontalScrollBarHeight;
                textBox_Console.ScrollBars = ScrollBars.None;
            }
        }

        public void appendLog(string text)
        {
            /*
            Label label = new Label(); //���x���쐬
            label.Font = textBox_Console.Font; //�t�H���g�����킹��
            label.Text = textBox_Console.Text; //�e�L�X�g���Z�b�g

            //���������e�L�X�gBOX�𒴂��āA�܂��X�N���[���o�[���\������Ă��Ȃ��Ȃ�
            if (textBox_Console.Width < label.PreferredWidth && textBox_Console.ScrollBars == ScrollBars.None)
            {
                textBox_Console.ScrollBars = ScrollBars.Horizontal; //�X�N���[���o�[�Z�b�g
                //�����X�N���[���o�[�̍������v���X
                textBox_Console.Height += SystemInformation.HorizontalScrollBarHeight;
            }
            //��L�̋t����
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