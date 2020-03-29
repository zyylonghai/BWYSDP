using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BWYSDP.com;
using SDPCRL.COM.ModelManager.Reports;
using SDPCRL.CORE;
using System.Reflection;
using SDPCRL.COM.ModelManager;

namespace BWYSDP.Controls
{
    public partial class ReportSourceControl : UserControl
    {
        private LibTreeNode _funNode;
        private LibReportsSource _rpt = null;
        private List<ReportGridProperty> _gridgrouplst = null;
        private List<ReportFieldProperty> _reportFieldlst = null;
        ReportSourceProperty _rptproperty = null;
        public ReportSourceControl()
        {
            InitializeComponent();
        }

        public ReportSourceControl(LibTreeNode funcNode)
            :this()
        {
            this._funNode = funcNode;
            _rptproperty = new ReportSourceProperty();
            _rptproperty.Dock = DockStyle.Fill;

            this.splitContainer1.Panel2.Controls.Add(_rptproperty);

            _gridgrouplst = new List<ReportGridProperty>();
            _reportFieldlst = new List<ReportFieldProperty>();

            this.treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.treeView1.HideSelection = false;
            this.treeView1.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
        }

        public void GetControlValueBindToRpt()
        {
            _rptproperty.GetControlsValue();
            foreach (ReportGridProperty reportGrid in _gridgrouplst)
            {
                reportGrid.GetControlsValue();
            }
            foreach (ReportFieldProperty field in _reportFieldlst)
            {
                field.GetControlsValue();
            }
        }

        void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
            return;
        }

