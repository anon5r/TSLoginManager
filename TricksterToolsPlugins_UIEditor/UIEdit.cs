using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using TricksterTools.Debug;

namespace TricksterTools.Plugins.UIEditor
{

    public class UIEdit
    {


        /// <summary>
        /// インストール先パスを取得
        /// </summary>
        /// <param name="void"></param>
        /// <returns>インストール先パス</returns>
        public static string getInstallPath()
        {
            string rKeyName;
            // 操作するレジストリ・キーの名前
            if (isx64())
            {
                rKeyName = @"SOFTWARE\WOW6432Node\NTreev\Trickster_GCrest";
            }
            else
            {
                rKeyName = @"SOFTWARE\NTreev\Trickster_GCrest";
            }
            // 取得処理を行う対象となるレジストリの値の名前
            string rGetValueName = "FullPath";

            SimpleLogger.WriteLine("get client installed path...");
            // レジストリの取得
            try
            {
                // レジストリ・キーのパスを指定してレジストリを開く
                Microsoft.Win32.RegistryKey rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(rKeyName);

                // レジストリの値を取得
                string location = (string)rKey.GetValue(rGetValueName);

                // 開いたレジストリ・キーを閉じる
                rKey.Close();

                // 取得したレジストリの値return
                return location.Substring(0, (location.Length - @"\Splash.exe".Length));
            }
            catch (NullReferenceException)
            {
                SimpleLogger.WriteLine("failed to get client installed path.");
                // レジストリ・キーまたは値が存在しない
                return @"C:\Games\Trickster";
            }
        }

        /// <summary>
        /// x64環境での動作か判定します。
        /// </summary>
        /// <returns>64bit OSの場合はtrue</returns>
        public static bool isx64()
        {
            // 現在のプラットフォームでのポインタまたはハンドルのサイズ (バイト)。
            // このプロパティの値は 32 ビット プラットフォームでは 4、64 ビット プラットフォームでは 8 です。
            return (IntPtr.Size == 8);
        }

