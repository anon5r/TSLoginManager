using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
//using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;
using TricksterTools.Library;
using TricksterTools.CommonXmlStructure;
using TricksterTools.API.Controller;
using TricksterTools.Debug;
using TricksterTools.Library.Xml.Settings;

namespace TricksterTools
{
    namespace Library
    {
        namespace LoginManager
        {
            /// <summary>
            /// watcher()��thread�o�[�W����
            /// </summary>
            public class WatchController
            {
                //protected Timer timer;
                protected System.Windows.Forms.Timer timer;
                protected int msec ;
                //public TimerCallback timerCallback;
                private status processStatus = status.End;    // ���݂̃X�e�[�^�X�󋵂��Ď�
                private stats clientStats = stats.Shutdown;     // �N��/�I���݂̂��Ď�����
                //private int hungCount;          // �����Ȃ����o�񐔂��J�E���g
                public static System.Diagnostics.Process TrickProcess;  // Trickster�̃v���Z�XID

                


                /// <summary>
                /// �N���C�A���g�̋N���ۃX�e�[�^�X
                /// </summary>
                public enum stats
                {
                    /// <summary>
                    /// �N����
                    /// </summary>
                    Start,

                    /// <summary>
                    /// �I��
                    /// </summary>
                    Shutdown
                }

                /// <summary>
                /// �v���Z�X�̓���X�e�[�^�X
                /// </summary>
                public enum status{
                    /// <summary>
                    /// ���쒆
                    /// </summary>
                    Running,

                    /// <summary>
                    /// �n���O�A�b�v
                    /// </summary>
                    Stop,

                    /// <summary>
                    /// TSLoginManager�ɂ�鋭���I����
                    /// </summary>
                    ForceKilled,

                    /// <summary>
                    /// �I��
                    /// </summary>
                    End
                }

                /// <summary>
                /// �Q�[���N���C�A���g�̌��݂̏�Ԃ��擾�܂��͐ݒ肵�܂��B
                /// </summary>
                /// <param name="stat">Running/Freeze</param>
                public status ProcessStatus{
                    set
                    {
                        this.processStatus = value;
                    }

                    get
                    {
                        return this.processStatus;
                    }
                }
                
                /// <summary>
                /// �Q�[���N���C�A���g�̋N��/�I����Ԃ��擾�܂��͐ݒ肵�܂��B
                /// </summary>
                /// <param name="stat">Start/Shutdown</param>
                public stats gameStats
                {
                    set
                    {
                        this.clientStats = value;
                    }

                    get
                    {
                        return this.clientStats;
                    }
                }


                /// <summary>
                /// Initialize
                /// ���������܂��B
                /// </summary>
                public WatchController()
                {
                    //this.hungCount = 0;
                    this.ProcessStatus = WatchController.processWatch();
                    //this.timerCallback = new TimerCallback(this.detectHungUp);
                    //this.timer = new Timer(this.timerCallback);
                }

                //static Logger logger = SimpleLogger.setLogger("ProgramController");
                //private static bool _flag_hung_diag = false; // �_�C�A���O�\�����i�X���[�v�������Ԃ���Ȃ��悤�Ɂj

                /*
                #region detectHungUp()
                /// <summary>
                /// �����Ȃ������o���܂��B
                /// </summary>
                public void detectHungUp(object state)
                {
                    if (!SettingController.HungUp.enable)
                    {
                        // �t���[�Y���o������
                        return;
                    }
                    
                    // �N���C�A���g�̏�Ԕ���
                    if (this.gameStats == stats.Shutdown && this.ProcessStatus == status.Running)
                    {
                        this.gameStats = stats.Start;
                        SimpleLogger.WriteLine("Trickster " + this.gameStats);
                    }
                    else if (this.gameStats == stats.Start && this.ProcessStatus == status.End)
                    {
                        this.gameStats = stats.Shutdown;
                        SimpleLogger.WriteLine("Trickster " + this.gameStats);
                    }

                    // �t���[�Y����
                    int msec = SettingController.HungUp.sec * 1000;   // �P�ʃ~���b
                    this.watcher(timer, msec);
                }
                #endregion

                public void Run()
                {
                    while(true){
                        this.watcher(this.timer, SettingController.HungUp.sec);
                    }
                }
                */

