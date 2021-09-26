using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Graylog.Core
{
    public static class LogUtils
    {
        static string temp = AppDomain.CurrentDomain.BaseDirectory;
        public static void AddLog(string value, string fileName = "graylog")
        {
            string logs = @"logs\" + string.Format("{0}\\{1}\\{2}\\", DateTime.Now.Year, DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Day.ToString().PadLeft(2, '0'));
            if (Directory.Exists(Path.Combine(temp, logs)) == false)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(temp, logs));
                directoryInfo.Create();
            }
            using (StreamWriter sw = File.AppendText(Path.Combine(temp, $"{logs}{fileName}-{DateTime.Now.ToString("yyyy-MM-dd")}.log")))
            {
                sw.WriteLine($"[{DateTime.Now}] {value}");
            }
        }
    }
}
