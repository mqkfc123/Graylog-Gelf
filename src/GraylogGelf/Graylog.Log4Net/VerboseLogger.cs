using Graylog.Core;
using log4net.Util;
using System;

namespace Graylog.Log4Net
{
    public sealed class VerboseLogger : IGraylogGelfLogger
    {
        public void Error(string message, Exception exception)
        {
            LogLog.Error(message, exception);
        }

        public void Debug(string message)
        {
            LogLog.Debug(message);
        }
    }
}
