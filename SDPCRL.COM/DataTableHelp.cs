using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    public class DataTableHelp
    {
        #region 公开属性
        /// <summary>
        /// 来源表
        /// </summary>
        public DataTable SDataTable { get; set; }
        /// <summary>
        /// 目标表
        /// </summary>
        public DataTable TDataTable { get; set; }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public DataTableHelp()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="tdt"></param>
        public DataTableHelp(DataTable sdt, DataTable tdt)
        {
            this.SDataTable = sdt;
            this.TDataTable = tdt;
        }
        /// <summary>
        /// 复制来源表数据到目标表，目标表最后执行AcceptChanges，数据处于无更新状态（如果属性SDataTable，TDataTable 有被赋值）
        /// </summary>
        public void CopyStable()
        {
            if (this.SDataTable != null && this.TDataTable != null)
            {
                DataColumnCollection tcols = TDataTable.Columns;
                DataColumnCollection scols = SDataTable.Columns;
                DataColumn col = null;
                DataRow dr = null;
                TDataTable.Rows.Clear();
                foreach (DataRow srow in SDataTable.Rows)
                {
                    dr = TDataTable.NewRow();
                    foreach (DataColumn c in tcols)
                    {
                        col = scols[c.ColumnName];
                        if (col != null)
                        {
                            if (c.DataType.Equals(typeof(Date)))
                            {
                                dr[c] = new Date { value = srow[col].ToString() };
                            }
                            else
                                dr[c] = srow[col];
                        }
                    }
                    TDataTable.Rows.Add(dr);
                }
                TDataTable.AcceptChanges();
            }
        }

        #region 静态函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srow"></param>
        /// <param name="trow"></param>
        public static void CopyRow(DataRow srow, DataRow trow)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colnm"></param>
        /// <param name="val"></param>
        public static void SetColomnValue(DataRow row, string colnm, object val)
        {
            if (row.Table.Columns[colnm].DataType.Equals(typeof(Date)))
            {
                row[colnm] = new Date { value = val.ToString () };
            }
            else
                row[colnm] = val;
        }
        #endregion
    }
}
