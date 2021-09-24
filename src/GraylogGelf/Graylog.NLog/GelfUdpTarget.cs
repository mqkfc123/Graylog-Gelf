using Graylog.Core;
using Graylog.Core.Encoders;
using Graylog.Core.Transports;
using Graylog.Core.Transports.Udp;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Graylog.NLog
{
    [Target("GelfUdp")]
    public sealed class GelfUdpTarget : GelfTargetBase
    {
        public GelfUdpTarget()
        {
            RemoteAddress = IPAddress.Loopback.ToString();
            RemotePort = 12201;
            MessageSize = 8096;
        }

        public string RemoteAddress { get; set; }

        public int RemotePort { get; set; }

        public int MessageSize { get; set; }

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