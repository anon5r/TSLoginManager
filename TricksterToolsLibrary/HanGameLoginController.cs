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
            /// ハンゲーム用ログインコントローラ
            /// </summary>
            public class HanGameLoginController : LoginController
            {
                #region startGame()
                /// <summary>
                /// HanGame IDとパスワードを使用してゲームを起動します。
                /// ゲームクライアントのアップデートがある場合にはランチャーを起動します。
                /// </summary>
                /// <param name="id">HanGameID</param>
                /// <param name="password">アカウントパスワード</param>
                public new static void startGame(string id, string password)
                {

                    HanGameLoginController clsLoginCon = new LoginManager.HanGameLoginController();
                    clsLoginCon.initialize();
                    System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");
                    string url;
                    string param = "";
                    HttpWebResponse res;
                    string html;
                    
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


                    // パラメータ
                    param = @"strmemberid=" + ProgramController.UrlEncode(id, Encoding.GetEncoding("Shift_JIS"));
                    param += "&strpassword=" + ProgramController.UrlEncode(password, Encoding.GetEncoding("Shift_JIS"));
                    //param += "&chkIdSave=0";
                    param += "&chkSSLLogin=on";
                    param += "&nxtURL=" + ProgramController.UrlEncode("http://trickster.hangame.co.jp/lpindex.nhn", Encoding.UTF8);

                    try
                    {
                        /*
                         * 
                         * ハンゲームトリックスタートップページに接続
                         * 
                         */
                        url = "http://trickster.hangame.co.jp/";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.GET, null, 60000, null);


                        // レスポンスの取得と読み込み
                        res = clsLoginCon.getResponse();


                        /*
                         * 
                         * ハンゲームにログイン
                         * 
                         */
                        url = "https://id.hangame.co.jp/login.asp";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.POST, param, 5000, "http://trickster.hangame.jp/lpindex.nhn");


                        // レスポンスの取得と読み込み
                        res = clsLoginCon.getResponse();
                        
                        /*
                        // Cookie チェック停止 on 2009-05-31 0:06:20
                        if (res.Cookies["login"].Value == "null")
                        {
                            SimpleLogger.WriteLine("Failed to login hangame.co.jp.");
                            MessageBox.Show("ハンゲームにログインできませんでした。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clsLoginCon.ResponseClose();
                            clsLoginCon.initialize();
                            return;
                        }
                        */

                        if (res.Headers["loginstatus"] == "null")
                        {
                            SimpleLogger.WriteLine("failed to login hangame.co.jp.");
                            MessageBox.Show("ハンゲームにログインできませんでした。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clsLoginCon.ResponseClose();
                            clsLoginCon.initialize();
                            return;
                        }



                        /*
                         * 
                         * ハンゲームトリックスタートップページに接続
                         * 
                         */
                        url = "http://trickster.hangame.co.jp/lpindex.nhn";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.GET, null, 5000, "http://id.hangame.co.jp/login.asp");

                        // レスポンスの取得と読み込み
                        res = clsLoginCon.getResponse();


                        /*
                         * 
                         * ハンゲームからTricksterへ起動を投げる
                         * 
                         */
                        //url = "http://trickster.hangame.co.jp/startGame.nhn";
                        // 以下のURLに変更されたため変更
                        url = "http://trickster.hangame.co.jp/externalStartGame.nhn";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.GET, null, 5000, "http://trickster.hangame.co.jp/lpindex.nhn");

                        // レスポンスの取得と読み込み
                        res = clsLoginCon.getResponse();

                        html = clsLoginCon.getResponseStream();

                        /*
                         * 取得したHTMLからランチャー起動用URLを取得する
                         * window.open( 'https://ssl2.gcrest.com/hangame/hg_start_ts2.php?xxxxxxxxxxxxx', 'gamestart',"scrollbars=no,toolbar=no,channelmode=no,location=no,width=600,height=300,menubar=no");
                         */
                        //int seekStart = html.IndexOf("https://ssl2.gcrest.com");
                        //int seekEnd = html.IndexOf("'", html.IndexOf("https://ssl2.gcrest.com")) - seekStart;
                        //html.Substring(seekStart, seekEnd);
                        //string LauncherUrl = "https://ssl2.gcrest.com";
                        //SimpleLogger.WriteLine("LauncherURL: " + LauncherUrl);

                        // リダイレクトURL形式に変わったため以下の方法で対応
                        //string LauncherUrl = res.ResponseUri.AbsoluteUri;

                        if (res.Headers["Location"] == null || res.Headers["Location"].Length == 0)
                        {
                            SimpleLogger.WriteLine("failed to login hangame.co.jp.");
                            MessageBox.Show("ハンゲームにログインできませんでした。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clsLoginCon.ResponseClose();
                            clsLoginCon.initialize();
                            return;
                        }
                        string LauncherUrl = res.Headers["Location"];


                        /*
                         * ハンゲームdummy URL
                         */
                        /*
                        url = "http://trickster.hangame.co.jp/mymenuinfo.nhn?gid=TS";
                        req = (HttpWebRequest)HttpWebRequest.Create(url);
                        req.Method = "GET";
                        req.UserAgent = __USER_AGENT__;
                        req.Timeout = 5000; // タイムアウト5秒
                        //req.Headers.Add(HttpRequestHeader.Connection, "Keep-Alive");
                        req.Headers.Add(HttpRequestHeader.AcceptLanguage, "ja");
                        req.Referer = "http://trickster.hangame.co.jp/startGame.nhn";
                        req.KeepAlive = true;
                        req.AllowAutoRedirect = true;
                        req.CookieContainer = cc;
                        SimpleLogger.WriteLine("[ Request ] ----------------------------------------");
                        SimpleLogger.WriteLine("URL: " + url);
                        SimpleLogger.WriteLine("Method: " + req.Method);
                        SimpleLogger.WriteLine(req.Headers.ToString());
                        SimpleLogger.WriteLine("----------------------------------------------------");

                        // レスポンスの取得と読み込み
                        res = (HttpWebResponse)req.GetResponse();
                        SimpleLogger.WriteLine("[ Response ] ---------------------------------------");
                        SimpleLogger.WriteLine("Status: " + (int)res.StatusCode + " " + res.StatusCode.ToString());
                        SimpleLogger.WriteLine(res.Headers.ToString());
                        SimpleLogger.WriteLine("----------------------------------------------------");
                        */


                        /*
                         * 
                         * ランチャー起動
                         * 
                         */
                        //url = "https://ssl2.gcrest.com/hangame/hg_start_ts2.php";
                        url = LauncherUrl;
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.GET, null, 5000, "http://trickster.hangame.co.jp/startGame.nhn");

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
                            MessageBox.Show("認証エラー", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("認証エラー", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    catch (WebException we)
                    {
                        SimpleLogger.WriteLine(we.GetType().ToString() + Environment.NewLine + we.Message);
                        //MessageBox.Show("例外エラー:" + we.GetType().ToString() + Environment.NewLine + we.Message.ToString(), "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
