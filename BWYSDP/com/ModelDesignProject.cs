using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.COM.ModelManager;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using SDPCRL.CORE;
using SDPCRL.COM;
using SDPCRL.CORE.FileUtils;

namespace BWYSDP.com
{
    /// <summary>模型设计工厂类</summary>
    public class ModelDesignProject
    {
        private static Hashtable _dataSourceContain = Hashtable.Synchronized(new Hashtable());
        private static Hashtable _formSourceContain = new Hashtable();
        private static Hashtable _permissionSourceContain = new Hashtable();
        private static DSList _dsList = new DSList();

        #region 公开函数

        #region 旧代码
        /// <summary>创建数据源</summary>
        /// <param name="dsId"></param>
        public static void CreateDS(DataSource dataSource)
        {
            if (!_dataSourceContain.ContainsValue(dataSource))
            {
                //DataSource ds = new DataSource();
                //ds.DSID = dsId;
                _dataSourceContain.Add(dataSource.DSID, dataSource);
            }
        }

        /// <summary>根据DSName获取数据源</summary>
        /// <param name="dsName">数据源名称</param>
        /// <returns></returns>
        public static DataSource GetDataSource(string dsName)
        {
            foreach (DataSource item in _dataSourceContain.Values)
            {
                if (string.Compare(item.DataSourceName, dsName, true) == 0)
                {
                    return item;
                }
            }
            return DoGetDSFromXML(dsName);
        }

        /// <summary>当前DSID是否存在</summary>
        /// <param name="dsId"></param>
        /// <returns></returns>
        public static bool ExistId(int dsId)
        {
            foreach (int key in _dataSourceContain.Keys)
            {
                if (key == dsId)
                    return true;
            }
            return DoExistId(dsId);
        }

        /// <summary>当前数据源名称是否存在</summary>
        /// <param name="dataSourceName"></param>
        /// <returns></returns>
        public static bool ExistDSName(string dataSourceName)
        {
            foreach (DataSource item in _dataSourceContain.Values)
            {
                if (string.Compare(item.DataSourceName, dataSourceName, true) == 0)
                {
                    return true;
                }
            }
            return DoExistDSName(dataSourceName);
        }

        /// <summary>获取数据源信息列表</summary>
        /// <returns></returns>
        public static List<DataSource> GetDataSourceList()
        {
            return DoGetDataSourceList();
        }

        /// <summary>根据DSID获取数据源</summary>
        /// <param name="dsId"></param>
        /// <returns></returns>
        public static DataSource GetDataSource(int dsId)
        {
            return _dataSourceContain.ContainsKey(dsId) ? _dataSourceContain[dsId] as DataSource : DoGetDSFromXML(dsId);
        }

        public static void DoSaveDS()
        {

        }
        public static void DoSaveDS(string dataSourceName)
        {
            foreach (DataSource item in _dataSourceContain.Values)
            {
                if (string.Compare(item.DataSourceName, dataSourceName, true) == 0)
                {
                    SaveDataSource(item);
                }
            }
        }

        /// <summary>保存某个数据源</summary>
        /// <param name="dsId"></param>
        public static void DoSaveDS(int dsId)
        {
            if (_dataSourceContain.ContainsKey(dsId))
            {
                DataSource ds = (DataSource)_dataSourceContain[dsId];
                SaveDataSource(ds);
                //SerializerUtils.xmlserialzaition(ds, string.Format("{0}\\{1}\\{2}.xml", SysConstManage.DSFileRootPath, ds.Package, ds.DataSourceName));
            }
        }
        /// <summary>创建数据表对象</summary>
        /// <param name="ds"></param>
        public static void CreateTableObj(DataSource ds)
        {
            DBModelOperation dbModelOperation = new DBModelOperation();
            dbModelOperation.CreateDataSourceObj(ds);
        }

        /// <summary>创建数据表对象</summary>
        /// <param name="dsId"></param>
        public static void CreateTableObj(int dsId)
        {

        }

