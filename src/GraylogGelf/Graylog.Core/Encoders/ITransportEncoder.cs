using System.Collections.Generic;

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
