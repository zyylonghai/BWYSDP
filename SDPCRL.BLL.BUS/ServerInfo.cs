using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWYResFactory;

namespace SDPCRL.BLL.BUS
{
    public static class ServerInfo
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public static string IPAddress { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public static int Point { get; set; }
        /// <summary>
        /// DAL服务名
        /// </summary>
        public static string DalServerName { get { return ResFactory.ResManager.GetResByKey("DALServerName"); } }
        /// <summary>
        /// TCP/HTTP链接
        /// </summary>
        public static string ConnectType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static string ServerKey { get; set; }
    }
}
