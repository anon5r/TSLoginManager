using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace TricksterTools
{
    namespace API
    {
        namespace DataStructure
        {
            public class Accounts
            {
                public class AccountProperties : IComparer

                {
                    /// <summary>
                    /// 項目
                    /// </summary>
                    public enum Item
                    {
                        ID,
                        Password,
                        Site,
                    }

                    public enum LoginSite
                    {
                        Official,
                        HanGame,
                        AtGames,
                        //Lievo,
                        Gamers1,
                    }

                    /// <summary>
                    /// ログインアカウントIDを設定または取得します。
                    /// </summary>
                    public string ID;
                    /// <summary>
                    /// ログインパスワードを設定または取得します。
                    /// </summary>
                    public string Password;
                    /// <summary>
                    /// ログイン先サイトを設定または取得します。
                    /// </summary>
                    public LoginSite Site;

                    /// <summary>
                    /// 並び替える方法を指定します。
                    /// </summary>
                    public SortOrder Order = SortOrder.Ascending;
                    /// <summary>
                    /// ソート方法
                    /// </summary>
                    public enum SortOrder
                    {
                        /// <summary>
                        /// 昇順
                        /// </summary>
                        Ascending,
                        /// <summary>
                        /// 降順
                        /// </summary>
                        Descending,
                    }

                    /// <summary>
                    /// 並び替える項目
                    /// </summary>
                    public SortKey Key = SortKey.ID;
                    /// <summary>
                    /// ソート対象となる項目
                    /// </summary>
                    public enum SortKey
                    {
                        /// <summary>
                        /// ID順
                        /// </summary>
                        ID,
                        /// <summary>
                        /// IDの長さ順
                        /// </summary>
                        IDLength,
                    }


                    /// <summary>
                    /// xがyより小さいときはマイナスの数、大きいときはプラスの数、
                    /// 同じときは0を返す。
                    /// </summary>
                    /// <param name="x">比較する値</param>
                    /// <param name="y">比較する値</param>
                    /// <returns></returns>
                    public int Compare(object x, object y)
                    {
                        int result = 0;
                        try{
                            AccountProperties cx = (AccountProperties)x;
                            AccountProperties cy = (AccountProperties)y;

                            if (Key == SortKey.ID)
                            {
                                // IDで並び替える
                                result = string.Compare(cx.ID, cy.ID);
                            }
                            else if (Key == SortKey.IDLength)
                            {
                                result = cx.ID.Length - cy.ID.Length;
                            }
                            else
                            {
                                result = 0;
                            }

                            // 降順のときは+-を逆転させる
                            if (Order == SortOrder.Descending)
                            {
                                result = -result;
                            }
                        }
                        catch(Exception)
                        {
                            // 型の指定が違う場合は比較しない
                            result = 0;
                        }

                        //結果を返す
                        return result;
                    }


                    /// <summary>
                    /// アカウント情報を文字列で返します。
                    /// </summary>
                    /// <returns>AccountProperties内の文字列情報</returns>
                    public override string ToString()
                    {
                        return "AccountProperties"
                            + "{"
                            + "ID: " + ID + ", "
                            + "Password: " + Password + ", "
                            + "Site: " + Site
                            + "}";
                    }
                }
            }
        }
    }
}
