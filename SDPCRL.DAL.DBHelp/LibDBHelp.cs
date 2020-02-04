using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using SDPCRL.CORE.FileUtils;
using SDPCRL.CORE;
using SDPCRL.DAL.IDBHelp;
using SDPCRL.DAL.COM;
using System.Data;
using System.Collections;
using SDPCRL.COM;

namespace SDPCRL.DAL.DBHelp
{
    [Serializable]
    class LibDBHelp : DBhelpBase ,ILibDBHelp
    {
        //private DbProviderFactory _dbProvierFactory;
        //private DbConnection _dbConnect = null;
        //private DbCommand _dbcmd;
        //private DbDataAdapter _dbAdapter;
        //private string _dbConnectStr = string.Empty;
        //private DBInfoHelp _dbInfoHelp;

        //private DBInfoHelp DBInfoHelp {
        //    get {
        //        if (_dbInfoHelp == null)
        //            _dbInfoHelp = new DBInfoHelp();
        //        return _dbInfoHelp;
        //    }
        //}


        //#region 构造函数
        ///// <summary>
        ///// 用于测试连接
        ///// </summary>
        ///// <param name="pType"></param>
        //public LibDBHelp(LibProviderType pType)
        //{
        //    _dbProvierFactory = LibDBProviderFactory.GetDbProviderFactory(pType);
        //    GetConnectStr();
        //}
        //public LibDBHelp()
        //{
        //    _dbProvierFactory = LibDBProviderFactory.GetDbProviderFactory(DBInfoHelp.BinaryReadProviderType());
        //    GetConnectStr();

        //}
        //#endregion

        //#region 私有函数
        //private DbConnection dbConnect
        //{
        //    get
        //    {
        //        if (_dbConnect == null)
        //        {
        //            _dbConnect = _dbProvierFactory.CreateConnection();
        //            _dbConnect.ConnectionString = _dbConnectStr;
        //            _dbConnect.Open();
        //        }
        //        else if (_dbConnect.State == System.Data.ConnectionState.Broken)
        //        {
        //            _dbConnect.Close();
        //            _dbConnect.Open();
        //        }
        //        else if (_dbConnect.State == System.Data.ConnectionState.Closed)
        //        {
        //            _dbConnect.Open();
        //        }
        //        return _dbConnect;
        //    }
        //}
        //private DbCommand dbCommand
        //{
        //    get {
        //        if (_dbcmd == null)
        //        {
        //            _dbcmd = _dbProvierFactory.CreateCommand();
        //            _dbcmd.Connection = dbConnect;
        //        }
        //        return _dbcmd;
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //private void GetConnectStr()
        //{
        //    this._dbConnectStr = DBInfoHelp.BinaryReadDBConnectStr();
        //}
        //#endregion
        ///// <summary>
        ///// 执行sql语法返回受影响的行数
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //public int ExecuteNonQuery(string sql)
        //{
        //    dbCommand.CommandText = sql;
        //    return dbCommand.ExecuteNonQuery();
        //}

        //public bool TestConnect(string connectStr, out string ex)
        //{
        //    try
        //    {
        //        //string connectstr = "server=DESKTOP-1CN18NT;database=zyyTest;uid=sa;password=152625";
        //        _dbConnect = _dbProvierFactory.CreateConnection();
        //        _dbConnect.ConnectionString = connectStr;
        //        _dbConnect.Open();
        //        //Connect.Open();
        //        ex = "success";
        //        return true;
        //    }
        //    catch (Exception excep)
        //    {
        //        ex = excep.Message;
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //public object ExecuteScalar(string sql)
        //{
        //    dbCommand.CommandText = sql;
        //    return dbCommand.ExecuteScalar();
        //}
        ///// <summary>
        ///// 执行sql语法，返回结果集的第一行。
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //public DataRow GetDataRow(string sql)
        //{
        //    dbCommand.CommandText = sql;
        //    DataRow row = null;
        //    using (IDataReader reader = dbCommand.ExecuteReader())
        //    {
        //        if (reader.Read())
        //        {

        //        }
        //    }
        //    return row;
        //}
        public LibDBHelp()
        {
            if (this.CurrentDBOpreate == null) this.CurrentDBOpreate = new DBOperate();
            if (this.NeedInitial)
                this.CurrentDBOpreate.Initial();
            //DBOperate.Initial();
        }
        public LibDBHelp(LibProviderType pType)
        {
            if(this.CurrentDBOpreate ==null) this.CurrentDBOpreate = new DBOperate();
            if (this.NeedInitial)
                this.CurrentDBOpreate.Initial(pType);
            //DBOperate.Initial(pType);
        }
        public LibDBHelp(string guid)
        {
            this.Guid = guid;
            this.AddConnect();
            if (this.NeedInitial)
                this.CurrentDBOpreate.Initial();
        }

