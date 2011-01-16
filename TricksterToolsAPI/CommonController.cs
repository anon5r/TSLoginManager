using System;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Win32;
using TricksterTools.CommonXmlStructure;
using TricksterTools.Debug;
// Update時の権限昇格に使う
using System.Diagnostics;
using System.Security;
using System.Security.Principal;


namespace TricksterTools
{
    namespace API
    {
        namespace Controller
        {
            /// <summary>
            /// 共通仕様
            /// </summary>
            public class Common
            {

                //public static string ClientWindowClassHandleName = "classTrickster";
                public static string ClientWindowClassHandleName = "xmflrtmxj";

                //static Logger logger = SimpleLogger.setLogger("Common");
                #region 暗号化関係
                /// <summary>
                /// 文字列を暗号化する
                /// </summary>
                /// <param name="str">暗号化する文字列</param>
                /// <param name="key">パスワード</param>
                /// <returns>暗号化された文字列</returns>
                public static string EncryptString(string str, string key)
                {
                    //文字列をバイト型配列にする
                    byte[] bytesIn = System.Text.Encoding.UTF8.GetBytes(str);

                    //DESCryptoServiceProviderオブジェクトの作成
                    System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();

                    //共有キーと初期化ベクタを決定
                    //パスワードをバイト配列にする
                    byte[] bytesKey = System.Text.Encoding.UTF8.GetBytes(key);
                    //共有キーと初期化ベクタを設定
                    des.Key = ResizeBytesArray(bytesKey, des.Key.Length);
                    des.IV = ResizeBytesArray(bytesKey, des.IV.Length);

                    //暗号化されたデータを書き出すためのMemoryStream
                    MemoryStream msOut = new System.IO.MemoryStream();
                    //DES暗号化オブジェクトの作成
                    System.Security.Cryptography.ICryptoTransform desdecrypt = des.CreateEncryptor();
                    //書き込むためのCryptoStreamの作成
                    System.Security.Cryptography.CryptoStream cryptStreem = new System.Security.Cryptography.CryptoStream(msOut, desdecrypt, System.Security.Cryptography.CryptoStreamMode.Write);
                    //書き込む
                    cryptStreem.Write(bytesIn, 0, bytesIn.Length);
                    cryptStreem.FlushFinalBlock();
                    //暗号化されたデータを取得
                    byte[] bytesOut = msOut.ToArray();

                    //閉じる
                    cryptStreem.Close();
                    msOut.Close();

                    //Base64で文字列に変更して結果を返す
                    return System.Convert.ToBase64String(bytesOut).Replace("=", "");
                }

