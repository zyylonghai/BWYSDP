using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWYResFactory;
using SDPCRL.CORE;
using System.Text.RegularExpressions;
using SDPCRL.COM.ModelManager.FormTemplate;
using SDPCRL.COM.ModelManager;

namespace SDPCRL.DAL.COM
{
    public class SQLBuilder
    {
        private string _id = string.Empty;
        private bool _mark = true;
        #region 构造函数
        public SQLBuilder()
        {

        }
        /// <summary> </summary>
        /// <param name="id">ProgId或者DSID</param>
        /// <param name="mark">是否为DSID，默认true</param>
        public SQLBuilder(string id, bool mark = true)
        {
            this._id = id;
            this._mark = mark;
        }
        #endregion 
        public string GetSQL(string tableNm, string[] fields, string whereStr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ResFactory.ResManager.SQLSelect);
            foreach (string field in fields)
            {
                if (builder.Length != ResFactory.ResManager.SQLSelect.Length)
                {
                    builder.Append(SysConstManage.Comma);
                }
                builder.AppendFormat(" {0}", field);
            }

            builder.AppendFormat(" {0}", ResFactory.ResManager.SQLFrom);
            builder.AppendFormat(" {0}", tableNm);
            builder.AppendFormat(" {0} {1}", ResFactory.ResManager.SQLWhere, whereStr);
            return builder.ToString();
        }


