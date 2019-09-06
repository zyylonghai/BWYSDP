using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.COM.ModelManager
{
    public class LibKeyValueCollection
    {
        #region 公开属性
        /// <summary>字典模型ID</summary>
        [LibAttribute("kv_txtID", LibControlType.TextBox, "ID")]
        [XmlAttribute]
        public string ID
        {
            get;
            set;
        }
        /// <summary>显示名</summary>
        [LibAttribute("kv_txtName", LibControlType.TextBox, "Name")]
        [XmlAttribute]
        /// <summary> </summary>
        public string Name
        {
            get;set;
        }
        /// <summary>所属包</summary>
        [LibAttribute("kv_txtPackage")]
        public string Package
        {
            get;
            set;
        }

        public LibCollection<LibKeyValue> KeyValues { get; set; }
        #endregion 
    }
}
