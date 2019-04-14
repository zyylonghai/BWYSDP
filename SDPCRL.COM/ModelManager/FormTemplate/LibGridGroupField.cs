using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.CORE;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.FormTemplate
{
    /// <summary>表格组的字段实体</summary>
    [Serializable]
    public class LibGridGroupField
    {
        /// <summary>ID（唯一）</summary>
        [LibAttribute("gridgfld_ID", LibControlType.TextBox, "ID", true, true)]
        [XmlAttribute]
        public string ID
        {
            get;
            set;
        }
        /// <summary>字段名</summary>
        [LibAttribute("gridgfld_Name", LibControlType.TextBox, "字段名", true)]
        //[XmlAttribute]
        public string Name
        {
            get;
            set;
        }
        /// <summary>来源表名（源结构表）</summary>
        [LibAttribute("gridgfld_FromTableNm", LibControlType.TextBox, "源结构表", true)]
        //[XmlAttribute]
        public string FromTableNm
        {
            get;
            set;
        }
        /// <summary>来源表名(自定义表)</summary>
        [LibAttribute("gridgfld_FromDefTableNm", LibControlType.TextBox, "来源表名", true)]
        public string FromDefTableNm
        {
            get;
            set;
        }

        /// <summary>字段描述</summary>
        [LibAttribute("gridgfld_DisplayName", LibControlType.TextBox, "字段描述")]
        //[XmlAttribute]
        public string DisplayName
        {
            get;
            set;
        }

        ///<summary>控件类型</summary>
        [LibAttribute("gridgfld_ElemType", LibControlType.Combox, "元素类型")]
        //[XmlAttribute]
        public ElementType ElemType
        {
            get;
            set;
        }

        ///<summary>元素宽度(占用列数)</summary>
        [LibAttribute("gridgfld_Width", LibControlType.TextBox, "元素宽度")]
        //[XmlAttribute]
        public int Width
        {
            get;
            set;
        }
        ///<summary>是否排序</summary>
        [LibAttribute("gridgfld_HasSort", LibControlType.Combox, "是否排序")]
        public bool HasSort
        {
            get;
            set;
        }
        ///<summary>是否隐藏</summary>
        [LibAttribute("gridgfld_Hidden", LibControlType.Combox, "是否隐藏")]
        public bool Hidden
        {
            get;set;
        }
        /// <summary>是否只读</summary>
        [LibAttribute("gridgfld_ReadOnly", LibControlType.Combox, "是否只读")]
        public bool ReadOnly
        {
            get;set;
        }

        ///<summary>默认值</summary>
        [LibAttribute("gridgfld_DealfValue", LibControlType.TextBox, "默认值")]
        //[XmlAttribute]
        public object DealfValue
        {
            get;
            set;
        }
    }
}
