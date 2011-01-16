using System;
using System.Collections.Generic;
using System.Text;
using TricksterTools.Plugins;
using TricksterTools.Debug;

namespace TricksterTools.Library
{
    public class PluginController
    {
        /// <summary>
        /// プラグインを読み込みます
        /// </summary>
        public static IPlugin[] loadPlugins(IPluginHost host)
        {
            //インストールされているプラグインを探す
            PluginInfo[] pis = PluginInfo.FindPlugins();

            //プラグインのインスタンスを取得する
            IPlugin[] plugins = new IPlugin[pis.Length];
            for (int i = 0; i < plugins.Length; i++)
            {
                plugins[i] = pis[i].CreateInstance(host);
            }

            return plugins;
        }
        
        /// <summary>
        /// 指定されたプラグインを実行します
        /// </summary>
        /// <param name="plugins">プラグイン一覧</param>
        /// <param name="pluginName">プラグインクラス名</param>
        public static void PluginRun(IPlugin[] plugins, string pluginName)
        {
            // プラグインクラス名からプラグインを探す
            //（同じ名前のプラグインクラスが複数あると困ったことに...）
            foreach (IPlugin plugin in plugins)
            {
                if (pluginName == plugin.GetType().Name)
                {
                    SimpleLogger.WriteLine("run " + plugin.GetType().Name);
                    //クリックされたプラグインを実行する
                    plugin.Run();
                    return;
                }
            }
        }

        /// <summary>
        /// プラグインの情報を取得します
        /// </summary>
        /// <param name="plugins">読み込まれたプラグインの配列</param>
        /// <param name="pluginName">プラグインクラス名</param>
        public static IPlugin getPluginInfo(IPlugin[] plugins, string pluginClassName)
        {
            // プラグイン名からプラグインを探す
            //（同じ名前のプラグインが複数あると困ったことに...）
            IPlugin retPlugin = plugins[0];

            foreach (IPlugin plugin in plugins)
            {
                if (pluginClassName == plugin.GetType().Name)
                {
                    retPlugin = plugin;
                    break;
                }

            }
            return retPlugin;
        }



        /// <summary>
        /// 指定されたプラグインをフックポイントに応じて実行します
        /// </summary>
        /// <param name="plugins">プラグイン一覧</param>
        /// <param name="pluginName">プラグインクラス名</param>
        public static void PluginHook(IPlugin[] plugins, HookPoint hookPoint)
        {
            // プラグインクラス名からプラグインを探す
            //（同じ名前のプラグインクラスが複数あると困ったことに...）
            foreach (IPlugin plugin in plugins)
            {
                Type typeHook = typeof(IPlugin);
                System.Reflection.MethodInfo mi = typeHook.GetMethod("HookRun");
                if (mi != null)
                {
                    SimpleLogger.WriteLine("HookRun[" + hookPoint.ToString() + "]: " + plugin.GetType().Name);
                    //クリックされたプラグインを実行する
                    plugin.HookRun(hookPoint);
                    return;
                }
            }
        }
    }
}
