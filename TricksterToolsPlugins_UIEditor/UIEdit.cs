using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using TricksterTools.Debug;

namespace TricksterTools.Plugins.UIEditor
{

    public class UIEdit
    {


        /// <summary>
        /// �C���X�g�[����p�X���擾
        /// </summary>
        /// <param name="void"></param>
        /// <returns>�C���X�g�[����p�X</returns>
        public static string getInstallPath()
        {
            string rKeyName;
            // ���삷�郌�W�X�g���E�L�[�̖��O
            if (isx64())
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
                Microsoft.Win32.RegistryKey rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(rKeyName);

                // ���W�X�g���̒l���擾
                string location = (string)rKey.GetValue(rGetValueName);

                // �J�������W�X�g���E�L�[�����
                rKey.Close();

                // �擾�������W�X�g���̒lreturn
                return location.Substring(0, (location.Length - @"\Splash.exe".Length));
            }
            catch (NullReferenceException)
            {
                SimpleLogger.WriteLine("failed to get client installed path.");
                // ���W�X�g���E�L�[�܂��͒l�����݂��Ȃ�
                return @"C:\Games\Trickster";
            }
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

        public static void generateChatUI(int width, int height)
        {
            // �t�@�C���p�X���w��
            String filePath = getInstallPath() + @"\data\UI_nori\ChatUI.ucf";

            /*
             * ; �\���g�T�C�Y
             * iViewDimX	= iStatViewDimX + 2
             * iViewDimY	= 22�i�Œ�l/�s�ρj
             * 
             * ; �ʏ탂�[�h���̕������͘g
             * iEditBoxDimX	= iStatViewDimX + 74
             * iEditBoxDimY	= 18�i�Œ�l/�s�ρj
             * iEditBoxCoordX	= 58�i�Œ�l/�s��/���ӂ�0�Ƃ��Ă݂����̉��̈ʒu�j
             * iEditBoxCoordY	= 3�i�Œ�l/�s��/��ӂ�0�Ƃ��Ă݂����̍����̈ʒu�j
             * 
             * ; whisper���[�h���̕������͘g
             * iEditBoxMinDimX	= iStatViewDimX - iEditBoxMinCoordX - 16
             * iEditBoxMinDimY	= 18�i�Œ�l/�s�ρj
             * iEditBoxMinCoordX	= ((iStatViewDimX - (iComboBoxDimX + iComboBoxCoordX + 2)) - iStatViewDimX) * -1�i/���ӂ�0�Ƃ��Ă݂����̉��̈ʒu/�E���̓_�j
             * iEditBoxMinCoordY	= 3�i�Œ�l/�s��/��ӂ�0�Ƃ��Ă݂����̍����̈ʒu/�E���̓_�j
             * 
             * ; whisper���[�h���̃R���{�{�b�N�X�T�C�Y
             * iComboBoxDimX	= �R���{�{�b�N�X�T�C�Y�̉����i�Œ�116�j
             * iComboBoxDimY	= 18�i�Œ�l/�s�ρj
             * iComboBoxCoordX	= 58�i�Œ�l/�s�ρj
             * iComboBoxCoordY	= 3�i�Œ�l/�s�ρj
             * 
             * ; �G���{�^���̈ʒu
             * iEmoticonBtnCoordX	= 5�i�Œ�l/�s�ρj
             * iEmoticonBtnCoordY	= 5�i�Œ�l/�s�ρj
             * ; �����{�^���̈ʒu
             * iMemoBtnCoordX		= 22�i�Œ�l/�s�ρj
             * iMemoBtnCoordY		= 5�i�Œ�l/�s�ρj
             * ; �����{�^���̈ʒu
             * iWhisperBtnCoordX	= 40�i�Œ�l/�s�ρj
             * iWhisperBtnCoordY	= 5�i�Œ�l/�s�ρj
             * ; ����{�^���̈ʒu
             * iCloseBtnCoordX		= iStatViewDimX - 15
             * iCloseBtnCoordY		= 6�i�Œ�l/�s�ρj
             * 
             * iStatViewDimX	= �`���b�g����\���̉���
             * iStatViewDimY	= 78�i�Œ�l/�s�ρj
             * 
             * iStatViewMaxDimX	= iStatViewDimX
             * iStatViewMaxDimY	= �`���b�g����\���̍���
             * 
             * iModeButtonDimX	= 65�i�Œ�l/�s�ρj
             * iModeButtonDimY	= 18�i�Œ�l/�s�ρj
             * 
             * iDummyModeButtonDimX	= iStatViewDimX - (iModeButtonDimX * 4)
             * iDummyModeButtonDimY	= 18�i�Œ�l/�s�ρj
             */

            ChatUI chat = new ChatUI(width, height);


            StringBuilder builder = new StringBuilder();
            builder.AppendLine("iViewDimX = " + chat.iViewDimX);
            builder.AppendLine("iViewDimY = " + chat.iViewDimY);
            builder.AppendLine();
            builder.AppendLine("iEditBoxDimX = " + chat.iEditBoxDimX);
            builder.AppendLine("iEditBoxDimY = " + chat.iEditBoxDimY);
            builder.AppendLine("iEditBoxCoordX = " + chat.iEditBoxCoordX);
            builder.AppendLine("iEditBoxCoordY = " + chat.iEditBoxCoordY);
            builder.AppendLine("iEditBoxMinDimX = " + chat.iEditBoxMinDimX);
            builder.AppendLine("iEditBoxMinDimY = " + chat.iEditBoxMinDimY);
            builder.AppendLine("iEditBoxMinCoordX = " + chat.iEditBoxMinCoordX);
            builder.AppendLine("iEditBoxMinCoordY = " + chat.iEditBoxMinCoordY);
            builder.AppendLine();
            builder.AppendLine("iComboBoxDimX = " + chat.iComboBoxDimX);
            builder.AppendLine("iComboBoxDimY = " + chat.iComboBoxDimY);
            builder.AppendLine("iComboBoxCoordX = " + chat.iComboBoxCoordX);
            builder.AppendLine("iComboBoxCoordY = " + chat.iComboBoxCoordY);
            builder.AppendLine();
            builder.AppendLine("iEmoticonBtnCoordX = " + chat.iEmoticonBtnCoordX);
            builder.AppendLine("iEmoticonBtnCoordY = " + chat.iEmoticonBtnCoordY);
            builder.AppendLine("iMemoBtnCoordX  = " + chat.iMemoBtnCoordX);
            builder.AppendLine("iMemoBtnCoordY  = " + chat.iMemoBtnCoordY);
            builder.AppendLine("iWhisperBtnCoordX = " + chat.iWhisperBtnCoordX);
            builder.AppendLine("iWhisperBtnCoordY = " + chat.iWhisperBtnCoordY);
            builder.AppendLine("iCloseBtnCoordX  = " + chat.iCloseBtnCoordX);
            builder.AppendLine("iCloseBtnCoordY  = " + chat.iCloseBtnCoordY);
            builder.AppendLine();
            builder.AppendLine("iStatViewDimX = " + chat.iStatViewDimX);
            builder.AppendLine("iStatViewDimY = " + chat.iStatViewDimY);
            builder.AppendLine();
            builder.AppendLine("iStatViewMaxDimX = " + chat.iStatViewMaxDimX);
            builder.AppendLine("iStatViewMaxDimY = " + chat.iStatViewMaxDimY);
            builder.AppendLine();
            builder.AppendLine("iModeButtonDimX = " + chat.iModeButtonDimX);
            builder.AppendLine("iModeButtonDimY = " + chat.iModeButtonDimY);
            builder.AppendLine();
            builder.AppendLine("iDummyModeButtonDimX = " + chat.iDummyModeButtonDimX);
            builder.AppendLine("iDummyModeButtonDimY = " + chat.iDummyModeButtonDimY);


            SimpleLogger.WriteLine(builder.ToString());
            try
            {
                StreamWriter fst = new StreamWriter(filePath, false, Encoding.Default);
                fst.Write(builder.ToString());
                fst.Close();
            }
            catch (IOException ioe)
            {
                MessageBox.Show("�ݒ�t�@�C���̏������݂Ɏ��s���܂����B", "UIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException uae)
            {
                MessageBox.Show("�ݒ�t�@�C���ւ̏������݌���������܂���B�ݒ�͕ۑ�����Ă��܂���B", "UIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("�Z�L�����e�B�ݒ�ɂ��ݒ�t�@�C���ւ̃A�N�Z�X���s���܂���ł����B�ݒ�͕ۑ�����Ă��܂���B", "UIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static int[] loadChatUISize()
        {
            // �t�@�C���p�X���w��
            String filePath = getInstallPath() + @"\data\UI_nori\ChatUI.ucf";

            /*
             * iStatViewDimX	= �`���b�g����\���̉���
             * iStatViewMaxDimY	= �`���b�g����\���̍���
             */
            int width = 325;
            int height = 478;
            if (File.Exists(filePath))
            {
                using (StreamReader fst = new StreamReader(filePath, Encoding.Default))
                {
                    int pos;
                    while (fst.EndOfStream == false)
                    {
                        String line = fst.ReadLine();
                        if (line.Trim().Length == 0) continue;

                        try
                        {
                            pos = line.IndexOf("iStatViewDimX = ", 0);
                            if (pos >= 0)
                            {
                                width = Int16.Parse(line.Substring("iStatViewDimX = ".Length).Trim());
                                continue;
                            }
                        }
                        catch (ArgumentOutOfRangeException aore)
                        {
                        }
                        try
                        {
                            pos = line.IndexOf("iStatViewMaxDimY = ", 0);
                            if (pos >= 0)
                            {
                                height = Int16.Parse(line.Substring("iStatViewMaxDimY = ".Length).Trim());
                                continue;
                            }
                        }
                        catch (ArgumentOutOfRangeException aore)
                        {
                        }
                    }
                    fst.Close();
                }
            }
            int[] size = { width, height };
            SimpleLogger.WriteLine("size: [ {0:D}, {1:D} ]", width, height);
            return size;
        }

        /// <summary>
        /// �v���O�C���ݒ肩��ǂݍ���� ChatUI �𐶐�����
        /// </summary>
        /// <param name="config"></param>
        public static void generateChatUI(ConfigUIEditor config)
        {
            ConfigChatUI chatUI = config.chatUI;
            generateChatUI(chatUI.Width, chatUI.Height);
        }
    }
}
