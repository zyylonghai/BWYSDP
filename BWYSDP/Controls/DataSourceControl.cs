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

            this.treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.treeView1.HideSelection = false;
            this.treeView1.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
        }

        void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
            return;
            //if ((e.State & TreeNodeStates.Selected) != 0)
            //{
            //    //演示为绿底白字  
            //    e.Graphics.FillRectangle(Brushes.DarkBlue, e.Node.Bounds);

            //    Font nodeFont = e.Node.NodeFont;
            //    if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
            //    e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0));
            //}
            //else
            //{
            //    e.DrawDefault = true;
            //}

            //if ((e.State & TreeNodeStates.Focused) != 0)
            //{
            //    using (Pen focusPen = new Pen(Color.Black))
            //    {
            //        focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            //        Rectangle focusBounds = e.Node.Bounds;
            //        focusBounds.Size = new Size(focusBounds.Width - 1,
            //        focusBounds.Height - 1);
            //        e.Graphics.DrawRectangle(focusPen, focusBounds);
            //    }
            //}
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
                deftbp.Dock = DockStyle.Fill;
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
                tbstructP.Dock = DockStyle.Fill;
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
                                    fieldNode.Text =string.Format ("{0}({1})",field.Name , field.DisplayName);
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
                            deftbp.Dock = DockStyle.Fill;
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
                            tbstrucp.Dock = DockStyle.Fill;
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
                            fieldp.Dock = DockStyle.Fill;
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
                else if (libnode.NodeType == NodeType.Field)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip4;
                }
                this.treeView1.SelectedNode = libnode;
            }
            else
            {

            }
        }
        /// <summary>
        /// 数据集节点上的右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>自定义表节点上的右键菜单</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary> 表结构节点上的右键菜单</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    //LibTreeNode fieldNode = new LibTreeNode();
                    //fieldNode.NodeId = Guid.NewGuid().ToString();
                    //fieldNode.Name = string.Format("{0}_Field{1}", currentTBStruct.Name, currentTBStruct.Fields.Count + 1);
                    //fieldNode.Text = fieldNode.Name;
                    //fieldNode.NodeType = NodeType.Field;
                    //curentNode.Nodes.Add(fieldNode);

                    //DefFieldProperty fieldP = new DefFieldProperty(fieldNode.NodeId);
                    //fieldP.Dock = DockStyle.Fill;
                    //this._fieldPropertylst.Add(fieldP);
                    //this.splitContainer1.Panel2.Controls.Add(fieldP);

                    //LibField field = new LibField();
                    //field.ID = fieldNode.NodeId;
                    //field.Name = fieldNode.Name;
                    //field.DisplayName = fieldNode.Text;
                    //currentTBStruct.Fields.Add(field);

                    //fieldP.SetPropertyValue(field, fieldNode);

                    string fieldnm= string.Format("{0}_Field{1}", currentTBStruct.Name, currentTBStruct.Fields.Count + 1);
                    DoCreateField(fieldnm, fieldnm, curentNode, currentTBStruct,false);
                    UpdateTabPageText();
                    break;
                #endregion
                case "CreatesysFields"://添加系统字段
                    Panel p = new Panel();
                    p.Dock = DockStyle.Fill;
                    p.Name = "pfieldcollection";
                    p.AutoScroll = true;
                    ListBox listBox = new ListBox();
                    listBox.Dock = DockStyle.Fill;
                    listBox.SelectionMode = SelectionMode.MultiExtended;
                    p.Controls.Add(listBox);
                    foreach (LibSysField sysfld in ModelManager.Sysfields)
                    {
                        listBox.Items.Add(sysfld);
                    }
                    FieldCollectionForm fielsform = new FieldCollectionForm(p);
                    DialogResult dialog = fielsform.ShowDialog(this);
                    if (dialog == DialogResult.OK)
                    {
                        foreach (LibSysField item in listBox.SelectedItems)
                        {

                            DoCreateField(item.Name, item.DisplayName, curentNode, currentTBStruct,true);
                        }
                        UpdateTabPageText();
                    }
                    break;
            }
        }

        /// <summary>字段节点上的右键菜单</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip4_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode = (LibTreeNode)this.treeView1.SelectedNode;
            LibDefineTable defineTB = _ds.DefTables.FindFirst("ID", ((LibTreeNode)curentNode.Parent.Parent).NodeId);
            LibDataTableStruct currentTBStruct = defineTB.TableStruct.FindFirst("ID", ((LibTreeNode)curentNode.Parent).NodeId);
            switch (e.ClickedItem.Name)
            {
                case "deleteField"://删除字段节点
                   //LibField f= currentTBStruct.Fields.FindFirst("ID", curentNode.NodeId);
                   //currentTBStruct.Fields.Remove(f);
                    currentTBStruct.Fields.Remove("ID", curentNode.NodeId);
                   DefFieldProperty fp= this._fieldPropertylst.FirstOrDefault(i => i.Name == curentNode.NodeId);
                   if (fp != null)
                       this._fieldPropertylst.Remove(fp);
                   this.treeView1.Nodes.Remove(curentNode);
                   UpdateTabPageText();
                    break;
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

        public void CreateTableStructToDB()
        {
            //foreach (TBStructProperty tbstruct in _tbStructPropertylst)
            //{
            //    tbstruct.CreateTableStruct();
            //}
            foreach (LibDefineTable deftb in _ds.DefTables)
            {
                foreach (LibDataTableStruct tb in deftb.TableStruct)
                {
                    ModelDesignProject.UpdateTableStruct(tb);
                }
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
        /// <summary>
        /// 设置tabpage的标题后都一个*。表示已被修改
        /// </summary>
        private void UpdateTabPageText()
        {
            TabPage page = (TabPage)this.Parent;
            if (!page.Text.Contains(SysConstManage.Asterisk))
            {
                page.Text += SysConstManage.Asterisk;
            }
        }

        private void DoCreateField(string fieldnm, string displaynm, LibTreeNode currentNode, LibDataTableStruct currentTBStruct,bool sysfield)
        {
            LibTreeNode fieldNode = new LibTreeNode();
            fieldNode.NodeId = Guid.NewGuid().ToString();
            fieldNode.Name = fieldnm;
            fieldNode.Text = displaynm;
            fieldNode.NodeType = NodeType.Field;
            currentNode.Nodes.Add(fieldNode);

            DefFieldProperty fieldP = new DefFieldProperty(fieldNode.NodeId);
            fieldP.Dock = DockStyle.Fill;
            this._fieldPropertylst.Add(fieldP);
            this.splitContainer1.Panel2.Controls.Add(fieldP);

            LibField field = new LibField();
            field.ID = fieldNode.NodeId;
            field.Name = fieldNode.Name;
            field.DisplayName = fieldNode.Text;
            field.IsActive = true;
            field.AllowNull = true;
            field.SysField = sysfield;
            
            currentTBStruct.Fields.Add(field);

            fieldP.SetPropertyValue(field, fieldNode);
        }

        //private void CreateNode()
        //{
            
        //}

        #endregion


    }
}