        private void ReportSourceControl_Load(object sender, EventArgs e)
        {
            _rpt = ModelDesignProject.GetLibReportsSourceById(_funNode.Name);

            LibTreeNode rptNode = new LibTreeNode();
            rptNode.Name = _funNode.Name;
            rptNode.Text = ReSourceManage.GetResource(NodeType.ReportPanel);
            rptNode.NodeType = NodeType.ReportPanel;
            if (_rpt.GridGroups != null)
            {
                foreach (LibReportGrid grid in _rpt.GridGroups)
                {
                    #region 创建表格组节点
                    LibTreeNode gdgroupNode = new LibTreeNode();
                    gdgroupNode.NodeId = grid.GridGroupID;
                    gdgroupNode.NodeType = NodeType.GridGroup;
                    gdgroupNode.Name = grid.GridGroupName;
                    gdgroupNode.Text = grid.GridGroupDisplayNm;
                    rptNode.Nodes.Add(gdgroupNode);
                    #endregion
                    if (grid.ReportFields != null)
                        foreach (LibReportField  fd in grid.ReportFields)
                        {
                            #region 创建表格组字段节点
                            LibTreeNode gdgroupfield = new LibTreeNode();
                            gdgroupfield.NodeType = NodeType.GridGroup_Field;
                            gdgroupfield.NodeId = fd.ID;
                            gdgroupfield.Name = fd.Name;
                            gdgroupfield.Text = fd.Name;
                            gdgroupNode.Nodes.Add(gdgroupfield);
                            #endregion
                        }
                }
            }
            this.treeView1.Nodes.Add(rptNode);
            this.treeView1.SelectedNode = rptNode;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LibTreeNode libnode = (LibTreeNode)e.Node;
            if (libnode == null) return;
            bool exists = false;
            switch (libnode.NodeType)
            {
                case NodeType.ReportPanel:
                    if (this._rptproperty == null)
                    {
                        _rptproperty = new ReportSourceProperty();
                        this.splitContainer1.Panel2.Controls.Add(_rptproperty);
                    }
                    SetPanel2ControlsVisible(_rptproperty);

                    _rpt.ReportName = string.IsNullOrEmpty(_rpt.ReportName) ? _funNode.Text : _rpt.ReportName;
                    _rpt.Package = string.IsNullOrEmpty(_rpt.Package) ? _funNode.Package : _rpt.Package;
                    //if (ModelDesignProject.ExitsDataSource(_fm.FormId, _fm.Package))
                    //{
                    //    _fm.DSID = _fm.FormId;
                    //}
                    _rptproperty.SetPropertyValue(_rpt, libnode);
                    break;
                case NodeType.GridGroup:
                    if (_gridgrouplst != null)
                    {
                        foreach (ReportGridProperty item in _gridgrouplst)
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
                            ReportGridProperty gridgroupp = new ReportGridProperty(libnode.NodeId);
                            gridgroupp.Dock = DockStyle.Fill;
                            this._gridgrouplst.Add(gridgroupp);
                            this.splitContainer1.Panel2.Controls.Add(gridgroupp);
                            gridgroupp.SetPropertyValue(_rpt.GridGroups.FindFirst("GridGroupID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(gridgroupp);
                        }
                    }
                    break;
                case NodeType.GridGroup_Field:
                    if (_reportFieldlst != null)
                    {
                        foreach (ReportFieldProperty item in _reportFieldlst)
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
                            ReportFieldProperty gdgroupfdp = new ReportFieldProperty(libnode.NodeId);
                            gdgroupfdp.Dock = DockStyle.Fill;
                            this._reportFieldlst.Add(gdgroupfdp);
                            this.splitContainer1.Panel2.Controls.Add(gdgroupfdp);
                            LibReportGrid librptgd = _rpt .GridGroups.FindFirst("GridGroupID", ((LibTreeNode)libnode.Parent).NodeId);
                            gdgroupfdp.SetPropertyValue(librptgd.ReportFields.FindFirst("ID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(gdgroupfdp);
                        }
                    }
                    break;
            }
        }

        private void SetPanel2ControlsVisible(Control ctl)
        {
            foreach (Control item in this.splitContainer1.Panel2.Controls)
            {
                item.Visible = item == ctl ? true : false;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                LibTreeNode libnode = (LibTreeNode)e.Node;
                if (libnode.NodeType == NodeType.ReportPanel)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip1;
                }
                if (libnode.NodeType == NodeType.GridGroup)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip2;
                }
                this.treeView1.SelectedNode = libnode;
            }
            else
            {

            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode = (LibTreeNode)this.treeView1.SelectedNode;
            switch (e.ClickedItem.Name)
            {
                case "AddGrid": //添加表格组
                    if (_rpt.GridGroups == null) _rpt.GridGroups = new LibCollection<LibReportGrid>();
                    AddNodeBindEntityToCtr<LibReportGrid,ReportGridProperty>(_gridgrouplst, _rpt.GridGroups, "GridGroupID", "GridGroupName", "GridGroupDisplayNm", "GridGroup", curentNode, NodeType.GridGroup);
                    break;
            }
        }

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode = (LibTreeNode)this.treeView1.SelectedNode;
            LibReportGrid currentlibrptgrid = _rpt.GridGroups.FindFirst("GridGroupID", curentNode.NodeId);
            switch (e.ClickedItem.Name)
            {
                case "AddFromDSField": //添加数据源字段
                    if (currentlibrptgrid.ReportFields == null) currentlibrptgrid.ReportFields = new LibCollection<LibReportField>();
                    var existfields = currentlibrptgrid.ReportFields.Tolist();
                    LibDataSource ds = ModelDesignProject.GetDataSourceById(currentlibrptgrid.DSID);
                    Panel p = new Panel();
                    p.Dock = DockStyle.Fill;
                    p.Name = "pfieldcollection";
                    p.AutoScroll = true;
                    TreeView tree = new TreeView();
                    tree.AfterCheck += new TreeViewEventHandler(Gridtree_AfterCheck);
                    tree.CheckBoxes = true;
                    tree.Dock = DockStyle.Fill;
                    p.Controls.Add(tree);
                    LibTreeNode _node;
                    if (ds.DefTables != null)
                    {
                        #region 收集数据源字段
                        foreach (LibDefineTable deftb in ds.DefTables)
                        {
                            LibTreeNode deftbnode = new LibTreeNode();
                            deftbnode.Name = deftb.TableName;
                            deftbnode.Text = string.Format("{0}({1})", deftb.DisplayName, deftb.TableName);
                            tree.Nodes.Add(deftbnode);
                            if (deftb.TableStruct != null)
                            {
                                foreach (LibDataTableStruct dtstruct in deftb.TableStruct)
                                {
                                    LibTreeNode dtstructnode = new LibTreeNode();
                                    dtstructnode.Name = dtstruct.Name;
                                    dtstructnode.Text = string.Format("{0}({1})", dtstruct.DisplayName, dtstruct.Name);
                                    dtstructnode.NodeId = dtstruct.TableIndex.ToString ();
                                    deftbnode.Nodes.Add(dtstructnode);
                                    if (dtstruct.Fields != null)
                                    {
                                        foreach (LibField fld in dtstruct.Fields)
                                        {
                                            _node = new LibTreeNode();
                                            _node.Name = fld.Name;
                                            _node.Text = fld.DisplayName;

                                            _node.Checked = existfields.FirstOrDefault(i => i.Name == fld.Name && i.FromTableNm == dtstruct.Name) != null;
                                            dtstructnode.Nodes.Add(_node);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    FieldCollectionForm fielsform = new FieldCollectionForm(p);
                    DialogResult dialog = fielsform.ShowDialog(this);

                    if (dialog == DialogResult.OK)
                    {
                        curentNode.Nodes.Clear();
                        if (currentlibrptgrid.ReportFields != null && currentlibrptgrid.ReportFields.Count > 0)
                            currentlibrptgrid.ReportFields.RemoveAll();
                        foreach (LibTreeNode deftb in tree.Nodes)
                        {
                            foreach (LibTreeNode tbstruct in deftb.Nodes)
                            {
                                foreach (LibTreeNode f in tbstruct.Nodes)
                                {

                                    if (!f.Checked) continue;
                                    #region 添加节点
                                    //树节点

                                    LibTreeNode fieldNode = new LibTreeNode();
                                    fieldNode.NodeId = Guid.NewGuid().ToString();
                                    fieldNode.NodeType = NodeType.GridGroup_Field;
                                    fieldNode.Name = f.Name;
                                    fieldNode.Text = fieldNode.Name;
                                    curentNode.Nodes.Add(fieldNode);

                                    //控件属性
                                    ReportFieldProperty rptfieldProperty = new ReportFieldProperty(fieldNode.NodeId);
                                    _reportFieldlst.Add(rptfieldProperty);
                                    rptfieldProperty.Dock = DockStyle.Fill;
                                    this.splitContainer1.Panel2.Controls.Add(rptfieldProperty);

                                    //对应实体
                                    LibReportField librptfield = new LibReportField();
                                    librptfield.ID = fieldNode.NodeId;
                                    librptfield.Name = f.Name;
                                    librptfield.FromTableNm = tbstruct.Name;
                                    librptfield.FromDefTableNm = deftb.Name;
                                    librptfield.FromTableIndex = Convert.ToInt32(tbstruct.NodeId);
                                    librptfield.DisplayName = f.Text;

                                    currentlibrptgrid.ReportFields.Add(librptfield);

                                    rptfieldProperty.SetPropertyValue(librptfield, fieldNode);

                                    #endregion
                                }
                            }
                        }
                        UpdateTabPageText();
                    }
                    break;
                case "AddDefineField"://添加自定义字段
                    break;
            }
        }

        private void AddNodeBindEntityToCtr<T, PropertyT>(List<PropertyT> propertylst, LibCollection<T> libCollection, string entiyid, string entiynm, string entiydisplaytext, string name, LibTreeNode curentNode, NodeType nodeType)
            where PropertyT : BaseUserControl<T>
        {
            //树节点
            LibTreeNode Node = new LibTreeNode();
            Node.NodeId = Guid.NewGuid().ToString();
            Node.NodeType = nodeType;
            //if (_fm.BtnGroups == null) _fm.BtnGroups = new LibCollection<LibButtonGroup>();
            Node.Name = string.Format("{1}{0}", libCollection.Count + 1, name);
            Node.Text = string.Format("{0}{1}", ReSourceManage.GetResource(nodeType), libCollection.Count + 1);
            curentNode.Nodes.Add(Node);

            //控件属性
            PropertyT propertyT = Activator.CreateInstance<PropertyT>();
            ((UserControl)propertyT).Dock = DockStyle.Fill;
            propertylst.Add(propertyT);
            this.splitContainer1.Panel2.Controls.Add(propertyT);

            //对应实体
            T entity = Activator.CreateInstance<T>();
            Type type = typeof(T);
            PropertyInfo p = null;
            p = type.GetProperty(entiyid);
            p.SetValue(entity, Node.NodeId, null);
            p = type.GetProperty(entiynm);
            p.SetValue(entity, Node.Name, null);
            p = type.GetProperty(entiydisplaytext);
            p.SetValue(entity, Node.Text, null);
            //if (nodeType == NodeType.GridGroup)
            //{
            //    string[] pnm = { "HasAddRowButton", "HasEditRowButton", "HasDeletRowButton" };
            //    foreach (string nm in pnm)
            //    {
            //        p = type.GetProperty(nm);
            //        if (p != null)
            //            p.SetValue(entity, true, null);
            //    }
            //}
            libCollection.Add(entity);

            propertyT.SetPropertyValue(entity, Node);
        }

        void Gridtree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            LibTreeNode node = (LibTreeNode)e.Node;
            if (node.Nodes.Count > 0)
            {
                foreach (LibTreeNode item in node.Nodes)
                {
                    item.Checked = node.Checked;

                }
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
    }
}
