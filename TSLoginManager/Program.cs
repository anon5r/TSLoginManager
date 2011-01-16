using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;
using System.Net;
using System.IO;
using System.Drawing;
using System.Threading;
using TricksterTools.CommonXmlStructure;
using TricksterTools.API.Controller;
using TricksterTools.Library;
using TricksterTools.Library.LoginManager;
using TricksterTools.Versions;
using TricksterTools.Plugins;
using TricksterTools.Debug;

namespace TSLoginManager
{
    static class Program
    {
        private static System.Threading.Mutex _mutex;
        //private static Logger logger;

        public static SplashForm splash;
        public static MainForm frm;
        public static IPlugin[] plugins;

        //public static System.Threading.Thread watchThrd;


        #region Main()
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ThreadException にハンドラをバインド
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadingException);
            
            // UnhandledException にハンドラをバインド
            Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler (Application_UnhandledException);
            


            //logger = SimpleLogger.setLogger("Main");
            //Console.Title = "TSLoginManager (DEBUG Mode)";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (File.Exists(Environment.CurrentDirectory + "\\debug"))
            {
                SimpleLogger.Enable(true);
            }
            else
            {
                SimpleLogger.Enable(false);
            }

            ProgramController.showTaskBar();

            if (!Program.isLoaded())
            {
                SimpleLogger.WriteLine("TSLoginManager start...");
                Program.splash = new SplashForm();
                Program.splash.Show();
                Program.splash.Refresh();
                Program.splash.statusUpdate("初期化しています...", 1);
                Program.frm = new MainForm();

                // auto update check
                if (SettingController.Update.startupAutoCehck)
                {
                    Program.checkMyUpdate(false);
                }

                Application.Run();
            }
            else
            {
                MessageBox.Show("既に起動中です。", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                /*
                Program.frm.notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
                Program.frm.notifyIcon.BalloonTipTitle = "既に起動中です";
                Program.frm.notifyIcon.BalloonTipText = "TSLoginManagerはタスクバー通知領域でアイコンとして既に起動中です。";
                Program.frm.notifyIcon.ShowBalloonTip(10000);
                */
                Application.Exit();
            }

            /*
            // コンソール画面を隠す
            System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
            IntPtr hWnd = p.MainWindowHandle;
            IntPtr minimize = (IntPtr)0xF020;
            IntPtr hide = (IntPtr)0x80;
            Int32 syscommand = 0x112;
            if ((int)hWnd > 0)
            {
                System.Windows.Forms.Message winMsg = Message.Create(hWnd, syscommand, minimize, (IntPtr)0x00);
                System.Windows.Forms.NativeWindow nativeWindow = new System.Windows.Forms.NativeWindow();
                nativeWindow.DefWndProc(ref winMsg);

                winMsg = Message.Create(hWnd, syscommand, hide, (IntPtr)0x00);
                nativeWindow.DefWndProc(ref winMsg);
                nativeWindow.DestroyHandle();
            }
            */

        }
        #endregion
        #region checkMyUpdate()
        /// <summary>
        /// 自分自身のアップデートが存在するか確認
        /// </summary>
        public static void checkMyUpdate(bool viewCurrentNewVersion)
        {
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("UTF-8");
            string url = "http://trickster.anoncom.net/versions.xml";
            string param = @"name=TSLoginManager&version=" + Application.ProductVersion.ToString();
            if (SettingController.Update.checkBetaVersion)
            {
                param += @"&check=beta";
            }
            else
            {
                param += @"&check=release";
            }
            HttpWebRequest req;
            HttpWebResponse res = null;

            #region ネットワーク接続を試みる
            int inetFlg;
            while (!TricksterTools.Library.WinInet.InternetGetConnectedState(out inetFlg, 0))
            {

                SimpleLogger.WriteLine("Could not connect network.");

                DialogResult dgRes = MessageBox.Show("ネットワークに接続されていないため、処理を続行できません。", global::TSLoginManager.Properties.Settings.Default.AppName, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (dgRes == DialogResult.Cancel)
                {
                    return;
                }
            }
            #endregion
            url += "?" + param;
            // リクエストの作成
            try
            {
                req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";
                req.UserAgent = LoginController.__USER_AGENT__;
                req.Headers.Add("Accept-Language", "ja");
                //req.KeepAlive = true;
                req.AllowAutoRedirect = true;
                req.Timeout = 60 * 1000; // タイムアウト1分

                //SimpleLogger.WriteLine("[Request]");
                SimpleLogger.WriteLine("[ Request ] ---------------------------------------");
                SimpleLogger.WriteLine("URL: " + url);
                SimpleLogger.WriteLine("Method: " + req.Method);
                SimpleLogger.WriteLine(req.Headers.ToString());
                SimpleLogger.WriteLine("----------------------------------------------------");

                // レスポンスの取得と読み込み
                res = (System.Net.HttpWebResponse) req.GetResponse();
                //Stream resStream = res.GetResponseStream();

                HttpWebResponse deb_res;
                deb_res = (System.Net.HttpWebResponse)req.GetResponse();
                //SimpleLogger.WriteLine("[Response]\nURL: " + url + Environment.NewLine + deb_res.Headers.ToString());
                SimpleLogger.WriteLine("[ Response ] ---------------------------------------");
                SimpleLogger.WriteLine(deb_res.Headers.ToString());
                SimpleLogger.WriteLine("");

                Stream deb_st = deb_res.GetResponseStream();
                StreamReader deb_sr = new StreamReader(deb_st, enc);
                SimpleLogger.WriteLine(deb_sr.ReadToEnd().Replace("\n", Environment.NewLine));
                SimpleLogger.WriteLine("----------------------------------------------------");
                SimpleLogger.WriteLine("");
                deb_sr.Close();
                deb_st.Close();

                
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

                    System.Xml.XmlReaderSettings xmlrdset = new System.Xml.XmlReaderSettings();
                    xmlrdset.IgnoreWhitespace = true;
                    xmlrdset.IgnoreComments = true;
                    System.Xml.XmlReader reader = System.Xml.XmlReader.Create(url, xmlrdset);
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlVersionsRoot));
                    XmlVersionsRoot XmlVerRoot = (XmlVersionsRoot)serializer.Deserialize(reader);
                    

                    if (XmlVerRoot.Status.Trim() == "Error")
                    {

                        // メッセージボックスによる通知
                        MessageBox.Show("バージョンチェックエラー", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // バルーンチップによる通知
                        Program.frm.notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
                        Program.frm.notifyIcon.BalloonTipTitle = "TSLoginManager";
                        Program.frm.notifyIcon.BalloonTipText = "バージョンチェック時エラー";
                        Program.frm.notifyIcon.ShowBalloonTip(30 * 1000);   // 表示タイムアウト(30秒)
                        return;
                    }
                    XmlVersionInfo info = (XmlVersionInfo)XmlVerRoot.Information;
                                                
                    if (XmlVerRoot.Status.Trim() == "OK")
                    {
                        if (viewCurrentNewVersion)
                        {
                            // メッセージボックスによる通知
                            //MessageBox.Show("使用中のバージョンは最新版です。", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // バルーンチップによる通知
                            Program.frm.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                            Program.frm.notifyIcon.BalloonTipTitle = "TSLoginManager";
                            Program.frm.notifyIcon.BalloonTipText = "使用中のバージョンは最新版です。";
                            Program.frm.notifyIcon.ShowBalloonTip(30 * 1000);   // 表示タイムアウト(30秒)

                            Program.frm.notifyIcon.BalloonTipClicked += delegate
                            {
                                return;
                            };
                        }
                    }
                    else if (XmlVerRoot.Status.Trim() == "Update")
                    {
                        string currentVersion = "ver. " + Application.ProductVersion.ToString();
                        string newVersion = "ver. " + info.Version;
                        if (SettingController.Update.checkBetaVersion && XmlVerRoot.Information.isBeta)
                        {
                            newVersion += "β";
                        }
                        string message = (info.Message.Trim() == "") ? "" : "\n" + info.Message;
                        /*
                        // メッセージボックスによる通知
                        DialogResult dres = MessageBox.Show("最新版が公開されています。" + Environment.NewLine + 
                                                            "ダウンロードしますか？" + Environment.NewLine + Environment.NewLine + 
                                                            currentVersion + " → " + newVersion + message, "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dres == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start("http://trickster.anoncom.net/tools/TSLoginManager.html");
                        }
                        */
                        // バルーンチップによる通知
                        Program.frm.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                        Program.frm.notifyIcon.BalloonTipTitle = "TSLoginManager";
                        Program.frm.notifyIcon.BalloonTipText = "最新版が公開されています。" + Environment.NewLine
                            + "ダウンロードするにはここをクリックしてください。" + Environment.NewLine + Environment.NewLine
                            + currentVersion + " → " + newVersion + Environment.NewLine
                            + message;
                        Program.frm.notifyIcon.BalloonTipClicked += delegate {
                            System.Diagnostics.Process.Start("http://trickster.anoncom.net/tools/TSLoginManager.html");
                        };
                        Program.frm.notifyIcon.ShowBalloonTip(30 * 1000);   // 表示タイムアウト(30秒)
                    }
                    else
                    {
                        // メッセージボックスによる通知
                        //MessageBox.Show("最新版確認時にエラーを取得しました。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // バルーンチップによる通知
                        Program.frm.notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
                        Program.frm.notifyIcon.BalloonTipTitle = "TSLoginManager";
                        Program.frm.notifyIcon.BalloonTipText = "最新版確認時にエラーを取得しました。";
                        Program.frm.notifyIcon.ShowBalloonTip(30 * 1000);   // 表示タイムアウト(30秒)
                        Program.frm.notifyIcon.BalloonTipClicked += delegate
                        {
                            return;
                        };
                    }
                    
                }else{
                    // メッセージボックスによる通知
                    MessageBox.Show("通信に失敗しました。しばらくたってから再度操作してください。" + Environment.NewLine + 
                                "このエラーが長時間、継続的に表示される場合はお問い合わせください。", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                res.Close();
            }
            catch (WebException we)
            {
                //MessageBox.Show(e.Message.ToString(), "WebException", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                SimpleLogger.WriteLine(we.Message.ToString());
                if (we.Status == WebExceptionStatus.Timeout)
                {
                    MessageBox.Show("通信がタイムアウトしました。しばらくたってから再度操作してください。" + Environment.NewLine + 
                                "このエラーが長時間、継続的に表示される場合はお問い合わせください。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("通信に失敗しました。しばらくたってから再度操作してください。" + Environment.NewLine + 
                                "このエラーが長時間、継続的に表示される場合はお問い合わせください。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }
            catch (FileLoadException fle)
            {
                SimpleLogger.WriteLine(fle.Message.ToString());

                MessageBox.Show("バージョン情報の取得に失敗しました。" + Environment.NewLine + 
                            "コード:101", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Xml.XmlException xe)
            {
                SimpleLogger.WriteLine(xe.Message.ToString());

                MessageBox.Show("バージョン情報の取得に失敗しました。" + Environment.NewLine + 
                            "コード:102", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.InvalidOperationException ioe)
            {
                SimpleLogger.WriteLine(ioe.Message.ToString());

                MessageBox.Show("バージョン情報の取得に失敗しました。" + Environment.NewLine + "コード:103");
            }
            catch (Exception e)
            {
                SimpleLogger.WriteLine(e.Message.ToString());

                //MessageBox.Show("例外エラー" + Environment.NewLine + "コード:500");
                throw e;
            }
            finally
            {

                if (res != null && res.GetType() == typeof(HttpWebResponse))
                {
                    try
                    {
                        res.Close();
                    }
                    catch { }
                }
            }

        }
        #endregion

        #region isLoaded()
        /// <summary>
        /// すでに起動中か否か
        /// </summary>
        /// <returns>true:起動中, false:未起動</returns>
        private static bool isLoaded()
        {
            // 次の静的フィールドが宣言されているものとする
            // static private System.Threading.Mutex _mutex;

            // Mutexクラスの作成
            // "MyName"の部分を適当な文字列に変えてください
            _mutex = new System.Threading.Mutex(false, global::TSLoginManager.Properties.Settings.Default.AppName);
            // ミューテックスの所有権を要求する
            if (_mutex.WaitOne(0, false) == false)
            {
                // すでに起動していると判断する
                return true;
            }
            return false;
        }
        #endregion

        #region loadIconResource()
        /// <summary>
        /// アイコンリソースファイルを読み込む
        /// </summary>
        /// <param name="resourceName">リソースファイル名</param>
        /// <returns>アイコン</returns>
        public static Icon loadIconResource(string resourceName){
            return (Icon)Properties.Resources.ResourceManager.GetObject(resourceName, Properties.Resources.Culture);
        }
        #endregion

        #region LoadHandler()
        /// <summary>
        /// 起動時に必要な設定を読み込みます。
        /// </summary>
        public static void LoadHandler()
        {

            // 保存元のファイル名
            string fileName = "accounts.dat";
            string settingFile = "settings.xml";
            string LinkFileName = "links.txt";
            string MasterKeyFile = Environment.CurrentDirectory + @"\masterkey.cfg";
            //Program.loadAccountFromText(fileName);
            ProgramController.AController = new AccountController();

            Program.splash.statusUpdate("マスターキーを読み込んでいます...", 10);
            if (System.IO.File.Exists(MasterKeyFile))
            {
                SimpleLogger.WriteLine("MasterKey file exist.");
                System.IO.StreamReader sr = System.IO.File.OpenText(MasterKeyFile);
                AccountController.MasterKey = sr.ReadLine();
            }
            else
            {
                SimpleLogger.WriteLine("MasterKey file does not found.");
                AccountController.MasterKey = Microsoft.VisualBasic.Interaction.InputBox("暗号化解除キーワードを入力してください。", "TSLoginManager", "", (Screen.PrimaryScreen.Bounds.Width / 2) - (360 / 2), (Screen.PrimaryScreen.Bounds.Height / 2) - 120);
            }
            Program.splash.statusUpdate("基本設定を読み込み中...", 5);

            // load settings.xml
            SimpleLogger.WriteLine("load settings...");
            SettingController.loadSettings(settingFile);
            Program.splash.statusUpdate("設定を読み込み中...", 5);

            Program.splash.statusUpdate("アカウント情報を読み込み中...", 5);
            // load accounts.dat
            SimpleLogger.WriteLine("load account data...");
            ProgramController.AController.loadAccounts(fileName, AccountController.MasterKey);
            Program.splash.statusUpdate("アカウント情報を読み込み中...", 5);

            // load plugins
            Program.splash.statusUpdate("プラグインを読み込み中...", 5);
            SimpleLogger.WriteLine("load plugins...");
            Program.plugins = PluginController.loadPlugins(Program.frm);

            // load links.txt
            Program.splash.statusUpdate("リンクファイルを読み込み中...", 5);
            SimpleLogger.WriteLine("load link file...");
            SettingController.loadLinks(LinkFileName);


            SimpleLogger.WriteLine("all data load complete.");
            Program.splash.statusUpdate("設定の読み込み完了", 5);

            // auto update check
            /*
            if (SettingController.Update.startupAutoCehck)
            {
                Program.splash.statusUpdate("アップデートを確認中...", 5);
                Program.checkMyUpdate(false);
            }
            */
            Program.splash.statusUpdate("", 5);

        }
        #endregion
        #region ExitHandler()
        /// <summary>
        /// MainForm以外から終了処理を行う際に、
        /// タイマの処理を行わない状態で終了します。
        /// 外部からのやむを得ない終了処理時以外の使用を推奨しません。
        /// </summary>
        public static void ExitHandler()
        {
            /// プラグインフックの起動
            PluginController.PluginHook(Program.plugins, TricksterTools.Plugins.HookPoint.Shutdown);



            // 設定保存
            string settingfile = Environment.CurrentDirectory + @"\settings.xml";
            string MasterKeyFile = Environment.CurrentDirectory + @"\masterkey.cfg";
            SettingController.saveSettings(settingfile);
            // プラグインの設定保存
            //PluginSettings.saveConfig(plugins);

            // アカウント情報保存
            //Program.saveAccountToText(filename);
            string filename = Environment.CurrentDirectory + @"\accounts.dat";
            if (AccountController.isLoadedAccount && (AccountController.AccountData.Count > 0))
            {
                ProgramController.AController.saveAccounts(filename, AccountController.MasterKey);
                if (!File.Exists(MasterKeyFile))
                {
                    DialogResult mbtn = DialogResult.No;
                    mbtn = MessageBox.Show("暗号化キーワードを保存しますか？" + Environment.NewLine +
                                "保存すると次回起動時に暗号化キーワードの入力をスキップできます。" + Environment.NewLine + 
                                "※複数人と共有コンピュータに保存する場合は推奨しません。", "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (mbtn == DialogResult.Yes)
                    {
                        if (!TricksterTools.API.Controller.AccountController.saveMasterKey(AccountController.MasterKey, MasterKeyFile))
                        {
                            MessageBox.Show("暗号化キーワードファイルの保存に失敗しました。");
                        }
                    }
                }
            }
            else if (AccountController.AccountData.Equals(null))
            {
                return;
            }
            else
            {
                if (AccountController.AccountData.Count > 0 && !AccountController.isLoadedAccount)
                {
                    AccountController.MasterKey = Microsoft.VisualBasic.Interaction.InputBox("暗号化に使用するキーワードを入力してください。", "Error", "", (Screen.PrimaryScreen.Bounds.Width / 2) - (360 / 2), (Screen.PrimaryScreen.Bounds.Height / 2) - 120);
                    Boolean abortFlg = false;
                    while (AccountController.MasterKey.Trim() == "" || abortFlg)
                    {
                        DialogResult mboxbutton = MessageBox.Show("暗号化キーワードは必ず入力してください。", "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        if (mboxbutton == DialogResult.Abort)
                        {
                            DialogResult mbtn = MessageBox.Show("この操作を中断するとアカウント情報を保存できません。" + Environment.NewLine + 
                                        "よろしいですか？", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (mbtn == DialogResult.Yes)
                            {
                                abortFlg = true;
                                break;
                            }
                        }
                        AccountController.MasterKey = Microsoft.VisualBasic.Interaction.InputBox("暗号化に使用するキーワードを入力してください。", "Error", "", (Screen.PrimaryScreen.Bounds.Width / 2) - (360 / 2), (Screen.PrimaryScreen.Bounds.Height / 2) - 120);
                    }
                    if (!abortFlg)
                    {
                        bool NoSaveFlag = false;
                        if (System.IO.File.Exists(filename))
                        {
                            DialogResult mbtn = DialogResult.No;
                            while (mbtn == DialogResult.No)
                            {
                                mbtn = MessageBox.Show("この状態で保存すると、既に存在しているアカウント情報は上書きされ、復元できなくなります。" + Environment.NewLine + 
                                            "上書きしてよろしいですか？", "上書き確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (mbtn == DialogResult.Yes)
                                {
                                    break;
                                }
                                mbtn = MessageBox.Show("保存せずに終了しますか？", "終了確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (mbtn == DialogResult.Yes)
                                {
                                    NoSaveFlag = true;
                                }
                            }
                        }
                        if (!NoSaveFlag)
                        {
                            ProgramController.AController.saveAccounts(filename, AccountController.MasterKey);
                            if (!File.Exists(MasterKeyFile))
                            {
                                DialogResult mbtn2 = DialogResult.No;
                                mbtn2 = MessageBox.Show("暗号化キーワードを保存しますか？" + Environment.NewLine +
                                            "保存すると次回起動時に暗号化キーワードの入力をスキップできます。" + Environment.NewLine + 
                                            "※複数人と共有コンピュータに保存する場合は推奨しません。", "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (mbtn2 == DialogResult.Yes)
                                {
                                    if (!TricksterTools.API.Controller.AccountController.saveMasterKey(AccountController.MasterKey, MasterKeyFile))
                                    {
                                        MessageBox.Show("暗号化キーワードファイルの保存に失敗しました。");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion



        /// <summary>
        /// ThreadException ハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Application_ThreadingException(object sender, ThreadExceptionEventArgs e)
        {
            //SimpleLogger.Close();
            //MessageBox.Show(Form.ActiveForm, "エラーが発生しました。内容をログとして保存しました。", "スレッド例外");
            ErrorLogging.OutputException(e.Exception);

        }

        /// <summary>
        /// UnhandledException ハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Application_UnhandledException(object sender,  UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                //SimpleLogger.Close();
                //MessageBox.Show(Form.ActiveForm, "[" + ex.InnerException.TargetSite.Name + "] " + ex.Message, "ハンドルされない例外");
                ErrorLogging.OutputException(ex);
            }
        }
    }
}