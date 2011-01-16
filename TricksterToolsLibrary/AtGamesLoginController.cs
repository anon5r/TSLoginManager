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
            /// �A�b�g�Q�[���Y���O�C���R���g���[��
            /// </summary>
            public class AtGamesLoginController : LoginController
            {
                #region startGame()
                /// <summary>
                /// �A�b�g�Q�[���Y ID�ƃp�X���[�h���g�p���ăQ�[�����N�����܂��B
                /// �Q�[���N���C�A���g�̃A�b�v�f�[�g������ꍇ�ɂ̓����`���[���N�����܂��B
                /// </summary>
                /// <param name="id">AtGamesID</param>
                /// <param name="password">�A�J�E���g�p�X���[�h</param>
                public new static void startGame(string id, string password)
                {
                    AtGamesLoginController clsLoginCon = new LoginManager.AtGamesLoginController();
                    clsLoginCon.initialize();

                    System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");
                    string url;
                    string param = "";
                    HttpWebResponse res;
                    string html;


                    // �l�b�g���[�N�ڑ������݂�
                    while (!LoginController.isAliveNetwork())
                    {
                        SimpleLogger.WriteLine("does not connect network.");
                        DialogResult dgRes = MessageBox.Show("�l�b�g���[�N�ɐڑ�����Ă��Ȃ����߁A�����𑱍s�ł��܂���B", "Trickster Tools", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (dgRes == DialogResult.Cancel)
                        {
                            return;
                        }
                    }


                    // �p�����[�^
                    param = @"accountID=" + ProgramController.UrlEncode(id, Encoding.GetEncoding("Shift_JIS"));
                    param += "&password=" + ProgramController.UrlEncode(password, Encoding.GetEncoding("Shift_JIS"));

                    try
                    {

                        /*
                         * 
                         * �A�b�g�Q�[���Y�g�b�v�y�[�W�ɐڑ�
                         * 
                         */
                        url = "http://www.atgames.jp/atgames/top.html";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.GET, null, 60000, null);

                        // ���X�|���X�̎擾�Ɠǂݍ���
                        res = clsLoginCon.getResponse();


                        /*
                         * 
                         * �A�b�g�Q�[���Y�Ƀ��O�C��
                         * 
                         */
                        url = "http://www.atgames.jp/atgames/login.do";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.POST, param, 5000, "http://www.atgames.jp/atgames/top.html");

                        // ���X�|���X�̎擾�Ɠǂݍ���
                        res = clsLoginCon.getResponse();

                        if (res.Headers["Set-Cookie"].IndexOf("atgames=", 0) < 0)
                        {
                            SimpleLogger.WriteLine("Failed to login www.atgames.jp.");
                            MessageBox.Show("�A�b�g�Q�[���Y�Ƀ��O�C���ł��܂���ł����B", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clsLoginCon.ResponseClose();
                            clsLoginCon.initialize();
                            return;
                        }

                        string param0 = res.Headers["Set-Cookie"].ToString();
                        string[] tmps1 = param0.Split(';');
                        foreach (string tmp in tmps1)
                        {
                            string[] tmps2 = tmp.Trim().Split(',');
                            if (tmps2[0] != "Path=/") { continue; }
                            if (tmps2[1].Trim().IndexOf('=') < 0) { continue; }

                            tmps2 = tmps2[1].Trim().Split('=');

                            if (!System.Text.RegularExpressions.Regex.IsMatch(tmps2[0], @"^breadcrumb_id[0-9]+$"))
                            {
                                continue;
                            }
                            if (tmps2[1] == "" || tmps2[1] == "0") { continue; }
                            param0 = tmps2[1];
                            break;
                        }


                        /*
                         * 
                         * �A�b�g�Q�[���Y�g���b�N�X�^�[�g�b�v�y�[�W�ɐڑ�
                         * 
                         */
                        url = "http://www.atgames.jp/atgames/html/game/mo_mmo/trick/index.html";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.GET, null, 5000, "http://www.atgames.jp/atgames/login.do");
                        

                        // ���X�|���X�̎擾�Ɠǂݍ���
                        res = clsLoginCon.getResponse();
                        html = clsLoginCon.getResponseStream();

                        /*
                         * ���̃��N�G�X�g�ւ̃p�����[�^����
                         */

                        int seekStart, seekEnd;
                        string seekStartString, seekEndString;


                        seekStartString = "startGame( true, '" + param0 + "', '";
                        seekEndString = "', 'entry.html' );setExec( false );}";
                        seekStart = html.IndexOf(seekStartString) + seekStartString.Length;
                        seekEnd = html.IndexOf(seekEndString, seekStart);
                        //seekEnd = seekEnd - seekStart;
                        string param1 = html.Substring(seekStart, (seekEnd - seekStart));
                        // "onClick=\"if ( !isExec() ) {setExec( true );startGame( true, '5485393', '1003', 'entry.html' );setExec( false );}\"";
                        // if ( !isExec() ) {setExec( true );startGame( true, '5402834', '1003', 'entry.html' );setExec( false );}

                        Random rnd = new Random();
                        int c_id = rnd.Next(9999);
                        param = @"callCount=1";
                        param += "&c0-scriptName=ContentLink";
                        param += "&c0-methodName=isRegist";
                        param += "&c0-id=" + (c_id.ToString() + "_" + DateTime.Now.Ticks.ToString());
                        param += "&c0-param0=string:" + param0;
                        param += "&c0-param1=string:" + param1;
                        param += "&xml=true";

                        /*
                         * 
                         * �A�b�g�Q�[���Y����Trickster�֋N���𓊂���i�o�^�m�F�H�j
                         * 
                         */
                        url = "http://www.atgames.jp/atgames/dwr/exec/ContentLink.isRegist.dwr";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.POST, param, 5000, "http://www.atgames.jp/atgames/html/game/mo_mmo/trick/index.html#head");
                        res = clsLoginCon.getResponse();

                        html = clsLoginCon.getResponseStream();

                        if (html.IndexOf("var s0=true") < 0)
                        {
                            SimpleLogger.WriteLine("maybe you should not registry \"play trickster\" in \"atgames.jp\".");
                            MessageBox.Show("�A�b�g�Q�[���Y���Ńg���b�N�X�^�[�̗��p�o�^���s���Ă��Ȃ��悤�ł��B", "TricksterTools", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            clsLoginCon.ResponseClose();
                            clsLoginCon.initialize();
                            return;
                        }



                        rnd = new Random();
                        c_id = rnd.Next(9999);
                        param = @"callCount=1";
                        param += "&c0-scriptName=ContentLink";
                        param += "&c0-methodName=bootGame";
                        param += "&c0-id=" + (c_id.ToString() + "_" + DateTime.Now.Ticks.ToString());
                        param += "&c0-param0=string:" + param0;
                        param += "&c0-param1=string:" + param1;
                        param += "&xml=true";
                        

                        /*
                         * 
                         * �A�b�g�Q�[���Y����Trickster�֋N���𓊂���i�Q�[���̋N���j
                         * 
                         */
                        url = "http://www.atgames.jp/atgames/dwr/exec/ContentLink.bootGame.dwr";
                        clsLoginCon.doRequest(url, LoginController.RequestMethod.POST, param, 5000, "http://www.atgames.jp/atgames/html/game/mo_mmo/trick/index.html#head");
                        res = clsLoginCon.getResponse();

                        html = clsLoginCon.getResponseStream();


                        /*
                         * �擾����HTML���烉���`���[�N���pURL���擾����
                         * window.open( 'https://ssl2.gcrest.com/hangame/hg_start_ts2.php?xxxxxxxxxxxxx', 'gamestart',"scrollbars=no,toolbar=no,channelmode=no,location=no,width=600,height=300,menubar=no");
                         */
                        seekStart = html.IndexOf("https://ssl2.gcrest.com");
                        seekEnd = html.IndexOf("\"", html.IndexOf("https://ssl2.gcrest.com")) - seekStart;
                        string LauncherUrl = html.Substring(seekStart, seekEnd);
                        //SimpleLogger.WriteLine("LauncherURL: " + LauncherUrl);

                        /*
                         * 
                         * �����`���[�N��
                         * 
                         */
                        url = LauncherUrl;

                        clsLoginCon.doRequest(url, LoginController.RequestMethod.GET, null, 5000, "http://www.atgames.jp/atgames/html/game/mo_mmo/trick/index.html#head");
                        

                        // ���X�|���X�̎擾�Ɠǂݍ���
                        res = clsLoginCon.getResponse();

                        // set-cookie�����݂��邩�m�F
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
                            res.Close();
                            SimpleLogger.WriteLine("could not find 'Set-Cookies' in HTTP response header.");
                            MessageBox.Show("�F�؃G���[", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clsLoginCon.ResponseClose();
                            clsLoginCon.initialize();
                            return;
                        }

                        if (res.Headers["Set-Cookie"].IndexOf("TrickLaunch[ID]=") != -1)
                        {


                            /*
                             * 
                             * �����`���[�N��
                             * 
                             */
                            clsLoginCon.parseCookies(res.Headers["Set-Cookie"]);
                            clsLoginCon.runGame();

                        }
                        else
                        {
                            SimpleLogger.WriteLine("could not find name of cookies 'TrickLaunch[ID]'");
                            MessageBox.Show("�F�؃G���[", "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        res.Close();
                    }
                    catch (WebException we)
                    {
                        SimpleLogger.WriteLine(we.GetType().ToString() + Environment.NewLine + we.Message);
                        //MessageBox.Show("��O�G���[:" + we.GetType().ToString() + Environment.NewLine + we.Message.ToString(), "Trickster Tools", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
