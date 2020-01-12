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
        /// <summary>表格组ID（唯一,guid）</summary>
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

        /// <summary>是否有新增行按钮</summary>
        [LibAttribute("grid_HasAddRowButton", LibControlType.Combox, "新增按钮")]
        [XmlAttribute]
        public bool HasAddRowButton { get; set; }

        /// <summary>是否有编辑行按钮</summary>
        [LibAttribute("grid_HasEditRowButton", LibControlType.Combox, "编辑按钮")]
        [XmlAttribute]

        public bool HasEditRowButton { get; set; }

        /// <summary>是否有删除行按钮</summary>
        [LibAttribute("grid_HasDeletRowButton", LibControlType.Combox, "删除按钮")]
        [XmlAttribute]
        public bool HasDeletRowButton { get; set; }

        /// <summary>自定义按钮</summary>
        [LibAttribute("grid_GdButtons", LibControlType.TextAndBotton, "自定义按钮")]
        public LibCollection<LibGridButton> GdButtons { get; set; }

        /// <summary>子表格组名 </summary>
        [LibAttribute("grid_ChildGridNm", LibControlType.TextBox, "子表格组")]
        [XmlAttribute]
        public string ChildGridNm
        {
            get;set;
        }

        /// <summary>字段集合</summary>
        public LibCollection<LibGridGroupField> GdGroupFields
        {
            get;
            set;
        }
    }
}
