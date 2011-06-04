using System;
using System.Collections.Generic;
using System.Text;
using TricksterTools.Plugins;
using System.Windows.Forms;

namespace TricksterTools.Plugins.UIEditor
{
    public class UIEditor : IPlugin
    {
        private IPluginHost _host;
        private IPluginConfig _config;

        private Form frm;

        /// <summary>
        /// �v���O�C����
        /// </summary>
        public string Name
        {
            get
            {
                return "UI�G�f�B�^";
            }
        }

        /// <summary>
        /// �v���O�C���̃o�[�W����
        /// </summary>
        public string Version
        {
            get
            {
                return "0.1.0";
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
                // �Ȃ��ꍇ�͋�̕������ return ���Ă��������B
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
                return "�Q�[�����̃E�B���h�E�̃T�C�Y��ҏW���܂��B" + Environment.NewLine + "��ʂ����ɂ́A�E�N���b�N����u����v��I�����Ă��������B";
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

        /// <summary>
        /// ���C�����s�|�C���g
        /// TSLoginManager�̉E�N���b�N���v���O�C�����i�v���O�C�����j
        /// ������s���ꂽ�ۂɌĂяo����܂��B
        /// </summary>
        public void Run()
        {
            // �t�H�[����\������ꍇ
            if (this.frm != null && !this.frm.IsDisposed)
            {
                this.frm.Activate();
                return;
            }
            this.frm = new UIEditor_ChatUI();
            this.frm.Show();
        }


        /// <summary>
        /// TSLoginManager�̃t�b�N�|�C���g�ɂ�������s
        /// TSLoginManager�̎��s�ɘA�����ăv���O�C���̎��s���s���܂��B
        /// ���Ɏ����̕K�v���Ȃ��ꍇ�͉����L�q���� return ���Ă��������B
        /// </summary>
        public void HookRun(HookPoint hp)
        {
            switch (hp)
            {
                // �Q�[�����N�����钼�O�ɌĂяo����܂�
                //case HookPoint.RunGame:
                //    break;

                // TSLoginManager���I�����钼�O�ŌĂяo����܂�
                //case HookPoint.Shutdown:
                //    break;

                // TSLoginManager�̋N������ɌĂяo����܂�
                //case HookPoint.Startup:
                //    break;

                // �g���b�N�X�^�[�̃A�b�v�f�[�g���������ہA�A�b�v�f�[�g�̒���ɌĂяo����܂��B
                case HookPoint.UpdatedGame:
                    //UIEdit.generateChatUI(_config);
                    break;
            }
            return;
        }

    }



    public static class UIEditor_Unit
    {
        private static Form frm;
        public static void Main(String[] args)
        {
            UIEditor cls = new UIEditor();
            frm = new UIEditor_ChatUI();
            cls.Run();
            //cls.HookRun(HookPoint.UpdatedGame);
        }
    }


}
