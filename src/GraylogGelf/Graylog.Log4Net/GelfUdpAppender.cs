using Graylog.Core;
using Graylog.Core.Encoders;
using Graylog.Core.Transports;
using Graylog.Core.Transports.Udp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Graylog.Log4Net
{
    public sealed class GelfUdpAppender : GelfAppenderBase
    {
        public GelfUdpAppender()
        {
            RemoteAddress = IPAddress.Loopback.ToString();
            RemotePort = 12201;
            MessageSize = 8096;
        }

        public int MessageSize { get; set; }

        public string RemoteAddress { get; set; }

        public int RemotePort { get; set; }

        protected override ITransport InitializeTransport(IGraylogGelfLogger logger)
        {
            var encoder = new CompositeEncoder(new GZipEncoder(), new ChunkingEncoder(new MessageBasedIdGenerator(), MessageSize.UdpMessageSize()));
            var configuration = new UdpTransportConfiguration
            {
                RemoteAddress = RemoteAddress,
                RemotePort = RemotePort
            };
            return new UdpTransport(configuration, encoder, new GelfMessageSerializer());
        }
    }
}
