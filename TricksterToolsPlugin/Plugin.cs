using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections;
using TricksterTools.Debug;

namespace TricksterTools.Plugins
{
    /// <summary>
    /// プラグインに関する情報
    /// </summary>
    public class PluginInfo
    {
        private string _location;
        private string _className;
        private IPluginConfig _config;

        /// <summary>
        /// PluginInfoクラスのコンストラクタ
        /// </summary>
        /// <param name="path">アセンブリファイルのパス</param>
        /// <param name="cls">クラスの名前</param>
        private PluginInfo(string path, string cls)
        {
            this._location = path;
            this._className = cls;
        }

        /// <summary>
        /// アセンブリファイルのパス
        /// </summary>
        public string Location
        {
            get { return _location; }
        }

        /// <summary>
        /// クラスの名前
        /// </summary>
        public string ClassName
        {
            get { return _className; }
        }

        /// <summary>
        /// 有効なプラグインを探す
        /// </summary>
        /// <returns>有効なプラグインのPluginInfo配列</returns>
        public static PluginInfo[] FindPlugins()
        {
            ArrayList plugins = new ArrayList();
            //IPlugin型の名前
            string ipluginName = typeof(Plugins.IPlugin).FullName;

            //プラグインフォルダ
            string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            folder += @"\plugins";
            if (!Directory.Exists(folder))
            {

                SimpleLogger.WriteLine("Plugin folder \"" + folder + "\" was not found.");
                try
                {
                    Directory.CreateDirectory(folder);
                }
                catch (Exception e)
                {
                    SimpleLogger.WriteLine("Failed to create plugin folder \"" + folder + "\".");
                    SimpleLogger.WriteLine(e.Message);
                    //System.Windows.Forms.MessageBox.Show("プラグインフォルダの作成に失敗しました。");
                }
            }

            //.dllファイルを探す
            string[] dlls = Directory.GetFiles(folder, "*.dll");

            foreach (string dll in dlls)
            {
                try
                {
                    //アセンブリとして読み込む
                    Assembly asm = Assembly.LoadFrom(dll);
                    foreach (Type t in asm.GetTypes())
                    {
                        //アセンブリ内のすべての型について、
                        //プラグインとして有効か調べる
                        if (t.IsClass && t.IsPublic && !t.IsAbstract &&
                            t.GetInterface(ipluginName) != null)
                        {
                            //PluginInfoをコレクションに追加する
                            plugins.Add(new PluginInfo(dll, t.FullName));
                        }
                    }
                }
                catch
                {
                }
            }

            //コレクションを配列にして返す
            return (PluginInfo[])plugins.ToArray(typeof(PluginInfo));
        }

        /// <summary>
        /// プラグインクラスのインスタンスを作成する
        /// </summary>
        /// <returns>プラグインクラスのインスタンス</returns>
        public IPlugin CreateInstance(IPluginHost host)
        {
            try
            {
                //アセンブリを読み込む
                Assembly asm = Assembly.LoadFrom(this.Location);
                //クラス名からインスタンスを作成する
                Plugins.IPlugin plugin = (Plugins.IPlugin)asm.CreateInstance(this.ClassName);
                //IPluginHostの設定
                plugin.Host = host;
                try
                {
                    plugin.Config = PluginSettings.loadSettings(this.ClassName);
                }
                catch (ConfigLoadException cle)
                {
                    
                }
                catch (PluginConfigNotFoundException pcnfe)
                {

                }
                return plugin;
            }
            catch
            {
                return null;
            }
        }
    }


}
