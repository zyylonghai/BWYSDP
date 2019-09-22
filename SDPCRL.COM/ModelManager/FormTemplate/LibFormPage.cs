using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.CORE;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager.FormTemplate
{
    /// <summary>排版模型页实体</summary>
    [Serializable]
    public class LibFormPage
    {
        #region 公开属性
        /// <summary>排版模型ID（唯一）</summary>
        [LibAttribute("FormmID", LibControlType.TextBox, "排版模型ID")]
        [XmlAttribute]
        public string FormId
        {
            get;
            set;
        }

        /// <summary>排版名称(显示名)</summary>
        [LibAttribute("fm_txtName", LibControlType.TextBox, "排版名称")]
        [XmlAttribute]
        public string FormName
        {
            get;
            set;
        }

        /// <summary>来源数据源ID（唯一）</summary>
        [LibAttribute("fm_txtDSID", LibControlType.TextAndBotton, "数据源ID")]
        [XmlAttribute]
        public string DSID
        {
            get;
            set;
        }
        /// <summary>控制器名称</summary>
        [LibAttribute("fm_ControlClassNm", LibControlType.TextBox, "控制类名")]
        [XmlAttribute]
        public string ControlClassNm
        {
            get;set;
        }

        /// <summary>样式文件</summary>
        [LibAttribute("fm_txtCssFile", LibControlType.TextBox, "样式文件")]
        [XmlAttribute]
        public string CssFile
        {
            get;
            set;
        }

        /// <summary>脚本文件</summary>
        [LibAttribute("fm_txtScriptFile", LibControlType.TextBox, "脚本文件")]
        [XmlAttribute]
        public string ScriptFile
        {
            get;
            set;
        }

        /// <summary>所属包</summary>
        [LibAttribute("fm_txtPackage", LibControlType.TextBox, "所属包")]
        [XmlAttribute]
        public string Package
        {
            get;
            set;
        }
        /// <summary>所属包</summary>
        [LibAttribute("fm_txtModuleOrder", LibControlType.TextBox, "模块顺序",true ,true)]
        public LibCollection<ModuleOrder> ModuleOrder { get; set; }

        /// <summary>信息组集合</summary>
        public LibCollection<LibFormGroup> FormGroups
        {
            get;
            set;
        }
        /// <summary>表格组集合</summary>
        public LibCollection<LibGridGroup> GridGroups
        {
            get;
            set;
        }
        #endregion
    }

    [Serializable]
    public class ModuleOrder
    {
        public ModuleType moduleType { get; set; }
        public string ID { get; set; }
    }
    [Serializable]
    public enum ModuleType
    {
        FormGroup=0,
        GridGroup=1
    }
}
