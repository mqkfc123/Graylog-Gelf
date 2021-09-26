using Graylog.Core.Transports;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Graylog.Core
{

    public sealed class BufferedTransport : ITransport
    {
        private readonly Queue<GelfMessage> buffer = new Queue<GelfMessage>();
        private readonly ManualResetEvent stopEvent = new ManualResetEvent(false);

        public BufferedTransport(IGraylogGelfLogger logger, ITransport transport)
        {
            new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        try
                        {
                            if (buffer.Count <= 0)
                                continue;

                            GelfMessage message = buffer.Dequeue();

                            transport.Send(message);
                            Thread.Sleep(100);
                        }
                        catch (Exception exception)
                        {
                            logger.Error("Cannot send message", exception);
                        }
                    }
                }
                catch
                {
                    while (true)
                    {
                        try
                        {
                            if (buffer.Count <= 0)
                                continue;

                            GelfMessage message = buffer.Dequeue();
                            transport.Send(message);
                            Thread.Sleep(100);
                        }
                        catch (Exception exception)
                        {
                            transport.Close();
                            stopEvent.Set();
                            logger.Error("Cannot send message", exception);
                        }
                    }
                }
            })
            { IsBackground = true, Name = "Graylog Buffered Transport Thread" }.Start();
        }

        public void Send(GelfMessage message)
        {
            buffer.Enqueue(message);
        }

        public void Close()
        {
            stopEvent.WaitOne();
            stopEvent.Close();
        }
    }
}
