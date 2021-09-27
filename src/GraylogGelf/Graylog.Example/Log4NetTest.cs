using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Graylog.Example
{
    public static class Log4NetTest
    {
        private static readonly ILog Log = LogManager.GetLogger("Log4NetTest");

        /// <summary>
        /// net2.0
        /// </summary>
        public static void Main()
        {
            ConfigureLogging();
            while (true)
            {
                Log.Info("I'm alive a");
                try
                {
                    ThrowException();
                }
                catch (Exception ex)
                {
                    Log.Error("Descriptive message example", ex);
                }
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
        }

        private static void ConfigureLogging()
        {
            var fileInfo = new FileInfo("log4net.config");
            if (!fileInfo.Exists)
                throw new Exception();
            XmlConfigurator.Configure(fileInfo);
        }

        private static void ThrowException()
        {
            throw new Exception("Exception example");
        }

    }
}
