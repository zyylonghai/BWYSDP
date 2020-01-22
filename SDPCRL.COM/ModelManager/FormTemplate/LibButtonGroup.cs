using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.FormTemplate
{
    /// <summary>自定义按钮组</summary>
    [Serializable]
    public class LibButtonGroup
    {
        /// <summary>按钮组ID（唯一,guid）</summary>
        [LibAttribute("btnG_BtnGroupID", LibControlType.TextBox, "按钮组ID", true)]
        [XmlAttribute]
        public string BtnGroupID
        {
            get;
            set;
        }

        /// <summary>按钮组名</summary>
        [LibAttribute("btnG_BtnGroupName", LibControlType.TextBox, "按钮组名")]
        [XmlAttribute]
        public string BtnGroupName
        {
            get;
            set;
        }

        /// <summary>按钮组名(显示名)</summary>
        [LibAttribute("btnG_BtnGroupDisplayNm", LibControlType.TextBox, "按钮组描述")]
        [XmlAttribute]
        public string BtnGroupDisplayNm
        {
            get;
            set;
        }

        /// <summary>按钮集合</summary>
        public LibCollection<LibButton> LibButtons
        {
            get;
            set;
        }
    }
}
