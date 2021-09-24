using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Graylog.Example
{
    public static class NLogTest
    {
        private static readonly Logger Log = LogManager.GetLogger("NLogTest");

        public static void Main()
        {
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

        private static void ThrowException()
        {
            throw new Exception("Exception example");
        }
    }
}
