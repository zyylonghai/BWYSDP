using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.DAL.BUS;
using System.Data;
using BWYResFactory;
using SDPCRL.DAL.COM;
using SDPCRL.COM;

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
            //SQLBuilder builder2 = new SQLBuilder("CheckBill");
            //string sql= builder2.GetSQL("CheckBill", new string[] { }, new WhereObject { WhereFormat= "A.BillNo={0} and A.billStatus={1}", Values=new object[] { "T201903160001","1" } });
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

        public List<FuncAssemblyInfo> GetDalAssemblyInfos()
        {
            return DalAssemblyHelp.GetDalAssemblyInfos();
        }

        #region  多语言处理
        public string InternalGetFieldDesc(int languageId, string dsid, string tablenm, string fieldid)
        {
            SQLBuilder build = new SQLBuilder();
           string sql= build.GetSQL("Language_Field", new string[] { "Vals" }, 
                                    build.Where("LanguageId={0} and DSID={1} and FieldNm={2} and TableNm={3}",languageId, dsid,fieldid ,tablenm));
           DataRow row= this.DataAccess.GetDataRow(sql);
            if (row != null)
            {
                return row["Vals"].ToString();
            }
            return fieldid;
        }
        public DataTable GetFieldDescByDSID(string dsid)
        {
            SQLBuilder build = new SQLBuilder();
            string sql = build.GetSQL("Language_Field", new string[] { "FieldNm", "DSID", "LanguageId", "TableNm", "Vals" },
                                     build.Where("DSID={0} OR DSID={1}",dsid,""));
            DataTable data = this.DataAccess.GetDataTable(sql);
            return data;
        }
        public DataTable GetFieldDescByFieldNm(string dsid,string fieldid)
        {
            SQLBuilder build = new SQLBuilder();
            string sql = build.GetSQL("Language_Field", new string[] { "FieldNm", "DSID", "LanguageId", "TableNm", "Vals" },
                                     build.Where("DSID={0} And FieldNm={1}", dsid, fieldid));
            DataTable data = this.DataAccess.GetDataTable(sql);
            return data;
        }
        public DataTable GetAllMsgDesc(int languageId)
        {
            SQLBuilder build = new SQLBuilder();
            string sql = build.GetSQL("Language_msg", new string[] { "MsgCode","Vals" },
                                     build.Where("LanguageId={0}", languageId));
            DataTable data = this.DataAccess.GetDataTable(sql);
            return data;
        }

        public void UpdateLanguage(DataTable dt,string dsid)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                this.DataAccess.BeginTrans();
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (var item in Enum.GetValues(typeof(SDPCRL.COM.Language)))
                    {
                        sql.Clear();
                        sql.Append(" begin ");
                        sql.AppendLine();
                        sql.AppendFormat("if exists(select vals from Language_Field where LanguageId='{0}' and DSID='{1}' and FieldNm='{2}' and TableNm='{3}')",
                                          (int)item,dsid,dr["FieldNm"],dr["TableNm"]);
                        sql.AppendFormat("update Language_Field set Vals='{4}' where LanguageId='{0}' and DSID='{1}' and FieldNm='{2}' and TableNm='{3}'",
                                          (int)item, dsid, dr["FieldNm"], dr["TableNm"],dr[item.ToString ()]);
                        sql.AppendLine();
                        sql.Append(" else ");
                        sql.AppendLine();
                        sql.AppendFormat("insert into Language_Field values('{0}','{1}','{2}','{3}','{4}')",
                                          (int)item, dsid, dr["FieldNm"], dr["TableNm"], dr[item.ToString()]);
                        sql.Append(" end ");
                        this.DataAccess.ExecuteNonQuery(sql.ToString());
                    }
                }
                this.DataAccess.CommitTrans();
            }
            catch (Exception ex)
            {
                this.DataAccess.RollbackTrans();
            }
        }
        #endregion 
    }
}
