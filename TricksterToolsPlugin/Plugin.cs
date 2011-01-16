using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections;
using TricksterTools.Debug;

namespace TricksterTools.Plugins
{
    /// <summary>
    /// �v���O�C���Ɋւ�����
    /// </summary>
    public class PluginInfo
    {
        private string _location;
        private string _className;
        private IPluginConfig _config;

        /// <summary>
        /// PluginInfo�N���X�̃R���X�g���N�^
        /// </summary>
        /// <param name="path">�A�Z���u���t�@�C���̃p�X</param>
        /// <param name="cls">�N���X�̖��O</param>
        private PluginInfo(string path, string cls)
        {
            this._location = path;
            this._className = cls;
        }

        /// <summary>
        /// �A�Z���u���t�@�C���̃p�X
        /// </summary>
        public string Location
        {
            get { return _location; }
        }

        /// <summary>
        /// �N���X�̖��O
        /// </summary>
        public string ClassName
        {
            get { return _className; }
        }

        /// <summary>
        /// �L���ȃv���O�C����T��
        /// </summary>
        /// <returns>�L���ȃv���O�C����PluginInfo�z��</returns>
        public static PluginInfo[] FindPlugins()
        {
            ArrayList plugins = new ArrayList();
            //IPlugin�^�̖��O
            string ipluginName = typeof(Plugins.IPlugin).FullName;

            //�v���O�C���t�H���_
            string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            folder += @"\plugins";
            if (!Directory.Exists(folder))
            {

                SimpleLogger.WriteLine("Plugin folder \"" + folder + "\" was not found.");
                try
                {
                    Directory.CreateDirectory(folder);
                }
                catch (Exception e)
                {
                    SimpleLogger.WriteLine("Failed to create plugin folder \"" + folder + "\".");
                    SimpleLogger.WriteLine(e.Message);
                    //System.Windows.Forms.MessageBox.Show("�v���O�C���t�H���_�̍쐬�Ɏ��s���܂����B");
                }
            }

            //.dll�t�@�C����T��
            string[] dlls = Directory.GetFiles(folder, "*.dll");

            foreach (string dll in dlls)
            {
                try
                {
                    //�A�Z���u���Ƃ��ēǂݍ���
                    Assembly asm = Assembly.LoadFrom(dll);
                    foreach (Type t in asm.GetTypes())
                    {
                        //�A�Z���u�����̂��ׂĂ̌^�ɂ��āA
                        //�v���O�C���Ƃ��ėL�������ׂ�
                        if (t.IsClass && t.IsPublic && !t.IsAbstract &&
                            t.GetInterface(ipluginName) != null)
                        {
                            //PluginInfo���R���N�V�����ɒǉ�����
                            plugins.Add(new PluginInfo(dll, t.FullName));
                        }
                    }
                }
                catch
                {
                }
            }

            //�R���N�V������z��ɂ��ĕԂ�
            return (PluginInfo[])plugins.ToArray(typeof(PluginInfo));
        }

        /// <summary>
        /// �v���O�C���N���X�̃C���X�^���X���쐬����
        /// </summary>
        /// <returns>�v���O�C���N���X�̃C���X�^���X</returns>
        public IPlugin CreateInstance(IPluginHost host)
        {
            try
            {
                //�A�Z���u����ǂݍ���
                Assembly asm = Assembly.LoadFrom(this.Location);
                //�N���X������C���X�^���X���쐬����
                Plugins.IPlugin plugin = (Plugins.IPlugin)asm.CreateInstance(this.ClassName);
                //IPluginHost�̐ݒ�
                plugin.Host = host;
                try
                {
                    plugin.Config = PluginSettings.loadSettings(this.ClassName);
                }
                catch (ConfigLoadException cle)
                {
                    
                }
                catch (PluginConfigNotFoundException pcnfe)
                {

                }
                return plugin;
            }
            catch
            {
                return null;
            }
        }
    }


}
