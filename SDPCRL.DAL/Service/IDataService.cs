using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SDPCRL.DAL.Service
{
    public interface IDataService
    {
        /// <summary> </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string sql);
        /// <summary></summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable ExecuteQueryToTable(string sql);
        /// <summary></summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql);

        //void 

        void testsql();


    }
}
