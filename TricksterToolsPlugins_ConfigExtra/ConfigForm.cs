using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Win32;
using TricksterTools.Debug;

namespace TricksterTools.Plugins.ConfigExtra
{
    public partial class ConfigForm : Form
    {
        Structures.Display.DisplayCollector dispCollector;
        Structures.Sound.SampleRateCollector samprateCollector;
        Structures.Sound.BitCollector bitCollector;
        Structures.Sound.ChannelCollector channelCollector;
        Structures.Images.FormatsCollector formatCollector;


        private bool useFullScreen = false;
        private int ScreenWidth = 640;
        private int ScreenHeight = 480;

        private bool useSound = true;
        private int SoundBit = 16;
        private int SoundFreq = 44100;
        private int SoundChannel = 16;

        private bool use3DEffect = true;
        private bool useMapEffect = true;

        private Structures.Images.ImageFormat CaptureScreenFormat = Structures.Images.ImageFormat.JPG;
        private int CaptureScreenJPGQuality = 85;



        SystemInfoForm sysinfoForm = new SystemInfoForm();
        

        public ConfigForm()
        {
            InitializeComponent();
            this.dispCollector = new Structures.Display.DisplayCollector();
            this.samprateCollector = new Structures.Sound.SampleRateCollector();
            this.bitCollector = new Structures.Sound.BitCollector();
            this.channelCollector = new Structures.Sound.ChannelCollector();
            this.formatCollector = new Structures.Images.FormatsCollector();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // 現在の設定をレジストリから読み込む
            this.loadConfig();

            int current, i;

            this.checkBox_FullScreen.Checked = this.useFullScreen;
            this.checkBox_FullSoundOFF.Checked = !this.useSound;
            this.checkBox_MapEffect.Checked = this.useMapEffect;
            this.checkBox_SpecailEffect.Checked = this.use3DEffect;


            // ディスプレイ設定
            //Structures.Display.Display currentDisplay = this.dispCollector.getDisplay(this.ScreenWidth, this.ScreenHeight);
            Structures.Display.Display currentDisplay = new TricksterTools.Plugins.ConfigExtra.Structures.Display.Display(this.ScreenWidth, this.ScreenHeight);
            ComboBoxDisplay dispItem;
            current = 0; i = 0;
            foreach (Structures.Display.Display display in this.dispCollector.getMap())
            {
                //dispItem = new ComboBoxDisplay(display.Gauge, display.ToString());
                dispItem = new ComboBoxDisplay(display.Width, display.Height, display.ToString());
                this.comboBox_Resolution.Items.Add(dispItem);
                //if (dispItem.Gauge == currentDisplay.Gauge)
                if (dispItem.Width == currentDisplay.Width && dispItem.Height == currentDisplay.Height)
                {
                    current = i;
                }

                i++;
            }
            if (this.comboBox_Resolution.Items.Count > 0)
            {
                this.comboBox_Resolution.SelectedIndex = current;
            }




            // サンプリングレート設定
            Structures.Sound.SampleRate currentRate = new Structures.Sound.SampleRate();
            currentRate.Rate = this.SoundFreq;
            current = 0; i = 0;
            foreach (Structures.Sound.SampleRate samplerate in this.samprateCollector.getMap())
            {
                this.comboBox_SampleRate.Items.Add(samplerate);
                if (samplerate.Rate == currentRate.Rate)
                {
                    current = i;
                }

                i++;
            }
            this.comboBox_SampleRate.SelectedIndex = current;



            // ビットレート設定
            Structures.Sound.Bits currentBit = new Structures.Sound.Bits();
            currentBit.Bit = this.SoundBit;
            current = 0; i = 0;
            foreach (Structures.Sound.Bits bit in this.bitCollector.getMap())
            {
                this.comboBox_Bits.Items.Add(bit);
                if (bit.Bit == currentBit.Bit)
                {
                    current = i;
                }

                i++;
            }
            this.comboBox_Bits.SelectedIndex = current;


            // チャンネル設定
            Structures.Sound.Channel currentChannel = new Structures.Sound.Channel();
            currentChannel.ch = this.SoundChannel;
            current = 0; i = 0;
            foreach (Structures.Sound.Channel ch in this.channelCollector.getMap())
            {
                this.comboBox_Channel.Items.Add(ch);
                if (ch.ch == currentChannel.ch)
                {
                    current = i;
                }

                i++;
            }
            this.comboBox_Channel.SelectedIndex = current;



            // 画像フォーマット設定
            Structures.Images.Formats currentFormat = new Structures.Images.Formats();
            currentFormat.Format = this.CaptureScreenFormat;
            current = 0; i = 0;
            foreach (Structures.Images.Formats format in this.formatCollector.getMap())
            {
                this.comboBox_FileFormat.Items.Add(format);
                if (format.Format == currentFormat.Format)
                    current = i;

                i++;
            }
            this.comboBox_FileFormat.SelectedIndex = current;



            // JPEG画質クオリティ（圧縮率）
            this.trackBar_JPGOption.Value = this.CaptureScreenJPGQuality;
            this.label_JPGOption_QualityLevel.Text = this.CaptureScreenJPGQuality.ToString() + " %";
        }




