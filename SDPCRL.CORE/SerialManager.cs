using System;
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
        //public static readonly string DBInfoFilePath = string.Format(@"{0}\Runtime\DBInfo.xml", Environment.CurrentDirectory);
        public static readonly string DBFilePath = string.Format(@"{0}\Runtime\DBInfo.bin", Environment.CurrentDirectory);
        /// <summary>模型列表文件</summary>
        public static readonly string ModelTemp = string.Format(@"{0}\Runtime\ModelTreeTemp.xml", Environment.CurrentDirectory);
        public static readonly string ModelPath = string.Format(@"{0}\Models", Environment.CurrentDirectory);
        //DAL程序集所在路径
        public static readonly string DALAssemblyPath = string.Format(@"{0}\Runtime\DAL", Environment.CurrentDirectory);
        //客户端存储的服务端信息文件路径
        public static readonly string ServerConfigPath = string.Format(@"{0}\Runtime\ServerInfo.bin", Environment.CurrentDirectory);
        #region 特殊字符
        public const string  DBInfoSeparator = "&&";
        public const string DBInfovalSeparator = "::";
        public const char DBInfoArraySeparator = '[';
        public const char DBInfoArraySeparator2 = ']';
        public const char ColonChar=':';
        public const char Underline = '_';
        public const char Comma = ',';
        public const char Asterisk = '*';
        public const char Point = '.';
        public const char SingleQuotes = '\'';
        public const char DollarSign = '$';

        #endregion

        public const string SQLConnect = "SQLSERVERCONN";
        public const string OracleConnect = "ORACLECONN";
        public const string SaveStr = "SaveStr";

        #region xml操作常量 ModelTreeTemp.xml的节点名或属性
        public const string ClassNodeNm = "Class";
        public const string FuncNodeNm = "Func";
        public const string AtrrName = "name";
        public const string AtrrPackage = "package";
        public const string ReportFuncNodeNm = "ReportFunc";
        public const string TransModelNm = "Trans";
        #endregion

        #region 模型存储 所在路径的 文件夹名
        public const string DataSourceNm = "DataSource";
        public const string FormSourceNm = "FormSource";
        public const string PermissionSourceNm = "PermissionSource";
        public const string KeyValues = "KeyValues";
        public const string ReportSourceNm = "ReportSource";
        public const string TransSourceNm = "TransSource";
        #endregion

        public const string BtnCtrlNmPrefix = "btn_";
        public const string BtnCtrlDefaultText = "...";


        #region BWYSDPWeb
        public const string PageinfoCookieNm = "PageInfo";
        public const string ProgidCookieKey = "Progid";
        public const string PackageCookieKey = "Package";

        public const string OperateAction = "Action";
        //用于创建表结构时，设置表和列的自定义属性关键字
        public const string ExtProp = "extProp";
        //用于客户端表格的行项列。
        public const string sdp_rowid = "sdp_rowid";

        //用于客户端表格的行项列，是否选中
        public const string IsSelect = "sdp_select";

        //用于搜索模态框的固定命名。
        //public const string sdp_smodalform = "sdp_smodalform";
        //public const string sdp_smodalCondition = "sdp_smodalCondition";
        public const string sdp_smodalfield = "sdp_smodalfield";
        public const string sdp_smodalsymbol = "sdp_smodalsymbol";
        public const string sdp_smodalval = "sdp_smodalval";
        public const string sdp_smodallogic = "sdp_smodallogic";
        public const string sdp_total_row = "total_row";
        public const string sdp_Schcond = "sdp_searchcond";
        public const string sdp_language = "sdp_language";
        public const string sdp_userinfo = "sdp_userinfo";

        //系统字段 创建者，创建日期，最后修改者，最后修改日期
        public const string sysfld_creater = "Creater";
        public const string sysfld_createDT = "CreateDate";
        public const string sysfld_lastmodifier = "LastmodifyUser";
        public const string sysfld_lastmodifyDT = "LastmodifyDate";

        //dal最基类名称
        public const string sdp_webdalbase = "webdaldatabase";

        public const string _pwdkeyEncrykey = "bwyAccount";
        public const string sdp_IdentityTick = "tick";

        //gridview 报表
        public const string sdp_summaryprefix = "sum_";
        #endregion
        public const string TBSchemasuffix = "_dsschema";
        public const string Sdp_LogId = "sdp_logid";
    }
    /// <summary>编码管理类接口</summary>
    public interface ISerialManager
    {
         
    }
}
