﻿using SDPCRL.COM;
using SDPCRL.DAL.COM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SDPCRL.DAL.IDBHelp
{
    public interface ILibDBHelp
    {
        void BeginTrans();
        void CommitTrans();
        void RollbackTrans();
        /// <summary>执行sql语法返回受影响的行数</summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string commandText);
        int ExecuteNonQuery(List<string> commandtextlist);
        /// <summary>
        /// 执行sql语法返回结果集中的第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object ExecuteScalar(string commandText);
        /// <summary>
        /// 执行sql语法，返回结果集的第一行。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataRow GetDataRow(string commandText);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable GetDataTable(string commandText);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        void GetDataTables(string commandText,ref DataTable[] dts);
        DataSet GetDataTables(string commandText);
        void StoredProcedureReturnValue(string storedprocedureNm, LibDbParameter[] parameters);
        DataTable ExecStoredProcedure(string storedprocedureNm, LibDbParameter[] parameters);
        //object ExecuteProcedure(string procedureNm,
        bool TestConnect(string connectStr, out string ex);
        bool SaveAccout(SDPCRL.DAL.COM.DBInfo dbinfo);
        DataTable GetAccout();
    }
}
