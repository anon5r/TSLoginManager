using System;
using System.Collections.Generic;
using System.Text;
using TricksterTools.Plugins;
using TricksterTools.Plugins.Config;
using TricksterTools.Debug;

namespace TricksterTools.Plugins.ConfigExtra
{
    public class ConfigExtra : IPlugin
    {
        private IPluginHost _host;
        private IPluginConfig _config;
        private ConfigForm _mainForm;


        public string Name
        {
            get
            {
                return "�g���ݒ���";
            }
        }

        /// <summary>
        /// �v���O�C���̃o�[�W����
        /// </summary>
        public string Version
        {
            get
            {
                return "0.1.2.2";
            }
        }

        /// <summary>
        /// �v���O�C���̍��
        /// </summary>
        public string Author
        {
            get
            {
                return "anon";
            }
        }

        /// <summary>
        /// �v���O�C����҂̃T�C�g
        /// </summary>
        public string URL
        {
            get
            {
                return "http://trickster.anoncom.net/";
            }
        }

        /// <summary>
        /// �v���O�C���̐���
        /// </summary>
        public string Description
        {
            get
            {
                return "�g���b�N�X�^�[�̐ݒ���g���ݒ�ł���ݒ��ʂ�\�����܂��B";
            }
        }

        /// <summary>
        /// �v���O�C���̐ݒ�
        /// </summary>
        public IPluginConfig Config
        {
            get
            {
                return this._config;
            }
            set
            {
                this._config = value;
            }
        }

        /// <summary>
        /// �v���O�C���̃z�X�g
        /// </summary>
        public IPluginHost Host
        {
            get
            {
                return this._host;
            }
            set
            {
                this._host = value;
            }
        }


        public static void Main()
        {
            //�����E�B���h�E���쐬���A�\������
            ConfigForm cls = new ConfigForm();
            cls.Show();
        }

        public void Run()
        {
            //�t�H�[�����\������Ă���΁A�A�N�e�B�u�ɂ��ďI��
            if (this._mainForm != null && !this._mainForm.IsDisposed)
            {
                this._mainForm.Activate();
                return;
            }

            //�����E�B���h�E���쐬���A�\������
            this._mainForm = new ConfigForm();
            //this._mainForm.Owner = this._host.HostForm;
            this._mainForm.Show();
        }

