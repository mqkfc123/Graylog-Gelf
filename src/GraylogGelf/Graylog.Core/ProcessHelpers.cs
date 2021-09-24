using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Graylog.Core
{
    /// <summary>
    /// 进程名称
    /// </summary>
    public static class ProcessHelpers
    {
        public static string ProcessName
        {
            get
            {
                var process = Process.GetCurrentProcess();
                return process.ProcessName;
            }
        }
    }
}
