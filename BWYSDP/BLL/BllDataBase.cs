using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.BLL.BUS;

namespace BWYSDP.BLL
{
    public class BllDataBase:BllBus
    {
        #region 构造函数
        public BllDataBase()
        {
 
        }
        #endregion
        public Dictionary<string, string> GetAccount()
        {
           return  (Dictionary <string ,string >) this.ExecuteDalMethod("TestFunc", "GetAccount");
        }
    }
}
