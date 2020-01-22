using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.FormTemplate
{
    /// <summary>自定义按钮</summary>
    [Serializable]
    public class LibButton
    {
        /// <summary>自定义按钮ID（唯一,guid）</summary>
        [LibAttribute("libbtn_LibButtonID", LibControlType.TextBox, "按钮ID", true)]
        [XmlAttribute]
        public string LibButtonID
        {
            get;
            set;
        }

        /// <summary>按钮名</summary>
        [LibAttribute("libbtn_LibButtonName", LibControlType.TextBox, "按钮名")]
        [XmlAttribute]
        public string LibButtonName
        {
            get;
            set;
        }
        /// <summary>按钮描述(显示名)</summary>
        [LibAttribute("libbtn_LibButtonDisplayNm", LibControlType.TextBox, "按钮描述")]
        [XmlAttribute]
        public string LibButtonDisplayNm
        {
            get;
            set;
        }
        /// <summary>按钮点击事件</summary>
        [LibAttribute("libbtn_LibButtonEvent", LibControlType.TextBox, "点击事件")]
        [XmlAttribute]
        public string LibButtonEvent { get; set; }
    }
}
