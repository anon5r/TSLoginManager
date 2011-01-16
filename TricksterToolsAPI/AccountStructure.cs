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
                    /// ����
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
                    /// ���O�C���A�J�E���gID��ݒ�܂��͎擾���܂��B
                    /// </summary>
                    public string ID;
                    /// <summary>
                    /// ���O�C���p�X���[�h��ݒ�܂��͎擾���܂��B
                    /// </summary>
                    public string Password;
                    /// <summary>
                    /// ���O�C����T�C�g��ݒ�܂��͎擾���܂��B
                    /// </summary>
                    public LoginSite Site;

                    /// <summary>
                    /// ���ёւ�����@���w�肵�܂��B
                    /// </summary>
                    public SortOrder Order = SortOrder.Ascending;
                    /// <summary>
                    /// �\�[�g���@
                    /// </summary>
                    public enum SortOrder
                    {
                        /// <summary>
                        /// ����
                        /// </summary>
                        Ascending,
                        /// <summary>
                        /// �~��
                        /// </summary>
                        Descending,
                    }

                    /// <summary>
                    /// ���ёւ��鍀��
                    /// </summary>
                    public SortKey Key = SortKey.ID;
                    /// <summary>
                    /// �\�[�g�ΏۂƂȂ鍀��
                    /// </summary>
                    public enum SortKey
                    {
                        /// <summary>
                        /// ID��
                        /// </summary>
                        ID,
                        /// <summary>
                        /// ID�̒�����
                        /// </summary>
                        IDLength,
                    }


                    /// <summary>
                    /// x��y��菬�����Ƃ��̓}�C�i�X�̐��A�傫���Ƃ��̓v���X�̐��A
                    /// �����Ƃ���0��Ԃ��B
                    /// </summary>
                    /// <param name="x">��r����l</param>
                    /// <param name="y">��r����l</param>
                    /// <returns></returns>
                    public int Compare(object x, object y)
                    {
                        int result = 0;
                        try{
                            AccountProperties cx = (AccountProperties)x;
                            AccountProperties cy = (AccountProperties)y;

                            if (Key == SortKey.ID)
                            {
                                // ID�ŕ��ёւ���
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

                            // �~���̂Ƃ���+-���t�]������
                            if (Order == SortOrder.Descending)
                            {
                                result = -result;
                            }
                        }
                        catch(Exception)
                        {
                            // �^�̎w�肪�Ⴄ�ꍇ�͔�r���Ȃ�
                            result = 0;
                        }

                        //���ʂ�Ԃ�
                        return result;
                    }


                    /// <summary>
                    /// �A�J�E���g���𕶎���ŕԂ��܂��B
                    /// </summary>
                    /// <returns>AccountProperties���̕�������</returns>
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