        public DataTable GetDataTable(string commandText)
        {
            DataTable result= this.CurrentDBOpreate.GetDataTable(commandText);
            if (result == null)
            {
                LibEventManager.TouchEvent(this, LibEventType.SqlException, this.CurrentDBOpreate.Exception);
                LibEventManager.LogOutListener(this);
            }

            return result;
            //return DBOperate.GetDataTable(commandText);
        }

        int ILibDBHelp.ExecuteNonQuery(string commandText)
        {
            int result= this.CurrentDBOpreate.ExecuteNonQuery(commandText);
            if (result == -900)
            {
                LibEventManager.TouchEvent(this, LibEventType.SqlException, this.CurrentDBOpreate.Exception);
                LibEventManager.LogOutListener(this);
            }
            //return this.CurrentDBOpreate.ExecuteNonQuery(commandText);
            //return DBOperate.ExecuteNonQuery(commandText);
            return result;
        }

        object ILibDBHelp.ExecuteScalar(string commandText)
        {
            object result= this.CurrentDBOpreate.ExecuteScalar(commandText);
            if (result == null)
            {
                LibEventManager.TouchEvent(this, LibEventType.SqlException, this.CurrentDBOpreate.Exception);
                LibEventManager.LogOutListener(this);
            }
            return result;
            //return DBOperate.ExecuteScalar(commandText);
        }

        DataRow ILibDBHelp.GetDataRow(string commandText)
        {
            int flag = 0;
            DataRow result= this.CurrentDBOpreate.GetDataRow(commandText, out flag);
            if (flag == -1)
            {
                LibEventManager.TouchEvent(this, LibEventType.SqlException, this.CurrentDBOpreate.Exception);
                LibEventManager.LogOutListener(this);
            }
            return result;
           //return DBOperate.GetDataRow(commandText);
        }

        bool ILibDBHelp.TestConnect(string connectStr, out string ex)
        {
            return this.CurrentDBOpreate.TestConnect(connectStr, out ex);
            //return DBOperate.TestConnect(connectStr, out ex);
        }


        public bool SaveAccout(DBInfo dbinfo)
        {
          return  this.CurrentDBOpreate.SaveAccout(dbinfo);
        }

        public void BeginTrans()
        {
            this.CurrentDBOpreate.BeginTransation();
        }

        public void CommitTrans()
        {
            this.CurrentDBOpreate.CommitTransation();
        }

        public void RollbackTrans()
        {
            this.CurrentDBOpreate.RollbackTransation();
        }

        public void GetDataTables(string commandText,ref DataTable[] dts)
        {
            this.CurrentDBOpreate.GetDataTables(commandText,ref dts);
        }
    }
    class DBOperate
    {
        private  DbProviderFactory _dbProvierFactory;
        private  DbConnection _dbConnect = null;
        private  DbCommand _dbcmd;
        private  DbDataAdapter _dbAdapter;
        //private DbTransaction _dbTransaction=null;
        private  string _dbConnectStr = string.Empty;
        private  DBInfoHelp _dbInfoHelp;
        private string _guid=string.Empty ;
        private bool _transaction = false;

        private  DBInfoHelp DBInfoHelp
        {
            get
            {
                if (_dbInfoHelp == null)
                {
                    _dbInfoHelp = new DBInfoHelp();
                    _dbInfoHelp.Guid = this._guid;
                }
                return _dbInfoHelp;
            }
        }

        #region 私有属性
        private  DbConnection dbConnect
        {
            get
            {
                if (_dbConnect == null)
                {
                    _dbConnect = _dbProvierFactory.CreateConnection();
                    _dbConnect.ConnectionString = _dbConnectStr;
                    _dbConnect.Open();
                }
                else if (_dbConnect.State == System.Data.ConnectionState.Broken)
                {
                    _dbConnect.Close();
                    _dbConnect.Open();
                }
                else if (_dbConnect.State == System.Data.ConnectionState.Closed)
                {
                    _dbConnect.Open();
                }
                return _dbConnect;
            }
        }
        private  DbCommand dbCommand
        {
            get
            {
                if (_dbcmd == null)
                {
                    _dbcmd = _dbProvierFactory.CreateCommand();
                    _dbcmd.Connection = dbConnect;
                }
                else
                {
                    if (_dbcmd.Connection.State == ConnectionState.Closed)
                        _dbcmd.Connection.Open();
                }
                return _dbcmd;
            }
        }
        private  DbDataAdapter dbAdapter
        {
            get
            {
                if (_dbAdapter == null)
                {
                    _dbAdapter = _dbProvierFactory.CreateDataAdapter(); 
                }
                return _dbAdapter;
            }
        }

