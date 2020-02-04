﻿using SDPCRL.COM.ModelManager;
using SDPCRL.CORE;
using SDPCRL.CORE.FileUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    public class CreateTableSchemaHelp
    {
        private string _root;
        public CreateTableSchemaHelp() {
        }
        public CreateTableSchemaHelp(string modelpath)
        {
            _root = modelpath;
        }

        public LibTable[] CreateTableSchema(string dsid,string package)
        {
            //List<LibTable> dts = new List<LibTable>();
            //LibTable dftb = null;
            //DataTable dt = null;
            //DataColumn col = null;
            //List<DataColumn> primarykey= null;
            //int index = 0;
            FileOperation fileoperation = new FileOperation();
            LibDataSource data = ModelManager.ModelManager.GetModelBypath<LibDataSource>(_root, dsid, package);
            return CreateTableSchema(data);
            #region 旧代码
            //if (data != null)
            //{
            //    if (data.DefTables == null) return null;
            //    foreach (LibDefineTable deftb in data.DefTables)
            //    {
            //        if (deftb.TableStruct == null) continue;
            //        dftb = new LibTable(deftb.TableName);
            //        dftb.Tables = new DataTable[deftb.TableStruct.Count];
            //        index = 0;
            //        foreach (LibDataTableStruct tb in deftb.TableStruct)
            //        {
            //            if (tb.Fields == null) continue;
            //            dt = new DataTable(tb.Name);
            //            dftb.Tables[index] = dt;
            //            primarykey = new List<DataColumn>();
            //            foreach (LibField f in tb.Fields)
            //            {
            //                col = new DataColumn(f.Name);
            //                col.Caption = f.DisplayName;
            //                switch (f.FieldType)
            //                {
            //                    case LibFieldType.Byte:
            //                        col.DataType = typeof(byte);
            //                        if (!f.AutoIncrement)
            //                            col.DefaultValue = 0;
            //                        break;
            //                    case LibFieldType.Date:
            //                        col.DataType = typeof(Date);
            //                        //if (!f.AutoIncrement)
            //                        //    col.DefaultValue = new Date { value = DateTime.Now.ToString() };
            //                        break;
            //                    case LibFieldType.DateTime:
            //                        col.DataType = typeof(DateTime);
            //                        if (!f.AutoIncrement)
            //                            col.DefaultValue = DateTime.Now;
            //                        break;
            //                    case LibFieldType.Decimal:
            //                        col.DataType = typeof(decimal);
            //                        if (!f.AutoIncrement)
            //                            col.DefaultValue = 0;
            //                        break;
            //                    case LibFieldType.Interger:
            //                        col.DataType = typeof(Int32);
            //                        if (!f.AutoIncrement)
            //                            col.DefaultValue = 0;
            //                        break;
            //                    case LibFieldType.Long:
            //                        col.DataType = typeof(long);
            //                        if (!f.AutoIncrement)
            //                            col.DefaultValue = 0;
            //                        break;
            //                    case LibFieldType.String:
            //                    case LibFieldType.Text:
            //                        col.DataType = typeof(string);
            //                        if (!f.AutoIncrement)
            //                            col.DefaultValue = string.Empty;
            //                        break;
            //                }
            //                if (tb.PrimaryKey.Contains(f.Name))//属于主键
            //                {
            //                    primarykey.Add(col);
            //                }
            //                if (f.AutoIncrement)
            //                {
            //                    col.AutoIncrement = true;
            //                    col.AutoIncrementSeed = f.AutoIncrementSeed;
            //                    col.AutoIncrementStep = f.AutoIncrementStep;
            //                }
            //                col.ExtendedProperties.Add(SysConstManage.ExtProp, new ColExtendedProperties { IsActive=f.IsActive ,
            //                    IsRelate = true,
            //                    MapPrimarykey =f.RelatePrimarykey,
            //                    DataTypeLen =f.FieldLength,
            //                    Decimalpoint =f.Decimalpoint,
            //                    AliasName=f.AliasName
            //                    });
            //                dt.Columns.Add(col);
            //            }

            //            #region 系统默认新增的一列行号 用于系统对行项 唯一标识，自增长。
            //            col = new DataColumn(SysConstManage.sdp_rowid);
            //            col.DataType = typeof(int);
            //            col.AutoIncrement = true;
            //            col.AutoIncrementSeed = 1;
            //            col.ExtendedProperties.Add(SysConstManage.ExtProp, new ColExtendedProperties { IsActive =false, IsRelate = false});
            //            dt.Columns.Add(col);
            //            #endregion

            //            #region 系统默认新增的一列 是否选中。
            //            col = new DataColumn(SysConstManage.IsSelect);
            //            col.DataType = typeof(bool);
            //            col.DefaultValue = false;
            //            col.ExtendedProperties.Add(SysConstManage.ExtProp, new ColExtendedProperties { IsActive = false, IsRelate = false });
            //            dt.Columns.Add(col);
            //            #endregion

            //            dt.PrimaryKey = primarykey.ToArray();
            //            dt.ExtendedProperties.Add(SysConstManage.ExtProp, new TableExtendedProperties
            //            {
            //                TableIndex = tb.TableIndex,
            //                RelateTableIndex = tb.JoinTableIndex,
            //                Ignore = tb.Ignore
            //            }) ;
            //            index++;
            //        }
            //        dts.Add(dftb);
            //    }
            //}
            //return dts.ToArray();
            #endregion
        }
        public  LibTable[] CreateTableSchema(LibDataSource data)
        {
            List<LibTable> dts = new List<LibTable>();
            LibTable dftb = null;
            DataTable dt = null;
            DataColumn col = null;
            List<DataColumn> primarykey = null;
            int index = 0;
            if (data != null)
            {
                if (data.DefTables == null) return null;
                foreach (LibDefineTable deftb in data.DefTables)
                {
                    if (deftb.TableStruct == null) continue;
                    dftb = new LibTable(deftb.TableName);
                    dftb.Tables = new DataTable[deftb.TableStruct.Count];
                    index = 0;
                    foreach (LibDataTableStruct tb in deftb.TableStruct)
                    {
                        if (tb.Fields == null) continue;
                        dt = new DataTable(tb.Name);
                        dftb.Tables[index] = dt;
                        primarykey = new List<DataColumn>();
                        foreach (LibField f in tb.Fields)
                        {
                            col = new DataColumn(f.Name);
                            col.Caption = f.DisplayName;
                            switch (f.FieldType)
                            {
                                case LibFieldType.Byte:
                                    col.DataType = typeof(bool);
                                    if (!f.AutoIncrement)
                                        col.DefaultValue = 0;
                                    break;
                                case LibFieldType.Date:
                                    col.DataType = typeof(Date);
                                    //if (!f.AutoIncrement)
                                    //    col.DefaultValue = new Date { value = DateTime.Now.ToString() };
                                    break;
                                case LibFieldType.DateTime:
                                    col.DataType = typeof(DateTime);
                                    if (!f.AutoIncrement)
                                        col.DefaultValue = DateTime.Now;
                                    break;
                                case LibFieldType.Decimal:
                                    col.DataType = typeof(decimal);
                                    if (!f.AutoIncrement)
                                        col.DefaultValue = 0;
                                    break;
                                case LibFieldType.Interger:
                                    col.DataType = typeof(Int32);
                                    if (!f.AutoIncrement)
                                        col.DefaultValue = 0;
                                    break;
                                case LibFieldType.Long:
                                    col.DataType = typeof(long);
                                    if (!f.AutoIncrement)
                                        col.DefaultValue = 0;
                                    break;
                                case LibFieldType.String:
                                case LibFieldType.Text:
                                    col.DataType = typeof(string);
                                    if (!f.AutoIncrement)
                                        col.DefaultValue = string.Empty;
                                    break;
                                case LibFieldType.Img:
                                    col.DataType = typeof(byte[]);
                                    break;
                            }
                            if (tb.PrimaryKey.Contains(f.Name))//属于主键
                            {
                                primarykey.Add(col);
                            }
                            if (f.AutoIncrement)
                            {
                                col.AutoIncrement = true;
                                col.AutoIncrementSeed = f.AutoIncrementSeed;
                                col.AutoIncrementStep = f.AutoIncrementStep;
                            }
                            col.ExtendedProperties.Add(SysConstManage.ExtProp, new ColExtendedProperties
                            {
                                IsActive = f.IsActive,
                                IsRelate = false,
                                MapPrimarykey = f.RelatePrimarykey,
                                DataTypeLen = f.FieldLength,
                                Decimalpoint = f.Decimalpoint,
                                AliasName = f.AliasName
                            });
                            dt.Columns.Add(col);
                            if (f.SourceField != null && f.SourceField.Count > 0)
                            {
                                foreach (LibFromSourceField item in f.SourceField)
                                {
                                    foreach (var relatef in item.RelateFieldNm)
                                    {
                                        if (relatef.FromTableIndex != item.FromTableIndex) continue;
                                        col = new DataColumn(string.IsNullOrEmpty(relatef.AliasName) ? relatef.FieldNm : relatef.AliasName);
                                        col.Caption = relatef.DisplayNm;
                                        switch (relatef.FieldType)
                                        {
                                            case LibFieldType.Byte:
                                                col.DataType = typeof(byte);
                                                col.DefaultValue = 0;
                                                break;
                                            case LibFieldType.Date:
                                                col.DataType = typeof(Date);
                                                break;
                                            case LibFieldType.DateTime:
                                                col.DataType = typeof(DateTime);
                                                col.DefaultValue = DateTime.Now;
                                                break;
                                            case LibFieldType.Decimal:
                                                col.DataType = typeof(decimal);
                                                col.DefaultValue = 0;
                                                break;
                                            case LibFieldType.Interger:
                                                col.DataType = typeof(Int32);
                                                col.DefaultValue = 0;
                                                break;
                                            case LibFieldType.Long:
                                                col.DataType = typeof(long);
                                                col.DefaultValue = 0;
                                                break;
                                            case LibFieldType.String:
                                            case LibFieldType.Text:
                                                col.DataType = typeof(string);
                                                col.DefaultValue = string.Empty;
                                                break;
                                        }
                                        col.ExtendedProperties.Add(SysConstManage.ExtProp, new ColExtendedProperties
                                        {
                                            IsActive = false,
                                            IsRelate = true,
                                            MapPrimarykey = string.Empty,
                                            DataTypeLen = 0,
                                            Decimalpoint = 0,
                                            AliasName = relatef.AliasName
                                        });
                                        dt.Columns.Add(col);
                                    }
                                }
                            }
                        }

                        #region 系统默认新增的一列行号 用于系统对行项 唯一标识，自增长。
                        col = new DataColumn(SysConstManage.sdp_rowid);
                        col.DataType = typeof(int);
                        col.AutoIncrement = true;
                        col.AutoIncrementSeed = 1;
                        col.ExtendedProperties.Add(SysConstManage.ExtProp, new ColExtendedProperties { IsActive = false, IsRelate = false });
                        dt.Columns.Add(col);
                        #endregion

                        #region 系统默认新增的一列 是否选中。
                        col = new DataColumn(SysConstManage.IsSelect);
                        col.DataType = typeof(bool);
                        col.DefaultValue = false;
                        col.ExtendedProperties.Add(SysConstManage.ExtProp, new ColExtendedProperties { IsActive = false, IsRelate = false });
                        dt.Columns.Add(col);
                        #endregion

                        dt.PrimaryKey = primarykey.ToArray();
                        dt.ExtendedProperties.Add(SysConstManage.ExtProp, new TableExtendedProperties
                        {
                            TableIndex = tb.TableIndex,
                            RelateTableIndex = tb.JoinTableIndex,
                            Ignore = tb.Ignore
                        });
                        index++;
                    }
                    dts.Add(dftb);
                }
            }
            return dts.ToArray();
        }
    }

    [Serializable]
    public class Date
    {
       public string value { get; set; }
        //public Date()
        //{

        //}
        //public Date(string date)
        //{
        //    value = date;
        //}

        //public Date CurrentDate { get { return new Date { value = DateTime.Now.ToString("yyyy-MM-dd") }; } }

        public override string ToString()
        {
            return LibSysUtils .IsNULLOrEmpty(value )?string.Empty : Convert.ToDateTime(value).ToString("yyyy-MM-dd");
            //return value.ToString ();
        }
    }

    [Serializable]
    public class LibTable
    {
        /// <summary>
        /// 自定义表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 来源数据库表集
        /// </summary>
        public DataTable[] Tables { get; set; }

        public LibTable(string name)
        {
            this.Name = name;
        }
    }
    [Serializable]
    public class TableExtendedProperties
    {
        public int TableIndex { get; set; }
        public int RelateTableIndex { get; set; }

        /// <summary>是否忽略创建表结构</summary>
        public bool Ignore { get; set; }
        public override string ToString()
        {
            //{"TableIndex":0,"RelateTableIndex":0,"Ignore":false}
            string s = string.Format("\"TableIndex\":\"{0}\",\"RelateTableIndex\":\"{1}\",\"Ignore\":\"{2}\"", TableIndex, RelateTableIndex, Ignore);
            return "{" + s + "}";
        }
    }
    [Serializable]
    public class ColExtendedProperties
    {
        public bool IsActive { get; set; }
        public bool IsRelate { get; set; }
        public string MapPrimarykey { get; set; }

        public int DataTypeLen { get; set; }

        public int Decimalpoint { get; set; }

        public string AliasName { get; set; }

        public override string ToString()
        {
            string s= string.Format("\"IsActive\":\"{0}\",\"IsRelate\":\"{1}\",\"MapPrimarykey\":\"{2}\",\"DataTypeLen\":\"{3}\",\"Decimalpoint\":\"{4}\",\"AliasName\":\"{5}\"", IsActive, IsRelate, MapPrimarykey, DataTypeLen, Decimalpoint,AliasName);
            return "{" + s + "}";
        }
    }
}
