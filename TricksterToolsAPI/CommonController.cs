using System;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Win32;
using TricksterTools.CommonXmlStructure;
using TricksterTools.Debug;
// Update���̌������i�Ɏg��
using System.Diagnostics;
using System.Security;
using System.Security.Principal;


namespace TricksterTools
{
    namespace API
    {
        namespace Controller
        {
            /// <summary>
            /// ���ʎd�l
            /// </summary>
            public class Common
            {

                //public static string ClientWindowClassHandleName = "classTrickster";
                public static string ClientWindowClassHandleName = "xmflrtmxj";

                //static Logger logger = SimpleLogger.setLogger("Common");
                #region �Í����֌W
                /// <summary>
                /// ��������Í�������
                /// </summary>
                /// <param name="str">�Í������镶����</param>
                /// <param name="key">�p�X���[�h</param>
                /// <returns>�Í������ꂽ������</returns>
                public static string EncryptString(string str, string key)
                {
                    //��������o�C�g�^�z��ɂ���
                    byte[] bytesIn = System.Text.Encoding.UTF8.GetBytes(str);

                    //DESCryptoServiceProvider�I�u�W�F�N�g�̍쐬
                    System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();

                    //���L�L�[�Ə������x�N�^������
                    //�p�X���[�h���o�C�g�z��ɂ���
                    byte[] bytesKey = System.Text.Encoding.UTF8.GetBytes(key);
                    //���L�L�[�Ə������x�N�^��ݒ�
                    des.Key = ResizeBytesArray(bytesKey, des.Key.Length);
                    des.IV = ResizeBytesArray(bytesKey, des.IV.Length);

                    //�Í������ꂽ�f�[�^�������o�����߂�MemoryStream
                    MemoryStream msOut = new System.IO.MemoryStream();
                    //DES�Í����I�u�W�F�N�g�̍쐬
                    System.Security.Cryptography.ICryptoTransform desdecrypt = des.CreateEncryptor();
                    //�������ނ��߂�CryptoStream�̍쐬
                    System.Security.Cryptography.CryptoStream cryptStreem = new System.Security.Cryptography.CryptoStream(msOut, desdecrypt, System.Security.Cryptography.CryptoStreamMode.Write);
                    //��������
                    cryptStreem.Write(bytesIn, 0, bytesIn.Length);
                    cryptStreem.FlushFinalBlock();
                    //�Í������ꂽ�f�[�^���擾
                    byte[] bytesOut = msOut.ToArray();

                    //����
                    cryptStreem.Close();
                    msOut.Close();

                    //Base64�ŕ�����ɕύX���Č��ʂ�Ԃ�
                    return System.Convert.ToBase64String(bytesOut).Replace("=", "");
                }

