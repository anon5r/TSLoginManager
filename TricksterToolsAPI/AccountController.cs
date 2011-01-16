using System;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Win32;
using TricksterTools.CommonXmlStructure;
using TricksterTools.API.DataStructure;
using TricksterTools.Debug;

namespace TricksterTools
{
    namespace API
    {
        namespace Controller
        {
            /// <summary>
            /// �A�J�E���g����
            /// </summary>
            public class AccountController
            {
                //static Logger logger = SimpleLogger.setLogger("AccountController");
                static System.Text.Encoding encoder = System.Text.Encoding.GetEncoding("Shift_JIS");
                const string BASEKEYWORD = @"%s%l%o%o%T%$%r%e%t%s%k%c%i%r%T%$%f%O%$%d%r%o%w%y%e%K";
                public static string MasterKey = null;
                public static bool isLoadedAccount = false;

                public static ArrayList AccountData = new ArrayList();

                /// <summary>
                /// �A�J�E���g����ǉ����܂��B
                /// </summary>
                /// <param name="props">�A�J�E���g�v���p�e�B</param>
                public void add(Accounts.AccountProperties props)
                {
                    AccountData.Add(props);
                }
                /// <summary>
                /// �A�J�E���gID����A�J�E���g�����擾���܂��B
                /// </summary>
                /// <param name="id">�A�J�E���gID</param>
                /// <returns>�A�J�E���g���</returns>
                public Accounts.AccountProperties get(string id, Accounts.AccountProperties.LoginSite site)
                {
                    return this.containID(id, site);
                }
                /// <summary>
                /// �A�J�E���g�����ꗗ����폜���܂��B
                /// </summary>
                /// <param name="id">�A�J�E���gID</param>
                /// <param name="site">�T�C�gID</param>
                public void delete(string id, Accounts.AccountProperties.LoginSite site)
                {
                    Accounts.AccountProperties props = this.containID(id, site);
                    if (props.Equals(null))
                    {
                        SimpleLogger.WriteLine("ID: \""+ id + "\" is already deleted or nothing.");
                        MessageBox.Show("ID: \" " + id + " \"�͑��݂��܂���B", "TricksterTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    AccountData.Remove(props);
                    SimpleLogger.WriteLine("ID: \"" + id + "\" is deleted.");
                }

                /// <summary>
                /// ���ݕێ����Ă���A�J�E���g����S�č폜���܂��B
                /// </summary>
                public void clear()
                {
                    AccountData.Clear();
                }
                /// <summary>
                /// ���ݕێ����Ă���A�J�E���g����S�ĕԂ��܂��B
                /// </summary>
                /// <returns>�z�񉻂��ꂽ�A�J�E���g���</returns>
                public ArrayList list()
                {
                    return AccountData;
                }

                /// <summary>
                /// ���ݕێ����Ă���A�J�E���g���ꗗ�̒�����w��̕������ID���܂܂�Ă��邩�������܂��B
                /// �������@�ɂ��Ă͊��S��v�݂̂��s���܂��B
                /// </summary>
                /// <param name="ID">��������ID</param>
                /// <param name="site">�T�C�g</param>
                /// <returns>���������ꍇ�Ώۂ�AccountProperties��Ԃ��܂��B������Ȃ������ꍇ��null��Ԃ��܂��B</returns>
                public Accounts.AccountProperties containID(string ID, Accounts.AccountProperties.LoginSite site)
                {
                    IEnumerator ienum = AccountData.GetEnumerator();
                    while (ienum.MoveNext())
                    {
                        Accounts.AccountProperties acprop = (Accounts.AccountProperties)ienum.Current;
                        if (acprop.ID == ID && acprop.Site == site)
                        {
                            return acprop;
                        }
                    }
                    SimpleLogger.WriteLine("\"" + ID + "\" was not found.");
                    return null;
                }

                /// <summary>
                /// ID�����݂̈ꗗ���ɑ��݂��邩�m�F���܂��B
                /// </summary>
                /// <param name="ID">�����Ώۂ�ID</param>
                /// <param name="site">�����Ώۂ̃T�C�g</param>
                /// <returns>���������ꍇ��true, ������Ȃ������ꍇ��false��Ԃ��܂��B</returns>
                public bool isExists(string ID, Accounts.AccountProperties.LoginSite site)
                {
                    Accounts.AccountProperties tmp = this.containID(ID, site);
                    if (tmp == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                #region saveAccounts()
                public void saveAccounts(string filename, string keyword)
                {
                    string PasswordEncryptKey;
                    XmlAccount[] config = new XmlAccount[AccountData.Count];

                    PasswordEncryptKey = setKeyword(keyword);
                    IEnumerator ienum = AccountData.GetEnumerator();
                    int i = 0;
                    while(ienum.MoveNext())
                    {
                        Accounts.AccountProperties acprop = (Accounts.AccountProperties)ienum.Current;
                        config[i] = new XmlAccount();
                        config[i].ID = acprop.ID;
                        config[i].Password = Common.EncryptString(acprop.Password, keyword);
                        config[i].Site = (int)acprop.Site;
                        i++;
                    }
                    XmlMyAccounts accountdatas = new XmlMyAccounts();
                    accountdatas.AccountList = config;
                    accountdatas.EncryptKey = PasswordEncryptKey;

                    XmlTricksterRoot XmlRoot = new XmlTricksterRoot();
                    XmlRoot.MyAccount = accountdatas;


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

                    FileInfo fi = new FileInfo(filepath);
                    if (fi.IsReadOnly)
                    {
                        SimpleLogger.WriteLine("could not write to file \"" + filepath + "\"");
                        MessageBox.Show("�A�J�E���g���t�@�C���͓ǂݎ���p�̂��߁A�ۑ��ł��܂���B", "TricksterTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (AccountData.Count > 0)
                    {
                        try
                        {
                            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlTricksterRoot));
                            FileStream fs = new FileStream(filepath, FileMode.Create);
                            serializer.Serialize(fs, XmlRoot);
                            fs.Close();
                        }
                        /*
                        catch (System.Security.SecurityException se)
                        {
                            SimpleLogger.WriteLine(se.GetType().ToString() + Environment.NewLine + se.Message);
                            //MessageBox.Show("��O�G���[:\n�Z�L�����e�B�G���[�ł��B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw se;
                        }
                        */
                        catch (System.IO.IOException ioe)
                        {
                            SimpleLogger.WriteLine(ioe.GetType().ToString() + Environment.NewLine + ioe.Message);
                            //MessageBox.Show("��O�G���[:\n���o�͎��ɃG���[���������܂����B", "IOException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw ioe;
                        }
                        catch (System.Xml.XmlException xe)
                        {
                            SimpleLogger.WriteLine(xe.GetType().ToString() + Environment.NewLine + xe.Message);
                            //MessageBox.Show("��O�G���[:\n�A�J�E���g���ǂݍ��݃G���[", "XmlException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw xe;
                        }
                        catch (System.Exception e)
                        {
                            SimpleLogger.WriteLine(e.GetType().ToString() + Environment.NewLine + e.Message);
                            //MessageBox.Show("��O�G���[:\n�����̓��肪�ł��܂���ł����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw e;
                        }
                    }
                }
                #endregion
                #region loadAccounts()
                /// <summary>
                /// XML�t�@�C������ID�ƃp�X���[�h��ǂݍ��݁A�n�b�V���e�[�u��������
                /// </summary>
                /// 
                /// <param name="filename">�ǂݍ���XML�t�@�C����</param>
                /// <param name="keyword">�A�J�E���g�f�[�^��ǂݍ��ރL�[</param>
                /// <returns>ID���L�[�APasswod��l�Ƃ����n�b�V���e�[�u��</returns>
                public void loadAccounts(string filename, string keyword)
                {
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
                    if (File.Exists(filepath))
                    {
                        try
                        {
                            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlTricksterRoot));
                            System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.Open);
                            XmlTricksterRoot XmlRoot = (XmlTricksterRoot)serializer.Deserialize(fs);
                            XmlMyAccounts accountDatas = new XmlMyAccounts();
                            accountDatas = XmlRoot.MyAccount;
                            String PasswordDecryptKey;
                            PasswordDecryptKey = getKeyword(accountDatas.EncryptKey);

                            if (AccountController.MasterKey != PasswordDecryptKey)
                            {
                                SimpleLogger.WriteLine("failed to match MasterKey and EncryptedKey in account data.");
                                MessageBox.Show("�Í����L�[���[�h����v���Ȃ����߃A�J�E���g����ǂݍ��߂܂���ł����B", "�A�J�E���g���ǂݍ��݃G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            AccountController.isLoadedAccount = true;

                            XmlAccount[] dataPack = (XmlAccount[])accountDatas.AccountList;

                            foreach (XmlAccount data in dataPack)
                            {
                                Accounts.AccountProperties props = new Accounts.AccountProperties();
                                //AccountData[data.ID] = Common.DecryptString(data.Password, AccountController.MasterKey);
                                props.ID = data.ID;
                                props.Password = Common.DecryptString(data.Password, PasswordDecryptKey);
                                if (data.Site.Equals(null))
                                {
                                    props.Site = Accounts.AccountProperties.LoginSite.Official;
                                }
                                else
                                {
                                    try
                                    {
                                        props.Site = (Accounts.AccountProperties.LoginSite)data.Site;
                                    }catch(Exception e){
                                        SimpleLogger.WriteLine(e.Message);
                                        props.Site = Accounts.AccountProperties.LoginSite.Official;
                                    }
                                }

                                AccountData.Add(props);
                            }
                            fs.Close();
                        }
                        catch (FileLoadException fle)
                        {
                            SimpleLogger.WriteLine(fle.GetType().ToString() + Environment.NewLine + fle.Message);
                            //MessageBox.Show("��O�G���[:\n�t�@�C���̓ǂݍ��݂Ɏ��s���܂����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return;
                            throw fle;
                        }
                        catch (System.Xml.XmlException xe)
                        {
                            SimpleLogger.WriteLine(xe.GetType().ToString() + Environment.NewLine + xe.Message);
                            //MessageBox.Show("��O�G���[:\n�f�[�^�̏����Ɏ��s���܂����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return;
                            throw xe;
                        }
                        catch (System.UnauthorizedAccessException uae)
                        {
                            SimpleLogger.WriteLine(uae.GetType().ToString() + Environment.NewLine + uae.Message);
                            //MessageBox.Show("��O�G���[:\n�A�J�E���g�t�@�C�����ǂݎ���p���A�������Ȃ����ߓǂݍ��߂܂���ł����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return;
                            throw uae;
                        }
                        catch (System.InvalidOperationException ioe)
                        {
                            SimpleLogger.WriteLine(ioe.GetType().ToString() + Environment.NewLine + ioe.Message);
                            //MessageBox.Show("��O�G���[:\n�����ȃ��\�b�h�̌Ăяo�����s���܂����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return;
                            throw ioe;
                        }
                    }
                    else
                    {
                        SimpleLogger.WriteLine("account file 'accounts.dat' could not read/found.");
                        MessageBox.Show("�A�J�E���g���t�@�C����ǂݍ��߂܂���ł����B" + Environment.NewLine + "'" + filepath + "'", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        isLoadedAccount = false;
                        return;
                    }
                }
                #endregion
                #region loadAccountKey()
                /// <summary>
                /// XML�t�@�C������ID�ƃp�X���[�h��ǂݍ��݁A�n�b�V���e�[�u��������
                /// </summary>
                /// 
                /// <param name="string">�ǂݍ��ރe�L�X�g�t�@�C����</param>
                /// <returns>ID���L�[�APasswod��l�Ƃ����n�b�V���e�[�u��</returns>
                public static ArrayList loadAccountKey(string filename)
                {
                    string filepath = "";
                    if (Path.IsPathRooted(filename))
                    {
                        filepath = filename;
                        filename = Path.GetFileName(filename);
                    }
                    else
                    {
                        filepath = Path.GetFullPath(@".\" + filename);
                    }
                    if (File.Exists(filepath))
                    {
                        try
                        {
                            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlAccount));
                            System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.Open);
                            XmlMyAccounts loadDatas = (XmlMyAccounts)serializer.Deserialize(fs);

                            XmlAccount[] dataPack = (XmlAccount[])loadDatas.AccountList;
                            foreach (XmlAccount data in dataPack)
                            {
                                Accounts.AccountProperties props = new Accounts.AccountProperties();
                                props.ID = data.ID;
                                props.Password = Common.DecryptString(data.Password, AccountController.MasterKey);
                                if (data.Site.Equals(null))
                                {
                                    props.Site = Accounts.AccountProperties.LoginSite.Official;
                                }
                                else
                                {
                                    try
                                    {
                                        props.Site = (Accounts.AccountProperties.LoginSite)data.Site;
                                    }
                                    catch (Exception e)
                                    {
                                        SimpleLogger.WriteLine(e.Message);
                                        props.Site = Accounts.AccountProperties.LoginSite.Official;
                                    }
                                }

                                AccountData.Add(props);
                            }
                            fs.Close();
                            
                            return AccountData;
                        }
                        catch (FileLoadException ex)
                        {
                            SimpleLogger.WriteLine(ex.Message);
                            MessageBox.Show("��O�G���[:\n�t�@�C���̓ǂݍ��ݓ񎸔s���܂����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return AccountData;
                        }
                    }
                    else
                    {
                        MessageBox.Show("�A�J�E���g���t�@�C����ǂݍ��߂܂���ł����B" + Environment.NewLine + "'" + filepath + "'", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return AccountData;
                    }
                }
                #endregion
                #region DeleteAccount()
                /// <summary>
                /// �A�J�E���g�폜
                /// </summary>
                /// <param name="id">�폜����ID</param>
                public void DeleteAccount(string id, Accounts.AccountProperties.LoginSite site)
                {
                    string msg = "�A�J�E���g��� '" + id + "' ���폜���悤�Ƃ��Ă��܂��B��낵���ł����H";
                    DialogResult msgbox = MessageBox.Show(msg, "Trickster Tools", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (msgbox == DialogResult.No)
                    {
                        return;
                    }

                    this.delete(id, site);
                    SimpleLogger.WriteLine("delete account: " + id);

                }
                #endregion
                #region loadAccountFromText()
                /*
                /// <summary>
                /// �e�L�X�g�t�@�C������ID�ƃp�X���[�h��ǂݍ��݁A�n�b�V���e�[�u��������
                /// [[����]]
                /// AccountID:Password
                /// </summary>
                /// 
                /// <param name="string">�ǂݍ��ރe�L�X�g�t�@�C����</param>
                /// <returns>ID���L�[�APasswod��l�Ƃ����n�b�V���e�[�u��</returns>
                public static SortedList loadAccountFromText(string filename)
                {
                    string filepath = "";
                    if (Path.IsPathRooted(filename))
                    {
                        filepath = filename;
                        filename = Path.GetFileName(filename);
                    }
                    else
                    {
                        filepath = Path.GetFullPath(@".\" + filename);
                    }
                    if (File.Exists(filepath))
                    {
                        try
                        {
                            StreamReader sr = new StreamReader(filepath, System.Text.Encoding.GetEncoding("Shift_JIS"));
                            string line;
                            string[] SplitStr;

                            while ((line = sr.ReadLine()) != null)
                            {
                                SplitStr = line.Split(new char[] { ':' });
                                try
                                {
                                    AccountData[SplitStr[0].ToString()] = Common.DecryptString(SplitStr[1].ToString(), "TrickLauncher");
                                }
                                catch (IndexOutOfRangeException ioore)
                                {
                                    SimpleLogger.WriteLine(ioore.Message);
                                    MessageBox.Show("�ǂݍ��܂ꂽ�t�@�C���̌`�����������Ȃ��\�������邽��" + Environment.NewLine + "����ȏ㏈���𑱍s�ł��܂���B", "Convert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    System.Environment.Exit(1);
                                }
                            }
                            if (AccountData.Count < 1)
                            {
                                AccountData["__TSLM_NULL__"] = "__TSLM_NULL__";
                            }

                            return AccountData;
                        }
                        catch (FileLoadException ex)
                        {
                            SimpleLogger.WriteLine(ex.Message);
                            MessageBox.Show("��O�G���[:" + Environment.NewLine + "�t�@�C���̓ǂݍ��݂Ɏ��s���܂����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            AccountData["__TSLM_NULL__"] = "__TSLM_NULL__";
                            return AccountData;
                        }
                    }
                    else
                    {
                        //if (!System.Deployment.Application.ApplicationDeployment.CurrentDeployment.IsFirstRun)
                        //{
                        MessageBox.Show("�ݒ�t�@�C����ǂݍ��߂܂���ł����B" + Environment.NewLine + "'" + filepath + "'", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                        AccountData["__TSLM_NULL__"] = "__TSLM_NULL__";
                        return AccountData;
                    }
                }
                */
                #endregion
                #region saveloadAccountToText()
                /*
                /// <summary>
                /// �t�@�C���̕ۑ�
                /// </summary>
                /// <param name="filename">�t�@�C����</param>
                public static void saveloadAccountToText(string filename)
                {
                    System.Text.StringBuilder config = new System.Text.StringBuilder();
                    config.Insert(0, "");
                    foreach (string id in AccountData.Keys)
                    {
                        config.AppendLine(id + ":" + Common.EncryptString(AccountData[id].ToString(), "TrickLauncher"));
                    }
                    string filepath = "";
                    if (Path.IsPathRooted(filename))
                    {
                        filepath = filename;
                        filename = Path.GetFileName(filename);
                    }
                    else
                    {
                        filepath = Path.GetFullPath(@".\" + filename);
                    }
                    if (!File.Exists(filepath))
                    {
                        // �t�@�C�����Ȃ���΍쐬
                        FileStream fs = File.Create(filepath);
                        fs.Close();
                    }
                    if (!AccountData.Contains("__TSLM_NULL__"))
                    {
                        try
                        {
                            StreamWriter writer = new StreamWriter(filename, false, System.Text.Encoding.Default);
                            writer.Write(config.ToString());
                            writer.Close();
                        }
                        catch (System.Security.SecurityException se)
                        {
                            SimpleLogger.WriteLine(se.Message);
                            MessageBox.Show("��O�G���[:" + Environment.NewLine + "�Z�L�����e�B�G���[�B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (System.IO.IOException ioe)
                        {
                            SimpleLogger.WriteLine(ioe.Message);
                            MessageBox.Show("��O�G���[:" + Environment.NewLine + "���o�͊֌W�ŃG���[���������܂����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (System.Exception e)
                        {
                            SimpleLogger.WriteLine(e.Message);
                            MessageBox.Show("��O�G���[:" + Environment.NewLine + "���������ł��܂���ł����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                */
                #endregion
                #region getKeyword()
                /// <summary>
                /// �x�[�X�L�[���[�h�̕�����
                /// </summary>
                /// <param name="encryptedKeyword">�Í������ꂽ�x�[�X�L�[���[�h</param>
                /// <returns>���������ꂽ�x�[�X�L�[</returns>
                public static string getKeyword(string encryptedKeyword)
                {
                    string key = AccountController.BASEKEYWORD;
                    string[] arr = key.Split('%');
                    int i = arr.Length - 1;
                    key = "";
                    while(i >= 0){
                        key += arr[i].Replace("$", "/");
                        i--;
                    }
                    return Common.DecryptString(encryptedKeyword, key);
                }
                #endregion
                #region setKeyword()
                /// <summary>
                /// �x�[�X�L�[���[�h�̈Í���
                /// </summary>
                /// <param name="encryptedKeyword">�x�[�X�L�[</param>
                /// <returns>�Í����ς݃x�[�X�L�[</returns>
                public static string setKeyword(string Keyword)
                {
                    string key = AccountController.BASEKEYWORD;
                    string[] arr = key.Split('%');
                    int i = arr.Length - 1;
                    key = "";
                    while (i >= 0)
                    {
                        key += arr[i].Replace("$", "/");
                        i--;
                    }
                    return Common.EncryptString(Keyword, key);
                }
                #endregion
                public static bool saveMasterKey(string MasterKey, string KeyFileName)
                {
                    string filepath = "";
                    if (Path.IsPathRooted(KeyFileName))
                    {
                        filepath = KeyFileName;
                        KeyFileName = Path.GetFileName(KeyFileName);
                    }
                    else
                    {
                        filepath = Path.GetFullPath(@".\" + KeyFileName);
                    }
                    if (!File.Exists(KeyFileName))
                    {
                        // �t�@�C�����Ȃ���΍쐬
                        FileStream fs = File.Create(KeyFileName);
                        fs.Close();
                    }
                    try
                    {
                        StreamWriter writer = new StreamWriter(KeyFileName, false, System.Text.Encoding.Default);
                        writer.Write(MasterKey);
                        writer.Close();
                        return true;
                    }
                    catch (System.Security.SecurityException se)
                    {
                        SimpleLogger.WriteLine(se.Message);
                        //MessageBox.Show("��O�G���[:" + Environment.NewLine + "�Z�L�����e�B�G���[", "SecurityException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw se;
                    }
                    catch (System.IO.IOException ioe)
                    {
                        SimpleLogger.WriteLine(ioe.Message);
                        //MessageBox.Show("��O�G���[:" + Environment.NewLine + "���o�͊֌W�ŃG���[���������܂����B", "IOException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw ioe;
                    }
                    catch (UnauthorizedAccessException uae)
                    {
                        SimpleLogger.WriteLine(uae.Message);
                        //MessageBox.Show("��O�G���[:" + Environment.NewLine + "�t�@�C�����ǂݎ���p���[�h�ɂȂ��Ă��邩�A�������݌���������܂���B", "UnauthorizedAccessException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw uae;
                    }
                    catch (System.Exception e)
                    {
                        SimpleLogger.WriteLine(e.Message);
                        //MessageBox.Show("��O�G���[:" + Environment.NewLine + "���������ł��܂���ł����B", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw e;
                    }

                    //return false;
                }
            }
        }
    }
}