                /// <summary>
                /// 暗号化された文字列を復号化する
                /// </summary>
                /// <param name="str">暗号化された文字列</param>
                /// <param name="key">パスワード</param>
                /// <returns>復号化された文字列</returns>
                public static string DecryptString(string str, string key)
                {
                    if (str == null) { return ""; }
                    if (str.Length % 4 > 0)
                    {
                        while (str.Length % 4 != 0)
                        {
                            str += "=";
                        }

                    }
                    //DESCryptoServiceProviderオブジェクトの作成
                    System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();

                    //共有キーと初期化ベクタを決定
                    //パスワードをバイト配列にする
                    byte[] bytesKey = System.Text.Encoding.UTF8.GetBytes(key);
                    //共有キーと初期化ベクタを設定
                    des.Key = ResizeBytesArray(bytesKey, des.Key.Length);
                    des.IV = ResizeBytesArray(bytesKey, des.IV.Length);

                    //Base64で文字列をバイト配列に戻す
                    byte[] bytesIn = System.Convert.FromBase64String(str);
                    //暗号化されたデータを読み込むためのMemoryStream
                    MemoryStream msIn = new System.IO.MemoryStream(bytesIn);
                    //DES復号化オブジェクトの作成
                    System.Security.Cryptography.ICryptoTransform desdecrypt = des.CreateDecryptor();
                    //読み込むためのCryptoStreamの作成
                    System.Security.Cryptography.CryptoStream cryptStreem = new System.Security.Cryptography.CryptoStream(msIn, desdecrypt, System.Security.Cryptography.CryptoStreamMode.Read);

                    //復号化されたデータを取得するためのStreamReader
                    StreamReader srOut =
                        new StreamReader(cryptStreem, System.Text.Encoding.UTF8);
                    //復号化されたデータを取得する
                    string result = srOut.ReadToEnd();

                    //閉じる
                    srOut.Close();
                    cryptStreem.Close();
                    msIn.Close();

                    return result;
                }
                #region ResizeBytesArray()
                /// <summary>
                /// 共有キー用に、バイト配列のサイズを変更する
                /// </summary>
                /// <param name="bytes">サイズを変更するバイト配列</param>
                /// <param name="newSize">バイト配列の新しい大きさ</param>
                /// <returns>サイズが変更されたバイト配列</returns>
                private static byte[] ResizeBytesArray(byte[] bytes, int newSize)
                {
                    byte[] newBytes = new byte[newSize];
                    if (bytes.Length <= newSize)
                    {
                        for (int i = 0; i < bytes.Length; i++)
                            newBytes[i] = bytes[i];
                    }
                    else
                    {
                        int pos = 0;
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            newBytes[pos++] ^= bytes[i];
                            if (pos >= newBytes.Length)
                                pos = 0;
                        }
                    }
                    return newBytes;
                }
                #endregion
                #endregion
                #region Game Client Information
                /// <summary>
                /// インストールされているか調べる
                /// </summary>
                /// <param name="void"></param>
                /// <returns>boolean</returns>
                public static bool isInstalled()
                {
                    // 操作するレジストリ・キーの名前
                    string rKeyName;
                    if (Common.isx64())
                    {
                        SimpleLogger.WriteLine("Running on 64bit OS");
                        rKeyName = @"SOFTWARE\WOW6432Node\NTreev\Trickster_GCrest";
                    }
                    else
                    {
                        SimpleLogger.WriteLine("Running on 32bit OS");
                        rKeyName = @"SOFTWARE\NTreev\Trickster_GCrest";
                    }

                    // 取得処理を行う対象となるレジストリの値の名前
                    //string rGetValueName = "FullPath";

                    // レジストリの取得
                    try
                    {
                        // レジストリ・キーのパスを指定してレジストリを開く
                        RegistryKey rKey = Registry.LocalMachine.OpenSubKey(rKeyName);

                        // レジストリの値を取得
                        //string location = (string)rKey.GetValue(rGetValueName);

                        // 開いたレジストリ・キーを閉じる
                        rKey.Close();

                        SimpleLogger.WriteLine("Trickster Client is installed.");
                        return true;
                    }
                    catch (NullReferenceException)
                    {
                        // レジストリ・キーまたは値が存在しない
                        SimpleLogger.WriteLine("Trickster Client is not installed.");
                        return false;
                    }
                }

