using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
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
            /// LoginController interface
            /// ゲームへのログイン、ゲームの起動を定義します。
            /// </summary>
            interface ILoginController
            {
                /// <summary>
                /// IDとパスワードを使用してゲームの起動を実装します。
                /// </summary>
                /// <param name="id">アカウントID</param>
                /// <param name="password">アカウントパスワード</param>
                void startGame(string id, string password);
            }

            /// <summary>
            /// ログインおよびゲームクライアントの起動処理
            /// 抽象クラス
            /// </summary>
            public abstract class LoginController
            {
                //static Logger logger = SimpleLogger.setLogger("LoginManager");

                
                public static readonly string __USER_AGENT__ = "Mozilla/4.0 (TricksterTools/" + Application.ProductVersion + "; +http://trickster.anoncom.net/TrickToolsInfo.html)";

                private string gameStartID;
                private string gameStartKey;
                private HttpWebRequest webReq;
                private HttpWebResponse webRes;
                private CookieContainer cc;
                private Encoding enc = Encoding.GetEncoding("Shift_JIS");
                private string resString;
                private byte[] data;

                public enum RequestMethod
                {
                    /// <summary>
                    /// GET
                    /// </summary>
                    GET,

                    /// <summary>
                    /// POST
                    /// </summary>
                    POST,

                    /// <summary>
                    /// FILE (恐らく未使用)
                    /// </summary>
                    FILE,
                }

                protected internal static TricksterTools.Plugins.IPluginHost pluginHost;

                public static void setPluginHost(TricksterTools.Plugins.IPluginHost hp)
                {
                    pluginHost = hp;
                }


                public LoginController(){}

                /// <summary>
                /// IDとパスワードを使用してゲームを起動します。
                /// ゲームクライアントのアップデートがある場合にはランチャーを起動します。
                /// </summary>
                /// <param name="id">アカウントID</param>
                /// <param name="password">アカウントパスワード</param>
                public static void startGame(string id, string password)
                {
                    /// TODO: ログイン処理の実装や、ゲーム起動の実装を行う
                }

                /// <summary>
                /// ネットワークに接続できるか試みます
                /// </summary>
                /// <returns>true = 接続可能  / false = 接続不可</returns>
                public static bool isAliveNetwork()
                {
                    int inetFlg;
                    return WinInet.InternetGetConnectedState(out inetFlg, 0);
                }

                protected internal void initialize()
                {
                    this.cc = new CookieContainer();
                }

                /// <summary>
                /// <value>url</value>に対して<value>method</value>でリクエストを行います。
                /// </summary>
                /// <param name="url">URL</param>
                /// <param name="method">リクエストメソッド(GET/POST)</param>
                /// <param name="param">リクエスト時のパラメータ(ない場合はnull)</param>
                /// <param name="timeout">タイムアウトを指定</param>
                /// <param name="referer">リファラ 指定しない場合はnull</param>
                /// <returns>WebRequest</returns>
                protected internal void doRequest(string url, RequestMethod method, string param, int timeout, string referer)
                {
                    try
                    {
                        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                        req.Method = method.ToString();
                        req.UserAgent = LoginController.__USER_AGENT__;
                        req.Headers.Add(HttpRequestHeader.AcceptLanguage, "ja");
                        req.Headers.Add(HttpRequestHeader.AcceptEncoding, "*/*");
                        req.KeepAlive = true;
                        req.AllowAutoRedirect = true;
                        if (referer != null && referer.Length > 0)
                        {
                            req.Referer = referer;
                        }
                        req.Timeout = timeout;
                        if (param != null && param.Length > 0)
                        {
                            data = System.Text.Encoding.ASCII.GetBytes(param);
                            req.ContentLength = data.Length;
                        }
                        if (method == RequestMethod.POST)
                        {
                            req.ContentType = "application/x-www-form-urlencoded";
                        }
                        req.KeepAlive = true;
                        //req.AllowAutoRedirect = true;
                        req.AllowAutoRedirect = false;
                        if (this.cc != null && this.cc.Count > 0)
                        {
                            //req.CookieContainer = new CookieContainer();
                            //req.CookieContainer.Add(this.cc.GetCookies(req.RequestUri));
                            req.CookieContainer = this.cc;
                        }
                        else
                        {
                            this.cc = new CookieContainer();
                            req.CookieContainer = this.cc;
                        }

                        SimpleLogger.WriteLine("[ Request ] ----------------------------------------");
                        SimpleLogger.WriteLine("URL: " + url);
                        SimpleLogger.WriteLine("Method: " + req.Method);
                        if (param != null && param.Length > 0)
                        {
                            SimpleLogger.WriteLine("Query: " + param);
                        }
                        SimpleLogger.WriteLine(req.Headers.ToString().Replace("\n\r", Environment.NewLine));
                        SimpleLogger.WriteLine("----------------------------------------------------");

                        if (method == RequestMethod.POST)
                        {
                            // ポスト・データの書き込み
                            Stream reqStream = req.GetRequestStream();
                            reqStream.Write(data, 0, data.Length);
                            reqStream.Close();
                        }

                        this.webReq = req;
                    }
                    catch (System.NotSupportedException nse)
                    {
                        throw nse;
                    }
                    catch (System.ArgumentException ae)
                    {
                        throw ae;
                    }
                    catch (System.Security.SecurityException se)
                    {
                        throw se;
                    }
                }

                /// <summary>
                /// リクエストに対するレスポンスを取得します。
                /// </summary>
                /// <returns></returns>
                protected internal HttpWebResponse getResponse()
                {
                    HttpWebResponse res = (HttpWebResponse)this.webReq.GetResponse();
                    SimpleLogger.WriteLine("[ Response ] ---------------------------------------");
                    SimpleLogger.WriteLine("Status: " + (int)res.StatusCode + " " + res.StatusCode.ToString());
                    SimpleLogger.WriteLine(res.Headers.ToString().Replace("\n\r", Environment.NewLine));

                    //受信したCookieのコレクションを取得する
                    if (this.webReq.CookieContainer != null && this.webReq.CookieContainer.GetCookies(this.webReq.RequestUri).Count > 0)
                    {
                        SimpleLogger.Write( "Cookies: { " );
                        // CookieがあればCookie名と値を列挙する
                        foreach (Cookie cook in this.webReq.CookieContainer.GetCookies(this.webReq.RequestUri))
                        {
                            SimpleLogger.Write("{0}=\"{1}\"; ", cook.Name, cook.Value);
                        }
                        SimpleLogger.Write("}" + Environment.NewLine);
                        //取得したCookieを保存しておく
                        //this.cc.Add(this.webReq.CookieContainer.GetCookies(this.webReq.RequestUri));
                        this.cc = this.webReq.CookieContainer;
                        res.Cookies = this.webReq.CookieContainer.GetCookies(this.webReq.RequestUri);
                    }
                    SimpleLogger.WriteLine("----------------------------------------------------");
                    



                    Stream resStream = res.GetResponseStream();
                    StreamReader sr = new StreamReader(resStream, enc);
                    this.resString = sr.ReadToEnd();
                    sr.Close();
                    resStream.Close();

                    this.webRes = res;
                    res.Close();

                    return this.webRes;
                }

                /// <summary>
                /// クッキーからゲーム起動用IDとキーを解析します
                /// </summary>
                /// <param name="cookies"></param>
                protected internal void parseCookies(string cookies)
                {
                    string id;
                    string key;
                    int idxStart, idxEnd;
                    idxStart = cookies.IndexOf("TrickLaunch[ID]=");
                    idxEnd = cookies.IndexOf(';', cookies.IndexOf("TrickLaunch[ID]=")) - idxStart;
                    id = cookies.Substring(idxStart, idxEnd);
                    idxStart = cookies.IndexOf("TrickLaunch[KEY]=");
                    idxEnd = cookies.IndexOf(';', cookies.IndexOf("TrickLaunch[KEY]=")) - idxStart;
                    key = cookies.Substring(idxStart, idxEnd);
                    id = id.Remove(0, 16);    // TrickLaunch[ID]=  を除去
                    key = key.Remove(0, 17);  // TrickLaunch[KEY]= を除去
                    id = id.Replace("%3A", ":");  // 「%3A」を「:」に置換する

                    this.gameStartID = id;
                    this.gameStartKey = key;
                    
                }

                /// <summary>
                /// ゲームを起動します
                /// </summary>
                protected internal void runGame()
                {
                    // execute launcher
                    string splash_path = Common.getInstallPath();
                    string bin_path = splash_path.Replace("Splash.exe", "Trickster.bin");
                    if (SettingController.GameStartUp.mode == SettingController.RUN_GAME_DIRECT)
                    {
                        SimpleLogger.WriteLine("GameStartUpMode: Direct");
                    }
                    else
                    {
                        SimpleLogger.WriteLine("GameStartUpMode: Launcher");
                        bin_path = splash_path;
                    }

                    string serverVersion = Common.getServerVersion();
                    string clientVersion = Common.getClientVersion();
                    if (Common.isNeedUpdate(clientVersion, serverVersion) && SettingController.GameStartUp.mode == SettingController.RUN_GAME_DIRECT)
                    {
                        SimpleLogger.WriteLine("need update version: " + clientVersion + " -> " + serverVersion);
                        MessageBox.Show("アップデートを行います。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        if (System.IO.File.Exists(splash_path))
                        {
                            Common.updateGame(splash_path);
                            /// プラグインフックの起動
                            TricksterTools.Plugins.IPlugin[] plugins = PluginController.loadPlugins(pluginHost);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.UpdatedGame);
                        }
                    }

                    if (System.IO.File.Exists(bin_path))
                    {
                        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                        psi.FileName = bin_path;
                        psi.RedirectStandardInput = false;
                        psi.RedirectStandardOutput = false;
                        psi.UseShellExecute = false;
                        // ウィンドウを表示しないようにする
                        psi.CreateNoWindow = false;
                        // 引数
                        psi.Arguments = this.gameStartID + "," + this.gameStartKey;
                        SimpleLogger.WriteLine(bin_path + " " + psi.Arguments.ToString());


                        if (Common.isNeedUpdate(clientVersion, serverVersion) && SettingController.GameStartUp.mode == SettingController.RUN_GAME_LAUNCHER)
                        {
                            // 起動
                            WatchController.TrickProcess = System.Diagnostics.Process.Start(psi);

                            SimpleLogger.WriteLine("need update version: " + clientVersion + " -> " + serverVersion);
                            MessageBox.Show("アップデートがあります。\n"
                                + "アップデート完了後、ゲームスタートをする前にOKボタンを押してください。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                            /// プラグインフックの起動
                            TricksterTools.Plugins.IPlugin[] plugins = PluginController.loadPlugins(pluginHost);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.UpdatedGame);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.RunGame);


                        }
                        else
                        {
                            /// プラグインフックの起動
                            TricksterTools.Plugins.IPlugin[] plugins = PluginController.loadPlugins(pluginHost);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.RunGame);


                            // 起動
                            WatchController.TrickProcess = System.Diagnostics.Process.Start(psi);
                        }
                    }
                    else
                    {
                        SimpleLogger.WriteLine("\"" + bin_path + "\" does not exsit.");
                        MessageBox.Show("ゲーム起動に必要なプログラムが存在しないため起動できません。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                /// <summary>
                /// ゲームを起動します
                /// </summary>
                protected internal void runGame(string startID, string startKey)
                {
                    // execute launcher
                    string splash_path = Common.getInstallPath();
                    string bin_path = splash_path.Replace("Splash.exe", "Trickster.bin");
                    if (SettingController.GameStartUp.mode == SettingController.RUN_GAME_DIRECT)
                    {
                        SimpleLogger.WriteLine("GameStartUpMode: Direct");
                    }
                    else
                    {
                        SimpleLogger.WriteLine("GameStartUpMode: Launcher");
                        bin_path = splash_path;
                    }

                    string serverVersion = Common.getServerVersion();
                    string clientVersion = Common.getClientVersion();
                    if (Common.isNeedUpdate(clientVersion, serverVersion) && SettingController.GameStartUp.mode == SettingController.RUN_GAME_DIRECT)
                    {
                        SimpleLogger.WriteLine("need update version: " + clientVersion + " -> " + serverVersion);
                        MessageBox.Show("アップデートがあるためランチャーを起動します。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (File.Exists(splash_path))
                        {
                            Common.updateGame(splash_path);
                            /// プラグインフックの起動
                            TricksterTools.Plugins.IPlugin[] plugins = PluginController.loadPlugins(pluginHost);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.UpdatedGame);
                        }
                    }

                    if (File.Exists(bin_path))
                    {
                        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                        psi.FileName = bin_path;
                        psi.RedirectStandardInput = false;
                        psi.RedirectStandardOutput = false;
                        psi.UseShellExecute = false;
                        // ウィンドウを表示しないようにする
                        psi.CreateNoWindow = false;
                        // 引数
                        psi.Arguments = startID + "," + startKey;
                        SimpleLogger.WriteLine(bin_path + " " + psi.Arguments.ToString());

                        if (Common.isNeedUpdate(clientVersion, serverVersion) && SettingController.GameStartUp.mode == SettingController.RUN_GAME_LAUNCHER)
                        {
                            // 起動
                            WatchController.TrickProcess = System.Diagnostics.Process.Start(psi);

                            SimpleLogger.WriteLine("need update version: " + clientVersion + " -> " + serverVersion);
                            MessageBox.Show("アップデートがあります。\n"
                                + "アップデート完了後、ゲームスタートをする前にOKボタンを押してください。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                            /// プラグインフックの起動
                            TricksterTools.Plugins.IPlugin[] plugins = PluginController.loadPlugins(pluginHost);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.UpdatedGame);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.RunGame);


                        }
                        else
                        {
                            /// プラグインフックの起動
                            TricksterTools.Plugins.IPlugin[] plugins = PluginController.loadPlugins(pluginHost);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.RunGame);


                            // 起動
                            WatchController.TrickProcess = System.Diagnostics.Process.Start(psi);
                        }

                    }
                    else
                    {
                        SimpleLogger.WriteLine("\"" + bin_path + "\" does not exsit.");
                        MessageBox.Show("ゲーム起動に必要なプログラムが存在しないため起動できません。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }







                /// <summary>
                /// HttpWebResponseオブジェクトを閉じます
                /// </summary>
                protected internal void ResponseClose()
                {
                    if (this.webRes != null)
                    {
                        this.webRes.Close();
                    }
                }

                /// <summary>
                /// レスポンスから得られたストリーム文字列を返します。
                /// </summary>
                /// <returns>レスポンス文字列</returns>
                protected internal string getResponseStream(){
                    return this.resString;
                }
            }

        }
    }
}
