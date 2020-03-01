using SDPCRL.COM.ModelManager;
using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace SDPCRL.COM
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LibTableObj
    {
        Dictionary<string, LibFieldObj> _cols = null;
        string _dsid = string.Empty;
        Dictionary<string, string> _tbaliasmdic = null;
            
        WhereObject _whereObject = null;
        //LibDataSource _ds = null;

        #region 公开的属性
        public DataTable DataTable { get; set; }
        public dynamic Columns;
        public string TableName { get { return this.DataTable.TableName; } }
        public int TableIndex
        {
            get
            {
                TableExtendedProperties tableExtended = this.DataTable.ExtendedProperties[SysConstManage.ExtProp] as TableExtendedProperties;
                if (tableExtended == null) return -1;
                return tableExtended.TableIndex;
            }
        }
        public WhereObject WhereObject { get { return this._whereObject; } }
        public string FromDSID { get { return _dsid; } set { if (string.IsNullOrEmpty(this._dsid)) _dsid = value; } }

        public List<dynamic> Rows { 
            get {
                if (this.DataTable != null && this.DataTable.Rows != null && this.DataTable.Rows.Count > 0)
                {
                    List<dynamic> _rows = new List<dynamic>();
                    foreach (DataRow dr in this.DataTable.Rows)
                    {
                        _rows.Add(new DataRowObj(this._cols, dr,this));
                    }
                    return _rows;
                }
                return null;
            } 
        }
        #endregion

        public LibTableObj(string dsid, string tablenm)
        {
            LibDataSource _ds =SDPCRL.COM.ModelManager.ModelManager.GetDataSource(dsid);
            //101：数据原:{0} 不存在
            if (_ds == null) throw new LibExceptionBase(101,dsid);
            //100：没有表结构
            if (_ds.DefTables == null) throw new LibExceptionBase(100);
            this._dsid = dsid;
            foreach (LibDefineTable deftb in _ds.DefTables)
            {
                if (deftb.TableStruct == null) continue;
                foreach (LibDataTableStruct tb in deftb.TableStruct)
                {
                    if (tb.Name.ToUpper() != tablenm.ToUpper()) continue;
                    DataTable = new CreateTableSchemaHelp().DoCreateTableShema(tb);
                    break;
                }
            }
            if (DataTable == null) throw new LibExceptionBase(521);
            GetColumns();
        }
        public LibTableObj(DataTable table)
        {
            if (table == null)
            {
                //521：DataTable 不能为Null
                throw new LibExceptionBase(521);
            }
            this.DataTable = table;
            GetColumns();

        }
        public dynamic NewRow()
        {
            DataRow row = this.DataTable.NewRow();
            this.DataTable.Rows.Add(row);
            return new DataRowObj(this._cols, row,this);
        }

        /// <summary>
        /// 找出行，该行在表中必须是唯一的
        /// </summary>
        /// <param name="whereExpress"></param>
        /// <returns></returns>
        public dynamic FindRow(Expression<Func<dynamic, dynamic>> uniquekey)
        {
            //LambdaExpression expression = where.Body
            if (this.DataTable == null || this.DataTable.Rows.Count == 0) return null;
            NewExpression newexp = uniquekey.Body as NewExpression;
            dynamic rowobj = null;
            bool isexist = false;
            if (newexp != null)
            {
                if (newexp.Members != null && newexp.Members.Count > 0)
                {
                    //DataRow row = this.DataTable.NewRow();
                    foreach (DataRow row in this.DataTable.Rows)
                    {
                        if (row.RowState == DataRowState.Deleted) continue;
                        ConstantExpression arg = null;
                        for (int i = 0; i < newexp.Members.Count; i++)
                        {
                            arg = newexp.Arguments[i] as ConstantExpression;
                            isexist = row[_cols[newexp.Members[i].Name].ToString()] == arg.Value;
                            if (!isexist) break;
                        }
                        if (isexist)
                        {
                            rowobj = new DataRowObj(this._cols, row,this);
                            break;
                        }
                    }

                    //this.DataTable.Rows.Add(row);
                    //row.AcceptChanges();
                }
            }
            //bool result = where.Method.GetParameters;

            return rowobj;
        }

        /// <summary>
        /// 找出行，该行在表中必须是唯一的
        /// </summary>
        /// <param name="index">行索引</param>
        /// <returns></returns>
        public dynamic FindRow(int index)
        {
            if (this.DataTable != null && this.DataTable.Rows.Count > 0)
            {
                if (this.DataTable.Rows.Count <= index)
                {
                    return null;
                }
                DataRow row = this.DataTable.Rows[index];
                //102：the RowIndex:{0} has been Deleted
                if (row.RowState == DataRowState.Deleted) { throw new LibExceptionBase(102,index); }
                return new DataRowObj(this._cols, this.DataTable.Rows[index],this);
            }
            return null;
        }
        public dynamic FindRow(DataRow row)
        {
            if (this.DataTable != null && this.DataTable.Rows.Count > 0)
            {
                object[] valus = new object[this.DataTable.PrimaryKey.Length];
                for (int i = 0; i < valus.Length; i++)
                {
                    valus[i] = row[this.DataTable.PrimaryKey[i]];
                }
                DataRow dr=this.DataTable.Rows.Find(valus);
                //107 该行状态处于已被删除
                if (dr.RowState == DataRowState.Deleted) { throw new LibExceptionBase(107); }
                return new DataRowObj(this._cols, dr, this);
            }
            return null;
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="index">行索引</param>
        public void DeleteRow(int index)
        {
            if (this.DataTable != null && this.DataTable.Rows.Count > 0)
            {
                if (index < this.DataTable.Rows.Count && this.DataTable.Rows[index].RowState != DataRowState.Deleted)
                {
                    this.DataTable.Rows[index].Delete();
                }

            }
        }
        public void DeleteRow(Expression<Func<dynamic, dynamic>> uniquekey)
        {
            NewExpression newexp = uniquekey.Body as NewExpression;
        }

        public LibTableObj Where(string format, params object[] values)
        {
            if (this._whereObject == null) this._whereObject = new WhereObject();
            this._whereObject.WhereFormat = format;
            this._whereObject.Values = values;
            return this;
        }

        /// <summary>填充数据。有存在旧数据的将被清空。所有数据的行状态为Unchanged</summary>
        /// <param name="dt"></param>
        public void FillData(DataTable dt)
        {
            if (dt == null) return;
            DataColumnCollection srcols = dt.Columns;
            foreach (DataRow row in dt.Rows)
            {
                DoFillRow(row, srcols);
            }
            this.DataTable.AcceptChanges(); 
        }
        /// <summary>填充数据。有存在旧数据的将被清空。所有数据的行状态为Unchanged</summary>
        /// <param name="rows"></param>
        public void FillData(DataRow[] rows)
        {
            if (rows == null ||rows .Length ==0 ) return;
            DataColumnCollection srcols = rows[0].Table .Columns;
            foreach (DataRow dr in rows)
            {
                DoFillRow(dr, srcols);
            }
            this.DataTable.AcceptChanges();
        }
        public string GetTBAliasmn(string dsid, int tableindex, string sourcefield)
        {
            string key = string.Format("{0}{1}{2}", dsid, tableindex, sourcefield);
            string result = string.Empty;
            if (this._tbaliasmdic != null) this._tbaliasmdic.TryGetValue(key, out result);
            return result;
        }
        //public 
        #region 私有函数
        private void GetColumns()
        {
            _cols = new Dictionary<string, LibFieldObj>();
            List<string> vals = new List<string>();
            ColExtendedProperties colExtended = null;
            LibFieldObj fobj = null;
            string ckey = string.Empty;
            TableExtendedProperties tableExtended = DataTable.ExtendedProperties[SysConstManage.ExtProp] as TableExtendedProperties;
            if (_tbaliasmdic == null) _tbaliasmdic = new Dictionary<string, string>();
            foreach (DataColumn c in DataTable.Columns)
            {
                fobj = new LibFieldObj();
                colExtended = c.ExtendedProperties[SysConstManage.ExtProp] as ColExtendedProperties;
                if (colExtended != null)
                {
                    if (colExtended.IsRelate)
                    {
                        if (!string.IsNullOrEmpty(colExtended.AliasName))
                        {
                            ckey = colExtended.AliasName;
                            //fobj.FieldNm = colExtended.AliasName;
                        }
                        else if (!string.IsNullOrEmpty(colExtended.ObjectNm))
                        {
                            ckey = colExtended.ObjectNm;
                            //fobj.FieldNm = colExtended.ObjectNm;
                        }
                        else
                        {
                            ckey = c.ColumnName;
                            //fobj.FieldNm = c.ColumnName;
                        }
                        string key= string.Format("{0}{1}{2}", colExtended.FromDSID, colExtended.FromTableIndex, colExtended.SourceFieldNm);
                        if (!_tbaliasmdic.ContainsKey(key))
                        {
                            string v = string.Format("{0}{1}", LibSysUtils.ToCharByTableIndex(tableExtended.TableIndex), LibSysUtils.ToCharByTableIndex(colExtended.FromTableIndex));
                            //int t = vals.Where(i => i == v).Count();
                            int cout= vals.Where(i => i == v).Count();
                            if (cout == 0)
                            {
                                _tbaliasmdic.Add(key, string.Format("{0}", v));
                            }
                            else
                                _tbaliasmdic.Add(key, string.Format("{0}{1}", v, cout));
                            vals.Add(v);
                        }
                        fobj.TBAliasNm = _tbaliasmdic[key];
                        fobj.FieldNm = colExtended.FieldNm;
                    }
                    else
                    {
                        if (tableExtended != null)
                        {
                            fobj.TBAliasNm = LibSysUtils.ToCharByTableIndex(tableExtended.TableIndex).ToString ();
                        }
                        ckey = string.IsNullOrEmpty(colExtended.ObjectNm) ? c.ColumnName : colExtended.ObjectNm;
                        fobj.FieldNm = c.ColumnName;
                    }
                    fobj.colNm = c.ColumnName;
                    _cols.Add(ckey, fobj);
                }
                else
                {
                    fobj.FieldNm = c.ColumnName;
                    fobj.colNm = c.ColumnName;
                    _cols.Add(fobj.FieldNm, fobj);
                }
                
            }
            Columns = new ColumnObj(_cols);
        }
        private void DoFillRow(DataRow srcrow, DataColumnCollection srcols)
        {
            DataColumn srcol = null;
            DataRow newrow = this.DataTable.NewRow();
            foreach (DataColumn c in this.DataTable.Columns)
            {
                srcol = srcols[c.ColumnName];
                if (srcol == null) continue;
                newrow[c] = srcrow[srcol];
            }
            this.DataTable.Rows.Add(newrow);
        }
        #endregion 

    }
    [Serializable]
    public class ColumnObj : DynamicObject
    {
        Dictionary<string, LibFieldObj> _cols = null;
        public ColumnObj(Dictionary<string, LibFieldObj> cols)
        {
            this._cols = cols;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return this._cols.ContainsKey(binder.Name);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            LibFieldObj fobj = null;
            if (_cols.TryGetValue(binder.Name, out fobj))
            {
                if (string.IsNullOrEmpty(fobj.TBAliasNm))
                {
                    result = fobj.FieldNm;
                }
                else
                    result = string.Format("{0}{1}{2}", fobj.TBAliasNm, SysConstManage.Point, fobj.FieldNm);
                return true;
            }
            result = string.Empty;
            return false;
            //return _cols.TryGetValue(binder.Name, out result);
        }
    }
    [Serializable]
    public class DataRowObj : DynamicObject
    {
        Dictionary<string, LibFieldObj> _cols = null;
        DataRow _dataRow = null;
        LibTableObj _tbobj = null;

        #region 公开的属性
        public DataRowState DataRowState
        {
            get {
                return this._dataRow.RowState;
            }
        }
        public DataRow Row { get { return _dataRow; } }
        public LibTableObj TableObj { get { return _tbobj; } }
        #endregion 
        public DataRowObj(Dictionary<string, LibFieldObj> cols, DataRow row,LibTableObj tbobj)
        {
            this._cols = cols;
            this._dataRow = row;
            this._tbobj = tbobj;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (this._cols.ContainsKey(binder.Name))
            {
                this._dataRow[_cols[binder.Name].colNm] = value;
                //this._cols[binder.Name] = value;
                return true;
            }
            return false;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (this._cols.ContainsKey(binder.Name))
            {
                try
                {
                    string nm = _cols[binder.Name].colNm;
                    result = this._dataRow[nm];
                    return true;
                }
                catch 
                {
                    result = DBNull.Value; 
                }
            }
            result = DBNull.Value;
            return false;
            //return _cols.TryGetValue(binder.Name, out result);
        }
    }

    [Serializable]
    public class LibFieldObj
    {
        public string FieldNm { get; set; }
        public string colNm { get; set; }
        public string TBAliasNm { get; set; }
    }
    [Serializable]
    public class WhereObject
    {
        protected string _whereformat = string.Empty;
        private string[] _params;
        protected string patter = @"{\w*}+";
        private List<string> _appendwhereformats = null;
        #region 公开属性
        public string WhereFormat
        {
            get
            {
                string result = _whereformat;
                MatchCollection matchs = Regex.Matches(result, patter);
                int index = 0;
                if (matchs.Count > 0)
                    _params = new string[matchs.Count];
                foreach (Match item in matchs)
                {
                    index = Convert.ToInt32(item.Value.Replace("{", "").Replace("}", ""));
                    _params[index] = string.Format("@V{0}", index);
                    result = result.Replace(item.Value, _params[index]);
                }
                return result;
            }
            set
            {
                _whereformat = value;
            }
        }

        public object[] Values
        {
            get;
            set;
        }

        public string ValueTostring
        {
            get
            {
                if (_params != null && _params.Length > 0)
                {
                    StringBuilder partype = new StringBuilder();
                    StringBuilder val = new StringBuilder();
                    object o = null;
                    Type t = null;
                    for (int n = 0; n < _params.Length; n++)
                    {
                        if (_params[n] == null) continue;
                        if (partype.Length > 0)
                        {
                            partype.Append(",");
                            val.Append(",");
                        }
                        o = Values[Convert.ToInt32(_params[n].Substring(2))];
                        t = o.GetType();
                        if (t == typeof(string))
                        {
                            partype.AppendFormat("{0} nvarchar({1})", _params[n], o.ToString().Length == 0 ? 1 : o.ToString().Length);
                            val.AppendFormat("{0}='{1}'", _params[n], o);
                        }
                        else if (t == typeof(int))
                        {
                            partype.AppendFormat("{0} int ", _params[n]);
                            val.AppendFormat("{0}={1}", _params[n], o);
                        }
                        else if (t == typeof(bool))
                        {
                            partype.AppendFormat("{0} bit ", _params[n]);
                            val.AppendFormat("{0}={1}", _params[n], o);
                        }
                        //switch (Values[n].GetType().Name)
                        //{
                        //    case "String":

                        //        partype.AppendFormat("{0} nvarchar({1})", _params[n], o.ToString().Length == 0 ? 1 : o.ToString().Length);
                        //        val.AppendFormat("{0}='{1}'", _params[n], o);
                        //        break;
                        //    case "Int32":
                        //        partype.AppendFormat("{0} int ", _params[n]);
                        //        val.AppendFormat("{0}={1}", _params[n], o);
                        //        break;
                        //    case "DateTime":
                        //        break;
                        //}
                    }
                    return string.Format("N'{0}',{1}", partype.ToString(), val.ToString());
                }
                else
                    return string.Empty;
            }
        }
        #endregion

        #region 公开方法
        public void AppendWhereFormat(string andor, string whereformat, params object[] values)
        {
            if (string.IsNullOrEmpty(this._whereformat))
                this._whereformat = whereformat;
            else
            {
                MatchCollection matchs = Regex.Matches(_whereformat, patter);
                if (matchs != null)
                {
                    int oldcout = matchs.Count;
                    matchs = Regex.Matches(whereformat, patter);
                    int index = 0;
                    foreach (Match item in matchs)
                    {
                        index = Convert.ToInt32(item.Value.Replace("{", "").Replace("}", ""));
                        whereformat = whereformat.Replace(item.Value, item.Value.Replace(index.ToString(), string.Format("{0}*", (oldcout + index).ToString())));
                    }
                }
                whereformat = whereformat.Replace("*", "");
                this._whereformat = string.Format("{0} {1} {2}", this._whereformat, andor, whereformat);
                if (_appendwhereformats == null) _appendwhereformats = new List<string>();
                _appendwhereformats.Add(string.Format("{0}", whereformat));
            }
            if (values != null)
            {
                List<object> vals = new List<object>();
                if (this.Values != null && this.Values.Length > 0)
                    vals.AddRange(this.Values);
                vals.AddRange(values);
                this.Values = vals.ToArray();
            }
        }

        /// <summary>
        /// 只用于生成Updatesql时，取Updatefield转为参数的语法。
        /// </summary>
        /// <returns></returns>
        public string GetUpdatefieldformat()
        {
            StringBuilder result = new StringBuilder();
            if (_appendwhereformats != null)
            {
                foreach (string item in _appendwhereformats)
                {
                    if (result.Length > 0)
                    {
                        result.Append(SysConstManage.Comma);
                    }
                    result.Append(item);
                }
            }
            return result.ToString();
        }
        #endregion
        #region 受保护的方法

        #endregion 

    }
}
