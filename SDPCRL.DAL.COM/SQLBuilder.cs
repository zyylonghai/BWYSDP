﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWYResFactory;
using SDPCRL.CORE;
using System.Text.RegularExpressions;
using SDPCRL.COM.ModelManager.FormTemplate;
using SDPCRL.COM.ModelManager;
using SDPCRL.COM;

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


        public string GetSQL(string tableNm, string[] fields, WhereObject where, bool IsJoinRelateTable = true, bool IsJoinFromSourceField = true)
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
                    DoGetSQL(builder, tableNm, ds, where,null , false, IsJoinRelateTable, IsJoinFromSourceField);
                }
            }
            if (where !=null && !string.IsNullOrEmpty(where.WhereFormat))
            {
                return string.Format("EXEC sp_executesql N'{0} where {1}',{2}", builder.ToString(), where.WhereFormat, where.ValueTostring);
            }
            return string.Format("EXEC sp_executesql N'{0}'", builder.ToString());
        }
        //public string GetSQL(string tableNm, string[] fields, WhereObject where, bool IsContainRelateField = true)
        //{

        //}
        public string GetSQLByPage(string tableNm, string[] fields, WhereObject where, int pageindex, int pagesize, bool IsJoinRelateTable = true, bool IsJoinFromSourceField = true)
        {
            StringBuilder builder = new StringBuilder();
            InternalGetSQL(builder, tableNm, fields, null, where, true, IsJoinRelateTable, IsJoinFromSourceField,false);
            #region 旧代码
            //builder.Append(ResFactory.ResManager.SQLSelect);
            //if (fields != null)
            //{
            //    foreach (string field in fields)
            //    {
            //        if (builder.Length != ResFactory.ResManager.SQLSelect.Length)
            //        {
            //            builder.Append(SysConstManage.Comma);
            //        }
            //        builder.AppendFormat(" {0}", field);
            //    }
            //}
            //if (string.IsNullOrEmpty(this._id))
            //{
            //    if (builder.Length == ResFactory.ResManager.SQLSelect.Length)
            //    {
            //        builder.AppendFormat(" {0}", SysConstManage.Asterisk);
            //    }

            //    //builder.AppendFormat("{0}ROW_NUMBER()OVER(order by A.BillNo) as rownumber");
            //    builder.AppendFormat(" {0}", ResFactory.ResManager.SQLFrom);
            //    builder.AppendFormat(" {0}", tableNm);
            //}
            //else
            //{
            //    LibDataSource ds = null;
            //    if (this._mark)
            //    {
            //        ds = ModelManager.GetDataSource(this._id);
            //    }
            //    else
            //    {
            //        LibFormPage form = ModelManager.GetFormSource(this._id);
            //        ds = ModelManager.GetDataSource(form.DSID);
            //    }
            //    if (ds != null)
            //    {
            //        DoGetSQL(builder, tableNm, ds, where,null , true, IsJoinRelateTable, IsJoinFromSourceField);
            //    }
            //}
            #endregion 

            string coutstr = builder.ToString();
            int beginindex = coutstr.IndexOf(ResFactory.ResManager.SQLSelect)+ ResFactory.ResManager.SQLSelect.Length;
            int endindex = coutstr.IndexOf(ResFactory.ResManager.SQLFrom);
            string ss = coutstr.Replace(coutstr.Substring(beginindex, endindex - beginindex), string.Format(" COUNT(1) as {0} ",SysConstManage.sdp_total_row)).ToString();

            if (where !=null && !string.IsNullOrEmpty(where.WhereFormat))
            {
                return string.Format("EXEC sp_executesql N'select *from({0} where {1}) as temp where rownumber>={3} and rownumber<={4} {5} where {1}',{2}", 
                                      builder.ToString(), where.WhereFormat, where.ValueTostring, (pageindex - 1) * pagesize + 1, pageindex * pagesize,ss);
            }
            return string.Format("EXEC sp_executesql N'select *from({0}) as temp where rownumber>={1} and rownumber<={2} {3}'", builder.ToString(), (pageindex - 1) * pagesize + 1, pageindex * pagesize,ss);
        }

        public string GetRptSQLByPage(string tableNm, string[] fields,string[] sumaryfields,string groupfieldstr, WhereObject where, int pageindex, int pagesize)
        {
            StringBuilder builder = new StringBuilder();
            InternalGetSQL(builder, tableNm,string.IsNullOrEmpty(groupfieldstr) ? fields: groupfieldstr.Split(SysConstManage .Comma), sumaryfields, where, true,true , false,true);
            #region 旧代码
            //builder.Append(ResFactory.ResManager.SQLSelect);
            //if (fields != null)
            //{
            //    foreach (string field in fields)
            //    {
            //        if (builder.Length != ResFactory.ResManager.SQLSelect.Length)
            //        {
            //            builder.Append(SysConstManage.Comma);
            //        }
            //        builder.AppendFormat(" {0}", field);
            //    }
            //}
            //if (string.IsNullOrEmpty(this._id))
            //{
            //    if (builder.Length == ResFactory.ResManager.SQLSelect.Length)
            //    {
            //        builder.AppendFormat(" {0}", SysConstManage.Asterisk);
            //    }

            //    //builder.AppendFormat("{0}ROW_NUMBER()OVER(order by A.BillNo) as rownumber");
            //    builder.AppendFormat(" {0}", ResFactory.ResManager.SQLFrom);
            //    builder.AppendFormat(" {0}", tableNm);
            //}
            //else
            //{
            //    LibDataSource ds = null;
            //    if (this._mark)
            //    {
            //        ds = ModelManager.GetDataSource(this._id);
            //    }
            //    else
            //    {
            //        LibFormPage form = ModelManager.GetFormSource(this._id);
            //        ds = ModelManager.GetDataSource(form.DSID);
            //    }
            //    if (ds != null)
            //    {
            //        DoGetSQL(builder, tableNm, ds, where, sumaryfields, true, true, false);
            //    }
            //}
            #endregion 
            string coutstr = builder.ToString();
            int beginindex = coutstr.IndexOf(ResFactory.ResManager.SQLSelect) + ResFactory.ResManager.SQLSelect.Length;
            int endindex = coutstr.IndexOf(ResFactory.ResManager.SQLFrom);
            StringBuilder summarybuilder = new StringBuilder();
            StringBuilder summarybuilder2 = new StringBuilder();
            //StringBuilder groupbuilder = new StringBuilder("Group by ");
            if (sumaryfields != null) //汇总字段
            {
                foreach (string field in sumaryfields)
                {
                    summarybuilder.AppendFormat(",SUM({0}) as {1}{2}", field, SysConstManage.sdp_summaryprefix, field.Substring(field.IndexOf(".") + 1));
                    summarybuilder2.AppendFormat(",0 as {0}{1}", SysConstManage.sdp_summaryprefix, field.Substring(field.IndexOf(".") + 1));
                }
            }
            string ss = coutstr.Replace(
                                  coutstr.Substring(beginindex, endindex - beginindex),
                                  string.IsNullOrEmpty(groupfieldstr)? string.Format(" COUNT(1) as {0} {1} ", SysConstManage.sdp_total_row,summarybuilder.ToString ()):string.Format(" {0} ", groupfieldstr)).ToString();

            if (where != null && !string.IsNullOrEmpty(where.WhereFormat))
            {
                if (!string.IsNullOrEmpty(groupfieldstr))
                {
                    string s1 = "ROW_NUMBER()OVER";
                    endindex = builder.ToString().IndexOf(s1);
                    string orderbystr = builder.ToString().Substring(endindex + s1.Length);
                    string[] orders = orderbystr.Split('(', ')');
                    if (orders != null && orders.Length > 1)
                    {
                        orderbystr = orders[1];
                        if (!groupfieldstr.Contains(orderbystr))
                        {
                            builder.Replace(builder.ToString().Substring(endindex + s1.Length, orderbystr.Length + orders[0].Length + 1), string.Format("(order by {0} ", groupfieldstr));
                        }
                    }
                    builder.Replace(builder.ToString().Substring(beginindex, endindex - beginindex), string.Format(" {0},", groupfieldstr));
                    return string.Format("EXEC sp_executesql N'select *from({0} where {1} Group by {6}) as temp where rownumber>={3} and rownumber<={4} SELECT COUNT(1) as {7} {8} from ({5} where {1} group by {6}) as temp2',{2}",
                        builder.ToString(), where.WhereFormat, where.ValueTostring, (pageindex - 1) * pagesize + 1, pageindex * pagesize, ss,groupfieldstr, SysConstManage.sdp_total_row, summarybuilder2.ToString ());
                }
                return string.Format("EXEC sp_executesql N'select *from({0} where {1}) as temp where rownumber>={3} and rownumber<={4} {5} where {1}',{2}",
                         builder.ToString(), where.WhereFormat, where.ValueTostring, (pageindex - 1) * pagesize + 1, pageindex * pagesize, ss);
            }
            if (!string.IsNullOrEmpty(groupfieldstr))
            {
                string s1 = "ROW_NUMBER()OVER";
                endindex = builder.ToString().IndexOf(s1);
                string orderbystr = builder.ToString().Substring(endindex + s1.Length);
                string[] orders= orderbystr.Split('(', ')');
                if (orders != null && orders.Length > 1)
                {
                    orderbystr = orders[1];
                    if (!groupfieldstr.Contains(orderbystr))
                    {
                        builder.Replace(builder.ToString().Substring(endindex + s1.Length, orderbystr.Length+orders[0].Length+1), string.Format("(order by {0} ",groupfieldstr));
                    }
                }
                builder.Replace(builder.ToString().Substring(beginindex, endindex - beginindex),string.Format(" {0},", groupfieldstr));
                return string.Format("EXEC sp_executesql N'select *from({0} Group by {4}) as temp where rownumber>={1} and rownumber<={2} SELECT COUNT(1) as {5} {6} from ({3} group by {4}) as temp2'", 
                    builder.ToString(), (pageindex - 1) * pagesize + 1, pageindex * pagesize, ss,groupfieldstr,SysConstManage.sdp_total_row,summarybuilder2 .ToString ());
            }
            return string.Format("EXEC sp_executesql N'select *from({0}) as temp where rownumber>={1} and rownumber<={2} {3}'", builder.ToString(), (pageindex - 1) * pagesize + 1, pageindex * pagesize, ss);
        }
        public string GetRptSQL(string tableNm, string[] fields, string[] sumaryfields, WhereObject where)
        {
            StringBuilder builder = new StringBuilder();
            InternalGetSQL(builder, tableNm, fields, sumaryfields, where, false, true, false ,true);
            if (where != null && !string.IsNullOrEmpty(where.WhereFormat))
            {
                return string.Format("EXEC sp_executesql N'{0} where {1}',{2}", builder.ToString(), where.WhereFormat, where.ValueTostring);
            }
            return string.Format("EXEC sp_executesql N'{0}'", builder.ToString());
        }
        /// <summary>用于取整个数据源的查询语法</summary>
        /// <param name="where">指主表的条件</param>
        /// <returns></returns>
        public string GetSQL(WhereObject where)
        {
            StringBuilder sql = new StringBuilder();
            if (string.IsNullOrEmpty(this._id)) return string.Empty;
            StringBuilder fields = null;

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
                        fields = new StringBuilder();
                        char tbaliasnm = LibSysUtils.ToCharByTableIndex(tbstruct.TableIndex);
                        foreach (LibField f in tbstruct.Fields)
                        {
                            if (fields.Length > 0)
                                fields.Append(SysConstManage.Comma);
                            fields.AppendFormat("{0}.{1}", tbaliasnm, string.IsNullOrEmpty(f.AliasName) ? f.Name : f.AliasName);
                        }
                        if (fields.Length > 0)
                        {
                            if (string.IsNullOrEmpty(where.WhereFormat))
                                sql.AppendFormat("select {0} from {1} {2}", fields.ToString(), tbstruct.Name, tbaliasnm);
                            else
                                sql.AppendFormat("select {0} from {1} {2} where {3}", fields.ToString(), tbstruct.Name, tbaliasnm, where.WhereFormat);
                            sql.AppendLine();
                        }
                    }
                }
            }
            return sql.ToString();
        }

        public string GetUpdateSQL(string tableNm, UpdateObject updatefield, WhereObject where)
        {
            StringBuilder builder = new StringBuilder();
            //string updatefieldformat = string.Empty;
            string whereformat = string.Empty;
            builder.AppendFormat("{0} {1} Set ", ResFactory.ResManager.SQLUpdate, tableNm);
            if (updatefield == null) throw new LibExceptionBase("param updatefield is not null");
            if (!string.IsNullOrEmpty(updatefield.UpdateFieldFormat))
            {
                where.AppendWhereFormat(" ", updatefield.UpdateFieldFormat, updatefield.Values);
                updatefield.WhereFormat = where.GetUpdatefieldformat();
                builder.Append(updatefield.UpdateFieldSQL);
            }
            whereformat = where.WhereFormat.Replace(updatefield.UpdateFieldSQL, "");
            if (!string.IsNullOrEmpty(whereformat))
            {
                return string.Format("EXEC sp_executesql N'{0} where {1}',{2}", builder.ToString(), whereformat, where.ValueTostring);
            }
            else if (!string.IsNullOrEmpty(updatefield.UpdateFieldFormat))
            {
                return string.Format("EXEC sp_executesql N'{0}',{1}", builder.ToString(), where.ValueTostring);
            }
            return string.Format("EXEC sp_executesql N'{0}'", builder.ToString());
        }

        public WhereObject Where(string format, params object[] values)
        {
            WhereObject wobj = new WhereObject();
            wobj.WhereFormat = format;
            wobj.Values = values;
            return wobj;
        }

        public UpdateObject UpdateField(string format, params object[] values)
        {
            UpdateObject updateobj = new UpdateObject();
            updateobj.WhereFormat = format;
            updateobj.Values = values;
            return updateobj;
        }

        #region 私有函数
        private void DoGetSQL(StringBuilder builder, string tableNm, LibDataSource ds, WhereObject where,string[] sumaryfields, bool page = false, bool IsJoinRelateTable = true, bool IsJoinFromSourceField = true,bool IsRpt=false)
        {
            List<LibDataTableStruct> list = new List<LibDataTableStruct>();
            StringBuilder joinstr = new StringBuilder();
            StringBuilder joinfield = null;
            StringBuilder allfields = new StringBuilder();
            Dictionary<string, string> tablealiasmdic = new Dictionary<string, string>();
            foreach (LibDefineTable item in ds.DefTables)
            {
                if (item.TableStruct == null) continue;
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
                #region 日志列
                if (allfields.Length > 0)
                {
                    allfields.Append(SysConstManage.Comma);
                }
                allfields.AppendFormat(" {0}.{1}", tbaliasnm, SysConstManage.Sdp_LogId);
                #endregion 
            }
            #endregion
            if (tb != null)
            {
                if (IsJoinRelateTable)
                {
                    var relatetbs = list.Where(i => i.JoinTableIndex == tb.TableIndex && i.TableIndex != tb.TableIndex && i.Ignore).ToList();
                    List<int> tbindexs = null;
                    //char tbaliasnm2 = tbaliasnm;
                    while (relatetbs != null && relatetbs.Count > 0)
                    {
                        tbindexs = new List<int>();
                        foreach (var jointb in relatetbs)
                        {
                            tbindexs.Add(jointb.TableIndex);
                            #region 组织joinstr语句
                            joinstr.AppendFormat(" {0} {1} {2} {3} ",
                                                 ResFactory.ResManager.SQLLeftJoin,
                                                 jointb.Name,
                                                 LibSysUtils.ToCharByTableIndex(jointb.TableIndex),
                                                 //(char)(jointb.TableIndex+65),
                                                 ResFactory.ResManager.SQLOn);
                            joinfield = new StringBuilder();
                            foreach (var relatfield in jointb.JoinFields)
                            {
                                if (joinfield.Length > 0)
                                {
                                    joinfield.Append(ResFactory.ResManager.SQLAnd); ;
                                }
                                LibField libfd = jointb.Fields.FindFirst("Name", relatfield);
                                //if (string.IsNullOrEmpty(libfd.RelatePrimarykey))
                                joinfield.AppendFormat(" {0}.{1}={2}.{3} ",
                                    LibSysUtils.ToCharByTableIndex(jointb.JoinTableIndex),
                                    string.IsNullOrEmpty(libfd.RelatePrimarykey) ? relatfield : libfd.RelatePrimarykey,
                                    LibSysUtils.ToCharByTableIndex(jointb.TableIndex),
                                    relatfield);
                                //else 

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
                                    if (allfields.ToString().ToUpper().Contains(string.Format(".{0}", f2.Name.ToUpper()))&& string.IsNullOrEmpty(f2.AliasName))
                                    {
                                        allfields.AppendFormat("{0}{1}.{2} as {3}", SysConstManage.Comma, LibSysUtils.ToCharByTableIndex(jointb.TableIndex), f2.Name, string.Format("{0}{2}{1}", LibSysUtils.ToCharByTableIndex(jointb.TableIndex), f2.Name, SysConstManage.Underline));
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(f2.AliasName))
                                        {
                                            allfields.AppendFormat("{0}{1}.{2} as {3}", SysConstManage.Comma, LibSysUtils.ToCharByTableIndex(jointb.TableIndex), f2.Name, f2.AliasName);
                                        }
                                        else
                                            allfields.AppendFormat("{0}{1}.{2}", SysConstManage.Comma, LibSysUtils.ToCharByTableIndex(jointb.TableIndex), f2.Name);
                                    }
                                }
                            }
                            #endregion
                        }
                        relatetbs = list.Where(i => tbindexs.Contains(i.JoinTableIndex) && i.TableIndex != i.JoinTableIndex && i.Ignore).ToList();
                        //tbaliasnm2=
                    }
                }
                #region 字段上的来源表。
                var relatefields = tb.Fields.ToArray().Where(i => i.SourceField != null);
                List<string> vals = new List<string>();
                foreach (LibField f in relatefields)
                {
                    foreach (LibFromSourceField fromfield in f.SourceField)
                    {
                        string key = string.Format("{0}{1}{2}", f.Name, fromfield.FromDataSource, fromfield.FromTableIndex);
                        string tbnm = string.Format("{0}{1}", tbaliasnm, LibSysUtils.ToCharByTableIndex(fromfield.FromTableIndex));
                        if (!tablealiasmdic.ContainsKey(key))
                        {
                            //vals.Add(tbnm);
                            int cout = vals.Where(i => i == tbnm).Count();
                            if (cout > 0)
                            {
                                tbnm = string.Format("{0}{1}", tbnm, cout);
                                //tablealiasmdic.Add(key, string.Format("{0}", tbnm));
                            }
                            vals.Add(tbnm);
                            tablealiasmdic.Add(key, tbnm);
                            //tablealiasmdic.Add(key,string.Format( tbnm);
                            //tablealiasmdic[tbnm]++;
                            //tbnm = string.Format("{0}{1}", tbnm, tablealiasmdic[tbnm]+1);
                        }
                        else
                        {
                            ////int cout = vals.Where(i => i == tbnm).Count();
                            tbnm = tablealiasmdic[key];
                            //tablealiasmdic.Add(tbnm, 0);
                        }
                       
                        #region 组织joinstr语句
                        joinstr.AppendFormat(" {0} {1} {2} {3} ",
                                         ResFactory.ResManager.SQLLeftJoin,
                                         fromfield.FromStructTableNm,
                                         tbnm,
                                         //(char)(jointb.TableIndex+65),
                                         ResFactory.ResManager.SQLOn);
                        joinfield = new StringBuilder();
                        if (joinfield.Length > 0)
                        {
                            joinfield.Append(ResFactory.ResManager.SQLAnd);
                        }
                        joinfield.AppendFormat(" {0}.{1}={2}.{3} ",
                            tbaliasnm,
                            f.Name,
                            tbnm,
                            fromfield.FromFieldNm);
                        #region 处理JoinOnCondition
                        if (!string.IsNullOrEmpty(fromfield.JoinOnCondition))
                        {
                            if (where == null) where = new WhereObject();
                            DoJoinOnCondition(fromfield.JoinOnCondition, where);
                        }
                        #endregion 
                        joinstr.Append(joinfield.ToString());
                        joinstr.AppendLine();
                        if (IsJoinFromSourceField)
                        {
                            #region 取来源表所在数据源的 与来源表关联的表。
                            LibDataSource fromSouceDS = ModelManager.GetDataSource(fromfield.FromDataSource);
                            if (fromSouceDS != null)
                            {
                                List<LibDataTableStruct> list2 = new List<LibDataTableStruct>();
                                foreach (LibDefineTable def in fromSouceDS.DefTables)
                                {
                                    if (def.TableStruct == null) continue;
                                    list2.AddRange(def.TableStruct.ToArray());
                                }
                                var relatetbs = list2.Where(i => i.JoinTableIndex == fromfield.FromTableIndex && i.TableIndex != fromfield.FromTableIndex && i.Ignore).ToList();
                                List<int> tbindexs2 = null;
                                while (relatetbs != null && relatetbs.Count > 0)
                                {
                                    tbindexs2 = new List<int>();
                                    foreach (var jointb in relatetbs)
                                    {
                                        string key2 = string.Format("{0}{1}{2}", f.Name, fromfield.FromDataSource, jointb.TableIndex);
                                        tbindexs2.Add(jointb.TableIndex);
                                        string tbnm2 = string.Format("{0}{1}", tbaliasnm, LibSysUtils.ToCharByTableIndex(jointb.TableIndex));
                                        if (!tablealiasmdic.ContainsKey(key2))
                                        {
                                            //vals.Add(tbnm2);
                                            int cout = vals.Where(i => i == tbnm2).Count();
                                            if (cout > 0)
                                            {
                                                tbnm2 = string.Format("{0}{1}", tbnm2, cout);
                                                //tablealiasmdic.Add(key, string.Format("{0}", tbnm));
                                            }
                                            tablealiasmdic.Add(key, tbnm2);
                                            //tablealiasmdic.Add(key2, tbnm2);
                                            //tablealiasmdic[tbnm2]++;
                                            //tbnm2 = string.Format("{0}{1}", tbnm2, tablealiasmdic[tbnm2]+1);
                                        }
                                        else
                                        {
                                            tbnm2 = tablealiasmdic[key2];
                                            //tablealiasmdic.Add(tbnm2, 0);
                                        }
                                        joinstr.AppendFormat(" {0} {1} {2} {3} ",
                                         ResFactory.ResManager.SQLLeftJoin,
                                         jointb.Name,
                                        tbnm2,
                                         //(char)(jointb.TableIndex+65),
                                         ResFactory.ResManager.SQLOn);
                                        joinfield = new StringBuilder();
                                        foreach (var relatfield in jointb.JoinFields)
                                        {
                                            if (joinfield.Length > 0)
                                            {
                                                joinfield.Append(ResFactory.ResManager.SQLAnd); ;
                                            }
                                            LibField libfd = jointb.Fields.FindFirst("Name", relatfield);
                                            joinfield.AppendFormat(" {0}.{1}={2}.{3} ",
                                                string.Format("{0}{1}", tbaliasnm, LibSysUtils.ToCharByTableIndex(jointb.JoinTableIndex)),
                                                string.IsNullOrEmpty(libfd.RelatePrimarykey) ? relatfield : libfd.RelatePrimarykey,
                                                string.Format("{0}{1}", tbaliasnm, LibSysUtils.ToCharByTableIndex(jointb.TableIndex)),
                                                relatfield);
                                        }
                                        joinstr.Append(joinfield.ToString());
                                        joinstr.AppendLine();
                                    }
                                    relatetbs = list2.Where(i => tbindexs2.Contains(i.JoinTableIndex) && i.TableIndex != i.JoinTableIndex && i.Ignore).ToList();
                                    //tbaliasnm2=
                                }
                            }
                            #endregion
                        }
                        if (builder.Length == ResFactory.ResManager.SQLSelect.Length || IsRpt)
                        {
                            if (fromfield.RelateFieldNm != null)
                            {
                                foreach (LibRelateField relatef in fromfield.RelateFieldNm)
                                {
                                    if (!IsJoinFromSourceField && relatef.FromTableIndex != fromfield.FromTableIndex) continue;
                                    key= string.Format("{0}{1}{2}", f.Name, fromfield.FromDataSource, relatef.FromTableIndex );
                                    //tbnm = string.Format("{0}{1}", tbaliasnm, LibSysUtils.ToCharByTableIndex(relatef.FromTableIndex));
                                    //if (IsJoinFromSourceField && relatef.FromTableIndex != fromfield.FromTableIndex)
                                    //{
                                    tbnm = tablealiasmdic[key];
                                    //}
                                    if (allfields.Length > 0)
                                    {
                                        allfields.Append(SysConstManage.Comma);
                                    }
                                    if (!string.IsNullOrEmpty(relatef.AliasName))
                                    {

                                        allfields.AppendFormat("{0}.{1} as {2}",
                                            //SysConstManage.Comma,
                                            tbnm,
                                            relatef.FieldNm,
                                            relatef.AliasName);
                                    }
                                    else
                                        allfields.AppendFormat("{0}.{1}",
                                            //SysConstManage.Comma,
                                            tbnm,
                                            relatef.FieldNm);
                                }
                            }
                        }
                        #endregion
                    }
                }
                #endregion
                if (builder.Length == ResFactory.ResManager.SQLSelect.Length ||IsRpt)
                {
                    if (builder.Length != ResFactory.ResManager.SQLSelect.Length)
                        builder.Append(SysConstManage.Comma);
                    builder.Append(allfields.ToString());
                }
                if (page) //分页
                {
                    StringBuilder orderstr = new StringBuilder();
                    foreach (string key in tb.PrimaryKey)
                    {
                        if (orderstr.Length > 0)
                        {
                            orderstr.Append(SysConstManage.Comma);
                        }
                        orderstr.AppendFormat("{0}.{1}", LibSysUtils.ToCharByTableIndex(tb.TableIndex), key);
                    }
                    builder.AppendFormat(",ROW_NUMBER()OVER(order by {0}) as rownumber ", orderstr.ToString());
                }
                //if (sumaryfields != null) //汇总字段
                //{
                //    foreach (string field in sumaryfields)
                //    {
                //        builder.AppendFormat(",SUM({0})OVER() as {1}{2}", field,SysConstManage.sdp_summaryprefix,field .Substring(field.IndexOf(".")+1));
                //    }
                //}
                builder.AppendFormat(" {0}", ResFactory.ResManager.SQLFrom);
                builder.AppendFormat(" {0} {1}", tableNm, tbaliasnm);
                builder.Append(joinstr.ToString());
            }
        }

        private void InternalGetSQL(StringBuilder builder, string tableNm, string[] fields, string[] sumaryfields, WhereObject where,bool ispage, bool IsJoinRelateTable, bool IsJoinFromSourceField,bool IsRpt)
        {
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

                //builder.AppendFormat("{0}ROW_NUMBER()OVER(order by A.BillNo) as rownumber");
                builder.AppendFormat(" {0}", ResFactory.ResManager.SQLFrom);
                builder.AppendFormat(" {0}", tableNm);
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
                    DoGetSQL(builder, tableNm, ds, where, sumaryfields, ispage, IsJoinRelateTable, IsJoinFromSourceField,IsRpt);
                }
            }
        }
        private void DoJoinOnCondition(string condition, WhereObject where)
        {
            string[] array = condition.ToUpper().Split('=');
            string[] separator = { "(", ")", "AND", "OR" };
            string itemstr = string.Empty;
            StringBuilder condformat = new StringBuilder();
            object[] valus = { };
            //string pattern = "^[0-9]*$";
            //Regex regex = new Regex(pattern);
            if (array.Length > 0)
            {
                condformat.Append(array[0]);
                int index = -1;
                int n = 0;
                Int32 val = -1;
                for (int i = 1; i < array.Length; i++)
                {
                    foreach (string sep in separator)
                    {
                        index = array[i].IndexOf(sep);
                        if (index == -1) continue;
                        break;
                    }
                    val = -1;
                    if (index != -1)
                    {
                        itemstr = array[i].Substring(0, index).Trim();
                        if (itemstr.Contains("'") || LibSysUtils.IsNumberic(itemstr, out val))
                        {
                            condformat.AppendFormat("={0} ", array[i].Replace(itemstr, "{" + n + "} "));
                            n++;
                            Array.Resize(ref valus, valus.Length + 1);
                            if (val == -1)
                            {
                                valus[valus.Length - 1] = itemstr.Trim();
                            }
                            else
                            {
                                valus[valus.Length - 1] = val;
                            }
                            //valus[valus.Length - 1] =(val == -1 ? itemstr.Trim() : val);
                        }
                        else
                        {
                            condformat.AppendFormat("={0} ", array[i]);
                        }
                    }
                    else
                    {
                        if (array[i].Contains("'") || LibSysUtils.IsNumberic(array[i], out val))
                        {
                            condformat.AppendFormat("={0} ", array[i].Replace(array[i], "{" + n + "} "));
                            n++;
                            Array.Resize(ref valus, valus.Length + 1);
                            if (val == -1)
                            {
                                valus[valus.Length - 1] = val;
                            }
                            else
                                valus[valus.Length - 1] = array[i].Replace(SysConstManage.SingleQuotes, ' ').Trim();
                        }
                        else
                            condformat.AppendFormat("={0}", array[i]);
                    }
                }
                where.AppendWhereFormat(ResFactory.ResManager.SQLAnd, string.Format("({0})", condformat), valus);
            }
        }
        #endregion
    }
    public class UpdateObject : WhereObject
    {
        public string UpdateFieldFormat
        {
            get
            {
                return _whereformat;
            }
        }

        public string UpdateFieldSQL
        {
            get
            {
                string result = _whereformat;
                MatchCollection matchs = Regex.Matches(result, patter);
                int index = 0;
                for (int i = 0; i < matchs.Count; i++)
                {
                    index = Convert.ToInt32(matchs[i].Value.Replace("{", "").Replace("}", ""));
                    result = result.Replace(matchs[i].Value, string.Format("@V{0}", index));
                }
                return result;
            }
        }
    }

}
