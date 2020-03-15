using SDPCRL.DAL.BUS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BWYSDPDAL
{
    public class DataLogDal : DALBase
    {
        public bool AddDataLogs()
        {
            DataTable dt = this.DataAccess.GetDataTable("select * from  DataLogsM");
            return true;
        }
    }
}
