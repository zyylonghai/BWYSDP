using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.IBussiness;
using SDPCRL.DAL.DBHelp;
using SDPCRL.DAL.IDBHelp;
using System.Data;
using SDPCRL.CORE;
using SDPCRL.COM;
using SDPCRL.DAL.COM;

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

        public void FillTableObj(LibTableObj tableObj)
        {
            SQLBuilder sQLBuilder = new SQLBuilder(tableObj.FromDSID);
            if (tableObj.WhereObject != null)
            {
                DataTable dt = this.GetDataTable(sQLBuilder.GetSQL(tableObj .TableName ,null ,tableObj.WhereObject,false ,false ));
                tableObj.FillData(dt);
            }
        }
        public void SaveChange(LibTableObj[] tableObjs)
        {
            if (tableObjs == null || tableObjs.Length == 0) return;
            try
            {
                this.BeginTrans();
                DataTable dt = null;
                StringBuilder fields = null;
                StringBuilder fieldtypes = null;
                StringBuilder fieldvalue = null;

                StringBuilder updateFields = null;
                StringBuilder updatefldtypes = null;
                StringBuilder updatefldval = null;
                StringBuilder updatewhere = null;
                string sql = string.Empty;
                ColExtendedProperties colextprop = null;
                TableExtendedProperties tbextprop = null;
                List<object> updatevalue = null;
                foreach (LibTableObj tableObj in tableObjs)
                {
                    dt = tableObj.DataTable;
                    tbextprop = Newtonsoft.Json.JsonConvert.DeserializeObject<TableExtendedProperties>(dt.ExtendedProperties[SysConstManage.ExtProp].ToString());
                    if (!tbextprop.Ignore) continue;
                    fields = new StringBuilder();
                    fieldtypes = new StringBuilder();
                    fieldvalue = new StringBuilder();
                    List<int> bytecols = new List<int>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        colextprop = Newtonsoft.Json.JsonConvert.DeserializeObject<ColExtendedProperties>(col.ExtendedProperties[SysConstManage.ExtProp].ToString());
                        if (!colextprop.IsActive) continue;
                        if (fields.Length > 0)
                        {
                            fields.Append(",");
                            fieldtypes.Append(",");
                            fieldvalue.Append(",");
                        }
                        fields.AppendFormat("@{0}", col.ColumnName);
                        fieldtypes.AppendFormat("@{0} ", col.ColumnName);
                        if (col.DataType == typeof(byte[]))
                        {
                            bytecols.Add(dt.Columns.IndexOf(col));
                        }
                        SetColmnTypeAndValue(col, col.ColumnName, colextprop, fieldtypes, fieldvalue, dt.Columns.IndexOf(col));
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        switch (row.RowState)
                        {
                            case DataRowState.Added:
                                object[] vals = new object[row.ItemArray.Length];
                                if (bytecols.Count > 0)
                                {
                                    for (int a = 0; a < row.ItemArray.Length; a++)
                                    {
                                        vals[a] = bytecols.Contains(a) && row.ItemArray[a] != DBNull.Value ? Convert.ToBase64String((byte[])row.ItemArray[a]) : row.ItemArray[a];
                                    }
                                }
                                else
                                    vals = row.ItemArray;
                                sql = string.Format(string.Format("EXEC sp_executesql N'insert into {0} values({1}) ',N'{2}',{3}",
                                                                  dt.TableName, fields.ToString(), fieldtypes.ToString(), fieldvalue.ToString()
                                                                  ),
                                                    vals);
                                break;
                            case DataRowState.Modified:
                                updateFields = new StringBuilder();
                                updatefldtypes = new StringBuilder();
                                updatefldval = new StringBuilder();
                                updatewhere = new StringBuilder();
                                updatevalue = new List<object>();
                                int index = 0;
                                foreach (DataColumn c in dt.Columns)
                                {
                                    colextprop = Newtonsoft.Json.JsonConvert.DeserializeObject<ColExtendedProperties>(c.ExtendedProperties[SysConstManage.ExtProp].ToString());
                                    if (!colextprop.IsActive) continue;
                                    if (!LibSysUtils.Compare(row[c, DataRowVersion.Original], row[c, DataRowVersion.Current], false))
                                    {
                                        if (updateFields.Length > 0)
                                        {
                                            updateFields.Append(",");
                                        }
                                        if (updatefldtypes.Length > 0)
                                        {
                                            updatefldtypes.Append(",");
                                        }
                                        if (updatefldval.Length > 0)
                                        {
                                            updatefldval.Append(",");
                                        }
                                        updateFields.AppendFormat("{0}=@{0}", c.ColumnName);
                                        updatefldtypes.AppendFormat("@{0} ", c.ColumnName);
                                        SetColmnTypeAndValue(c, c.ColumnName, colextprop, updatefldtypes, updatefldval, index);
                                        index++;
                                        if (c.DataType.Equals(typeof(byte[])))
                                        {
                                            updatevalue.Add(Convert.ToBase64String((byte[])row[c, DataRowVersion.Current]));
                                        }
                                        else
                                            updatevalue.Add(row[c, DataRowVersion.Current]);
                                    }
                                    if (dt.PrimaryKey.Contains(c))
                                    {
                                        if (updatewhere.Length > 0)
                                        {
                                            updatewhere.Append(" And ");
                                        }
                                        if (updatefldtypes.Length > 0)
                                        {
                                            updatefldtypes.Append(",");
                                            updatefldval.Append(",");
                                        }
                                        updatewhere.AppendFormat("{0}=@{1}", c.ColumnName, string.Format("{0}1", c.ColumnName));
                                        updatefldtypes.AppendFormat("@{0} ", string.Format("{0}1", c.ColumnName));
                                        SetColmnTypeAndValue(c, string.Format("{0}1", c.ColumnName), colextprop, updatefldtypes, updatefldval, index);
                                        index++;
                                        updatevalue.Add(row[c, DataRowVersion.Original]);
                                    }
                                }
                                if (updateFields.Length > 0)
                                {
                                    sql = string.Format(string.Format("EXEC sp_executesql N'Update {0} Set {1} where {2}',N'{3}',{4}",
                                                                       dt.TableName,
                                                                       updateFields.ToString(),
                                                                       updatewhere.ToString(),
                                                                       updatefldtypes.ToString(),
                                                                       updatefldval.ToString()
                                                                      ),
                                                        updatevalue.ToArray());
                                    break;
                                }
                                continue;
                            case DataRowState.Deleted:
                                updatefldtypes = new StringBuilder();
                                updatefldval = new StringBuilder();
                                updatewhere = new StringBuilder();
                                updatevalue = new List<object>();
                                index = 0;
                                foreach (DataColumn col in dt.PrimaryKey)
                                {
                                    colextprop = Newtonsoft.Json.JsonConvert.DeserializeObject<ColExtendedProperties>(col.ExtendedProperties[SysConstManage.ExtProp].ToString());
                                    if (updatewhere.Length > 0)
                                    {
                                        updatewhere.Append(" And ");
                                    }
                                    if (updatefldtypes.Length > 0)
                                    {
                                        updatefldtypes.Append(",");
                                        updatefldval.Append(",");
                                    }
                                    updatewhere.AppendFormat("{0}=@{1}", col.ColumnName, string.Format("{0}1", col.ColumnName));
                                    updatefldtypes.AppendFormat("@{0} ", string.Format("{0}1", col.ColumnName));
                                    SetColmnTypeAndValue(col, string.Format("{0}1", col.ColumnName), colextprop, updatefldtypes, updatefldval, index);
                                    index++;
                                    updatevalue.Add(row[col, DataRowVersion.Original]);
                                }
                                if (updatefldval.Length > 0)
                                {
                                    sql = string.Format(string.Format("EXEC sp_executesql N'Delete from {0} where {1}',N'{2}',{3}",
                                                                       dt.TableName,
                                                                       updatewhere.ToString(),
                                                                       updatefldtypes.ToString(),
                                                                       updatefldval.ToString()
                                                                      ),
                                                        updatevalue.ToArray());
                                    break;
                                }
                                continue;
                            //break;
                            default:
                                continue;
                                //break;
                        }
                        this.ExecuteNonQuery(sql);
                    }
                }
                this.CommitTrans();
            }
            catch (Exception ex)
            {
                this.RollbackTrans();
                throw ex;
            }

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

        #region 私有函数
        private void SetColmnTypeAndValue(DataColumn col, string parmNm, ColExtendedProperties colextprop, StringBuilder fieldtypes, StringBuilder fieldvalue, int index)
        {
            if (col.DataType == typeof(string))
            {
                if (colextprop.DataTypeLen == 0)
                {
                    fieldtypes.Append("ntext");
                }
                else
                    fieldtypes.AppendFormat("nvarchar({0})", colextprop.DataTypeLen);
                fieldvalue.Append(string.Format("@{0}", parmNm) + "='{" + index + "}'");
            }
            else if (col.DataType == typeof(long))
            {
                fieldtypes.AppendFormat("bigint");
                fieldvalue.Append(string.Format("@{0}", parmNm) + "={" + index + "}");
            }
            else if (col.DataType == typeof(Int32))
            {
                fieldtypes.AppendFormat("int");
                fieldvalue.Append(string.Format("@{0}", parmNm) + "={" + index + "}");
            }
            else if (col.DataType == typeof(decimal))
            {
                fieldtypes.AppendFormat("decimal({0}, {1})", colextprop.DataTypeLen, colextprop.Decimalpoint);
                fieldvalue.Append(string.Format("@{0}", parmNm) + "={" + index + "}");
            }
            else if (col.DataType == typeof(DateTime))
            {
                fieldtypes.AppendFormat("datetime");
                fieldvalue.Append(string.Format("@{0}", parmNm) + "='{" + index + "}'");
            }
            else if (col.DataType == typeof(Date))
            {
                fieldtypes.AppendFormat("date");
                fieldvalue.Append(string.Format("@{0}", parmNm) + "='{" + index + "}'");
            }
            else if (col.DataType == typeof(bool))
            {
                fieldtypes.AppendFormat("bit");
                fieldvalue.Append(string.Format("@{0}", parmNm) + "={" + index + "}");
            }
            else if (col.DataType == typeof(byte[]))
            {
                fieldtypes.AppendFormat("image");
                fieldvalue.Append(string.Format("@{0}", parmNm) + "='{" + index + "}'");
            }
        }
        #endregion
    }
}
