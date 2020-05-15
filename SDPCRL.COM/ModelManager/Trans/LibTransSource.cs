using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.Trans
{
    [Serializable]
    public class LibTransSource
    {
        /// <summary>转单模型ID（唯一）</summary>
        [LibAttribute("tran_TransId", LibControlType.TextBox, "转单模型ID")]
        [XmlAttribute]
        public string TransId
        {
            get;
            set;
        }
        /// <summary>转单名称(显示名)</summary>
        [LibAttribute("tran_TransName", LibControlType.TextBox, "转单名称")]
        [XmlAttribute]
        public string TransName
        {
            get;
            set;
        }
        /// <summary>来源单ProgId</summary>
        [LibAttribute("tran_SrcProgId", LibControlType.TextAndBotton, "来源单ProgId")]
        [XmlAttribute]
        public string SrcProgId { get; set; }

        /// <summary>目的单ProgId</summary>
        [LibAttribute("tran_TargetProgId", LibControlType.TextAndBotton, "目的单ProgId")]
        [XmlAttribute]
        public string TargetProgId { get; set; }

        /// <summary>目的单ProgId</summary>
        [LibAttribute("tran_TargetPackage", LibControlType.TextBox, "目标所属包",true)]
        [XmlAttribute]
        public string TargetPackage { get; set; }

        /// <summary>所属包</summary>
        [LibAttribute("tran_Package", LibControlType.TextBox, "所属包")]
        [XmlAttribute]
        public string Package
        {
            get;
            set;
        }

        /// <summary>转单字段集合</summary>
        public LibCollection<LibTransFieldMap > TransFields { get; set; }



    }
}
