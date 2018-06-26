using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.CORE;

namespace SDPCRL.COM.ModelManager
{
    [Serializable]
    public class DataSource
    {
        #region 私有属性
        private int  _dsid;
        private string _datasourceName;

        #endregion

        #region 公开属性
        /// <summary>数据源ID</summary>
        [LibXmlAttribute("ds_txtDSID")]
        public int DSID
        {
            get { return _dsid; }
            set { _dsid = value; }
        }
        /// <summary>数据源名称</summary>
        [LibXmlAttribute("ds_txtDSNm")]
        public string DataSourceName
        {
            get { return _datasourceName; }
            set { _datasourceName = value; }
        }
        /// <summary>数据源显示名称</summary>
        //public string DSDisplayText
        //{
        //    get;
        //    set;
        //}
        /// <summary>所属包</summary>
        [LibXmlAttribute("ds_txtPackage")]
        public string Package
        {
            get;
            set;
        }
        /// <summary>自定义表集合</summary>
        public LibCollection<DefineTable> DefTables
        {
            get;
            set;
        }
        #endregion


    }

    [Serializable]
    public class LibDataSource
    {
        #region 私有属性
        private string  _dsid;
        private string _datasourceName;

        #endregion

        #region 公开属性
        /// <summary>数据源ID</summary>
        [LibXmlAttribute("ds_txtDSID")]
        public string DSID
        {
            get { return _dsid; }
            set { _dsid = value; }
        }
        /// <summary>数据源名称</summary>
        [LibXmlAttribute("ds_txtDSNm")]
        public string DataSourceName
        {
            get { return _datasourceName; }
            set { _datasourceName = value; }
        }
        /// <summary>所属包</summary>
        [LibXmlAttribute("ds_txtPackage")]
        public string Package
        {
            get;
            set;
        }
        /// <summary>自定义表集合</summary>
        public LibCollection<LibDefineTable> DefTables
        {
            get;
            set;
        }
        #endregion


    }
}
