using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.CORE;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.FormTemplate
{
    /// <summary>页面信息组实体</summary>
    [Serializable]
    public class LibFormGroup
    {
        /// <summary>信息组ID（唯一）</summary>
        [LibAttribute("fm_FormGroupID", LibControlType.TextBox, "信息组ID")]
        [XmlAttribute]
        public string FormGroupID
        {
            get;
            set;
        }

        /// <summary>信息组名(显示名)</summary>
        [LibAttribute("fm_FormGroupName", LibControlType.TextBox, "信息组名")]
        [XmlAttribute]
        public string FormGroupName
        {
            get;
            set;
        }
    }
}
