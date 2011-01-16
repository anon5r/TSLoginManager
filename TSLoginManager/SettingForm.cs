using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using TricksterTools.Library.LoginManager;
using TricksterTools.Debug;

namespace TSLoginManager
{
    public partial class SettingForm : Form
    {
        SortedList<int, string> resources = new SortedList<int, string>();

        string logDirectory = Environment.CurrentDirectory + "\\logs";

        public SettingForm()
        {
            InitializeComponent();

            // �A�C�R�����\�[�X�ꗗ
            this.resources = SettingController.Icons.resourceList();
            hScrollBar_change_icon.Maximum = this.resources.Count + 1;
            
            // �f�t�H���g
            pictureBox_icon_view.Image = (Image)Properties.Resources.char99.ToBitmap();
            hScrollBar_change_icon.Value = 0;

            // �t�H�[���Ɍ��݂̐ݒ�𔽉f
            UpDown_FreezeTime.Value = (decimal)SettingController.HungUp.sec;
            checkBox_AutoUpdateCheck.Checked = SettingController.Update.startupAutoCehck;
            //checkBox_isCheckBetaVersion.Checked = SettingController.Update.checkBetaVersion;
            checkBox_isCheckBetaVersion.Checked = false;
            checkBox_isCheckBetaVersion.Enabled = false;
            checkBox_Freeze_Setting_Enable.Checked = SettingController.HungUp.enable;
            groupBox_freeze_settings.Enabled = SettingController.HungUp.enable;
            pictureBox_icon_view.Image = (Image)Program.loadIconResource(SettingController.Icons.resourceName).ToBitmap();
            int resID = 0;
            foreach(int key in this.resources.Keys){
                if (this.resources[key] == SettingController.Icons.resourceName)
                {
                    resID = key;
                    break;
                }
            }
            hScrollBar_change_icon.Value = resID;

            switch (SettingController.GameStartUp.mode)
            {
                case 0:
                    radioButton_WayOfGameStartUp_Direct.Checked = true;
                    break;
                case 1:
                    radioButton_WayOfGameStartUp_Launcher.Checked = true;
                    break;
                default:
                    radioButton_WayOfGameStartUp_Direct.Checked = true;
                    break;
            }

            // ���O�ݒ�
            /*
            checkBox_OutputLog.Checked = SettingController.Logging.enable;
            logDirectory = SettingController.Logging.filePath;
            if (logDirectory == null || logDirectory.Length == 0)
            {
                logDirectory = Environment.CurrentDirectory + "\\logs";
                if (Directory.Exists(logDirectory) == false)
                {
                    Directory.CreateDirectory(logDirectory);
                }
            }
            if (checkBox_OutputLog.Checked == true)
            {
                button_FolderDiag.Enabled = true;
            }
            else
            {
                button_FolderDiag.Enabled = false;
            }
            */
            checkBox_OutputLog.Checked = File.Exists(Environment.CurrentDirectory + "\\debug");

        }

