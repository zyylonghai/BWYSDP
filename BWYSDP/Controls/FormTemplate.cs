using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWYSDP.com;
using SDPCRL.COM.ModelManager.FormTemplate;
using SDPCRL.CORE;
using SDPCRL.COM.ModelManager;

namespace BWYSDP.Controls
{
    public partial class FormTemplate : UserControl
    {
        private LibTreeNode _funNode;
        private LibFormPage _fm = null;
        private FormPageProperty _fmProperty = null;
        private List<FormGroupProperty> _formgrouplst = null;
        private List<FormGroupFieldProperty> _formgroupfieldlst = null;

        private List<GridGroupProperty> _gridgrouplst = null;
        private List<GridGroupFieldProperty> _gridgroupfieldlst = null;
        public FormTemplate(LibTreeNode funcNode)
        {
            this._funNode = funcNode;
            InitializeComponent();
            _fmProperty = new FormPageProperty();
            _fmProperty.Dock = DockStyle.Fill;

            _formgrouplst = new List<FormGroupProperty>();
            _formgroupfieldlst = new List<FormGroupFieldProperty>();
            _gridgrouplst = new List<GridGroupProperty>();
            _gridgroupfieldlst = new List<GridGroupFieldProperty>();
            this.splitContainer1.Panel2.Controls.Add(_fmProperty);

            this.treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.treeView1.HideSelection = false;
            this.treeView1.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
        }

