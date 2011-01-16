using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
//using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;
using TricksterTools.Library;
using TricksterTools.CommonXmlStructure;
using TricksterTools.API.Controller;
using TricksterTools.Debug;
using TricksterTools.Library.Xml.Settings;

namespace TricksterTools
{
    namespace Library
    {
        namespace LoginManager
        {
            /// <summary>
            /// watcher()のthreadバージョン
            /// </summary>
            public class WatchController
            {
                //protected Timer timer;
                protected System.Windows.Forms.Timer timer;
                protected int msec ;
                //public TimerCallback timerCallback;
                private status processStatus = status.End;    // 現在のステータス状況を監視
                private stats clientStats = stats.Shutdown;     // 起動/終了のみを監視する
                //private int hungCount;          // 応答なし検出回数をカウント
                public static System.Diagnostics.Process TrickProcess;  // TricksterのプロセスID

                


                /// <summary>
                /// クライアントの起動可否ステータス
                /// </summary>
                public enum stats
                {
                    /// <summary>
                    /// 起動中
                    /// </summary>
                    Start,

                    /// <summary>
                    /// 終了
                    /// </summary>
                    Shutdown
                }

                /// <summary>
                /// プロセスの動作ステータス
                /// </summary>
                public enum status{
                    /// <summary>
                    /// 動作中
                    /// </summary>
                    Running,

                    /// <summary>
                    /// ハングアップ
                    /// </summary>
                    Stop,

                    /// <summary>
                    /// TSLoginManagerによる強制終了時
                    /// </summary>
                    ForceKilled,

                    /// <summary>
                    /// 終了
                    /// </summary>
                    End
                }

                /// <summary>
                /// ゲームクライアントの現在の状態を取得または設定します。
                /// </summary>
                /// <param name="stat">Running/Freeze</param>
                public status ProcessStatus{
                    set
                    {
                        this.processStatus = value;
                    }

                    get
                    {
                        return this.processStatus;
                    }
                }
                
                /// <summary>
                /// ゲームクライアントの起動/終了状態を取得または設定します。
                /// </summary>
                /// <param name="stat">Start/Shutdown</param>
                public stats gameStats
                {
                    set
                    {
                        this.clientStats = value;
                    }

                    get
                    {
                        return this.clientStats;
                    }
                }


                /// <summary>
                /// Initialize
                /// 初期化します。
                /// </summary>
                public WatchController()
                {
                    //this.hungCount = 0;
                    this.ProcessStatus = WatchController.processWatch();
                    //this.timerCallback = new TimerCallback(this.detectHungUp);
                    //this.timer = new Timer(this.timerCallback);
                }

                //static Logger logger = SimpleLogger.setLogger("ProgramController");
                //private static bool _flag_hung_diag = false; // ダイアログ表示中（スリープ処理がぶつからないように）

                /*
                #region detectHungUp()
                /// <summary>
                /// 応答なしを検出します。
                /// </summary>
                public void detectHungUp(object state)
                {
                    if (!SettingController.HungUp.enable)
                    {
                        // フリーズ検出無効時
                        return;
                    }
                    
                    // クライアントの状態判定
                    if (this.gameStats == stats.Shutdown && this.ProcessStatus == status.Running)
                    {
                        this.gameStats = stats.Start;
                        SimpleLogger.WriteLine("Trickster " + this.gameStats);
                    }
                    else if (this.gameStats == stats.Start && this.ProcessStatus == status.End)
                    {
                        this.gameStats = stats.Shutdown;
                        SimpleLogger.WriteLine("Trickster " + this.gameStats);
                    }

                    // フリーズ判定
                    int msec = SettingController.HungUp.sec * 1000;   // 単位ミリ秒
                    this.watcher(timer, msec);
                }
                #endregion

                public void Run()
                {
                    while(true){
                        this.watcher(this.timer, SettingController.HungUp.sec);
                    }
                }
                */

                public void setTimer(System.Windows.Forms.Timer timer, int msec)
                {
                    this.timer = timer;
                    this.msec = msec;
                }


                #region watcher()
                /// <summary>
                /// トリックスターのクライアントを監視し、一定時間「応答なし」になった場合に
                /// 強制終了を行うか可否の選択と、終了の処理を行います。
                /// </summary>
                /// <param name="timer">監視させるTimerクラスを指定</param>
                /// <param name="msec">応答なしと判定する時間（ミリ秒単位）</param>
                //public void watcher(Timer timer, int msec)
                public void watcher(System.Windows.Forms.Timer timer, int msec)
                {
                    this.setTimer(timer, msec);
                }

