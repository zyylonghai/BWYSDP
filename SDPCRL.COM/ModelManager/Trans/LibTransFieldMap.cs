using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.Trans
{
    [Serializable]
    public class LibTransFieldMap
    {
        /// <summary>ID（guid唯一）</summary>
        [LibAttribute("tranfld_ID", LibControlType.TextBox, "ID", true, false)]
        [XmlAttribute]
        public string ID
        {
            get;
            set;
        }
        /// <summary>来源表字段</summary>
        [LibAttribute("tranfld_SrcFieldNm", LibControlType.TextBox, "源表字段", true)]
        public string SrcFieldNm { get; set; }

        /// <summary>来源表字段描述</summary>
        [LibAttribute("tranfld_SrcFieldDisplay", LibControlType.TextBox, "源表字段描述", true)]
        public string SrcFieldDisplay { get; set; }

        /// <summary>来源表名（源结构表）</summary>
        [LibAttribute("tranfld_SrcTableNm", LibControlType.TextBox, "源结构表", true)]
        //[XmlAttribute]
        public string SrcTableNm
        {
            get;
            set;
        }
        /// <summary>来源表索引（源结构表索引）</summary>
        [LibAttribute("tranfld_SrcTableIndex", LibControlType.TextBox, "源表索引", true)]
        public int SrcTableIndex
        {
            get; set;
        }

        /// <summary>目标字段</summary>
        [LibAttribute("tranfld_TargetFieldNm", LibControlType.TextAndBotton, "目标字段", true)]
        public string TargetFieldNm { get; set; }

        /// <summary>目标字段描述</summary>
        [LibAttribute("tranfld_TargetFieldDisplay", LibControlType.TextBox, "目标字段描述", true)]
        public string TargetFieldDisplay { get; set; }

        /// <summary>目标表名（源结构表）</summary>
        [LibAttribute("tranfld_TargetTableNm", LibControlType.TextBox, "目标结构表", true)]
        //[XmlAttribute]
        public string TargetTableNm
        {
            get;
            set;
        }
        /// <summary>目标表索引（目标结构表索引）</summary>
        [LibAttribute("tranfld_TargetTableIndex", LibControlType.TextBox, "目标表索引", true)]
        public int TargetTableIndex
        {
            get; set;
        }

    }
}
