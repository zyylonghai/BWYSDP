using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.IBussiness;
using SDPCRL.CORE;
using SDPCRL.COM;
using System.Data;

namespace SDPCRL.DAL.BUS
{
    public class DALBase
    {
        private IDataAccess _dataAccess;
        private List<LibMessage> MsgList = null;
        #region 公开的属性
        public IDataAccess DataAccess
        {
            get
            {
                if (_dataAccess == null)
                {
                    if (string.IsNullOrEmpty(AccountID))
                        _dataAccess = new DataAccess();
                    else
                        _dataAccess = new DataAccess(AccountID);
                }
                return _dataAccess;
            }
        }

        public string AccountID { get; set; }
        #endregion

        #region 构造函数
        public DALBase()
        {
            MsgList = new List<LibMessage>();
        }
        #endregion

        #region 受保护 虚拟函数
        protected virtual void BeforeUpdate()
        {

        }

        protected virtual void AfterUpdate()
        {

        }
        #endregion

        #region 私有函数

        #endregion

        #region 公开函数

        public virtual void Save(LibTable[] libtables)
        {
            try
            {
                #region 数据验证.

                #endregion

                BeforeUpdate();
                this.DataAccess.BeginTrans();
                #region 解析LibTable 并转为sql语句
                LibTable libtable = null;
                DataTable dt = null;
                StringBuilder fields = null;
                StringBuilder fieldtypes = null;
                StringBuilder fieldvalue = null;
                string sql = string.Empty;
                ColExtendedProperties colextprop = null;
                TableExtendedProperties tbextprop = null;
                for (int i = 0; i < libtables.Length; i++)
                {
                    libtable = libtables[i];
                    if (libtable.Tables == null) continue;
                    for (int n = 0; n < libtable.Tables.Length; n++)
                    {
                        dt = libtable.Tables[n];
                        tbextprop = Newtonsoft.Json.JsonConvert.DeserializeObject<TableExtendedProperties>(dt.ExtendedProperties[SysConstManage.ExtProp].ToString());
                        if (!tbextprop.Ignore) continue;
                        fields = new StringBuilder();
                        fieldtypes = new StringBuilder();
                        fieldvalue = new StringBuilder();
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
                            if (col.DataType == typeof(string))
                            {
                                if (colextprop.DataTypeLen == 0)
                                {
                                    fieldtypes.Append("ntext");
                                }
                                else
                                    fieldtypes.AppendFormat("nvarchar({0})", colextprop.DataTypeLen);
                                fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "='{" + dt.Columns.IndexOf(col) + "}'");
                            }
                            else if (col.DataType == typeof(long))
                            {
                                fieldtypes.AppendFormat("bigint");
                                fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "={" + dt.Columns.IndexOf(col) + "}");
                            }
                            else if (col.DataType == typeof(Int32))
                            {
                                fieldtypes.AppendFormat("int");
                                fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "={" + dt.Columns.IndexOf(col) + "}");
                            }
                            else if (col.DataType == typeof(decimal))
                            {
                                fieldtypes.AppendFormat("decimal({0}, {1})", colextprop.DataTypeLen, colextprop.Decimalpoint);
                                fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "={" + dt.Columns.IndexOf(col) + "}");
                            }
                            else if (col.DataType == typeof(DateTime))
                            {
                                fieldtypes.AppendFormat("datetime");
                                fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "='{" + dt.Columns.IndexOf(col) + "}'");
                            }
                            else if (col.DataType == typeof(Date))
                            {
                                fieldtypes.AppendFormat("date");
                                fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "='{" + dt.Columns.IndexOf(col) + "}'");
                            }
                            else if (col.DataType == typeof(byte))
                            {
                                fieldtypes.AppendFormat("bit");
                                fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "={" + dt.Columns.IndexOf(col) + "}");
                            }
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            switch (row.RowState)
                            {
                                case DataRowState.Added:
                                    sql = string.Format(string.Format("EXEC sp_executesql N'insert into {0} values({1}) ',N'{2}',{3}",
                                        dt.TableName, fields.ToString(), fieldtypes.ToString(), fieldvalue.ToString()), row.ItemArray);
                                    break;
                                case DataRowState.Modified:

                                    continue;
                                //break;
                                case DataRowState.Deleted:
                                    continue;
                                //break;
                                default:
                                    continue;
                                    //break;
                            }
                            this.DataAccess.ExecuteNonQuery(sql);
                        }
                    }
                }
                #endregion
                AfterUpdate();
                if (this.MsgList.FirstOrDefault(i => i.MsgType == LibMessageType.Error) != null)//有类型为error的信息，必须回滚事务
                {
                    this.DataAccess.RollbackTrans();
                }
                else
                    this.DataAccess.CommitTrans();
            }
            catch(Exception ex) {
                this.DataAccess.RollbackTrans();
                throw ex;
            }
        }
        public void AddErrorMessage(string msg,LibMessageType type)
        {
            this.MsgList.Add(new LibMessage { Message = msg, MsgType = type });
        }

        public List<LibMessage> GetErrorMessage()
        {
            return MsgList;
        }
        #endregion
    }
}
