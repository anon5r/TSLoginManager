using System;
using System.Collections.Generic;
using System.Text;
using TricksterTools.Plugins;
using TricksterTools.Plugins.Config;

namespace TricksterTools.Plugins.MyBGM
{
    public class MyBGM : IPlugin
    {
        private IPluginHost _host;
        private IPluginConfig _config;
        //private BGMListManageForm frm;

        public string Name
        {
            get
            {
                return "MyBGM";
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
                return "�Q�[�����ŗ���BGM���D�݂̋ȂɕύX����v���O�C���ł��B";
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
        
        public void Run()
        {
            /*
            if (this.frm != null && !this.frm.IsDisposed)
            {
                this.frm.Activate();
                return;
            }
            this.frm = new BGMListManagerForm();
            this.frm.Show();
            */
        }


        public void HookRun(HookPoint hp)
        {
            switch (hp)
            {
                case HookPoint.RunGame:
                    break;
                case HookPoint.Shutdown:
                    break;
                case HookPoint.Startup:
                    break;
                case HookPoint.UpdatedGame:
                    break;
            }
            return;
        }

    }
}
