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
        /// <summary>
        /// 错误，警告等信息集
        /// </summary>
        public List<LibMessage> MsgList { get; set; }
        public ValidateExpressHelper()
        {
            this.MsgList = new List<LibMessage>();
        }


        public bool ColumnValidate(LibTable[] libTables)
        {
            ColExtendedProperties colExtended = null;
            char[] separator = { '+', '-', '*', '/', '(', ')', '<', '>', '=',';',':' };
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
                            expressions = expressions.Replace(" AND ", ";").Replace(" OR ", ":");
                            splitarry = expressions.Split(separator);
                            foreach (string s in splitarry)
                            {
                                if (string.IsNullOrEmpty(s)){ continue; }
                                if (funcs.FirstOrDefault(i => i.FuncNm.ToUpper() == s) != null)
                                {
                                    ComputeBracketsContent(ref expressions, s, libTables);
                                    //switch (s)
                                    //{
                                    //    case "SUM":

                                    //        break;
                                    //}
                                }
                                else
                                {
                                    if (LibSysUtils.IsDigit(s) || !s.Contains(SysConstManage.Point)) continue;
                                    string[] vs = s.Split(SysConstManage.Point);
                                    if (vs.Length < 1) { continue; }
                                    int tbindex = LibSysUtils.ToTableIndexByChar(vs[0][0]);
                                    DataTable dt = GetTableByIndex(tbindex, libTables);
                                    int index = expressions.IndexOf(s);
                                    if (index == -1) continue;
                                    expressions = expressions.Remove(index, s.Length);
                                    expressions = expressions.Insert(index, dt.Rows[0][vs[1]].ToString());
                                }
                            }
                            expressions = expressions.Replace(";", " AND ").Replace(":", " OR ");
                            expressions = expressions.Replace("SDP_FX", dr[c.ColumnName].ToString());
                            bool right =(bool)computer.Compute(expressions, "");
                            if (!right)
                            {
                                result = false;
                                //表{0},字段{1},值有效性验证不通过，表达式{2}
                                this.MsgList.Add(new LibMessage { Message =string.Format(BWYResFactory.ResFactory .ResManager.GetResByKey("110"), tbobj.TableName,c.ColumnName, colExtended.ValidateExpression.Replace("<","'<'")), MsgType = LibMessageType.Error });
                            }
                        }
                    }
                }
            }
            return result;
        }

        #region  私有函数
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

        private bool ComputeBracketsContent(ref string express,string funcnm, LibTable[] libTables)
        {
            int index = express.IndexOf(funcnm) + funcnm .Length;
            int accout = 1;
            int index1 = -1;
            int leng = 0;
            string s2 = string.Empty;
            for (int i = 0; i != accout;)
            {
                s2 = express.Substring(i == 0 ? index : (leng + index + i));
                index1 = s2.IndexOf(")");
                if (index1 == -1) break;
                s2 = express.Substring(index, index1 + leng + i);
                accout = s2.Where(a => a == '(').Count();
                i++;
                leng += index1;
            }
            leng = s2.Length+funcnm .Length;
            char[] separator = { '+', '-', '*', '/', '(', ')', '<', '>', '='};
            string[] splitarry = s2.Split(separator );
            if (splitarry != null && splitarry .Length >0)
            {
                DataTable dt = null;
                char tbalisnm='*';
                List<string> cols = new List<string>();
                foreach (string s in splitarry)
                {
                    if (LibSysUtils.IsDigit(s) || !s.Contains(SysConstManage.Point)) continue;
                    string[] vs = s.Split(SysConstManage.Point);
                    if (vs.Length < 1) { continue; }
                    if (tbalisnm != '*' && tbalisnm != vs[0][0])
                    {
                        return false;
                    }
                    tbalisnm = vs[0][0];
                    if (dt == null)
                    {
                        int tbindex = LibSysUtils.ToTableIndexByChar(tbalisnm);
                        dt = GetTableByIndex(tbindex, libTables);
                    }
                    cols.Add(vs[1]);
                    int index3 = s2.IndexOf(s);
                    s2 = s2.Remove(index3, s.Length);
                    s2 = s2.Insert(index3, string.Format("sdp_col{0}",cols .Count));
                }
                if (dt == null || tbalisnm == '*') return false;
                object result=string.Empty;
                s2 = s2.Substring(1);
                if (string.Compare(funcnm, "sum", true) == 0)
                {
                    decimal sum = 0;
                    string s3 = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        s3 = s2;
                        for (int i = 0; i < cols.Count; i++)
                        {
                            s3 = s3.Replace(string.Format("sdp_col{0}", i + 1), dr[cols[i]].ToString());
                        }
                        sum += Convert.ToDecimal(dt.Compute(s3, ""));
                    }
                    result = sum;
                }
                express = express.Remove(index - funcnm.Length, leng + 1);
                express = express.Insert(index - funcnm.Length, result.ToString ());
                return true;
            }
            return false;
        }
        #endregion

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
