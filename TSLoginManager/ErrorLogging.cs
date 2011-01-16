using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Windows.Forms;

namespace TSLoginManager
{
    class ErrorLogging
    {

        private static readonly object LockObj = new object();
        public static bool _endingFlag = false;        // �I���t���O
        public static bool TraceFlag = false;

        public static void TraceOut(string Message)
        {
            TraceOut(TraceFlag, Message);
        }

        public static void TraceOut(bool OutputFlag, String Message)
        {
            lock (LockObj)
            {
                if (!OutputFlag) return;
                
                DateTime now = DateTime.Now;
                string fileName = String.Format("TSLoginManagerTrace-{0:0000}{1:00}{2:00}-{3:00}{4:00}{5:00}.log", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName))
                {
                    writer.WriteLine("**** TraceOut: {0} ****", DateTime.Now.ToString());
                    writer.WriteLine("���̃t�@�C���̓��e�� anon.jp@gmail.com �܂ő����Ă���������Ə�����܂��B");
                    writer.WriteLine();
                    writer.WriteLine("�����:");
                    writer.WriteLine("   �I�y���[�e�B���O �V�X�e��: {0}", Environment.OSVersion.VersionString);
                    writer.WriteLine("   ���ʌ��ꃉ���^�C��          : {0}", Environment.Version.ToString());
                    writer.WriteLine("   TSLoginManager�̃o�[�W����    : {0}", Application.ProductVersion.ToString());
                    writer.WriteLine(Message);
                    writer.WriteLine();
                }
            }
        }

        // �G���[���e���o�b�t�@�ɏ����o��
        // ���ӁF�ŏI�I�Ƀt�@�C���o�͂����G���[���O�ɋL�^����邽�ߎ��̏��͏����o���Ȃ�
        // �������b�Z�[�W�A�����A�����
        // Data�v���p�e�B�ɂ���I�����t���O�̃p�[�X�������ōs��

        public static string OutputException(Exception ex, String buffer, ref bool IsTerminatePermission)
        {

            StringBuilder buf = new StringBuilder();

            buf.AppendFormat("��O {0}: {1}", ex.GetType().FullName, ex.Message);
            buf.AppendLine();
            if(ex.Data != null )
            {
                bool needHeader = true;
                foreach (System.Collections.DictionaryEntry dt in ex.Data)
                {
                    if (needHeader)
                    {
                        buf.AppendLine();
                        buf.AppendLine("-------Extra Information-------");
                        needHeader = false;
                    }
                    buf.AppendFormat("{0}  :  {1}", dt.Key, dt.Value);
                    buf.AppendLine();
                    if( dt.Key.Equals("IsTerminatePermission"))
                    {
                        IsTerminatePermission = Convert.ToBoolean(dt.Value);
                    }
                }
                if ( needHeader == false )
                {
                    buf.AppendLine("-----End Extra Information-----");
                }
            }
            buf.AppendLine(ex.StackTrace);
            buf.AppendLine();

            // InnerException�����݂���ꍇ�����o��
            Exception _ex = ex.InnerException;
            int nesting = 0;
            while (_ex != null)
            {
                buf.AppendFormat("-----InnerException[{0}]-----" + Environment.NewLine, nesting);
                buf.AppendLine();
                buf.AppendFormat("��O {0}: {1}", _ex.GetType().FullName, _ex.Message);
                buf.AppendLine();
                if (_ex.Data != null)
                {
                    bool needHeader = true;

                    foreach (System.Collections.DictionaryEntry dt in _ex.Data)
                    {
                        if (needHeader)
                        {
                            buf.AppendLine();
                            buf.AppendLine("-------Extra Information-------");
                            needHeader = false;
                        }
                        buf.AppendFormat("{0}  :  {1}", dt.Key, dt.Value);
                        if (dt.Key.Equals("IsTerminatePermission") )
                        {
                            IsTerminatePermission = Convert.ToBoolean(dt.Value);
                        }
                    }
                    if (!needHeader)
                    {
                        buf.AppendLine("-----End Extra Information-----");
                    }
                }
                buf.AppendLine(_ex.StackTrace);
                buf.AppendLine();
                nesting += 1;
                _ex = _ex.InnerException;
            }
            buffer = buf.ToString();
            return buffer;
        }

        public static void OutputException(Exception ex)
        {
            lock (LockObj){
                bool IsTerminatePermission = true;

                DialogResult diagres = MessageBox.Show(
                    String.Format("��O�G���[���������܂����B{0}�G���[���O��ۑ����A�J���܂���{0}{0}�͂��@�@�@�E�E�E���O��ۑ����ĊJ���܂��B{0}�������@�@�E�E�E���O��ۑ����܂����J���܂���B{0}�L�����Z���E�E�E���O��ۑ������I�����܂��B", Environment.NewLine)
                    , "��O�G���[����", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error
                    );
                if (diagres == DialogResult.Cancel)
                {
                    // �I�������O�̏�Ԃ�ۑ�����n���h�����R�[������
                    //Program.ExitHandler();    // �ǂ��Ŕ��������G���[��������Ȃ����A������E�E�E
                    Application.Exit();
                    return;
                }


                DateTime now = DateTime.Now;
                string fileName = String.Format("TSLoginManager-{0:0000}{1:00}{2:00}-{3:00}{4:00}{5:00}.log", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName))
                {
                    WindowsIdentity ident = WindowsIdentity.GetCurrent();
                    WindowsPrincipal princ = new WindowsPrincipal(ident);

                    writer.WriteLine("**** �G���[ ���O: {0} ****", DateTime.Now.ToString());
                    writer.WriteLine("���̃t�@�C���̓��e�� anon.jp@gmail.com �܂ő����Ă���������Ə�����܂��B");
                    writer.WriteLine("�܂��̓T�|�[�g�t�H�[����( http://trickster.anoncom.net/forum/viewforum.php?f=4 )�܂ł��m�点���������B");
                    // ���������o��
                    writer.WriteLine("Administrator�����F" + princ.IsInRole(WindowsBuiltInRole.Administrator).ToString());
                    writer.WriteLine("Users�����F" + princ.IsInRole(WindowsBuiltInRole.User).ToString());
                    writer.WriteLine();
                    // OSVersion,AppVersion�����o��
                    writer.WriteLine("�����:");
                    writer.WriteLine("   �I�y���[�e�B���O �V�X�e��: {0}", Environment.OSVersion.VersionString);
                    writer.WriteLine("   ���ʌ��ꃉ���^�C�� : {0}", Environment.Version.ToString());
                    writer.WriteLine("   TSLoginManager�̃o�[�W���� : {0}", Application.ProductVersion.ToString());

                    string buffer = null;
                    writer.Write(OutputException(ex, buffer, ref IsTerminatePermission));
                    writer.Flush();
                }


                switch (diagres)
                {
                    case DialogResult.Yes:
                        System.Diagnostics.Process.Start(fileName);
                        //return false;
                        break;
                    case DialogResult.No:
                        //return false;
                        break;
                    //case DialogResult.Cancel:
                        //return IsTerminatePermission;
                }
            }
        }
    }
}
