using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
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
            #region SettingController
            /// <summary>
            /// プログラムの設定
            /// </summary>
            public class SettingController
            {
                //static Logger logger = SimpleLogger.setLogger("SettingController");
                public static int RUN_GAME_DIRECT = 0;
                public static int RUN_GAME_LAUNCHER = 1;

                public static SortedList Links = new SortedList();

                public class HungUp
                {
                    public static bool enable;
                    public static int sec;
                }


                public class Update
                {
                    public static bool startupAutoCehck;
                    public static bool checkBetaVersion;
                }

                public class GameStartUp
                {
                    public static int mode;
                }
                
                public class Logging
                {
                    public static bool enable;
                    public static string filePath;
                }
                
                public class Icons
                {
                    /// <summary>
                    /// リソースファイル名を取得または設定します。
                    /// </summary>
                    public static string resourceName;

                    /// <summary>
                    /// リソースファイルの一覧を返します。
                    /// </summary>
                    /// <returns>リソースファイル名の一覧</returns>
                    public static SortedList<int, string> resourceList()
                    {
                        SortedList<int, string> lists = new SortedList<int, string>();
                        lists.Add(0, "char99");
                        lists.Add(1, "char00");
                        lists.Add(2, "char01");
                        lists.Add(3, "char02");
                        lists.Add(4, "char03");
                        lists.Add(5, "char04");
                        lists.Add(6, "char05");
                        lists.Add(7, "char06");
                        lists.Add(8, "char07");
                        lists.Add(9, "char10");
                        lists.Add(10, "char11");
                        lists.Add(11, "char12");
                        lists.Add(12, "char13");
                        lists.Add(13, "char14");
                        lists.Add(14, "char15");
                        lists.Add(15, "char16");
                        lists.Add(16, "char17");
                        lists.Add(17, "char20");
                        lists.Add(18, "char21");
                        lists.Add(19, "char22");
                        lists.Add(20, "char23");
                        lists.Add(21, "char24");
                        lists.Add(22, "char25");
                        lists.Add(23, "char26");
                        lists.Add(24, "char27");
                        return lists;
                    }
                }


                #region saveSettings()
                public static void saveSettings(string filename)
                {
                    TricksterTools.Library.Xml.Settings.XmlTricksterRoot XmlRoot = new TricksterTools.Library.Xml.Settings.XmlTricksterRoot();
                    XmlTools Tools = new XmlTools();
                    XmlSettings Settings = new XmlSettings();
                    XmlSettingHungUpTime hungtime = new XmlSettingHungUpTime();
                    XmlSettingUpdate update = new XmlSettingUpdate();
                    XmlSettingGameStartUp gamestartup = new XmlSettingGameStartUp();
                    XmlSettingLogging logging = new XmlSettingLogging();
                    XmlSettingIcons icons = new XmlSettingIcons();

                    hungtime.enable = (HungUp.enable) ? "true" : "false";
                    hungtime.sec = HungUp.sec;
                    update.startup = (Update.startupAutoCehck) ? "true" : "false";
                    update.checkBeta = (Update.checkBetaVersion) ? "true" : "false";
                    gamestartup.mode = GameStartUp.mode;
                    logging.enable = (Logging.enable) ? "true" : "false";
                    logging.Path = Logging.filePath;
                    icons.resourceName = Icons.resourceName;

                    Settings.HungupTime = hungtime;
                    Settings.UpdateCheck = update;
                    Settings.StartUpGame = gamestartup;
                    //Settings.Logging = logging;
                    Settings.Icon = icons;
                    Tools.Settings = Settings;
                    XmlRoot.Tools = Tools;


                    string filepath = "";
                    if (Path.IsPathRooted(filename))
                    {
                        filepath = filename;
                        filename = Path.GetFileName(filename);
                    }
                    else
                    {
                        filepath = Path.GetFullPath(Environment.CurrentDirectory + @"\" + filename);
                    }

                    if (!File.Exists(filepath))
                    {
                        // ファイルがなければ作成
                        FileStream fs = new FileStream(filepath, FileMode.Create);
                        fs.Close();
                    }

                    try
                    {
                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(TricksterTools.Library.Xml.Settings.XmlTricksterRoot));
                        FileStream fs = new FileStream(filepath, FileMode.Create);
                        serializer.Serialize(fs, XmlRoot);
                        fs.Close();
                    }
                    catch (System.Security.SecurityException se)
                    {
                        SimpleLogger.WriteLine(se.Message);
                        //MessageBox.Show("例外エラー:" + Environment.NewLine + "セキュリティエラーです。", "SecurityExceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw se;
                    }
                    catch (System.IO.IOException ioe)
                    {
                        SimpleLogger.WriteLine(ioe.Message);
                        //MessageBox.Show("例外エラー:" + Environment.NewLine + "入出力時にエラーが発生しました。", "IOExceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw ioe;
                    }
                    catch (System.Xml.XmlException xe)
                    {
                        SimpleLogger.WriteLine(xe.Message);
                        //MessageBox.Show("例外エラー:" + Environment.NewLine + "設定読み込みエラー", "XmlExceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw xe;
                    }
                    catch (System.Exception e)
                    {
                        SimpleLogger.WriteLine(e.Message);
                        //MessageBox.Show("例外エラー:" + Environment.NewLine + "原因の特定ができませんでした。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw e;
                    }
                }
                #endregion
                #region loadSettings()
                /// <summary>
                /// XMLファイルから設定を読み込み、クラス内のプロパティに設定する
                /// </summary>
                /// 
                /// <param name="filename">読み込むXMLファイル名</param>
                /// <returns>IDをキー、Passwodを値としたハッシュテーブル</returns>
                public static void loadSettings(string filename)
                {
                    string filepath = "";
                    if (Path.IsPathRooted(filename))
                    {
                        filepath = filename;
                        filename = Path.GetFileName(filename);
                    }
                    else
                    {
                        filepath = Path.GetFullPath(Environment.CurrentDirectory + @"\" + filename);
                    }
                    if (File.Exists(filepath))
                    {
                        try
                        {
                            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(TricksterTools.Library.Xml.Settings.XmlTricksterRoot));
                            System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.Open);
                            TricksterTools.Library.Xml.Settings.XmlTricksterRoot XmlRoot = (TricksterTools.Library.Xml.Settings.XmlTricksterRoot)serializer.Deserialize(fs);
                            XmlSettings Tools = new XmlSettings();

                            if (XmlRoot.Tools.name != "TSLoginManager")
                            {
                                MessageBox.Show("TSLoginManager以外の設定ファイルが読み込まれています。" + Environment.NewLine + "設定は読み込まれませんでした。", "設定読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                HungUp.enable = false;
                                HungUp.sec = 60;
                                Update.startupAutoCehck = false;
                                Update.checkBetaVersion = false;
                                GameStartUp.mode = RUN_GAME_DIRECT;
                                //Logging.enable = false;
                                //Logging.fileName = Environment.CurrentDirectory + @"\debug.log";
                                Icons.resourceName = "char99";

                                return;
                            }
                            Tools = XmlRoot.Tools.Settings;

                            // Logging設定
                            /*
                            try
                            {
                                if (Tools.Logging.enable == "true")
                                {
                                    Logging.enable = true;
                                }
                                else
                                {
                                    Logging.enable = false;
                                }
                            }
                            catch (Exception e)
                            {
                                SimpleLogger.WriteLine(e.Message);
                                Logging.enable = false;
                            }
                            try
                            {
                                Logging.filePath = Tools.Logging.Path;
                            }
                            catch (Exception e)
                            {
                                SimpleLogger.WriteLine(e.Message);
                                Logging.filePath = Environment.CurrentDirectory + "\\logs";
                            } 
                            */

                            // アイコン設定
                            try
                            {
                                Icons.resourceName = Tools.Icon.resourceName;
                            }
                            catch (Exception e)
                            {
                                SimpleLogger.WriteLine(e.Message);
                                Icons.resourceName = "char99";  // default
                            }

                            // フリーズ設定
                            try
                            {
                                if (Tools.HungupTime.enable == "true")
                                {
                                    HungUp.enable = true;
                                }
                                else
                                {
                                    HungUp.enable = false;
                                }
                            }
                            catch (Exception e)
                            {
                                SimpleLogger.WriteLine(e.Message);
                                SimpleLogger.WriteLine("load default: [HungUp] enable");
                                HungUp.enable = true;
                            }
                            try
                            {
                                HungUp.sec = Tools.HungupTime.sec;
                            }
                            catch (Exception e)
                            {
                                SimpleLogger.WriteLine(e.Message);
                                SimpleLogger.WriteLine("load default: [HungUp] time = 60");
                                HungUp.sec = 60;
                            }

                            // アップデートチェック設定
                            try
                            {
                                if (Tools.UpdateCheck.startup.ToLower() == "true")
                                {
                                    Update.startupAutoCehck = true;
                                }
                                else
                                {
                                    Update.startupAutoCehck = false;
                                }
                            }
                            catch (Exception e)
                            {
                                SimpleLogger.WriteLine(e.Message);
                                SimpleLogger.WriteLine("load default: [StartupAutoCheck] false");
                                Update.startupAutoCehck = false;
                            }

                            // ベータアップデート設定
                            try
                            {
                                if (Tools.UpdateCheck.checkBeta.ToLower() == "true")
                                {
                                    Update.checkBetaVersion = true;
                                }
                                else
                                {
                                    Update.checkBetaVersion = false;
                                }
                            }
                            catch (Exception e)
                            {
                                SimpleLogger.WriteLine(e.Message);
                                SimpleLogger.WriteLine("load default: [BetaCheck] false");
                                Update.checkBetaVersion = false;
                            }

                            // ゲーム起動方法設定
                            try
                            {
                                GameStartUp.mode = Tools.StartUpGame.mode;
                            }
                            catch (Exception e)
                            {
                                SimpleLogger.WriteLine(e.Message);
                                SimpleLogger.WriteLine("load default: [GameStartUp] mode=0");
                                GameStartUp.mode = RUN_GAME_DIRECT;
                            }
                        }
                        catch (FileLoadException fle)
                        {
                            SimpleLogger.WriteLine(fle.Message);
                            MessageBox.Show("例外エラー:" + Environment.NewLine + "ファイルの読み込みに失敗しました。", "FileLoadException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            HungUp.sec = 60;
                            HungUp.enable = true;
                            Update.startupAutoCehck = false;
                            Update.checkBetaVersion = false;
                            GameStartUp.mode = RUN_GAME_DIRECT;
                            Icons.resourceName = "char99";
                        }
                        catch (System.Xml.XmlException xe)
                        {
                            SimpleLogger.WriteLine(xe.Message);
                            MessageBox.Show("例外エラー:" + Environment.NewLine + "データの処理に失敗しました。", "XmlException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            HungUp.sec = 60;
                            HungUp.enable = true;
                            Update.startupAutoCehck = false;
                            Update.checkBetaVersion = false;
                            GameStartUp.mode = RUN_GAME_DIRECT;
                            Icons.resourceName = "char99";
                        }
                        catch (System.InvalidOperationException ioe)
                        {
                            SimpleLogger.WriteLine(ioe.Message);
                            MessageBox.Show("例外エラー:" + Environment.NewLine + "無効なメソッドの呼び出しが行われました。", "InvalidOperationException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            HungUp.sec = 60;
                            HungUp.enable = true;
                            Update.startupAutoCehck = false;
                            Update.checkBetaVersion = false;
                            GameStartUp.mode = RUN_GAME_DIRECT;
                            Icons.resourceName = "char99";
                        }
                    }
                    else
                    {
                        SimpleLogger.WriteLine("setting file 'settings.xml' could not read/found.");
                        MessageBox.Show("設定ファイルを読み込めませんでした。" + Environment.NewLine + "'" + filepath + "'", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        HungUp.sec = 60;
                        HungUp.enable = true;
                        Update.startupAutoCehck = false;
                        Update.checkBetaVersion = false;
                        GameStartUp.mode = RUN_GAME_DIRECT;
                        Icons.resourceName = "char99";
                    }
                }
                #endregion
                #region loadLinks()
                /// <summary>
                /// テキストリンク情報を読み込み、ハッシュテーブル化する
                /// [[書式]]
                /// サイト名\tURL
                /// </summary>
                /// 
                /// <param name="string">読み込むテキストファイル名</param>
                /// <returns>サイト名をキー、URLを値としたハッシュテーブル</returns>
                public static SortedList loadLinks(string filename)
                {
                    string filepath = Path.GetFullPath(@".\" + filename);
                    if (File.Exists(filepath))
                    {
                        try
                        {
                            StreamReader sr = new StreamReader(filepath, System.Text.Encoding.GetEncoding("Shift_JIS"));
                            string line;
                            string[] SplitStr;

                            while ((line = sr.ReadLine()) != null)
                            {
                                SplitStr = line.Split(new char[] { '\t' });
                                Links[SplitStr[0].ToString()] = SplitStr[1].ToString();
                            }
                            if (Links.Count < 1)
                            {
                                Links["__TSLM_NULL__"] = "__TSLM_NULL__";
                            }

                            return Links;
                        }
                        catch (FileLoadException ex)
                        {
                            MessageBox.Show(ex.ToString());
                            Links["__TSLM_NULL__"] = "__TSLM_NULL__";
                            return Links;
                        }
                    }
                    else
                    {
                        //if (!System.Deployment.Application.ApplicationDeployment.CurrentDeployment.IsFirstRun)
                        //{
                        //MessageBox.Show("リンク用ファイルを読み込めませんでした。" + Environment.NewLine + "'" + filepath + "'", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}
                        //Links["__TSLM_NULL__"] = "__TSLM_NULL__";
                        
                        /** リンクファイルを生成 **/
                        System.Text.StringBuilder outputLink = new System.Text.StringBuilder();
                        outputLink.Insert(0, "");
                        outputLink.AppendLine("(&1) 公式サイト\thttp://www.trickster.jp/");
                        outputLink.AppendLine("(&2) Trickster Wiki\thttp://www.tricksterwiki.info/wiki/");
                        outputLink.AppendLine("(&3) とり☆すた～ごにょごにょ～\thttp://trickster.anoncom.net/");
                        
                        if (!File.Exists(filepath))
                        {
                            // ファイルがなければ作成
                            FileStream fs = File.Create(filepath);
                            fs.Close();
                        }
                        try
                        {
                            StreamWriter writer = new StreamWriter(filename, false, System.Text.Encoding.Default);
                            writer.Write(outputLink.ToString());
                            writer.Close();
                        }
                        catch (Exception e)
                        {
                            SimpleLogger.WriteLine("could not create \"links.txt\", got exception: " + e.Message);
                        }


                        /** デフォルトのリンクを設定 **/
                        Links["(&1) 公式サイト"] = "http://www.trickster.jp/";
                        Links["(&2) Trickster Wiki"] = "http://www.tricksterwiki.info/wiki/";
                        Links["(&3) とり☆すた～ごにょごにょ～"] = "http://trickster.anoncom.net/";
                        return Links;
                    }
                }
                #endregion
            }
            #endregion
        }
    }
}
