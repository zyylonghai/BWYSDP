using SDPCRL.COM.ModelManager.FormTemplate;
using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.Reports
{
    [Serializable]
    public class LibReportElement
    {
        /// <summary>元素控件ID（唯一,guid）</summary>
        [LibAttribute("rptelem_ElementID", LibControlType.TextBox, "元素ID", true)]
        [XmlAttribute]
        public string ElementID
        {
            get;
            set;
        }

        /// <summary>元素名称</summary>
        [LibAttribute("rptelem_ElementNm", LibControlType.TextBox, "元素名称")]
        [XmlAttribute]
        public string ElementNm
        {
            get;
            set;
        }

        /// <summary>元素类型</summary>
        [LibAttribute("rptelem_ElemType", LibControlType.Combox, "元素类型")]
        //[XmlAttribute]
        public ElementType ElemType
        {
            get;
            set;
        }
        /// <summary>元素宽度</summary>
        [LibAttribute("rptelem_Width", LibControlType.TextBox, "元素宽度")]
        public int Width { get; set; }
        /// <summary>元素高度</summary>
        [LibAttribute("rptelem_Height", LibControlType.TextBox, "元素高度")]
        public int Height { get; set; }

        /// <summary>值来源</summary>
        [LibAttribute("rptelem_ValueSource", LibControlType.TextBox, "值来源")]
        public string ValueSource { get; set; }

        /// <summary>水平对齐方式</summary>
        [LibAttribute("rptelem_HorizontalAlignment", LibControlType.Combox, "水平对齐方式")]
        public HorizontalAlignment HorizontalAlignment { get; set; }
        /// <summary>垂直对齐方式</summary>
        [LibAttribute("rptelem_HorizontalAlignment", LibControlType.Combox, "垂直对齐方式")]
        public VerticalAlignment VerticalAlignment { get; set; }

        /// <summary>字体大小</summary>
        [LibAttribute("rptelem_FontSize", LibControlType.TextBox, "字体大小")]
        public int FontSize { get; set; }
        /// <summary>字体颜色</summary>
        [LibAttribute("rptelem_FontColor", LibControlType.TextBox, "字体颜色")]
        public string FontColor { get; set; }
    }

    public enum HorizontalAlignment
    {
        ///<summary>左对齐</summary>
        [LibReSource("左对齐")]
        Left =0,
        ///<summary>右对齐</summary>
        [LibReSource("右对齐")]
        Right =1,
        ///<summary>水平居中</summary>
        [LibReSource("水平居中")]
        Center =2
    }
    public enum VerticalAlignment
    {
        ///<summary>顶部对齐</summary>
        [LibReSource("顶部对齐")]
        Top =0,
        ///<summary>底部对齐</summary>
        [LibReSource("底部对齐")]
        Bottom =1,
        ///<summary>垂直居中</summary>
        [LibReSource("垂直居中")]
        Center =2,
    }
}
