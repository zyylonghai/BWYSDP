using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWYResFactory;
using SDPCRL.CORE;

namespace SDPCRL.DAL.COM
{
    public class SQLBuilder
    {
        public string GetSQL(string tableNm,string[] fields,string whereStr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ResFactory.ResManager.SQLSelect);
            foreach (string field in fields)
            {
                if(builder .Length !=ResFactory.ResManager .SQLSelect .Length)
                {
                    builder.Append(SysConstManage.Comma);
                }
                builder.AppendFormat(" {0}", field);
            }

            builder.AppendFormat(" {0}",ResFactory.ResManager.SQLFrom);
            builder.AppendFormat(" {0}", tableNm);
            builder.AppendFormat(" {0} {1}", ResFactory.ResManager.SQLWhere, whereStr);
            return builder.ToString();
        }
    }
}