        /// <summary>
        /// �ݒ���V�X�e���ɔ��f���܂��B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_setting_Click(object sender, EventArgs e)
        {
            bool isRestart = false;         // �ċN�����s�����ۂ�
            bool isNeedRestart = false;     // �ċN�����K�v�Ȑݒ�̏ꍇ

            // �t���[�Y���o�̗L��/����
            if (checkBox_Freeze_Setting_Enable.Checked == true)
            {
                SettingController.HungUp.enable = true;
            }
            else
            {
                SettingController.HungUp.enable = false;
            }

            // �t���[�Y���o���Ԑݒ�
            if ((int)UpDown_FreezeTime.Value > 0 || (int)UpDown_FreezeTime.Value <= 1000)
            {
                SettingController.HungUp.sec = (int)UpDown_FreezeTime.Value;
                SimpleLogger.WriteLine("setting changed: HungUpTime: " + (SettingController.HungUp.enable ? "enable" : "disable") + ", " + UpDown_FreezeTime.Value + " sec.");
            }
            else
            {
                SimpleLogger.WriteLine("over setting value limit. input value: "+(int)UpDown_FreezeTime.Value);
                MessageBox.Show("1�b�����A�܂���1,000�b�ȏ�ɂ͐ݒ�ł��܂���B");
                UpDown_FreezeTime.Focus();
                return;
            }

            // �A�b�v�f�[�g�m�F���@
            SettingController.Update.startupAutoCehck = checkBox_AutoUpdateCheck.Checked;
            SimpleLogger.WriteLine("setting changed: StartUpAutoVersionCheck: " + (checkBox_AutoUpdateCheck.Checked ? "true" : "false"));
            
            SettingController.Update.checkBetaVersion = checkBox_isCheckBetaVersion.Checked;
            SimpleLogger.WriteLine("setting changed: isCheckBetaVersion: " + (checkBox_isCheckBetaVersion.Checked ? "true" : "false"));

            // �Q�[���N�����@
            if(radioButton_WayOfGameStartUp_Direct.Checked == true){
                SettingController.GameStartUp.mode = SettingController.RUN_GAME_DIRECT;
                SimpleLogger.WriteLine("setting changed: GameStartUpMode: Direct");
            }
            else if(radioButton_WayOfGameStartUp_Launcher.Checked == true)
            {
                SettingController.GameStartUp.mode = SettingController.RUN_GAME_LAUNCHER;
                SimpleLogger.WriteLine("setting changed: GameStartUpMode: Launcher");
            }
            else
            {
                SettingController.GameStartUp.mode = SettingController.RUN_GAME_DIRECT;
                SimpleLogger.WriteLine("setting changed: GameStartUpMode: Direct");
            }

            // �A�C�R���̐ݒ�
            if (SettingController.Icons.resourceName != this.resources[hScrollBar_change_icon.Value])
            {
                isNeedRestart = true;
                SettingController.Icons.resourceName = this.resources[hScrollBar_change_icon.Value];
                SimpleLogger.WriteLine("setting changed: TrayIcon: " + SettingController.Icons.resourceName);
            }
            
            // ���O�o�͐ݒ�
            SettingController.Logging.enable = checkBox_OutputLog.Checked;
            if (File.Exists(Environment.CurrentDirectory + "\\debug") && checkBox_OutputLog.Checked == false)
            {
                File.Delete(Environment.CurrentDirectory + "\\debug");
                isNeedRestart = true;
            }
            else 
            if (File.Exists(Environment.CurrentDirectory + "\\debug") == false && checkBox_OutputLog.Checked == true)
            {
                File.Create(Environment.CurrentDirectory + "\\debug");
                isNeedRestart = true;
            }
            SimpleLogger.WriteLine("setting changed: Logging: " + (checkBox_OutputLog.Checked ? "true" : "false"));
            /*
            if (checkBox_OutputLog.Checked == true)
            {
                SettingController.Logging.filePath = logDirectory;
                SimpleLogger.WriteLine("setting changed: Logging directory: " + logDirectory);
            }
            */

            if (isNeedRestart)
            {
                DialogResult msg = MessageBox.Show("�ꕔ�ݒ��L���ɂ���ɂ͍ċN�����K�v�ł��B" + Environment.NewLine + 
                            "�����ōċN�����܂����H", "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (msg == DialogResult.Yes)
                {
                    isRestart = true;   // �ċN��
                }
            }

            // �ݒ蔽�f�ɍċN���������K�v�ȏꍇ
            if (isRestart)
            {
                //MessageBox.Show("�ݒ�𔽉f���邽�߁A�ċN�����܂��B", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.ExitHandler();
                Application.Restart();
            }

            // �t�H�[�������
            this.Close();
        }

        private void button_ChangeEncryptKey_Click(object sender, EventArgs e)
        {
            string Keyword = Microsoft.VisualBasic.Interaction.InputBox("�Í����Ɏg�p����L�[���[�h����͂��Ă��������B", "�Í����L�[���[�h�̕ύX", "", (Screen.PrimaryScreen.Bounds.Width / 2) - (360 / 2), (Screen.PrimaryScreen.Bounds.Height / 2) - 120);
            if (Keyword.Trim() != "")
            {
                TricksterTools.API.Controller.AccountController.MasterKey = Keyword;
                SimpleLogger.WriteLine("MasterKey changed.");
                MessageBox.Show("�ύX�𔽉f���܂����B", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ���Ƀ}�X�^�[�L�[�����݂���ꍇ�͏㏑������
                if (File.Exists(Environment.CurrentDirectory + @"\MasterKey.cfg"))
                {
                    if (!TricksterTools.API.Controller.AccountController.saveMasterKey(TricksterTools.API.Controller.AccountController.MasterKey, Environment.CurrentDirectory + @"\MasterKey.cfg"))
                    {
                        MessageBox.Show("�Í����L�[���[�h�t�@�C���̕ۑ��Ɏ��s���܂����B");
                    }
                }
                else
                {
                    // ���݂��Ȃ��ꍇ�͍쐬�ۂ����߂�
                    DialogResult mbtn = DialogResult.No;
                    mbtn = MessageBox.Show("�Í����L�[���[�h��ۑ����܂����H" + Environment.NewLine +
                                "�ۑ�����Ǝ���N�����ɈÍ����L�[���[�h�̓��͂��X�L�b�v�ł��܂��B" + Environment.NewLine + 
                                "�������l�Ƌ��L�R���s���[�^�ɕۑ�����ꍇ�͐������܂���B", "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (mbtn == DialogResult.Yes)
                    {
                        if (!TricksterTools.API.Controller.AccountController.saveMasterKey(TricksterTools.API.Controller.AccountController.MasterKey, Environment.CurrentDirectory + @"\MasterKey.cfg"))
                        {
                            MessageBox.Show("�Í����L�[���[�h�t�@�C���̕ۑ��Ɏ��s���܂����B");
                        }
                    }
                }
            }
        }

        private void checkBox_Freeze_Setting_Enable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Freeze_Setting_Enable.Checked)
            {
                groupBox_freeze_settings.Enabled = true;
            }
            else
            {
                groupBox_freeze_settings.Enabled = false;
            }
        }

        private void hScrollBar_change_icon_ValueChanged(object sender, EventArgs e)
        {
            string resName = this.resources[hScrollBar_change_icon.Value];
            pictureBox_icon_view.Image = (Image)Program.loadIconResource(resName).ToBitmap();
        }

        private void button_FolderDiag_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            diag.ShowNewFolderButton = true;
            diag.Description = "�o�͐�f�B���N�g�����w�肵�Ă�������";
            diag.SelectedPath = logDirectory;
            DialogResult result = diag.ShowDialog();
            /*
            if (result == DialogResult.Cancel)
            {
                return;
            }
            */
            if (result == DialogResult.OK)
            {
                logDirectory = diag.SelectedPath;
            }
        }

        private void checkBox_OutputLog_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_OutputLog.Checked == true)
            {
                button_FolderDiag.Enabled = true;
            }
            else
            {
                button_FolderDiag.Enabled = false;
            }
        }
    }
}