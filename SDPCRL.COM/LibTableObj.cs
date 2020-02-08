using SDPCRL.COM.ModelManager;
using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SDPCRL.COM
{
    /// <summary>
    /// 
    /// </summary>
    public class LibTableObj
    {
        Dictionary<string, object> _cols = null;
        LibDataSource _ds = null;

        #region 公开的属性
        public DataTable DataTable { get; set; }
        public dynamic Columns;
        #endregion

        public LibTableObj(string dsid, string tablenm)
        {
            _ds =SDPCRL.COM.ModelManager.ModelManager.GetDataSource(dsid);
            if (_ds == null) throw new LibExceptionBase(string.Format("DataSource:{0} not exist", dsid));
            if (_ds.DefTables == null) throw new LibExceptionBase(string.Format("Do not LibDataTableStruct"));
            foreach (LibDefineTable deftb in _ds.DefTables)
            {
                if (deftb.TableStruct == null) continue;
                foreach (LibDataTableStruct tb in deftb.TableStruct)
                {
                    if (tb.Name.ToUpper() != tablenm.ToUpper()) continue;
                    DataTable = new CreateTableSchemaHelp().DoCreateTableShema(tb);
                }
            }
            if (DataTable == null) throw new LibExceptionBase(string.Format("DataTable is null", dsid));
            _cols = new Dictionary<string, object>();
            foreach (DataColumn c in DataTable.Columns)
            {
                _cols.Add(c.ColumnName, c.ColumnName);
            }
            Columns = new ColumnObj(_cols);
        }
        public dynamic NewRow()
        {
            DataRow row = this.DataTable.NewRow();
            this.DataTable.Rows.Add(row);
            return new DataRowObj(this._cols, row);
        }
    }
    public class ColumnObj : DynamicObject
    {
        Dictionary<string, object> _cols = null;
        public ColumnObj(Dictionary<string, object> cols)
        {
            this._cols = cols;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return this._cols.ContainsKey(binder.Name);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _cols.TryGetValue(binder.Name, out result);
        }
    }
    public class DataRowObj : DynamicObject
    {
        Dictionary<string, object> _cols = null;
        DataRow _dataRow = null;
        public DataRowObj(Dictionary<string, object> cols, DataRow row)
        {
            this._cols = cols;
            this._dataRow = row;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (this._cols.ContainsKey(binder.Name))
            {
                this._dataRow[binder.Name] = value;
                //this._cols[binder.Name] = value;
                return true;
            }
            return false;
        }
    }
}
