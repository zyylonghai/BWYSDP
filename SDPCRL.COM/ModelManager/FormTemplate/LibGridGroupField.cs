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
        [LibAttribute("gridgfld_ID", LibControlType.TextBox, "ID", true, false)]
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
        /// <summary>格式化表达式(显示的值来源某个表的字段则表达式:实体表名.字段名,如果是脚本函数则表达式:$.函数名)</summary>
        [LibAttribute("gridgfld_Formatter", LibControlType.TextBox, "格式化值")]
        public string Formatter { get; set; }

        /// <summary>是否来源数据所关联出来的字段</summary>
        [LibAttribute("gridgfld_IsFromSourceField", LibControlType.Combox, "是否来源关联出来的字段",true ,true)]
        public bool IsFromSourceField
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

        /// <summary>点击事件(值为脚本函数名例如(click()))</summary>
        [LibAttribute("gridgfld_Onclick", LibControlType.TextBox, "Onclick事件")]
        public string Onclick { get; set; }

        /// <summary>Change事件(值为脚本函数名例如(Change()))</summary>
        [LibAttribute("gridgfld_OnChange", LibControlType.TextBox, "Change事件")]
        public string OnChange { get; set; }

        /// <summary>blur事件(值为脚本函数名例如(blur()))</summary>
        [LibAttribute("gridgfld_Onblur", LibControlType.TextBox, "blur事件")]
        public string Onblur { get; set; }
        /// <summary>Keydown事件(值为脚本函数名例如(Keydown()))</summary>
        [LibAttribute("gridgfld_Keydown", LibControlType.TextBox, "Keydown事件")]
        public string Keydown { get; set; }
    }
}