        public string GetSQL(string tableNm, string[] fields, WhereObject where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ResFactory.ResManager.SQLSelect);
            if (fields != null)
            {
                foreach (string field in fields)
                {
                    if (builder.Length != ResFactory.ResManager.SQLSelect.Length)
                    {
                        builder.Append(SysConstManage.Comma);
                    }
                    builder.AppendFormat(" {0}", field);
                }
            }
            if (string.IsNullOrEmpty(this._id))
            {
                if (builder.Length == ResFactory.ResManager.SQLSelect.Length)
                {
                    builder.AppendFormat(" {0}", SysConstManage.Asterisk);
                }
                builder.AppendFormat(" {0}", ResFactory.ResManager.SQLFrom);
                builder.AppendFormat(" {0}", tableNm);
                //if (!string.IsNullOrEmpty(where.WhereFormat))
                //{
                //    return string.Format("EXEC sp_executesql N'{0} where {1}',{2}", builder.ToString(), where.WhereFormat, where.ValueTostring);
                //}
                //return string.Format("EXEC sp_executesql N'{0}'", builder.ToString());
            }
            else
            {
                LibDataSource ds = null;
                if (this._mark)
                {
                    ds = ModelManager.GetDataSource(this._id);
                }
                else
                {
                    LibFormPage form = ModelManager.GetFormSource(this._id);
                    ds = ModelManager.GetDataSource(form.DSID);
                }
                if (ds != null)
                {
                    List<LibDataTableStruct> list = new List<LibDataTableStruct>();
                    StringBuilder joinstr = new StringBuilder();
                    StringBuilder joinfield = null;
                    StringBuilder allfields = new StringBuilder();
                    foreach (LibDefineTable item in ds.DefTables)
                    {
                        list.AddRange(item.TableStruct.ToArray());
                    }
                    var tb = list.FirstOrDefault(i => i.Name == tableNm && i.Ignore);
                    char tbaliasnm = LibSysUtils.ToCharByTableIndex(tb.TableIndex);
                    #region 组织要查询表的字段
                    if (builder.Length == ResFactory.ResManager.SQLSelect.Length)
                    {
                        foreach (LibField f in tb.Fields)
                        {
                            if (!f.IsActive) continue;
                            if (allfields.Length > 0)
                            {
                                allfields.Append(SysConstManage.Comma);
                            }
                            allfields.AppendFormat(" {0}.{1}", tbaliasnm, f.Name);
                        }
                    }
                    #endregion
                    if (tb != null)
                    {
                        var relatetbs = list.Where(i => i.JoinTableIndex == tb.TableIndex && i.TableIndex !=tb.TableIndex).ToList();
                        if (relatetbs != null)
                        {
                            foreach (var jointb in relatetbs)
                            {
                                #region 组织joinstr语句
                                joinstr.AppendFormat(" {0} {1} {2} {3} ",
                                                     ResFactory.ResManager.SQLLeftJoin,
                                                     jointb.Name,
                                                     LibSysUtils .ToCharByTableIndex (jointb.TableIndex),
                                                     //(char)(jointb.TableIndex+65),
                                                     ResFactory.ResManager.SQLOn);
                                joinfield = new StringBuilder();
                                foreach (var relatfield in jointb.JoinFields)
                                {
                                    if (joinfield.Length > 0)
                                    {
                                        joinfield.Append(ResFactory.ResManager.SQLAnd); ;
                                    }
                                    joinfield.AppendFormat(" {0}.{1}={2}.{3} ", tbaliasnm, relatfield, LibSysUtils.ToCharByTableIndex(jointb.TableIndex), relatfield);
                                }
                                joinstr.Append(joinfield.ToString());
                                joinstr.AppendLine();
                                #endregion

                                #region 取关联表的字段
                                if (builder.Length == ResFactory.ResManager.SQLSelect.Length)
                                {
                                    foreach (LibField f2 in jointb.Fields)
                                    {
                                        if (!f2.IsActive || jointb.JoinFields.Contains(f2.Name)) continue;
                                        allfields.AppendFormat("{0}{1}.{2}", SysConstManage.Comma, LibSysUtils.ToCharByTableIndex(jointb.TableIndex), f2.Name);
                                    }
                                }
                                #endregion
                            }
                        }
                        if (builder.Length == ResFactory.ResManager.SQLSelect.Length)
                        {
                            builder.Append(allfields.ToString());
                        }
                        builder.AppendFormat(" {0}", ResFactory.ResManager.SQLFrom);
                        builder.AppendFormat(" {0} {1}", tableNm, tbaliasnm);
                        builder.Append(joinstr.ToString());
                    }
                }
            }
            if (!string.IsNullOrEmpty(where.WhereFormat))
            {
                return string.Format("EXEC sp_executesql N'{0} where {1}',{2}", builder.ToString(), where.WhereFormat, where.ValueTostring);
            }
            return string.Format("EXEC sp_executesql N'{0}'", builder.ToString());
        }

        public string GetSQLBydeftableNm(string deftbnm,string[] fields, WhereObject where)
        {
            StringBuilder sql = new StringBuilder();
            LibFormPage form = ModelManager.GetFormSource(this._id);
            var datasourse = ModelManager.GetDataSource(form.DSID);
            foreach (LibDefineTable item in datasourse.DefTables)
            {
                if (item.TableName != deftbnm) continue;
                foreach (LibDataTableStruct tbstruct in item.TableStruct)
                {

                    if (tbstruct.TableIndex != tbstruct.JoinTableIndex) {

                    }
                }
                break;
            }
            return sql.ToString();
        }

        public string GetSQL()
        {
            StringBuilder sql = new StringBuilder();
            LibFormPage form = ModelManager.GetFormSource(this._id);
            var datasourse = ModelManager.GetDataSource(form.DSID);
            if (datasourse != null)
            {
                foreach (LibDefineTable deftb in datasourse.DefTables)
                {
                    if (deftb.TableStruct == null) continue;
                    foreach (LibDataTableStruct tbstruct in deftb.TableStruct)
                    {
                        if (!tbstruct.Ignore) continue;

                    }
                }
            }
            return sql.ToString();
        }

        public WhereObject Where(string format, params object[] values)
        {
            WhereObject wobj = new WhereObject();
            wobj.WhereFormat = format;
            wobj.Values = values;
            return wobj;
        }

        #region 私有函数
        //private void DoGetSQLField(StringBuilder builder,string field)
        //{
        //    if (builder.Length != ResFactory.ResManager.SQLSelect.Length)
        //    {
        //        builder.Append(SysConstManage.Comma);
        //    }
        //    builder.AppendFormat(" {0}", field);
        //}
        #endregion
    }
   public class WhereObject
    {
        private string _whereformat = string.Empty;
        private string[] _params;
        public string WhereFormat
        {
            get
            {
                MatchCollection matchs = Regex.Matches(_whereformat, @"{\w*}+");
                int index = 0;
                if (matchs.Count > 0)
                    _params = new string[matchs.Count];
                foreach (Match item in matchs)
                {
                    index = Convert.ToInt32(item.Value.Replace("{", "").Replace("}", ""));
                    _params[index] = string.Format("@V{0}", index);
                    _whereformat = _whereformat.Replace(item.Value, _params[index]);
                }
                return _whereformat;
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
                if (_params !=null && _params.Length > 0)
                {
                    StringBuilder partype = new StringBuilder();
                    StringBuilder val = new StringBuilder();
                    for (int n = 0; n < _params.Length; n++)
                    {
                        if (partype.Length > 0)
                        {
                            partype.Append(",");
                            val.Append(",");
                        }
                        switch (Values[n].GetType().Name)
                        {
                            case "String":
                                partype.AppendFormat("{0} nvarchar({1})", _params[n], Values[Convert.ToInt32(_params[n].Substring(2))].ToString().Length);
                                val.AppendFormat("{0}='{1}'", _params[n], Values[Convert.ToInt32(_params[n].Substring(2))]);
                                break;
                            case "Int32":
                                partype.AppendFormat("{0} int)", _params[n]);
                                val.AppendFormat("{0}={1}", _params[n], Values[Convert.ToInt32(_params[n].Substring(2))]);
                                break;
                            case "DateTime":
                                break;
                        }
                    }
                    return string.Format("N'{0}',{1}", partype.ToString(), val.ToString());
                }
                else
                    return string.Empty;
            }
        }
    }

}
