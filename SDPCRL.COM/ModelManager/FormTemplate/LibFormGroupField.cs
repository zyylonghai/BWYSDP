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
        [LibAttribute("fmfld_ID", LibControlType.TextBox, "ID", true)]
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

        /// <summary>是否只读</summary>
        [LibAttribute("fmfld_Readonly", LibControlType.Combox, "是否只读")]
        public bool Readonly { get; set; }

        ///<summary>默认值</summary>
        [LibAttribute("fmfld_DealfValue", LibControlType.TextBox, "默认值")]
        //[XmlAttribute]
        public object DealfValue
        {
            get;
            set;
        }
        /// <summary>点击事件(值为脚本函数名例如(click()))</summary>
        [LibAttribute("fmfld_Onclick", LibControlType.TextBox, "Onclick事件")]
        public string Onclick { get; set; }

        /// <summary>Change事件(值为脚本函数名例如(Change()))</summary>
        [LibAttribute("fmfld_OnChange", LibControlType.TextBox, "Change事件")]
        public string OnChange { get; set; }

        /// <summary>blur事件(值为脚本函数名例如(blur()))</summary>
        [LibAttribute("fmfld_Onblur", LibControlType.TextBox, "blur事件")]
        public string Onblur { get; set; }
        /// <summary>Keydown事件(值为脚本函数名例如(Keydown()))</summary>
        [LibAttribute("fmfld_Keydown", LibControlType.TextBox, "Keydown事件")]
        public string Keydown { get; set; }


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
        Textarea =5,
        ///<summary>图片控件</summary>
        [LibReSource("图片控件")]
        Img =6,
        ///<summary>文件上传控件</summary>
        [LibReSource("文件上传控件")]
        FileUpload =7,
        ///<summary>密码控件</summary>
        [LibReSource("密码控件")]
        Password =8,
        ///<summary>Label标签</summary>
        [LibReSource("Label标签")]
        Label =9
    }
}
