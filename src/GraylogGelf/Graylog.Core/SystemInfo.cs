using System;
using System.Collections.Generic;
using System.Management;
using System.Net;
using System.Text;

namespace Graylog.Core
{
    public class SystemInfo
    {
        /// <summary>
        /// 获取本机所有ip地址
        /// </summary>
        /// <param name="netType">"InterNetwork":ipv4地址，"InterNetworkV6":ipv6地址</param>
        /// <returns>ip地址集合</returns>
        public static List<string> GetLocalIpAddress(string netType)
        {
            List<string> IPList = new List<string>();
            if (netType == string.Empty)
            {
                string hostName = Dns.GetHostName();                    //获取主机名称  
                IPAddress[] addresses = Dns.GetHostAddresses(hostName); //解析主机IP地址 
                for (int i = 0; i < addresses.Length; i++)
                {
                    IPList.Add(addresses[i].ToString());
                }
            }
            else
            {
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                IPList = GetNetInfo();
            }
            return IPList;
        }

        /// <summary>
        /// 获取网络相关信息
        /// </summary>
        /// <returns></returns>
        public static List<string> GetNetInfo()
        {
            List<string> netList = new List<string>();
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection nics = mc.GetInstances();
                foreach (ManagementObject nic in nics)
                {
                    if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                    {
                        string ip = (nic["IPAddress"] as String[])[0];//IP地址    
                        string ipsubnet = (nic["IPSubnet"] as String[])[0];//子网掩码     
                        string ipgateway = nic["DefaultIPGateway"] == null ? "" : (nic["DefaultIPGateway"] as String[])[0];//默认网关  
                        string mac = nic["MacAddress"].ToString();//Mac地址
                        string[] dns = (string[])(nic["DNSServerSearchOrder"]);//DNS地址(首选、备用)
                        netList.Add(ip);
                        netList.Add(ipsubnet);
                        netList.Add(ipgateway);
                        netList.Add(mac);
                        if (dns != null && dns.Length > 0)
                        {
                            netList.Add(dns[0]);
                        }
                        break;
                    }
                }
                return netList;
            }
            catch (Exception ex)
            {
                netList.Add("127.0.0.1");
                return netList;
            }
        }


    }
}
