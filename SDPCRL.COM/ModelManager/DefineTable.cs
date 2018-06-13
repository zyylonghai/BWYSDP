using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SDPCRL.CORE;

namespace SDPCRL.COM.ModelManager
{
    /// <summary>自定义数据表</summary>
    [Serializable]
    public class DefineTable
    {
        #region 私有属性
        private string _tableName;
        #endregion

        #region 公开属性
        /// <summary>名称</summary>
        [LibXmlAttribute("DefTB_txtTableName")]
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        /// <summary>编号 </summary>
        [LibXmlAttribute("DefTB_txtID")]
        public int ID
        {
            get;
            set;
        }
        ///<summary>显示名称</summary>
        [LibXmlAttribute("DefTB_txtDisplayName")]
        public string DisplayName
        {
            get;set;
        }
        /// <summary>表结构集合</summary>
        public LibCollection<DataTableStruct> TableStruct
        {
            get;
            set;
        }
        #endregion

        
    }

    /// <summary>数据表结构</summary>
    [Serializable]
    public class DataTableStruct
    {
        
        ///<summary>名称</summary>
        [LibXmlAttribute("tbStruct_txtTableName")]
        public string Name
        {
            get;
            set;
        }
        ///<summary>显示名称</summary>
        [LibXmlAttribute("tbStruct_txtTableDisplayName")]
        public string DisplayName
        {
            get;
            set;
        }
        ///<summary>忽略创建表结构</summary>
        [LibXmlAttribute("comb_createTBStruct")]
        public bool Ignore
        {
            get;
            set;
        }
        ///<summary>字段集 </summary>
        public LibCollection <LibField> Fields
        {
            get;
            set;
        }

        
    }

    public class DefTableCollection
    {
        #region 私有属性
        private List<DefineTable> _defTBList;
        private DefineTable _currentDefTable;
        #endregion

        #region 构造函数，公开属性
        public DefineTable this[string name]
        {
            get 
            {
                foreach (DefineTable item in _defTBList)
                {
                    if (string.Compare(item.TableName, name, false) == 0)
                    {
                        _currentDefTable = item;
                        break;
                    }
                }
                return _currentDefTable;
            }
        }

        public DefTableCollection()
        {
            if (_defTBList == null)
                _defTBList = new List<DefineTable>();
        }

        public string DataSourceName { get; set; }
        #endregion


        #region 公开方法

        /// <summary>添加元素</summary>
        /// <param name="defTable"></param>
        /// <returns></returns>
        public void Add(DefineTable defTable)
        {
            
        }

        #endregion
    }
}
