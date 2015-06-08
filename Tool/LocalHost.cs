using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;

namespace Tool
{
    /// <summary>
    /// 获取本机信息
    /// </summary>
    public class LocalHost
    {
        private string name;
        private string ip;
        private string mac;
        /// <summary>
        /// 获取机器IP信息
        /// </summary>
        public string Ip
        {
            get { return ip; }

        }
        /// <summary>
        /// 获取机器物理地址
        /// </summary>
        public string Mac
        {
            get { return mac; }

        }
        /// <summary>
        /// 获取机器主机名
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        public LocalHost()
        {
            //this.Name = Dns.GetHostName();
            this.getIpAddress();
            this.GetMacAddress();
        }
        /// <summary>
        /// 获取机器IP方法
        /// </summary>
        /// <returns></returns>
        private void getIpAddress()
        {
            try
            {
                this.name = Dns.GetHostName();
                //IP地址  
                //System.Net.IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;这个过时  
                System.Net.IPAddress[] addressList = Dns.GetHostEntry(this.name).AddressList;
                for (int i = 0; i < addressList.Length; i++)
                {
                    if (addressList[i].ToString().IndexOf('.') > 0)
                    {
                        this.ip = addressList[i].ToString();
                    }

                }
            }
            catch
            {
                this.ip = "";
            }

        }
        /// <summary>
        /// 获取物理地址
        /// </summary>
        /// <returns></returns>
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);
        private void GetMacAddress()
        {
            try
            {
                string strClientIP = this.ip;
                Int32 ldest = inet_addr(strClientIP); //目的地的ip 
                Int32 lhost = inet_addr("");   //本地服务器的ip 
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");


                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }

                string mac_dest = "";

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
                this.mac = mac_dest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