        /// <summary>
        /// レジストリから設定を読み込みます。
        /// </summary>
        private void loadConfig()
        {
            string rKeyName;
            
            // 操作するレジストリ・キーの名前
            if (ConfigExtra.isx64() && (ConfigExtra.getWindowsName() == "Windows Vista" || ConfigExtra.getWindowsName() == "Windows XP"))
            {
                rKeyName = @"SOFTWARE\WOW6432Node\NTreev\Trickster_GCrest\Setup";
            }
            else
            {
                rKeyName = @"SOFTWARE\NTreev\Trickster_GCrest\Setup";
            }
            
            // レジストリの取得
            try
            {
                // レジストリ・キーのパスを指定してレジストリを開く
                RegistryKey rKey = Registry.CurrentUser.OpenSubKey(rKeyName);

                // レジストリの値を取得
                this.useFullScreen = ((int)rKey.GetValue("Full Screen") == 1) ? true : false;
                this.ScreenHeight = (int)rKey.GetValue("heightPixel");
                this.ScreenWidth = (int)rKey.GetValue("widthPixel");

                this.useSound = ((int)rKey.GetValue("UseSound") == 1) ? true : false;
                this.SoundBit = (int)rKey.GetValue("SoundBit");
                this.SoundFreq = (int)rKey.GetValue("SoundFrequency");
                this.SoundChannel = (int)rKey.GetValue("2DSample");
                switch (this.SoundChannel)
                {
                    case 4:
                        this.SoundChannel = 8; break;
                    case 8:
                        this.SoundChannel = 16; break;
                    case 16:
                        this.SoundChannel = 32; break;
                    case 32:
                        this.SoundChannel = 48; break;
                }

                this.use3DEffect = ((int)rKey.GetValue("Use3DEffect") == 1) ? true : false;
                this.useMapEffect = ((int)rKey.GetValue("UseMapEffect") == 1) ? true : false;

                this.CaptureScreenFormat = (rKey.GetValue("CaptureScreenFormat").ToString() == "JPG") ? Structures.Images.ImageFormat.JPG : Structures.Images.ImageFormat.BMP;
                this.CaptureScreenJPGQuality = (int)rKey.GetValue("CaptureScreenJPGQuality");


                // 開いたレジストリ・キーを閉じる
                rKey.Close();
            }
            catch (Exception)
            {
            }
        }

        public class ComboBoxDisplay
        {
            //private string ComboGauge = "";
            private string ComboName = "";
            private int ComboWidth = 0;
            private int ComboHeight = 0;

            //コンストラクタ
            public ComboBoxDisplay(int Width, int Height, string Title)
            {
                this.ComboWidth = Width;
                this.ComboHeight = Height;
                //this.ComboGauge = Gauge;
                this.ComboName = Title;
            }

            //実際の値
            /*
            public string Gauge
            {
                get
                {
                    return this.ComboGauge;
                }
            }
            */