                /// <summary>
                /// トリックスターのクライアントを監視し、一定時間「応答なし」になった場合に
                /// 強制終了を行うか可否の選択と、終了の処理を行います。
                /// </summary>
                public void watcher()
                {
                    status proc = WatchController.processWatch();
                    if(proc != status.End)
                    {
                        this.gameStats = stats.Start;

                        // 起動中
                        if (this.ProcessStatus != status.Running)
                        {
                            this.ProcessStatus = status.Running;
                            //SimpleLogger.WriteLine("Trickster Client start...");
                        }

                        /*
                         * 1回目の応答なし判定
                         * メモ：1回で応答なしとみなさない理由は、マップ移動やキャラクターデータ
                         * 読み込み時にも一時的に応答なし状態として検出されてしまうため。
                         */
                        if (proc == status.Stop)
                        {
                            // 応答なし検出時
                            SimpleLogger.WriteLine("detected hunged up trickster.");

                            /*
                             * 検出時動作のsleep衝突によるアプリケーション自体のハングアップ回避のため、
                             * 一時的にタイマーを無効化
                             */
                            //timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                            if (this.processStatus == status.Stop)
                                return;

                            this.ProcessStatus = status.Stop;
                            SimpleLogger.WriteLine("sleep " + msec + "msec.");
                            System.Threading.Thread.Sleep(msec);   // sleep
                            SimpleLogger.WriteLine("got up.");
                            this.ProcessStatus = status.Running;

                            proc = WatchController.processWatch();
                            if (proc == status.Running)
                            {
                                this.ProcessStatus = status.Running;
                                SimpleLogger.WriteLine("detected to revive.");
                                // 応答回復の場合は修理再開
                                timer.Enabled = true;
                                return;
                            }

                            /*
                             * 検出閾値時間分sleepしても応答がない場合は
                             * 正式に応答なしと判定する
                             */
                            if (proc == status.Stop)
                            {
                                this.ProcessStatus = status.Stop;
                                // 一時的にtimerを無効化
                                timer.Enabled = false;
                                SimpleLogger.WriteLine("Trickster process is hunged up !!");
                                System.Windows.Forms.DialogResult MsgRes = System.Windows.Forms.MessageBox.Show("Trickster の応答なしを検出しました。" + Environment.NewLine + "ゲームを強制終了しますか？", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                                if (MsgRes == System.Windows.Forms.DialogResult.Yes)
                                {
                                    SimpleLogger.WriteLine("Trickster ProcessID: " + WatchController.TrickProcess.Id);

                                    try
                                    {
                                        WatchController.TrickProcess.Kill();
                                        SimpleLogger.WriteLine("ProcessID: " + WatchController.TrickProcess.Id + " is killed.");
                                        this.ProcessStatus = status.ForceKilled;
                                    }
                                    catch (System.InvalidOperationException ioe)
                                    {
                                        SimpleLogger.WriteLine(ioe.Message);

                                        SimpleLogger.WriteLine("failed to kill " + WatchController.TrickProcess.Id);
                                        System.Windows.Forms.MessageBox.Show("ゲームの終了処理に失敗しました。", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    }
                                    catch (System.SystemException se)
                                    {
                                        SimpleLogger.WriteLine(se.Message);
                                        SimpleLogger.WriteLine("failed to kill " + WatchController.TrickProcess.Id);
                                        System.Windows.Forms.MessageBox.Show(se.Message, "Exceptional error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                        System.Windows.Forms.MessageBox.Show("ゲームの終了処理に失敗しました。", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    SimpleLogger.WriteLine("ignore hungup");
                                    //this.hungCount = 0;
                                    this.ProcessStatus = status.Running;
                                }

                                timer.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        // 終了時
                        // 起動した形跡があったらタスクバーの表示を試みる
                        if (this.gameStats == stats.Start && (this.ProcessStatus == status.Running || this.ProcessStatus == status.ForceKilled))
                        {
                            SimpleLogger.WriteLine("try to recover taskbar.");
                            ProgramController.showTaskBar();
                        }
                        this.processStatus = status.End;
                        this.gameStats = stats.Shutdown;

                    }
                }
                #endregion

                /// <summary>
                /// トリックスタークライアントのプロセスの状態を監視します。
                /// </summary>
                /// <returns>プロセスの状態をstatus型で返します。(Running/Stop/End)</returns>
                private static status processWatch()
                {
                    int hWnd = 0;
                    //StringBuilder sbClassName = new StringBuilder(256);
                    //Win32API.GetClassName(hwnd, sbClassName, sbClassName.Capacity);

                    // Trickster.binのウィンドウを監視
                    hWnd = Win32API.FindWindow(Common.ClientWindowClassHandleName, null);
                    if (hWnd > 0)
                    {
                        if (Win32API.IsHungAppWindow((IntPtr)hWnd))
                        {
                            return status.Stop;
                        }

                        return status.Running;
                    }
                    else
                    {
                        return status.End;
                    }
                }

            }


            /// <summary>
            /// プログラムの動作に必要な部分
            /// </summary>
            public class ProgramController
            {

                public static AccountController AController = new AccountController();
                //public static WatchController Watcher = new WatchController();

                #region showTaskBar()
                /// <summary>
                /// タスクバーの回復用
                /// </summary>
                public static void showTaskBar()
                {
                    int intRet = Win32API.FindWindow("Shell_TrayWnd", "");
                    Win32API.SetWindowPos(intRet, 0, 0, 0, 0, 0, Win32API.SWP_SHOWWINDOW);
                }
                #endregion
                #region UrlEncode()
                /// <summary>
                /// URLエンコード
                /// </summary>
                /// <param name="s">エンコードする文字列</param>
                /// <param name="enc">エンコーディング</param>
                /// <returns></returns>
                public static string UrlEncode(string s, Encoding enc)
                {
                    StringBuilder rt = new StringBuilder();
                    foreach (byte i in enc.GetBytes(s))
                        if (i == 0x20)
                            rt.Append('+');
                        else if (i >= 0x21 && i <= 0x7e)
                            rt.Append((char)i);
                        else
                            rt.Append("%" + i.ToString("X2"));
                    return rt.ToString();
                }
                #endregion
                #region UrlDecode()
                /// <summary>
                /// 
                /// </summary>
                /// <param name="s">デコードする文字列</param>
                /// <param name="enc">エンコーディング</param>
                /// <returns></returns>
                public static string UrlDecode(string s, Encoding enc)
                {
                    List<byte> bytes = new List<byte>();
                    for (int i = 0; i < s.Length; i++)
                    {
                        char c = s[i];
                        if (c == '%')
                            bytes.Add(byte.Parse(s[++i].ToString() + s[++i].ToString()));
                        else if (c == '+')
                            bytes.Add((byte)0x20);
                        else
                            bytes.Add((byte)c);
                    }
                    return enc.GetString(bytes.ToArray(), 0, bytes.Count);
                }
                #endregion
                /*
                public static void startWatch()
                {
                    Watcher.timer = new Timer(ProgramController.Watcher.timerCallback, null, 0, 1000);
                    Watcher.timerCallback = new TimerCallback(ProgramController.Watcher.detectHungUp);
                    Thread th = new Thread(new ThreadStart(Watcher.Run));
                }
                */
                #region 旧監視システム
                /*
                //static Logger logger = SimpleLogger.setLogger("ProgramController");
                // Trickster.binが起動しているかを監視するためのフラグ
                private static bool _flg_start_ts = false;      // 起動中判定用
                private static bool _flag_hung_ts_diag = false; // ダイアログ表示中（スリープ処理がぶつからないように）
                private static bool _flag_sleeping = false;     // スリープ中判定
                private static bool _flag_ts_run_switch = false;  // 起動中スイッチ
                public static System.Diagnostics.Process TrickProcess;

                #region detectHungUp()
                /// <summary>
                /// 
                /// </summary>
                private void detectHungUp()
                {
                    if (!SettingController.HungUp.enable)
                    {
                        // フリーズ検出無効時
                        return;
                    }
                    string status = "";

                    // クライアントの状態判定
                    if (ProgramController.getChangedClientStatus(ref status))
                    {
                        SimpleLogger.WriteLine("Trickster " + status);
                    }
                    // フリーズ判定
                    int msec = SettingController.HungUp.sec * 1000;   // 単位ミリ秒
                    ProgramController.Watcher(timer, msec);
                }
                #endregion


                #region getChangedClientStatus()
                /// <summary>
                /// クライアントの状態を取得します。
                /// </summary>
                /// <param name="status">状態: Start:Shutdown</param>
                /// <returns>成功時にtrue, 失敗時にfalseを返します。</returns>
                public static bool getChangedClientStatus(ref string status)
                {
                    if (_flg_start_ts && !_flag_ts_run_switch)
                    {
                        _flag_ts_run_switch = true;
                        status = "Start";
                        return true;
                    }
                    else if (!_flg_start_ts && _flag_ts_run_switch)
                    {
                        _flag_ts_run_switch = false;
                        status = "Shutdown";
                        return true;
                    }

                    return false;
                }
                #endregion

                #region watcher()
                /// <summary>
                /// トリックスターのクライアントを監視し、一定時間「応答なし」になった場合に
                /// 強制終了を行うか可否の選択と、終了の処理を行います。
                /// </summary>
                /// <param name="timer">監視させるTimerクラスを指定</param>
                /// <param name="msec">応答なしと判定する時間（ミリ秒単位）</param>
                public static void watcher(System.Threading.Timer timer, int msec)
                {
                    int hWnd = 0;
                    //StringBuilder sbClassName = new StringBuilder(256);
                    //Win32API.GetClassName(hwnd, sbClassName, sbClassName.Capacity);

                    // Trickster.binのウィンドウを監視
                    hWnd = Win32API.FindWindow("classTrickster", null);
                    if (hWnd > 0)
                    {
                        // 起動中
                        if (!_flg_start_ts)
                        {
                            _flg_start_ts = true;
                        }

                         // 1回目の応答なし判定
                         // メモ：1回で応答なしとみなさない理由は、マップ移動やキャラクターデータ
                         // 読み込み時にも一時的に応答なし状態として検出されてしまうため。
                        if (Win32API.IsHungAppWindow((IntPtr)hWnd))
                        {
                            // 応答なし検出時
                            SimpleLogger.WriteLine("detected hunged up trickster.");

                             // 検出時動作のsleep衝突によるアプリケーション自体のハングアップ回避のため、
                             // 一時的にタイマーを無効化
                            //timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                            if (_flag_sleeping)
                                return;

                            _flag_sleeping = true;
                            SimpleLogger.WriteLine("sleep " + msec + "msec.");
                            System.Threading.Thread.Sleep(msec);   // sleep
                            SimpleLogger.WriteLine("get upped.");
                            _flag_sleeping = false;


                            if (!Win32API.IsHungAppWindow((IntPtr)hWnd))
                            {
                                SimpleLogger.WriteLine("detected to revive.");
                                // 応答回復の場合は修理再開
                                return;
                            }

                             // 検出閾値時間分sleepしても応答がない場合は
                             // 正式に応答なしと判定する
                             
                            if (_flag_hung_ts_diag == false)
                            {
                                _flag_hung_ts_diag = true;
                                // 一時的にtimerを無効化
                                SimpleLogger.WriteLine("Trickster process is hunged up !!");
                                System.Windows.Forms.DialogResult MsgRes = System.Windows.Forms.MessageBox.Show("Trickster の応答なしを検出しました。" + Environment.NewLine + "ゲームを強制終了しますか？", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                                if (MsgRes == System.Windows.Forms.DialogResult.Yes)
                                {
                                    //uint processId = 0, threadId = 0;
                                    //threadId = Win32API.GetWindowThreadProcessId((IntPtr)hWnd, out processId);

                                    SimpleLogger.WriteLine("Trickster ProcessID: " + ProgramController.TrickProcess.Id);

                                    //System.Diagnostics.Process prc = System.Diagnostics.Process.GetProcessById((int)processId);
                                    try
                                    {
                                        ProgramController.TrickProcess.Kill();
                                        SimpleLogger.WriteLine("ProcessID: " + ProgramController.TrickProcess.Id + " is killed.");
                                        //prc.Kill();
                                    }
                                    catch (System.InvalidOperationException ioe)
                                    {
                                        SimpleLogger.WriteLine(ioe.Message);

                                        //MessageBox.Show(ioe.Message);
                                        SimpleLogger.WriteLine("failed to kill " + ProgramController.TrickProcess.Id);
                                        System.Windows.Forms.MessageBox.Show("ゲームの終了処理に失敗しました。", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    }
                                    catch (System.SystemException se)
                                    {
                                        SimpleLogger.WriteLine(se.Message);
                                        SimpleLogger.WriteLine("failed to kill " + ProgramController.TrickProcess.Id);
                                        System.Windows.Forms.MessageBox.Show(se.Message, "Exceptional error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                        System.Windows.Forms.MessageBox.Show("ゲームの終了処理に失敗しました。", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    SimpleLogger.WriteLine("ignore hungup");
                                    _flag_hung_ts_diag = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        // 終了時
                        // 起動した形跡があったらタスクバーの表示を試みる
                        if (_flg_start_ts)
                        {
                            SimpleLogger.WriteLine("try to recover taskbar.");
                            ProgramController.showTaskBar();
                            _flg_start_ts = false;
                        }
                    }
                }
                #endregion
                */
                #endregion
                #region getProcessId()
                /*
                /// <summary>
                /// 起動しているウィンドウハンドルからプロセスIDを取得する
                /// </summary>
                /// <param name="hWnd">ウィンドウハンドル</param>
                /// <returns>プロセスID</returns>
                public static uint getProcessId(IntPtr hWnd)
                {
                    uint procId = 0;
                    foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
                    {
                        if (p.MainWindowHandle != IntPtr.Zero)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(p.MainWindowTitle, @"^Trickster -"))
                            {
                                string processName = p.ProcessName;
                                break;
                            }

                        }
                    }

                    Win32API.GetWindowThreadProcessId(hWnd, procId);
                    return procId;
                }
                */
                #endregion


            }

        }
    }
}
