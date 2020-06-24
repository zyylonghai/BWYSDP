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
        //public List<string> MsgcodeList { get; set; }
        public List<ValidateMessage> ValidateMessages { get; set; }
        public ValidateExpressHelper()
        {
            this.MsgList = new List<LibMessage>();
            ValidateMessages = new List<ValidateMessage>();
        }


        public bool ColumnValidate(LibTable[] libTables)
        {
            ColExtendedProperties colExtended = null;
            char[] separator = { '+', '-', '*', '/', '(', ')', '<', '>', '=',';',':' };
            string[] splitarry = null;
            bool result = true;
            List<LibdefFunc> funcs = GetLibdefFuncs();
            DataTable computer = new DataTable();
            ValidateMessage message = null;
            List<string> v = new List<string>();
            foreach (LibTable libtb in libTables)
            {
                foreach (LibTableObj tbobj in libtb.Tables)
                {
                    if (tbobj.DataTable == null) continue;
                    string expressions = string.Empty;
                    foreach (DataColumn c in tbobj.DataTable.Columns)
                    {
                        colExtended = c.ExtendedProperties[SysConstManage.ExtProp] as ColExtendedProperties;
                        if (colExtended.ValidateExpression==null || string.IsNullOrEmpty(colExtended.ValidateExpression.Express)) continue;
                        if (!colExtended.ValidateExpression.Express.ToUpper().Contains(SysConstManage.sdp_fx.ToUpper()))
                        {
                            continue;
                        }
                        foreach (DataRow dr in tbobj.DataTable.Rows)
                        {
                            expressions = colExtended.ValidateExpression.Express.ToUpper().Replace(SysConstManage.sdp_fx.ToUpper(), "SDP_FX");
                            expressions = expressions.Replace(" AND ", ";").Replace(" OR ", ":");
                            splitarry = expressions.Split(separator);
                            foreach (string s in splitarry)
                            {
                                if (string.IsNullOrEmpty(s)){ continue; }
                                if (v.FirstOrDefault(i => i == s) != null) { v.Remove(s);continue; }
                                if (funcs.FirstOrDefault(i => i.FuncNm.ToUpper() == s.Trim ()) != null)
                                {
                                    if (ComputeBracketsContent(ref expressions, s, libTables, colExtended,v))
                                        result = false;
                                    continue;
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
                                if (string.IsNullOrEmpty(colExtended.ValidateExpression.MsgCode))
                                {
                                    //表{0},字段{1},值有效性验证不通过，表达式{2}
                                    this.MsgList.Add(new LibMessage { Message = string.Format(BWYResFactory.ResFactory.ResManager.GetResByKey("110"), tbobj.TableName, c.ColumnName, colExtended.ValidateExpression.Express.Replace("<", "'<'")), MsgType = LibMessageType.Error });
                                }
                                else
                                {
                                    message = new ValidateMessage { MsgCode = colExtended.ValidateExpression.MsgCode };
                                    this.ValidateMessages.Add(message);
                                    if (!string.IsNullOrEmpty(colExtended.ValidateExpression.MsgParams))
                                    {
                                        string[] parms = colExtended.ValidateExpression.MsgParams.ToUpper().Split(SysConstManage.Comma);
                                        if (parms != null && parms.Length > 0)
                                        {
                                            message.MsgParams = new object[parms.Length];
                                            string p = string.Empty;
                                            bool jg = false;
                                            #region 解析信息参数
                                            for (int i=0;i<parms.Length;i++)
                                            {
                                                p = parms[i];
                                                if (p.StartsWith("GetDesc", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    jg = ComputeBracketsContent(ref p, "GetDesc", libTables, new ColExtendedProperties { ValidateExpression = new ModelManager.LibFieldValidatExpress { Express = p } }, null);
                                                    if (jg)
                                                    {
                                                        message.MsgParams[i] = p;
                                                    }
                                                }
                                                else if (p.StartsWith("Sum", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    jg = ComputeBracketsContent(ref p, "Sum", libTables, new ColExtendedProperties { ValidateExpression = new ModelManager.LibFieldValidatExpress { Express = p } }, null);
                                                    if (jg)
                                                    {
                                                        message.MsgParams[i] = p;
                                                    }
                                                }
                                                else if (p.StartsWith("Avg", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    jg = ComputeBracketsContent(ref p, "Avg", libTables, new ColExtendedProperties { ValidateExpression = new ModelManager.LibFieldValidatExpress { Express = p } }, null);
                                                    if (jg)
                                                    {
                                                        message.MsgParams[i] = p;
                                                    }
                                                }
                                                else if (p.StartsWith("Count", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    jg = ComputeBracketsContent(ref p, "Count", libTables, new ColExtendedProperties { ValidateExpression = new ModelManager.LibFieldValidatExpress { Express = p } }, null);
                                                    if (jg)
                                                    {
                                                        message.MsgParams[i] = p;
                                                    }
                                                }
                                                else
                                                {
                                                    if (p.Contains(SysConstManage.Point))
                                                    {
                                                        string[] v0 = p.Split(SysConstManage.Point);
                                                        if (v0 != null && v0.Length > 1)
                                                        {
                                                            if (v0[0].Length > 1)
                                                            {
                                                                //112 信息参数{0},无法解析。
                                                                this.MsgList.Add(new LibMessage { Message = string.Format(BWYResFactory.ResFactory.ResManager.GetResByKey("112"),p), MsgType = LibMessageType.Error });
                                                                return false;
                                                            }
                                                            int tbindex = LibSysUtils.ToTableIndexByChar(v0[0][0]);
                                                            DataTable table = GetTableByIndex(tbindex, libTables);
                                                            if (table.TableName == tbobj.TableName)
                                                            {
                                                                message.MsgParams[i] = dr[v0[1]];
                                                            }
                                                            else
                                                            {
                                                                message.MsgParams[i] = table.Rows[0][v0[1]];
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                }
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

        /// <summary> 计算函数括号里的表达式</summary>
        /// <param name="express"></param>
        /// <param name="funcnm"></param>
        /// <param name="libTables"></param>
        /// <param name="colExtended"></param>
        /// <returns></returns>
        private bool ComputeBracketsContent(ref string express,string funcnm, LibTable[] libTables, ColExtendedProperties colExtended,List<string> v)
        {
            funcnm = funcnm.Trim();
            int index = express.IndexOf(funcnm,StringComparison.CurrentCultureIgnoreCase) + funcnm .Length;
            int accout = 1;
            int index1 = -1;
            int leng = 0;
            string s2 = string.Empty;//最终为函数括号中的表达式
            #region 取函数括号中的表达式
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
            #endregion
            leng = s2.Length+funcnm .Length;
            char[] separator = { '+', '-', '*', '/', '(', ')', '<', '>', '='};
            string[] splitarry = s2.Split(separator );
            if (splitarry != null && splitarry .Length >0)
            {
                DataTable dt = null;
                char tbalisnm='*';
                List<string> cols = new List<string>();
                if (v != null) v.AddRange(splitarry);
                foreach (string s in splitarry)
                {
                    if (LibSysUtils.IsDigit(s) || !s.Contains(SysConstManage.Point)) continue;
                    string[] vs = s.Split(SysConstManage.Point);
                    if (vs.Length < 1) { continue; }
                    if (tbalisnm != '*' && tbalisnm != vs[0][0])
                    {
                        //111 表达式{0},中的函数{1},括号中的字段必须是属相同表。
                        this.MsgList.Add(new LibMessage { Message = string.Format(BWYResFactory.ResFactory.ResManager.GetResByKey("111"), colExtended.ValidateExpression.Express.Replace("<", "'<'"), funcnm), MsgType = LibMessageType.Error });
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
                #region 函数的计算求值
                if (string.Compare(funcnm, "sum", true) == 0)
                {
                    decimal sum = 0;
                    string s3 = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr.RowState == DataRowState.Deleted) continue;
                        s3 = s2;
                        for (int i = 0; i < cols.Count; i++)
                        {
                            s3 = s3.Replace(string.Format("sdp_col{0}", i + 1), dr[cols[i]].ToString());
                        }
                        sum += Convert.ToDecimal(dt.Compute(s3, ""));
                    }
                    result = sum;
                }
                else if (string.Compare(funcnm, "Avg", true) == 0)
                {
                    decimal sum = 0;
                    string s3 = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr.RowState == DataRowState.Deleted) continue;
                        s3 = s2;
                        for (int i = 0; i < cols.Count; i++)
                        {
                            s3 = s3.Replace(string.Format("sdp_col{0}", i + 1), dr[cols[i]].ToString());
                        }
                        sum += Convert.ToDecimal(dt.Compute(s3, ""));
                    }
                    if (dt.Rows.Count == 0) result = 0;
                    else
                        result = sum / dt.Rows.Count;
                }
                else if (string.Compare(funcnm, "Count", true) == 0)
                {
                    result = dt.Rows.Count;
                }
                else if (string.Compare(funcnm, "GetDesc", true) == 0)
                {
                    result = string.Format("{2}_{0}_{1}", dt.TableName, cols[0],SysConstManage .sdp_desc);
                }
                #endregion 
                express = express.Remove(index - funcnm.Length, leng + 1);
                express = express.Insert(index - funcnm.Length, result.ToString ());
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>获取自定义系统函数</summary>
        /// <returns></returns>
        public static List<LibdefFunc> GetLibdefFuncs()
        {
            List<LibdefFunc> result = new List<LibdefFunc>();
            result.Add(new LibdefFunc { FuncNm = "Sum", FuncDesc = "求和" });
            result.Add(new LibdefFunc { FuncNm = "Avg", FuncDesc = "求平均值" });
            result.Add(new LibdefFunc { FuncNm = "Count", FuncDesc = "求表行总数" });
            result.Add(new LibdefFunc { FuncNm = "GetDesc", FuncDesc = "取字段描述" });
            return result;
        }
    }

    /// <summary> 验证失败信息 </summary>
    public class ValidateMessage
    {
        public string MsgCode { get; set; }
        public object[] MsgParams { get; set; }
    }
}