        private void FormTemplate_Load(object sender, EventArgs e)
        {
            _fm = ModelDesignProject.GetFormSourceByFormId(_funNode.Name);
            LibTreeNode fmNode = new LibTreeNode();
            fmNode.Name = _funNode.Name;
            fmNode.Text = ReSourceManage.GetResource(NodeType.FormPanel);
            fmNode.NodeType = NodeType.FormPanel;

            if (_fm.FormGroups != null)
            {
                #region 加载信息组节点及其字段子节点
                foreach (LibFormGroup fg in _fm.FormGroups)
                {
                    #region 创建信息组节点
                    LibTreeNode fmgroupNode = new LibTreeNode();
                    fmgroupNode.NodeId = fg.FormGroupID;
                    fmgroupNode.NodeType = NodeType.FormGroup;
                    fmgroupNode.Name = fg.FormGroupName;
                    fmgroupNode.Text = fg.FormGroupDisplayNm;
                    fmNode.Nodes.Add(fmgroupNode);
                    #endregion
                    foreach (LibFormGroupField fd in fg.FmGroupFields)
                    {
                        #region 创建信息组字段节点
                        LibTreeNode fmgroupfield = new LibTreeNode();
                        fmgroupfield.NodeType = NodeType.FormGroup_Field;
                        fmgroupfield.NodeId = fd.ID;
                        fmgroupfield.Name = fd.Name;
                        fmgroupfield.Text = fd.Name;
                        fmgroupNode.Nodes.Add(fmgroupfield);
                        #endregion
                    }
                }
                #endregion
            }
            if (_fm.GridGroups != null)
            {
                #region 加载表格组节点及其字段子节点
                foreach (LibGridGroup gd in _fm.GridGroups)
                {
                    #region 创建表格组节点
                    LibTreeNode gdgroupNode = new LibTreeNode();
                    gdgroupNode.NodeId = gd.GridGroupID;
                    gdgroupNode.NodeType = NodeType.GridGroup;
                    gdgroupNode.Name = gd.GridGroupName;
                    gdgroupNode.Text = gd.GridGroupDisplayNm;
                    fmNode.Nodes.Add(gdgroupNode);
                    #endregion
                    foreach (LibGridGroupField fd in gd.GdGroupFields)
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
                #endregion
            }
            this.treeView1.Nodes.Add(fmNode);
            this.treeView1.SelectedNode = fmNode;
        }
        void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
            return;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LibTreeNode libnode = (LibTreeNode)e.Node;
            if (libnode == null) return;
            bool exists = false;
            switch (libnode.NodeType)
            {
                case NodeType.FormPanel:
                    if (this._fmProperty == null)
                    {
                        _fmProperty = new FormPageProperty();
                        this.splitContainer1.Panel2.Controls.Add(_fmProperty);
                    }
                    SetPanel2ControlsVisible(_fmProperty);

                    _fm.FormName = string.IsNullOrEmpty(_fm.FormName) ? _funNode.Text : _fm.FormName;
                    _fm.Package = string.IsNullOrEmpty(_fm.Package) ? _funNode.Package : _fm.Package;
                    if (ModelDesignProject.ExitsDataSource(_fm.FormId, _fm.Package))
                    {
                        _fm.DSID = _fm.FormId;
                    }
                    _fmProperty.SetPropertyValue(_fm, libnode);
                    break;
                case NodeType.FormGroup:
                    if (_formgrouplst != null)
                    {
                        foreach (FormGroupProperty item in _formgrouplst)
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
                            FormGroupProperty fmgroupp = new FormGroupProperty(libnode.NodeId);
                            fmgroupp.Dock = DockStyle.Fill;
                            this._formgrouplst.Add(fmgroupp);
                            this.splitContainer1.Panel2.Controls.Add(fmgroupp);
                            fmgroupp.SetPropertyValue(_fm.FormGroups.FindFirst("FormGroupID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(fmgroupp);
                        }
                    }
                    break;
                case NodeType.FormGroup_Field:
                    if (_formgroupfieldlst != null)
                    {
                        foreach (FormGroupFieldProperty item in _formgroupfieldlst)
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
                            FormGroupFieldProperty fmgroupfdp = new FormGroupFieldProperty(libnode.NodeId);
                            fmgroupfdp.Dock = DockStyle.Fill;
                            this._formgroupfieldlst.Add(fmgroupfdp);
                            this.splitContainer1.Panel2.Controls.Add(fmgroupfdp);
                            LibFormGroup libfg = _fm.FormGroups.FindFirst("FormGroupID", ((LibTreeNode)libnode.Parent).NodeId);
                            fmgroupfdp.SetPropertyValue(libfg.FmGroupFields.FindFirst("ID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(fmgroupfdp);
                        }
                    }
                    break;
                case NodeType.GridGroup:
                    if (_gridgrouplst != null)
                    {
                        foreach (GridGroupProperty item in _gridgrouplst)
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
                            GridGroupProperty gridgroupp = new GridGroupProperty(libnode.NodeId);
                            gridgroupp.Dock = DockStyle.Fill;
                            this._gridgrouplst.Add(gridgroupp);
                            this.splitContainer1.Panel2.Controls.Add(gridgroupp);
                            gridgroupp.SetPropertyValue(_fm.GridGroups.FindFirst("GridGroupID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(gridgroupp);
                        }
                    }
                    break;
                case NodeType.GridGroup_Field:

                    if (_gridgroupfieldlst != null)
                    {
                        foreach (GridGroupFieldProperty item in _gridgroupfieldlst)
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
                            GridGroupFieldProperty gdgroupfdp = new GridGroupFieldProperty(libnode.NodeId);
                            gdgroupfdp.Dock = DockStyle.Fill;
                            this._gridgroupfieldlst.Add(gdgroupfdp);
                            this.splitContainer1.Panel2.Controls.Add(gdgroupfdp);
                            LibGridGroup libfg = _fm.GridGroups.FindFirst("GridGroupID", ((LibTreeNode)libnode.Parent).NodeId);
                            gdgroupfdp.SetPropertyValue(libfg.GdGroupFields.FindFirst("ID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(gdgroupfdp);
                        }
                    }
                    break;
            }
        }




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

        #endregion

        #region 公开函数
        /// <summary>获取控件值并赋值给LibFormPage对象</summary>
        public void GetControlValueBindToFM()
        {
            _fmProperty.GetControlsValue();
            foreach (FormGroupProperty fg in _formgrouplst)
            {
                fg.GetControlsValue();
            }
            foreach (FormGroupFieldProperty fgfield in _formgroupfieldlst)
            {
                fgfield.GetControlsValue();
            }
            foreach (GridGroupProperty grid in _gridgrouplst)
            {
                grid.GetControlsValue();
            }
            foreach (GridGroupFieldProperty gdfield in _gridgroupfieldlst)
            {
                gdfield.GetControlsValue();
            }
        }
        #endregion

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                LibTreeNode libnode = (LibTreeNode)e.Node;
                if (libnode.NodeType == NodeType.FormPanel)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip1;
                }
                else if (libnode.NodeType == NodeType.FormGroup)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip2;
                }
                else if (libnode.NodeType == NodeType.GridGroup)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip3;
                }
                //else if (libnode.NodeType == NodeType.Field)
                //{
                //    libnode.ContextMenuStrip = this.contextMenuStrip4;
                //}
                this.treeView1.SelectedNode = libnode;
            }
            else
            {

            }
        }
        /// <summary>
        /// 页面容器节点上的右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode = (LibTreeNode)this.treeView1.SelectedNode;
            switch (e.ClickedItem.Name)
            {
                case "addFormGroup": //添加信息组
                    //树节点
                    LibTreeNode fmgroupNode = new LibTreeNode();
                    fmgroupNode.NodeId = Guid.NewGuid().ToString();
                    fmgroupNode.NodeType = NodeType.FormGroup;
                    if (_fm.FormGroups == null) _fm.FormGroups = new LibCollection<LibFormGroup>();
                    fmgroupNode.Name = string.Format("FormGroup{0}", _fm.FormGroups.Count + 1);
                    fmgroupNode.Text = string.Format("{0}{1}", ReSourceManage.GetResource(NodeType.FormGroup), _fm.FormGroups.Count + 1);
                    curentNode.Nodes.Add(fmgroupNode);

                    //控件属性
                    FormGroupProperty fgProperty = new FormGroupProperty(fmgroupNode.NodeId);
                    fgProperty.Dock = DockStyle.Fill;
                    _formgrouplst.Add(fgProperty);
                    this.splitContainer1.Panel2.Controls.Add(fgProperty);

                    //对应实体
                    LibFormGroup libfg = new LibFormGroup();
                    libfg.FormGroupID = fmgroupNode.NodeId;
                    libfg.FormGroupName = fmgroupNode.Name;
                    libfg.FormGroupDisplayNm = fmgroupNode.Text;
                    _fm.FormGroups.Add(libfg);

                    fgProperty.SetPropertyValue(libfg, fmgroupNode);
                    break;
                case "addGridGroup"://添加表格组
                    //树节点
                    LibTreeNode gridNode = new LibTreeNode();
                    gridNode.NodeId = Guid.NewGuid().ToString();
                    gridNode.NodeType = NodeType.GridGroup;
                    if (_fm.GridGroups == null) _fm.GridGroups = new LibCollection<LibGridGroup>();
                    gridNode.Name = string.Format("GridGroup{0}", _fm.GridGroups.Count + 1);
                    gridNode.Text = string.Format("{0}{1}", ReSourceManage.GetResource(NodeType.GridGroup), _fm.GridGroups.Count + 1);
                    curentNode.Nodes.Add(gridNode);

                    //控件属性
                    GridGroupProperty gridProperty = new GridGroupProperty(gridNode.NodeId);
                    gridProperty.Dock = DockStyle.Fill;
                    _gridgrouplst.Add(gridProperty);
                    this.splitContainer1.Panel2.Controls.Add(gridProperty);

                    //对应实体
                    LibGridGroup libgrid = new LibGridGroup();
                    libgrid.GridGroupID = gridNode.NodeId;
                    libgrid.GridGroupName = gridNode.Name;
                    libgrid.GridGroupDisplayNm = gridNode.Text;
                    _fm.GridGroups.Add(libgrid);

                    gridProperty.SetPropertyValue(libgrid, gridNode);

                    break;
            }
            UpdateTabPageText();
        }

        /// <summary>
        /// 信息组节点上的右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode = (LibTreeNode)this.treeView1.SelectedNode;
            switch (e.ClickedItem.Name)
            {
                case "addfmGroupFields"://添加字段
                    LibFormGroup currentlibfmgroup = _fm.FormGroups.FindFirst("FormGroupID", curentNode.NodeId);
                    if (currentlibfmgroup.FmGroupFields == null) currentlibfmgroup.FmGroupFields = new LibCollection<LibFormGroupField>();
                    LibDataSource ds = ModelDesignProject.GetDataSourceById(_fm.DSID);
                    Panel p = new Panel();
                    p.Dock = DockStyle.Fill;
                    p.Name = "pfieldcollection";
                    p.AutoScroll = true;
                    TreeView tree = new TreeView();
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
                                    deftbnode.Nodes.Add(dtstructnode);
                                    if (dtstruct.Fields != null)
                                    {
                                        foreach (LibField fld in dtstruct.Fields)
                                        {
                                            _node = new LibTreeNode();
                                            _node.Name = fld.Name;
                                            _node.Text = fld.DisplayName;
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
                                    fieldNode.NodeType = NodeType.FormGroup_Field;
                                    fieldNode.Name = f.Name;
                                    fieldNode.Text = fieldNode.Name;
                                    curentNode.Nodes.Add(fieldNode);

                                    //控件属性
                                    FormGroupFieldProperty fgfieldProperty = new FormGroupFieldProperty(fieldNode.NodeId);
                                    _formgroupfieldlst.Add(fgfieldProperty);
                                    fgfieldProperty.Dock = DockStyle.Fill;
                                    this.splitContainer1.Panel2.Controls.Add(fgfieldProperty);

                                    //对应实体
                                    LibFormGroupField libfgfield = new LibFormGroupField();
                                    libfgfield.ID = fieldNode.NodeId;
                                    libfgfield.Name = f.Name;
                                    libfgfield.FromTableNm = tbstruct.Name;
                                    libfgfield.FromDefTableNm = deftb.Name;
                                    libfgfield.DisplayName = f.Text;
                                    LibField libfield = ds.DefTables.FindFirst("TableName", deftb.Name).TableStruct.FindFirst("Name", tbstruct.Name).Fields.FindFirst("Name", f.Name);
                                    switch (libfield.FieldType)
                                    {
                                        case LibFieldType.Date:
                                            libfgfield.ElemType = ElementType.Date;
                                            break;
                                        case LibFieldType.DateTime:
                                            libfgfield.ElemType = ElementType.DateTime;
                                            break;
                                    }
                                    if (libfield.SourceField != null)
                                    {
                                        libfgfield.ElemType = ElementType.Search;
                                    }
                                    currentlibfmgroup.FmGroupFields.Add(libfgfield);

                                    fgfieldProperty.SetPropertyValue(libfgfield, fieldNode);

                                    #endregion
                                }
                            }
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// 表格组节点上的右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode = (LibTreeNode)this.treeView1.SelectedNode;
            switch (e.ClickedItem.Name)
            {
                case "addgridField"://添加字段
                    LibGridGroup currentlibgridgroup = _fm.GridGroups.FindFirst("GridGroupID", curentNode.NodeId);
                    if (currentlibgridgroup.GdGroupFields == null) currentlibgridgroup.GdGroupFields = new LibCollection<LibGridGroupField>();
                    LibDataSource ds = ModelDesignProject.GetDataSourceById(_fm.DSID);
                    Panel p = new Panel();
                    p.Dock = DockStyle.Fill;
                    p.Name = "pfieldcollection";
                    p.AutoScroll = true;
                    TreeView tree = new TreeView();
                    tree.AfterCheck += new TreeViewEventHandler(tree_AfterCheck);
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
                            deftbnode.NodeType = NodeType.DefindTable;
                            deftbnode.Text = string.Format("{0}({1})", deftb.DisplayName, deftb.TableName);
                            tree.Nodes.Add(deftbnode);
                            if (deftb.TableStruct != null)
                            {
                                foreach (LibDataTableStruct dtstruct in deftb.TableStruct)
                                {
                                    LibTreeNode dtstructnode = new LibTreeNode();
                                    dtstructnode.NodeType = NodeType.TableStruct;
                                    dtstructnode.Name = dtstruct.Name;
                                    dtstructnode.Text = string.Format("{0}({1})", dtstruct.DisplayName, dtstruct.Name);
                                    deftbnode.Nodes.Add(dtstructnode);
                                    if (dtstruct.Fields != null)
                                    {
                                        foreach (LibField fld in dtstruct.Fields)
                                        {
                                            _node = new LibTreeNode();
                                            _node.NodeType = NodeType.Field;
                                            _node.Name = fld.Name;
                                            _node.Text = fld.DisplayName;
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
                                    GridGroupFieldProperty gdfieldProperty = new GridGroupFieldProperty(fieldNode.NodeId);
                                    _gridgroupfieldlst.Add(gdfieldProperty);
                                    gdfieldProperty.Dock = DockStyle.Fill;
                                    this.splitContainer1.Panel2.Controls.Add(gdfieldProperty);

                                    //对应实体
                                    LibGridGroupField libgdfield = new LibGridGroupField();
                                    libgdfield.ID = fieldNode.NodeId;
                                    libgdfield.Name = f.Name;
                                    libgdfield.FromTableNm = tbstruct.Name;
                                    libgdfield.FromDefTableNm = deftb.Name;
                                    libgdfield.DisplayName = f.Text;
                                    currentlibgridgroup.GdGroupFields.Add(libgdfield);

                                    gdfieldProperty.SetPropertyValue(libgdfield, fieldNode);

                                    #endregion
                                }
                            }
                        }
                    }

                    break;
            }
        }

        void tree_AfterCheck(object sender, TreeViewEventArgs e)
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
    }
}
