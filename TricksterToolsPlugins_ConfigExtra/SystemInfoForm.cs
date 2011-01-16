using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Management;
using TricksterTools.Debug;


namespace TricksterTools.Plugins.ConfigExtra
{
    public partial class SystemInfoForm : Form
    {
        public SystemInfoForm()
        {
            InitializeComponent();
        }

        private static string sysinfo = "";

        private void SystemInfoForm_Load(object sender, EventArgs e)
        {
            sysinfo = getSystemInformation();
        }

        /// <summary>
        /// インストールされているDirectXのバージョンを取得します。
        /// </summary>
        /// <returns>一般リリース向けバージョンを返します。</returns>
        private static string getDrirectXVersion()
        {

            // 取得処理を行う対象となるレジストリの値の名前
            string rKeyName = @"SOFTWARE\Microsoft\DirectX";

            // レジストリの取得
            try
            {
                // レジストリ・キーのパスを指定してレジストリを開く
                Microsoft.Win32.RegistryKey rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(rKeyName);

                // レジストリの値を取得
                string ver = (string)rKey.GetValue("Version");

                // 開いたレジストリ・キーを閉じる
                rKey.Close();

                SimpleLogger.WriteLine("DirectX version: " + ver);

                if (ver == "4.02.0095") return "1.0";
                else if (ver == "4.03.00.1096") return "2.0";
                else if (ver == "4.04.0068") return "3.0";
                else if (ver == "4.04.0069") return "3.0";
                else if (ver == "4.05.00.0155") return "5.0";
                else if (ver == "4.05.01.1721") return "5.0";
                else if (ver == "4.05.01.1998") return "5.0";
                else if (ver == "4.06.02.0436") return "6.0";
                else if (ver == "4.07.00.0700") return "7.0";
                else if (ver == "4.07.00.0716") return "7.0a";
                else if (ver == "4.08.00.0400") return "8.0";
                else if (ver == "4.08.01.0810") return "8.1";
                else if (ver == "4.08.01.0881") return "8.1";
                else if (ver == "4.08.01.0901") return "8.1a";
                else if (ver == "4.08.01.0901") return "8.1b";
                else if (ver == "4.08.02.0134") return "8.2";
                else if (ver == "4.09.00.0900") return "9";
                else if (ver == "4.09.00.0901") return "9a";
                else if (ver == "4.09.00.0902") return "9b";
                else if (ver == "4.09.00.0903") return "9c";
                else if (ver == "4.09.00.0904") return "9c";
                else if (ver == "4.10.00.6000") return "10";
                else return "10/10.1";
            }
            catch (NullReferenceException)
            {
                // レジストリ・キーまたは値が存在しない
                SimpleLogger.WriteLine("Microsoft DirectX is not installed.");
                return "";
            }
        }

        private string getSystemInformation()
        {

            StringBuilder sb;
            ManagementObjectSearcher searcher;

            //try
            //{
            sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("========================================================");
            sb.AppendLine("");
            sb.AppendLine("		 Trickster Tools System Infomation");
            sb.AppendLine("");
            sb.AppendLine("========================================================");
            sb.AppendLine("");


            /**
             * CPU情報取得
             */
            sb.Append("CPU : ");
            searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                sb.AppendLine(queryObj["Name"].ToString());
            }
            sb.AppendLine("");


            /**
             * メモリ情報取得
             */
            searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_OperatingSystem");
            sb.Append("System Memory : ");
            string SystemDrive = "C:";
            foreach (ManagementObject queryObj in searcher.Get())
            {
                //利用可能な物理メモリ
                sb.Append(queryObj["FreePhysicalMemory"].ToString());
                sb.Append("/");
                //合計物理メモリ
                sb.AppendLine(queryObj["TotalVisibleMemorySize"].ToString());

                // システムドライブを取得
                SystemDrive = queryObj["SystemDrive"].ToString();
            }
            sb.AppendLine("");

