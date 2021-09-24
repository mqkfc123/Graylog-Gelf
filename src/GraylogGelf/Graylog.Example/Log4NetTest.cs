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
        /// dddd 20
        /// </summary>
        public static void Mains()
        {
            ConfigureLogging();
            var cancelationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) => cancelationTokenSource.Cancel();
            while (!cancelationTokenSource.IsCancellationRequested)
            {
                Log.Info("I'm alive");

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
