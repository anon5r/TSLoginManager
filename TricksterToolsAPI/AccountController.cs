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
            /// アカウント処理
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
                /// アカウント情報を追加します。
                /// </summary>
                /// <param name="props">アカウントプロパティ</param>
                public void add(Accounts.AccountProperties props)
                {
                    AccountData.Add(props);
                }
                /// <summary>
                /// アカウントIDからアカウント情報を取得します。
                /// </summary>
                /// <param name="id">アカウントID</param>
                /// <returns>アカウント情報</returns>
                public Accounts.AccountProperties get(string id, Accounts.AccountProperties.LoginSite site)
                {
                    return this.containID(id, site);
                }
                /// <summary>
                /// アカウント情報を一覧から削除します。
                /// </summary>
                /// <param name="id">アカウントID</param>
                /// <param name="site">サイトID</param>
                public void delete(string id, Accounts.AccountProperties.LoginSite site)
                {
                    Accounts.AccountProperties props = this.containID(id, site);
                    if (props.Equals(null))
                    {
                        SimpleLogger.WriteLine("ID: \""+ id + "\" is already deleted or nothing.");
                        MessageBox.Show("ID: \" " + id + " \"は存在しません。", "TricksterTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    AccountData.Remove(props);
                    SimpleLogger.WriteLine("ID: \"" + id + "\" is deleted.");
                }

                /// <summary>
                /// 現在保持しているアカウント情報を全て削除します。
                /// </summary>
                public void clear()
                {
                    AccountData.Clear();
                }
                /// <summary>
                /// 現在保持しているアカウント情報を全て返します。
                /// </summary>
                /// <returns>配列化されたアカウント情報</returns>
                public ArrayList list()
                {
                    return AccountData;
                }

                /// <summary>
                /// 現在保持しているアカウント情報一覧の中から指定の文字列のIDが含まれているか検索します。
                /// 検索方法については完全一致のみが行われます。
                /// </summary>
                /// <param name="ID">検索するID</param>
                /// <param name="site">サイト</param>
                /// <returns>見つかった場合対象のAccountPropertiesを返します。見つからなかった場合はnullを返します。</returns>
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
                /// IDが現在の一覧内に存在するか確認します。
                /// </summary>
                /// <param name="ID">検索対象のID</param>
                /// <param name="site">検索対象のサイト</param>
                /// <returns>見つかった場合はtrue, 見つからなかった場合はfalseを返します。</returns>
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
                        // ファイルがなければ作成
                        FileStream fs = new FileStream(filepath, FileMode.Create);
                        fs.Close();
                    }

                    FileInfo fi = new FileInfo(filepath);
                    if (fi.IsReadOnly)
                    {
                        SimpleLogger.WriteLine("could not write to file \"" + filepath + "\"");
                        MessageBox.Show("アカウント情報ファイルは読み取り専用のため、保存できません。", "TricksterTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            //MessageBox.Show("例外エラー:\nセキュリティエラーです。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw se;
                        }
                        */
                        catch (System.IO.IOException ioe)
                        {
                            SimpleLogger.WriteLine(ioe.GetType().ToString() + Environment.NewLine + ioe.Message);
                            //MessageBox.Show("例外エラー:\n入出力時にエラーが発生しました。", "IOException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw ioe;
                        }
                        catch (System.Xml.XmlException xe)
                        {
                            SimpleLogger.WriteLine(xe.GetType().ToString() + Environment.NewLine + xe.Message);
                            //MessageBox.Show("例外エラー:\nアカウント情報読み込みエラー", "XmlException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw xe;
                        }
                        catch (System.Exception e)
                        {
                            SimpleLogger.WriteLine(e.GetType().ToString() + Environment.NewLine + e.Message);
                            //MessageBox.Show("例外エラー:\n原因の特定ができませんでした。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw e;
                        }
                    }
                }
                #endregion
                #region loadAccounts()
                /// <summary>
                /// XMLファイルからIDとパスワードを読み込み、ハッシュテーブル化する
                /// </summary>
                /// 
                /// <param name="filename">読み込むXMLファイル名</param>
                /// <param name="keyword">アカウントデータを読み込むキー</param>
                /// <returns>IDをキー、Passwodを値としたハッシュテーブル</returns>
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
                                MessageBox.Show("暗号化キーワードが一致しないためアカウント情報を読み込めませんでした。", "アカウント情報読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            //MessageBox.Show("例外エラー:\nファイルの読み込みに失敗しました。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return;
                            throw fle;
                        }
                        catch (System.Xml.XmlException xe)
                        {
                            SimpleLogger.WriteLine(xe.GetType().ToString() + Environment.NewLine + xe.Message);
                            //MessageBox.Show("例外エラー:\nデータの処理に失敗しました。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return;
                            throw xe;
                        }
                        catch (System.UnauthorizedAccessException uae)
                        {
                            SimpleLogger.WriteLine(uae.GetType().ToString() + Environment.NewLine + uae.Message);
                            //MessageBox.Show("例外エラー:\nアカウントファイルが読み取り専用か、権限がないため読み込めませんでした。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return;
                            throw uae;
                        }
                        catch (System.InvalidOperationException ioe)
                        {
                            SimpleLogger.WriteLine(ioe.GetType().ToString() + Environment.NewLine + ioe.Message);
                            //MessageBox.Show("例外エラー:\n無効なメソッドの呼び出しが行われました。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return;
                            throw ioe;
                        }
                    }
                    else
                    {
                        SimpleLogger.WriteLine("account file 'accounts.dat' could not read/found.");
                        MessageBox.Show("アカウント情報ファイルを読み込めませんでした。" + Environment.NewLine + "'" + filepath + "'", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        isLoadedAccount = false;
                        return;
                    }
                }
                #endregion
                #region loadAccountKey()
                /// <summary>
                /// XMLファイルからIDとパスワードを読み込み、ハッシュテーブル化する
                /// </summary>
                /// 
                /// <param name="string">読み込むテキストファイル名</param>
                /// <returns>IDをキー、Passwodを値としたハッシュテーブル</returns>
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
                            MessageBox.Show("例外エラー:\nファイルの読み込み二失敗しました。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return AccountData;
                        }
                    }
                    else
                    {
                        MessageBox.Show("アカウント情報ファイルを読み込めませんでした。" + Environment.NewLine + "'" + filepath + "'", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return AccountData;
                    }
                }
                #endregion
                #region DeleteAccount()
                /// <summary>
                /// アカウント削除
                /// </summary>
                /// <param name="id">削除するID</param>
                public void DeleteAccount(string id, Accounts.AccountProperties.LoginSite site)
                {
                    string msg = "アカウント情報 '" + id + "' を削除しようとしています。よろしいですか？";
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
                /// テキストファイルからIDとパスワードを読み込み、ハッシュテーブル化する
                /// [[書式]]
                /// AccountID:Password
                /// </summary>
                /// 
                /// <param name="string">読み込むテキストファイル名</param>
                /// <returns>IDをキー、Passwodを値としたハッシュテーブル</returns>
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
                                    MessageBox.Show("読み込まれたファイルの形式が正しくない可能性があるため" + Environment.NewLine + "これ以上処理を続行できません。", "Convert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("例外エラー:" + Environment.NewLine + "ファイルの読み込みに失敗しました。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            AccountData["__TSLM_NULL__"] = "__TSLM_NULL__";
                            return AccountData;
                        }
                    }
                    else
                    {
                        //if (!System.Deployment.Application.ApplicationDeployment.CurrentDeployment.IsFirstRun)
                        //{
                        MessageBox.Show("設定ファイルを読み込めませんでした。" + Environment.NewLine + "'" + filepath + "'", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                /// ファイルの保存
                /// </summary>
                /// <param name="filename">ファイル名</param>
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
                        // ファイルがなければ作成
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
                            MessageBox.Show("例外エラー:" + Environment.NewLine + "セキュリティエラー。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (System.IO.IOException ioe)
                        {
                            SimpleLogger.WriteLine(ioe.Message);
                            MessageBox.Show("例外エラー:" + Environment.NewLine + "入出力関係でエラーが発生しました。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (System.Exception e)
                        {
                            SimpleLogger.WriteLine(e.Message);
                            MessageBox.Show("例外エラー:" + Environment.NewLine + "原因を特定できませんでした。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                */
                #endregion
                #region getKeyword()
                /// <summary>
                /// ベースキーワードの復号化
                /// </summary>
                /// <param name="encryptedKeyword">暗号化されたベースキーワード</param>
                /// <returns>復号化されたベースキー</returns>
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
                /// ベースキーワードの暗号化
                /// </summary>
                /// <param name="encryptedKeyword">ベースキー</param>
                /// <returns>暗号化済みベースキー</returns>
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
                        // ファイルがなければ作成
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
                        //MessageBox.Show("例外エラー:" + Environment.NewLine + "セキュリティエラー", "SecurityException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw se;
                    }
                    catch (System.IO.IOException ioe)
                    {
                        SimpleLogger.WriteLine(ioe.Message);
                        //MessageBox.Show("例外エラー:" + Environment.NewLine + "入出力関係でエラーが発生しました。", "IOException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw ioe;
                    }
                    catch (UnauthorizedAccessException uae)
                    {
                        SimpleLogger.WriteLine(uae.Message);
                        //MessageBox.Show("例外エラー:" + Environment.NewLine + "ファイルが読み取り専用モードになっているか、書き込み権限がありません。", "UnauthorizedAccessException error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw uae;
                    }
                    catch (System.Exception e)
                    {
                        SimpleLogger.WriteLine(e.Message);
                        //MessageBox.Show("例外エラー:" + Environment.NewLine + "原因を特定できませんでした。", "Exceptional error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw e;
                    }

                    //return false;
                }
            }
        }
    }
}
