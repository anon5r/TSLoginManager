using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Diagnostics;
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
            /// Lievo用ログインコントローラ
            /// </summary>
            public class LievoLoginController : LoginController
            {
                #region startGame()
                /// <summary>
                /// Lievo IDとパスワードを使用してゲームを起動します。
                /// ゲームクライアントのアップデートがある場合にはランチャーを起動します。
                /// </summary>
                /// <param name="id">LievoID</param>
                /// <param name="password">アカウントパスワード</param>
                public new static void startGame(string id, string password)
                {
                    LievoLoginController clsLoginCon = new LievoLoginController();
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
                    param = @"fUserID=" + ProgramController.UrlEncode(id, Encoding.GetEncoding("Shift_JIS"));
                    param += "&fUserPW=" + ProgramController.UrlEncode(password, Encoding.GetEncoding("Shift_JIS"));

                    try
                    {


                        /*
                         * 
                         * Lievoトリックスタートップページに接続
                         * 
                         */
                        url = "http://www.lievo.jp/contents/trickster/index.php";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.GET, null, 60000, null);

                        // レスポンスの取得と読み込み
                        res = clsLoginCon.getResponse();


                        /*
                         * 
                         * Lievoにログイン
                         * 
                         */
                        url = "https://www.lievo.jp/contents/trickster/mall/process/login.php";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.POST, param, 5000, "http://www.lievo.jp/contents/trickster/index.php");

                        // レスポンスの取得と読み込み
                        res = clsLoginCon.getResponse();


                        if (res.ResponseUri.AbsoluteUri == "http://www.lievo.jp/trickster/mall/error.asp")
                        {
                            SimpleLogger.WriteLine("Failed to login lievo.jp.");
                            MessageBox.Show("Lievo トリックスター にログインできませんでした。", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            clsLoginCon.ResponseClose();
                            clsLoginCon.initialize();
                            return;
                        }


                        /*
                         * 
                         * Lievoトリックスタートップページに接続
                         * 
                         */
                        url = "https://www.lievo.jp/contents/trickster/";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.POST, null, 5000, "https://www.lievo.jp/contents/trickster/mail/process/login.php");

                        
                        /*
                         * 
                         * LievoからTricksterへ起動を投げる
                         * 
                         */
                        url = "https://www.lievo.jp/contents/trickster/gameloader/gamestart.php";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.GET, null, 5000, "https://www.lievo.jp/contents/trickster/");

                        // レスポンスの取得と読み込み
                        res = clsLoginCon.getResponse();

                        html = clsLoginCon.getResponseStream();

                        /*
                         * 取得したHTMLからランチャー起動用IDとキーを取得する
                         * <html xmlns="http://www.w3.org/1999/xhtml">
                         * 
                         * <head>
                         * <meta http-equiv="Content-Type" content="text/html; charset=Shift-JIS" />
                         * <title>トリックスター0 -ラブ-起動</title>
                         * <script language="JScript" type="text/javascript"><!--
                         * function execGame() {
                         * 
                         * 
                         * 
                         * var objActiveX = document.activeXObj
                         * var dd = new Date();
                         * objActiveX.loader.UA = 'LV:0000000';		←★ クライアント起動時のID
                         * objActiveX.loader.PA = '0000000000';		←★ クライアント起動時のパスワード
                         * objActiveX.loader.DS = 'ee2d0ed0b6360caeb8c28201fbf8bcd4';
                         * objActiveX.loader.go();
                         * 
                         * 
                         * window.setTimeout("closeWin()",500);
                         * }
                         * 
                         * function closeWin() {
                         *  window.close();
                         * }
                         * --></script>
                         * 
                         * </head>
                         * <body background="./img/bg.jpg" onload="execGame()">
                         * <br />
                         * 
                         * 
                         * <form name="activeXObj" action=""> 
                         * <object id="loader" width="0" height="0"
                         *   CLASSID="CLSID:D85BD1B8-0000-4C5B-80D1-04AF514D200A"
                         *   codebase="https://www.lievo.jp/trickster/trickloader.cab#Version=1,0,0,001">
                         * </object>
                         * </form>
                         * 
                         * </body>
                         * </html>
                         * 
                         */
                        int seekStart, seekEnd;
                        string seekStartString, seekEndString;
                        string startID;
                        string startKey;

                        seekStartString = "objActiveX.loader.UA = '";
                        seekEndString = "';";
                        seekStart = html.IndexOf(seekStartString) + seekStartString.Length;
                        seekEnd = html.IndexOf(seekEndString, seekStart) - seekStart;
                        
                        startID = html.Substring(seekStart, seekEnd);

                        seekStartString = "objActiveX.loader.PA = '";
                        seekStart = html.IndexOf(seekStartString) + seekStartString.Length;
                        seekEnd = html.IndexOf(seekEndString, seekStart) - seekStart;
                        
                        startKey = html.Substring(seekStart, seekEnd);


                        /*
                         * 
                         * ランチャー起動
                         * 
                         */
                        clsLoginCon.runGame(startID, startKey);

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
