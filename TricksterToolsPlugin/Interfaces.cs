using System;
using System.Collections.Generic;
using System.Text;

namespace TricksterTools.Plugins
{
    public enum HookPoint
    {
        /// <summary>
        /// アプリケーション起動直後
        /// </summary>
        Startup,

        /// <summary>
        /// ゲームアップデート後
        /// </summary>
        UpdatedGame,

        /// <summary>
        /// ゲームの実行直前
        /// </summary>
        RunGame,

        /// <summary>
        /// アプリケーション終了直前
        /// </summary>
        Shutdown,
    }

    public interface IPlugin
    {
        /// <summary>
        /// プラグインの名前
        /// </summary>
        string Name { get;}
        
        /// <summary>
        /// プラグインのバージョン
        /// </summary>
        string Version { get;}

        /// <summary>
        /// プラグインの作者
        /// </summary>
        string Author { get;}

        /// <summary>
        /// プラグイン作者のサイト
        /// </summary>
        string URL { get;}

        /// <summary>
        /// プラグインの説明
        /// </summary>
        string Description { get;}

        /// <summary>
        /// プラグインの設定
        /// </summary>
        IPluginConfig Config { get; set; }

        /// <summary>
        /// プラグインのホスト
        /// </summary>
        IPluginHost Host { get; set;}

        /// <summary>
        /// プラグインを実行する
        /// </summary>
        void Run();

        /// <summary>
        /// プラグインを実行する
        /// </summary>
        void HookRun(HookPoint hp);

    }

    /// <summary>
    /// プラグインのホストで実装するインターフェイス
    /// </summary>
    public interface IPluginHost
    {
        /// <summary>
        /// ホストのメインフォーム
        /// </summary>
        System.Windows.Forms.Form HostForm { get;}

        /// <summary>
        /// ホストでメッセージを表示する
        /// </summary>
        /// <param name="plugin">メソッドを呼び出すプラグイン</param>
        /// <param name="msg">表示するメッセージ</param>
        void ShowMessage(IPlugin plugin, string msg);
    }
}
