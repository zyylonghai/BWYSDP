using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWYSDP.DAL;
using SDPCRL.CORE;

namespace BWYSDP.BLL
{
    public  class DataSourceInfoBLL
    {
        public static  int GetMaxDSID()
        {
            string sql = "select max(DSID) FROM DSINFO";
            //return Int32.Parse( DBHelp.DataAccess.ExecuteScalar(sql).ToString ());
            return 1;
        }

        public static int GetMaxDefTBID(int dsId)
        {
            string sql =string.Format("select max(DefTBID) FROM DSINFO where DSID={0}",dsId);
            //return LibSysUtils.ToInt32(DBHelp.DataAccess.ExecuteScalar(sql).ToString());
            return 1;
        }
    }
}
