using Graylog.Core;
using NLog.Common;
using System;

namespace Graylog.NLog
{
    public sealed class VerboseLogger : IGraylogGelfLogger
    {
        public void Error(string message, Exception exception)
        {
            InternalLogger.Error(string.Format("{0} ---> {1}", message, exception));
        }

        public void Debug(string message)
        {
            InternalLogger.Debug(message);
        }
    }
}
