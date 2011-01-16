using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using TricksterTools.Plugins.Config;
using TricksterTools.Debug;

namespace TricksterTools.Plugins
{
    public class PluginSettings
    {
        #region saveConfig()
        public static void saveConfig(XmlPlugin[] Plugin)
        {
            string filename = @".\plugins.config.xml";
            XmlTricksterRoot XmlRoot = new XmlTricksterRoot();
            XmlTools Tools = new XmlTools();
            
            Tools.Plugin = Plugin;
            XmlRoot.Tools = Tools;


            string filepath = "";
            if (Path.IsPathRooted(filename))
            {
                filepath = filename;
                filename = Path.GetFileName(filename);
            }
            else
            {
                filepath = Path.GetFullPath(Environment.CurrentDirectory + @"\" + filename);
            }

            if (!File.Exists(filepath))
            {
                // �t�@�C�����Ȃ���΍쐬
                FileStream fs = new FileStream(filepath, FileMode.Create);
                fs.Close();
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XmlTricksterRoot));
                FileStream fs = new FileStream(filepath, FileMode.Create);
                serializer.Serialize(fs, XmlRoot);
                fs.Close();
            }
            catch (System.Security.SecurityException se)
            {
                SimpleLogger.WriteLine(se.Message);
                //MessageBox.Show("��O�G���[:" + Environment.NewLine + "�Z�L�����e�B�G���[�ł��B", "SecurityException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw se;
            }
            catch (System.IO.IOException ioe)
            {
                SimpleLogger.WriteLine(ioe.Message);
                //MessageBox.Show("��O�G���[:" + Environment.NewLine + "���o�͎��ɃG���[���������܂����B", "IOException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ioe;
            }
            catch (System.Xml.XmlException xe)
            {
                SimpleLogger.WriteLine(xe.Message);
                //MessageBox.Show("��O�G���[:" + Environment.NewLine + "�ݒ�ǂݍ��݃G���[", "XmlException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw xe;
            }
            catch (System.Exception e)
            {
                SimpleLogger.WriteLine(e.Message);
                //MessageBox.Show("��O�G���[:" + Environment.NewLine + "�����̓��肪�ł��܂���ł����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw e;
            }
        }
        #endregion
        #region loadSettings()
        /// <summary>
        /// XML�t�@�C������ݒ��ǂݍ��݁A�N���X���̃v���p�e�B�ɐݒ肷��
        /// </summary>
        /// 
        /// <param name="pluginName">�v���O�C����</param>
        /// <returns>IPluginConfig</returns>
        public static IPluginConfig loadSettings(string PluginName)
        {
            string filepath = "";
            string filename = @".\plugins.config.xml";
            if (Path.IsPathRooted(filename))
            {
                filepath = filename;
                filename = Path.GetFileName(filename);
            }
            else
            {
                filepath = Path.GetFullPath(Environment.CurrentDirectory + @"\" + filename);
            }
            if (File.Exists(filepath))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(XmlTricksterRoot));
                    System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.Open);
                    XmlTricksterRoot XmlRoot = (XmlTricksterRoot)serializer.Deserialize(fs);
                    XmlTools Tools;
                    XmlPlugin[] Plugins;


                    if (XmlRoot.Tools.name != "TSLoginManager")
                    {
                        throw new ConfigLoadException("TSLoginManager��Plugin�ݒ�ȊO�̐ݒ�t�@�C�����ǂݍ��܂�Ă��܂��B" + Environment.NewLine + "�ݒ�͓ǂݍ��܂�܂���ł����B");
                    }
                    Tools = XmlRoot.Tools;
                    Plugins = Tools.Plugin;
                    foreach (XmlPlugin Plugin in Plugins)
                    {
                        if (Plugin.name.Length == 0) continue;

                        if (Plugin.name == PluginName)
                        {
                            return Plugin.Config;
                        }
                    }
                    throw new PluginConfigNotFoundException();

                }
                catch (ConfigLoadException cle)
                {
                    //MessageBox.Show(cle.Message, "�ݒ�ǂݍ��݃G���[", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    throw cle;
                }
                catch (PluginConfigNotFoundException pcnfe)
                {
                    throw pcnfe;
                }
                catch (FileLoadException fle)
                {
                    SimpleLogger.WriteLine(fle.Message);
                    //MessageBox.Show("��O�G���[:" + Environment.NewLine + "�t�@�C���̓ǂݍ��݂Ɏ��s���܂����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new ConfigLoadException("�t�@�C���̓ǂݍ��݂Ɏ��s���܂����B");
                }
                catch (System.Xml.XmlException xe)
                {
                    SimpleLogger.WriteLine(xe.Message);
                    //MessageBox.Show("��O�G���[:" + Environment.NewLine + "�f�[�^�̏����Ɏ��s���܂����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new ConfigLoadException("�f�[�^�̏����Ɏ��s���܂����B");
                }
                catch (System.InvalidOperationException ioe)
                {
                    SimpleLogger.WriteLine(ioe.Message);
                    //MessageBox.Show("��O�G���[:" + Environment.NewLine + "�����ȃ��\�b�h�̌Ăяo�����s���܂����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new ConfigLoadException("�����ȃ��\�b�h�̌Ăяo�����s���܂����B");
                }
            }
            else
            {
                SimpleLogger.WriteLine("setting file 'plugins.config.xml' could not read/found.");
                throw new ConfigLoadException("�ݒ�t�@�C����ǂݍ��߂܂���ł����B" + Environment.NewLine + "'" + filepath + "'");
            }
        }
        #endregion
    }

    public class ConfigLoadException : Exception
    {
        public ConfigLoadException(string Message) : base(Message)
        {
        }

        public ConfigLoadException(string Message, Exception innerException) : base(Message, innerException)
        {
        }
    }
    public class PluginConfigNotFoundException : Exception
    {
    }
}
