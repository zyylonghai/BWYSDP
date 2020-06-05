using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    public class ValidateExpressHelper
    {
        public ValidateExpressHelper()
        {
            
        }

        public bool ColumnValidate(LibTable[] libTables)
        {
            ColExtendedProperties colExtended = null;
            char[] separator = { '+', '-', '*', '/', '(', ')', '<', '>', '=' };
            string[] splitarry = null;
            bool result = true;
            List<LibdefFunc> funcs = GetLibdefFuncs();
            DataTable computer = new DataTable();
            foreach (LibTable libtb in libTables)
            {
                foreach (LibTableObj tbobj in libtb.Tables)
                {
                    if (tbobj.DataTable == null) continue;
                    string expressions = string.Empty;
                    foreach (DataColumn c in tbobj.DataTable.Columns)
                    {
                        colExtended = c.ExtendedProperties[SysConstManage.ExtProp] as ColExtendedProperties;
                        if (string.IsNullOrEmpty(colExtended.ValidateExpression)) continue;
                        if (!colExtended.ValidateExpression.ToUpper().Contains(SysConstManage.sdp_fx.ToUpper()))
                        {

                            continue;
                        }
                        foreach (DataRow dr in tbobj.DataTable.Rows)
                        {
                            expressions = colExtended.ValidateExpression.ToUpper().Replace(SysConstManage.sdp_fx.ToUpper(), "SDP_FX");
                            splitarry = expressions.Split(separator);
                            foreach (string s in splitarry)
                            {
                                if (string.IsNullOrEmpty(s)){ continue; }
                                if (funcs.FirstOrDefault(i => i.FuncNm.ToUpper() == s) != null)
                                {
                                    switch (s)
                                    {
                                        case "SUM":

                                            break;
                                    }
                                }
                                else
                                {
                                    if (LibSysUtils.IsDigit(s) || !s.Contains(SysConstManage.Point)) continue;
                                    string[] vs = s.Split(SysConstManage.Point);
                                    if (vs.Length < 1) { continue; }
                                    int tbindex = LibSysUtils.ToTableIndexByChar(vs[0][0]);
                                    DataTable dt = GetTableByIndex(tbindex, libTables);
                                    int index = expressions.IndexOf(s);
                                    expressions = expressions.Remove(index, s.Length);
                                    expressions = expressions.Insert(index, dt.Rows[0][vs[1]].ToString());
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private DataTable GetTableByIndex(int index,LibTable[] libTables)
        {
            foreach (LibTable libtb in libTables)
            {
                foreach (LibTableObj tableobj in libtb.Tables)
                {
                    TableExtendedProperties extprop = tableobj.DataTable.ExtendedProperties[SysConstManage.ExtProp] as TableExtendedProperties;
                    if (extprop != null && extprop.TableIndex == index)
                    {
                        return tableobj.DataTable;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取自定义系统函数
        /// </summary>
        /// <returns></returns>
        public static List<LibdefFunc> GetLibdefFuncs()
        {
            List<LibdefFunc> result = new List<LibdefFunc>();
            result.Add(new LibdefFunc { FuncNm = "Sum", FuncDesc = "求和" });
            result.Add(new LibdefFunc { FuncNm = "Avg", FuncDesc = "求平均值" });
            return result;
        }
    }
}
