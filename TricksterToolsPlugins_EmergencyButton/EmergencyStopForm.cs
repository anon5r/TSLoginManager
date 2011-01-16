using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TricksterTools.Debug;

namespace TricksterTools.Plugins.EmergencyButton
{
    public partial class EmergencyStopForm : Form
    {
        public EmergencyStopForm()
        {
            InitializeComponent();
        }

        private void btnRunStop_Click(object sender, EventArgs e)
        {
            DialogResult DiagRes = MessageBox.Show("�Q�[���������I�����܂����H", "�ً}��~", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (DialogResult.Yes == DiagRes)
            {
                int hWnd = 0;
                // find "classTrickster"
                hWnd = Win32API.FindWindow("xmflrtmxj", null);
                if (hWnd > 0)
                {
                    int ProcessId = 0;
                    /*
                    if (TricksterTools.Library.LoginManager.ProgramController.TrickProcess.Id < 1)
                    {
                        ProcessId = (int)TricksterTools.Library.LoginManager.ProgramController.getProcessId((IntPtr)hWnd);
                    }
                    else
                    {
                        ProcessId = TricksterTools.Library.LoginManager.ProgramController.TrickProcess.Id;
                    }
                    */
                    try
                    {
                        ProcessId = TricksterTools.Library.LoginManager.WatchController.TrickProcess.Id;
                    }
                    catch(NullReferenceException nre)
                    {
                        SimpleLogger.WriteLine(nre.Message);
                        MessageBox.Show("�v���Z�X�̎擾�Ɏ��s���܂����B" + Environment.NewLine
                            //+ "���݂�TSLoginManager�ȊO����N�������Q�[���͂�������I���ł��܂���B"
                            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch(Exception ex)
                    {
                        SimpleLogger.WriteLine(ex.Message);
                        //MessageBox.Show("��O�G���[", "Exceptional Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw ex;
                    }
                    SimpleLogger.WriteLine("Trickster ProcessID: " + ProcessId);

                    try
                    {
                        TricksterTools.Library.LoginManager.WatchController.TrickProcess.Kill();
                        //prc.Kill();
                    }
                    catch (System.InvalidOperationException ioe)
                    {
                        SimpleLogger.WriteLine(ioe.Message);
                        MessageBox.Show("�Q�[���̏I�������Ɏ��s���܂����B", "�ً}��~�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (System.SystemException se)
                    {
                        MessageBox.Show(se.Message, "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("�Q�[���̏I�������Ɏ��s���܂����B", "�ً}��~�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("�Q�[�����N�����Ă��܂���B", "�ً}��~�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EmergencyStopForm_Load(object sender, EventArgs e)
        {
            int h, w;
            //�f�B�X�v���C�̍�Ɨ̈�̍���
            h = System.Windows.Forms.Screen.GetWorkingArea(this).Height;
            //�f�B�X�v���C�̍�Ɨ̈�̕�
            w = System.Windows.Forms.Screen.GetWorkingArea(this).Width;


            this.DesktopLocation = new Point(w - 122, h - 129);
        }
    }
}