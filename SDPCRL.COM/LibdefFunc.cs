using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    /// <summary>
    /// 自定义系统函数
    /// </summary>
    public class LibdefFunc
    {
        public string FuncNm { get; set; }

        public string FuncDesc { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", FuncDesc, FuncNm);
        }
    }
}