                /// <summary>
                /// �Í������ꂽ������𕜍�������
                /// </summary>
                /// <param name="str">�Í������ꂽ������</param>
                /// <param name="key">�p�X���[�h</param>
                /// <returns>���������ꂽ������</returns>
                public static string DecryptString(string str, string key)
                {
                    if (str == null) { return ""; }
                    if (str.Length % 4 > 0)
                    {
                        while (str.Length % 4 != 0)
                        {
                            str += "=";
                        }

                    }
                    //DESCryptoServiceProvider�I�u�W�F�N�g�̍쐬
                    System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();

                    //���L�L�[�Ə������x�N�^������
                    //�p�X���[�h���o�C�g�z��ɂ���
                    byte[] bytesKey = System.Text.Encoding.UTF8.GetBytes(key);
                    //���L�L�[�Ə������x�N�^��ݒ�
                    des.Key = ResizeBytesArray(bytesKey, des.Key.Length);
                    des.IV = ResizeBytesArray(bytesKey, des.IV.Length);

                    //Base64�ŕ�������o�C�g�z��ɖ߂�
                    byte[] bytesIn = System.Convert.FromBase64String(str);
                    //�Í������ꂽ�f�[�^��ǂݍ��ނ��߂�MemoryStream
                    MemoryStream msIn = new System.IO.MemoryStream(bytesIn);
                    //DES�������I�u�W�F�N�g�̍쐬
                    System.Security.Cryptography.ICryptoTransform desdecrypt = des.CreateDecryptor();
                    //�ǂݍ��ނ��߂�CryptoStream�̍쐬
                    System.Security.Cryptography.CryptoStream cryptStreem = new System.Security.Cryptography.CryptoStream(msIn, desdecrypt, System.Security.Cryptography.CryptoStreamMode.Read);

                    //���������ꂽ�f�[�^���擾���邽�߂�StreamReader
                    StreamReader srOut =
                        new StreamReader(cryptStreem, System.Text.Encoding.UTF8);
                    //���������ꂽ�f�[�^���擾����
                    string result = srOut.ReadToEnd();

                    //����
                    srOut.Close();
                    cryptStreem.Close();
                    msIn.Close();

                    return result;
                }
                #region ResizeBytesArray()
                /// <summary>
                /// ���L�L�[�p�ɁA�o�C�g�z��̃T�C�Y��ύX����
                /// </summary>
                /// <param name="bytes">�T�C�Y��ύX����o�C�g�z��</param>
                /// <param name="newSize">�o�C�g�z��̐V�����傫��</param>
                /// <returns>�T�C�Y���ύX���ꂽ�o�C�g�z��</returns>
                private static byte[] ResizeBytesArray(byte[] bytes, int newSize)
                {
                    byte[] newBytes = new byte[newSize];
                    if (bytes.Length <= newSize)
                    {
                        for (int i = 0; i < bytes.Length; i++)
                            newBytes[i] = bytes[i];
                    }
                    else
                    {
                        int pos = 0;
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            newBytes[pos++] ^= bytes[i];
                            if (pos >= newBytes.Length)
                                pos = 0;
                        }
                    }
                    return newBytes;
                }
                #endregion
                #endregion
                #region Game Client Information
                /// <summary>
                /// �C���X�g�[������Ă��邩���ׂ�
                /// </summary>
                /// <param name="void"></param>
                /// <returns>boolean</returns>
                public static bool isInstalled()
                {
                    // ���삷�郌�W�X�g���E�L�[�̖��O
                    string rKeyName;
                    if (Common.isx64())
                    {
                        SimpleLogger.WriteLine("Running on 64bit OS");
                        rKeyName = @"SOFTWARE\WOW6432Node\NTreev\Trickster_GCrest";
                    }
                    else
                    {
                        SimpleLogger.WriteLine("Running on 32bit OS");
                        rKeyName = @"SOFTWARE\NTreev\Trickster_GCrest";
                    }

                    // �擾�������s���ΏۂƂȂ郌�W�X�g���̒l�̖��O
                    //string rGetValueName = "FullPath";

                    // ���W�X�g���̎擾
                    try
                    {
                        // ���W�X�g���E�L�[�̃p�X���w�肵�ă��W�X�g�����J��
                        RegistryKey rKey = Registry.LocalMachine.OpenSubKey(rKeyName);

                        // ���W�X�g���̒l���擾
                        //string location = (string)rKey.GetValue(rGetValueName);

                        // �J�������W�X�g���E�L�[�����
                        rKey.Close();

                        SimpleLogger.WriteLine("Trickster Client is installed.");
                        return true;
                    }
                    catch (NullReferenceException)
                    {
                        // ���W�X�g���E�L�[�܂��͒l�����݂��Ȃ�
                        SimpleLogger.WriteLine("Trickster Client is not installed.");
                        return false;
                    }
                }

