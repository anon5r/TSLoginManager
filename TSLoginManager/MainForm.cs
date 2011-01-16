using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.Threading;
using TricksterTools.CommonXmlStructure;
using TricksterTools.API.Controller;
using TricksterTools.Library;
using TricksterTools.Library.LoginManager;
using TricksterTools.API.DataStructure;
using TricksterTools.Plugins;
using TricksterTools.Debug;



namespace TSLoginManager
{
    public partial class MainForm : Form, IPluginHost
    {
        //static Logger logger = new Logger();

        public Thread watchThread;
        public WatchController WController;
        private WatchController.stats prevStatus = WatchController.stats.Shutdown;
        private WatchController.status prevProcessStatus = WatchController.status.End;

        

        public MainForm()
        {
            Program.splash.statusUpdate("初期化しています...", 10);
            InitializeComponent();
            // コントロールボックスの［閉じる］ボタンの無効化
            IntPtr hMenu = TricksterTools.Library.Win32API.GetSystemMenu(this.Handle, 0);
            TricksterTools.Library.Win32API.RemoveMenu(hMenu, TricksterTools.Library.Win32API.SC_CLOSE, TricksterTools.Library.Win32API.MF_BYCOMMAND);

            /*
            // .NET 3.5以上、Windows Vista / Windows 7 以上で対応可能なコード
            // 再起動用コードの登録
            ApplicationRestartRecoveryManager.RegisterForApplicationRestart(
                    new RestartSettings("/restart",
                    RestartRestrictions.NotOnReboot | RestartRestrictions.NotOnPatch));

            // 修復用メソッドの登録
            RecoveryData data = new RecoveryData(new RecoveryCallback(RecoveryProcedure), null);
            RecoverySettings settings = new RecoverySettings(data, 0);
            ApplicationRestartRecoveryManager.RegisterForApplicationRecovery(settings);

            // 起動時の再起動かどうかの判断
            // 再起動時にデータを修復するコードの作成
            if (System.Environment.GetCommandLineArgs().Length > 1 &&
                System.Environment.GetCommandLineArgs()[1] == "/restart")
            {
                if (File.Exists(RecoveryFile))
                {
                    textBox1.Text = File.ReadAllText(RecoveryFile);
                }
            }
            */


            Program.splash.statusUpdate("設定を読み込み中...", 15);

            // 起動時に必要な設定を読み込みます。
            Program.LoadHandler();

            // start detect hangup timer
            /*
            if (!SettingController.HungUp.enable)
            {
                this.WController = new WatchController();
                this.WController.timer = new System.Threading.Timer(this.WController.timerCallback, null, 0, 1000);
                this.WController.timerCallback = new System.Threading.TimerCallback(this.WController.detectHungUp);
                this.watchThread = new Thread(new ThreadStart(this.WController.Run));
            }
            */

            // 設定からアイコンを取得する。
            Program.splash.statusUpdate("リソース読み込み中...", 5);
            notifyIcon.Icon = Program.loadIconResource(SettingController.Icons.resourceName);
            
            // アイコン表示
            notifyIcon.Visible = true;
            Program.splash.statusUpdate("", 100);
            Thread.Sleep(50);
            Program.splash.Close();


            /// プラグインフックの起動
            PluginController.PluginHook(Program.plugins, TricksterTools.Plugins.HookPoint.Startup);

        }

        #region フォームのCreateParamsプロパティをオーバーライドする
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TOOLWINDOW = 0x80;
                const long WS_POPUP = 0x80000000L;
                const int WS_VISIBLE = 0x10000000;
                const int WS_SYSMENU = 0x80000;
                const int WS_MAXIMIZEBOX = 0x10000;
             
                CreateParams cp = base.CreateParams;

                //cp.ClassName = "TricksterLoginManager";
                cp.ExStyle = WS_EX_TOOLWINDOW;
                cp.Style = unchecked((int)WS_POPUP) |
                    WS_VISIBLE | WS_SYSMENU | WS_MAXIMIZEBOX;
                cp.Width = 0;
                cp.Height = 0;

