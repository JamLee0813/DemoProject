using log4net;
using log4net.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace DataCenter.Common.Helper
{
    /// <summary>
    ///     Log 用来记录系统的日志,包括异常等.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// </summary>
        public static bool DebugMode = false;

        private static readonly ILog log;

        #region 构造函数

        /// <summary>
        ///     构造函数
        /// </summary>
        static Log()
        {
            var repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo("Log4net.config"));
            log = LogManager.GetLogger(repository.Name, "DemoProject");
        }

        #endregion 构造函数

        #region 公布的写日志函数

        /// <summary>
        ///     记录异常（在调试模式）
        /// </summary>
        /// <param name="exp"></param>
        public static void WriteDebugException(Exception exp)
        {
            WriteDebugException(exp, null);
        }

        /// <summary>
        ///     记录异常（在调试模式）
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="strFormat"></param>
        /// <param name="args"></param>
        public static void WriteDebugException(Exception exp, string strFormat, params object[] args)
        {
            try
            {
                Debug.Assert(exp != null);
                var sb = new StringBuilder();

                sb.AppendFormat("[{0}] DEBUG 找到 [{1}] 异常: {2}\r\n", DateTime.Now.ToString("HH:mm:ss"),
                    exp.GetType().Name, exp.Message);

                // 附加信息
                if (strFormat != null) sb.AppendFormat("    Annex  : {0}\r\n", string.Format(strFormat, args));

                // 调试信息
                sb.AppendFormat("    Source : {0}\r\n", exp.Source);
                sb.AppendFormat("    Time   : {0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.AppendFormat("    OS/VER : {0} {1}\r\n", Environment.OSVersion.Platform,
                    Environment.OSVersion.Version);
                sb.AppendFormat("    Thread : {0}\r\n", Thread.CurrentThread.Name);

                // 栈信息
                sb.AppendFormat("    Stack  : {0}\r\n", exp.StackTrace);

                // 内部异常, 最多5级
                var inner = exp.InnerException;
                for (var i = 0; i < 5 && inner != null; i++)
                {
                    // 显示内部异常
                    sb.AppendFormat("   ----- InnerException ---------------------------\r\n");
                    sb.AppendFormat("     ExceptionType: {0}\r\n", inner.GetType().Name);
                    sb.AppendFormat("     Message: {0}\r\n", inner.Message);
                    sb.AppendFormat("     Stack  : {0}\r\n", inner.StackTrace);

                    // 获取异常的内部异常
                    inner = inner.InnerException;
                }

                log.Error(sb.ToString());
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LOG ERROR: " + ex.Message);
            }
        }

        /// <summary>
        ///     记录一条日志信息。注意：在记录时该程序会自动在左侧增加一格日期。
        /// </summary>
        /// <param name="strLog">字符串的组成格式</param>
        public static void WriteDebugLine(string strLog)
        {
            try
            {
                Debug.Assert(strLog != null);
                log.Debug($"{strLog}");
            }
            catch (Exception exp)
            {
                Trace.WriteLine("LOG ERROR: " + exp.Message);
            }
        }

        /// <summary>
        ///     记录一条错误日志信息。注意：在记录时该程序会自动在左侧增加一格日期。
        /// </summary>
        /// <param name="strLog"></param>
        public static void WriteErrorLine(string strLog)
        {
            try
            {
                Debug.Assert(strLog != null);
                log.Error($"{strLog}");
            }
            catch (Exception exp)
            {
                Trace.WriteLine("LOG ERROR: " + exp.Message);
            }
        }

        /// <summary>
        ///     记录下该异常
        /// </summary>
        /// <param name="exp">需要记录的异常对象</param>
        public static void WriteException(Exception exp)
        {
            WriteException(exp, null);
        }

        /// <summary>
        ///     记录下该异常
        /// </summary>
        /// <param name="exp">异常对象</param>
        /// <param name="strFormat">附加信息格式字符串,如果不需要,则该参数为 null</param>
        /// <param name="args">参数</param>
        public static void WriteException(Exception exp, string strFormat, params object[] args)
        {
            try
            {
                Debug.Assert(exp != null);
                var sb = new StringBuilder();

                {
                    sb.AppendFormat("[{0}] 找到 [{1}] 异常: {2}\r\n", DateTime.Now.ToString("HH:mm:ss"), exp.GetType().Name,
                        exp.Message);

                    // 附加信息
                    if (strFormat != null) sb.AppendFormat("    Annex  : {0}\r\n", string.Format(strFormat, args));

                    // 调试信息
                    sb.AppendFormat("    Source : {0}\r\n", exp.Source);
                    sb.AppendFormat("    Time   : {0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sb.AppendFormat("    OS/VER : {0} {1}\r\n", Environment.OSVersion.Platform,
                        Environment.OSVersion.Version);
                    sb.AppendFormat("    Thread : {0}\r\n", Thread.CurrentThread.Name);

                    // 栈信息
                    sb.AppendFormat("    Stack  : {0}\r\n", exp.StackTrace);

                    // 内部异常, 最多5级
                    var inner = exp.InnerException;
                    for (var i = 0; i < 5 && inner != null; i++)
                    {
                        // 显示内部异常
                        sb.AppendFormat("   ----- InnerException ---------------------------\r\n");
                        sb.AppendFormat("     ExceptionType: {0}\r\n", inner.GetType().Name);
                        sb.AppendFormat("     Message: {0}\r\n", inner.Message);
                        sb.AppendFormat("     Stack  : {0}\r\n", inner.StackTrace);

                        // 获取异常的内部异常
                        inner = inner.InnerException;
                    }

                    log.Error(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LOG ERROR: " + ex.Message);
            }
        }

        /// <summary>
        ///     记录一条日志信息。注意：在记录时该程序会自动在左侧增加一格日期。
        /// </summary>
        /// <param name="strFormat">字符串的组成格式</param>
        /// <param name="args">参数</param>
        public static void WriteLine(string strFormat, params object[] args)
        {
            try
            {
                log.Info($"{string.Format(strFormat, args)}");
            }
            catch (Exception exp)
            {
                Trace.WriteLine("LOG ERROR: " + exp.Message);
            }
        }

        /// <summary>
        ///     记录一条日志信息。注意：在记录时该程序会自动在左侧增加一格日期。
        /// </summary>
        /// <param name="strLog">字符串的组成格式</param>
        public static void WriteLine(string strLog)
        {
            try
            {
                Debug.Assert(strLog != null);
                log.Info($"{strLog}");
            }
            catch (Exception exp)
            {
                Trace.WriteLine("LOG ERROR: " + exp.Message);
            }
        }

        /// <summary>
        ///     Dump 一个对象
        /// </summary>
        /// <param name="obj"></param>
        public static void WriteObject(object obj)
        {
            try
            {
                var sb = new StringBuilder();
                if (obj == null)
                {
                    sb.AppendLine("-- The object is null\r\n");
                }
                else
                {
                    sb.AppendFormat("[{0}] {1} has {2} property\r\n",
                        DateTime.Now, obj.GetType().Name, obj.GetType().GetProperties().Length);

                    var pis = obj.GetType().GetProperties();
                    var iMaxLength = 0;
                    foreach (var pi in pis)
                        if (pi.CanRead && !pi.IsSpecialName)
                            iMaxLength = Math.Max(pi.Name.Length, iMaxLength);

                    foreach (var pi in pis)
                        if (pi.CanRead && !pi.IsSpecialName)
                            sb.AppendFormat("    {0} - {1}\r\n", pi.Name.PadRight(iMaxLength, ' '),
                                pi.GetValue(obj, null));
                }
            }
            catch (Exception exp)
            {
                Trace.WriteLine("LOG ERROR: " + exp.Message);
            }
        }

        #endregion 公布的写日志函数
    }
}