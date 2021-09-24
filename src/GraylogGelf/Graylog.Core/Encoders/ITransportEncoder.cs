using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graylog.Core.Encoders
{
    /// <summary>
    /// 编码
    /// </summary>
    public interface ITransportEncoder
    {
        IEnumerable<byte[]> Encode(byte[] bytes);
    }
}
