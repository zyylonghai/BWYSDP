using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.CORE;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.FormTemplate
{
    /// <summary>页面表格组实体</summary>
    [Serializable]
    public class LibGridGroup
    {
        /// <summary>表格组ID（唯一）</summary>
        [LibAttribute("fm_GridGroupID", LibControlType.TextBox, "表格组ID",true)]
        [XmlAttribute]
        public string GridGroupID
        {
            get;
            set;
        }

        /// <summary>表格组名(显示名)</summary>
        [LibAttribute("fm_GridGroupName", LibControlType.TextBox, "表格组名")]
        [XmlAttribute]
        public string GridGroupName
        {
            get;
            set;
        }
    }
}
