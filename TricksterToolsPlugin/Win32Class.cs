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

            //�m����n�{�^���𖳌������邽�߂̒l
            public const UInt32 SC_CLOSE = 0x0000F060;
            public const UInt32 MF_BYCOMMAND = 0x00000000;

            // EnumDisplaySettings�Ŏg�p
            public const int ENUM_CURRENT_SETTINGS = -1;
            public const int ENUM_REGISTRY_SETTINGS = -2;

            // EnumDisplayDevices�Ŏg�p
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
            /// �N���X���A�E�B���h�E���Ɉ�v����E�B���h�E�̃n���h�����擾
            /// </summary>
            /// <param name="lpClassName">�N���X��</param>
            /// <param name="lpWindowName">�E�B���h�E��</param>
            /// <returns>�E�B���h�E�n���h��</returns>
            [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindowA")]
            public static extern int FindWindow(string lpClassName, string lpWindowName);


            /// <summary>
            /// �E�B���h�E�|�W�V�����ƕ\����Ԃ�ݒ�
            /// </summary>
            /// <param name="hwnd">�E�B���h�E�n���h��</param>
            /// <param name="hWndInsertAfter"></param>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="cx"></param>
            /// <param name="cy"></param>
            /// <param name="wFlags">�E�B���h�E�̏��</param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

            /// <summary>
            /// �E�B���h�E�̕\����Ԃ�ݒ�
            /// </summary>
            /// <param name="hWnd">�E�B���h�E�n���h��</param>
            /// <param name="nCmdShow">�\�����</param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);


            /// <summary>
            /// �E�B���h�E�̃N���X�����擾
            /// </summary>
            /// <param name="hWnd">�E�B���h�E�n���h��</param>
            /// <param name="s">�N���X��</param>
            /// <param name="nMaxCount">�N���X���̃o�b�t�@�T�C�Y</param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public static extern int GetClassName(IntPtr hWnd, StringBuilder s, int nMaxCount);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);



            /// <summary>
            /// �w���Window�n���h�����u�����Ȃ��v�����肷��
            /// </summary>
            /// <param name="hWnd"></param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool IsHungAppWindow(IntPtr hWnd);

            /// <summary>
            /// �w�肳�ꂽ�E�B���h�E���쐬�����X���b�h��ID�ƃv���Z�XID���擾
            /// </summary>
            /// <param name="hWnd">�ΏۂƂȂ�Window�n���h��</param>
            /// <param name="lpdwProcessId">�v���Z�XID</param>
            /// <returns>�X���b�hID</returns>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

            [System.Runtime.InteropServices.DllImport("kernel32.dll")]
            public static extern bool AttachConsole(uint dwProcessId);

            [System.Runtime.InteropServices.DllImport("kernel32.dll")]
            public static extern bool FreeConsole();

            /// <summary>
            /// �w�肳�ꂽ�E�B���h�E���쐬�����X���b�h�� ID ���擾���܂��B
            /// �K�v�ł���΁A�E�B���h�E���쐬�����v���Z�X�� ID ���擾�ł��܂��B
            /// </summary>
            /// <param name="hWnd">�E�B���h�E�̃n���h��</param>
            /// <param name="lpdwProcessId">�v���Z�X ID</param>
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
            /// �f�B�X�v���C�f�o�C�X�̂����ꂩ�̃O���t�B�b�N�X���[�h�Ɋւ�������擾���܂��B
            /// �A�����ČĂяo���ƁA�f�B�X�v���C�f�o�C�X�̂��ׂẴO���t�B�b�N�X���[�h�Ɋւ�������擾���邱�Ƃ��ł��܂��B
            /// </summary>
            /// <param name="lpszDeviceName">�f�B�X�v���C�f�o�C�X</param>
            /// <param name="iModeNum">�O���t�B�b�N�X���[�h</param>
            /// <param name="lpDevMode">�O���t�B�b�N�X���[�h�̐ݒ�</param>
            /// <returns>�֐������������ 0 �ȊO�̒l���Ԃ�܂��B�֐������s����� 0 ���Ԃ�܂��B</returns>
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
            /// �V�X�e���̃f�B�X�v���C�f�o�C�X�����擾���܂��B
            /// </summary>
            /// <param name="Unused">�g���܂���BNULL ���w�肵�Ă�������</param>
            /// <param name="iDevNum">�f�B�X�v���C�f�o�C�X���w�肵�܂�</param>
            /// <param name="lpDisplayDevice">�f�B�X�v���C�f�o�C�X�����󂯎��\���̂ւ̃|�C���^</param>
            /// <param name="dwFlags">�����֐��̓���Ɋւ���t���O</param>
            /// <returns>�֐������������ 0 �ȊO�̒l���Ԃ�܂��B
            /// �֐������s����� 0 ���Ԃ�܂��B
            /// IDevNum �p�����[�^�ɍő�f�o�C�X�C���f�b�N�X�ԍ����傫���l���w�肳��Ă���Ɗ֐������s���܂��B</returns>
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
            /// �T�[�r�X�p�b�N�̏����擾���܂��B
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
