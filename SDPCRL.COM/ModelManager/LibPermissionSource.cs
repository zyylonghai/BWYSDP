using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager
{
    /// <summary>权限模型对象</summary>
    [Serializable]
    public class LibPermissionSource
    {
        [LibAttribute("Perm_txtID", LibControlType.TextBox, "权限模型ID")]
        [XmlAttribute]
        public string PermissionID { get; set; }

        /// <summary>排版模型ID</summary>
        [LibAttribute("Perm_txtFormmID", LibControlType.TextBox, "排版模型ID")]
        [XmlAttribute]
        public string FormId
        {
            get;
            set;
        }

        /// <summary>数据源ID</summary>
        [LibAttribute("Perm_txtDSID", LibControlType.TextBox, "数据源ID",true ,true)]
        [XmlAttribute]
        public string DSID
        {
            get;
            set;
        }
        /// <summary>所属包</summary>
        [LibAttribute("Perm_txtPackage", LibControlType.TextBox, "所属包")]
        [XmlAttribute]
        public string Package
        {
            get;
            set;
        }
        /// <summary>是否挂菜单</summary>
        [LibAttribute("Perm_IsMenu", LibControlType.Combox, "是否挂菜单")]
        [XmlAttribute]
        public bool IsMenu
        {
            get;set;
        }
        /// <summary>是否新增</summary>
        [LibAttribute("Perm_IsIsAdd", LibControlType.Combox, "是否新增")]
        [XmlAttribute]
        public bool IsAdd { get; set; }

        /// <summary>是否删除</summary>
        [LibAttribute("Perm_IsDelete", LibControlType.Combox, "是否删除")]
        [XmlAttribute]
        public bool IsDelete { get; set; }

        /// <summary>是否修改</summary>
        [LibAttribute("Perm_IsEdit", LibControlType.Combox, "是否修改")]
        [XmlAttribute]
        public bool IsEdit { get; set; }

        /// <summary>是否搜索</summary>
        [LibAttribute("Perm_IsSearch", LibControlType.Combox, "是否搜索")]
        [XmlAttribute]
        public bool IsSearch { get; set; }
       


    }
}
