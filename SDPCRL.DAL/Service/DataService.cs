using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SDPCRL.DAL.Service
{
    class DataService:DBServiceBase ,IService  
    {
        
        public DataService()
        {
            
        }


        public int ExecuteNonQuery(string sql)
        {
           return  this.DoExecuteNonQuery(sql);
        }


        public DataTable ExecuteQueryToTable(string sql)
        {
            DataSet ds = this.DoExecuteQuery(sql);
            return ds == null ? null : ds.Tables[0];
        }


        public object ExecuteScalar(string sql)
        {
           return   this.DoExecuteScalar(sql);
        }

        public void Star()
        {
            
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void testsql()
        {
            this.test();
        }
    }
}
