
namespace Graylog.Core.Encoders
{
    /// <summary>
    /// 消息id生成器
    /// </summary>
    public interface IChunkedMessageIdGenerator
    {
        /// <summary>
        /// 生成id
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        byte[] GenerateId(byte[] message);
    }
}