        /// <summary>根据控件类型，设置控件的值</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controls"></param>
        /// <param name="valueType"></param>
        public static void DoSetPropertyValue<T>(Control.ControlCollection controls, T valueType)
        {
            foreach (Control item in controls)
            {
                PropertyInfo[] propertis = valueType.GetType().GetProperties();
                foreach (PropertyInfo info in propertis)
                {
                    object[] attrArray = info.GetCustomAttributes(typeof(LibXmlAttributeAttribute), true);
                    if (attrArray.Length > 0)
                    {
                        LibXmlAttributeAttribute attr = attrArray[0] as LibXmlAttributeAttribute;
                        if (string.Compare(attr.ControlNm, item.Name, false) == 0)
                        {
                            if (CheckEnumFields(item, info, valueType)) //dropdownlist控件
                            {
                            }
                            else if (string.Compare(info.PropertyType.Name, "Boolean", true) == 0)
                            {
                                item.Text = LibSysUtils.BooleanToText((bool)info.GetValue(valueType, null));
                            }
                            else
                                item.Text = info.GetValue(valueType, null).ToString();
                        }
                    }
                }
            }
        }
        #endregion

        #region 2018.5.24

        /// <summary>
        /// 初始化 模型菜单列表
        /// </summary>
        public static void InitialModelTree(TreeView tree)
        {
            if (tree != null && tree.Nodes.Count > 0)
                tree.Nodes.Clear();
            LibTreeNode node;
            List<NodeInfo> infolist = DoReadNodes("/Root");
            foreach (NodeInfo item in infolist)
            {
                node = new LibTreeNode();

                if (string.Compare(item.NodeName, SysConstManage.ClassNodeNm) == 0)//分类节点
                {
                    node.Text = item.Attributions[SysConstManage.AtrrName];
                    node.Name = node.Text;
                    node.NodeType = NodeType.Class;
                }
                else if (string.Compare(item.NodeName, SysConstManage.FuncNodeNm) == 0)//功能节点
                {
                    node.Text = item.InnerText;
                    node.Name = item.Attributions[SysConstManage.AtrrName];
                    node.NodeType = NodeType.Func;
                    node.Package = item.Attributions[SysConstManage.AtrrPackage];
                }
                node.OriginalName = node.Name;
                LibTreeNode child = new LibTreeNode(string.Empty);
                child.Name = "-1";
                node.Nodes.Add(child);
                tree.Nodes.Add(node);
            }
        }
        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="parent"></param>
        public static void GetChildNode(LibTreeNode parent)
        {
            string express = string.Empty;
            if (parent != null)
            {
                List<NodeInfo> childs = null;
                LibTreeNode node;
                LibTreeNode preParent = (LibTreeNode)parent.Parent;
                parent.Nodes.RemoveByKey("-1");
                if (parent.NodeType == NodeType.Class)
                {
                    express = string.Format("{0}[@{1}='{2}']", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, parent.Name);
                    //else if((int)parent.Tag == 1)
                    //    express = string.Format("{0}[@name='{1}']", SysConstManage.FuncNodeNm, parent.Name);
                    #region 组织express的值
                    while (preParent != null)
                    {
                        if (preParent.NodeType == NodeType.Class)
                        {
                            express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                        }
                        else if (preParent.NodeType == NodeType.Func)
                        {
                            express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.FuncNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                        }

                        preParent = (LibTreeNode)preParent.Parent;
                    }
                    #endregion
                    childs = DoReadNodes(string.Format("/Root/{0}", express));
                    foreach (NodeInfo item in childs)
                    {
                        if (parent.Nodes.Find(item.Attributions[SysConstManage.AtrrName], false).Count() > 0)
                        {
                            continue;
                        }
                        node = new LibTreeNode();
                        //node.Name = item.Attributions[SysConstManage.AtrrName];
                        if (string.Compare(item.NodeName, SysConstManage.ClassNodeNm) == 0)//分类节点
                        {
                            node.Text = item.Attributions[SysConstManage.AtrrName];
                            node.Name = node.Text;
                            node.NodeType = NodeType.Class;
                        }
                        else if (string.Compare(item.NodeName, SysConstManage.FuncNodeNm) == 0)//功能节点
                        {
                            node.Text = item.InnerText;
                            node.Name = item.Attributions[SysConstManage.AtrrName];
                            node.NodeType = NodeType.Func;
                            node.Package = item.Attributions[SysConstManage.AtrrPackage];
                        }
                        node.OriginalName = node.Name;
                        LibTreeNode child = new LibTreeNode(string.Empty);
                        child.Name = "-1";
                        node.Nodes.Add(child);
                        parent.Nodes.Add(node);
                    }
                }
                else
                {
                    FileOperation fileoperation = new FileOperation();
                    fileoperation.FilePath = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, SysConstManage.DataSourceNm, parent.Package, parent.Name);
                    if (fileoperation.ExistsFile())
                    {
                        AddFuncNode(parent, NodeType.DataModel);
                    }
                    fileoperation.FilePath = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, SysConstManage.FormSourceNm, parent.Package, parent.Name);
                    if (fileoperation.ExistsFile())
                    {
                        AddFuncNode(parent, NodeType.FormModel);
                    }
                    fileoperation.FilePath = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, SysConstManage.PermissionSourceNm, parent.Package, parent.Name);
                    if (fileoperation.ExistsFile())
                    {
                        AddFuncNode(parent, NodeType.PermissionModel);
                    }
                }
            }
        }
        /// <summary>添加ModelTreeTemp文件的xml节点</summary>
        /// <param name="newNode"></param>
        public static void AddXmlNode(LibTreeNode newNode)
        {
            if (newNode != null)
            {
                LibTreeNode preParent = (LibTreeNode)newNode.Parent;
                string express = string.Empty;
                #region 组织express的值
                express = string.Format("{0}[@{1}='{2}']", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, preParent.Name);
                preParent = (LibTreeNode)preParent.Parent;
                while (preParent != null)
                {
                    if (preParent.NodeType == NodeType.Class)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }
                    else if (preParent.NodeType == NodeType.Func)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.FuncNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }

                    preParent = (LibTreeNode)preParent.Parent;
                }
                #endregion

                XMLOperation xmlOperation = new XMLOperation(SysConstManage.ModelTemp);
                NodeInfo nodeinfo = new NodeInfo();
                LibXMLAttributCollection attributcollection = new LibXMLAttributCollection();
                switch (newNode.NodeType)
                {
                    case NodeType.Class:
                        nodeinfo.NodeName = SysConstManage.ClassNodeNm;
                        break;
                    case NodeType.Func:
                        nodeinfo.NodeName = SysConstManage.FuncNodeNm;
                        attributcollection.Add(SysConstManage.AtrrPackage, newNode.Package);
                        break;
                }
                //nodeinfo.NodeName =newNode.NodeType==NodeType.Class SysConstManage.ClassNodeNm;
                nodeinfo.InnerText = newNode.Text;
                attributcollection.Add(SysConstManage.AtrrName, newNode.Name);
                nodeinfo.Attributions = attributcollection;
                xmlOperation.AddNode(nodeinfo, string.Format("/Root/{0}", express));
            }
        }
        /// <summary>
        /// 修改ModelTreeTemp文件的xml节点
        /// </summary>
        /// <param name="currentNode"></param>
        public static void UpdateXmlNode(LibTreeNode currentNode)
        {
            if (currentNode != null)
            {
                LibTreeNode preParent = (LibTreeNode)currentNode.Parent;
                string express = string.Empty;
                #region 组织express的值
                express = string.Format("{0}[@{1}='{2}']", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, currentNode.OriginalName);
                while (preParent != null)
                {
                    if (preParent.NodeType == NodeType.Class)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }
                    else if (preParent.NodeType == NodeType.Func)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.FuncNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }

                    preParent = (LibTreeNode)preParent.Parent;
                }
                #endregion
                XMLOperation xmlOperation = new XMLOperation(SysConstManage.ModelTemp);
                NodeInfo nodeinfo = new NodeInfo();
                LibXMLAttributCollection attributcollection = new LibXMLAttributCollection();
                switch (currentNode.NodeType)
                {
                    case NodeType.Class:
                        nodeinfo.NodeName = SysConstManage.ClassNodeNm;
                        break;
                    case NodeType.Func:
                        nodeinfo.NodeName = SysConstManage.FuncNodeNm;
                        attributcollection.Add(SysConstManage.AtrrPackage, currentNode.Package);
                        break;
                }
                nodeinfo.InnerText = currentNode.Text;
                attributcollection.Add(SysConstManage.AtrrName, currentNode.Name);
                nodeinfo.Attributions = attributcollection;
                if (xmlOperation.UpdateNode(nodeinfo, string.Format("/Root/{0}", express)))
                {
                    currentNode.OriginalName = currentNode.Name;
                }
            }
        }

        /// <summary>删除ModelTreeTemp文件的xml节点</summary>
        /// <param name="currentNode"></param>
        public static void DeleteXmlNode(LibTreeNode currentNode)
        {
            if (currentNode != null)
            {
                LibTreeNode preParent = (LibTreeNode)currentNode.Parent;
                string express = string.Empty;
                #region 组织express的值
                express = string.Format("{0}[@{1}='{2}']", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, currentNode.OriginalName);
                while (preParent != null)
                {
                    if (preParent.NodeType == NodeType.Class)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }
                    else if (preParent.NodeType == NodeType.Func)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.FuncNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }

                    preParent = (LibTreeNode)preParent.Parent;
                }
                #endregion
                XMLOperation xmlOperation = new XMLOperation(SysConstManage.ModelTemp);
                xmlOperation.DeletNode(string.Format("/Root/{0}", express));
            }
        }

        public static void CreatModelFile(LibTreeNode treeNode)
        {
            FileOperation fileoperation = new FileOperation();
            string dir = string.Empty;
            switch (treeNode.NodeType)
            {
                case NodeType.DataModel :
                    dir = SysConstManage.DataSourceNm;
                    break;
                case NodeType.FormModel :
                    dir = SysConstManage.FormSourceNm;
                    break;
                case NodeType.PermissionModel:
                    dir = SysConstManage.PermissionSourceNm;
                    break;
            }
            fileoperation.FilePath = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, dir, treeNode.Package, treeNode.Name);
            fileoperation.CreateFile(true);
        }

        public static void DeleteModelFile(LibTreeNode treeNode)
        {
 
        }

        private static List<NodeInfo> DoReadNodes(string express)
        {
            XMLOperation xmlOperation = new XMLOperation(SysConstManage.ModelTemp);
            ILibXMLNodeRead noderead = xmlOperation.NodeRead(express);
            List<NodeInfo> result = new List<NodeInfo>();
            while (!noderead.EOF)
            {
                if (noderead.HasChildNode)
                {
                    NodeInfo childinfo = null;
                    for (int index = 0; (childinfo = noderead.ReadChild(index)) != null; index++)
                    {
                        result.Add(childinfo);
                    }
                }
                noderead.ReadNext();
            }
            return result;
        }

        private static void AddFuncNode(LibTreeNode parent, NodeType type)
        {
            LibTreeNode node = new LibTreeNode();
            node.Text = parent.Text;
            node.Name = parent.Name;
            node.OriginalName = node.Name;
            node.NodeType = type;
            node.Package = parent.Package;
            parent.Nodes.Add(node);
        }

        #region 模型对象操作
        public static DataSource GetDataSourceById(string dataSourceId)
        {
            if (_dataSourceContain.ContainsKey(dataSourceId))
            {
                return (DataSource)_dataSourceContain[dataSourceId];
            }
            else
            {
                DataSource ds=ModelManager.GetDataSource(dataSourceId);
                _dataSourceContain.Add(dataSourceId, ds);
                return ds;
            }
            
        }
        #endregion

        #endregion

        #endregion

        #region 私有函数

        #region 旧代码
        private static void SaveDataSource(DataSource ds)
        {
            DSInfo dsinfo = new DSInfo();
            dsinfo.DSID = ds.DSID;
            dsinfo.Name = ds.DataSourceName;
            //dsinfo.DISPLAYTEXT = ds.DSDisplayText;
            dsinfo.PACKAGE = ds.Package;
            if (_dsList.DSInfoCollection == null)
                _dsList.DSInfoCollection = new LibCollection<DSInfo>();
            if (!CheckDSList(dsinfo))
            {
                _dsList.DSInfoCollection.Add(dsinfo);
            }
            SerializerUtils.xmlserialzaition(_dsList, SysConstManage.DSListFile);
            SerializerUtils.xmlserialzaition(ds, string.Format("{0}\\{1}\\{2}.xml", SysConstManage.DSFileRootPath, ds.Package, ds.DataSourceName));
        }

        /// <summary>检查数据源列表是否已经存在对应数据</summary>
        /// <param name="dsInfo"></param>
        /// <returns></returns>
        private static bool CheckDSList(DSInfo dsInfo)
        {
            foreach (DSInfo item in _dsList.DSInfoCollection)
            {
                if (item.DSID == dsInfo.DSID)
                {
                    return true;
                }
            }
            return false;
        }

        private static DataSource DoGetDSFromXML(int dsId)
        {
            return ModelManager.DoGetDataSource(dsId);
        }

        private static DataSource DoGetDSFromXML(string dsName)
        {
            return ModelManager.DoGetDataSource(dsName);
        }

        private static bool DoExistId(int dsId)
        {
            return ModelManager.DoIsExistId(dsId);
        }


        private static bool DoExistDSName(string dataSourceName)
        {
            return ModelManager.DoIsExistDSName(dataSourceName);
        }

        private static List<DataSource> DoGetDataSourceList()
        {
            return ModelManager.DoGetDataSourceListEx(ref _dsList);
        }

        /// <summary>检查具体制定 的dropdowlist控件</summary>
        /// <param name="control"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        private static bool CheckEnumFields(Control control, PropertyInfo info, object valueType)
        {
            bool result = false;
            if (string.Compare(control.Name, "fd_combFieldType", true) == 0)
            {
                control.Text = ReSourceManage.GetResource((LibFieldType)info.GetValue(valueType, null));
                result = true;
            }
            return result;
        }
        #endregion
        #endregion

    }

    public class LibTreeNode : TreeNode
    {
        public LibTreeNode()
            : base()
        {

        }
        public LibTreeNode(string text)
            : base(text)
        {

        }

        public void CopyTo(LibTreeNode newNode)
        {
            if (newNode != null)
            {
                PropertyInfo[] propertys = newNode.GetType().GetProperties();
                foreach (PropertyInfo p in propertys)
                {
                    PropertyInfo info = this.GetType().GetProperty(p.Name);
                    if (info.GetSetMethod() != null)
                        p.SetValue(newNode, info.GetValue(this, null), null);
                }
                //newNode.NodeType = this.NodeType;
                //newNode.Name = this.Name;
                //newNode.Text = this.Text;
                //newNode.OriginalName = this.OriginalName;
                //newNode.Package = this.Package;
            }
            else
            {
                throw new LibExceptionBase("参数newNode不允许为null");
            }
        }

        #region 公开属性
        /// <summary>原始名称</summary>
        public string OriginalName { get; set; }
        /// <summary>所属包</summary>
        public string Package { get; set; }

        /// <summary>节点类型</summary>
        public NodeType NodeType { get; set; }
        #endregion
    }
    /// <summary>节点类型</summary>
    public enum NodeType
    {
        /// <summary>分类节点</summary>
        [LibReSource("分类节点")]
        Class = -1,
        /// <summary>功能节点</summary>
        [LibReSource("功能节点")]
        Func = 1,
        /// <summary>数据源模型节点</summary>
        [LibReSource("数据源模型节点")]
        DataModel = 2,
        /// <summary>排版模型节点</summary>
        [LibReSource("排版模型节点")]
        FormModel = 3,
        /// <summary>权限模型节点</summary>
        [LibReSource("权限模型节点")]
        PermissionModel = 4,
        /// <summary>特殊功能</summary>
        [LibReSource("特殊功能节点")]
        SpectFunc = 5,
        /// <summary>报表功能</summary>
        [LibReSource("报表功能节点")]
        ReportFunc = 6
    }
}
