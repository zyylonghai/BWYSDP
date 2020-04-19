using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SDPCRL.COM;

namespace SDPCRL.IBussiness
{
    public interface IDataAccess
    {
        void BeginTrans();
        void CommitTrans();
        void RollbackTrans();
        object ExecuteScalar(string commandText);
        int ExecuteNonQuery(string commandText);
        DataRow GetDataRow(string commandText);
        DataTable GetDataTable(string commandText);
        void GetDatatTables(string commandText, ref DataTable[] dts);
        DataSet GetDataTables(string commandText);
        DataTable ExecStoredProcedure(string storedprocedureNm, LibDbParameter[] parameters);

        void FillTableObj(LibTableObj tableObj);
        void SaveChange(LibTableObj[] tableObjs, bool IsTrans = true);
        void WriteDataLog(LibTableObj[] tableObjs);
    }
}
