using SDPCRL.DAL.BUS;
using SDPCRL.DAL.COM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SDPCRL.COM;
using SDPCRL.CORE;

namespace BWYSDPDAL
{
    public class DataLogDal : DALBase
    {
        //public bool AddDataLogs()
        //{
        //    DataTable dt = this.DataAccess.GetDataTable("select * from  DataLogsM");
        //    return true;
        //}

        public DataTable[] SearchData(Dictionary<string, List<string>> tableAndlogids)
        {
            DataTable[] results = { };
            if (tableAndlogids != null)
            {
                foreach (var item in tableAndlogids)
                {
                    foreach (string logid in item.Value)
                    {
                        LibDbParameter[] parameters = new LibDbParameter[2];
                        parameters[0] = new LibDbParameter { ParameterNm = "@tablenm", DbType = DbType.String, Value = item.Key };
                        parameters[1] = new LibDbParameter { ParameterNm = "@logid", DbType = DbType.String, Value = logid };
                        Array.Resize(ref results, results.Length + 1);
                        results[results.Length - 1] = this.DataAccess.ExecStoredProcedure("p_searclog", parameters);
                        results[results.Length - 1].TableName = string.Format("{0}{2}{1}", item.Key, logid,SysConstManage.Underline);
                    }
                }
            }
            return results;
        }
    }
}