        private DbTransaction DbTransaction
        {
            get { return  dbConnect.BeginTransaction();  }
        }
        #endregion

        public Exception Exception { get; set; }

        public DBOperate(string guid)
        {
            this._guid = guid;
        }
        public DBOperate()
        { }

        /// <summary>
        /// 
        /// </summary>
        private void GetConnectStr()
        {
            //_dbConnectStr = DBInfoHelp.BinaryReadDBConnectStr();
            _dbConnectStr = DBInfoHelp.ReadSysDBConnect();
            if (!string.IsNullOrEmpty(_dbConnectStr))
            {
                _dbProvierFactory = LibDBProviderFactory.GetDbProviderFactory(DBInfoHelp.ProviderType);
                DBInfoHelp.Key = GetAccoutKey();
                if (!string.IsNullOrEmpty(DBInfoHelp.Key))
                {
                    _dbConnect = null;
                    _dbcmd = null;
                    _dbConnectStr = DBInfoHelp.ReadDBConnect();
                }
            }
        }
        public  void Initial()
        {
            GetConnectStr();
            _dbProvierFactory = LibDBProviderFactory.GetDbProviderFactory(DBInfoHelp.ProviderType);
        }
        public  void Initial(LibProviderType pType)
        {
            GetConnectStr();
            _dbProvierFactory = LibDBProviderFactory.GetDbProviderFactory(pType);
        }

        public System.Data.ConnectionState ConnectState
        {
            get { return this._dbConnect.State; }
        }

        #region SQL命令执行相关函数

        public void BeginTransation()
        {
            //dbConnect.BeginTransaction();
            dbCommand.Transaction = DbTransaction;
            this._transaction = true;
        }

        public void CommitTransation()
        {
            if (dbCommand.Transaction != null)
                dbCommand.Transaction.Commit();
            this._transaction = false;
            CloseConnect();
        }

        public void RollbackTransation()
        {
            if (dbCommand.Transaction != null)
                dbCommand.Transaction.Rollback();
            this._transaction = false;
            CloseConnect();
        }

        /// <summary>
        /// 执行sql语法返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public  int ExecuteNonQuery(string sql)
        {
            try
            {
                dbCommand.CommandText = sql;
                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception excep)
            {
                //ex = excep.Message;
                this.Exception = excep;
                //if (this._transaction && dbCommand.Transaction != null)
                //    RollbackTransation();
                return -900;
            }
            finally
            {
                if (!this._transaction)
                    CloseConnect();
            }
        }

