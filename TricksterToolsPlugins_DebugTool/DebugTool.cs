using System;
using System.Collections.Generic;
using System.Text;
using TricksterTools.Plugins;
using TricksterTools.Debug;

namespace TricksterTools.Plugins.DebugTool
{
    public class DebugTool : IPlugin
    {
        private IPluginHost _host;
        private IPluginConfig _config;

        private System.Windows.Forms.Form frm;

        /// <summary>
        /// プラグイン名
        /// </summary>
        public string Name
        {
            get
            {
                return "De6u9 T00lz";
            }
        }

        /// <summary>
        /// プラグインのバージョン
        /// </summary>
        public string Version
        {
            get
            {
                return "1.0.0";
            }
        }

        /// <summary>
        /// プラグインの作者
        /// </summary>
        public string Author
        {
            get
            {
                return "anon";
            }
        }

        /// <summary>
        /// プラグイン作者のサイト
        /// </summary>
        public string URL
        {
            get
            {
                // ない場合は空の文字列で return してください。
                return "http://xyg.jp/";
            }
        }

        /// <summary>
        /// プラグインの説明
        /// </summary>
        public string Description
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// プラグインの設定
        /// </summary>
        public IPluginConfig Config
        {
            get
            {
                return this._config;
            }
            set
            {
                this._config = value;
            }
        }

        /// <summary>
        /// プラグインのホスト
        /// </summary>
        public IPluginHost Host
        {
            get
            {
                return this._host;
            }
            set
            {
                this._host = value;
            }
        }

        /// <summary>
        /// メイン実行ポイント
        /// TSLoginManagerの右クリック→プラグイン→（プラグイン名）
        /// から実行された際に呼び出されます。
        /// </summary>
        public void Run()
        {
            SimpleLogger.WriteLine();

            // フォームを表示する場合
            if (this.frm != null && !this.frm.IsDisposed)
            {
                this.frm.Activate();
                return;
            }
            this.frm = new DebugToolForm();
            this.frm.Show();
        }


        /// <summary>
        /// TSLoginManagerのフックポイントにおける実行
        /// TSLoginManagerの実行に連動してプラグインの実行を行えます。
        /// 特に実装の必要がない場合は何も記述せず return してください。
        /// </summary>
        public void HookRun(HookPoint hp)
        {
            /*
            switch (hp)
            {
                // ゲームを起動する直前に呼び出されます
                //case HookPoint.RunGame:
                //    break;

                // TSLoginManagerを終了する直前で呼び出されます
                //case HookPoint.Shutdown:
                //    break;

                // TSLoginManagerの起動直後に呼び出されます
                //case HookPoint.Startup:
                //    break;

                // トリックスターのアップデートがあった際、アップデートの直後に呼び出されます。
                //case HookPoint.UpdatedGame:
                //    break;
            }
            */
            return;
        }
    }
}
