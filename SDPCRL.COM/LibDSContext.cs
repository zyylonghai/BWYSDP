using BWYResFactory;
using SDPCRL.COM.ModelManager;
using SDPCRL.COM.ModelManager.FormTemplate;
using SDPCRL.CORE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    public class LibDSContext
    {
        LibDataSource _ds = null;
        LibTableObj[] _tableObjs = null;
        string _dsid = string.Empty;
        //Dictionary<string, Dictionary<string, object>> _allcols = null;
        LibDataSource DataSource { 
            get {
                if (_ds == null)
                {
                    _ds= SDPCRL.COM.ModelManager.ModelManager.GetDataSource(_dsid);
                    CachHelp cach = new CachHelp();
                    InitialContext();
                    cach.AddCachItem(string.Format("{0}_{1}", _dsid, SysConstManage.TBSchemasuffix), _ds, DateTimeOffset.Now.AddMinutes(30));
                }
                return _ds;
            } 
        }
        public LibTableObj this[int Tableindex]
        { 
            get {
                if (_tableObjs == null) return null;
                var o= _tableObjs.FirstOrDefault(i => i.TableIndex == Tableindex);
                if (o != null) return o;
                return null;
            } 
        }
        public LibTableObj this[string TableNm]
        {
            get {
                if (_tableObjs == null) return null;
                var o = _tableObjs.FirstOrDefault(i => i.TableName == TableNm);
                if (o != null) return o;
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsid">数据源id或progid</param>
        public LibDSContext(string dsid)
        {
            this._dsid = dsid;
            CachHelp cach = new CachHelp();
            _ds = cach.GetCach(string.Format("{0}_{1}", dsid, SysConstManage.TBSchemasuffix)) as LibDataSource;
            if (_ds == null)
            {
                _ds = SDPCRL.COM.ModelManager.ModelManager.GetDataSource(dsid);
                if (_ds == null)
                {
                    LibFormPage form = SDPCRL.COM.ModelManager.ModelManager.GetFormSource(dsid);
                    _ds = SDPCRL.COM.ModelManager.ModelManager.GetDataSource(form.DSID);
                }
                //101：数据源:{0} 不存在
                if (_ds == null) throw new LibExceptionBase(101, dsid);
                //100：没有表结构
                if (_ds.DefTables == null) throw new LibExceptionBase(100);
                cach.AddCachItem(string.Format("{0}_{1}", dsid, SysConstManage.TBSchemasuffix), _ds, DateTimeOffset.Now.AddMinutes(30));
            }
            InitialContext();
        }

        //public LibTableObj GetTableObj(string tableNm)
        //{
        //    LibTableObj tableObj = null;
        //    foreach (LibDefineTable deftb in _ds.DefTables)
        //    {
        //        if (deftb.TableStruct == null) continue;
        //        foreach (LibDataTableStruct tb in deftb.TableStruct)
        //        {
        //            if (tb.Name.ToUpper() != tableNm.ToUpper()) continue;
        //            tableObj = new LibTableObj(new CreateTableSchemaHelp().DoCreateTableShema(tb));
        //            break;
        //        }
        //    }
        //    return tableObj;
        //}

        /// <summary>
        /// 取查询的sql语法
        /// </summary>
        /// <param name="tableNm"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="IsJoinRelateTable"></param>
        /// <param name="IsJoinFromSourceField"></param>
        /// <returns></returns>
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
            if (this.DataSource==null)
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
                //LibDataSource ds = null;
                //if (this._mark)
                //{
                //    ds = ModelManager.GetDataSource(this._id);
                //}
                //else
                //{
                //    LibFormPage form = ModelManager.GetFormSource(this._id);
                //    ds = ModelManager.GetDataSource(form.DSID);
                //}
                //if (this._ds != null)
                //{
                    DoGetSQL(builder, tableNm, this.DataSource, where, false, IsJoinRelateTable, IsJoinFromSourceField);
                //}
            }
            if (!string.IsNullOrEmpty(where.WhereFormat))
            {
                return string.Format("EXEC sp_executesql N'{0} where {1}',{2}", builder.ToString(), where.WhereFormat, where.ValueTostring);
            }
            return string.Format("EXEC sp_executesql N'{0}'", builder.ToString());
        }
        /// <summary>
        /// 取分页查询的sql语法
        /// </summary>
        /// <param name="tableNm"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="IsJoinRelateTable"></param>
        /// <param name="IsJoinFromSourceField"></param>
        /// <returns></returns>
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
            if (this.DataSource==null)
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
                //LibDataSource ds = null;
                //if (this._mark)
                //{
                //    ds = ModelManager.GetDataSource(this._id);
                //}
                //else
                //{
                //    LibFormPage form = ModelManager.GetFormSource(this._id);
                //    ds = ModelManager.GetDataSource(form.DSID);
                //}
                //if (ds != null)
                //{
                    DoGetSQL(builder, tableNm, this.DataSource, where, true, IsJoinRelateTable, IsJoinFromSourceField);
                //}
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
        //public string GetSQL(WhereObject where)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    if (this.DataSource==null) return string.Empty;
        //    StringBuilder fields = null;

        //    //LibFormPage form =SDPCRL.COM.ModelManager.ModelManager.GetFormSource(this._id);
        //    //var datasourse = SDPCRL.COM.ModelManager.ModelManager.GetDataSource(form.DSID);
        //    if (this.DataSource != null)
        //    {
        //        foreach (LibDefineTable deftb in this.DataSource.DefTables)
        //        {
        //            if (deftb.TableStruct == null) continue;
        //            foreach (LibDataTableStruct tbstruct in deftb.TableStruct)
        //            {
        //                if (!tbstruct.Ignore) continue;
        //                fields = new StringBuilder();
        //                char tbaliasnm = LibSysUtils.ToCharByTableIndex(tbstruct.TableIndex);
        //                foreach (LibField f in tbstruct.Fields)
        //                {
        //                    if (fields.Length > 0)
        //                        fields.Append(SysConstManage.Comma);
        //                    fields.AppendFormat("{0}.{1}", tbaliasnm, string.IsNullOrEmpty(f.AliasName) ? f.Name : f.AliasName);
        //                }
        //                if (fields.Length > 0)
        //                {
        //                    if (string.IsNullOrEmpty(where.WhereFormat))
        //                        sql.AppendFormat("select {0} from {1} {2}", fields.ToString(), tbstruct.Name, tbaliasnm);
        //                    else
        //                        sql.AppendFormat("select {0} from {1} {2} where {3}", fields.ToString(), tbstruct.Name, tbaliasnm, where.WhereFormat);
        //                    sql.AppendLine();
        //                }
        //            }
        //        }
        //    }
        //    return sql.ToString();
        //}


        public WhereObject Where(string format, params object[] values)
        {
            WhereObject wobj = new WhereObject();
            wobj.WhereFormat = format;
            wobj.Values = values;
            return wobj;
        }

        #region 私有函数
        private void DoGetSQL(StringBuilder builder, string tableNm, LibDataSource ds, WhereObject where, bool page = false, bool IsJoinRelateTable = true, bool IsJoinFromSourceField = true)
        {
            List<LibDataTableStruct> list = new List<LibDataTableStruct>();
            StringBuilder joinstr = new StringBuilder();
            StringBuilder joinfield = null;
            StringBuilder allfields = new StringBuilder();
            //Dictionary<string, int> tablealiasmdic = new Dictionary<string, int>();
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
                                    if (allfields.ToString().ToUpper().Contains(string.Format(".{0}", f2.Name.ToUpper()))&&string.IsNullOrEmpty(f2.AliasName))
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
                        string tbnm = this[tableNm].GetTBAliasmn(fromfield.FromDataSource, fromfield.FromTableIndex, f.Name);
                        //string tbnm = string.Format("{0}{1}", tbaliasnm, LibSysUtils.ToCharByTableIndex(fromfield.FromTableIndex));
                        //if (tablealiasmdic.ContainsKey(tbnm))
                        //{
                        //    tbnm = string.Format("{0}{1}", tbnm, tablealiasmdic[tbnm]++);
                        //}
                        //else
                        //{
                        //    tablealiasmdic.Add(tbnm, 0);
                        //}
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
                            DoJoinOnCondition(fromfield.JoinOnCondition, where);
                        }
                        #endregion 
                        joinstr.Append(joinfield.ToString());
                        joinstr.AppendLine();
                        if (IsJoinFromSourceField)
                        {
                            #region 取来源表所在数据源的 与来源表关联的表。
                            LibDataSource fromSouceDS = SDPCRL.COM.ModelManager.ModelManager.GetDataSource(fromfield.FromDataSource);
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
                                        string tbnm2 = this[tableNm].GetTBAliasmn(fromfield.FromDataSource, jointb.TableIndex, f.Name);
                                        //string tbnm2 = string.Format("{0}{1}", tbaliasnm, LibSysUtils.ToCharByTableIndex(jointb.TableIndex));
                                        //if (tablealiasmdic.ContainsKey(tbnm2))
                                        //{
                                        //    tbnm2 = string.Format("{0}{1}", tbnm2, tablealiasmdic[tbnm2]++);
                                        //}
                                        //else
                                        //{
                                        //    tablealiasmdic.Add(tbnm2, 0);
                                        //}
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
                                    tbnm = this[tableNm].GetTBAliasmn(fromfield.FromDataSource, relatef.FromTableIndex, f.Name);
                                    //tbnm = string.Format("{0}{1}", tbaliasnm, LibSysUtils.ToCharByTableIndex(relatef.FromTableIndex));
                                    //if (IsJoinFromSourceField && relatef.FromTableIndex != fromfield.FromTableIndex)
                                    //{
                                    //    tbnm = tablealiasmdic[tbnm] == 0 ? tbnm : string.Format("{0}{1}", tbnm, tablealiasmdic[tbnm]);
                                    //}
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

        private void InitialContext()
        {
            if (_tableObjs == null) _tableObjs = new LibTableObj[] { };
            Array.Clear(_tableObjs, 0, _tableObjs.Length);
            if (_ds.DefTables != null)
            {
                CreateTableSchemaHelp tableSchemaHelp = new CreateTableSchemaHelp();
                foreach (LibDefineTable deftb in _ds.DefTables)
                {
                    if (deftb == null || deftb.TableStruct == null) continue;
                    foreach (LibDataTableStruct tbstruct in deftb.TableStruct)
                    {
                        Array.Resize(ref _tableObjs, _tableObjs.Length + 1);
                        this._tableObjs[_tableObjs.Length - 1] = new LibTableObj(tableSchemaHelp.DoCreateTableShema(tbstruct, true, true));
                        this._tableObjs[_tableObjs.Length - 1].FromDSID = _ds.DSID;
                        //this._tableObjs.Add(new LibTableObj(tableSchemaHelp.DoCreateTableShema(tbstruct)));
                    }
                }
            }
        }
        #endregion
    }
}
