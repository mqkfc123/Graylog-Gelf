﻿using Graylog.Core.Transports;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Graylog.Core
{

    public sealed class BufferedTransport : ITransport
    {
        private readonly BlockingCollection<GelfMessage> buffer = new BlockingCollection<GelfMessage>();
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent stopEvent = new ManualResetEvent(false);

        public BufferedTransport(IGraylogGelfLogger logger, ITransport transport)
        {
            new Thread(() =>
            {
                var cancellationToken = cancellationTokenSource.Token;
                try
                {
                    GelfMessage mesage;
                    while (buffer.TryTake(out mesage, -1, cancellationToken))
                    {
                        try
                        {
                            transport.Send(mesage);
                        }
                        catch (Exception exception)
                        {
                            logger.Error("Cannot send message", exception);
                        }
                    }
                }
                catch
                {
                    GelfMessage message;
                    while (buffer.TryTake(out message))
                    {
                        try
                        {
                            transport.Send(message);
                        }
                        catch (Exception exception)
                        {
                            logger.Error("Cannot send message", exception);
                        }
                    }
                }
                transport.Close();
                stopEvent.Set();
            })
            { IsBackground = true, Name = "Graylog Buffered Transport Thread" }.Start();
        }

        public void Send(GelfMessage message)
        {
            buffer.Add(message, cancellationTokenSource.Token);
        }

        public void Close()
        {
            buffer.CompleteAdding();
            cancellationTokenSource.Cancel();
            stopEvent.WaitOne();
            stopEvent.Dispose();
        }
    }
}