            public int Width
            {
                get
                {
                    return this.ComboWidth;
                }
            }

            public int Height
            {
                get
                {
                    return this.ComboHeight;
                }
            }
            
            //表示名称
            public string Title
            {
                get
                {
                    return this.ComboName;
                }
            }

            //オーバーライドしたメソッド
            //これがコンボボックスに表示される
            public override string ToString()
            {
                return this.ComboName;
            }
        }

        private void button_SystemInfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.sysinfoForm = new SystemInfoForm();
                this.sysinfoForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                SimpleLogger.WriteLine(ex.Source);
                SimpleLogger.WriteLine(ex.Message);
                SimpleLogger.WriteLine(ex.StackTrace);
            }
        }

        private void button_CANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackBar_JPGOption_Scroll(object sender, EventArgs e)
        {
            this.label_JPGOption_QualityLevel.Text = this.trackBar_JPGOption.Value + " %";
        }

        private void button_OK_Click(object sender, EventArgs e)
        {

            this.useFullScreen = this.checkBox_FullScreen.Checked;
            this.useSound = !this.checkBox_FullSoundOFF.Checked;
            this.useMapEffect = this.checkBox_MapEffect.Checked;
            this.use3DEffect = this.checkBox_SpecailEffect.Checked;
            
            ComboBoxDisplay combDisplay = (ComboBoxDisplay)this.comboBox_Resolution.SelectedItem;
            Structures.Display.DisplayCollector dispCol= new Structures.Display.DisplayCollector();
            //Structures.Display.Display display = dispCol.getDisplay(combDisplay.Gauge);
            Structures.Display.Display display = new TricksterTools.Plugins.ConfigExtra.Structures.Display.Display(combDisplay.Width, combDisplay.Height);
            if (display.Width > 0 && display.Height > 0)
            {
                this.ScreenWidth = display.Width;
                this.ScreenHeight = display.Height;
            }

            this.SoundBit = Convert.ToInt16(this.comboBox_Bits.SelectedItem.ToString().Replace(" Bit", ""));
            this.SoundChannel = Convert.ToInt16(this.comboBox_Channel.SelectedItem.ToString().Replace(" Channel", ""));
            this.SoundFreq = Convert.ToInt32(this.comboBox_SampleRate.SelectedItem.ToString());

            this.CaptureScreenFormat = (this.comboBox_FileFormat.SelectedItem.ToString() == "JPG") ? Structures.Images.ImageFormat.JPG : Structures.Images.ImageFormat.BMP;
            this.CaptureScreenJPGQuality = this.trackBar_JPGOption.Value;

            string rKeyName;
            // 操作するレジストリ・キーの名前
            rKeyName = @"SOFTWARE\NTreev\Trickster_GCrest\Setup";

            // レジストリの取得
            try
            {
                // レジストリ・キーのパスを指定してレジストリを開く
                RegistryKey rKey = Registry.CurrentUser.CreateSubKey(rKeyName);

                // レジストリの値を取得
                rKey.SetValue("Full Screen", ((this.useFullScreen) ? 1 : 0), RegistryValueKind.DWord);
                rKey.SetValue("heightPixel", this.ScreenHeight, RegistryValueKind.DWord);
                rKey.SetValue("widthPixel", this.ScreenWidth, RegistryValueKind.DWord);

                rKey.SetValue("UseSound", ((this.useSound) ? 1 : 0), RegistryValueKind.DWord);
                rKey.SetValue("SoundBit", this.SoundBit, RegistryValueKind.DWord);
                rKey.SetValue("SoundFrequency", this.SoundFreq, RegistryValueKind.DWord);

                int ch = 8;
                switch (this.SoundChannel)
                {
                    case 8:
                        ch = 4; break;
                    case 16:
                        ch = 8; break;
                    case 32:
                        ch = 16; break;
                    case 48:
                        ch = 32; break;
                }
                int ch3d = (ch > 16) ? 16 : ch;
                rKey.SetValue("2DSample", ch, RegistryValueKind.DWord);
                rKey.SetValue("3DSample", ch3d, RegistryValueKind.DWord);

                rKey.SetValue("Use3DEffect", ((this.use3DEffect) ? 1 : 0), RegistryValueKind.DWord);
                rKey.SetValue("UseMapEffect", ((this.useMapEffect) ? 1 : 0), RegistryValueKind.DWord);

                rKey.SetValue("CaptureScreenFormat", this.CaptureScreenFormat.ToString(), RegistryValueKind.String);
                rKey.SetValue("CaptureScreenJPGQuality", this.CaptureScreenJPGQuality, RegistryValueKind.DWord);
                
                // 開いたレジストリ・キーを閉じる
                rKey.Close();


                // 画面サイズに依存するファイルを生成
                if (!this.createFileOfScreenSize())
                {
                    MessageBox.Show("必要なファイルのコピーが行えませんでした。", "Trickster Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                ConfigExtra.updateUcf();

            }
            catch (Exception ex)
            {
                SimpleLogger.WriteLine(ex.Message);
                MessageBox.Show("設定の変更に失敗しました。", "Trickster Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;

            }

            this.Close();
        }


        /// <summary>
        /// インストール先パスを取得
        /// </summary>
        /// <param name="void"></param>
        /// <returns>インストール先パス</returns>
        private static string getInstallPath()
        {
            string rKeyName;
            // 操作するレジストリ・キーの名前
            if (ConfigExtra.isx64())
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
                string location = rKey.GetValue(rGetValueName).ToString();

                // 開いたレジストリ・キーを閉じる
                rKey.Close();

                // 取得したレジストリの値return
                return location.Replace("\\Splash.exe", "");
            }
            catch (NullReferenceException)
            {
                SimpleLogger.WriteLine("failed to get client installed path.");
                // レジストリ・キーまたは値が存在しない
                MessageBox.Show("インストールパスの取得に失敗しました", "ランチャー起動中", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return @"C:\Games\Trickster";
            }
        }

        /// <summary>
        /// 画面サイズに依存するファイルを元ファイルからコピーして作成します。
        /// </summary>
        /// <returns></returns>
        private bool createFileOfScreenSize()
        {
            string installDir = ConfigForm.getInstallPath();
            string copyBasedPixel;
            if (this.ScreenWidth < 1024)
            {
                copyBasedPixel = "800";
            }
            else
            {
                copyBasedPixel = "1024";
            }

            try
            {
                if (this.ScreenWidth > 1024)
                {
                    SimpleLogger.WriteLine("File deleting...");
                    if (File.Exists(installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".nri"))
                    {
                        File.Delete(installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".nri");
                    }
                    if (File.Exists(installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".ucf"))
                    {
                        File.Delete(installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".ucf");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".ucf");
                    }
                    if (File.Exists(installDir + @"\data\UI_nori\InstructionUI_" + this.ScreenWidth + ".nri"))
                    {
                        File.Delete(installDir + @"\data\UI_nori\InstructionUI_" + this.ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\InstructionUI_" + this.ScreenWidth + ".nri");
                    }
                    if (File.Exists(installDir + @"\data\UI_nori\InstructionUI_" + this.ScreenWidth + ".ucf"))
                    {
                        File.Delete(installDir + @"\data\UI_nori\InstructionUI_" + this.ScreenWidth + ".ucf");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\InstructionUI_" + this.ScreenWidth + ".ucf");
                    }
                    if (File.Exists(installDir + @"\data\UI_nori\map_loading_" + this.ScreenWidth + ".jpg"))
                    {
                        File.Delete(installDir + @"\data\UI_nori\map_loading_" + this.ScreenWidth + ".jpg");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\map_loading_" + this.ScreenWidth + ".jpg");
                    }
                    if (File.Exists(installDir + @"\data\UI_nori\LoginUI_" + this.ScreenWidth + ".nri"))
                    {
                        File.Delete(installDir + @"\data\UI_nori\LoginUI_" + this.ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\LoginUI_" + this.ScreenWidth + ".nri");
                    }

                    if (Directory.Exists(installDir + @"\data\UI_nori\LoadingImg_" + this.ScreenWidth))
                    {
                        Directory.Delete(installDir + @"\data\UI_nori\LoadingImg_" + this.ScreenWidth, true);
                        SimpleLogger.WriteLine("Delete directory: " + installDir + @"\data\UI_nori\LoadingImg_" + this.ScreenWidth);
                    }

                    if (File.Exists(installDir + @"\data\UI_nori\intro_img\intro_base" + this.ScreenWidth + ".nri"))
                    {
                        File.Delete(installDir + @"\data\UI_nori\intro_img\intro_base" + this.ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\intro_img\intro_base" + this.ScreenWidth + ".nri");
                    }
                    if (File.Exists(installDir + @"\data\UI_nori\intro_img\intro_Load" + this.ScreenWidth + ".nri"))
                    {
                        File.Delete(installDir + @"\data\UI_nori\intro_img\intro_Load" + this.ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\intro_img\intro_Load" + this.ScreenWidth + ".nri");
                    }

                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri");
                    }



                    SimpleLogger.WriteLine("File copying...");
                    File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".nri -> " + installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".nri");
                    File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".ucf -> " + installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".ucf");
                    File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".nri -> " + installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".nri");
                    File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".ucf -> " + installDir + @"\data\UI_nori\CharCreateUI_" + this.ScreenWidth + ".ucf");
                    File.Copy(installDir + @"\data\UI_nori\InstructionUI_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\InstructionUI_" + this.ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\InstructionUI_" + copyBasedPixel + ".nri -> " + installDir + @"\data\UI_nori\InstructionUI_" + this.ScreenWidth + ".nri");
                    File.Copy(installDir + @"\data\UI_nori\InstructionUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\InstructionUI_" + this.ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\InstructionUI_" + copyBasedPixel + ".ucf -> " + installDir + @"\data\UI_nori\InstructionUI_" + this.ScreenWidth + ".ucf");
                    File.Copy(installDir + @"\data\UI_nori\map_loading_" + copyBasedPixel + ".jpg", installDir + @"\data\UI_nori\map_loading_" + this.ScreenWidth + ".jpg", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\map_loading_" + copyBasedPixel + ".jpg -> " + installDir + @"\data\UI_nori\map_loading_" + this.ScreenWidth + ".jpg");
                    File.Copy(installDir + @"\data\UI_nori\LoginUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\LoginUI_" + this.ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\LoginUI_" + copyBasedPixel + ".ucf -> " + installDir + @"\data\UI_nori\LoginUI_" + this.ScreenWidth + ".ucf");

                    Directory.CreateDirectory(installDir + @"\data\UI_nori\LoadingImg_" + this.ScreenWidth);
                    SimpleLogger.WriteLine("Create directory: " + installDir + @"\data\UI_nori\LoadingImg_" + this.ScreenWidth);
                    string[] files = Directory.GetFiles(installDir + @"\data\UI_nori\LoadingImg_" + copyBasedPixel, "*.*", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < files.Length; i++)
                    {
                        File.Copy(files[i], files[i].Replace(copyBasedPixel, this.ScreenWidth.ToString()), true);
                        SimpleLogger.WriteLine("Copy file: " + files[i] + " -> " + files[i].Replace(copyBasedPixel, this.ScreenWidth.ToString()));
                    }

                    File.Copy(installDir + @"\data\UI_nori\intro_img\intro_base" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\intro_img\intro_base" + this.ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\intro_img\intro_base" + copyBasedPixel + ".nri -> " + installDir + @"\data\UI_nori\intro_img\intro_base" + this.ScreenWidth + ".nri");
                    File.Copy(installDir + @"\data\UI_nori\intro_img\intro_Load" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\intro_img\intro_Load" + this.ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\intro_img\intro_Load" + copyBasedPixel + ".nri -> " + installDir + @"\data\UI_nori\intro_img\intro_Load" + this.ScreenWidth + ".nri");

                    File.Copy(installDir + @"\data\UI_nori\FortuneBg_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri");


                }
                return true;
            }
            catch (Exception e)
            {
                SimpleLogger.WriteLine("Exception: " + e.Message);
                return false;
            }
        }
    }
}