﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.IBussiness;
using SDPCRL.DAL.DBHelp;
using SDPCRL.DAL.IDBHelp;
using System.Data;
using SDPCRL.CORE;

namespace SDPCRL.DAL.BUS
{
    class DataAccess:IDataAccess,ILibEventListener, IDisposable,IlibException
    {
       private  static DBHelpFactory  _dbFactory;
       private ILibDBHelp  _dbHelp;
        private ExceptionHelp _ExceptionHelp;
        private ExceptionHelp ExceptionHelp {
            get {
                if (_ExceptionHelp == null)
                    _ExceptionHelp = new ExceptionHelp();
                return _ExceptionHelp;
            }
        }
        public DataAccess()
        {
            if (_dbFactory == null)
            {
                _dbFactory = new DBHelpFactory();
            }
            _dbHelp = _dbFactory.GetDBHelp();
        }
        public DataAccess(string guid)
            :this()
        {
            _dbHelp = _dbFactory.GetDBHelp(guid);
            LibEventManager.SubscribeEvent(new LibSqlExceptionEventSource(this, _dbHelp), LibEventType.SqlException);
        }

        public void BeginTrans()
        {
            _dbHelp.BeginTrans();
        }

        public void CommitTrans()
        {
            _dbHelp.CommitTrans();
        }


        public void RollbackTrans()
        {
            _dbHelp.RollbackTrans();
        }
        public object ExecuteScalar(string commandText)
        {
            return _dbHelp.ExecuteScalar(commandText);
        }

        public DataRow GetDataRow(string commandText)
        {
            return _dbHelp.GetDataRow(commandText);
        }


        public DataTable GetDataTable(string commandText)
        {
            return _dbHelp.GetDataTable(commandText);
        }


        public void GetDatatTables(string commandText, ref DataTable[] dts)
        {
             _dbHelp.GetDataTables(commandText,ref dts);
        }

        public int ExecuteNonQuery(string commandText)
        {
            return _dbHelp.ExecuteNonQuery(commandText);
        }

        public void Dispose()
        {
            
        }

        public void DoEvents(LibEventType eventType, LibEventArgs args)
        {
            switch (eventType)
            {
                case LibEventType.SqlException:
                    LibSqlExceptionEventArgs eventarg = args as LibSqlExceptionEventArgs;
                    this.ExceptionHelp.ThrowError(this, eventarg.Exception.Message,eventarg.Exception .StackTrace);
                    break;
            }
        }

        public void BeforeThrow()
        {
            
        }
    }
}
