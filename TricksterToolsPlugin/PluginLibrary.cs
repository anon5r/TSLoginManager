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
        /// �A�J�E���g����ǉ����܂��B
        /// </summary>
        /// <param name="props">�A�J�E���g�v���p�e�B</param>
        public void AddAccount(Accounts.AccountProperties props)
        {
            AccountData.Add(props);
        }
        /// <summary>
        /// �A�J�E���gID����A�J�E���g�����擾���܂��B
        /// </summary>
        /// <param name="id">�A�J�E���gID</param>
        /// <returns>�A�J�E���g���</returns>
        public Accounts.AccountProperties GetAccount(string id, Accounts.AccountProperties.LoginSite site)
        {
            return this.ContainAccount(id, site);
        }
        /// <summary>
        /// �A�J�E���g�����ꗗ����폜���܂��B
        /// </summary>
        /// <param name="id">�A�J�E���gID</param>
        /// <param name="site">�T�C�gID</param>
        public void DeleteAccount(string id, Accounts.AccountProperties.LoginSite site)
        {
            Accounts.AccountProperties props = this.ContainAccount(id, site);
            if (props.Equals(null))
            {
                SimpleLogger.WriteLine("ID: \"" + id + "\" is already deleted or nothing.");
                MessageBox.Show("ID: \" " + id + " \"�͑��݂��܂���B", "TricksterTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AccountData.Remove(props);
            SimpleLogger.WriteLine("ID: \"" + id + "\" is deleted.");
        }

        /// <summary>
        /// ���ݕێ����Ă���A�J�E���g����S�č폜���܂��B
        /// </summary>
        public void ClearAccounts()
        {
            AccountData.Clear();
        }
        /// <summary>
        /// ���ݕێ����Ă���A�J�E���g����S�ĕԂ��܂��B
        /// </summary>
        /// <returns>�z�񉻂��ꂽ�A�J�E���g���</returns>
        public ArrayList ListAccounts()
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
        /// ID�����݂̈ꗗ���ɑ��݂��邩�m�F���܂��B
        /// </summary>
        /// <param name="ID">�����Ώۂ�ID</param>
        /// <param name="site">�����Ώۂ̃T�C�g</param>
        /// <returns>���������ꍇ��true, ������Ȃ������ꍇ��false��Ԃ��܂��B</returns>
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
