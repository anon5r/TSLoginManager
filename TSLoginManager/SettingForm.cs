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

            // アイコンリソース一覧
            this.resources = SettingController.Icons.resourceList();
            hScrollBar_change_icon.Maximum = this.resources.Count + 1;
            
            // デフォルト
            pictureBox_icon_view.Image = (Image)Properties.Resources.char99.ToBitmap();
            hScrollBar_change_icon.Value = 0;

            // フォームに現在の設定を反映
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

            // ログ設定
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
        /// 設定をシステムに反映します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_setting_Click(object sender, EventArgs e)
        {
            bool isRestart = false;         // 再起動を行うか否か
            bool isNeedRestart = false;     // 再起動が必要な設定の場合

            // フリーズ検出の有効/無効
            if (checkBox_Freeze_Setting_Enable.Checked == true)
            {
                SettingController.HungUp.enable = true;
            }
            else
            {
                SettingController.HungUp.enable = false;
            }

            // フリーズ検出時間設定
            if ((int)UpDown_FreezeTime.Value > 0 || (int)UpDown_FreezeTime.Value <= 1000)
            {
                SettingController.HungUp.sec = (int)UpDown_FreezeTime.Value;
                SimpleLogger.WriteLine("setting changed: HungUpTime: " + (SettingController.HungUp.enable ? "enable" : "disable") + ", " + UpDown_FreezeTime.Value + " sec.");
            }
            else
            {
                SimpleLogger.WriteLine("over setting value limit. input value: "+(int)UpDown_FreezeTime.Value);
                MessageBox.Show("1秒未満、または1,000秒以上には設定できません。");
                UpDown_FreezeTime.Focus();
                return;
            }

            // アップデート確認方法
            SettingController.Update.startupAutoCehck = checkBox_AutoUpdateCheck.Checked;
            SimpleLogger.WriteLine("setting changed: StartUpAutoVersionCheck: " + (checkBox_AutoUpdateCheck.Checked ? "true" : "false"));
            
            SettingController.Update.checkBetaVersion = checkBox_isCheckBetaVersion.Checked;
            SimpleLogger.WriteLine("setting changed: isCheckBetaVersion: " + (checkBox_isCheckBetaVersion.Checked ? "true" : "false"));

            // ゲーム起動方法
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

            // アイコンの設定
            if (SettingController.Icons.resourceName != this.resources[hScrollBar_change_icon.Value])
            {
                isNeedRestart = true;
                SettingController.Icons.resourceName = this.resources[hScrollBar_change_icon.Value];
                SimpleLogger.WriteLine("setting changed: TrayIcon: " + SettingController.Icons.resourceName);
            }
            
            // ログ出力設定
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
                DialogResult msg = MessageBox.Show("一部設定を有効にするには再起動が必要です。" + Environment.NewLine + 
                            "ここで再起動しますか？", "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (msg == DialogResult.Yes)
                {
                    isRestart = true;   // 再起動
                }
            }

            // 設定反映に再起動処理が必要な場合
            if (isRestart)
            {
                //MessageBox.Show("設定を反映するため、再起動します。", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.ExitHandler();
                Application.Restart();
            }

            // フォームを閉じる
            this.Close();
        }

        private void button_ChangeEncryptKey_Click(object sender, EventArgs e)
        {
            string Keyword = Microsoft.VisualBasic.Interaction.InputBox("暗号化に使用するキーワードを入力してください。", "暗号化キーワードの変更", "", (Screen.PrimaryScreen.Bounds.Width / 2) - (360 / 2), (Screen.PrimaryScreen.Bounds.Height / 2) - 120);
            if (Keyword.Trim() != "")
            {
                TricksterTools.API.Controller.AccountController.MasterKey = Keyword;
                SimpleLogger.WriteLine("MasterKey changed.");
                MessageBox.Show("変更を反映しました。", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 既にマスターキーが存在する場合は上書きする
                if (File.Exists(Environment.CurrentDirectory + @"\MasterKey.cfg"))
                {
                    if (!TricksterTools.API.Controller.AccountController.saveMasterKey(TricksterTools.API.Controller.AccountController.MasterKey, Environment.CurrentDirectory + @"\MasterKey.cfg"))
                    {
                        MessageBox.Show("暗号化キーワードファイルの保存に失敗しました。");
                    }
                }
                else
                {
                    // 存在しない場合は作成可否を求める
                    DialogResult mbtn = DialogResult.No;
                    mbtn = MessageBox.Show("暗号化キーワードを保存しますか？" + Environment.NewLine +
                                "保存すると次回起動時に暗号化キーワードの入力をスキップできます。" + Environment.NewLine + 
                                "※複数人と共有コンピュータに保存する場合は推奨しません。", "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (mbtn == DialogResult.Yes)
                    {
                        if (!TricksterTools.API.Controller.AccountController.saveMasterKey(TricksterTools.API.Controller.AccountController.MasterKey, Environment.CurrentDirectory + @"\MasterKey.cfg"))
                        {
                            MessageBox.Show("暗号化キーワードファイルの保存に失敗しました。");
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
            diag.Description = "出力先ディレクトリを指定してください";
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