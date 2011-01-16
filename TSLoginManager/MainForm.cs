using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.Threading;
using TricksterTools.CommonXmlStructure;
using TricksterTools.API.Controller;
using TricksterTools.Library;
using TricksterTools.Library.LoginManager;
using TricksterTools.API.DataStructure;
using TricksterTools.Plugins;
using TricksterTools.Debug;



namespace TSLoginManager
{
    public partial class MainForm : Form, IPluginHost
    {
        //static Logger logger = new Logger();

        public Thread watchThread;
        public WatchController WController;
        private WatchController.stats prevStatus = WatchController.stats.Shutdown;
        private WatchController.status prevProcessStatus = WatchController.status.End;

        

        public MainForm()
        {
            Program.splash.statusUpdate("���������Ă��܂�...", 10);
            InitializeComponent();
            // �R���g���[���{�b�N�X�́m����n�{�^���̖�����
            IntPtr hMenu = TricksterTools.Library.Win32API.GetSystemMenu(this.Handle, 0);
            TricksterTools.Library.Win32API.RemoveMenu(hMenu, TricksterTools.Library.Win32API.SC_CLOSE, TricksterTools.Library.Win32API.MF_BYCOMMAND);

            /*
            // .NET 3.5�ȏ�AWindows Vista / Windows 7 �ȏ�őΉ��\�ȃR�[�h
            // �ċN���p�R�[�h�̓o�^
            ApplicationRestartRecoveryManager.RegisterForApplicationRestart(
                    new RestartSettings("/restart",
                    RestartRestrictions.NotOnReboot | RestartRestrictions.NotOnPatch));

            // �C���p���\�b�h�̓o�^
            RecoveryData data = new RecoveryData(new RecoveryCallback(RecoveryProcedure), null);
            RecoverySettings settings = new RecoverySettings(data, 0);
            ApplicationRestartRecoveryManager.RegisterForApplicationRecovery(settings);

            // �N�����̍ċN�����ǂ����̔��f
            // �ċN�����Ƀf�[�^���C������R�[�h�̍쐬
            if (System.Environment.GetCommandLineArgs().Length > 1 &&
                System.Environment.GetCommandLineArgs()[1] == "/restart")
            {
                if (File.Exists(RecoveryFile))
                {
                    textBox1.Text = File.ReadAllText(RecoveryFile);
                }
            }
            */


            Program.splash.statusUpdate("�ݒ��ǂݍ��ݒ�...", 15);

            // �N�����ɕK�v�Ȑݒ��ǂݍ��݂܂��B
            Program.LoadHandler();

            // start detect hangup timer
            /*
            if (!SettingController.HungUp.enable)
            {
                this.WController = new WatchController();
                this.WController.timer = new System.Threading.Timer(this.WController.timerCallback, null, 0, 1000);
                this.WController.timerCallback = new System.Threading.TimerCallback(this.WController.detectHungUp);
                this.watchThread = new Thread(new ThreadStart(this.WController.Run));
            }
            */

            // �ݒ肩��A�C�R�����擾����B
            Program.splash.statusUpdate("���\�[�X�ǂݍ��ݒ�...", 5);
            notifyIcon.Icon = Program.loadIconResource(SettingController.Icons.resourceName);
            
            // �A�C�R���\��
            notifyIcon.Visible = true;
            Program.splash.statusUpdate("", 100);
            Thread.Sleep(50);
            Program.splash.Close();


            /// �v���O�C���t�b�N�̋N��
            PluginController.PluginHook(Program.plugins, TricksterTools.Plugins.HookPoint.Startup);

        }

        #region �t�H�[����CreateParams�v���p�e�B���I�[�o�[���C�h����
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TOOLWINDOW = 0x80;
                const long WS_POPUP = 0x80000000L;
                const int WS_VISIBLE = 0x10000000;
                const int WS_SYSMENU = 0x80000;
                const int WS_MAXIMIZEBOX = 0x10000;
             
                CreateParams cp = base.CreateParams;

                //cp.ClassName = "TricksterLoginManager";
                cp.ExStyle = WS_EX_TOOLWINDOW;
                cp.Style = unchecked((int)WS_POPUP) |
                    WS_VISIBLE | WS_SYSMENU | WS_MAXIMIZEBOX;
                cp.Width = 0;
                cp.Height = 0;

