using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDPCRL.CORE;

namespace SDPCRL.COM.ModelManager
{
    /// <summary>字段结构类 </summary>
    [Serializable]
    public class LibField
    {
        #region 公开的属性
        /// <summary>编号 </summary>
        [LibAttribute("fd_txtID", LibControlType.TextBox, "编号", true,true)]
        public string ID
        {
            get;
            set;
        }

        /// <summary>字段名称</summary>
        [LibAttribute("fd_txtFieldName", LibControlType.TextBox, "字段名称")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>字段别名</summary>
        [LibAttribute("fd_txtAliasName", LibControlType.TextBox, "字段别名")]
        public string AliasName
        {
            get;
            set;
        }

        /// <summary>显示名称</summary>
        [LibAttribute("fd_txtDisplayText", LibControlType.TextBox, "显示名称")]
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary> 原字段名</summary>
        [LibAttribute("fd_txtOrignNm", LibControlType.TextBox, "原字段名")]
        public string OrignNm { get; set; }

        /// <summary>字段数据类型</summary>
        [LibAttribute("fd_combFieldType", LibControlType.Combox, "字段类型")]
        public LibFieldType FieldType
        {
            set;
            get;
        }

        /// <summary>字段长度</summary>
        [LibAttribute("fd_txtFieldLen", LibControlType.TextBox, "字段长度")]
        public int FieldLength { get; set; }

        /// <summary>小数点位数</summary>
        [LibAttribute("fd_txtDecimalpoint", LibControlType.TextBox, "小数点位数")]
        public int Decimalpoint { get; set; }

        /// <summary>是否允许为null</summary>
        [LibAttribute("fd_combAllowNull", LibControlType.Combox, "允许为null")]
        public bool AllowNull
        {
            get;
            set;
        }
        /// <summary>是否实字段</summary>
        [LibAttribute("fd_combActive", LibControlType.Combox, "实字段")]
        public bool IsActive
        {
            get;
            set;
        }
        /// <summary>来源字段</summary>
        [LibAttribute("fd_SourceField", LibControlType.TextAndBotton, "来源字段")]
        public LibFromSourceField SourceField { get; set; }
        #endregion
    }

    ///<summary>字段数据类型枚举</summary>
    [Serializable]
    public enum LibFieldType
    {
        ///<summary>字符型</summary>
        [LibReSource("字符串型-String")]
        String = 0,
        ///<summary>小位数</summary>
        [LibReSource("小位数-Byte")]
        Byte = 1,
        ///<summary>浮点型</summary>
        [LibReSource("浮点型-Decimal")]
        Decimal = 2,
        /// <summary>整型</summary>
        [LibReSource("整型-Interger")]
        Interger = 3,
        /// <summary>日期</summary>
        [LibReSource("日期-Date")]
        Date=4,
        /// <summary>日期时间</summary>
        [LibReSource("日期时间-DateTime")]
        DateTime=5,
        /// <summary>长整型</summary>
        [LibReSource("长整型-Long")]
        Long=6
    }

    /// <summary>
    /// 来源字段
    /// </summary>
    public class LibFromSourceField
    {
        /// <summary>编号 </summary>
        [LibAttribute("fsfield_txtID", LibControlType.TextBox, "编号", true, true)]
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 来源数据源
        /// </summary>
        [LibAttribute("fsfield_FromDataSource", LibControlType.TextBox, "来源数据源")]
        public string FromDataSource { get; set; }

        /// <summary>
        /// 来源自定义表名
        /// </summary>
        [LibAttribute("fsfield_FromDefindTableNm", LibControlType.TextBox, "源自定义表")]
        public string FromDefindTableNm { get; set; }
        /// <summary>
        /// 来源数据表名.
        /// </summary>
        [LibAttribute("fsfield_FromStructTableNm", LibControlType.TextBox, "来源数据表")]
        public string FromStructTableNm { get; set; }

        /// <summary>
        /// 来源字段
        /// </summary>
        [LibAttribute("fsfield_FromFieldNm", LibControlType.TextBox, "来源字段")]
        public string FromFieldNm { get; set; }

        /// <summary>
        /// 关联字段集合
        /// </summary>
        [LibAttribute("fsfield_RelateFieldNm", LibControlType.TextAndBotton , "关联字段集")]
        public List<string> RelateFieldNm { get; set; }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}.{3}", FromDataSource, FromDefindTableNm, FromStructTableNm, FromFieldNm);
        }
    }
}
