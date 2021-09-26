using System;

namespace Graylog.Core
{
    public interface IGraylogGelfLogger
    {
        void Error(string message, Exception exception);
        void Debug(string message);
    }


    public sealed class SilentLogger : IGraylogGelfLogger
    {
        public void Error(string message, Exception exception)
        {
            LogUtils.AddLog(message + "  ex:" + exception.Message);
        }
        public void Debug(string message)
        {
            LogUtils.AddLog(message);
        }
    }

}
