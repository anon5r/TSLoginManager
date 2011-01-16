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
            DialogResult DiagRes = MessageBox.Show("ゲームを強制終了しますか？", "緊急停止", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
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
                        MessageBox.Show("プロセスの取得に失敗しました。" + Environment.NewLine
                            //+ "現在のTSLoginManager以外から起動したゲームはここから終了できません。"
                            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch(Exception ex)
                    {
                        SimpleLogger.WriteLine(ex.Message);
                        //MessageBox.Show("例外エラー", "Exceptional Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("ゲームの終了処理に失敗しました。", "緊急停止エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (System.SystemException se)
                    {
                        MessageBox.Show(se.Message, "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("ゲームの終了処理に失敗しました。", "緊急停止エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("ゲームが起動していません。", "緊急停止エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EmergencyStopForm_Load(object sender, EventArgs e)
        {
            int h, w;
            //ディスプレイの作業領域の高さ
            h = System.Windows.Forms.Screen.GetWorkingArea(this).Height;
            //ディスプレイの作業領域の幅
            w = System.Windows.Forms.Screen.GetWorkingArea(this).Width;


            this.DesktopLocation = new Point(w - 122, h - 129);
        }
    }
}