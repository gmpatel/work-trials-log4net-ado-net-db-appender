using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ConsoleApplicationLog4NetTest.Logging
{
    public class Logger : ILogger
    {
        private static volatile ILogger instance;
        private static ILog logger = null;

        private static readonly object ConsturctorLock = new Object();

        private readonly object syncLock = new Object();

        private Logger()
        {
            if (logger == null)
            {
                logger =
                    LogManager
                    .GetLogger("Log4Net");

                log4net
                    .Config
                    .XmlConfigurator
                    .Configure();
            }
        }

        public static ILogger Instance()
        {
            if (instance == null)
            {
                lock (ConsturctorLock)
                {
                    if (instance == null) instance = new Logger();

                    GlobalContext.Properties["Method"] = string.Empty;
                    GlobalContext.Properties["ClassName"] = string.Empty;
                    GlobalContext.Properties["ClassFullName"] = string.Empty;

                    GlobalContext.Properties["ExceptionTypeName"] = string.Empty;
                    GlobalContext.Properties["ExceptionTypeFullName"] = string.Empty;

                    GlobalContext.Properties["Property1"] = string.Empty;
                    GlobalContext.Properties["Property2"] = string.Empty;
                }
            }

            return instance;
        }

        private static string GetCallerInfo(bool inclide = false)
        {
            var stackFrames = new StackTrace().GetFrames();

            if (stackFrames != null)
            {
                var method = stackFrames[2].GetMethod();

                if (method.DeclaringType != null)
                {
                    GlobalContext.Properties["Method"] = method.Name;
                    GlobalContext.Properties["ClassName"] = method.DeclaringType.Name;
                    GlobalContext.Properties["ClassFullName"] = method.DeclaringType.FullName;

                    if (inclide)
                    {
                        return string.Format("Method = {0}(), Class Name = {1}, Full Name = {2}, Message =", method.Name, method.DeclaringType.Name,
                            method.DeclaringType.FullName);                        
                    }
                }
            }

            return string.Empty;
        }

        public string Property1
        {
            get { return GlobalContext.Properties["Property1"].ToString(); }
            private set { GlobalContext.Properties["Property1"] = value; }
        }

        public string Property2
        {
            get { return GlobalContext.Properties["Property2"].ToString(); }
            private set { GlobalContext.Properties["Property2"] = value; }
        }

        public string ExceptionTypeName
        {
            get { return GlobalContext.Properties["ExceptionTypeName"].ToString(); }
            private set { GlobalContext.Properties["ExceptionTypeName"] = value; }
        }

        public string ExceptionTypeFullName
        {
            get { return GlobalContext.Properties["ExceptionTypeFullName"].ToString(); }
            private set { GlobalContext.Properties["ExceptionTypeFullName"] = value; }
        }

        public string Method
        {
            get { return GlobalContext.Properties["Method"].ToString(); }
            private set { GlobalContext.Properties["Method"] = value; }
        }

        public string ClassName
        {
            get { return GlobalContext.Properties["ClassName"].ToString(); }
            private set { GlobalContext.Properties["ClassName"] = value; }
        }

        public string ClassFullName
        {
            get { return GlobalContext.Properties["ClassFullName"].ToString(); }
            private set { GlobalContext.Properties["ClassFullName"] = value; }
        }

        public void Debug(object message, Exception exception, string property1 = "", string property2 = "")
        {
            lock (syncLock)
            {
                Property1 = property1;
                Property2 = property2;

                if (exception != null)
                {
                    ExceptionTypeName = exception.GetType().Name;
                    ExceptionTypeFullName = exception.GetType().FullName;
                }

                if (IsDebugEnabled)
                    logger.Debug(string.Format("{0} {1}", GetCallerInfo(), message).Trim(), exception);

                ExceptionTypeName = string.Empty;
                ExceptionTypeFullName = string.Empty;
            }
        }

        public void Debug(object message, string property1 = "", string property2 = "")
        {
            Property1 = property1;
            Property2 = property2;

            lock (syncLock)
            {
                if (IsDebugEnabled)
                    logger.Debug(string.Format("{0} {1}", GetCallerInfo(), message).Trim());
            }
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            lock (syncLock)
            {
                if (IsDebugEnabled)
                    logger.DebugFormat(provider, format, args);
            }
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            lock (syncLock)
            {
                if (IsDebugEnabled)
                    logger.DebugFormat(format, arg0, arg1, arg2);
            }
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            lock (syncLock)
            {
                if (IsDebugEnabled)
                    logger.DebugFormat(format, arg0, arg1);
            }
        }

        public void DebugFormat(string format, object arg0)
        {
            lock (syncLock)
            {
                if (IsDebugEnabled)
                    logger.DebugFormat(format, arg0);
            }
        }

        public void DebugFormat(string format, params object[] args)
        {
            lock (syncLock)
            {
                if (IsDebugEnabled)
                    logger.DebugFormat(format, args);
            }
        }

        public void Error(object message, Exception exception, string property1 = "", string property2 = "")
        {
            lock (syncLock)
            {
                Property1 = property1;
                Property2 = property2;

                if (exception != null)
                {
                    ExceptionTypeName = exception.GetType().Name;
                    ExceptionTypeFullName = exception.GetType().FullName;
                }

                if (IsErrorEnabled)
                    logger.Error(string.Format("{0} {1}", GetCallerInfo(), message).Trim(), exception);

                ExceptionTypeName = string.Empty;
                ExceptionTypeFullName = string.Empty;
            }
        }

        public void Error(object message, string property1 = "", string property2 = "")
        {
            lock (syncLock)
            {
                Property1 = property1;
                Property2 = property2;

                if (IsErrorEnabled)
                    logger.Error(string.Format("{0} {1}", GetCallerInfo(), message).Trim());
            }
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            lock (syncLock)
            {
                if (IsErrorEnabled)
                    logger.ErrorFormat(provider, format, args);
            }
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            lock (syncLock)
            {
                if (IsErrorEnabled)
                    logger.ErrorFormat(format, arg0, arg1, arg2);
            }
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            lock (syncLock)
            {
                if (IsErrorEnabled)
                    logger.ErrorFormat(format, arg0, arg1);
            }
        }

        public void ErrorFormat(string format, object arg0)
        {
            lock (syncLock)
            {
                if (IsErrorEnabled)
                    logger.ErrorFormat(format, arg0);
            }
        }

        public void ErrorFormat(string format, params object[] args)
        {
            lock (syncLock)
            {
                if (IsErrorEnabled)
                    logger.ErrorFormat(format, args);
            }
        }

        public void Fatal(object message, Exception exception, string property1 = "", string property2 = "")
        {
            lock (syncLock)
            {
                Property1 = property1;
                Property2 = property2;

                if (exception != null)
                {
                    ExceptionTypeName = exception.GetType().Name;
                    ExceptionTypeFullName = exception.GetType().FullName;
                }

                if (IsFatalEnabled)
                    logger.Fatal(string.Format("{0} {1}", GetCallerInfo(), message).Trim(), exception);

                ExceptionTypeName = string.Empty;
                ExceptionTypeFullName = string.Empty;
            }
        }

        public void Fatal(object message, string property1 = "", string property2 = "")
        {
            lock (syncLock)
            {
                Property1 = property1;
                Property2 = property2;

                if (IsFatalEnabled)
                    logger.Fatal(string.Format("{0} {1}", GetCallerInfo(), message).Trim());
            }
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            lock (syncLock)
            {
                if (IsFatalEnabled)
                    logger.FatalFormat(provider, format, args);
            }
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            lock (syncLock)
            {
                if (IsFatalEnabled)
                    logger.FatalFormat(format, arg0, arg1, arg2);
            }
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            lock (syncLock)
            {
                if (IsFatalEnabled)
                    logger.FatalFormat(format, arg0, arg1);
            }
        }

        public void FatalFormat(string format, object arg0)
        {
            lock (syncLock)
            {
                if (IsFatalEnabled)
                    logger.FatalFormat(format, arg0);
            }
        }

        public void FatalFormat(string format, params object[] args)
        {
            lock (syncLock)
            {
                if (IsFatalEnabled)
                    logger.FatalFormat(format, args);
            }
        }

        public void Info(object message, Exception exception, string property1 = "", string property2 = "")
        {
            lock (syncLock)
            {
                Property1 = property1;
                Property2 = property2;

                if (exception != null)
                {
                    ExceptionTypeName = exception.GetType().Name;
                    ExceptionTypeFullName = exception.GetType().FullName;
                }

                if (IsInfoEnabled)
                    logger.Info(string.Format("{0} {1}", GetCallerInfo(), message).Trim(), exception);

                ExceptionTypeName = string.Empty;
                ExceptionTypeFullName = string.Empty;
            }
        }

        public void Info(object message, string property1 = "", string property2 = "")
        {
            lock (syncLock)
            {
                Property1 = property1;
                Property2 = property2;

                if (IsInfoEnabled)
                    logger.Info(string.Format("{0} {1}", GetCallerInfo(), message).Trim());
            }
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            lock (syncLock)
            {
                if (IsInfoEnabled)
                    logger.InfoFormat(provider, format, args);
            }
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            lock (syncLock)
            {
                if (IsInfoEnabled)
                    logger.InfoFormat(format, arg0, arg1, arg2);
            }
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            lock (syncLock)
            {
                if (IsInfoEnabled)
                    logger.InfoFormat(format, arg0, arg1);
            }
        }

        public void InfoFormat(string format, object arg0)
        {
            lock (syncLock)
            {
                if (IsInfoEnabled)
                    logger.InfoFormat(format, arg0);
            }
        }

        public void InfoFormat(string format, params object[] args)
        {
            lock (syncLock)
            {
                if (IsInfoEnabled)
                    logger.InfoFormat(format, args);
            }
        }

        public bool IsDebugEnabled
        {
            get { return logger.IsDebugEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return logger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return logger.IsFatalEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return logger.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return logger.IsWarnEnabled; }
        }

        public void Warn(object message, Exception exception, string property1 = "", string property2 = "")
        {
            lock (syncLock)
            {
                if (exception != null)
                {
                    Property1 = property1;
                    Property2 = property2;

                    ExceptionTypeName = exception.GetType().Name;
                    ExceptionTypeFullName = exception.GetType().FullName;
                }

                if (IsWarnEnabled)
                    logger.Warn(string.Format("{0} {1}", GetCallerInfo(), message).Trim(), exception);

                ExceptionTypeName = string.Empty;
                ExceptionTypeFullName = string.Empty;
            }
        }

        public void Warn(object message, string property1 = "", string property2 = "")
        {
            lock (syncLock)
            {
                Property1 = property1;
                Property2 = property2;

                if (IsWarnEnabled)
                    logger.Warn(string.Format("{0} {1}", GetCallerInfo(), message).Trim());
            }
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            lock (syncLock)
            {
                if (IsWarnEnabled)
                    logger.WarnFormat(provider, format, args);
            }
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            lock (syncLock)
            {
                if (IsWarnEnabled)
                    logger.WarnFormat(format, arg0, arg1, arg2);
            }
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            lock (syncLock)
            {
                if (IsWarnEnabled)
                    logger.WarnFormat(format, arg0, arg1);
            }
        }

        public void WarnFormat(string format, object arg0)
        {
            lock (syncLock)
            {
                if (IsWarnEnabled)
                    logger.WarnFormat(format, arg0);
            }
        }

        public void WarnFormat(string format, params object[] args)
        {
            lock (syncLock)
            {
                if (IsWarnEnabled)
                    logger.WarnFormat(format, args);
            }
        }
    }
}