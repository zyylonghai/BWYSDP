using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.Reports
{
    [Serializable]
    public class LibReportField
    {
        /// <summary>ID（唯一）</summary>
        [LibAttribute("rptgridfld_ID", LibControlType.TextBox, "ID", true, false)]
        [XmlAttribute]
        public string ID
        {
            get;
            set;
        }

        /// <summary>字段名</summary>
        [LibAttribute("rptgridfld_Name", LibControlType.TextBox, "字段名", true)]
        //[XmlAttribute]
        public string Name
        {
            get;
            set;
        }
        /// <summary>来源表名（源结构表）</summary>
        [LibAttribute("rptgridfld_FromTableNm", LibControlType.TextBox, "源结构表", true)]
        //[XmlAttribute]
        public string FromTableNm
        {
            get;
            set;
        }
        /// <summary>来源表名(自定义表)</summary>
        [LibAttribute("rptgridfld_FromDefTableNm", LibControlType.TextBox, "来源表名", true)]
        public string FromDefTableNm
        {
            get;
            set;
        }

        /// <summary>字段描述</summary>
        [LibAttribute("rptgridfld_DisplayName", LibControlType.TextBox, "字段描述")]
        //[XmlAttribute]
        public string DisplayName
        {
            get;
            set;
        }
        /// <summary>是否查询条件</summary>
        [LibAttribute("rptgridfld_IsSearchCondition", LibControlType.TextBox, "是否查询条件")]
        public bool IsSearchCondition
        { 
            get; 
            set; 
        }
    }
}
