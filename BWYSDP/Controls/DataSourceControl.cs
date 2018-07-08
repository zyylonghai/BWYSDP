using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWYSDP.com;
using SDPCRL.CORE;
using SDPCRL.COM.ModelManager;

namespace BWYSDP.Controls
{
    public partial class DataSourceControl : UserControl
    {
        //public DefTBProperty _defTBProperty;
        private DataSourceProperty _dsProperty;
        private List<DefTBProperty> _defTBPropertylst;
        private List<TBStructProperty> _tbStructPropertylst;
        private List<DefFieldProperty> _fieldPropertylst;
        private LibTreeNode _funNode;
        private LibDataSource _ds=null;
        public DataSourceControl(LibTreeNode funcNode)
        {
            this._funNode = funcNode;
            InitializeComponent();
            _dsProperty = new DataSourceProperty();
            _defTBPropertylst = new List<DefTBProperty>();
            _tbStructPropertylst = new List<TBStructProperty>();
            _fieldPropertylst = new List<DefFieldProperty>();
            this.splitContainer1.Panel2.Controls.Add(_dsProperty);
        }

        private void DataSourceControl_Load(object sender, EventArgs e)
        {
            _ds = ModelDesignProject.GetDataSourceById(_funNode.Name);
            //数据集节点
            LibTreeNode dsNode = new LibTreeNode();
            dsNode.Name = _funNode.Name;
            dsNode.Text = ReSourceManage.GetResource(NodeType.DefDataSet);
            dsNode.NodeType = NodeType.DefDataSet;

            if (_ds.DefTables == null)
            {
                _ds.DefTables = new LibCollection<LibDefineTable>();
                //自定义表节点
                #region
                LibTreeNode defTBNode = new LibTreeNode();
                defTBNode.NodeId = Guid.NewGuid().ToString();
                defTBNode.Name = string.Format("{0}", _funNode.Name);
                defTBNode.Text = string.Format("{0}({1})", _funNode.Text, defTBNode.Name);
                defTBNode.NodeType = NodeType.DefindTable;
                dsNode.Nodes.Add(defTBNode);

                //this._defTBPropertylst = new List<DefTBProperty>();
                DefTBProperty deftbp = new DefTBProperty(defTBNode.NodeId);
                this._defTBPropertylst.Add(deftbp);
                this.splitContainer1 .Panel2 .Controls .Add (deftbp);

                LibDefineTable definetb = new LibDefineTable();
                definetb.ID = defTBNode.NodeId;
                definetb.TableName = defTBNode.Name;
                definetb.DisplayName = defTBNode.Text;
                _ds.DefTables.Add(definetb);

                deftbp.SetPropertyValue(definetb, defTBNode);
                #endregion

                //数据结构表
                #region
                LibTreeNode tablestruc = new LibTreeNode();
                tablestruc.NodeId = Guid.NewGuid().ToString();
                tablestruc.Name = string.Format("{0}", _funNode.Name);
                tablestruc.Text =string.Format ("{0}-{1}","数据表", _funNode.Name);
                tablestruc.NodeType = NodeType.TableStruct;
                defTBNode.Nodes.Add(tablestruc);

                //this._tbStructPropertylst = new List<TBStructProperty>();
                TBStructProperty tbstructP = new TBStructProperty(tablestruc.NodeId);
                this._tbStructPropertylst.Add(tbstructP);
                this.splitContainer1.Panel2.Controls.Add(tbstructP);


                LibDataTableStruct tbstruct = new LibDataTableStruct();
                tbstruct.ID = tablestruc.NodeId;
                tbstruct.Name = tablestruc.Name;
                tbstruct.DisplayName = tablestruc.Text;
                definetb.TableStruct = new LibCollection<LibDataTableStruct>();
                definetb.TableStruct.Add(tbstruct);

                tbstructP.SetPropertyValue(tbstruct, tablestruc);
                #endregion
            }
            else
            {
                LibTreeNode node=null;
                LibTreeNode dtstructNode = null;
                LibTreeNode fieldNode = null;
                foreach (LibDefineTable item in _ds.DefTables)
                {
                    node = new LibTreeNode();
                    node.NodeId = item.ID;
                    node.Name = item.TableName;
                    node.Text = string.Format("{0}({1})", item .DisplayName, item.TableName);
                    node.NodeType = NodeType.DefindTable;
                    dsNode.Nodes.Add(node);
                    if (item.TableStruct != null)
                    {
                        foreach (LibDataTableStruct dtstruct in item.TableStruct)
                        {
                            #region 添加表结构节点
                            dtstructNode = new LibTreeNode();
                            dtstructNode.NodeId = dtstruct.ID;
                            dtstructNode.Name = dtstruct.Name;
                            dtstructNode .Text = string.Format("{0}-{1}", "数据表", dtstruct.Name);
                            dtstructNode.NodeType = NodeType.TableStruct;
                            node.Nodes.Add(dtstructNode);
                            #endregion
                            if (dtstruct.Fields != null)
                            {
                                foreach (LibField field in dtstruct.Fields)
                                {
                                    #region 添加字段节点
                                    fieldNode = new LibTreeNode();
                                    fieldNode.NodeId = field.ID;
                                    fieldNode.Name = field.Name;
                                    fieldNode.Text = field.DisplayName;
                                    fieldNode.NodeType = NodeType.Field;
                                    dtstructNode.Nodes.Add(fieldNode);
                                    #endregion
                                }
                            }

                        }
                    }
                }
            }
            this.treeView1.Nodes.Add(dsNode);
            this.treeView1.SelectedNode = dsNode;
 
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LibTreeNode libnode = (LibTreeNode)e.Node;
            if (libnode == null) return;
            bool exists = false;
            switch (libnode.NodeType)
            {
                case NodeType.DefDataSet :
                    if (this._dsProperty == null)
                    {
                        _dsProperty = new DataSourceProperty();
                        this.splitContainer1.Panel2.Controls.Add(_dsProperty);
                    }
                    SetPanel2ControlsVisible(_dsProperty);
                   
                    _ds.DataSourceName = string.IsNullOrEmpty(_ds.DataSourceName) ? _funNode.Text : _ds.DataSourceName;
                    _ds.Package = string.IsNullOrEmpty(_ds.Package) ? _funNode.Package : _ds.Package;
                    _dsProperty.SetPropertyValue(_ds, libnode);
                    break;
                case NodeType.DefindTable :
                    if (this._defTBPropertylst!=null)
                    {
                        foreach (DefTBProperty item in _defTBPropertylst)
                        {
                            if (string.Compare(item.Name, libnode.NodeId) == 0)
                            {
                                SetPanel2ControlsVisible(item);
                                exists = true;
                                break;
                            }
                        }
                        if (!exists) //还未创建对应的控件
                        {
                            DefTBProperty deftbp = new DefTBProperty(libnode.NodeId);
                            this._defTBPropertylst.Add(deftbp);
                            this.splitContainer1.Panel2.Controls.Add(deftbp);
                            deftbp.SetPropertyValue(_ds.DefTables.FindFirst("ID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(deftbp);
                        }
                    }

                    break;
                case NodeType.TableStruct :
                    if (this._tbStructPropertylst != null)
                    {
                        foreach (TBStructProperty item in _tbStructPropertylst)
                        {
                            if (string.Compare(item.Name, libnode.NodeId) == 0)
                            {
                                SetPanel2ControlsVisible(item);
                                exists = true;
                                break;
                            }
                        }
                        if (!exists) //还未创建对应的控件
                        {
                            TBStructProperty tbstrucp = new TBStructProperty(libnode.NodeId);
                            this._tbStructPropertylst.Add(tbstrucp);
                            this.splitContainer1.Panel2.Controls.Add(tbstrucp);
                            LibDefineTable deftb = _ds.DefTables.FindFirst("ID", ((LibTreeNode)libnode.Parent).NodeId);
                            tbstrucp.SetPropertyValue(deftb.TableStruct.FindFirst("ID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(tbstrucp);
                        }
                    }
                    break;
                case NodeType.Field :
                    if (this._fieldPropertylst!= null)
                    {
                        foreach (DefFieldProperty item in _fieldPropertylst)
                        {
                            if (string.Compare(item.Name, libnode.NodeId) == 0)
                            {
                                SetPanel2ControlsVisible(item);
                                exists = true;
                                break;
                            }
                        }
                        if (!exists) //还未创建对应的控件
                        {
                            DefFieldProperty fieldp = new DefFieldProperty(libnode.NodeId);
                            this._fieldPropertylst.Add(fieldp);
                            this.splitContainer1.Panel2.Controls.Add(fieldp);
                            LibDefineTable deftb = _ds.DefTables.FindFirst("ID", ((LibTreeNode)libnode.Parent.Parent).NodeId);
                            LibDataTableStruct dtstruct = deftb.TableStruct.FindFirst("ID", ((LibTreeNode)libnode.Parent).NodeId);
                            fieldp.SetPropertyValue(dtstruct.Fields.FindFirst("ID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(fieldp);
                        }
                    }
                    break;
            }
        }


        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                LibTreeNode libnode = (LibTreeNode)e.Node;
                if (libnode.NodeType == NodeType.DefDataSet)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip1;
                }
                else if (libnode.NodeType == NodeType.DefindTable)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip2;
                }
                else if (libnode.NodeType == NodeType.TableStruct)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip3;
                }
                this.treeView1.SelectedNode = libnode;
            }
            else
            {

            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode =(LibTreeNode) this.treeView1.SelectedNode;
            switch (e.ClickedItem.Name)
            {
                case "CreateDefineTable"://新建自定义表
                #region
                LibTreeNode defTBNode = new LibTreeNode();
                defTBNode.NodeId = Guid.NewGuid().ToString();
                defTBNode.Name = string.Format("DefineTable{0}",_ds.DefTables.Count+1);
                defTBNode.Text = defTBNode.Name;
                defTBNode.NodeType = NodeType.DefindTable;
                curentNode.Nodes.Add(defTBNode);

                DefTBProperty deftbp = new DefTBProperty(defTBNode.NodeId);
                this._defTBPropertylst.Add(deftbp);
                this.splitContainer1 .Panel2 .Controls .Add (deftbp);

                LibDefineTable definetb = new LibDefineTable();
                definetb.ID = defTBNode.NodeId;
                definetb.TableName = defTBNode.Name;
                definetb.DisplayName = defTBNode.Text;
                _ds.DefTables.Add(definetb);

                deftbp.SetPropertyValue(definetb, defTBNode);
                #endregion
                break;
            }
        }

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode = (LibTreeNode)this.treeView1.SelectedNode;
            LibDefineTable currentDefTB=_ds.DefTables.FindFirst("ID",curentNode.NodeId );
            if (currentDefTB.TableStruct == null) currentDefTB.TableStruct = new LibCollection<LibDataTableStruct>();
            switch (e.ClickedItem.Name)
            {
                case "CreateTableStruct"://新建表结构
                #region
                LibTreeNode tablestruc = new LibTreeNode();
                tablestruc.NodeId = Guid.NewGuid().ToString();
                tablestruc.Name = string.Format("{0}_TableStruct{1}",currentDefTB.TableName,currentDefTB.TableStruct .Count +1);
                tablestruc.Text =tablestruc.Name;
                tablestruc.NodeType = NodeType.TableStruct;
                curentNode.Nodes.Add(tablestruc);

                TBStructProperty tbstructP = new TBStructProperty(tablestruc.NodeId);
                this._tbStructPropertylst.Add(tbstructP);
                this.splitContainer1.Panel2.Controls.Add(tbstructP);


                LibDataTableStruct tbstruct = new LibDataTableStruct();
                tbstruct.ID = tablestruc.NodeId;
                tbstruct.Name = tablestruc.Name;
                tbstruct.DisplayName = tablestruc.Text;
                tbstruct.Ignore = true;
                currentDefTB.TableStruct .Add (tbstruct );

                tbstructP.SetPropertyValue(tbstruct, tablestruc);
                #endregion
                    break;
            }
        }

        private void contextMenuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode = (LibTreeNode)this.treeView1.SelectedNode;
            LibDefineTable defineTB = _ds.DefTables.FindFirst("ID", ((LibTreeNode)curentNode.Parent).NodeId);
            LibDataTableStruct  currentTBStruct = defineTB.TableStruct.FindFirst("ID", curentNode.NodeId);
            if (currentTBStruct.Fields == null) currentTBStruct.Fields = new LibCollection<LibField>();
            switch (e.ClickedItem.Name)
            {
                case "CreateField": //新建字段
                    #region
                    LibTreeNode fieldNode = new LibTreeNode();
                    fieldNode.NodeId = Guid.NewGuid().ToString();
                    fieldNode.Name = string.Format("{0}_Field{1}", currentTBStruct.Name, currentTBStruct.Fields.Count + 1);
                    fieldNode.Text = fieldNode.Name;
                    fieldNode.NodeType = NodeType.Field;
                    curentNode.Nodes.Add(fieldNode);

                    DefFieldProperty fieldP = new DefFieldProperty(fieldNode.NodeId);
                    this._fieldPropertylst.Add(fieldP);
                    this.splitContainer1.Panel2.Controls.Add(fieldP);

                    LibField field = new LibField();
                    field.ID = fieldNode.NodeId;
                    field.Name = fieldNode.Name;
                    field.DisplayName = fieldNode.Text;
                    currentTBStruct.Fields.Add(field);

                    fieldP.SetPropertyValue(field, fieldNode);
                    break;
                    #endregion
            }
        }

