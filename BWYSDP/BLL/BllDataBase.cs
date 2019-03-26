using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.BLL.BUS;
using BWYSDP.DAL;
using System.Data;

namespace BWYSDP.BLL
{
    public class BllDataBase:BllBus
    {
        #region 构造函数
        public BllDataBase(bool getcurrentserver = true)
        {
            if (getcurrentserver)
            {
                DAL.ServerInfo info = new SQLite().GetCurrentServer();
                if (info != null)
                {
                    this.AccoutId = info.accountid;
                    SDPCRL.BLL.BUS.ServerInfo.ConnectType = info.connectype;
                    SDPCRL.BLL.BUS.ServerInfo.IPAddress = info.ipAddress;
                    SDPCRL.BLL.BUS.ServerInfo.Point = info.point;
                }
            }
        }

        //public BllDataBase(bool getcurrentserver=false)
        //{
           
        //}
        #endregion


        public Dictionary<string, string> GetAccount()
        {
           return  (Dictionary <string ,string >) this.ExecuteSysDalMethod("TestFunc", "GetAccount");
        }
        /// <summary>查询表是否已存在</summary>
        /// <param name="tableNm"></param>
        /// <returns></returns>
        public bool IsExistTable(string tableNm)
        {
            return (bool)this.ExecuteDalMethod("TestFunc", "IsExistsTable", tableNm);   
        }

        /// <summary>获取表所有列信息</summary>
        /// <param name="tableNm"></param>
        /// <returns></returns>
        public DataTable GetTableSchema(string tableNm)
        {
            return (DataTable)this.ExecuteDalMethod("TestFunc", "GetTableSchema", tableNm);
        }
        public bool BuilderTableStruct(string sqlstr)
        {
            return (bool)this.ExecuteDalMethod("TestFunc", "BuilderTableStruct", sqlstr);
        }
    }
}
