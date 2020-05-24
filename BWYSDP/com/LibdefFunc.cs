using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWYSDP.com
{
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
