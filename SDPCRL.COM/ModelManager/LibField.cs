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
        [LibAttribute("fd_txtID", LibControlType.TextBox, "编号", true)]
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

        /// <summary>字段类型</summary>
        [LibAttribute("fd_combFieldType", LibControlType.Combox, "字段类型")]
        public LibFieldType FieldType
        {
            set;
            get;
        }
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
        #endregion
    }

    ///<summary>字段类型枚举</summary>
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
}
