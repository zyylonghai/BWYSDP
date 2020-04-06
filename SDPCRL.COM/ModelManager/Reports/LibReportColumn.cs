using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.Reports
{
    [Serializable]
    public class LibReportColumn
    {
        /// <summary>栅格列ID（唯一,guid）</summary>
        [LibAttribute("rptrow_ColumnID", LibControlType.TextBox, "栅格列ID", true)]
        [XmlAttribute]
        public string ColumnID
        {
            get;
            set;
        }
        [LibAttribute("rptrow_ColumnNm", LibControlType.TextBox, "栅格列名")]
        [XmlAttribute]
        public string ColumnNm { get; set; }

        /// <summary>占用位数，每栅格行共12位</summary>
        [LibAttribute("rptrow_Width", LibControlType.TextBox, "占用位数")]
        [XmlAttribute]
        public int Width
        {
            get;
            set;
        }
        /// <summary>上边框</summary>
        [LibAttribute("rptrow_TopBorder", LibControlType.TextBox, "上边框")]
        //[XmlAttribute]
        public int TopBorder { get; set; }

        /// <summary>下边框</summary>
        [LibAttribute("rptrow_BottomBorder", LibControlType.TextBox, "下边框")]
        [XmlAttribute]
        public int BottomBorder { get; set; }
        /// <summary>左边框</summary>
        [LibAttribute("rptrow_LeftBorder", LibControlType.TextBox, "左边框")]
        [XmlAttribute]
        public int LeftBorder { get; set; }
        /// <summary>右边框</summary>
        [LibAttribute("rptrow_RightBorder", LibControlType.TextBox, "右边框")]
        [XmlAttribute]
        public int RightBorder { get; set; }

        /// <summary>上边框颜色</summary>
        [LibAttribute("rptrow_TopBorderColor", LibControlType.TextBox, "上边框颜色")]
        [XmlAttribute]
        public string TopBorderColor { get; set; }
        /// <summary>下边框颜色</summary>
        [LibAttribute("rptrow_BottomBorderColor", LibControlType.TextBox, "下边框颜色")]
        [XmlAttribute]
        public string BottomBorderColor { get; set; }
        /// <summary>左边框颜色</summary>
        [LibAttribute("rptrow_LeftBorderColor", LibControlType.TextBox, "左边框颜色")]
        [XmlAttribute]
        public string LeftBorderColor { get; set; }
        /// <summary>右边框颜色</summary>
        [LibAttribute("rptrow_RightBorderColor", LibControlType.TextBox, "右边框颜色")]
        [XmlAttribute]
        public string RightBorderColor { get; set; }

        /// <summary>元素集合</summary>
        public LibCollection<LibReportElement> Elements { get; set; }

    }
}
