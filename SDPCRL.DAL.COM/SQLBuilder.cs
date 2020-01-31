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


        public string GetSQL(string tableNm, string[] fields, WhereObject where, bool IsJoinRelateTable = true,bool IsJoinFromSourceField = true)
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
                    DoGetSQL(builder, tableNm, ds, false, IsJoinRelateTable,IsJoinFromSourceField);
                }
            }
            if (!string.IsNullOrEmpty(where.WhereFormat))
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
                    DoGetSQL(builder, tableNm, ds, true,IsJoinRelateTable ,IsJoinFromSourceField);
                }
            }
            if (!string.IsNullOrEmpty(where.WhereFormat))
            {
                return string.Format("EXEC sp_executesql N'select *from({0} where {1}) as temp where rownumber>={3} and rownumber<={4}',{2}", builder.ToString(), where.WhereFormat, where.ValueTostring, (pageindex - 1) * pagesize + 1, pageindex * pagesize);
            }
            return string.Format("EXEC sp_executesql N'select *from({0}) as temp where rownumber>={1} and rownumber<={2}'", builder.ToString(), (pageindex - 1) * pagesize + 1, pageindex * pagesize);
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

        public WhereObject Where(string format, params object[] values)
        {
            WhereObject wobj = new WhereObject();
            wobj.WhereFormat = format;
            wobj.Values = values;
            return wobj;
        }

        #region 私有函数
        private void DoGetSQL(StringBuilder builder, string tableNm, LibDataSource ds, bool page = false, bool IsJoinRelateTable = true,bool IsJoinFromSourceField=true)
        {
            List<LibDataTableStruct> list = new List<LibDataTableStruct>();
            StringBuilder joinstr = new StringBuilder();
            StringBuilder joinfield = null;
            StringBuilder allfields = new StringBuilder();
            Dictionary<string, int> tablealiasmdic = new Dictionary<string, int>();
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
                                    if (allfields.ToString().Contains(string.Format(".{0}", f2.Name)))
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
                foreach (LibField f in relatefields)
                {
                    foreach (LibFromSourceField fromfield in f.SourceField)
                    {
                        string tbnm = string.Format("{0}{1}", tbaliasnm, LibSysUtils.ToCharByTableIndex(fromfield.FromTableIndex));
                        if (tablealiasmdic.ContainsKey(tbnm))
                        {
                            tbnm = string.Format("{0}{1}", tbnm, tablealiasmdic[tbnm]++);
                        }
                        else
                        {
                            tablealiasmdic.Add(tbnm, 0);
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
                                        tbindexs2.Add(jointb.TableIndex);
                                        string tbnm2 = string.Format("{0}{1}", tbaliasnm, LibSysUtils.ToCharByTableIndex(jointb.TableIndex));
                                        if (tablealiasmdic.ContainsKey(tbnm2))
                                        {
                                            tbnm2 = string.Format("{0}{1}", tbnm2, tablealiasmdic[tbnm2]++);
                                        }
                                        else
                                        {
                                            tablealiasmdic.Add(tbnm2, 0);
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
                        if (builder.Length == ResFactory.ResManager.SQLSelect.Length)
                        {
                            if (fromfield.RelateFieldNm != null)
                            {
                                foreach (LibRelateField relatef in fromfield.RelateFieldNm)
                                {
                                    if (!IsJoinFromSourceField && relatef.FromTableIndex != fromfield.FromTableIndex) continue;
                                    if (IsJoinFromSourceField&& relatef.FromTableIndex != fromfield.FromTableIndex)
                                    {
                                        tbnm = string.Format("{0}{1}", tbaliasnm, LibSysUtils.ToCharByTableIndex(relatef.FromTableIndex));
                                        tbnm = tablealiasmdic[tbnm] == 0 ? tbnm : string.Format("{0}{1}", tbnm, tablealiasmdic[tbnm]);
                                    }
                                    if (!string.IsNullOrEmpty(relatef.AliasName))
                                    {
                                        allfields.AppendFormat("{0}{1}.{2} as {3}",
                                            SysConstManage.Comma,
                                            tbnm,
                                            relatef.FieldNm,
                                            relatef.AliasName);
                                    }
                                    else
                                        allfields.AppendFormat("{0}{1}.{2}",
                                            SysConstManage.Comma,
                                            tbnm,
                                            relatef.FieldNm);
                                }
                            }
                        }
                        #endregion
                    }
                }
                #endregion
                if (builder.Length == ResFactory.ResManager.SQLSelect.Length)
                {
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
                    builder.AppendFormat(",ROW_NUMBER()OVER(order by {0}) as rownumber ,COUNT(1)OVER() as {1} ", orderstr.ToString(), SysConstManage.sdp_total_row);
                }
                builder.AppendFormat(" {0}", ResFactory.ResManager.SQLFrom);
                builder.AppendFormat(" {0} {1}", tableNm, tbaliasnm);
                builder.Append(joinstr.ToString());
            }
        }
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
                if (_params != null && _params.Length > 0)
                {
                    StringBuilder partype = new StringBuilder();
                    StringBuilder val = new StringBuilder();
                    object o = null;
                    for (int n = 0; n < _params.Length; n++)
                    {
                        if (partype.Length > 0)
                        {
                            partype.Append(",");
                            val.Append(",");
                        }
                        o = Values[Convert.ToInt32(_params[n].Substring(2))];
                        switch (Values[n].GetType().Name)
                        {
                            case "String":

                                partype.AppendFormat("{0} nvarchar({1})", _params[n], o.ToString().Length == 0 ? 1 : o.ToString().Length);
                                val.AppendFormat("{0}='{1}'", _params[n], o);
                                break;
                            case "Int32":
                                partype.AppendFormat("{0} int ", _params[n]);
                                val.AppendFormat("{0}={1}", _params[n], o);
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
