using System;
using System.Collections.Generic;
using System.Text;

namespace TricksterTools.Plugins
{
    public enum HookPoint
    {
        /// <summary>
        /// �A�v���P�[�V�����N������
        /// </summary>
        Startup,

        /// <summary>
        /// �Q�[���A�b�v�f�[�g��
        /// </summary>
        UpdatedGame,

        /// <summary>
        /// �Q�[���̎��s���O
        /// </summary>
        RunGame,

        /// <summary>
        /// �A�v���P�[�V�����I�����O
        /// </summary>
        Shutdown,
    }

    public interface IPlugin
    {
        /// <summary>
        /// �v���O�C���̖��O
        /// </summary>
        string Name { get;}
        
        /// <summary>
        /// �v���O�C���̃o�[�W����
        /// </summary>
        string Version { get;}

        /// <summary>
        /// �v���O�C���̍��
        /// </summary>
        string Author { get;}

        /// <summary>
        /// �v���O�C����҂̃T�C�g
        /// </summary>
        string URL { get;}

        /// <summary>
        /// �v���O�C���̐���
        /// </summary>
        string Description { get;}

        /// <summary>
        /// �v���O�C���̐ݒ�
        /// </summary>
        IPluginConfig Config { get; set; }

        /// <summary>
        /// �v���O�C���̃z�X�g
        /// </summary>
        IPluginHost Host { get; set;}

        /// <summary>
        /// �v���O�C�������s����
        /// </summary>
        void Run();

        /// <summary>
        /// �v���O�C�������s����
        /// </summary>
        void HookRun(HookPoint hp);

    }

    /// <summary>
    /// �v���O�C���̃z�X�g�Ŏ�������C���^�[�t�F�C�X
    /// </summary>
    public interface IPluginHost
    {
        /// <summary>
        /// �z�X�g�̃��C���t�H�[��
        /// </summary>
        System.Windows.Forms.Form HostForm { get;}

        /// <summary>
        /// �z�X�g�Ń��b�Z�[�W��\������
        /// </summary>
        /// <param name="plugin">���\�b�h���Ăяo���v���O�C��</param>
        /// <param name="msg">�\�����郁�b�Z�[�W</param>
        void ShowMessage(IPlugin plugin, string msg);
    }
}
