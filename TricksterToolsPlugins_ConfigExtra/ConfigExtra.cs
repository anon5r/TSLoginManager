using System;
using System.Collections.Generic;
using System.Text;
using TricksterTools.Plugins;
using TricksterTools.Plugins.Config;
using TricksterTools.Debug;

namespace TricksterTools.Plugins.ConfigExtra
{
    public class ConfigExtra : IPlugin
    {
        private IPluginHost _host;
        private IPluginConfig _config;
        private ConfigForm _mainForm;


        public string Name
        {
            get
            {
                return "拡張設定画面";
            }
        }

        /// <summary>
        /// プラグインのバージョン
        /// </summary>
        public string Version
        {
            get
            {
                return "0.1.2.2";
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
                return "http://trickster.anoncom.net/";
            }
        }

        /// <summary>
        /// プラグインの説明
        /// </summary>
        public string Description
        {
            get
            {
                return "トリックスターの設定を拡張設定できる設定画面を表示します。";
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


        public static void Main()
        {
            //検索ウィンドウを作成し、表示する
            ConfigForm cls = new ConfigForm();
            cls.Show();
        }

        public void Run()
        {
            //フォームが表示されていれば、アクティブにして終了
            if (this._mainForm != null && !this._mainForm.IsDisposed)
            {
                this._mainForm.Activate();
                return;
            }

            //検索ウィンドウを作成し、表示する
            this._mainForm = new ConfigForm();
            //this._mainForm.Owner = this._host.HostForm;
            this._mainForm.Show();
        }

        public void HookRun(HookPoint hp)
        {
            switch(hp){

                // アップデート後
                case HookPoint.UpdatedGame:
                    createFileOfScreenSize();
                    ConfigExtra.updateUcf();
                    break;
                // アプリケーション開始時
                case HookPoint.Startup:
                // ゲーム開始時
                case HookPoint.RunGame:
                    break;
                // アプリケーション終了時
                case HookPoint.Shutdown:
                    break;
                // その他
                default:
                    break;
            }
            return;
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

        /// <summary>
        /// Windows の名前を取得します
        /// </summary>
        /// <returns></returns>
        public static string getWindowsName()
        {
            OperatingSystem osInfo = Environment.OSVersion;

            string windowsName = "Undefined";  // Windows名

            switch (osInfo.Platform)
            {
                case PlatformID.Win32Windows:  // Windows 9x系
                    if (osInfo.Version.Major == 4)
                    {
                        switch (osInfo.Version.Minor)
                        {
                            case 0:  // .NET Frameworkのサポートなし
                                windowsName = "Windows 95";
                                break;
                            case 10:
                                windowsName = "Windows 98";
                                break;
                            case 90:
                                windowsName = "Windows Me";
                                break;
                        }
                    }
                    break;

                case PlatformID.Win32NT:  // Windows NT系
                    if (osInfo.Version.Major == 4)
                    {
                        // .NET Framework 2.0以降のサポートなし
                        windowsName = "Windows NT 4.0";
                    }
                    else if (osInfo.Version.Major == 5)
                    {
                        switch (osInfo.Version.Minor)
                        {
                            case 0:
                                windowsName = "Windows 2000";
                                break;

                            case 1:
                                windowsName = "Windows XP";
                                break;

                            case 2:
                                windowsName = "Windows Server 2003";
                                break;
                        }
                    }
                    else if (osInfo.Version.Major == 6)
                    {
                        switch (osInfo.Version.Minor)
                        {
                            case 0:
                                windowsName = "Windows Vista";
                                break;
                            case 1:
                                windowsName = "Windows 7";
                                break;
                        }
                    }
                    break;
            }
            return windowsName;
        }


        /// <summary>
        /// 画面サイズに依存するファイルを元ファイルからコピーして作成します。
        /// </summary>
        /// <returns></returns>
        private void createFileOfScreenSize()
        {
            string rKeyName;
            int ScreenWidth = 640;
            int ScreenHeight = 480;
            string installDir = @"C:\Games\Trickster";

            // 操作するレジストリ・キーの名前
            rKeyName = @"SOFTWARE\NTreev\Trickster_GCrest\Setup";

            // レジストリの取得
            try
            {
                // レジストリ・キーのパスを指定してレジストリを開く

                Microsoft.Win32.RegistryKey rKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(rKeyName);

                // レジストリの値を取得
                ScreenHeight = int.Parse(rKey.GetValue("heightPixel").ToString());
                ScreenWidth = int.Parse(rKey.GetValue("widthPixel").ToString());

                // 開いたレジストリ・キーを閉じる
                rKey.Close();

                // インストールパスを取得する
                rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(rKeyName.Replace(@"\Setup", ""));
                installDir = rKey.GetValue("FullPath").ToString();

                // 開いたレジストリ・キーを閉じる
                rKey.Close();

                // 取得したレジストリの値return
                installDir = installDir.Replace(@"\Splash.exe", "");
            }
            catch (Exception e)
            {
                SimpleLogger.WriteLine(e.Message);
            }

            string copyBasedPixel;
            if (ScreenWidth < 1024)
            {
                copyBasedPixel = "800";
            }
            else
            {
                copyBasedPixel = "1024";
            }

            if (ScreenWidth > 1024)
            {
                try
                {
                    SimpleLogger.WriteLine("File deleting...");
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".nri");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".ucf"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".ucf");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".ucf");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\map_loading_" + ScreenWidth + ".jpg"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\map_loading_" + ScreenWidth + ".jpg");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\map_loading_" + ScreenWidth + ".jpg");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\LoginUI_" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\LoginUI_" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\LoginUI_" + ScreenWidth + ".nri");
                    }