                return cp;
            }
        }
        #endregion
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            notifyIcon.Visible = true;
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // 終了処理のキャンセル
            this.WindowState = FormWindowState.Minimized; // フォームの非表示
        }
        
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddForm addFrm = new AddForm();
            addFrm.Show(); // フォームの表示
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            TricksterTools.Library.Win32API.SetForegroundWindow(new System.Runtime.InteropServices.HandleRef(this, this.Handle));

            Point mp = Control.MousePosition;

            /** ハッシュテーブルからIDを読み込む **/
            // 保存元のファイル名
            //string fileName = "config.txt";
            //System.Collections.Hashtable accounts = Program.loadConfig(fileName);
            System.Collections.SortedList links = SettingController.Links;

            if (e.Button == MouseButtons.Left)
            {
                #region 左クリック時

                this.contextMenuStrip_Left.Items.Clear();

                if (AccountController.AccountData.Count == 0)
                {
                    this.contextMenuStrip_Left.Items.Add("No Registered ID");
                    this.contextMenuStrip_Left.Enabled = false;
                }
                else
                {
                    this.contextMenuStrip_Left.Enabled = true;
                    IEnumerator ienum = AccountController.AccountData.GetEnumerator();
                    while(ienum.MoveNext())
                    {
                        Accounts.AccountProperties acprop = (Accounts.AccountProperties)ienum.Current;
                        string ID = acprop.ID;
                        string Password = acprop.Password;

                        ToolStripMenuItem items = new ToolStripMenuItem();
                        items.Text = ID;

                        if (acprop.Site == Accounts.AccountProperties.LoginSite.Official)
                        {
                            items.Image = (Image)Properties.Resources.official.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.HanGame)
                        {
                            items.Image = (Image)Properties.Resources.hangame.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.AtGames)
                        {
                            items.Image = (Image)Properties.Resources.atgames.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.Gamers1)
                        {
                            //items.Image = (Image)Properties.Resources.lievo.ToBitmap();
                            items.Image = (Image)Properties.Resources.gamers1.ToBitmap();
                        }
                        else
                        {
                            items.Image = (Image)Properties.Resources.official.ToBitmap();
                        }


                        items.Click += delegate
                        {
                            if (!Common.isInstalled()) // クライアントがインストールされているか
                            {
                                MessageBox.Show("トリックスター クライアントがインストールされていません。" + Environment.NewLine + 
                                    "クライアントプログラムをインストールして再度実行してください。", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //MessageBox.Show("TRICKSTER client program is not installed in this computer." + Environment.NewLine + 
                                //    "Please try to install and run.", "TSLoginManager");
                            }
                            else
                            {
                                SimpleLogger.Write("auto login start...");
                                //LoginController.startGame(ID, Password);
                                if (acprop.Site == Accounts.AccountProperties.LoginSite.Official)
                                {
                                    SimpleLogger.WriteLine("Official");
                                    OfficialLoginController.setPluginHost(this);
                                    OfficialLoginController.startGame(ID, Password);
                                }
                                else if (acprop.Site == Accounts.AccountProperties.LoginSite.HanGame)
                                {
                                    SimpleLogger.WriteLine("HanGame");
                                    HanGameLoginController.setPluginHost(this);
                                    HanGameLoginController.startGame(ID, Password);
                                }
                                else if (acprop.Site == Accounts.AccountProperties.LoginSite.AtGames)
                                {
                                    SimpleLogger.WriteLine("AtGames");
                                    AtGamesLoginController.setPluginHost(this);
                                    AtGamesLoginController.startGame(ID, Password);
                                }
                                else if (acprop.Site == Accounts.AccountProperties.LoginSite.Gamers1)
                                {
                                    SimpleLogger.WriteLine("Lievo");
                                    //LievoLoginController.setPluginHost(this);
                                    //LievoLoginController.startGame(ID, Password);
                                    GamersOneLoginController.setPluginHost(this);
                                    GamersOneLoginController.startGame(ID, Password);
                                }
                                else
                                {
                                    OfficialLoginController.setPluginHost(this);
                                    OfficialLoginController.startGame(ID, Password);
                                }
                            }
                        };
                        this.contextMenuStrip_Left.Items.Add(items);
                    }
                }

                //this.notifyIcon.ContextMenuStrip = this.contextMenuStrip_Left;
                //this.contextMenuStrip_Left.Show(mp.X - contextMenuStrip_Left.Width, mp.Y);
                this.contextMenuStrip_Left.Show(this, PointToClient(Cursor.Position));
            #endregion
            }
            else if (e.Button == MouseButtons.Right)
            {
                #region 右クリック時

                //this.ToolStripMenuItem_Right_Edit.DropDownItems.Clear();
                //this.ToolStripMenuItem_Right_Edit.DisplayStyle = ToolStripItemDisplayStyle.Text;
                this.ToolStripMenuItem_Right_Delete.DropDownItems.Clear();
                this.ToolStripMenuItem_Right_Delete.DisplayStyle = ToolStripItemDisplayStyle.Text;
                this.ToolStripMenuItem_Right_Plugins.DropDownItems.Clear();
                this.ToolStripMenuItem_Right_Plugins.DisplayStyle = ToolStripItemDisplayStyle.Text;
                this.ToolStripMenuItem_Right_Links.DropDownItems.Clear();
                this.ToolStripMenuItem_Right_Links.DisplayStyle = ToolStripItemDisplayStyle.Text;
                this.ToolStripMenuItem_Right_Tool_PluginInfo.DropDownItems.Clear();
                this.ToolStripMenuItem_Right_Tool_PluginInfo.DisplayStyle = ToolStripItemDisplayStyle.Text;

                #region アカウント削除用一覧
                if (AccountController.AccountData.Count == 0)
                {
                    //this.ToolStripMenuItem_Right_Edit.DropDownItems.Add("No Registered ID");
                    //this.ToolStripMenuItem_Right_Edit.Enabled = false;
                    //this.ToolStripMenuItem_Right_Delete.DropDownItems.Add("No Registered ID");
                    this.ToolStripMenuItem_Right_Delete.Enabled = false;
                }
                else
                {
                    //this.ToolStripMenuItem_Right_Edit.Enabled = true;
                    //this.ToolStripMenuItem_Right_Edit.DropDown.Enabled = true;
                    this.ToolStripMenuItem_Right_Delete.Enabled = true;
                    this.ToolStripMenuItem_Right_Delete.DropDown.Enabled = true;

                    this.contextMenuStrip_Left.Enabled = true;
                    IEnumerator ienum = AccountController.AccountData.GetEnumerator();
                    while (ienum.MoveNext())
                    {
                        //ToolStripMenuItem edit_items = new ToolStripMenuItem();
                        ToolStripMenuItem del_items = new ToolStripMenuItem();
                        Accounts.AccountProperties acprop = (Accounts.AccountProperties)ienum.Current;
                        string ID = acprop.ID;
                        //string Password = accounts[ID].ToString();

                        //edit_items.Text = ID;
                        del_items.Text = ID;
                        /*
                        edit_items.Click += delegate
                        {
                            EditForm edtFrm = new EditForm(ID);
                            edtFrm.Show(); // フォームの表示
                        };
                        this.ToolStripMenuItem_Right_Edit.DropDownItems.Add(edit_items);
                        */
                        if (acprop.Site == Accounts.AccountProperties.LoginSite.Official)
                        {
                            del_items.Image = (Image)Properties.Resources.official.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.HanGame)
                        {
                            del_items.Image = (Image)Properties.Resources.hangame.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.AtGames)
                        {
                            del_items.Image = (Image)Properties.Resources.atgames.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.Gamers1)
                        {
                            del_items.Image = (Image)Properties.Resources.lievo.ToBitmap();
                        }
                        else
                        {
                            del_items.Image = (Image)Properties.Resources.official.ToBitmap();
                        }

                        del_items.Click += delegate
                        {
                            DialogResult diagres = MessageBox.Show("ID: \" " + ID + " \" を削除しようとしています。" + Environment.NewLine + 
                                "よろしいですか？", "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            if(diagres == DialogResult.No){
                                return;
                            }
                            ProgramController.AController.delete(ID, acprop.Site);
                            /*
                             * この時点でもアカウント情報をファイルに保存する
                             */
                            string filename = Environment.CurrentDirectory + @"\accounts.dat";
                            if (AccountController.isLoadedAccount && (AccountController.AccountData.Count > 0))
                            {
                                ProgramController.AController.saveAccounts(filename, AccountController.MasterKey);
                            }

                        };
                        this.ToolStripMenuItem_Right_Delete.DropDownItems.Add(del_items);
                    }
                }
                #endregion
                #region プラグイン一覧
                if (Program.plugins.Length == 0)
                {
                    //this.ToolStripMenuItem_Right_Plugins.DropDownItems.Add("No Plugin");
                    this.ToolStripMenuItem_Right_Plugins.Enabled = false;
                }
                else
                {
                    this.ToolStripMenuItem_Right_Plugins.Enabled = true;
                    this.ToolStripMenuItem_Right_Plugins.DropDown.Enabled = true;

                    foreach (TricksterTools.Plugins.IPlugin plugin in Program.plugins)
                    {
                        ToolStripMenuItem_Plugin PluginItems = new ToolStripMenuItem_Plugin();
                        PluginItems.setPluginName(plugin.GetType().Name, plugin.Name);
                        PluginItems.Click += delegate
                        {
                            PluginController.PluginRun(Program.plugins, PluginItems.ClassName);
                        };
                        this.ToolStripMenuItem_Right_Plugins.DropDownItems.Add(PluginItems);
                    }
                }
                #endregion
                #region プラグイン情報
                if (Program.plugins.Length == 0)
                {
                    //this.ToolStripMenuItem_Right_Tool_PluginInfo.DropDownItems.Add("No Plugin");
                    this.ToolStripMenuItem_Right_Tool_PluginInfo.Enabled = false;
                }
                else
                {
                    this.ToolStripMenuItem_Right_Tool_PluginInfo.Enabled = true;
                    this.ToolStripMenuItem_Right_Tool_PluginInfo.DropDown.Enabled = true;

                    foreach (TricksterTools.Plugins.IPlugin plugin in Program.plugins)
                    {
                        ToolStripMenuItem_Plugin PluginInfoItems = new ToolStripMenuItem_Plugin();
                        PluginInfoItems.setPluginName(plugin.GetType().Name, plugin.Name);
                        PluginInfoItems.Click += delegate
                        {
                            //PluginInfoForm pluginInfoForm = new PluginInfoForm(plugin);
                            PluginInfoForm pluginInfoForm = new PluginInfoForm(PluginController.getPluginInfo(Program.plugins, PluginInfoItems.ClassName));
                            pluginInfoForm.Show();
                            pluginInfoForm.Owner = this;
                        };
                        this.ToolStripMenuItem_Right_Tool_PluginInfo.DropDownItems.Add(PluginInfoItems);
                    }
                }
                #endregion
                #region リンク一覧

                if (links.ContainsKey("__TSLM_NULL__"))
                {
                    this.ToolStripMenuItem_Right_Links.DropDownItems.Add("No Links");
                    this.ToolStripMenuItem_Right_Links.Enabled = false;
                }
                else
                {
                    this.ToolStripMenuItem_Right_Links.Enabled = true;
                    this.ToolStripMenuItem_Right_Links.DropDown.Enabled = true;

                    foreach (string key in links.Keys)
                    {
                        ToolStripMenuItem link_items = new ToolStripMenuItem();
                        string SiteName = key;
                        link_items.Text = SiteName;
                        link_items.Click += delegate
                        {
                            System.Diagnostics.Process.Start(links[SiteName].ToString());
                        };
                        this.ToolStripMenuItem_Right_Links.DropDownItems.Add(link_items);
                    }
                }
                #endregion
                #region プラグイン情報
                //this.notifyIcon.ContextMenuStrip = this.contextMenuStrip_Right;
                this.Activate();
                this.contextMenuStrip_Right.Show(mp.X, mp.Y + 275);
                //this.contextMenuStrip_Right.Show(this, Cursor.Position.X - this.Location.X, Cursor.Position.X - this.Location.X);
                //this.contextMenuStrip_Right.Show(PointToScreen(Cursor.Position));

                ToolStripDropDownMenu tsddm_r_del = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Delete.DropDown;
                tsddm_r_del.ShowImageMargin = true;
                ToolStripDropDownMenu tsddm_r_link = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Links.DropDown;
                tsddm_r_link.ShowImageMargin = false;
                ToolStripDropDownMenu tsddm_r_plugin = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Plugins.DropDown;
                tsddm_r_plugin.ShowImageMargin = false;
                ToolStripDropDownMenu tsddm_r_tool = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Tool.DropDown;
                tsddm_r_tool.ShowImageMargin = false;
                ToolStripDropDownMenu tsddm_r_tool_setting = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Tool_Settings.DropDown;
                tsddm_r_tool_setting.ShowImageMargin = false;
                ToolStripDropDownMenu tsddm_r_tool_plguininfo = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Tool_PluginInfo.DropDown;
                tsddm_r_tool_plguininfo.ShowImageMargin = false;
                #endregion
                #endregion
            }
        }

        private void ToolStripMenuItem_Right_Exit_Click(object sender, EventArgs e)
        {
            /**
             * 終了処理 
             */
            SimpleLogger.WriteLine("TSLoginManager shutdown...");
            Program.ExitHandler();
            this.notifyIcon.Visible = false; // アイコンをトレイから取り除く
            this.notifyIcon.Dispose();
            SimpleLogger.WriteLine("done.");
            SimpleLogger.Close();
            Application.Exit(); // アプリケーションの終了
        }

        private void ToolStripMenuItem_Right_Add_Click(object sender, EventArgs e)
        {
            SimpleLogger.WriteLine("show account add window.");
            AddForm addFrm = new AddForm();
            addFrm.Show(); // フォームの表示
            //addFrm.Activate(); // フォームをアクティブにする
            //addFrm.Dispose();
        }

        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            SimpleLogger.WriteLine("show about window.");
            AboutForm aboutFrm = new AboutForm();
            aboutFrm.ShowDialog();
            //aboutFrm.Dispose();
        }
        /*
        private void ToolStripMenuItem_Right_Edit_ID_Click(object sender, EventArgs e)
        {
            string ID = this.ToolStripMenuItem_Right_Edit_ID.Text;
            EditForm edtFrm = new EditForm(ID);
            edtFrm.Show(); // フォームの表示
        }
        */
        

        private void ToolStripMenuItem_Right_Tool_UpdateCheckGame_Click(object sender, EventArgs e)
        {

            string currentVer = Common.getClientVersion();
            string updateVer = Common.getServerVersion();

            NotifyIcon notiftIconClone = Program.frm.notifyIcon;

            if (Common.isNeedUpdate(currentVer, updateVer))
            {
                /*
                string strMsg = "アップデートがあります。" + Environment.NewLine + 
                        "     " + currentVer + " → " + updateVer + Environment.NewLine +  
                        "ランチャーを起動しますか？" + Environment.NewLine + 
                        "※ここからゲームの起動はできません。";
                DialogResult msg = MessageBox.Show(strMsg, "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    if (!Common.isInstalled())
                    {
                        MessageBox.Show("トリックスタークライアントがインストールされていません。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                    psi.FileName = Common.getInstallPath();
                    psi.RedirectStandardInput = false;
                    psi.RedirectStandardOutput = false;
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = false;
                    // 引数
                    //psi.Arguments = "";

                    SimpleLogger.WriteLine("start Launcher and launch Update.");

                    // 起動
                    System.Diagnostics.Process.Start(psi);
                }
                */

                string strMsg = "アップデートがあります。" + Environment.NewLine +
                        "     " + currentVer + " → " + updateVer + Environment.NewLine + 
                        "ランチャーを起動するにはここをクリックしてください。";
                Program.frm.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                Program.frm.notifyIcon.BalloonTipTitle = "トリックスターアップデート通知";
                Program.frm.notifyIcon.BalloonTipText = strMsg;
                Program.frm.notifyIcon.ShowBalloonTip(30000);
                Program.frm.notifyIcon.BalloonTipClicked += delegate
                {
                    if (!Common.isInstalled())
                    {
                        MessageBox.Show("トリックスタークライアントがインストールされていません。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.frm.notifyIcon = notiftIconClone;
                        return;
                    }
                    Common.updateGame();

                    /// プラグインによるフック起動ポイント
                    PluginController.PluginHook(Program.plugins, HookPoint.UpdatedGame);

                    Program.frm.notifyIcon = notiftIconClone;
                };
                Program.frm.notifyIcon.BalloonTipClicked -= delegate
                {
                    if (!Common.isInstalled())
                    {
                        MessageBox.Show("トリックスタークライアントがインストールされていません。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.frm.notifyIcon = notiftIconClone;
                        return;
                    }
                    Common.updateGame();

                    /// プラグインによるフック起動ポイント
                    PluginController.PluginHook(Program.plugins, HookPoint.UpdatedGame);

                    Program.frm.notifyIcon = notiftIconClone;
                };
            }
            else
            {
                SimpleLogger.WriteLine("no update.");
                /*
                MessageBox.Show("現在アップデートはありません。"
                    //+ "ver. " + currentVer
                    , "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                */
                Program.frm.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                Program.frm.notifyIcon.BalloonTipTitle = "トリックスターアップデート通知";
                Program.frm.notifyIcon.BalloonTipText = "現在アップデートはありません。";
                Program.frm.notifyIcon.ShowBalloonTip(10000);
                Program.frm.notifyIcon.BalloonTipClicked += delegate
                {
                    Program.frm.notifyIcon = notiftIconClone;
                    return;
                };
            }
        }

        private void ToolStripMenuItem_Right_Tool_CheckNewVersion_Click(object sender, EventArgs e)
        {
            Program.checkMyUpdate(true);
        }


        private void ToolStripMenuItem_Right_Tool_Settings_Click(object sender, EventArgs e)
        {
            SimpleLogger.WriteLine("show setting window...");
            SettingForm setForm = new SettingForm();
            setForm.Show(this);
        }
        

        private void timer_Tick(object sender, EventArgs e)
        {
            if (SettingController.HungUp.enable == false)
            {
                // フリーズ検出無効時
                return;
            }
            if (this.WController == null)
            {
                this.WController = new WatchController();
            }
            
            // クライアントの状態が変更された場合
            if (this.WController.gameStats != this.prevStatus)
            {
                SimpleLogger.WriteLine("Trickster client: " + this.WController.gameStats.ToString());
                this.prevStatus = this.WController.gameStats;
            }
            if (this.WController.ProcessStatus != this.prevProcessStatus)
            {
                SimpleLogger.WriteLine("Trickster process: " + this.WController.ProcessStatus.ToString());
                this.prevProcessStatus = this.WController.ProcessStatus;
            }

            // フリーズ判定
            int msec = SettingController.HungUp.sec * 1000;   // 単位ミリ秒
            this.WController.setTimer(timer, msec);
            notifyIcon.Text = "TSLoginManager (FREEZE MODE)";
            Thread worker = new Thread( this.WController.watcher );
            worker.IsBackground = true;
            worker.Start();
            notifyIcon.Text = "TSLoginManager";
        }

        /// <summary>
        /// メインフォーム
        /// </summary>
        public Form HostForm
        {
            get
            {
                return (Form)this;
            }
        }

        /// <summary>
        /// プラグインのメッセージを表示する
        /// </summary>
        /// <param name="plugin">メソッドを呼び出すプラグイン</param>
        /// <param name="msg">表示するメッセージ</param>
        public void ShowMessage(IPlugin plugin, string msg)
        {
            MessageBox.Show(msg);
        }


        public class ToolStripMenuItem_Plugin : ToolStripMenuItem
        {
            private string PluginClassName = "";
            private string PluginDisplayName = "";

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="ClassName">プラグインクラス名</param>
            /// <param name="DisplayName">プラグイン表示名</param>
            public void setPluginName(string ClassName, string DisplayName)
            {
                this.PluginClassName = ClassName;
                this.PluginDisplayName = DisplayName;
            }

            // 実際の値
            public string ClassName
            {
                get
                {
                    return this.PluginClassName;
                }
            }

            // 表示名称
            public override string Text
            {
                get
                {
                    return this.PluginDisplayName;
                }
                set
                {
                    this.PluginDisplayName = value;
                }
            }

            // オーバーライドしたメソッド
            public override string ToString()
            {
                return this.PluginDisplayName;
            }
        }
        
        /// <summary>
        /// ファイナライザ
        /// </summary>
        ~MainForm()
        {
            Program.ExitHandler();
            SimpleLogger.Close();
        }
    }
}