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
        /// <summary>功能编号</summary>
        public string ProgId { get; set; }
        /// <summary>
        /// 数据集
        /// </summary>
        public LibTable[] LibTables { get; set; }
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

        protected T JsonToObj<T>(string objstr)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(objstr);
        }
        #endregion

        #region 私有函数
        private void SetColmnTypeAndValue(DataColumn col,string parmNm, ColExtendedProperties colextprop, StringBuilder fieldtypes, StringBuilder fieldvalue, int index)
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
            else if (col.DataType == typeof(byte))
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

                StringBuilder updateFields = null;
                StringBuilder updatefldtypes = null;
                StringBuilder updatefldval = null;
                StringBuilder updatewhere = null;
                string sql = string.Empty;
                ColExtendedProperties colextprop = null;
                TableExtendedProperties tbextprop = null;
                List<object> updatevalue = null;
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
                            SetColmnTypeAndValue(col,col.ColumnName, colextprop, fieldtypes, fieldvalue, dt.Columns.IndexOf(col));
                            #region 旧代码
                            //if (col.DataType == typeof(string))
                            //{
                            //    if (colextprop.DataTypeLen == 0)
                            //    {
                            //        fieldtypes.Append("ntext");
                            //    }
                            //    else
                            //        fieldtypes.AppendFormat("nvarchar({0})", colextprop.DataTypeLen);
                            //    fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "='{" + dt.Columns.IndexOf(col) + "}'");
                            //}
                            //else if (col.DataType == typeof(long))
                            //{
                            //    fieldtypes.AppendFormat("bigint");
                            //    fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "={" + dt.Columns.IndexOf(col) + "}");
                            //}
                            //else if (col.DataType == typeof(Int32))
                            //{
                            //    fieldtypes.AppendFormat("int");
                            //    fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "={" + dt.Columns.IndexOf(col) + "}");
                            //}
                            //else if (col.DataType == typeof(decimal))
                            //{
                            //    fieldtypes.AppendFormat("decimal({0}, {1})", colextprop.DataTypeLen, colextprop.Decimalpoint);
                            //    fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "={" + dt.Columns.IndexOf(col) + "}");
                            //}
                            //else if (col.DataType == typeof(DateTime))
                            //{
                            //    fieldtypes.AppendFormat("datetime");
                            //    fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "='{" + dt.Columns.IndexOf(col) + "}'");
                            //}
                            //else if (col.DataType == typeof(Date))
                            //{
                            //    fieldtypes.AppendFormat("date");
                            //    fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "='{" + dt.Columns.IndexOf(col) + "}'");
                            //}
                            //else if (col.DataType == typeof(byte))
                            //{
                            //    fieldtypes.AppendFormat("bit");
                            //    fieldvalue.Append(string.Format("@{0}", col.ColumnName) + "={" + dt.Columns.IndexOf(col) + "}");
                            //}
                            #endregion
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            switch (row.RowState)
                            {
                                case DataRowState.Added:
                                    object[] vals = new object[row.ItemArray.Length];
                                    if (bytecols.Count > 0)
                                    {
                                        for(int a=0;a<row .ItemArray .Length;a++)
                                        {
                                            vals[a] = bytecols.Contains(a)? Convert .ToBase64String((byte[])row.ItemArray[a]) :row.ItemArray[a];
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
                                        if (!LibSysUtils .Compare(row[c, DataRowVersion.Original],row[c, DataRowVersion.Current],false))
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
                                            SetColmnTypeAndValue(c,c.ColumnName , colextprop, updatefldtypes, updatefldval, index);
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
            catch (Exception ex)
            {
                this.DataAccess.RollbackTrans();
                throw ex;
            }
        }
        public void AddMessage(string msg, LibMessageType type)
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