        public static void generateChatUI(int width, int height)
        {
            // ファイルパスを指定
            String filePath = getInstallPath() + @"\data\UI_nori\ChatUI.ucf";

            /*
             * ; 表示枠サイズ
             * iViewDimX	= iStatViewDimX + 2
             * iViewDimY	= 22（固定値/不変）
             * 
             * ; 通常モード時の文字入力枠
             * iEditBoxDimX	= iStatViewDimX + 74
             * iEditBoxDimY	= 18（固定値/不変）
             * iEditBoxCoordX	= 58（固定値/不変/左辺を0としてみた時の横の位置）
             * iEditBoxCoordY	= 3（固定値/不変/底辺を0としてみた時の高さの位置）
             * 
             * ; whisperモード時の文字入力枠
             * iEditBoxMinDimX	= iStatViewDimX - iEditBoxMinCoordX - 16
             * iEditBoxMinDimY	= 18（固定値/不変）
             * iEditBoxMinCoordX	= ((iStatViewDimX - (iComboBoxDimX + iComboBoxCoordX + 2)) - iStatViewDimX) * -1（/左辺を0としてみた時の横の位置/右下の点）
             * iEditBoxMinCoordY	= 3（固定値/不変/底辺を0としてみた時の高さの位置/右下の点）
             * 
             * ; whisperモード時のコンボボックスサイズ
             * iComboBoxDimX	= コンボボックスサイズの横幅（最低116）
             * iComboBoxDimY	= 18（固定値/不変）
             * iComboBoxCoordX	= 58（固定値/不変）
             * iComboBoxCoordY	= 3（固定値/不変）
             * 
             * ; エモボタンの位置
             * iEmoticonBtnCoordX	= 5（固定値/不変）
             * iEmoticonBtnCoordY	= 5（固定値/不変）
             * ; メモボタンの位置
             * iMemoBtnCoordX		= 22（固定値/不変）
             * iMemoBtnCoordY		= 5（固定値/不変）
             * ; 囁きボタンの位置
             * iWhisperBtnCoordX	= 40（固定値/不変）
             * iWhisperBtnCoordY	= 5（固定値/不変）
             * ; 閉じるボタンの位置
             * iCloseBtnCoordX		= iStatViewDimX - 15
             * iCloseBtnCoordY		= 6（固定値/不変）
             * 
             * iStatViewDimX	= チャット履歴表示の横幅
             * iStatViewDimY	= 78（固定値/不変）
             * 
             * iStatViewMaxDimX	= iStatViewDimX
             * iStatViewMaxDimY	= チャット履歴表示の高さ
             * 
             * iModeButtonDimX	= 65（固定値/不変）
             * iModeButtonDimY	= 18（固定値/不変）
             * 
             * iDummyModeButtonDimX	= iStatViewDimX - (iModeButtonDimX * 4)
             * iDummyModeButtonDimY	= 18（固定値/不変）
             */

            ChatUI chat = new ChatUI(width, height);


            StringBuilder builder = new StringBuilder();
            builder.AppendLine("iViewDimX = " + chat.iViewDimX);
            builder.AppendLine("iViewDimY = " + chat.iViewDimY);
            builder.AppendLine();
            builder.AppendLine("iEditBoxDimX = " + chat.iEditBoxDimX);
            builder.AppendLine("iEditBoxDimY = " + chat.iEditBoxDimY);
            builder.AppendLine("iEditBoxCoordX = " + chat.iEditBoxCoordX);
            builder.AppendLine("iEditBoxCoordY = " + chat.iEditBoxCoordY);
            builder.AppendLine("iEditBoxMinDimX = " + chat.iEditBoxMinDimX);
            builder.AppendLine("iEditBoxMinDimY = " + chat.iEditBoxMinDimY);
            builder.AppendLine("iEditBoxMinCoordX = " + chat.iEditBoxMinCoordX);
            builder.AppendLine("iEditBoxMinCoordY = " + chat.iEditBoxMinCoordY);
            builder.AppendLine();
            builder.AppendLine("iComboBoxDimX = " + chat.iComboBoxDimX);
            builder.AppendLine("iComboBoxDimY = " + chat.iComboBoxDimY);
            builder.AppendLine("iComboBoxCoordX = " + chat.iComboBoxCoordX);
            builder.AppendLine("iComboBoxCoordY = " + chat.iComboBoxCoordY);
            builder.AppendLine();
            builder.AppendLine("iEmoticonBtnCoordX = " + chat.iEmoticonBtnCoordX);
            builder.AppendLine("iEmoticonBtnCoordY = " + chat.iEmoticonBtnCoordY);
            builder.AppendLine("iMemoBtnCoordX  = " + chat.iMemoBtnCoordX);
            builder.AppendLine("iMemoBtnCoordY  = " + chat.iMemoBtnCoordY);
            builder.AppendLine("iWhisperBtnCoordX = " + chat.iWhisperBtnCoordX);
            builder.AppendLine("iWhisperBtnCoordY = " + chat.iWhisperBtnCoordY);
            builder.AppendLine("iCloseBtnCoordX  = " + chat.iCloseBtnCoordX);
            builder.AppendLine("iCloseBtnCoordY  = " + chat.iCloseBtnCoordY);
            builder.AppendLine();
            builder.AppendLine("iStatViewDimX = " + chat.iStatViewDimX);
            builder.AppendLine("iStatViewDimY = " + chat.iStatViewDimY);
            builder.AppendLine();
            builder.AppendLine("iStatViewMaxDimX = " + chat.iStatViewMaxDimX);
            builder.AppendLine("iStatViewMaxDimY = " + chat.iStatViewMaxDimY);
            builder.AppendLine();
            builder.AppendLine("iModeButtonDimX = " + chat.iModeButtonDimX);
            builder.AppendLine("iModeButtonDimY = " + chat.iModeButtonDimY);
            builder.AppendLine();
            builder.AppendLine("iDummyModeButtonDimX = " + chat.iDummyModeButtonDimX);
            builder.AppendLine("iDummyModeButtonDimY = " + chat.iDummyModeButtonDimY);


            SimpleLogger.WriteLine(builder.ToString());
            try
            {
                StreamWriter fst = new StreamWriter(filePath, false, Encoding.Default);
                fst.Write(builder.ToString());
                fst.Close();
            }
            catch (IOException ioe)
            {
                MessageBox.Show("設定ファイルの書き込みに失敗しました。", "UIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException uae)
            {
                MessageBox.Show("設定ファイルへの書き込み権限がありません。設定は保存されていません。", "UIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("セキュリティ設定により設定ファイルへのアクセスが行えませんでした。設定は保存されていません。", "UIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static int[] loadChatUISize()
        {
            // ファイルパスを指定
            String filePath = getInstallPath() + @"\data\UI_nori\ChatUI.ucf";

            /*
             * iStatViewDimX	= チャット履歴表示の横幅
             * iStatViewMaxDimY	= チャット履歴表示の高さ
             */
            int width = 325;
            int height = 478;
            if (File.Exists(filePath))
            {
                using (StreamReader fst = new StreamReader(filePath, Encoding.Default))
                {
                    int pos;
                    while (fst.EndOfStream == false)
                    {
                        String line = fst.ReadLine();
                        if (line.Trim().Length == 0) continue;

                        try
                        {
                            pos = line.IndexOf("iStatViewDimX = ", 0);
                            if (pos >= 0)
                            {
                                width = Int16.Parse(line.Substring("iStatViewDimX = ".Length).Trim());
                                continue;
                            }
                        }
                        catch (ArgumentOutOfRangeException aore)
                        {
                        }
                        try
                        {
                            pos = line.IndexOf("iStatViewMaxDimY = ", 0);
                            if (pos >= 0)
                            {
                                height = Int16.Parse(line.Substring("iStatViewMaxDimY = ".Length).Trim());
                                continue;
                            }
                        }
                        catch (ArgumentOutOfRangeException aore)
                        {
                        }
                    }
                    fst.Close();
                }
            }
            int[] size = { width, height };
            SimpleLogger.WriteLine("size: [ {0:D}, {1:D} ]", width, height);
            return size;
        }

        /// <summary>
        /// プラグイン設定から読み込んで ChatUI を生成する
        /// </summary>
        /// <param name="config"></param>
        public static void generateChatUI(ConfigUIEditor config)
        {
            ConfigChatUI chatUI = config.chatUI;
            generateChatUI(chatUI.Width, chatUI.Height);
        }
    }
}
