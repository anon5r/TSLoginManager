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
            /// �Q�[���ւ̃��O�C���A�Q�[���̋N�����`���܂��B
            /// </summary>
            interface ILoginController
            {
                /// <summary>
                /// ID�ƃp�X���[�h���g�p���ăQ�[���̋N�����������܂��B
                /// </summary>
                /// <param name="id">�A�J�E���gID</param>
                /// <param name="password">�A�J�E���g�p�X���[�h</param>
                void startGame(string id, string password);
            }

            /// <summary>
            /// ���O�C������уQ�[���N���C�A���g�̋N������
            /// ���ۃN���X
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
                    /// FILE (���炭���g�p)
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
                /// ID�ƃp�X���[�h���g�p���ăQ�[�����N�����܂��B
                /// �Q�[���N���C�A���g�̃A�b�v�f�[�g������ꍇ�ɂ̓����`���[���N�����܂��B
                /// </summary>
                /// <param name="id">�A�J�E���gID</param>
                /// <param name="password">�A�J�E���g�p�X���[�h</param>
                public static void startGame(string id, string password)
                {
                    /// TODO: ���O�C�������̎�����A�Q�[���N���̎������s��
                }

                /// <summary>
                /// �l�b�g���[�N�ɐڑ��ł��邩���݂܂�
                /// </summary>
                /// <returns>true = �ڑ��\  / false = �ڑ��s��</returns>
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
                /// <value>url</value>�ɑ΂���<value>method</value>�Ń��N�G�X�g���s���܂��B
                /// </summary>
                /// <param name="url">URL</param>
                /// <param name="method">���N�G�X�g���\�b�h(GET/POST)</param>
                /// <param name="param">���N�G�X�g���̃p�����[�^(�Ȃ��ꍇ��null)</param>
                /// <param name="timeout">�^�C���A�E�g���w��</param>
                /// <param name="referer">���t�@�� �w�肵�Ȃ��ꍇ��null</param>
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
                            // �|�X�g�E�f�[�^�̏�������
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
                /// ���N�G�X�g�ɑ΂��郌�X�|���X���擾���܂��B
                /// </summary>
                /// <returns></returns>
                protected internal HttpWebResponse getResponse()
                {
                    HttpWebResponse res = (HttpWebResponse)this.webReq.GetResponse();
                    SimpleLogger.WriteLine("[ Response ] ---------------------------------------");
                    SimpleLogger.WriteLine("Status: " + (int)res.StatusCode + " " + res.StatusCode.ToString());
                    SimpleLogger.WriteLine(res.Headers.ToString().Replace("\n\r", Environment.NewLine));

                    //��M����Cookie�̃R���N�V�������擾����
                    if (this.webReq.CookieContainer != null && this.webReq.CookieContainer.GetCookies(this.webReq.RequestUri).Count > 0)
                    {
                        SimpleLogger.Write( "Cookies: { " );
                        // Cookie�������Cookie���ƒl��񋓂���
                        foreach (Cookie cook in this.webReq.CookieContainer.GetCookies(this.webReq.RequestUri))
                        {
                            SimpleLogger.Write("{0}=\"{1}\"; ", cook.Name, cook.Value);
                        }
                        SimpleLogger.Write("}" + Environment.NewLine);
                        //�擾����Cookie��ۑ����Ă���
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
                /// �N�b�L�[����Q�[���N���pID�ƃL�[����͂��܂�
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
                    id = id.Remove(0, 16);    // TrickLaunch[ID]=  ������
                    key = key.Remove(0, 17);  // TrickLaunch[KEY]= ������
                    id = id.Replace("%3A", ":");  // �u%3A�v���u:�v�ɒu������

                    this.gameStartID = id;
                    this.gameStartKey = key;
                    
                }

                /// <summary>
                /// �Q�[�����N�����܂�
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
                        MessageBox.Show("�A�b�v�f�[�g���s���܂��B", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        if (System.IO.File.Exists(splash_path))
                        {
                            Common.updateGame(splash_path);
                            /// �v���O�C���t�b�N�̋N��
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
                        // �E�B���h�E��\�����Ȃ��悤�ɂ���
                        psi.CreateNoWindow = false;
                        // ����
                        psi.Arguments = this.gameStartID + "," + this.gameStartKey;
                        SimpleLogger.WriteLine(bin_path + " " + psi.Arguments.ToString());


                        if (Common.isNeedUpdate(clientVersion, serverVersion) && SettingController.GameStartUp.mode == SettingController.RUN_GAME_LAUNCHER)
                        {
                            // �N��
                            WatchController.TrickProcess = System.Diagnostics.Process.Start(psi);

                            SimpleLogger.WriteLine("need update version: " + clientVersion + " -> " + serverVersion);
                            MessageBox.Show("�A�b�v�f�[�g������܂��B\n"
                                + "�A�b�v�f�[�g������A�Q�[���X�^�[�g������O��OK�{�^���������Ă��������B", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                            /// �v���O�C���t�b�N�̋N��
                            TricksterTools.Plugins.IPlugin[] plugins = PluginController.loadPlugins(pluginHost);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.UpdatedGame);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.RunGame);


                        }
                        else
                        {
                            /// �v���O�C���t�b�N�̋N��
                            TricksterTools.Plugins.IPlugin[] plugins = PluginController.loadPlugins(pluginHost);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.RunGame);


                            // �N��
                            WatchController.TrickProcess = System.Diagnostics.Process.Start(psi);
                        }
                    }
                    else
                    {
                        SimpleLogger.WriteLine("\"" + bin_path + "\" does not exsit.");
                        MessageBox.Show("�Q�[���N���ɕK�v�ȃv���O���������݂��Ȃ����ߋN���ł��܂���B", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                /// <summary>
                /// �Q�[�����N�����܂�
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
                        MessageBox.Show("�A�b�v�f�[�g�����邽�߃����`���[���N�����܂��B", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (File.Exists(splash_path))
                        {
                            Common.updateGame(splash_path);
                            /// �v���O�C���t�b�N�̋N��
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
                        // �E�B���h�E��\�����Ȃ��悤�ɂ���
                        psi.CreateNoWindow = false;
                        // ����
                        psi.Arguments = startID + "," + startKey;
                        SimpleLogger.WriteLine(bin_path + " " + psi.Arguments.ToString());

                        if (Common.isNeedUpdate(clientVersion, serverVersion) && SettingController.GameStartUp.mode == SettingController.RUN_GAME_LAUNCHER)
                        {
                            // �N��
                            WatchController.TrickProcess = System.Diagnostics.Process.Start(psi);

                            SimpleLogger.WriteLine("need update version: " + clientVersion + " -> " + serverVersion);
                            MessageBox.Show("�A�b�v�f�[�g������܂��B\n"
                                + "�A�b�v�f�[�g������A�Q�[���X�^�[�g������O��OK�{�^���������Ă��������B", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                            /// �v���O�C���t�b�N�̋N��
                            TricksterTools.Plugins.IPlugin[] plugins = PluginController.loadPlugins(pluginHost);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.UpdatedGame);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.RunGame);


                        }
                        else
                        {
                            /// �v���O�C���t�b�N�̋N��
                            TricksterTools.Plugins.IPlugin[] plugins = PluginController.loadPlugins(pluginHost);
                            PluginController.PluginHook(plugins, TricksterTools.Plugins.HookPoint.RunGame);


                            // �N��
                            WatchController.TrickProcess = System.Diagnostics.Process.Start(psi);
                        }

                    }
                    else
                    {
                        SimpleLogger.WriteLine("\"" + bin_path + "\" does not exsit.");
                        MessageBox.Show("�Q�[���N���ɕK�v�ȃv���O���������݂��Ȃ����ߋN���ł��܂���B", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }







                /// <summary>
                /// HttpWebResponse�I�u�W�F�N�g����܂�
                /// </summary>
                protected internal void ResponseClose()
                {
                    if (this.webRes != null)
                    {
                        this.webRes.Close();
                    }
                }

                /// <summary>
                /// ���X�|���X���瓾��ꂽ�X�g���[���������Ԃ��܂��B
                /// </summary>
                /// <returns>���X�|���X������</returns>
                protected internal string getResponseStream(){
                    return this.resString;
                }
            }

        }
    }
}
