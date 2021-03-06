﻿using System;
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
        [LibAttribute("fd_txtID", LibControlType.TextBox, "编号", true,false)]
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
        /// <summary>实体字段名</summary>
        [LibAttribute("fd_ObjFieldName", LibControlType.TextBox, "实体字段名")]
        public string ObjFieldName
        {
            get;set;
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

        ///<summary>对应关联表的主键名</summary>
        [LibAttribute("fd_txtRelatePrimarykey", LibControlType.TextBox, "RelatePrimarykey")]
        public string RelatePrimarykey { get; set; }

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
        ///// <summary>有效性验证</summary>
        //[LibAttribute("fd_Validation", LibControlType.Combox, "值有效性")]
        //public LibFieldValidation Validation
        //{
        //    get;set;
        //}

        /// <summary>有效性验证</summary>
        [LibAttribute("fd_ValidateExpression", LibControlType.TextAndBotton, "值有效性")]
        public LibFieldValidatExpress ValidateExpression { get; set; }


        /// <summary>是否自增长</summary>
        [LibAttribute("fd_combAutoIncrement", LibControlType.Combox, "自增长")]
        public bool AutoIncrement
        {
            get;set;
        }
        /// <summary>自增起始值</summary>
        [LibAttribute("fd_txtAutoIncrementSeed", LibControlType.TextBox, "自增起始值")]
        public int AutoIncrementSeed
        {
            get;
            set;
        }
        /// <summary>自增增量值</summary>
        [LibAttribute("fd_txtAutoIncrementStep", LibControlType.TextBox, "自增增量值")]
        public int AutoIncrementStep
        {
            get;set;
        }
        /// <summary>来源字段</summary>
        [LibAttribute("fd_SourceField", LibControlType.TextAndBotton, "来源字段")]
        public LibCollection<LibFromSourceField> SourceField { get; set; }

        /// <summary>键值对集</summary>
        [LibAttribute("fd_Items", LibControlType.TextAndBotton, "键值对集")]
        public LibCollection<LibKeyValue> Items { get; set; }

        /// <summary>是否系统字段</summary>
        [LibAttribute("fd_combSysField", LibControlType.Combox, "是否系统字段",true ,true)]
        public bool SysField { get; set; }

        
        #endregion
    }

    ///<summary>字段数据类型枚举</summary>
    [Serializable]
    public enum LibFieldType
    {
        ///<summary>字符型</summary>
        [LibReSource("字符串型-String")]
        String = 0,
        ///<summary>boolean</summary>
        [LibReSource("boolean-bit")]
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
        Long=6,
        /// <summary>文本型</summary>
        [LibReSource("文本型-Text")]
        Text =7,
        /// <summary>图片类型</summary>
        [LibReSource("图片类型-Img")]
        Img = 8
    }

    /// <summary>
    /// 来源字段
    /// </summary>
    [Serializable]
    public class LibFromSourceField
    {
        /// <summary>编号 </summary>
        [LibAttribute("fsfield_txtID", LibControlType.TextBox, "编号", true, true)]
        public string ID
        {
            get;
            set;
        }
        /// <summary>来源数据源</summary>
        [LibAttribute("fsfield_FromDataSource", LibControlType.TextAndBotton, "来源数据源")]
        public string FromDataSource { get; set; }

        /// <summary>
        /// 来源自定义表名
        /// </summary>
        [LibAttribute("fsfield_FromDefindTableNm", LibControlType.TextAndBotton, "源自定义表")]
        public string FromDefindTableNm { get; set; }
        /// <summary>
        /// 来源数据表名.
        /// </summary>
        [LibAttribute("fsfield_FromStructTableNm", LibControlType.TextAndBotton, "来源数据表")]
        public string FromStructTableNm { get; set; }

        /// <summary>
        /// 来源字段
        /// </summary>
        [LibAttribute("fsfield_FromFieldNm", LibControlType.TextAndBotton, "来源字段")]
        public string FromFieldNm { get; set; }

        /// <summary>
        /// 来源字段描述
        /// </summary>
        [LibAttribute("fsfield_FromFieldDesc", LibControlType.TextAndBotton, "来源字段描述")]
        public string FromFieldDesc { get; set; }

        /// <summary>
        /// 来源表索引
        /// </summary>
        [LibAttribute("fsfield_FromTableIndex", LibControlType.TextBox, "来源表索引",true ,true)]
        public int FromTableIndex { get; set; }

        /// <summary>
        /// 关联条件
        /// </summary>
        [LibAttribute("fsfield_RelateCondition", LibControlType.TextBox, "前提条件")]
        public string RelateCondition { get; set; }

        /// <summary>
        /// 关联条件
        /// </summary>
        [LibAttribute("fsfield_JoinOnCondition", LibControlType.TextBox, "关联条件")]
        public string JoinOnCondition { get; set; }

        /// <summary>
        /// 关联字段集合
        /// </summary>
        [LibAttribute("fsfield_RelateFieldNm", LibControlType.TextAndBotton , "关联字段集")]
        public List<LibRelateField> RelateFieldNm { get; set; }

        public override string ToString()
        {
            if (FromDataSource == null)
                return ID;
            return string.Format("{0}.{1}.{2}.{3}", FromDataSource, FromDefindTableNm, FromStructTableNm, FromFieldNm);
        }
    }
    /// <summary>关联字段的属性</summary>
    public class LibRelateField
    {
        [LibAttribute("relate_txtID", LibControlType.TextBox, "编号", true)]
        public string ID
        {
            get;
            set;
        }
        [LibAttribute("relate_FromTableIndex", LibControlType.TextBox, "来源表索引",true)]
        public int FromTableIndex { get; set; }

        [LibAttribute("relate_FieldNm", LibControlType.TextBox, "字段名",true)]
        public string FieldNm { get; set; }

        [LibAttribute("relate_AliasName", LibControlType.TextBox, "别名")]
        public string AliasName { get; set; }

        [LibAttribute("relate_DisplayNm", LibControlType.TextBox, "字段描述", true)]
        public string DisplayNm { get; set; }

        public LibFieldType FieldType { get; set; }

        [LibAttribute("relate_ObjFieldName", LibControlType.TextBox, "实体字段名", true)]
        public string ObjFieldName { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(AliasName))
                return AliasName;
            return FieldNm;
        }
    }

    /// <summary>键值对</summary>
    [Serializable]
    public class LibKeyValue
    {
        /// <summary>编号</summary>
        [LibAttribute("keyval_ID", LibControlType.TextBox, "ID",true)]
        public string ID{get;set;}

        /// <summary>来源模型</summary>
        [LibAttribute("keyval_txtFromID", LibControlType.TextBox, "来源模型",true)]
        public string FromkeyValueID { get; set; }

        /// <summary>键</summary>
        [LibAttribute("keyval_Key", LibControlType.TextBox, "Key")]
        public object Key { get; set; }

        /// <summary> 值</summary>
        [LibAttribute("keyval_Value", LibControlType.TextBox, "Value")]
        public object Value { get; set; }

        /// <summary>备注</summary>
        [LibAttribute("keyval_Remark", LibControlType.TextBox, "备注")]
        public string Remark { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Key, Value);
        }
    }

    /// <summary>
    /// 系统字段
    /// </summary>
    [Serializable]
    public class LibSysField
    {
        /// <summary>字段名称</summary>
        [LibAttribute("sysfd_txtFieldName", LibControlType.TextBox, "字段名称")]
        public string Name
        {
            get;
            set;
        }
        /// <summary>显示名称</summary>
        [LibAttribute("sysfd_txtDisplayText", LibControlType.TextBox, "显示名称")]
        public string DisplayName
        {
            get;
            set;
        }
        /// <summary>字段数据类型</summary>
        [LibAttribute("sysfd_combFieldType", LibControlType.Combox, "字段类型")]
        public LibFieldType FieldType
        {
            set;
            get;
        }

        /// <summary>字段长度</summary>
        [LibAttribute("sysfd_txtFieldLen", LibControlType.TextBox, "字段长度")]
        public int FieldLength { get; set; }


        public override string ToString()
        {
            return string.Format("{0}:{1}", Name, DisplayName);
        }

    }

    [Serializable]
    public enum LibFieldValidation
    {
        [LibReSource("")]
        None =0,
        ///<summary>大于0</summary>
        [LibReSource("大于0")]
        MorethanZero =1,
        ///<summary>小于0</summary>
        [LibReSource("小于0")]
        LessthanZero =2,
        ///<summary>大于等于0</summary>
        [LibReSource("大于等于0")]
        MorethanEqualZero =3,
        ///<summary>小于等于0</summary>
        [LibReSource("小于等于0")]
        LessthanEqualZero =4,
        ///<summary>自定义表达式</summary>
        [LibReSource("自定义表达式")]
        DefExpress =5
    }
    [Serializable]
    public class LibFieldValidatExpress
    {
        /// <summary>表达式</summary>
        [LibAttribute("valideexpressfd_Express", LibControlType.TextBox, "表达式")]
        public string Express { get; set; }

        /// <summary>信息代码</summary>
        [LibAttribute("valideexpressfd_MsgCode", LibControlType.TextBox, "信息代码")]
        public string MsgCode { get; set; }

        /// <summary>信息参数</summary>
        [LibAttribute("valideexpressfd_MsgParams", LibControlType.TextBox, "信息参数")]
        public string MsgParams { get; set; }

        public override string ToString()
        {
            return this.Express;
        }
    }
}