                /// <summary>
                /// �C���X�g�[����p�X���擾
                /// </summary>
                /// <param name="void"></param>
                /// <returns>�C���X�g�[����p�X</returns>
                public static string getInstallPath()
                {
                    string rKeyName;
                    // ���삷�郌�W�X�g���E�L�[�̖��O
                    if (Common.isx64())
                    {
                        rKeyName = @"SOFTWARE\WOW6432Node\NTreev\Trickster_GCrest";
                    }
                    else
                    {
                        rKeyName = @"SOFTWARE\NTreev\Trickster_GCrest";
                    }
                    // �擾�������s���ΏۂƂȂ郌�W�X�g���̒l�̖��O
                    string rGetValueName = "FullPath";

                    SimpleLogger.WriteLine("get client installed path...");
                    // ���W�X�g���̎擾
                    try
                    {
                        // ���W�X�g���E�L�[�̃p�X���w�肵�ă��W�X�g�����J��
                        RegistryKey rKey = Registry.LocalMachine.OpenSubKey(rKeyName);

                        // ���W�X�g���̒l���擾
                        string location = (string)rKey.GetValue(rGetValueName);

                        // �J�������W�X�g���E�L�[�����
                        rKey.Close();

                        // �擾�������W�X�g���̒lreturn
                        return location;
                    }
                    catch (NullReferenceException)
                    {
                        SimpleLogger.WriteLine("failed to get client installed path.");
                        // ���W�X�g���E�L�[�܂��͒l�����݂��Ȃ�
                        MessageBox.Show("�C���X�g�[���p�X�̎擾�Ɏ��s���܂���", "�����`���[�N����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return @"C:\Games\Trickster\Splash.exe";
                    }
                }

                /// <summary>
                /// �N���C�A���g�̃o�[�W�������擾
                /// </summary>
                /// <param name="void"></param>
                /// <returns>�o�[�W�����ԍ��i��F2.13.2�j</returns>
                public static string getClientVersion()
                {
                    SimpleLogger.WriteLine("get client version...");
                    // ���삷�郌�W�X�g���E�L�[�̖��O
                    string RegKeyName;
                    if (Common.isx64())
                    {
                        RegKeyName = @"SOFTWARE\WOW6432Node\Ntreev\Trickster_GCrest";
                    }
                    else
                    {
                        RegKeyName = @"SOFTWARE\Ntreev\Trickster_GCrest";
                    }
                    // �擾�������s���ΏۂƂȂ郌�W�X�g���̒l�̖��O
                    string rGetValueName = "UpdateVersion";

                    // ���W�X�g���̎擾
                    try
                    {
                        // ���W�X�g���E�L�[�̃p�X���w�肵�ă��W�X�g�����J��
                        RegistryKey RegKey = Registry.LocalMachine.OpenSubKey(RegKeyName);

                        // ���W�X�g���̒l���擾
                        string RegValue = (string)RegKey.GetValue(rGetValueName);

                        // �J�������W�X�g���E�L�[�����
                        RegKey.Close();

                        // �R���\�[���Ɏ擾�������W�X�g���̒l��\��
                        SimpleLogger.WriteLine("client version: " + RegValue);
                        return RegValue;
                    }
                    catch (NullReferenceException)
                    {
                        SimpleLogger.WriteLine("failed to get client version.");
                        // ���W�X�g���E�L�[�܂��͒l�����݂��Ȃ�
                        return "0.0.0";
                    }

                }


                public static string TricksterVersionfromServer = "0.0.0";


                /// <summary>
                /// �T�[�o���o�[�W�������擾
                /// </summary>
                /// <param name="void"></param>
                /// <returns>�o�[�W�����ԍ��i��F2.13.2�j</returns>
                public static string getServerVersion()
                {
                    SimpleLogger.WriteLine("get version from server...");
                    // ���W�X�g���̎擾
                    try
                    {

                        string verInfo = getServerInfo();   // ��M

                        // �o�[�W�����ԍ��ƃt�@�C�����X�g�t�@�C�������擾
                        System.Text.RegularExpressions.Regex RegexVer = new System.Text.RegularExpressions.Regex("version = (?<version>.+)\r\n");
                        System.Text.RegularExpressions.Regex RegexFlist = new System.Text.RegularExpressions.Regex("filelist = (?<flist>filelist.+)\r\n");
                        System.Text.RegularExpressions.Match MatchVer = RegexVer.Match(verInfo);
                        System.Text.RegularExpressions.Match MatchFlist = RegexFlist.Match(verInfo);
                        if (MatchVer.Success)
                        {
                            SimpleLogger.WriteLine("server version: " + MatchVer.Groups["version"].Value);
                            return MatchVer.Groups["version"].Value;
                        }
                        else
                        {
                            // ������������

                            SimpleLogger.WriteLine("failed to get version info.");

                            return "0.0.0";
                        }
                    }
                    catch (WebException we)
                    {
                        // �ʐM���s
                        SimpleLogger.WriteLine(we.Message);
                        MessageBox.Show("�ʐM���s", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SimpleLogger.WriteLine("could not get new version from server.");
                        return "0.0.0";
                    }
                }


                public static string getServerInfo()
                {
                    //string url = "http://version.trickster.jp:5977/version/version";
                    string url = "http://patch.trickster.jp/version/version";

                    //WebRequest�̍쐬
                    System.Net.HttpWebRequest WebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                    WebRequest.Timeout = 5000; // �^�C���A�E�g5�b
                    System.Net.HttpWebResponse WebResponse = null;
                    try
                    {
                        // �T�[�o�[����̉�������M���邽�߂�WebResponse���擾
                        WebResponse = (System.Net.HttpWebResponse)WebRequest.GetResponse();


                        // ��������URI��\������
                        SimpleLogger.WriteLine("ResponseURL: " + WebResponse.ResponseUri.ToString());
                        // �����X�e�[�^�X�R�[�h��\������
                        SimpleLogger.WriteLine("Status: " + (int)WebResponse.StatusCode + " " + WebResponse.StatusCode.ToString());

                        if ((int)WebResponse.StatusCode <= 300)
                        {
                            // �����R�[�h���w�肷��
                            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

                            // �����f�[�^����M���邽�߂�Stream���擾
                            Stream stream = WebResponse.GetResponseStream();
                            StreamReader StreamReader = new StreamReader(stream, enc);

                            string VersionInfo = StreamReader.ReadToEnd();   // ��M
                            SimpleLogger.WriteLine(VersionInfo);

                            // ����
                            WebResponse.Close();

                            return VersionInfo;
                        }
                        else
                        {
                            SimpleLogger.WriteLine((int)WebResponse.StatusCode + ":" + WebResponse.StatusDescription);

                            // ����
                            WebResponse.Close();
                            return "0.0.0";
                        }
                    }
                    catch (WebException we)
                    {
                        SimpleLogger.WriteLine(we.Message);
                        MessageBox.Show("��O�G���[:" + Environment.NewLine + we.Message, "WebException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return "0.0.0";
                    }
                }

                /// <summary>
                /// �o�[�W�������̔�r���A�A�b�v�f�[�g�����邩���肵�܂��B
                /// </summary>
                /// <param name="clientVersion">�N���C�A���g���o�[�W����(1.2.3)</param>
                /// <param name="serverVersion">�T�[�o���o�[�W����(1.2.3)</param>
                /// <returns></returns>
                public static bool isNeedUpdate(string clientVersion, string serverVersion)
                {
                    SimpleLogger.WriteLine("check update...");

                    if(clientVersion == null)
                    {
                        SimpleLogger.WriteLine("client version is null.");
                        clientVersion = "0.0.0";
                    }

                    if (serverVersion == null || serverVersion == "0.0.0")
                    {
                        SimpleLogger.WriteLine("failed to check version.");
                        return false;
                    }
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[0-9.]+$");
                    //if (clientVersion == "�����s")
                    if ( regex.IsMatch(clientVersion, 0) == false )
                    {
                        clientVersion = "0.0.0";
                    }
                    string[] clv = clientVersion.Split('.');
                    string[] svv = serverVersion.Split('.');

                    int major_cl, major_sv, minor_cl, minor_sv, sub_cl, sub_sv;
                    major_cl = System.Convert.ToInt32(clv[0]);
                    minor_cl = System.Convert.ToInt32(clv[1]);
                    sub_cl = System.Convert.ToInt32(clv[2]);
                    major_sv = System.Convert.ToInt32(svv[0]);
                    minor_sv = System.Convert.ToInt32(svv[1]);
                    sub_sv = System.Convert.ToInt32(svv[2]);

                    SimpleLogger.WriteLine("comparering version...");
                    if (clv.Length < 3 || svv.Length < 3)
                    {
                        SimpleLogger.WriteLine("failed to comparer version");
                        return false;
                    }

                    SimpleLogger.WriteLine("Server: " + serverVersion);
                    SimpleLogger.WriteLine("Client: " + clientVersion);

                    // major version
                    if (major_cl < major_sv)
                    {
                        SimpleLogger.WriteLine("need update.");
                        return true;
                    }

                    // minor version
                    if (minor_cl < minor_sv)
                    {
                        SimpleLogger.WriteLine("need update.");
                        return true;
                    }

                    // sub version
                    if (sub_cl < sub_sv)
                    {
                        SimpleLogger.WriteLine("need update.");
                        return true;
                    }

                    SimpleLogger.WriteLine("no update.");
                    return false;
                }

                /// <summary>
                /// x64���ł̓��삩���肵�܂��B
                /// </summary>
                /// <returns>64bit OS�̏ꍇ��true</returns>
                public static bool isx64()
                {
                    // ���݂̃v���b�g�t�H�[���ł̃|�C���^�܂��̓n���h���̃T�C�Y (�o�C�g)�B
                    // ���̃v���p�e�B�̒l�� 32 �r�b�g �v���b�g�t�H�[���ł� 4�A64 �r�b�g �v���b�g�t�H�[���ł� 8 �ł��B
                    return (IntPtr.Size == 8);
                }
                #endregion

                /// <summary>
                /// �Q�[���̃A�b�v�f�[�g���s���܂��B
                /// �A�b�v�f�[�g��A�N�����ꂽ�����`���[�v���O�����͎����I�ɏI�����܂��B
                /// </summary>
                public static void updateGame()
                {
                    string splash_path = Common.getInstallPath();
                    if (!System.IO.File.Exists(splash_path))
                    {
                        MessageBox.Show("�����`���[�v���O���� \"" + splash_path + "\" ��������܂���ł����B", "Error");
                        return;
                    }
                    Common.updateGame(splash_path);
                }

                /// <summary>
                /// �Q�[���̃A�b�v�f�[�g���s���܂��B
                /// �A�b�v�f�[�g��A�N�����ꂽ�����`���[�v���O�����͎����I�ɏI�����܂��B
                /// </summary>
                /// <param name="LauncherPath">�����`���[�iSplash.exe�j�ւ̃t���p�X</param>
                public static void updateGame(string LauncherPath)
                {
                    // Splash.dmy�𒼐ڋN��
                    LauncherPath = LauncherPath.Replace(".exe", ".dmy");

                    bool ignoreDialog = false;

                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                    psi.FileName = LauncherPath;
                    psi.RedirectStandardInput = false;
                    psi.RedirectStandardOutput = false;
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = false;
                    // ���݂̃��[�U���Ǘ��Ҍ�����L���Ă��邩�m�F
                    if (Common.isAdmin() == false)
                    {
                        // �Ǘ��҂łȂ���ΊǗ��Ҍ����Ŏ��s�����݂�
                        psi.Verb = "runas";
                    }
                    
                    SimpleLogger.WriteLine(LauncherPath);
                    // ����
                    //psi.Arguments = "";

                    SimpleLogger.WriteLine("start Launcher and launch Update.");

                    try
                    {
                        // �N��
                        System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);

                        // �����`���[�̃v���Z�X���Ď�
                        System.Diagnostics.PerformanceCounter pc = new System.Diagnostics.PerformanceCounter();
                        pc.CategoryName = "Process";
                        pc.CounterName = "% Processor Time";
                        pc.MachineName = ".";
                        pc.InstanceName = p.ProcessName;
                        float pcp = 0;
                        System.Threading.Thread.Sleep(1000);
                        if (!p.HasExited)
                        {
                            try
                            {
                                pcp = pc.NextValue();
                            }
                            catch (InvalidOperationException ioe)
                            { }
                        }

                        // �ŏ���0%�Ō��o���I���܂�
                        while (!p.HasExited && pcp == 0)
                        {
                            pcp = pc.NextValue();
                            System.Threading.Thread.Sleep(1000);
                        }

                        bool isContinueLauncherMonitor = true;
                        while (isContinueLauncherMonitor)
                        {
                            // �����I��(CPU�g�p��0%)�܂ŊĎ�
                            int zeroCount = 0;
                            while (!p.HasExited && zeroCount < 10)
                            {
                                pcp = pc.NextValue();
                                SimpleLogger.WriteLine(p.ProcessName + ": " + pcp.ToString() + "%");
                                System.Threading.Thread.Sleep(1000);
                                if (pcp > 0)
                                {
                                    zeroCount = 0;
                                }
                                else
                                {
                                    zeroCount++;
                                }
                            }

                            if (p.HasExited == true)
                            {
                                // ���Ƀ����`���[���I�����Ă����ꍇ
                                isContinueLauncherMonitor = false;
                                break;
                            }

                            DialogResult killConfirm = new DialogResult();

                            if (ignoreDialog == false)
                            {
                                killConfirm = MessageBox.Show("�����`���[���I�����܂��B��낵���ł���" + Environment.NewLine +
                                            "���ケ�̃��b�Z�[�W��\�����Ȃ��ꍇ�̓L�����Z����I�����Ă��������B" + Environment.NewLine +
                                            "������A�b�v�f�[�g���s���ɂ͍ēx�\������܂��B", "TricksterTools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            }

                            if (killConfirm == DialogResult.Cancel)
                            {
                                ignoreDialog = true;
                                MessageBox.Show("�A�b�v�f�[�g������́A��x�����`���[���I�������Ă��������B" + Environment.NewLine +
                                            "TSLoginManager������ɓ���o���Ȃ��Ȃ鋰�ꂪ����܂��B", "TricksterTools", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            if (p.HasExited == true || killConfirm == DialogResult.Yes)
                            {
                                isContinueLauncherMonitor = false;

                                if (p.HasExited == false)
                                    p.Kill();


                                break;
                            }
                        }

                        p.Dispose();
                    }
                    catch (System.ComponentModel.Win32Exception e)
                    {
                        SimpleLogger.WriteLine("Exception!!: " + e.GetType().FullName);
                        SimpleLogger.WriteLine(e.Message);
                        MessageBox.Show("�A�b�v�f�[�g�̎��s�ɂ͊Ǘ��Ҍ������K�v�ł��B\n�����𒆒f���܂��B", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                /// <summary>
                /// ���݊Ǘ��Ҍ����Ŏ��s����Ă��邩�m�F���܂�
                /// </summary>
                /// <returns>�Ǘ��Ҍ����̏ꍇ��true, ��ʃ��[�U�̏ꍇ��false</returns>
                public static bool isAdmin()
                {
                    WindowsIdentity usrId = WindowsIdentity.GetCurrent();
                    WindowsPrincipal p = new WindowsPrincipal(usrId);
                    return p.IsInRole(@"BUILTIN\Administrators");
                }

                /// <summary>
                /// 
                /// </summary>
                public static void RestartApplicationAtAdministratorAuthority()
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.UseShellExecute = true;
                    startInfo.WorkingDirectory = Environment.CurrentDirectory;
                    startInfo.FileName = Application.ExecutablePath;
                    startInfo.Verb = "runas";

                    try
                    {
                        Process p = Process.Start(startInfo);
                    }
                    catch
                    {
                        return;
                    }
                    Application.Exit();
                }
            }
        }
    }
}
