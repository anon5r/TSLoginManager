using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using Microsoft.Win32;
using TricksterTools.Library;
using TricksterTools.CommonXmlStructure;
using TricksterTools.API.Controller;
using TricksterTools.Debug;
using TricksterTools.Library.Xml.Settings;

namespace TricksterTools
{
    namespace Library
    {
        namespace LoginManager
        {
            /// <summary>
            /// 公式サイト用ログインコントローラ
            /// </summary>
            public class OfficialLoginController : LoginController
            {
                #region startGame()
                /// <summary>
                /// IDとパスワードを使用してゲームを起動します。
                /// ゲームクライアントのアップデートがある場合にはランチャーを起動します。
                /// </summary>
                /// <param name="id">アカウントID</param>
                /// <param name="password">アカウントパスワード</param>
                public new static void startGame(string id, string password)
                {
                    OfficialLoginController clsLoginCon = new LoginManager.OfficialLoginController();
                    clsLoginCon.initialize();
                    System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");
                    string url;
                    string param = "";
                    HttpWebResponse res;

                    // ネットワーク接続を試みる
                    while (!LoginController.isAliveNetwork())
                    {
                        SimpleLogger.WriteLine("does not connect network.");
                        DialogResult dgRes = MessageBox.Show("ネットワークに接続されていないため、処理を続行できません。", "Trickster Tools", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (dgRes == DialogResult.Cancel)
                        {
                            return;
                        }
                    }



                    param = @"code=23&login=0&id=" + ProgramController.UrlEncode(id, Encoding.GetEncoding("Shift_JIS")) + "&pass=" + ProgramController.UrlEncode(password, Encoding.UTF8);
                    
                    url = "https://ssl2.gcrest.com/mp/gate.php";

                    try
                    {
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.POST, param, 60000, "http://www.trickster.jp/trickster/index.html");

                        // レスポンスの取得と読み込み
                        res = clsLoginCon.getResponse();

                        if (res.ResponseUri.Host == "www.trickster.jp")
                        {
                            SimpleLogger.WriteLine("Failed to login. Response uri is 'www.trickster.jp'.");
                            MessageBox.Show("ログインできません。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clsLoginCon.ResponseClose();
                            clsLoginCon.initialize();
                            return;
                        }
                        if (res.Headers["Location"] == "/trickster/mp/session_error.php")
                        {
                            SimpleLogger.WriteLine("got session error.");
                            MessageBox.Show("セッションを取得できませんでした。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clsLoginCon.ResponseClose();
                            clsLoginCon.initialize();
                            return;
                        }


                        /**** ランチャー起動 ****/


                        url = "https://ssl2.gcrest.com/trickster/mp/start.php";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.GET, null, 5000, "https://ssl2.gcrest.com/mp/menu.php");

                        // レスポンスの取得と読み込み
                        res = clsLoginCon.getResponse();

                        // set-cookieが存在するか確認
                        int i = 0, max = 0, chk = 0;
                        max = res.Headers.Count;
                        while (i < max)
                        {
                            if (res.Headers.Keys[i] == "Set-Cookie")
                            {
                                chk = 1;
                                break;
                            }
                            i++;

                        }
                        if (chk < 1)
                        {
                            clsLoginCon.ResponseClose();
                            SimpleLogger.WriteLine("could not find 'Set-Cookies' in HTTP response header.");
                            MessageBox.Show("ログインできませんでした。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clsLoginCon.ResponseClose();
                            clsLoginCon.initialize();
                            return;
                        }

                        if (res.Headers["Set-Cookie"].IndexOf("TrickLaunch[ID]=") != -1)
                        {
                            clsLoginCon.parseCookies(res.Headers["Set-Cookie"]);
                            clsLoginCon.runGame();
                        }
                        else
                        {
                            SimpleLogger.WriteLine("could not find name of cookies 'TrickLaunch[ID]'");
                            MessageBox.Show("ログインできませんでした。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        

                    }
                    catch (HttpListenerException hle)
                    {
                        SimpleLogger.WriteLine(hle.Message);
                        //MessageBox.Show("例外エラー:" + Environment.NewLine + hle.Message.ToString(), "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        throw hle;
                    }
                    catch (WebException we)
                    {
                        SimpleLogger.WriteLine(we.Message);
                        //MessageBox.Show("例外エラー:" + Environment.NewLine + we.Message.ToString(), "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        throw we;
                    }
                    finally
                    {
                        clsLoginCon.ResponseClose();
                        clsLoginCon.initialize();
                    }

                }
                #endregion
            }
        }
    }
}