        //private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        //{
        //    LibTreeNode node = (LibTreeNode)e.Node;
        //    switch (node.NodeType)
        //    {
        //        case NodeType.DefindTable ://当前节点是 自定义表节点，则添加 数据表结构节点
        //            foreach (LibDataTableStruct dtstruct in _ds.DefTables.FindFirst("ID", node.NodeId).TableStruct)
        //            {
        //                LibTreeNode childNode = new LibTreeNode();
        //                childNode.NodeId = dtstruct.ID;
        //                childNode.Name = dtstruct.Name;
        //                childNode.Text = string.Format("{0}-{1}", "数据表", dtstruct.Name);
        //                childNode.NodeType = NodeType.TableStruct;
        //                node.Nodes.Add(childNode);
        //            }
        //            break;
        //        case NodeType.TableStruct://当前节点是 数据表结构节点，则添加 字段节点
        //            LibDefineTable deftbNode = _ds.DefTables.FindFirst("ID", ((LibTreeNode)node.Parent).NodeId);
        //            foreach (LibField field in deftbNode.TableStruct.FindFirst ("ID",node.NodeId ).Fields)
        //            {
        //                LibTreeNode childNode = new LibTreeNode();
        //                childNode.NodeId = field.ID;
        //                childNode.Name = field.Name;
        //                childNode.Text = string.Format("{0}", field.DisplayName);
        //                childNode.NodeType = NodeType.Field;
        //                node.Nodes.Add(childNode);
        //            }
        //            break;
        //    }
        //}

        #region 公开函数

        /// <summary>获取控件值并赋值给LibDataSource对象</summary>
        public void GetControlValueBindToDS()
        {
            foreach (DefTBProperty deftb in _defTBPropertylst)
            {
                deftb.GetControlsValue();
            }
            foreach (TBStructProperty tbstruct in _tbStructPropertylst)
            {
                tbstruct.GetControlsValue();
            }
            foreach (DefFieldProperty field in _fieldPropertylst)
            {
                field.GetControlsValue();
            }
        }
        #endregion

        #region 私有自定义函数

        private void SetPanel2ControlsVisible(Control ctl)
        {
            foreach (Control item in this.splitContainer1.Panel2.Controls)
            {
                item.Visible = item == ctl ? true : false;
            }
        }
        //private void CreateNode()
        //{
            
        //}

        #endregion


    }
}
