using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.Reports
{
    [Serializable]
    public class LibReportGrid
    {
        /// <summary>表格组ID（唯一,guid）</summary>
        [LibAttribute("rptGrid_GridGroupID", LibControlType.TextBox, "表格组ID", true)]
        [XmlAttribute]
        public string GridGroupID
        {
            get;
            set;
        }
        /// <summary>表格组名</summary>
        [LibAttribute("rptGrid_GridGroupName", LibControlType.TextBox, "表格组名")]
        [XmlAttribute]
        public string GridGroupName
        {
            get;
            set;
        }
        /// <summary>表格组名(显示名)</summary>
        [LibAttribute("rptGrid_GridGroupDisplayNm", LibControlType.TextBox, "表格组描述")]
        [XmlAttribute]
        public string GridGroupDisplayNm
        {
            get;
            set;
        }
        /// <summary>来源数据源ID（唯一）</summary>
        [LibAttribute("rptGrid_DSID", LibControlType.TextAndBotton, "数据源ID")]
        [XmlAttribute]
        public string DSID
        {
            get;
            set;
        }
        /// <summary>来源表</summary>
        [LibAttribute("rptGrid_FromTable", LibControlType.TextAndBotton, "来源表")]
        [XmlAttribute]
        public string FromTable
        {
            get;set;
        }
        /// <summary>字段集合</summary>
        public LibCollection<LibReportField> ReportFields { get; set; }
    }
}
