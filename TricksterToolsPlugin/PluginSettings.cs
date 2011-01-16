using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using TricksterTools.Plugins.Config;
using TricksterTools.Debug;

namespace TricksterTools.Plugins
{
    public class PluginSettings
    {
        #region saveConfig()
        public static void saveConfig(XmlPlugin[] Plugin)
        {
            string filename = @".\plugins.config.xml";
            XmlTricksterRoot XmlRoot = new XmlTricksterRoot();
            XmlTools Tools = new XmlTools();
            
            Tools.Plugin = Plugin;
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
                XmlSerializer serializer = new XmlSerializer(typeof(XmlTricksterRoot));
                FileStream fs = new FileStream(filepath, FileMode.Create);
                serializer.Serialize(fs, XmlRoot);
                fs.Close();
            }
            catch (System.Security.SecurityException se)
            {
                SimpleLogger.WriteLine(se.Message);
                //MessageBox.Show("例外エラー:" + Environment.NewLine + "セキュリティエラーです。", "SecurityException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw se;
            }
            catch (System.IO.IOException ioe)
            {
                SimpleLogger.WriteLine(ioe.Message);
                //MessageBox.Show("例外エラー:" + Environment.NewLine + "入出力時にエラーが発生しました。", "IOException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ioe;
            }
            catch (System.Xml.XmlException xe)
            {
                SimpleLogger.WriteLine(xe.Message);
                //MessageBox.Show("例外エラー:" + Environment.NewLine + "設定読み込みエラー", "XmlException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// <param name="pluginName">プラグイン名</param>
        /// <returns>IPluginConfig</returns>
        public static IPluginConfig loadSettings(string PluginName)
        {
            string filepath = "";
            string filename = @".\plugins.config.xml";
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
                    XmlSerializer serializer = new XmlSerializer(typeof(XmlTricksterRoot));
                    System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.Open);
                    XmlTricksterRoot XmlRoot = (XmlTricksterRoot)serializer.Deserialize(fs);
                    XmlTools Tools;
                    XmlPlugin[] Plugins;


                    if (XmlRoot.Tools.name != "TSLoginManager")
                    {
                        throw new ConfigLoadException("TSLoginManagerのPlugin設定以外の設定ファイルが読み込まれています。" + Environment.NewLine + "設定は読み込まれませんでした。");
                    }
                    Tools = XmlRoot.Tools;
                    Plugins = Tools.Plugin;
                    foreach (XmlPlugin Plugin in Plugins)
                    {
                        if (Plugin.name.Length == 0) continue;

                        if (Plugin.name == PluginName)
                        {
                            return Plugin.Config;
                        }
                    }
                    throw new PluginConfigNotFoundException();

                }
                catch (ConfigLoadException cle)
                {
                    //MessageBox.Show(cle.Message, "設定読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    throw cle;
                }
                catch (PluginConfigNotFoundException pcnfe)
                {
                    throw pcnfe;
                }
                catch (FileLoadException fle)
                {
                    SimpleLogger.WriteLine(fle.Message);
                    //MessageBox.Show("例外エラー:" + Environment.NewLine + "ファイルの読み込みに失敗しました。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new ConfigLoadException("ファイルの読み込みに失敗しました。");
                }
                catch (System.Xml.XmlException xe)
                {
                    SimpleLogger.WriteLine(xe.Message);
                    //MessageBox.Show("例外エラー:" + Environment.NewLine + "データの処理に失敗しました。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new ConfigLoadException("データの処理に失敗しました。");
                }
                catch (System.InvalidOperationException ioe)
                {
                    SimpleLogger.WriteLine(ioe.Message);
                    //MessageBox.Show("例外エラー:" + Environment.NewLine + "無効なメソッドの呼び出しが行われました。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new ConfigLoadException("無効なメソッドの呼び出しが行われました。");
                }
            }
            else
            {
                SimpleLogger.WriteLine("setting file 'plugins.config.xml' could not read/found.");
                throw new ConfigLoadException("設定ファイルを読み込めませんでした。" + Environment.NewLine + "'" + filepath + "'");
            }
        }
        #endregion
    }

    public class ConfigLoadException : Exception
    {
        public ConfigLoadException(string Message) : base(Message)
        {
        }

        public ConfigLoadException(string Message, Exception innerException) : base(Message, innerException)
        {
        }
    }
    public class PluginConfigNotFoundException : Exception
    {
    }
}
