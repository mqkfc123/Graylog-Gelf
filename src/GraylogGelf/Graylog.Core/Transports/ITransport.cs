using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graylog.Core.Transports
{
    /// <summary>
    /// 通用的传输接口
    /// </summary>
    public interface ITransport
    {
        /// <summary>
        /// 发送Gelf消息
        /// </summary>
        /// <param name="message"></param>
        void Send(GelfMessage message);
        /// <summary>
        /// 关闭
        /// </summary>
        void Close();

    }
}
