using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.CORE
{
    public class IdentityHelp
    {
        public IdentityHelp()
        { }
        /// <summary>
        /// 根据用户id生成Tick
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string GenerateTick(string userid)
        {
            string tick = string.Empty;
            string md5 = DM5Help.Md5Encrypt(userid);
            foreach (char c in md5)
            {
                tick += ((Int32)c).ToString();
            }
            return tick;
        }
        /// <summary>
        /// 比较tick是否属于当前用户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public static bool CompareTick(string userid,string tick)
        {
            string newtick = GenerateTick(userid);
            return newtick == tick;
        }
    }

    public class IdentityCredential
    {
        public string CertificateID { get; set; }

        public string UserNm { get; set; }

        public bool HasAdminRole { get; set; }
    }
}
