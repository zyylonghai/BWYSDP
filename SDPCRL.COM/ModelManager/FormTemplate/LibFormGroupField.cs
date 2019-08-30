using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.CORE;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.FormTemplate
{
    /// <summary>页面信息组的字段实体</summary>
    [Serializable]
    public class LibFormGroupField
    {
        /// <summary>ID（唯一）</summary>
        [LibAttribute("fmfld_ID", LibControlType.TextBox, "ID", true,true)]
        [XmlAttribute]
        public string ID
        {
            get;
            set;
        }

        /// <summary>字段名</summary>
        [LibAttribute("fmfld_Name", LibControlType.TextBox, "字段名",true)]
        //[XmlAttribute]
        public string Name
        {
            get;
            set;
        }
        /// <summary>来源表名</summary>
        [LibAttribute("fmfld_FromTableNm", LibControlType.TextBox, "来源表名",true)]
        //[XmlAttribute]
        public string FromTableNm
        {
            get;
            set;
        }
        /// <summary>来源表名(自定义表)</summary>
        [LibAttribute("fmfld_FromDefTableNm", LibControlType.TextBox, "来源结构表", true)]
        public string FromDefTableNm
        {
            get;
            set;
        }

        /// <summary>字段描述</summary>
        [LibAttribute("fmfld_DisplayName", LibControlType.TextBox, "字段描述")]
        //[XmlAttribute]
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>控件类型</summary>
        [LibAttribute("fmfld_ElemType", LibControlType.Combox, "元素类型")]
        //[XmlAttribute]
        public ElementType ElemType
        {
            get;
            set;
        }

        ///<summary>元素宽度(占用列数)</summary>
        [LibAttribute("fmfld_Width", LibControlType.TextBox, "元素宽度")]
        //[XmlAttribute]
        public int Width
        {
            get;
            set;
        }
        /// <summary>字段长度</summary>
        [LibAttribute("fmfld_FieldLength", LibControlType.TextBox, "字符长度",true)]
        public int FieldLength { get; set; }

        /// <summary>是否必填</summary>
        [LibAttribute("fmfld_IsAllowNull", LibControlType.Combox, "是否必填")]
        public bool IsAllowNull { get; set; }

        /// <summary>是否数字</summary>
        [LibAttribute("fmfld_IsNumber", LibControlType.Combox, "是否数字", true)]
        public bool IsNumber { get; set; }

        ///<summary>默认值</summary>
        [LibAttribute("fmfld_DealfValue", LibControlType.TextBox, "默认值")]
        //[XmlAttribute]
        public object DealfValue
        {
            get;
            set;
        }

    }
    public enum ElementType
    {
        ///<summary>文本输入框</summary>
        [LibReSource("文本输入框")]
        Text=0,
        ///<summary>下拉选项框</summary>
        [LibReSource("下拉选项框")]
        Select=1,
        ///<summary>日期控件</summary>
        [LibReSource("日期控件")]
        Date=2,
        ///<summary>日期时间控件</summary>
        [LibReSource("日期时间控件")]
        DateTime=3,
        ///<summary>搜索控件</summary>
        [LibReSource("搜索控件")]
        Search =4,
        ///<summary>大文本框</summary>
        [LibReSource("大文本框")]
        Textarea =5
    }
}
