using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationLog4NetTest.Logging
{
    public interface ILogger
    {
        string Method { get; }
        string ClassName { get; }
        string ClassFullName { get; }

        string ExceptionTypeName { get; }
        string ExceptionTypeFullName { get; }

        string Property1 { get; }
        string Property2 { get; }
        
        void Debug(object message, Exception exception, string property1 = "", string property2 = "");
        void Debug(object message, string property1 = "", string property2 = "");
        void DebugFormat(IFormatProvider provider, string format, params object[] args);
        void DebugFormat(string format, object arg0, object arg1, object arg2);
        void DebugFormat(string format, object arg0, object arg1);
        void DebugFormat(string format, object arg0);
        void DebugFormat(string format, params object[] args);
        void Error(object message, Exception exception, string property1 = "", string property2 = "");
        void Error(object message, string property1 = "", string property2 = "");
        void ErrorFormat(IFormatProvider provider, string format, params object[] args);
        void ErrorFormat(string format, object arg0, object arg1, object arg2);
        void ErrorFormat(string format, object arg0, object arg1);
        void ErrorFormat(string format, object arg0);
        void ErrorFormat(string format, params object[] args);
        void Fatal(object message, Exception exception, string property1 = "", string property2 = "");
        void Fatal(object message, string property1 = "", string property2 = "");
        void FatalFormat(IFormatProvider provider, string format, params object[] args);
        void FatalFormat(string format, object arg0, object arg1, object arg2);
        void FatalFormat(string format, object arg0, object arg1);
        void FatalFormat(string format, object arg0);
        void FatalFormat(string format, params object[] args);
        void Info(object message, Exception exception, string property1 = "", string property2 = "");
        void Info(object message, string property1 = "", string property2 = "");
        void InfoFormat(IFormatProvider provider, string format, params object[] args);
        void InfoFormat(string format, object arg0, object arg1, object arg2);
        void InfoFormat(string format, object arg0, object arg1);
        void InfoFormat(string format, object arg0);
        void InfoFormat(string format, params object[] args);
        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarnEnabled { get; }
        void Warn(object message, Exception exception, string property1 = "", string property2 = "");
        void Warn(object message, string property1 = "", string property2 = "");
        void WarnFormat(IFormatProvider provider, string format, params object[] args);
        void WarnFormat(string format, object arg0, object arg1, object arg2);
        void WarnFormat(string format, object arg0, object arg1);
        void WarnFormat(string format, object arg0);
        void WarnFormat(string format, params object[] args);
    }
}