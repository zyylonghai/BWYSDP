using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.DAL.BUS;
using System.Data;

namespace BWYSDPDAL
{
    public class DalDataBase:DALBase
    {
        /// <summary>
        /// 获取所有账套
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAccount()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            DataTable dt= this.DataAccess.GetDataTable("select ID,AccoutNm from  Accout");
            foreach (DataRow row in dt.Rows)
            {
                result.Add(row["ID"].ToString(), row["AccoutNm"].ToString());
            }
            return result;
        }
    }
}