                /// <summary>
                /// インストール先パスを取得
                /// </summary>
                /// <param name="void"></param>
                /// <returns>インストール先パス</returns>
                public static string getInstallPath()
                {
                    string rKeyName;
                    // 操作するレジストリ・キーの名前
                    if (Common.isx64())
                    {
                        rKeyName = @"SOFTWARE\WOW6432Node\NTreev\Trickster_GCrest";
                    }
                    else
                    {
                        rKeyName = @"SOFTWARE\NTreev\Trickster_GCrest";
                    }
                    // 取得処理を行う対象となるレジストリの値の名前
                    string rGetValueName = "FullPath";

                    SimpleLogger.WriteLine("get client installed path...");
                    // レジストリの取得
                    try
                    {
                        // レジストリ・キーのパスを指定してレジストリを開く
                        RegistryKey rKey = Registry.LocalMachine.OpenSubKey(rKeyName);

                        // レジストリの値を取得
                        string location = (string)rKey.GetValue(rGetValueName);

                        // 開いたレジストリ・キーを閉じる
                        rKey.Close();

                        // 取得したレジストリの値return
                        return location;
                    }
                    catch (NullReferenceException)
                    {
                        SimpleLogger.WriteLine("failed to get client installed path.");
                        // レジストリ・キーまたは値が存在しない
                        MessageBox.Show("インストールパスの取得に失敗しました", "ランチャー起動中", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return @"C:\Games\Trickster\Splash.exe";
                    }
                }

                /// <summary>
                /// クライアントのバージョンを取得
                /// </summary>
                /// <param name="void"></param>
                /// <returns>バージョン番号（例：2.13.2）</returns>
                public static string getClientVersion()
                {
                    SimpleLogger.WriteLine("get client version...");
                    // 操作するレジストリ・キーの名前
                    string RegKeyName;
                    if (Common.isx64())
                    {
                        RegKeyName = @"SOFTWARE\WOW6432Node\Ntreev\Trickster_GCrest";
                    }
                    else
                    {
                        RegKeyName = @"SOFTWARE\Ntreev\Trickster_GCrest";
                    }
                    // 取得処理を行う対象となるレジストリの値の名前
                    string rGetValueName = "UpdateVersion";

                    // レジストリの取得
                    try
                    {
                        // レジストリ・キーのパスを指定してレジストリを開く
                        RegistryKey RegKey = Registry.LocalMachine.OpenSubKey(RegKeyName);

                        // レジストリの値を取得
                        string RegValue = (string)RegKey.GetValue(rGetValueName);

                        // 開いたレジストリ・キーを閉じる
                        RegKey.Close();

                        // コンソールに取得したレジストリの値を表示
                        SimpleLogger.WriteLine("client version: " + RegValue);
                        return RegValue;
                    }
                    catch (NullReferenceException)
                    {
                        SimpleLogger.WriteLine("failed to get client version.");
                        // レジストリ・キーまたは値が存在しない
                        return "0.0.0";
                    }

                }


                public static string TricksterVersionfromServer = "0.0.0";


                /// <summary>
                /// サーバ側バージョンを取得
                /// </summary>
                /// <param name="void"></param>
                /// <returns>バージョン番号（例：2.13.2）</returns>
                public static string getServerVersion()
                {
                    SimpleLogger.WriteLine("get version from server...");
                    // レジストリの取得
                    try
                    {

                        string verInfo = getServerInfo();   // 受信

                        // バージョン番号とファイルリストファイル名を取得
                        System.Text.RegularExpressions.Regex RegexVer = new System.Text.RegularExpressions.Regex("version = (?<version>.+)\r\n");
                        System.Text.RegularExpressions.Regex RegexFlist = new System.Text.RegularExpressions.Regex("filelist = (?<flist>filelist.+)\r\n");
                        System.Text.RegularExpressions.Match MatchVer = RegexVer.Match(verInfo);
                        System.Text.RegularExpressions.Match MatchFlist = RegexFlist.Match(verInfo);
                        if (MatchVer.Success)
                        {
                            SimpleLogger.WriteLine("server version: " + MatchVer.Groups["version"].Value);
                            return MatchVer.Groups["version"].Value;
                        }
                        else
                        {
                            // 何かおかしい

                            SimpleLogger.WriteLine("failed to get version info.");

                            return "0.0.0";
                        }
                    }
                    catch (WebException we)
                    {
                        // 通信失敗
                        SimpleLogger.WriteLine(we.Message);
                        MessageBox.Show("通信失敗", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SimpleLogger.WriteLine("could not get new version from server.");
                        return "0.0.0";
                    }
                }


                public static string getServerInfo()
                {
                    //string url = "http://version.trickster.jp:5977/version/version";
                    string url = "http://patch.trickster.jp/version/version";

                    //WebRequestの作成
                    System.Net.HttpWebRequest WebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                    WebRequest.Timeout = 5000; // タイムアウト5秒
                    System.Net.HttpWebResponse WebResponse = null;
                    try
                    {
                        // サーバーからの応答を受信するためのWebResponseを取得
                        WebResponse = (System.Net.HttpWebResponse)WebRequest.GetResponse();


                        // 応答したURIを表示する
                        SimpleLogger.WriteLine("ResponseURL: " + WebResponse.ResponseUri.ToString());
                        // 応答ステータスコードを表示する
                        SimpleLogger.WriteLine("Status: " + (int)WebResponse.StatusCode + " " + WebResponse.StatusCode.ToString());

                        if ((int)WebResponse.StatusCode <= 300)
                        {
                            // 文字コードを指定する
                            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

                            // 応答データを受信するためのStreamを取得
                            Stream stream = WebResponse.GetResponseStream();
                            StreamReader StreamReader = new StreamReader(stream, enc);

                            string VersionInfo = StreamReader.ReadToEnd();   // 受信
                            SimpleLogger.WriteLine(VersionInfo);

                            // 閉じる
                            WebResponse.Close();

                            return VersionInfo;
                        }
                        else
                        {
                            SimpleLogger.WriteLine((int)WebResponse.StatusCode + ":" + WebResponse.StatusDescription);

                            // 閉じる
                            WebResponse.Close();
                            return "0.0.0";
                        }
                    }
                    catch (WebException we)
                    {
                        SimpleLogger.WriteLine(we.Message);
                        MessageBox.Show("例外エラー:" + Environment.NewLine + we.Message, "WebException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return "0.0.0";
                    }
                }

                /// <summary>
                /// バージョン情報の比較し、アップデートがあるか判定します。
                /// </summary>
                /// <param name="clientVersion">クライアント側バージョン(1.2.3)</param>
                /// <param name="serverVersion">サーバ側バージョン(1.2.3)</param>
                /// <returns></returns>
                public static bool isNeedUpdate(string clientVersion, string serverVersion)
                {
                    SimpleLogger.WriteLine("check update...");

                    if(clientVersion == null)
                    {
                        SimpleLogger.WriteLine("client version is null.");
                        clientVersion = "0.0.0";
                    }

                    if (serverVersion == null || serverVersion == "0.0.0")
                    {
                        SimpleLogger.WriteLine("failed to check version.");
                        return false;
                    }
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[0-9.]+$");
                    //if (clientVersion == "初実行")
                    if ( regex.IsMatch(clientVersion, 0) == false )
                    {
                        clientVersion = "0.0.0";
                    }
                    string[] clv = clientVersion.Split('.');
                    string[] svv = serverVersion.Split('.');

                    int major_cl, major_sv, minor_cl, minor_sv, sub_cl, sub_sv;
                    major_cl = System.Convert.ToInt32(clv[0]);
                    minor_cl = System.Convert.ToInt32(clv[1]);
                    sub_cl = System.Convert.ToInt32(clv[2]);
                    major_sv = System.Convert.ToInt32(svv[0]);
                    minor_sv = System.Convert.ToInt32(svv[1]);
                    sub_sv = System.Convert.ToInt32(svv[2]);

                    SimpleLogger.WriteLine("comparering version...");
                    if (clv.Length < 3 || svv.Length < 3)
                    {
                        SimpleLogger.WriteLine("failed to comparer version");
                        return false;
                    }

                    SimpleLogger.WriteLine("Server: " + serverVersion);
                    SimpleLogger.WriteLine("Client: " + clientVersion);

                    // major version
                    if (major_cl < major_sv)
                    {
                        SimpleLogger.WriteLine("need update.");
                        return true;
                    }

                    // minor version
                    if (minor_cl < minor_sv)
                    {
                        SimpleLogger.WriteLine("need update.");
                        return true;
                    }

                    // sub version
                    if (sub_cl < sub_sv)
                    {
                        SimpleLogger.WriteLine("need update.");
                        return true;
                    }

                    SimpleLogger.WriteLine("no update.");
                    return false;
                }

                /// <summary>
                /// x64環境での動作か判定します。
                /// </summary>
                /// <returns>64bit OSの場合はtrue</returns>
                public static bool isx64()
                {
                    // 現在のプラットフォームでのポインタまたはハンドルのサイズ (バイト)。
                    // このプロパティの値は 32 ビット プラットフォームでは 4、64 ビット プラットフォームでは 8 です。
                    return (IntPtr.Size == 8);
                }
                #endregion

                /// <summary>
                /// ゲームのアップデートを行います。
                /// アップデート後、起動されたランチャープログラムは自動的に終了します。
                /// </summary>
                public static void updateGame()
                {
                    string splash_path = Common.getInstallPath();
                    if (!System.IO.File.Exists(splash_path))
                    {
                        MessageBox.Show("ランチャープログラム \"" + splash_path + "\" が見つかりませんでした。", "Error");
                        return;
                    }
                    Common.updateGame(splash_path);
                }

                /// <summary>
                /// ゲームのアップデートを行います。
                /// アップデート後、起動されたランチャープログラムは自動的に終了します。
                /// </summary>
                /// <param name="LauncherPath">ランチャー（Splash.exe）へのフルパス</param>
                public static void updateGame(string LauncherPath)
                {
                    // Splash.dmyを直接起動
                    LauncherPath = LauncherPath.Replace(".exe", ".dmy");

                    bool ignoreDialog = false;

                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                    psi.FileName = LauncherPath;
                    psi.RedirectStandardInput = false;
                    psi.RedirectStandardOutput = false;
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = false;
                    // 現在のユーザが管理者権限を有しているか確認
                    if (Common.isAdmin() == false)
                    {
                        // 管理者でなければ管理者権限で実行を試みる
                        psi.Verb = "runas";
                    }
                    
                    SimpleLogger.WriteLine(LauncherPath);
                    // 引数
                    //psi.Arguments = "";

                    SimpleLogger.WriteLine("start Launcher and launch Update.");

                    try
                    {
                        // 起動
                        System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);

                        // ランチャーのプロセスを監視
                        System.Diagnostics.PerformanceCounter pc = new System.Diagnostics.PerformanceCounter();
                        pc.CategoryName = "Process";
                        pc.CounterName = "% Processor Time";
                        pc.MachineName = ".";
                        pc.InstanceName = p.ProcessName;
                        float pcp = 0;
                        System.Threading.Thread.Sleep(1000);
                        if (!p.HasExited)
                        {
                            try
                            {
                                pcp = pc.NextValue();
                            }
                            catch (InvalidOperationException ioe)
                            { }
                        }

                        // 最初は0%で検出が終わるまで
                        while (!p.HasExited && pcp == 0)
                        {
                            pcp = pc.NextValue();
                            System.Threading.Thread.Sleep(1000);
                        }

                        bool isContinueLauncherMonitor = true;
                        while (isContinueLauncherMonitor)
                        {
                            // 処理終了(CPU使用率0%)まで監視
                            int zeroCount = 0;
                            while (!p.HasExited && zeroCount < 10)
                            {
                                pcp = pc.NextValue();
                                SimpleLogger.WriteLine(p.ProcessName + ": " + pcp.ToString() + "%");
                                System.Threading.Thread.Sleep(1000);
                                if (pcp > 0)
                                {
                                    zeroCount = 0;
                                }
                                else
                                {
                                    zeroCount++;
                                }
                            }

                            if (p.HasExited == true)
                            {
                                // 既にランチャーが終了していた場合
                                isContinueLauncherMonitor = false;
                                break;
                            }

                            DialogResult killConfirm = new DialogResult();

                            if (ignoreDialog == false)
                            {
                                killConfirm = MessageBox.Show("ランチャーを終了します。よろしいですか" + Environment.NewLine +
                                            "今後このメッセージを表示しない場合はキャンセルを選択してください。" + Environment.NewLine +
                                            "※次回アップデート実行時には再度表示されます。", "TricksterTools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            }

                            if (killConfirm == DialogResult.Cancel)
                            {
                                ignoreDialog = true;
                                MessageBox.Show("アップデート完了後は、一度ランチャーを終了させてください。" + Environment.NewLine +
                                            "TSLoginManagerが正常に動作出来なくなる恐れがあります。", "TricksterTools", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            if (p.HasExited == true || killConfirm == DialogResult.Yes)
                            {
                                isContinueLauncherMonitor = false;

                                if (p.HasExited == false)
                                    p.Kill();


                                break;
                            }
                        }

                        p.Dispose();
                    }
                    catch (System.ComponentModel.Win32Exception e)
                    {
                        SimpleLogger.WriteLine("Exception!!: " + e.GetType().FullName);
                        SimpleLogger.WriteLine(e.Message);
                        MessageBox.Show("アップデートの実行には管理者権限が必要です。\n処理を中断します。", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                /// <summary>
                /// 現在管理者権限で実行されているか確認します
                /// </summary>
                /// <returns>管理者権限の場合はtrue, 一般ユーザの場合はfalse</returns>
                public static bool isAdmin()
                {
                    WindowsIdentity usrId = WindowsIdentity.GetCurrent();
                    WindowsPrincipal p = new WindowsPrincipal(usrId);
                    return p.IsInRole(@"BUILTIN\Administrators");
                }

                /// <summary>
                /// 
                /// </summary>
                public static void RestartApplicationAtAdministratorAuthority()
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.UseShellExecute = true;
                    startInfo.WorkingDirectory = Environment.CurrentDirectory;
                    startInfo.FileName = Application.ExecutablePath;
                    startInfo.Verb = "runas";

                    try
                    {
                        Process p = Process.Start(startInfo);
                    }
                    catch
                    {
                        return;
                    }
                    Application.Exit();
                }
            }
        }
    }
}
