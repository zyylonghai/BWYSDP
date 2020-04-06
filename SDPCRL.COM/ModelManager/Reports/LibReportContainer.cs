using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.Reports
{
    /// <summary>
    /// 栅格容器
    /// </summary>
    [Serializable]
    public class LibReportContainer
    {
        /// <summary>栅格容器ID（唯一,guid）</summary>
        [LibAttribute("rptcontainer_ContainerID", LibControlType.TextBox, "栅格容器ID", true)]
        [XmlAttribute]
        public string ContainerID
        {
            get;
            set;
        }

        /// <summary>栅格容器</summary>
        [LibAttribute("rptcontainer_ContainerNm", LibControlType.TextBox, "栅格容器名")]
        [XmlAttribute]
        public string ContainerNm
        {
            get;
            set;
        }

        /// <summary>来源数据源ID（唯一）</summary>
        [LibAttribute("rptcontainer_DSID", LibControlType.TextAndBotton, "数据源ID")]
        [XmlAttribute]
        public string DSID
        {
            get;
            set;
        }
        /// <summary>来源表</summary>
        [LibAttribute("rptcontainer_FromTable", LibControlType.TextAndBotton, "来源表")]
        [XmlAttribute]
        public string FromTable
        {
            get; set;
        }

        /// <summary>栅格行集合</summary>
        public LibCollection<LibReportRow> ReportRows { get; set; }
    }
}
