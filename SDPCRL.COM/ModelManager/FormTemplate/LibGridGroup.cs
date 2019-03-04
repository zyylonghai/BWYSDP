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
        [LibAttribute("grid_GridGroupID", LibControlType.TextBox, "表格组ID",true)]
        [XmlAttribute]
        public string GridGroupID
        {
            get;
            set;
        }

        /// <summary>表格组名</summary>
        [LibAttribute("grid_GridGroupName", LibControlType.TextBox, "表格组名")]
        [XmlAttribute]
        public string GridGroupName
        {
            get;
            set;
        }
        /// <summary>表格组名(显示名)</summary>
        [LibAttribute("grid_GridGroupDisplayNm", LibControlType.TextBox, "表格组描述")]
        [XmlAttribute]
        public string GridGroupDisplayNm
        {
            get;
            set;
        }
        /// <summary>控制类名</summary>
        [LibAttribute("grid_ControlClassNm", LibControlType.TextBox, "控制类名")]
        [XmlAttribute]
        public string ControlClassNm
        {
            get;
            set;
        }
        /// <summary>显示汇总行</summary>
        [LibAttribute("grid_HasSummary", LibControlType.Combox, "显示汇总行")]
        [XmlAttribute]
        public bool HasSummary
        {
            get;
            set;
        }

        /// <summary>字段集合</summary>
        public LibCollection<LibGridGroupField> GdGroupFields
        {
            get;
            set;
        }
    }
}
