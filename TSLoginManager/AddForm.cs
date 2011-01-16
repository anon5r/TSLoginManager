using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TricksterTools.CommonXmlStructure;
using TricksterTools.API.Controller;
using TricksterTools.Library.LoginManager;
using TricksterTools.Debug;

namespace TSLoginManager
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            int h, w;
            //ディスプレイの作業領域の高さ
            h = System.Windows.Forms.Screen.GetWorkingArea(this).Height;
            //ディスプレイの作業領域の幅
            w = System.Windows.Forms.Screen.GetWorkingArea(this).Width;

            comboBox_LoginSite.Items.Add("公式サイト");
            comboBox_LoginSite.Items.Add("ハンゲーム");
            comboBox_LoginSite.Items.Add("アットゲームズ");
            comboBox_LoginSite.Items.Add("ゲーマーズワン");
            comboBox_LoginSite.SelectedIndex = 0;

            this.DesktopLocation = new Point(w - this.Width, h - this.Height);
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.TextBox_ID.Clear();
            this.TextBox_Pass.Clear();
            this.TextBox_ID.ClearUndo();
            this.TextBox_Pass.ClearUndo();
            this.Visible = false; // フォームの非表示
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            // 入力チェック
            if (this.TextBox_ID.TextLength <= 0)
            {
                MessageBox.Show("IDを入力してください。", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.TextBox_ID.Focus();
            }
            else
            {

                if (this.TextBox_Pass.TextLength <= 0)
                {
                    MessageBox.Show("パスワードを入力してください。", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.TextBox_Pass.Focus();
                }
                else
                {
                    if (ProgramController.AController.isExists(TextBox_ID.Text, (TricksterTools.API.DataStructure.Accounts.AccountProperties.LoginSite)comboBox_LoginSite.SelectedIndex))
                    {
                        string confirmMsg = "このIDは既に登録されています。上書きしますか？";
                        DialogResult confirmbox = MessageBox.Show(confirmMsg, "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmbox == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            // 上書きの場合、一度キーを削除
                            ProgramController.AController.delete(TextBox_ID.Text, (TricksterTools.API.DataStructure.Accounts.AccountProperties.LoginSite)comboBox_LoginSite.SelectedIndex);
                        }
                    }
                    if (AccountController.AccountData.Count == 0)
                    {
                        AccountController.AccountData.Clear();
                    }

                    TricksterTools.API.DataStructure.Accounts.AccountProperties newAccount = new TricksterTools.API.DataStructure.Accounts.AccountProperties();
                    newAccount.ID = TextBox_ID.Text;
                    newAccount.Password = TextBox_Pass.Text;

                    newAccount.Site = (TricksterTools.API.DataStructure.Accounts.AccountProperties.LoginSite)comboBox_LoginSite.SelectedIndex;
                    
                    ProgramController.AController.add(newAccount);
                    SimpleLogger.WriteLine("New account added. (" + newAccount.ToString() + ")");

                    /*
                     * この時点でもアカウント情報をファイルに保存する
                     */
                    string filename = Environment.CurrentDirectory + @"\accounts.dat";
                    if (AccountController.isLoadedAccount && (AccountController.AccountData.Count > 0))
                    {
                        ProgramController.AController.saveAccounts(filename, AccountController.MasterKey);
                    }

                    this.TextBox_ID.Clear();
                    this.TextBox_Pass.Clear();
                    this.TextBox_ID.ClearUndo();
                    this.TextBox_Pass.ClearUndo();
                    this.Visible = false;
                }
            }

        }

        private void AddForm_Shown(object sender, EventArgs e)
        {
            this.Activate();
            this.TextBox_ID.Focus();
        }
    }
}