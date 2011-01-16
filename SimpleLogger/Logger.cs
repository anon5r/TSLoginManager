using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TricksterTools.Debug
{

    /// <summary>
    /// ログ書き込み処理を行う
    /// </summary>
    public static class SimpleLogger
    {

        internal static StreamWriter _writer;

        internal static string _outputLogFile = "";

        internal static bool _enable = false;

        /// <summary>
        /// Writerを作成し、ログ書き込みに準備を行う
        /// </summary>
        private static void CreateWriter()
        {
            if (_outputLogFile.Length == 0)
            {
                _outputLogFile = Environment.CurrentDirectory + "\\logs\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            }
            // ディレクトリがない場合作成する
            if (Directory.Exists(Path.GetDirectoryName(_outputLogFile)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_outputLogFile));
            }

            
            // ライター作成
            try
            {
                SimpleLogger._writer = new StreamWriter(_outputLogFile, true);
                SimpleLogger._writer.AutoFlush = true;
            }
            catch (UnauthorizedAccessException e)
            {
                throw e;
            }
            catch (ArgumentNullException e)
            {
                throw e;
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (DirectoryNotFoundException e)
            {
                throw e;
            }
            catch (PathTooLongException e)
            {
                throw e;
            }
            catch (IOException e)
            {
                throw e;
            }
            catch (System.Security.SecurityException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// ファイル名を設定する
        /// </summary>
        /// <param name="filename"></param>
        public static void setFilename(string filename)
        {
            SimpleLogger._outputLogFile = filename;
        }

        /// <summary>
        /// ログ出力設定の有効可否を設定します
        /// </summary>
        /// <param name="value">true: 有効, false: 無効</param>
        public static void Enable(bool value)
        {
            SimpleLogger._enable = value;
        }

        /// <summary>
        /// ログ出力用フォーマットでタイムスタンプを返します
        /// </summary>
        /// <returns>ログ出力用フォーマットのタイムスタンプ</returns>
        private static string getTimestamp()
        {
            return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] ";
        }


        public static string getLogFilename()
        {
            return SimpleLogger._outputLogFile;
        }

        /// <summary>
        /// Writerをクローズします
        /// </summary>
        public static void Close()
        {
            if (SimpleLogger._writer != null && SimpleLogger._writer.GetType() == typeof(StreamWriter))
            {
                SimpleLogger._writer.Close();
                SimpleLogger._writer = null;
            }
        }





        public static void WriteLine()
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine();
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(bool value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            //SimpleLogger.writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(char value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(char[] buffer)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(buffer);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(decimal value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(double value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(float value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(int value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(long value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(object value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(uint value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(ulong value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(ushort value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(short value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(string value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(string format, object arg0)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(format, arg0);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(string format, params object[] arg)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(format, arg);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(char[] buffer, int index, int count)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(buffer, index, count);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(SimpleLogger.getTimestamp());
            SimpleLogger._writer.WriteLine(format, arg0, arg1, arg2);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }


        public static void Write(bool value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(char value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(char[] buffer)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(buffer);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(decimal value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(double value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(float value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(int value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(long value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(object value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(uint value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(ulong value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(ushort value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(short value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(string value)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(value);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(string format, object arg0)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(format, arg0);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(string format, params object[] arg)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(format, arg);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(char[] buffer, int index, int count)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(buffer, index, count);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
        public static void Write(string format, object arg0, object arg1, object arg2)
        {
            if (!SimpleLogger._enable) return;
            SimpleLogger.CreateWriter();
            SimpleLogger._writer.Write(format, arg0, arg1, arg2);
            SimpleLogger._writer.Flush();
            SimpleLogger.Close();
        }
    }
}
