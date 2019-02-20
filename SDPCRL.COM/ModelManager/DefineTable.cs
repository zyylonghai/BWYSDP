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
        [LibAttribute("DefTB_txtTableName")]
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        /// <summary>编号 </summary>
        [LibAttribute("DefTB_txtID")]
        public int ID
        {
            get;
            set;
        }
        ///<summary>显示名称</summary>
        [LibAttribute("DefTB_txtDisplayName")]
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

    /// <summary>自定义数据表</summary>
    [Serializable]
    public class LibDefineTable
    {
        #region 私有属性
        private string _tableName;
        #endregion

        #region 公开属性
        /// <summary>编号 </summary>
        [LibAttribute("DefTB_txtID", LibControlType.TextBox, "编号", true)]
        [XmlAttribute]
        public string ID
        {
            get;
            set;
        }
        /// <summary>名称</summary>
        [LibAttribute("DefTB_txtTableName",LibControlType.TextBox ,"表名")]
        [XmlAttribute]
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        ///<summary>显示名称</summary>
        [LibAttribute("DefTB_txtDisplayName", LibControlType.TextBox, "显示名称")]
        [XmlAttribute]
        public string DisplayName
        {
            get;
            set;
        }
        /// <summary>表结构集合</summary>
        public LibCollection<LibDataTableStruct> TableStruct
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
        [LibAttribute("tbStruct_txtTableName")]
        public string Name
        {
            get;
            set;
        }
        ///<summary>显示名称</summary>
        [LibAttribute("tbStruct_txtTableDisplayName")]
        public string DisplayName
        {
            get;
            set;
        }
        ///<summary>忽略创建表结构</summary>
        [LibAttribute("tbStruct_combcreateTBStruct")]
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

    /// <summary>数据表结构</summary>
    [Serializable]
    public class LibDataTableStruct
    {
        /// <summary>编号 </summary>
        [LibAttribute("tbStruct_txtID", LibControlType.TextBox, "编号", true)]
        [XmlAttribute]
        public string ID
        {
            get;
            set;
        }
        ///<summary>名称</summary>
        [LibAttribute("tbStruct_txtTableName", LibControlType.TextBox, "名称")]
        public string Name
        {
            get;
            set;
        }
        ///<summary>显示名称</summary>
        [LibAttribute("tbStruct_txtTableDisplayName", LibControlType.TextBox, "显示名称")]
        public string DisplayName
        {
            get;
            set;
        }
        ///<summary>创建表结构</summary>
        [LibAttribute("tbStruct_combcreateTBStruct",LibControlType .Combox ,"创建结构")]
        public bool Ignore
        {
            get;
            set;
        }
        ///<summary>表Index</summary>
        [LibAttribute("tbStruct_tableIndex", LibControlType.TextBox, "表Index")]
        public int TableIndex
        {
            get;
            set;
        }

        /// <summary>关联表Index</summary>
        [LibAttribute("tbStruct_joinTableIndex", LibControlType.TextBox, "关联表Index")]
        public int JoinTableIndex
        {
            get;
            set;
        }

        /// <summary> 关联字段</summary>
        [LibAttribute("tbStruct_JoinFields", LibControlType.TextAndBotton, "关联字段")]
        public List<string> JoinFields { get; set; }
        /// <summary>
        /// 主键集
        /// </summary>
        [LibAttribute("tbStruct_txtPrimarykey", LibControlType.TextAndBotton, "主键")]
        public List <string > PrimaryKey { get; set; }

        ///<summary>字段集 </summary>
        public LibCollection<LibField> Fields
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