                    if (System.IO.Directory.Exists(installDir + @"\data\UI_nori\LoadingImg_" + ScreenWidth))
                    {
                        System.IO.Directory.Delete(installDir + @"\data\UI_nori\LoadingImg_" + ScreenWidth, true);
                        SimpleLogger.WriteLine("Delete directory: " + installDir + @"\data\UI_nori\LoadingImg_" + ScreenWidth);
                    }

                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\intro_img\intro_base" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\intro_img\intro_base" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\intro_img\intro_base" + ScreenWidth + ".nri");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\intro_img\intro_Load" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\intro_img\intro_Load" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\intro_img\intro_Load" + ScreenWidth + ".nri");
                    }

                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri");
                    }
                    


                    SimpleLogger.WriteLine("File copying...");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\InstructionUI_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".nri");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\InstructionUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".ucf");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\map_loading_" + copyBasedPixel + ".jpg", installDir + @"\data\UI_nori\map_loading_" + ScreenWidth + ".jpg", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\map_loading_" + ScreenWidth + ".jpg");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\LoginUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\LoginUI_" + ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\LoginUI_" + ScreenWidth + ".ucf");

                    System.IO.Directory.CreateDirectory(installDir + @"\data\UI_nori\LoadingImg_" + ScreenWidth);
                    SimpleLogger.WriteLine("Create directory: " + installDir + @"\data\UI_nori\LoadingImg_" + ScreenWidth);
                    string[] files = System.IO.Directory.GetFiles(installDir + @"\data\UI_nori\LoadingImg_" + copyBasedPixel, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < files.Length; i++)
                    {
                        System.IO.File.Copy(files[i], files[i].Replace(copyBasedPixel, ScreenWidth.ToString()), true);
                        SimpleLogger.WriteLine("Copy file: " + files[i].Replace(copyBasedPixel, ScreenWidth.ToString()));
                    }

                    System.IO.File.Copy(installDir + @"\data\UI_nori\intro_img\intro_base" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\intro_img\intro_base" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\intro_img\intro_base" + ScreenWidth + ".nri");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\intro_img\intro_Load" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\intro_img\intro_Load" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\intro_img\intro_Load" + ScreenWidth + ".nri");

                    System.IO.File.Copy(installDir + @"\data\UI_nori\FortuneBg_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri");
                    

                    return;
                }
                catch (Exception e)
                {
                    SimpleLogger.WriteLine("Exception: " + e.Message);
                }
            }
        }

        /// <summary>
        /// UCFファイルの設定を変更する
        /// </summary>
        public static void updateUcf()
        {
            string rKeyName;
            int ScreenWidth = 800;
            int ScreenHeight = 600;
            string installDir = @"C:\Games\Trickster";

            // 操作するレジストリ・キーの名前
            rKeyName = @"SOFTWARE\NTreev\Trickster_GCrest\Setup";

            // レジストリの取得
            try
            {
                // レジストリ・キーのパスを指定してレジストリを開く

                Microsoft.Win32.RegistryKey rKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(rKeyName);

                // レジストリの値を取得
                ScreenHeight = int.Parse(rKey.GetValue("heightPixel").ToString());
                ScreenWidth = int.Parse(rKey.GetValue("widthPixel").ToString());

                // 開いたレジストリ・キーを閉じる
                rKey.Close();

                // インストールパスを取得する
                rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(rKeyName.Replace(@"\Setup", ""));
                installDir = rKey.GetValue("FullPath").ToString();

                // 開いたレジストリ・キーを閉じる
                rKey.Close();

                // 取得したレジストリの値return
                installDir = installDir.Replace(@"\Splash.exe", "");
            }
            catch (Exception e)
            {
                SimpleLogger.WriteLine(e.Message);
            }


            System.IO.TextReader reader;
            System.IO.TextWriter writer;

            // キャラセレクトウィンドウ
            int iBaseCoordX = ((ScreenWidth - 582) / 2);
            int iBaseCoordY = (((ScreenHeight - 346) / 2) - 97) + 50;

            // タイムカプセルウィンドウ
            int iMenuCoordX = ((ScreenWidth - 582) / 2);
            int iMenuCoordY = iBaseCoordY + 346;


            string content, search;
            int pos = 0;


            /*
             * data\UI_nori\CharSelectUI.ucf
             * 
             * 
             */

            // 既に設定があるか確認
            reader = new System.IO.StreamReader(installDir + @"\data\UI_nori\CharSelectUI.ucf");
            content = reader.ReadToEnd();
            reader.Close();
            try
            {
                //search = "iBase_CoordX_" + ScreenWidth;
                search = "iBase_CoordX_" + ScreenWidth;
                pos = content.IndexOf(search, 0);
                if (pos > 0)
                {
                    // あれば開始位置から行末まで削除
                    content = content.Replace("iBase_CoordX_" + ScreenWidth + "\t  = " + iBaseCoordX + System.Environment.NewLine, "");
                }

                search = "iBase_CoordY_" + ScreenWidth;
                pos = content.IndexOf(search, 0);
                if (pos > 0)
                {
                    // あれば開始位置から行末まで削除
                    content = content.Replace("iBase_CoordY_" + ScreenWidth + "\t  = " + iBaseCoordY + System.Environment.NewLine, "");
                }
            }
            catch (ArgumentOutOfRangeException aore)
            {}

            try
            {
                // 最初に改行が2回出てくるところを検索
                int appendPosition = content.IndexOf(System.Environment.NewLine + System.Environment.NewLine, 0) + System.Environment.NewLine.Length;
                
                content = content.Insert(appendPosition,
                    ""
                    + "iBase_CoordX_" + ScreenWidth + "\t  = " + iBaseCoordX + System.Environment.NewLine
                    + "iBase_CoordY_" + ScreenWidth + "\t  = " + iBaseCoordY + System.Environment.NewLine
                );

                // ファイルに書き込む
                writer = new System.IO.StreamWriter(installDir + @"\data\UI_nori\CharSelectUI.ucf");
                writer.Write(content);
                writer.Close();
            }
            catch (ArgumentOutOfRangeException aore)
            { }




            /*
             * data\UI_nori\TimeCapsuleUI.ucf
             * 
             * 
             */

            // 既に設定があるか確認
            reader = new System.IO.StreamReader(installDir + @"\data\UI_nori\TimeCapsuleUI.ucf");
            content = reader.ReadToEnd();
            reader.Close();
            
            try
            {
                search = "iMenu_CoordX_" + ScreenWidth;
                pos = content.IndexOf(search, 0);
                if (pos > 0)
                {
                    // あれば開始位置から行末まで削除
                    content = content.Replace("iMenu_CoordX_" + ScreenWidth + "\t= " + iMenuCoordX + System.Environment.NewLine, "");
                }

                search = "iMenu_CoordY_" + ScreenWidth;
                pos = content.IndexOf(search, 0);
                if (pos > 0)
                {
                    // あれば開始位置から行末まで削除
                    content = content.Replace("iMenu_CoordY_" + ScreenWidth + "\t= " + iMenuCoordY + System.Environment.NewLine, "");
                }
            }
            catch (ArgumentOutOfRangeException aore)
            { }


            try
            {
                // 最初に改行が2回出てくるところを検索
                int appendPosition = content.IndexOf(System.Environment.NewLine + System.Environment.NewLine, 0) + System.Environment.NewLine.Length;

                content = content.Insert(appendPosition,
                    ""
                    + "iMenu_CoordX_" + ScreenWidth + "\t= " + iMenuCoordX + System.Environment.NewLine
                    + "iMenu_CoordY_" + ScreenWidth + "\t= " + iMenuCoordY + System.Environment.NewLine
                );

                // ファイルに書き込む
                writer = new System.IO.StreamWriter(installDir + @"\data\UI_nori\TimeCapsuleUI.ucf");
                writer.Write(content);
                writer.Close();
            }
            catch (ArgumentOutOfRangeException aore)
            { }
        }
    }
}