                public void setTimer(System.Windows.Forms.Timer timer, int msec)
                {
                    this.timer = timer;
                    this.msec = msec;
                }


                #region watcher()
                /// <summary>
                /// �g���b�N�X�^�[�̃N���C�A���g���Ď����A��莞�ԁu�����Ȃ��v�ɂȂ����ꍇ��
                /// �����I�����s�����ۂ̑I���ƁA�I���̏������s���܂��B
                /// </summary>
                /// <param name="timer">�Ď�������Timer�N���X���w��</param>
                /// <param name="msec">�����Ȃ��Ɣ��肷�鎞�ԁi�~���b�P�ʁj</param>
                //public void watcher(Timer timer, int msec)
                public void watcher(System.Windows.Forms.Timer timer, int msec)
                {
                    this.setTimer(timer, msec);
                }

                /// <summary>
                /// �g���b�N�X�^�[�̃N���C�A���g���Ď����A��莞�ԁu�����Ȃ��v�ɂȂ����ꍇ��
                /// �����I�����s�����ۂ̑I���ƁA�I���̏������s���܂��B
                /// </summary>
                public void watcher()
                {
                    status proc = WatchController.processWatch();
                    if(proc != status.End)
                    {
                        this.gameStats = stats.Start;

                        // �N����
                        if (this.ProcessStatus != status.Running)
                        {
                            this.ProcessStatus = status.Running;
                            //SimpleLogger.WriteLine("Trickster Client start...");
                        }

                        /*
                         * 1��ڂ̉����Ȃ�����
                         * �����F1��ŉ����Ȃ��Ƃ݂Ȃ��Ȃ����R�́A�}�b�v�ړ���L�����N�^�[�f�[�^
                         * �ǂݍ��ݎ��ɂ��ꎞ�I�ɉ����Ȃ���ԂƂ��Č��o����Ă��܂����߁B
                         */
                        if (proc == status.Stop)
                        {
                            // �����Ȃ����o��
                            SimpleLogger.WriteLine("detected hunged up trickster.");

                            /*
                             * ���o�������sleep�Փ˂ɂ��A�v���P�[�V�������̂̃n���O�A�b�v����̂��߁A
                             * �ꎞ�I�Ƀ^�C�}�[�𖳌���
                             */
                            //timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                            if (this.processStatus == status.Stop)
                                return;

                            this.ProcessStatus = status.Stop;
                            SimpleLogger.WriteLine("sleep " + msec + "msec.");
                            System.Threading.Thread.Sleep(msec);   // sleep
                            SimpleLogger.WriteLine("got up.");
                            this.ProcessStatus = status.Running;

                            proc = WatchController.processWatch();
                            if (proc == status.Running)
                            {
                                this.ProcessStatus = status.Running;
                                SimpleLogger.WriteLine("detected to revive.");
                                // �����񕜂̏ꍇ�͏C���ĊJ
                                timer.Enabled = true;
                                return;
                            }

                            /*
                             * ���o臒l���ԕ�sleep���Ă��������Ȃ��ꍇ��
                             * �����ɉ����Ȃ��Ɣ��肷��
                             */
                            if (proc == status.Stop)
                            {
                                this.ProcessStatus = status.Stop;
                                // �ꎞ�I��timer�𖳌���
                                timer.Enabled = false;
                                SimpleLogger.WriteLine("Trickster process is hunged up !!");
                                System.Windows.Forms.DialogResult MsgRes = System.Windows.Forms.MessageBox.Show("Trickster �̉����Ȃ������o���܂����B" + Environment.NewLine + "�Q�[���������I�����܂����H", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                                if (MsgRes == System.Windows.Forms.DialogResult.Yes)
                                {
                                    SimpleLogger.WriteLine("Trickster ProcessID: " + WatchController.TrickProcess.Id);

                                    try
                                    {
                                        WatchController.TrickProcess.Kill();
                                        SimpleLogger.WriteLine("ProcessID: " + WatchController.TrickProcess.Id + " is killed.");
                                        this.ProcessStatus = status.ForceKilled;
                                    }
                                    catch (System.InvalidOperationException ioe)
                                    {
                                        SimpleLogger.WriteLine(ioe.Message);

                                        SimpleLogger.WriteLine("failed to kill " + WatchController.TrickProcess.Id);
                                        System.Windows.Forms.MessageBox.Show("�Q�[���̏I�������Ɏ��s���܂����B", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    }
                                    catch (System.SystemException se)
                                    {
                                        SimpleLogger.WriteLine(se.Message);
                                        SimpleLogger.WriteLine("failed to kill " + WatchController.TrickProcess.Id);
                                        System.Windows.Forms.MessageBox.Show(se.Message, "Exceptional error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                        System.Windows.Forms.MessageBox.Show("�Q�[���̏I�������Ɏ��s���܂����B", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    SimpleLogger.WriteLine("ignore hungup");
                                    //this.hungCount = 0;
                                    this.ProcessStatus = status.Running;
                                }

                                timer.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        // �I����
                        // �N�������`�Ղ���������^�X�N�o�[�̕\�������݂�
                        if (this.gameStats == stats.Start && (this.ProcessStatus == status.Running || this.ProcessStatus == status.ForceKilled))
                        {
                            SimpleLogger.WriteLine("try to recover taskbar.");
                            ProgramController.showTaskBar();
                        }
                        this.processStatus = status.End;
                        this.gameStats = stats.Shutdown;

                    }
                }
                #endregion

                /// <summary>
                /// �g���b�N�X�^�[�N���C�A���g�̃v���Z�X�̏�Ԃ��Ď����܂��B
                /// </summary>
                /// <returns>�v���Z�X�̏�Ԃ�status�^�ŕԂ��܂��B(Running/Stop/End)</returns>
                private static status processWatch()
                {
                    int hWnd = 0;
                    //StringBuilder sbClassName = new StringBuilder(256);
                    //Win32API.GetClassName(hwnd, sbClassName, sbClassName.Capacity);

                    // Trickster.bin�̃E�B���h�E���Ď�
                    hWnd = Win32API.FindWindow(Common.ClientWindowClassHandleName, null);
                    if (hWnd > 0)
                    {
                        if (Win32API.IsHungAppWindow((IntPtr)hWnd))
                        {
                            return status.Stop;
                        }

                        return status.Running;
                    }
                    else
                    {
                        return status.End;
                    }
                }

            }


            /// <summary>
            /// �v���O�����̓���ɕK�v�ȕ���
            /// </summary>
            public class ProgramController
            {

                public static AccountController AController = new AccountController();
                //public static WatchController Watcher = new WatchController();

                #region showTaskBar()
                /// <summary>
                /// �^�X�N�o�[�̉񕜗p
                /// </summary>
                public static void showTaskBar()
                {
                    int intRet = Win32API.FindWindow("Shell_TrayWnd", "");
                    Win32API.SetWindowPos(intRet, 0, 0, 0, 0, 0, Win32API.SWP_SHOWWINDOW);
                }
                #endregion
                #region UrlEncode()
                /// <summary>
                /// URL�G���R�[�h
                /// </summary>
                /// <param name="s">�G���R�[�h���镶����</param>
                /// <param name="enc">�G���R�[�f�B���O</param>
                /// <returns></returns>
                public static string UrlEncode(string s, Encoding enc)
                {
                    StringBuilder rt = new StringBuilder();
                    foreach (byte i in enc.GetBytes(s))
                        if (i == 0x20)
                            rt.Append('+');
                        else if (i >= 0x21 && i <= 0x7e)
                            rt.Append((char)i);
                        else
                            rt.Append("%" + i.ToString("X2"));
                    return rt.ToString();
                }
                #endregion
                #region UrlDecode()
                /// <summary>
                /// 
                /// </summary>
                /// <param name="s">�f�R�[�h���镶����</param>
                /// <param name="enc">�G���R�[�f�B���O</param>
                /// <returns></returns>
                public static string UrlDecode(string s, Encoding enc)
                {
                    List<byte> bytes = new List<byte>();
                    for (int i = 0; i < s.Length; i++)
                    {
                        char c = s[i];
                        if (c == '%')
                            bytes.Add(byte.Parse(s[++i].ToString() + s[++i].ToString()));
                        else if (c == '+')
                            bytes.Add((byte)0x20);
                        else
                            bytes.Add((byte)c);
                    }
                    return enc.GetString(bytes.ToArray(), 0, bytes.Count);
                }
                #endregion
                /*
                public static void startWatch()
                {
                    Watcher.timer = new Timer(ProgramController.Watcher.timerCallback, null, 0, 1000);
                    Watcher.timerCallback = new TimerCallback(ProgramController.Watcher.detectHungUp);
                    Thread th = new Thread(new ThreadStart(Watcher.Run));
                }
                */
                #region ���Ď��V�X�e��
                /*
                //static Logger logger = SimpleLogger.setLogger("ProgramController");
                // Trickster.bin���N�����Ă��邩���Ď����邽�߂̃t���O
                private static bool _flg_start_ts = false;      // �N��������p
                private static bool _flag_hung_ts_diag = false; // �_�C�A���O�\�����i�X���[�v�������Ԃ���Ȃ��悤�Ɂj
                private static bool _flag_sleeping = false;     // �X���[�v������
                private static bool _flag_ts_run_switch = false;  // �N�����X�C�b�`
                public static System.Diagnostics.Process TrickProcess;

                #region detectHungUp()
                /// <summary>
                /// 
                /// </summary>
                private void detectHungUp()
                {
                    if (!SettingController.HungUp.enable)
                    {
                        // �t���[�Y���o������
                        return;
                    }
                    string status = "";

                    // �N���C�A���g�̏�Ԕ���
                    if (ProgramController.getChangedClientStatus(ref status))
                    {
                        SimpleLogger.WriteLine("Trickster " + status);
                    }
                    // �t���[�Y����
                    int msec = SettingController.HungUp.sec * 1000;   // �P�ʃ~���b
                    ProgramController.Watcher(timer, msec);
                }
                #endregion


                #region getChangedClientStatus()
                /// <summary>
                /// �N���C�A���g�̏�Ԃ��擾���܂��B
                /// </summary>
                /// <param name="status">���: Start:Shutdown</param>
                /// <returns>��������true, ���s����false��Ԃ��܂��B</returns>
                public static bool getChangedClientStatus(ref string status)
                {
                    if (_flg_start_ts && !_flag_ts_run_switch)
                    {
                        _flag_ts_run_switch = true;
                        status = "Start";
                        return true;
                    }
                    else if (!_flg_start_ts && _flag_ts_run_switch)
                    {
                        _flag_ts_run_switch = false;
                        status = "Shutdown";
                        return true;
                    }

                    return false;
                }
                #endregion

                #region watcher()
                /// <summary>
                /// �g���b�N�X�^�[�̃N���C�A���g���Ď����A��莞�ԁu�����Ȃ��v�ɂȂ����ꍇ��
                /// �����I�����s�����ۂ̑I���ƁA�I���̏������s���܂��B
                /// </summary>
                /// <param name="timer">�Ď�������Timer�N���X���w��</param>
                /// <param name="msec">�����Ȃ��Ɣ��肷�鎞�ԁi�~���b�P�ʁj</param>
                public static void watcher(System.Threading.Timer timer, int msec)
                {
                    int hWnd = 0;
                    //StringBuilder sbClassName = new StringBuilder(256);
                    //Win32API.GetClassName(hwnd, sbClassName, sbClassName.Capacity);

                    // Trickster.bin�̃E�B���h�E���Ď�
                    hWnd = Win32API.FindWindow("classTrickster", null);
                    if (hWnd > 0)
                    {
                        // �N����
                        if (!_flg_start_ts)
                        {
                            _flg_start_ts = true;
                        }

                         // 1��ڂ̉����Ȃ�����
                         // �����F1��ŉ����Ȃ��Ƃ݂Ȃ��Ȃ����R�́A�}�b�v�ړ���L�����N�^�[�f�[�^
                         // �ǂݍ��ݎ��ɂ��ꎞ�I�ɉ����Ȃ���ԂƂ��Č��o����Ă��܂����߁B
                        if (Win32API.IsHungAppWindow((IntPtr)hWnd))
                        {
                            // �����Ȃ����o��
                            SimpleLogger.WriteLine("detected hunged up trickster.");

                             // ���o�������sleep�Փ˂ɂ��A�v���P�[�V�������̂̃n���O�A�b�v����̂��߁A
                             // �ꎞ�I�Ƀ^�C�}�[�𖳌���
                            //timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                            if (_flag_sleeping)
                                return;

                            _flag_sleeping = true;
                            SimpleLogger.WriteLine("sleep " + msec + "msec.");
                            System.Threading.Thread.Sleep(msec);   // sleep
                            SimpleLogger.WriteLine("get upped.");
                            _flag_sleeping = false;


                            if (!Win32API.IsHungAppWindow((IntPtr)hWnd))
                            {
                                SimpleLogger.WriteLine("detected to revive.");
                                // �����񕜂̏ꍇ�͏C���ĊJ
                                return;
                            }

                             // ���o臒l���ԕ�sleep���Ă��������Ȃ��ꍇ��
                             // �����ɉ����Ȃ��Ɣ��肷��
                             
                            if (_flag_hung_ts_diag == false)
                            {
                                _flag_hung_ts_diag = true;
                                // �ꎞ�I��timer�𖳌���
                                SimpleLogger.WriteLine("Trickster process is hunged up !!");
                                System.Windows.Forms.DialogResult MsgRes = System.Windows.Forms.MessageBox.Show("Trickster �̉����Ȃ������o���܂����B" + Environment.NewLine + "�Q�[���������I�����܂����H", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                                if (MsgRes == System.Windows.Forms.DialogResult.Yes)
                                {
                                    //uint processId = 0, threadId = 0;
                                    //threadId = Win32API.GetWindowThreadProcessId((IntPtr)hWnd, out processId);

                                    SimpleLogger.WriteLine("Trickster ProcessID: " + ProgramController.TrickProcess.Id);

                                    //System.Diagnostics.Process prc = System.Diagnostics.Process.GetProcessById((int)processId);
                                    try
                                    {
                                        ProgramController.TrickProcess.Kill();
                                        SimpleLogger.WriteLine("ProcessID: " + ProgramController.TrickProcess.Id + " is killed.");
                                        //prc.Kill();
                                    }
                                    catch (System.InvalidOperationException ioe)
                                    {
                                        SimpleLogger.WriteLine(ioe.Message);

                                        //MessageBox.Show(ioe.Message);
                                        SimpleLogger.WriteLine("failed to kill " + ProgramController.TrickProcess.Id);
                                        System.Windows.Forms.MessageBox.Show("�Q�[���̏I�������Ɏ��s���܂����B", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    }
                                    catch (System.SystemException se)
                                    {
                                        SimpleLogger.WriteLine(se.Message);
                                        SimpleLogger.WriteLine("failed to kill " + ProgramController.TrickProcess.Id);
                                        System.Windows.Forms.MessageBox.Show(se.Message, "Exceptional error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                        System.Windows.Forms.MessageBox.Show("�Q�[���̏I�������Ɏ��s���܂����B", "Trickster Tools", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    SimpleLogger.WriteLine("ignore hungup");
                                    _flag_hung_ts_diag = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        // �I����
                        // �N�������`�Ղ���������^�X�N�o�[�̕\�������݂�
                        if (_flg_start_ts)
                        {
                            SimpleLogger.WriteLine("try to recover taskbar.");
                            ProgramController.showTaskBar();
                            _flg_start_ts = false;
                        }
                    }
                }
                #endregion
                */
                #endregion
                #region getProcessId()
                /*
                /// <summary>
                /// �N�����Ă���E�B���h�E�n���h������v���Z�XID���擾����
                /// </summary>
                /// <param name="hWnd">�E�B���h�E�n���h��</param>
                /// <returns>�v���Z�XID</returns>
                public static uint getProcessId(IntPtr hWnd)
                {
                    uint procId = 0;
                    foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
                    {
                        if (p.MainWindowHandle != IntPtr.Zero)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(p.MainWindowTitle, @"^Trickster -"))
                            {
                                string processName = p.ProcessName;
                                break;
                            }

                        }
                    }

                    Win32API.GetWindowThreadProcessId(hWnd, procId);
                    return procId;
                }
                */
                #endregion


            }

        }
    }
}
