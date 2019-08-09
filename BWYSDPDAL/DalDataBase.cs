using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.DAL.BUS;
using System.Data;
using BWYResFactory;
using SDPCRL.DAL.COM;

namespace BWYSDPDAL
{
    public class DalDataBase : DALBase
    {
        /// <summary>
        /// 获取所有账套
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAccount()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            SQLBuilder builder2 = new SQLBuilder("CheckBill");
            string sql= builder2.GetSQL("CheckBill", new string[] { }, new WhereObject { WhereFormat= "A.BillNo={0} and A.billStatus={1}", Values=new object[] { "T201903160001","1" } });
            SQLBuilder build = new SQLBuilder();
            //DataTable dt = this.DataAccess.GetDataTable("select ID,AccoutNm from  Accout where AccoutNm!='" + ResFactory.ResManager.SysDBNm + "'");
            DataTable dt = this.DataAccess.GetDataTable(build.GetSQL("Accout", new string[] { "ID", "AccoutNm" }, build.Where("AccoutNm!={0}", ResFactory.ResManager.SysDBNm)));
            foreach (DataRow row in dt.Rows)
            {
                result.Add(row["ID"].ToString(), row["AccoutNm"].ToString());
            }
            return result;
        }

        /// <summary>判断是否存在表</summary>
        /// <param name="tableNm"></param>
        /// <returns></returns>
        public bool IsExistsTable(string tableNm)
        {
            object obj = this.DataAccess.ExecuteScalar(string.Format("select * from dbo.sysobjects where id = object_id(N'[dbo].[{0}]') and OBJECTPROPERTY(id, N'IsUserTable') = 1", tableNm));
            return obj != null;
        }

        public DataTable GetTableSchema(string tableNm)
        {
            //throw new Exception("ziidingyierror");
            return this.DataAccess.GetDataTable(string.Format("select column_name,data_type,IS_NULLABLE,NUMERIC_PRECISION,NUMERIC_SCALE,CHARACTER_MAXIMUM_LENGTH" +
                                                         " from information_schema.columns " +
                                                         "where table_name = '{0}'", tableNm));

        }

        public bool BuilderTableStruct(string sqlstr)
        {
            int result = this.DataAccess.ExecuteNonQuery(sqlstr);
            return true;
        }
    }
}
