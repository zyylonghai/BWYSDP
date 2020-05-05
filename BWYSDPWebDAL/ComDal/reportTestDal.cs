using SDPCRL.COM;
using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComDal
{
    [LibProg("reportTest")]
    public class reportTestDal : ComDal
    {
        public override DataTable RptSearchByPage(string dsid, string tbnm, string[] fields, string[] sumaryFields, string groupfields, List<LibSearchCondition> conds, int pageindex, int pagesize)
        {
            return base.RptSearchByPage(dsid, tbnm, fields, sumaryFields, groupfields, conds, pageindex, pagesize);
        }
    }
}
