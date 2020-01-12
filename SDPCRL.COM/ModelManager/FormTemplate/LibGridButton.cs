using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.FormTemplate
{
    /// <summary>表格组的自定义按钮</summary>
    [Serializable]
    public  class LibGridButton
    {
        /// <summary>表格按钮ID（唯一,guid）</summary>
        [LibAttribute("gridbtn_GridButtonID", LibControlType.TextBox, "按钮ID", true)]
        [XmlAttribute]
        public string GridButtonID
        {
            get;
            set;
        }

        /// <summary>按钮名</summary>
        [LibAttribute("gridbtn_GridButtonName", LibControlType.TextBox, "按钮名")]
        [XmlAttribute]
        public string GridButtonName
        {
            get;
            set;
        }
        /// <summary>按钮描述(显示名)</summary>
        [LibAttribute("gridbtn_GridButtonDisplayNm", LibControlType.TextBox, "按钮描述")]
        [XmlAttribute]
        public string GridButtonDisplayNm
        {
            get;
            set;
        }
        /// <summary>按钮点击事件</summary>
        [LibAttribute("gridbtn_GridButtonEvent", LibControlType.TextBox, "点击事件")]
        [XmlAttribute]
        public string GridButtonEvent { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", GridButtonName);
        }
    }
}