        public void HookRun(HookPoint hp)
        {
            switch(hp){

                // �A�b�v�f�[�g��
                case HookPoint.UpdatedGame:
                    createFileOfScreenSize();
                    ConfigExtra.updateUcf();
                    break;
                // �A�v���P�[�V�����J�n��
                case HookPoint.Startup:
                // �Q�[���J�n��
                case HookPoint.RunGame:
                    break;
                // �A�v���P�[�V�����I����
                case HookPoint.Shutdown:
                    break;
                // ���̑�
                default:
                    break;
            }
            return;
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

        /// <summary>
        /// Windows �̖��O���擾���܂�
        /// </summary>
        /// <returns></returns>
        public static string getWindowsName()
        {
            OperatingSystem osInfo = Environment.OSVersion;

            string windowsName = "Undefined";  // Windows��

            switch (osInfo.Platform)
            {
                case PlatformID.Win32Windows:  // Windows 9x�n
                    if (osInfo.Version.Major == 4)
                    {
                        switch (osInfo.Version.Minor)
                        {
                            case 0:  // .NET Framework�̃T�|�[�g�Ȃ�
                                windowsName = "Windows 95";
                                break;
                            case 10:
                                windowsName = "Windows 98";
                                break;
                            case 90:
                                windowsName = "Windows Me";
                                break;
                        }
                    }
                    break;

                case PlatformID.Win32NT:  // Windows NT�n
                    if (osInfo.Version.Major == 4)
                    {
                        // .NET Framework 2.0�ȍ~�̃T�|�[�g�Ȃ�
                        windowsName = "Windows NT 4.0";
                    }
                    else if (osInfo.Version.Major == 5)
                    {
                        switch (osInfo.Version.Minor)
                        {
                            case 0:
                                windowsName = "Windows 2000";
                                break;

                            case 1:
                                windowsName = "Windows XP";
                                break;

                            case 2:
                                windowsName = "Windows Server 2003";
                                break;
                        }
                    }
                    else if (osInfo.Version.Major == 6)
                    {
                        switch (osInfo.Version.Minor)
                        {
                            case 0:
                                windowsName = "Windows Vista";
                                break;
                            case 1:
                                windowsName = "Windows 7";
                                break;
                        }
                    }
                    break;
            }
            return windowsName;
        }


        /// <summary>
        /// ��ʃT�C�Y�Ɉˑ�����t�@�C�������t�@�C������R�s�[���č쐬���܂��B
        /// </summary>
        /// <returns></returns>
        private void createFileOfScreenSize()
        {
            string rKeyName;
            int ScreenWidth = 640;
            int ScreenHeight = 480;
            string installDir = @"C:\Games\Trickster";

            // ���삷�郌�W�X�g���E�L�[�̖��O
            rKeyName = @"SOFTWARE\NTreev\Trickster_GCrest\Setup";

            // ���W�X�g���̎擾
            try
            {
                // ���W�X�g���E�L�[�̃p�X���w�肵�ă��W�X�g�����J��

                Microsoft.Win32.RegistryKey rKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(rKeyName);

                // ���W�X�g���̒l���擾
                ScreenHeight = int.Parse(rKey.GetValue("heightPixel").ToString());
                ScreenWidth = int.Parse(rKey.GetValue("widthPixel").ToString());

                // �J�������W�X�g���E�L�[�����
                rKey.Close();

                // �C���X�g�[���p�X���擾����
                rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(rKeyName.Replace(@"\Setup", ""));
                installDir = rKey.GetValue("FullPath").ToString();

                // �J�������W�X�g���E�L�[�����
                rKey.Close();

                // �擾�������W�X�g���̒lreturn
                installDir = installDir.Replace(@"\Splash.exe", "");
            }
            catch (Exception e)
            {
                SimpleLogger.WriteLine(e.Message);
            }

            string copyBasedPixel;
            if (ScreenWidth < 1024)
            {
                copyBasedPixel = "800";
            }
            else
            {
                copyBasedPixel = "1024";
            }

            if (ScreenWidth > 1024)
            {
                try
                {
                    SimpleLogger.WriteLine("File deleting...");
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".nri");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".ucf"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".ucf");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".ucf");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\map_loading_" + ScreenWidth + ".jpg"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\map_loading_" + ScreenWidth + ".jpg");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\map_loading_" + ScreenWidth + ".jpg");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\LoginUI_" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\LoginUI_" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\LoginUI_" + ScreenWidth + ".nri");
                    }

                    if (System.IO.Directory.Exists(installDir + @"\data\UI_nori\LoadingImg_" + ScreenWidth))
                    {
                        System.IO.Directory.Delete(installDir + @"\data\UI_nori\LoadingImg_" + ScreenWidth, true);
                        SimpleLogger.WriteLine("Delete directory: " + installDir + @"\data\UI_nori\LoadingImg_" + ScreenWidth);
                    }

                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\intro_img\intro_base" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\intro_img\intro_base" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\intro_img\intro_base" + ScreenWidth + ".nri");
                    }
                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\intro_img\intro_Load" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\intro_img\intro_Load" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\intro_img\intro_Load" + ScreenWidth + ".nri");
                    }

                    if (System.IO.File.Exists(installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri"))
                    {
                        System.IO.File.Delete(installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri");
                        SimpleLogger.WriteLine("Delete file: " + installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri");
                    }
                    


                    SimpleLogger.WriteLine("File copying...");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".nri");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\CharCreateUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\CharCreateUI_" + ScreenWidth + ".ucf");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\InstructionUI_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".nri");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\InstructionUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\InstructionUI_" + ScreenWidth + ".ucf");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\map_loading_" + copyBasedPixel + ".jpg", installDir + @"\data\UI_nori\map_loading_" + ScreenWidth + ".jpg", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\map_loading_" + ScreenWidth + ".jpg");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\LoginUI_" + copyBasedPixel + ".ucf", installDir + @"\data\UI_nori\LoginUI_" + ScreenWidth + ".ucf", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\LoginUI_" + ScreenWidth + ".ucf");

                    System.IO.Directory.CreateDirectory(installDir + @"\data\UI_nori\LoadingImg_" + ScreenWidth);
                    SimpleLogger.WriteLine("Create directory: " + installDir + @"\data\UI_nori\LoadingImg_" + ScreenWidth);
                    string[] files = System.IO.Directory.GetFiles(installDir + @"\data\UI_nori\LoadingImg_" + copyBasedPixel, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < files.Length; i++)
                    {
                        System.IO.File.Copy(files[i], files[i].Replace(copyBasedPixel, ScreenWidth.ToString()), true);
                        SimpleLogger.WriteLine("Copy file: " + files[i].Replace(copyBasedPixel, ScreenWidth.ToString()));
                    }

                    System.IO.File.Copy(installDir + @"\data\UI_nori\intro_img\intro_base" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\intro_img\intro_base" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\intro_img\intro_base" + ScreenWidth + ".nri");
                    System.IO.File.Copy(installDir + @"\data\UI_nori\intro_img\intro_Load" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\intro_img\intro_Load" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\intro_img\intro_Load" + ScreenWidth + ".nri");

                    System.IO.File.Copy(installDir + @"\data\UI_nori\FortuneBg_" + copyBasedPixel + ".nri", installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri", true);
                    SimpleLogger.WriteLine("Copy file: " + installDir + @"\data\UI_nori\FortuneBg_" + ScreenWidth + ".nri");
                    

                    return;
                }
                catch (Exception e)
                {
                    SimpleLogger.WriteLine("Exception: " + e.Message);
                }
            }
        }

        /// <summary>
        /// UCF�t�@�C���̐ݒ��ύX����
        /// </summary>
        public static void updateUcf()
        {
            string rKeyName;
            int ScreenWidth = 800;
            int ScreenHeight = 600;
            string installDir = @"C:\Games\Trickster";

            // ���삷�郌�W�X�g���E�L�[�̖��O
            rKeyName = @"SOFTWARE\NTreev\Trickster_GCrest\Setup";

            // ���W�X�g���̎擾
            try
            {
                // ���W�X�g���E�L�[�̃p�X���w�肵�ă��W�X�g�����J��

                Microsoft.Win32.RegistryKey rKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(rKeyName);

                // ���W�X�g���̒l���擾
                ScreenHeight = int.Parse(rKey.GetValue("heightPixel").ToString());
                ScreenWidth = int.Parse(rKey.GetValue("widthPixel").ToString());

                // �J�������W�X�g���E�L�[�����
                rKey.Close();

                // �C���X�g�[���p�X���擾����
                rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(rKeyName.Replace(@"\Setup", ""));
                installDir = rKey.GetValue("FullPath").ToString();

                // �J�������W�X�g���E�L�[�����
                rKey.Close();

                // �擾�������W�X�g���̒lreturn
                installDir = installDir.Replace(@"\Splash.exe", "");
            }
            catch (Exception e)
            {
                SimpleLogger.WriteLine(e.Message);
            }


            System.IO.TextReader reader;
            System.IO.TextWriter writer;

            // �L�����Z���N�g�E�B���h�E
            int iBaseCoordX = ((ScreenWidth - 582) / 2);
            int iBaseCoordY = (((ScreenHeight - 346) / 2) - 97) + 50;

            // �^�C���J�v�Z���E�B���h�E
            int iMenuCoordX = ((ScreenWidth - 582) / 2);
            int iMenuCoordY = iBaseCoordY + 346;


            string content, search;
            int pos = 0;


            /*
             * data\UI_nori\CharSelectUI.ucf
             * 
             * 
             */

            // ���ɐݒ肪���邩�m�F
            reader = new System.IO.StreamReader(installDir + @"\data\UI_nori\CharSelectUI.ucf");
            content = reader.ReadToEnd();
            reader.Close();
            try
            {
                //search = "iBase_CoordX_" + ScreenWidth;
                search = "iBase_CoordX_" + ScreenWidth;
                pos = content.IndexOf(search, 0);
                if (pos > 0)
                {
                    // ����ΊJ�n�ʒu����s���܂ō폜
                    content = content.Replace("iBase_CoordX_" + ScreenWidth + "\t  = " + iBaseCoordX + System.Environment.NewLine, "");
                }

                search = "iBase_CoordY_" + ScreenWidth;
                pos = content.IndexOf(search, 0);
                if (pos > 0)
                {
                    // ����ΊJ�n�ʒu����s���܂ō폜
                    content = content.Replace("iBase_CoordY_" + ScreenWidth + "\t  = " + iBaseCoordY + System.Environment.NewLine, "");
                }
            }
            catch (ArgumentOutOfRangeException aore)
            {}

            try
            {
                // �ŏ��ɉ��s��2��o�Ă���Ƃ��������
                int appendPosition = content.IndexOf(System.Environment.NewLine + System.Environment.NewLine, 0) + System.Environment.NewLine.Length;
                
                content = content.Insert(appendPosition,
                    ""
                    + "iBase_CoordX_" + ScreenWidth + "\t  = " + iBaseCoordX + System.Environment.NewLine
                    + "iBase_CoordY_" + ScreenWidth + "\t  = " + iBaseCoordY + System.Environment.NewLine
                );

                // �t�@�C���ɏ�������
                writer = new System.IO.StreamWriter(installDir + @"\data\UI_nori\CharSelectUI.ucf");
                writer.Write(content);
                writer.Close();
            }
            catch (ArgumentOutOfRangeException aore)
            { }




            /*
             * data\UI_nori\TimeCapsuleUI.ucf
             * 
             * 
             */

            // ���ɐݒ肪���邩�m�F
            reader = new System.IO.StreamReader(installDir + @"\data\UI_nori\TimeCapsuleUI.ucf");
            content = reader.ReadToEnd();
            reader.Close();
            
            try
            {
                search = "iMenu_CoordX_" + ScreenWidth;
                pos = content.IndexOf(search, 0);
                if (pos > 0)
                {
                    // ����ΊJ�n�ʒu����s���܂ō폜
                    content = content.Replace("iMenu_CoordX_" + ScreenWidth + "\t= " + iMenuCoordX + System.Environment.NewLine, "");
                }

                search = "iMenu_CoordY_" + ScreenWidth;
                pos = content.IndexOf(search, 0);
                if (pos > 0)
                {
                    // ����ΊJ�n�ʒu����s���܂ō폜
                    content = content.Replace("iMenu_CoordY_" + ScreenWidth + "\t= " + iMenuCoordY + System.Environment.NewLine, "");
                }
            }
            catch (ArgumentOutOfRangeException aore)
            { }


            try
            {
                // �ŏ��ɉ��s��2��o�Ă���Ƃ��������
                int appendPosition = content.IndexOf(System.Environment.NewLine + System.Environment.NewLine, 0) + System.Environment.NewLine.Length;

                content = content.Insert(appendPosition,
                    ""
                    + "iMenu_CoordX_" + ScreenWidth + "\t= " + iMenuCoordX + System.Environment.NewLine
                    + "iMenu_CoordY_" + ScreenWidth + "\t= " + iMenuCoordY + System.Environment.NewLine
                );

                // �t�@�C���ɏ�������
                writer = new System.IO.StreamWriter(installDir + @"\data\UI_nori\TimeCapsuleUI.ucf");
                writer.Write(content);
                writer.Close();
            }
            catch (ArgumentOutOfRangeException aore)
            { }
        }
    }
}