        /// <summary>
        /// 执行sql语法返回结果集中的第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public  object ExecuteScalar(string commandText)
        {
            try
            {
                dbCommand.CommandText = commandText;
                return dbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                this.Exception = ex;
                return null;
            }
            finally
            {
                CloseConnect();
            }
        }
        /// <summary>
        /// 执行sql语法，返回结果集的第一行。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public  DataRow GetDataRow(string commandText,out int flag)
        {
            dbCommand.CommandText = commandText;
            DataRow row = null;
            try
            {
                using (IDataReader reader = dbCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DataTable dt = reader.GetSchemaTable();
                        DataTable resuldt = new DataTable();
                        DataColumn colnm = dt.Columns["ColumnName"];
                        foreach (DataRow dr in dt.Rows)
                        {
                            switch (dr["DataTypeName"].ToString())
                            {
                                //case "nvarchar":
                                //case "ntext":
                                //    resuldt.Columns.Add(new DataColumn(dr[colnm].ToString(), typeof(string)));
                                //    break;
                                case "bit":
                                    resuldt.Columns.Add(new DataColumn(dr[colnm].ToString(), typeof(bool)));
                                    break;
                                case "int":
                                    resuldt.Columns.Add(new DataColumn(dr[colnm].ToString(), typeof(int)));
                                    break;
                                case "date":
                                    resuldt.Columns.Add(new DataColumn(dr[colnm].ToString(), typeof(Date)));
                                    break;
                                case "datetime":
                                    resuldt.Columns.Add(new DataColumn(dr[colnm].ToString(), typeof(DateTime)));
                                    break;
                                case "image":
                                    resuldt.Columns.Add(new DataColumn(dr[colnm].ToString(), typeof(byte[])));
                                    break;
                                case "decimal":
                                    resuldt.Columns.Add(new DataColumn(dr[colnm].ToString(), typeof(decimal)));
                                    break;
                                default:
                                    resuldt.Columns.Add(new DataColumn(dr[colnm].ToString(), typeof(string)));
                                    break;
                            }
                            
                            //resuldt.Columns.Add(dr[0].ToString());ColumnSize
                            //row[col] = reader[col .ColumnName];
                        }
                        row = resuldt.NewRow();
                        foreach (DataRow dr in dt.Rows)
                        {
                            row[dr[colnm].ToString()] = reader[dr[colnm].ToString()];
                        }
                    }
                }
                flag = 1;
                return row;
            }
            catch (Exception ex)
            {
                flag = -1;
                this.Exception = ex;
                return null;
            }
            finally
            {
                CloseConnect();
            }
        }
        public  DataTable GetDataTable(string commandText)
        {
            DataTable dt = new DataTable();
            try
            {
                dbCommand.CommandText = commandText;
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dt);
            }
            catch (Exception ex) {
                this.Exception = ex;
                return null;
            }
            finally
            {
                CloseConnect();
            }
            return dt;
        }

        public void GetDataTables(string commandText,ref DataTable[] dts)
        {
            //DataTable[] dts = new DataTable[cout];
            //for (int i = 0; i < cout; i++)
            //{
            //    dts[i] = new DataTable();
            //}
            try
            {
                dbCommand.CommandText = commandText;
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(0, 0, dts);
            }
            catch (Exception ex)
            {
                this.Exception = ex;
                return;
            }
            finally
            {
                CloseConnect();
            }
            //return dts;
        }

        //public object DoExecuteProcedure(string procedure)
        //{
 
        //}

        public bool TestConnect(string connectStr, out string ex)
        {
            try
            {
                _dbConnect = _dbProvierFactory.CreateConnection();
                _dbConnect.ConnectionString = connectStr;
                _dbConnect.Open();
                //Connect.Open();
                ex = "success";
                return true;
            }
            catch (Exception excep)
            {
                ex = excep.Message;
                return false;
            }
            finally
            {
                CloseConnect();
            }
        }

        public bool SaveAccout(DBInfo info)
        {
            string commandText = string.Format("Insert Into Accout(ID,AccoutNm,IPAddress,CreateTime,Creater,[Key]) values('{0}','{1}','{2}','{3}','{4}','{5}')",
                                               info.Guid, info.DataBase, info.ServerAddr, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "admin", info.Key);
            if (ExecuteNonQuery(commandText) != -1)
                return true;
            return false;
        }
        #endregion

        #region 私有函数
        private void CloseConnect()
        {
            if (this._dbConnect != null)
                this._dbConnect.Close();
        }
        private string GetAccoutKey()
        {
            object result = ExecuteScalar("select [key] from Accout where ID='" + _guid + "'");
            if(result !=null )
                return result .ToString ();
            return string.Empty;
        }
        #endregion
    }


    class DBhelpBase: MarshalByRefObject
    {
        static Hashtable connetTable = new Hashtable();
        static readonly object locker = new object();
        DBOperate[] db;
        DBOperate _currentDBOperate;
        int _maxConnectAmount = 2;
        protected  bool NeedInitial = true;
        protected string Guid
        {
            get;
            set;
        }
        public DBhelpBase()
        {
 
        }
        protected void AddConnect()
        {
            DBOperate dboperate=null;
            lock (locker)
            {
                if (connetTable.ContainsKey(Guid))
                {
                    db = (DBOperate[])connetTable[Guid];
                    if (db.Length < _maxConnectAmount)
                    {
                        dboperate = new DBOperate(Guid);
                        Array.Resize(ref db, db.Length + 1);
                        db[db.Length - 1] = dboperate;
                        connetTable[Guid] = db;
                    }
                    else
                    {
                        NeedInitial = false;
                    }
                }
                else
                {
                    dboperate = new DBOperate(Guid);
                    db = new DBOperate[1];
                    db[0] = dboperate;
                    connetTable.Add(Guid, db);
                }
                _currentDBOperate = dboperate;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected DBOperate CurrentDBOpreate
        {
            get
            {
                if (_currentDBOperate == null&&!string.IsNullOrEmpty (Guid))
                {
                    db = (DBOperate[])connetTable[Guid];
                    foreach (DBOperate item in db)
                    {
                        if (item.ConnectState == System.Data.ConnectionState.Closed)
                        {
                            _currentDBOperate = item;
                            break;
                        }

                    }
                }
                return _currentDBOperate;
            }
            set { _currentDBOperate = value; }
        }
    }
}
