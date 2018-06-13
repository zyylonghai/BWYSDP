﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.CORE
{
    /// <summary>编码管理类</summary>
    class SerialManager
    {

    }

    /// <summary>系统常量管理</summary>
    public class SysConstManage
    {
        /// <summary>数据源列表文件路径 </summary>
        public static readonly string DSListFile = string.Format(@"{0}\Models\DSList.xml", Environment.CurrentDirectory);
        /// <summary>数据源文件根目录</summary>
        public static readonly string DSFileRootPath = string.Format(@"{0}\Models\DataSource", Environment.CurrentDirectory);
        /// <summary>账套信息文件</summary>
        public static readonly string DBInfoFilePath = string.Format(@"{0}\Runtime\DBInfo.xml", Environment.CurrentDirectory);
        public static readonly string DBFilePath = string.Format(@"{0}\Runtime\DBInfo.bin", Environment.CurrentDirectory);
        /// <summary>模型列表文件</summary>
        public static readonly string ModelTemp = string.Format(@"{0}\Runtime\ModelTreeTemp.xml", Environment.CurrentDirectory);
        public static readonly string ModelPath = string.Format(@"{0}\Models", Environment.CurrentDirectory);

        public const string  DBInfoSeparator = "&&";
        public const string DBInfovalSeparator = "::";
        public const char DBInfoArraySeparator = '[';
        public const char DBInfoArraySeparator2 = ']';
        public const char ColonChar=':';

        public const string SQLConnect = "SQLSERVERCONN";
        public const string OracleConnect = "ORACLECONN";
        public const string SaveStr = "SaveStr";

        #region xml操作常量
        public const string ClassNodeNm = "Class";
        public const string FuncNodeNm = "Func";
        public const string AtrrName = "name";
        public const string AtrrPackage = "package";
        #endregion

        public const string DataSourceNm = "DataSource";
        public const string FormSourceNm = "FormSource";
        public const string PermissionSourceNm = "PermissionSource";
    }

    /// <summary>编码管理类接口</summary>
    public interface ISerialManager
    {
         
    }
}
