using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace TricksterTools
{
    namespace Plugins
    {
        public class Win32API
        {

            public const int SWP_HIDEWINDOW = 0x80;
            public const int SWP_SHOWWINDOW = 0x40;
            public const int SW_HIDE = 0;
            public const int SW_SHOWNORMAL = 1;
            public const int SW_NORMAL = 1;
            public const int SW_SHOWMINIMIZED = 2;
            public const int SW_SHOWMAXIMIZED = 3;
            public const int SW_MAXIMIZE = 3;
            public const int SW_SHOWNOACTIVATE = 4;
            public const int SW_SHOW = 5;
            public const int SW_MINIMIZE = 6;
            public const int SW_SHOWMINNOACTIVE = 7;
            public const int SW_SHOWNA = 8;
            public const int SW_RESTORE = 9;
            public const int SW_SHOWDEFAULT = 10;
            public const int SW_FORCEMINIMIZE = 11;
            public const int SW_MAX = 11;

            //［閉じる］ボタンを無効化するための値
            public const UInt32 SC_CLOSE = 0x0000F060;
            public const UInt32 MF_BYCOMMAND = 0x00000000;

            // EnumDisplaySettingsで使用
            public const int ENUM_CURRENT_SETTINGS = -1;
            public const int ENUM_REGISTRY_SETTINGS = -2;

            // EnumDisplayDevicesで使用
            public const int DISPLAY_DEVICE_ATTACHED_TO_DESKTOP = 0x00000001;
            public const int DISPLAY_DEVICE_MULTI_DRIVER        = 0x00000002;
            public const int DISPLAY_DEVICE_PRIMARY_DEVICE      = 0x00000004;
            public const int DISPLAY_DEVICE_MIRRORING_DRIVER    = 0x00000008;
            public const int DISPLAY_DEVICE_VGA                 = 0x00000010;


            /// <summary>
            /// 
            /// </summary>
            /// <param name="hWnd"></param>
            /// <param name="bRevert"></param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern IntPtr GetSystemMenu(IntPtr hWnd, UInt32 bRevert);


            /// <summary>
            /// 
            /// </summary>
            /// <param name="hMenu"></param>
            /// <param name="nPosition"></param>
            /// <param name="wFlags"></param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern UInt32 RemoveMenu(IntPtr hMenu, UInt32 nPosition, UInt32 wFlags);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hWnd"></param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
            public static extern bool SetForegroundWindow(HandleRef hWnd);

            /// <summary>
            /// クラス名、ウィンドウ名に一致するウィンドウのハンドルを取得
            /// </summary>
            /// <param name="lpClassName">クラス名</param>
            /// <param name="lpWindowName">ウィンドウ名</param>
            /// <returns>ウィンドウハンドル</returns>
            [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindowA")]
            public static extern int FindWindow(string lpClassName, string lpWindowName);


            /// <summary>
            /// ウィンドウポジションと表示状態を設定
            /// </summary>
            /// <param name="hwnd">ウィンドウハンドル</param>
            /// <param name="hWndInsertAfter"></param>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="cx"></param>
            /// <param name="cy"></param>
            /// <param name="wFlags">ウィンドウの状態</param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

            /// <summary>
            /// ウィンドウの表示状態を設定
            /// </summary>
            /// <param name="hWnd">ウィンドウハンドル</param>
            /// <param name="nCmdShow">表示状態</param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);


            /// <summary>
            /// ウィンドウのクラス名を取得
            /// </summary>
            /// <param name="hWnd">ウィンドウハンドル</param>
            /// <param name="s">クラス名</param>
            /// <param name="nMaxCount">クラス名のバッファサイズ</param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public static extern int GetClassName(IntPtr hWnd, StringBuilder s, int nMaxCount);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);



            /// <summary>
            /// 指定のWindowハンドルが「応答なし」か判定する
            /// </summary>
            /// <param name="hWnd"></param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool IsHungAppWindow(IntPtr hWnd);

            /// <summary>
            /// 指定されたウィンドウを作成したスレッドのIDとプロセスIDを取得
            /// </summary>
            /// <param name="hWnd">対象となるWindowハンドラ</param>
            /// <param name="lpdwProcessId">プロセスID</param>
            /// <returns>スレッドID</returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

            [System.Runtime.InteropServices.DllImport("kernel32.dll")]
            public static extern bool AttachConsole(uint dwProcessId);

            [System.Runtime.InteropServices.DllImport("kernel32.dll")]
            public static extern bool FreeConsole();

            /// <summary>
            /// 指定されたウィンドウを作成したスレッドの ID を取得します。
            /// 必要であれば、ウィンドウを作成したプロセスの ID も取得できます。
            /// </summary>
            /// <param name="hWnd">ウィンドウのハンドル</param>
            /// <param name="lpdwProcessId">プロセス ID</param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern uint GetWindowThreadProcessId(IntPtr hWnd, uint lpdwProcessId);


            public struct DISPLAY_DEVICE_MODE
            {
                private const int CCHDEVICENAME = 0x20;
                private const int CCHFORMNAME = 0x20;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
                public string dmDeviceName;
                public short dmSpecVersion;
                public short dmDriverVersion;
                public short dmSize;
                public short dmDriverExtra;
                public int dmFields;
                public int dmPositionX;
                public int dmPositionY;
                public int dmDisplayOrientation;
                public int dmDisplayFixedOutput;
                public short dmColor;
                public short dmDuplex;
                public short dmYResolution;
                public short dmTTOption;
                public short dmCollate;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
                public string dmFormName;
                public short dmLogPixels;
                public int dmBitsPerPel;
                public int dmPelsWidth;
                public int dmPelsHeight;
                public int dmDisplayFlags;
                public int dmDisplayFrequency;
                public int dmICMMethod;
                public int dmICMIntent;
                public int dmMediaType;
                public int dmDitherType;
                public int dmReserved1;
                public int dmReserved2;
                public int dmPanningWidth;
                public int dmPanningHeight;
            }

            /// <summary>
            /// ディスプレイデバイスのいずれかのグラフィックスモードに関する情報を取得します。
            /// 連続して呼び出すと、ディスプレイデバイスのすべてのグラフィックスモードに関する情報を取得することができます。
            /// </summary>
            /// <param name="lpszDeviceName">ディスプレイデバイス</param>
            /// <param name="iModeNum">グラフィックスモード</param>
            /// <param name="lpDevMode">グラフィックスモードの設定</param>
            /// <returns>関数が成功すると 0 以外の値が返ります。関数が失敗すると 0 が返ります。</returns>
            [DllImport("user32.dll")]
            public static extern int EnumDisplaySettings(string deviceName, int modeNum, ref DISPLAY_DEVICE_MODE devMode);

            /*
            public struct DISPLAY_DEVICE {
                int cb;
                char DeviceName;
                char DeviceString;
                int StateFlags;
            }
            */

            /// <summary>
            /// システムのディスプレイデバイス情報を取得します。
            /// </summary>
            /// <param name="Unused">使いません。NULL を指定してください</param>
            /// <param name="iDevNum">ディスプレイデバイスを指定します</param>
            /// <param name="lpDisplayDevice">ディスプレイデバイス情報を受け取る構造体へのポインタ</param>
            /// <param name="dwFlags">条件関数の動作に関するフラグ</param>
            /// <returns>関数が成功すると 0 以外の値が返ります。
            /// 関数が失敗すると 0 が返ります。
            /// IDevNum パラメータに最大デバイスインデックス番号より大きい値が指定されていると関数が失敗します。</returns>
            //[DllImport("user32.dll")]
            //public static extern bool EnumDisplayDevices(DISPLAY_DEVICE Unused, int iDevNum, DISPLAY_DEVICE lpDisplayDevice, int dwFlags);



            
            [DllImport("kernel32.Dll")]
            public static extern short GetVersionEx(ref OSVERSIONINFO o);
            [StructLayout(LayoutKind.Sequential)]
            public struct OSVERSIONINFO
            {
                public int dwOSVersionInfoSize;
                public int dwMajorVersion;
                public int dwMinorVersion;
                public int dwBuildNumber;
                public int dwPlatformId;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
                public string szCSDVersion;
            }

            /// <summary>
            /// サービスパックの情報を取得します。
            /// </summary>
            /// <returns></returns>
            public static string GetServicePack()
            {
                OSVERSIONINFO os = new OSVERSIONINFO();
                os.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFO));
                GetVersionEx(ref os);
                if (os.szCSDVersion == "")
                    //return "No Service Pack Installed";
                    return "";
                else
                    return os.szCSDVersion;
            }

        }

        /// <summary>
        /// wininet.dll
        /// </summary>
        public class WinInet
        {
            [System.Runtime.InteropServices.DllImport("wininet.dll")]
            public extern static bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);

        }
    }
}
