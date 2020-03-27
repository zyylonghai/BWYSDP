using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.Reports
{
    /// <summary>报表模型实体</summary>
    [Serializable]
    public class LibReportsSource
    {
        /// <summary>报表模型ID（唯一）</summary>
        [LibAttribute("ReportId", LibControlType.TextBox, "报表模型ID")]
        [XmlAttribute]
        public string ReportId
        {
            get;
            set;
        }


        /// <summary>报表名称(显示名)</summary>
        [LibAttribute("rpt_ReportName", LibControlType.TextBox, "报表名称")]
        [XmlAttribute]
        public string ReportName
        {
            get;
            set;
        }

        /// <summary>控制器名称</summary>
        [LibAttribute("rpt_ControlClassNm", LibControlType.TextBox, "控制类名")]
        [XmlAttribute]
        public string ControlClassNm
        {
            get; set;
        }

        /// <summary>脚本文件</summary>
        [LibAttribute("rpt_txtScriptFile", LibControlType.TextBox, "脚本文件")]
        [XmlAttribute]
        public string ScriptFile
        {
            get;
            set;
        }

        /// <summary>所属包</summary>
        [LibAttribute("rpt_Package", LibControlType.TextBox, "所属包")]
        [XmlAttribute]
        public string Package
        {
            get;
            set;
        }

        /// <summary>表格组集合</summary>
        public LibCollection<LibReportGrid> GridGroups
        {
            get;
            set;
        }
    }
}
