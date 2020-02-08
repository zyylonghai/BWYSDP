using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    [Serializable]
    public class LibClientInfo
    {
        //public string AccoutId { get; set; }
        public Language Language { get; set; }
        public string SessionId { get; set; }
        public string IP { get; set; }
        public string ClientNm { get; set; }
    }
}
