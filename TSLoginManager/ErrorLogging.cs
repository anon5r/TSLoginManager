using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Windows.Forms;

namespace TSLoginManager
{
    class ErrorLogging
    {

        private static readonly object LockObj = new object();
        public static bool _endingFlag = false;        // 終了フラグ
        public static bool TraceFlag = false;

        public static void TraceOut(string Message)
        {
            TraceOut(TraceFlag, Message);
        }

        public static void TraceOut(bool OutputFlag, String Message)
        {
            lock (LockObj)
            {
                if (!OutputFlag) return;
                
                DateTime now = DateTime.Now;
                string fileName = String.Format("TSLoginManagerTrace-{0:0000}{1:00}{2:00}-{3:00}{4:00}{5:00}.log", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName))
                {
                    writer.WriteLine("**** TraceOut: {0} ****", DateTime.Now.ToString());
                    writer.WriteLine("このファイルの内容を anon.jp@gmail.com まで送っていただけると助かります。");
                    writer.WriteLine();
                    writer.WriteLine("動作環境:");
                    writer.WriteLine("   オペレーティング システム: {0}", Environment.OSVersion.VersionString);
                    writer.WriteLine("   共通言語ランタイム          : {0}", Environment.Version.ToString());
                    writer.WriteLine("   TSLoginManagerのバージョン    : {0}", Application.ProductVersion.ToString());
                    writer.WriteLine(Message);
                    writer.WriteLine();
                }
            }
        }

        // エラー内容をバッファに書き出し
        // 注意：最終的にファイル出力されるエラーログに記録されるため次の情報は書き出さない
        // 文頭メッセージ、権限、動作環境
        // Dataプロパティにある終了許可フラグのパースもここで行う

        public static string OutputException(Exception ex, String buffer, ref bool IsTerminatePermission)
        {

            StringBuilder buf = new StringBuilder();

            buf.AppendFormat("例外 {0}: {1}", ex.GetType().FullName, ex.Message);
            buf.AppendLine();
            if(ex.Data != null )
            {
                bool needHeader = true;
                foreach (System.Collections.DictionaryEntry dt in ex.Data)
                {
                    if (needHeader)
                    {
                        buf.AppendLine();
                        buf.AppendLine("-------Extra Information-------");
                        needHeader = false;
                    }
                    buf.AppendFormat("{0}  :  {1}", dt.Key, dt.Value);
                    buf.AppendLine();
                    if( dt.Key.Equals("IsTerminatePermission"))
                    {
                        IsTerminatePermission = Convert.ToBoolean(dt.Value);
                    }
                }
                if ( needHeader == false )
                {
                    buf.AppendLine("-----End Extra Information-----");
                }
            }
            buf.AppendLine(ex.StackTrace);
            buf.AppendLine();

            // InnerExceptionが存在する場合書き出す
            Exception _ex = ex.InnerException;
            int nesting = 0;
            while (_ex != null)
            {
                buf.AppendFormat("-----InnerException[{0}]-----" + Environment.NewLine, nesting);
                buf.AppendLine();
                buf.AppendFormat("例外 {0}: {1}", _ex.GetType().FullName, _ex.Message);
                buf.AppendLine();
                if (_ex.Data != null)
                {
                    bool needHeader = true;

                    foreach (System.Collections.DictionaryEntry dt in _ex.Data)
                    {
                        if (needHeader)
                        {
                            buf.AppendLine();
                            buf.AppendLine("-------Extra Information-------");
                            needHeader = false;
                        }
                        buf.AppendFormat("{0}  :  {1}", dt.Key, dt.Value);
                        if (dt.Key.Equals("IsTerminatePermission") )
                        {
                            IsTerminatePermission = Convert.ToBoolean(dt.Value);
                        }
                    }
                    if (!needHeader)
                    {
                        buf.AppendLine("-----End Extra Information-----");
                    }
                }
                buf.AppendLine(_ex.StackTrace);
                buf.AppendLine();
                nesting += 1;
                _ex = _ex.InnerException;
            }
            buffer = buf.ToString();
            return buffer;
        }

        public static void OutputException(Exception ex)
        {
            lock (LockObj){
                bool IsTerminatePermission = true;

                DialogResult diagres = MessageBox.Show(
                    String.Format("例外エラーが発生しました。{0}エラーログを保存し、開きますか{0}{0}はい　　　・・・ログを保存して開きます。{0}いいえ　　・・・ログを保存しますが開きません。{0}キャンセル・・・ログを保存せず終了します。", Environment.NewLine)
                    , "例外エラー発生", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error
                    );
                if (diagres == DialogResult.Cancel)
                {
                    // 終了時直前の状態を保存するハンドラをコールする
                    //Program.ExitHandler();    // どこで発生したエラーか分からないし、見送り・・・
                    Application.Exit();
                    return;
                }


                DateTime now = DateTime.Now;
                string fileName = String.Format("TSLoginManager-{0:0000}{1:00}{2:00}-{3:00}{4:00}{5:00}.log", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName))
                {
                    WindowsIdentity ident = WindowsIdentity.GetCurrent();
                    WindowsPrincipal princ = new WindowsPrincipal(ident);

                    writer.WriteLine("**** エラー ログ: {0} ****", DateTime.Now.ToString());
                    writer.WriteLine("このファイルの内容を anon.jp@gmail.com まで送っていただけると助かります。");
                    writer.WriteLine("またはサポートフォーラム( http://trickster.anoncom.net/forum/viewforum.php?f=4 )までお知らせください。");
                    // 権限書き出し
                    writer.WriteLine("Administrator権限：" + princ.IsInRole(WindowsBuiltInRole.Administrator).ToString());
                    writer.WriteLine("Users権限：" + princ.IsInRole(WindowsBuiltInRole.User).ToString());
                    writer.WriteLine();
                    // OSVersion,AppVersion書き出し
                    writer.WriteLine("動作環境:");
                    writer.WriteLine("   オペレーティング システム: {0}", Environment.OSVersion.VersionString);
                    writer.WriteLine("   共通言語ランタイム : {0}", Environment.Version.ToString());
                    writer.WriteLine("   TSLoginManagerのバージョン : {0}", Application.ProductVersion.ToString());

                    string buffer = null;
                    writer.Write(OutputException(ex, buffer, ref IsTerminatePermission));
                    writer.Flush();
                }


                switch (diagres)
                {
                    case DialogResult.Yes:
                        System.Diagnostics.Process.Start(fileName);
                        //return false;
                        break;
                    case DialogResult.No:
                        //return false;
                        break;
                    //case DialogResult.Cancel:
                        //return IsTerminatePermission;
                }
            }
        }
    }
}