            /**
             * ハードディスク空き領域情報取得
             */
            searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_LogicalDisk WHERE DriveType = 3 and DeviceID = '" + SystemDrive + "'");
            sb.Append("Hardisk Space : ");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                sb.AppendLine((((ulong)queryObj["FreeSpace"] / 1024) / 1024) + " MBytes");
            }
            sb.AppendLine("");


            /**
             * ディスプレイドライバ情報
             */
            searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_VideoController");
            StringBuilder DisplayMode = new StringBuilder();
            foreach (ManagementObject queryObj in searcher.Get())
            {
                sb.Append("Graphic Card : ");
                if (queryObj["Name"] != null)
                {
                    sb.Append(queryObj["Name"].ToString());
                }
                sb.AppendLine("");

                sb.AppendLine("");

                sb.Append("Graphic Card Driver : ");
                if (queryObj["InstalledDisplayDrivers"] != null)
                {
                    sb.Append(queryObj["InstalledDisplayDrivers"].ToString());
                    sb.Append(" ( " + queryObj["DriverVersion"].ToString() + ")");
                }
                sb.AppendLine("");

                sb.AppendLine("");

                sb.Append("Graphic Card Memory : ");
                if (queryObj["AdapterRAM"] != null)
                {
                    sb.Append(((uint)queryObj["AdapterRAM"] / 1024));
                    sb.Append(" Kbytes");
                }
                sb.AppendLine("");

                sb.AppendLine("");

                if (queryObj["CurrentHorizontalResolution"] != null && queryObj["CurrentVerticalResolution"] != null)
                {
                    DisplayMode.Append(queryObj["CurrentHorizontalResolution"].ToString());
                    DisplayMode.Append(" x ");
                    DisplayMode.Append(queryObj["CurrentVerticalResolution"].ToString());
                    if (queryObj["CurrentRefreshRate"] != null)
                    {
                        DisplayMode.Append(" (");
                        DisplayMode.Append(queryObj["CurrentRefreshRate"].ToString());
                        DisplayMode.Append(")");
                    }
                    if (queryObj["CurrentBitsPerPixel"] != null)
                    {
                        DisplayMode.Append(" ");
                        DisplayMode.Append(queryObj["CurrentBitsPerPixel"].ToString());
                        DisplayMode.Append("bit");
                    }
                    sb.AppendLine("");
                }
            }

            /**
             * Direct X 情報取得
             */
            sb.Append("DirectX Version : DirectX ");
            sb.Append(SystemInfoForm.getDrirectXVersion());
            sb.AppendLine("");

            sb.AppendLine("");

            /**
             * ディスプレイモードの情報を表示
             */
            if (DisplayMode.ToString() != "")
            {
                sb.Append("Display Mode : ");
                sb.Append(DisplayMode.ToString());
                sb.AppendLine("");

                sb.AppendLine("");
            }

            /**
             * サウンドカード情報
             */
            searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_SoundDevice");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                sb.Append("Sound Card : ");
                if (queryObj["Name"] != null)
                {
                    sb.Append(queryObj["Name"].ToString());
                }
                sb.AppendLine("");

                sb.AppendLine(""); break;
            }

            return sb.ToString();
            //}
            //catch(ArgumentOutOfRangeException aoore)
            //{
            //SimpleLogger.WriteLine(aoore.Source);
            //SimpleLogger.WriteLine(aoore.Message);
            //SimpleLogger.WriteLine(aoore.StackTrace);
            //}
            //catch (Exception ex)
            //{
            //SimpleLogger.WriteLine(ex.Source);
            //SimpleLogger.WriteLine(ex.Message);
            //SimpleLogger.WriteLine(ex.StackTrace);
            //}
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SystemInfoForm_Shown(object sender, EventArgs e)
        {
            textBox_SystemInfo.Text = sysinfo;
        }
    }
}