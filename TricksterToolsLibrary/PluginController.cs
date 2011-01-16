using System;
using System.Collections.Generic;
using System.Text;
using TricksterTools.Plugins;
using TricksterTools.Debug;

namespace TricksterTools.Library
{
    public class PluginController
    {
        /// <summary>
        /// �v���O�C����ǂݍ��݂܂�
        /// </summary>
        public static IPlugin[] loadPlugins(IPluginHost host)
        {
            //�C���X�g�[������Ă���v���O�C����T��
            PluginInfo[] pis = PluginInfo.FindPlugins();

            //�v���O�C���̃C���X�^���X���擾����
            IPlugin[] plugins = new IPlugin[pis.Length];
            for (int i = 0; i < plugins.Length; i++)
            {
                plugins[i] = pis[i].CreateInstance(host);
            }

            return plugins;
        }
        
        /// <summary>
        /// �w�肳�ꂽ�v���O�C�������s���܂�
        /// </summary>
        /// <param name="plugins">�v���O�C���ꗗ</param>
        /// <param name="pluginName">�v���O�C���N���X��</param>
        public static void PluginRun(IPlugin[] plugins, string pluginName)
        {
            // �v���O�C���N���X������v���O�C����T��
            //�i�������O�̃v���O�C���N���X����������ƍ��������Ƃ�...�j
            foreach (IPlugin plugin in plugins)
            {
                if (pluginName == plugin.GetType().Name)
                {
                    SimpleLogger.WriteLine("run " + plugin.GetType().Name);
                    //�N���b�N���ꂽ�v���O�C�������s����
                    plugin.Run();
                    return;
                }
            }
        }

        /// <summary>
        /// �v���O�C���̏����擾���܂�
        /// </summary>
        /// <param name="plugins">�ǂݍ��܂ꂽ�v���O�C���̔z��</param>
        /// <param name="pluginName">�v���O�C���N���X��</param>
        public static IPlugin getPluginInfo(IPlugin[] plugins, string pluginClassName)
        {
            // �v���O�C��������v���O�C����T��
            //�i�������O�̃v���O�C������������ƍ��������Ƃ�...�j
            IPlugin retPlugin = plugins[0];

            foreach (IPlugin plugin in plugins)
            {
                if (pluginClassName == plugin.GetType().Name)
                {
                    retPlugin = plugin;
                    break;
                }

            }
            return retPlugin;
        }



        /// <summary>
        /// �w�肳�ꂽ�v���O�C�����t�b�N�|�C���g�ɉ����Ď��s���܂�
        /// </summary>
        /// <param name="plugins">�v���O�C���ꗗ</param>
        /// <param name="pluginName">�v���O�C���N���X��</param>
        public static void PluginHook(IPlugin[] plugins, HookPoint hookPoint)
        {
            // �v���O�C���N���X������v���O�C����T��
            //�i�������O�̃v���O�C���N���X����������ƍ��������Ƃ�...�j
            foreach (IPlugin plugin in plugins)
            {
                Type typeHook = typeof(IPlugin);
                System.Reflection.MethodInfo mi = typeHook.GetMethod("HookRun");
                if (mi != null)
                {
                    SimpleLogger.WriteLine("HookRun[" + hookPoint.ToString() + "]: " + plugin.GetType().Name);
                    //�N���b�N���ꂽ�v���O�C�������s����
                    plugin.HookRun(hookPoint);
                    return;
                }
            }
        }
    }
}