                return cp;
            }
        }
        #endregion
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            notifyIcon.Visible = true;
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // �I�������̃L�����Z��
            this.WindowState = FormWindowState.Minimized; // �t�H�[���̔�\��
        }
        
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddForm addFrm = new AddForm();
            addFrm.Show(); // �t�H�[���̕\��
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            TricksterTools.Library.Win32API.SetForegroundWindow(new System.Runtime.InteropServices.HandleRef(this, this.Handle));

            Point mp = Control.MousePosition;

            /** �n�b�V���e�[�u������ID��ǂݍ��� **/
            // �ۑ����̃t�@�C����
            //string fileName = "config.txt";
            //System.Collections.Hashtable accounts = Program.loadConfig(fileName);
            System.Collections.SortedList links = SettingController.Links;

            if (e.Button == MouseButtons.Left)
            {
                #region ���N���b�N��

                this.contextMenuStrip_Left.Items.Clear();

                if (AccountController.AccountData.Count == 0)
                {
                    this.contextMenuStrip_Left.Items.Add("No Registered ID");
                    this.contextMenuStrip_Left.Enabled = false;
                }
                else
                {
                    this.contextMenuStrip_Left.Enabled = true;
                    IEnumerator ienum = AccountController.AccountData.GetEnumerator();
                    while(ienum.MoveNext())
                    {
                        Accounts.AccountProperties acprop = (Accounts.AccountProperties)ienum.Current;
                        string ID = acprop.ID;
                        string Password = acprop.Password;

                        ToolStripMenuItem items = new ToolStripMenuItem();
                        items.Text = ID;

                        if (acprop.Site == Accounts.AccountProperties.LoginSite.Official)
                        {
                            items.Image = (Image)Properties.Resources.official.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.HanGame)
                        {
                            items.Image = (Image)Properties.Resources.hangame.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.AtGames)
                        {
                            items.Image = (Image)Properties.Resources.atgames.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.Gamers1)
                        {
                            //items.Image = (Image)Properties.Resources.lievo.ToBitmap();
                            items.Image = (Image)Properties.Resources.gamers1.ToBitmap();
                        }
                        else
                        {
                            items.Image = (Image)Properties.Resources.official.ToBitmap();
                        }


                        items.Click += delegate
                        {
                            if (!Common.isInstalled()) // �N���C�A���g���C���X�g�[������Ă��邩
                            {
                                MessageBox.Show("�g���b�N�X�^�[ �N���C�A���g���C���X�g�[������Ă��܂���B" + Environment.NewLine + 
                                    "�N���C�A���g�v���O�������C���X�g�[�����čēx���s���Ă��������B", "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //MessageBox.Show("TRICKSTER client program is not installed in this computer." + Environment.NewLine + 
                                //    "Please try to install and run.", "TSLoginManager");
                            }
                            else
                            {
                                SimpleLogger.Write("auto login start...");
                                //LoginController.startGame(ID, Password);
                                if (acprop.Site == Accounts.AccountProperties.LoginSite.Official)
                                {
                                    SimpleLogger.WriteLine("Official");
                                    OfficialLoginController.setPluginHost(this);
                                    OfficialLoginController.startGame(ID, Password);
                                }
                                else if (acprop.Site == Accounts.AccountProperties.LoginSite.HanGame)
                                {
                                    SimpleLogger.WriteLine("HanGame");
                                    HanGameLoginController.setPluginHost(this);
                                    HanGameLoginController.startGame(ID, Password);
                                }
                                else if (acprop.Site == Accounts.AccountProperties.LoginSite.AtGames)
                                {
                                    SimpleLogger.WriteLine("AtGames");
                                    AtGamesLoginController.setPluginHost(this);
                                    AtGamesLoginController.startGame(ID, Password);
                                }
                                else if (acprop.Site == Accounts.AccountProperties.LoginSite.Gamers1)
                                {
                                    SimpleLogger.WriteLine("Lievo");
                                    //LievoLoginController.setPluginHost(this);
                                    //LievoLoginController.startGame(ID, Password);
                                    GamersOneLoginController.setPluginHost(this);
                                    GamersOneLoginController.startGame(ID, Password);
                                }
                                else
                                {
                                    OfficialLoginController.setPluginHost(this);
                                    OfficialLoginController.startGame(ID, Password);
                                }
                            }
                        };
                        this.contextMenuStrip_Left.Items.Add(items);
                    }
                }

                //this.notifyIcon.ContextMenuStrip = this.contextMenuStrip_Left;
                //this.contextMenuStrip_Left.Show(mp.X - contextMenuStrip_Left.Width, mp.Y);
                this.contextMenuStrip_Left.Show(this, PointToClient(Cursor.Position));
            #endregion
            }
            else if (e.Button == MouseButtons.Right)
            {
                #region �E�N���b�N��

                //this.ToolStripMenuItem_Right_Edit.DropDownItems.Clear();
                //this.ToolStripMenuItem_Right_Edit.DisplayStyle = ToolStripItemDisplayStyle.Text;
                this.ToolStripMenuItem_Right_Delete.DropDownItems.Clear();
                this.ToolStripMenuItem_Right_Delete.DisplayStyle = ToolStripItemDisplayStyle.Text;
                this.ToolStripMenuItem_Right_Plugins.DropDownItems.Clear();
                this.ToolStripMenuItem_Right_Plugins.DisplayStyle = ToolStripItemDisplayStyle.Text;
                this.ToolStripMenuItem_Right_Links.DropDownItems.Clear();
                this.ToolStripMenuItem_Right_Links.DisplayStyle = ToolStripItemDisplayStyle.Text;
                this.ToolStripMenuItem_Right_Tool_PluginInfo.DropDownItems.Clear();
                this.ToolStripMenuItem_Right_Tool_PluginInfo.DisplayStyle = ToolStripItemDisplayStyle.Text;

                #region �A�J�E���g�폜�p�ꗗ
                if (AccountController.AccountData.Count == 0)
                {
                    //this.ToolStripMenuItem_Right_Edit.DropDownItems.Add("No Registered ID");
                    //this.ToolStripMenuItem_Right_Edit.Enabled = false;
                    //this.ToolStripMenuItem_Right_Delete.DropDownItems.Add("No Registered ID");
                    this.ToolStripMenuItem_Right_Delete.Enabled = false;
                }
                else
                {
                    //this.ToolStripMenuItem_Right_Edit.Enabled = true;
                    //this.ToolStripMenuItem_Right_Edit.DropDown.Enabled = true;
                    this.ToolStripMenuItem_Right_Delete.Enabled = true;
                    this.ToolStripMenuItem_Right_Delete.DropDown.Enabled = true;

                    this.contextMenuStrip_Left.Enabled = true;
                    IEnumerator ienum = AccountController.AccountData.GetEnumerator();
                    while (ienum.MoveNext())
                    {
                        //ToolStripMenuItem edit_items = new ToolStripMenuItem();
                        ToolStripMenuItem del_items = new ToolStripMenuItem();
                        Accounts.AccountProperties acprop = (Accounts.AccountProperties)ienum.Current;
                        string ID = acprop.ID;
                        //string Password = accounts[ID].ToString();

                        //edit_items.Text = ID;
                        del_items.Text = ID;
                        /*
                        edit_items.Click += delegate
                        {
                            EditForm edtFrm = new EditForm(ID);
                            edtFrm.Show(); // �t�H�[���̕\��
                        };
                        this.ToolStripMenuItem_Right_Edit.DropDownItems.Add(edit_items);
                        */
                        if (acprop.Site == Accounts.AccountProperties.LoginSite.Official)
                        {
                            del_items.Image = (Image)Properties.Resources.official.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.HanGame)
                        {
                            del_items.Image = (Image)Properties.Resources.hangame.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.AtGames)
                        {
                            del_items.Image = (Image)Properties.Resources.atgames.ToBitmap();
                        }
                        else if (acprop.Site == Accounts.AccountProperties.LoginSite.Gamers1)
                        {
                            del_items.Image = (Image)Properties.Resources.lievo.ToBitmap();
                        }
                        else
                        {
                            del_items.Image = (Image)Properties.Resources.official.ToBitmap();
                        }

                        del_items.Click += delegate
                        {
                            DialogResult diagres = MessageBox.Show("ID: \" " + ID + " \" ���폜���悤�Ƃ��Ă��܂��B" + Environment.NewLine + 
                                "��낵���ł����H", "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            if(diagres == DialogResult.No){
                                return;
                            }
                            ProgramController.AController.delete(ID, acprop.Site);
                            /*
                             * ���̎��_�ł��A�J�E���g�����t�@�C���ɕۑ�����
                             */
                            string filename = Environment.CurrentDirectory + @"\accounts.dat";
                            if (AccountController.isLoadedAccount && (AccountController.AccountData.Count > 0))
                            {
                                ProgramController.AController.saveAccounts(filename, AccountController.MasterKey);
                            }

                        };
                        this.ToolStripMenuItem_Right_Delete.DropDownItems.Add(del_items);
                    }
                }
                #endregion
                #region �v���O�C���ꗗ
                if (Program.plugins.Length == 0)
                {
                    //this.ToolStripMenuItem_Right_Plugins.DropDownItems.Add("No Plugin");
                    this.ToolStripMenuItem_Right_Plugins.Enabled = false;
                }
                else
                {
                    this.ToolStripMenuItem_Right_Plugins.Enabled = true;
                    this.ToolStripMenuItem_Right_Plugins.DropDown.Enabled = true;

                    foreach (TricksterTools.Plugins.IPlugin plugin in Program.plugins)
                    {
                        ToolStripMenuItem_Plugin PluginItems = new ToolStripMenuItem_Plugin();
                        PluginItems.setPluginName(plugin.GetType().Name, plugin.Name);
                        PluginItems.Click += delegate
                        {
                            PluginController.PluginRun(Program.plugins, PluginItems.ClassName);
                        };
                        this.ToolStripMenuItem_Right_Plugins.DropDownItems.Add(PluginItems);
                    }
                }
                #endregion
                #region �v���O�C�����
                if (Program.plugins.Length == 0)
                {
                    //this.ToolStripMenuItem_Right_Tool_PluginInfo.DropDownItems.Add("No Plugin");
                    this.ToolStripMenuItem_Right_Tool_PluginInfo.Enabled = false;
                }
                else
                {
                    this.ToolStripMenuItem_Right_Tool_PluginInfo.Enabled = true;
                    this.ToolStripMenuItem_Right_Tool_PluginInfo.DropDown.Enabled = true;

                    foreach (TricksterTools.Plugins.IPlugin plugin in Program.plugins)
                    {
                        ToolStripMenuItem_Plugin PluginInfoItems = new ToolStripMenuItem_Plugin();
                        PluginInfoItems.setPluginName(plugin.GetType().Name, plugin.Name);
                        PluginInfoItems.Click += delegate
                        {
                            //PluginInfoForm pluginInfoForm = new PluginInfoForm(plugin);
                            PluginInfoForm pluginInfoForm = new PluginInfoForm(PluginController.getPluginInfo(Program.plugins, PluginInfoItems.ClassName));
                            pluginInfoForm.Show();
                            pluginInfoForm.Owner = this;
                        };
                        this.ToolStripMenuItem_Right_Tool_PluginInfo.DropDownItems.Add(PluginInfoItems);
                    }
                }
                #endregion
                #region �����N�ꗗ

                if (links.ContainsKey("__TSLM_NULL__"))
                {
                    this.ToolStripMenuItem_Right_Links.DropDownItems.Add("No Links");
                    this.ToolStripMenuItem_Right_Links.Enabled = false;
                }
                else
                {
                    this.ToolStripMenuItem_Right_Links.Enabled = true;
                    this.ToolStripMenuItem_Right_Links.DropDown.Enabled = true;

                    foreach (string key in links.Keys)
                    {
                        ToolStripMenuItem link_items = new ToolStripMenuItem();
                        string SiteName = key;
                        link_items.Text = SiteName;
                        link_items.Click += delegate
                        {
                            System.Diagnostics.Process.Start(links[SiteName].ToString());
                        };
                        this.ToolStripMenuItem_Right_Links.DropDownItems.Add(link_items);
                    }
                }
                #endregion
                #region �v���O�C�����
                //this.notifyIcon.ContextMenuStrip = this.contextMenuStrip_Right;
                this.Activate();
                this.contextMenuStrip_Right.Show(mp.X, mp.Y + 275);
                //this.contextMenuStrip_Right.Show(this, Cursor.Position.X - this.Location.X, Cursor.Position.X - this.Location.X);
                //this.contextMenuStrip_Right.Show(PointToScreen(Cursor.Position));

                ToolStripDropDownMenu tsddm_r_del = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Delete.DropDown;
                tsddm_r_del.ShowImageMargin = true;
                ToolStripDropDownMenu tsddm_r_link = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Links.DropDown;
                tsddm_r_link.ShowImageMargin = false;
                ToolStripDropDownMenu tsddm_r_plugin = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Plugins.DropDown;
                tsddm_r_plugin.ShowImageMargin = false;
                ToolStripDropDownMenu tsddm_r_tool = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Tool.DropDown;
                tsddm_r_tool.ShowImageMargin = false;
                ToolStripDropDownMenu tsddm_r_tool_setting = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Tool_Settings.DropDown;
                tsddm_r_tool_setting.ShowImageMargin = false;
                ToolStripDropDownMenu tsddm_r_tool_plguininfo = (ToolStripDropDownMenu)this.ToolStripMenuItem_Right_Tool_PluginInfo.DropDown;
                tsddm_r_tool_plguininfo.ShowImageMargin = false;
                #endregion
                #endregion
            }
        }

        private void ToolStripMenuItem_Right_Exit_Click(object sender, EventArgs e)
        {
            /**
             * �I������ 
             */
            SimpleLogger.WriteLine("TSLoginManager shutdown...");
            Program.ExitHandler();
            this.notifyIcon.Visible = false; // �A�C�R�����g���C�����菜��
            this.notifyIcon.Dispose();
            SimpleLogger.WriteLine("done.");
            SimpleLogger.Close();
            Application.Exit(); // �A�v���P�[�V�����̏I��
        }

        private void ToolStripMenuItem_Right_Add_Click(object sender, EventArgs e)
        {
            SimpleLogger.WriteLine("show account add window.");
            AddForm addFrm = new AddForm();
            addFrm.Show(); // �t�H�[���̕\��
            //addFrm.Activate(); // �t�H�[�����A�N�e�B�u�ɂ���
            //addFrm.Dispose();
        }

        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            SimpleLogger.WriteLine("show about window.");
            AboutForm aboutFrm = new AboutForm();
            aboutFrm.ShowDialog();
            //aboutFrm.Dispose();
        }
        /*
        private void ToolStripMenuItem_Right_Edit_ID_Click(object sender, EventArgs e)
        {
            string ID = this.ToolStripMenuItem_Right_Edit_ID.Text;
            EditForm edtFrm = new EditForm(ID);
            edtFrm.Show(); // �t�H�[���̕\��
        }
        */
        

        private void ToolStripMenuItem_Right_Tool_UpdateCheckGame_Click(object sender, EventArgs e)
        {

            string currentVer = Common.getClientVersion();
            string updateVer = Common.getServerVersion();

            NotifyIcon notiftIconClone = Program.frm.notifyIcon;

            if (Common.isNeedUpdate(currentVer, updateVer))
            {
                /*
                string strMsg = "�A�b�v�f�[�g������܂��B" + Environment.NewLine + 
                        "     " + currentVer + " �� " + updateVer + Environment.NewLine +  
                        "�����`���[���N�����܂����H" + Environment.NewLine + 
                        "����������Q�[���̋N���͂ł��܂���B";
                DialogResult msg = MessageBox.Show(strMsg, "TSLoginManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    if (!Common.isInstalled())
                    {
                        MessageBox.Show("�g���b�N�X�^�[�N���C�A���g���C���X�g�[������Ă��܂���B", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                    psi.FileName = Common.getInstallPath();
                    psi.RedirectStandardInput = false;
                    psi.RedirectStandardOutput = false;
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = false;
                    // ����
                    //psi.Arguments = "";

                    SimpleLogger.WriteLine("start Launcher and launch Update.");

                    // �N��
                    System.Diagnostics.Process.Start(psi);
                }
                */

                string strMsg = "�A�b�v�f�[�g������܂��B" + Environment.NewLine +
                        "     " + currentVer + " �� " + updateVer + Environment.NewLine + 
                        "�����`���[���N������ɂ͂������N���b�N���Ă��������B";
                Program.frm.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                Program.frm.notifyIcon.BalloonTipTitle = "�g���b�N�X�^�[�A�b�v�f�[�g�ʒm";
                Program.frm.notifyIcon.BalloonTipText = strMsg;
                Program.frm.notifyIcon.ShowBalloonTip(30000);
                Program.frm.notifyIcon.BalloonTipClicked += delegate
                {
                    if (!Common.isInstalled())
                    {
                        MessageBox.Show("�g���b�N�X�^�[�N���C�A���g���C���X�g�[������Ă��܂���B", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.frm.notifyIcon = notiftIconClone;
                        return;
                    }
                    Common.updateGame();

                    /// �v���O�C���ɂ��t�b�N�N���|�C���g
                    PluginController.PluginHook(Program.plugins, HookPoint.UpdatedGame);

                    Program.frm.notifyIcon = notiftIconClone;
                };
                Program.frm.notifyIcon.BalloonTipClicked -= delegate
                {
                    if (!Common.isInstalled())
                    {
                        MessageBox.Show("�g���b�N�X�^�[�N���C�A���g���C���X�g�[������Ă��܂���B", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.frm.notifyIcon = notiftIconClone;
                        return;
                    }
                    Common.updateGame();

                    /// �v���O�C���ɂ��t�b�N�N���|�C���g
                    PluginController.PluginHook(Program.plugins, HookPoint.UpdatedGame);

                    Program.frm.notifyIcon = notiftIconClone;
                };
            }
            else
            {
                SimpleLogger.WriteLine("no update.");
                /*
                MessageBox.Show("���݃A�b�v�f�[�g�͂���܂���B"
                    //+ "ver. " + currentVer
                    , "TSLoginManager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                */
                Program.frm.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                Program.frm.notifyIcon.BalloonTipTitle = "�g���b�N�X�^�[�A�b�v�f�[�g�ʒm";
                Program.frm.notifyIcon.BalloonTipText = "���݃A�b�v�f�[�g�͂���܂���B";
                Program.frm.notifyIcon.ShowBalloonTip(10000);
                Program.frm.notifyIcon.BalloonTipClicked += delegate
                {
                    Program.frm.notifyIcon = notiftIconClone;
                    return;
                };
            }
        }

        private void ToolStripMenuItem_Right_Tool_CheckNewVersion_Click(object sender, EventArgs e)
        {
            Program.checkMyUpdate(true);
        }


        private void ToolStripMenuItem_Right_Tool_Settings_Click(object sender, EventArgs e)
        {
            SimpleLogger.WriteLine("show setting window...");
            SettingForm setForm = new SettingForm();
            setForm.Show(this);
        }
        

        private void timer_Tick(object sender, EventArgs e)
        {
            if (SettingController.HungUp.enable == false)
            {
                // �t���[�Y���o������
                return;
            }
            if (this.WController == null)
            {
                this.WController = new WatchController();
            }
            
            // �N���C�A���g�̏�Ԃ��ύX���ꂽ�ꍇ
            if (this.WController.gameStats != this.prevStatus)
            {
                SimpleLogger.WriteLine("Trickster client: " + this.WController.gameStats.ToString());
                this.prevStatus = this.WController.gameStats;
            }
            if (this.WController.ProcessStatus != this.prevProcessStatus)
            {
                SimpleLogger.WriteLine("Trickster process: " + this.WController.ProcessStatus.ToString());
                this.prevProcessStatus = this.WController.ProcessStatus;
            }

            // �t���[�Y����
            int msec = SettingController.HungUp.sec * 1000;   // �P�ʃ~���b
            this.WController.setTimer(timer, msec);
            notifyIcon.Text = "TSLoginManager (FREEZE MODE)";
            Thread worker = new Thread( this.WController.watcher );
            worker.IsBackground = true;
            worker.Start();
            notifyIcon.Text = "TSLoginManager";
        }

        /// <summary>
        /// ���C���t�H�[��
        /// </summary>
        public Form HostForm
        {
            get
            {
                return (Form)this;
            }
        }

        /// <summary>
        /// �v���O�C���̃��b�Z�[�W��\������
        /// </summary>
        /// <param name="plugin">���\�b�h���Ăяo���v���O�C��</param>
        /// <param name="msg">�\�����郁�b�Z�[�W</param>
        public void ShowMessage(IPlugin plugin, string msg)
        {
            MessageBox.Show(msg);
        }


        public class ToolStripMenuItem_Plugin : ToolStripMenuItem
        {
            private string PluginClassName = "";
            private string PluginDisplayName = "";

            /// <summary>
            /// �R���X�g���N�^
            /// </summary>
            /// <param name="ClassName">�v���O�C���N���X��</param>
            /// <param name="DisplayName">�v���O�C���\����</param>
            public void setPluginName(string ClassName, string DisplayName)
            {
                this.PluginClassName = ClassName;
                this.PluginDisplayName = DisplayName;
            }

            // ���ۂ̒l
            public string ClassName
            {
                get
                {
                    return this.PluginClassName;
                }
            }

            // �\������
            public override string Text
            {
                get
                {
                    return this.PluginDisplayName;
                }
                set
                {
                    this.PluginDisplayName = value;
                }
            }

            // �I�[�o�[���C�h�������\�b�h
            public override string ToString()
            {
                return this.PluginDisplayName;
            }
        }
        
        /// <summary>
        /// �t�@�C�i���C�U
        /// </summary>
        ~MainForm()
        {
            Program.ExitHandler();
            SimpleLogger.Close();
        }
    }
}