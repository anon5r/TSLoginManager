using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using TricksterTools.API.Controller;
using TricksterTools.API.DataStructure;
using TricksterTools.Debug;


namespace TricksterTools.Plugins
{
    class AccountAPI
    {
        private static ArrayList AccountData;

        public AccountAPI(ArrayList accounts)
        {
            AccountData = accounts;
        }

        /// <summary>
        /// アカウント情報を追加します。
        /// </summary>
        /// <param name="props">アカウントプロパティ</param>
        public void AddAccount(Accounts.AccountProperties props)
        {
            AccountData.Add(props);
        }
        /// <summary>
        /// アカウントIDからアカウント情報を取得します。
        /// </summary>
        /// <param name="id">アカウントID</param>
        /// <returns>アカウント情報</returns>
        public Accounts.AccountProperties GetAccount(string id, Accounts.AccountProperties.LoginSite site)
        {
            return this.ContainAccount(id, site);
        }
        /// <summary>
        /// アカウント情報を一覧から削除します。
        /// </summary>
        /// <param name="id">アカウントID</param>
        /// <param name="site">サイトID</param>
        public void DeleteAccount(string id, Accounts.AccountProperties.LoginSite site)
        {
            Accounts.AccountProperties props = this.ContainAccount(id, site);
            if (props.Equals(null))
            {
                SimpleLogger.WriteLine("ID: \"" + id + "\" is already deleted or nothing.");
                MessageBox.Show("ID: \" " + id + " \"は存在しません。", "TricksterTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AccountData.Remove(props);
            SimpleLogger.WriteLine("ID: \"" + id + "\" is deleted.");
        }

        /// <summary>
        /// 現在保持しているアカウント情報を全て削除します。
        /// </summary>
        public void ClearAccounts()
        {
            AccountData.Clear();
        }
        /// <summary>
        /// 現在保持しているアカウント情報を全て返します。
        /// </summary>
        /// <returns>配列化されたアカウント情報</returns>
        public ArrayList ListAccounts()
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
        public Accounts.AccountProperties ContainAccount(string ID, Accounts.AccountProperties.LoginSite site)
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
        public bool IsExistAccount(string ID, Accounts.AccountProperties.LoginSite site)
        {
            Accounts.AccountProperties tmp = this.ContainAccount(ID, site);
            if (tmp == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
