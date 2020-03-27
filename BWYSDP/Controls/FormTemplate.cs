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
using System.Reflection;

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

        private List<ButtonGroupProperty> _btngrouplst = null;
        private List<ButtonProperty> _btnlst = null;
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
            _btngrouplst = new List<ButtonGroupProperty>();
            _btnlst = new List<ButtonProperty>();
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
            if (_fm.ModuleOrder != null)
            {
                foreach (ModuleOrder item in _fm.ModuleOrder)
                {
                    switch (item.moduleType)
                    {
                        case ModuleType.FormGroup:
                            if (_fm.FormGroups != null)
                            {
                                #region 加载信息组节点及其字段子节点
                                foreach (LibFormGroup fg in _fm.FormGroups)
                                {
                                    if (fg.FormGroupID != item.ID) continue;
                                    #region 创建信息组节点
                                    LibTreeNode fmgroupNode = new LibTreeNode();
                                    fmgroupNode.NodeId = fg.FormGroupID;
                                    fmgroupNode.NodeType = NodeType.FormGroup;
                                    fmgroupNode.Name = fg.FormGroupName;
                                    fmgroupNode.Text = fg.FormGroupDisplayNm;
                                    fmNode.Nodes.Add(fmgroupNode);
                                    #endregion
                                    if (fg.FmGroupFields != null)
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
                            break;
                        case ModuleType.GridGroup:
                            if (_fm.GridGroups != null)
                            {
                                #region 加载表格组节点及其字段子节点
                                foreach (LibGridGroup gd in _fm.GridGroups)
                                {
                                    if (gd.GridGroupID != item.ID) continue;
                                    #region 创建表格组节点
                                    LibTreeNode gdgroupNode = new LibTreeNode();
                                    gdgroupNode.NodeId = gd.GridGroupID;
                                    gdgroupNode.NodeType = NodeType.GridGroup;
                                    gdgroupNode.Name = gd.GridGroupName;
                                    gdgroupNode.Text = gd.GridGroupDisplayNm;
                                    fmNode.Nodes.Add(gdgroupNode);
                                    #endregion
                                    if (gd.GdGroupFields != null)
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
                            break;
                        case ModuleType.ButtonGroup:
                            if (_fm.BtnGroups != null)
                            {
                                foreach (LibButtonGroup btngroup in _fm.BtnGroups)
                                {
                                    if (btngroup.BtnGroupID != item.ID) continue;
                                    #region 创建按钮组节点
                                    LibTreeNode btngroupNode = new LibTreeNode();
                                    btngroupNode.NodeId = btngroup.BtnGroupID;
                                    btngroupNode.NodeType = NodeType.ButtonGroup;
                                    btngroupNode.Name = btngroup.BtnGroupName;
                                    btngroupNode.Text = btngroup.BtnGroupDisplayNm;
                                    fmNode.Nodes.Add(btngroupNode);
                                    #endregion 
                                    if (btngroup.LibButtons != null)
                                    {
                                        foreach (LibButton btn in btngroup.LibButtons)
                                        {
                                            LibTreeNode btnnode = new LibTreeNode();
                                            btnnode.NodeId = btn.LibButtonID;
                                            btnnode.NodeType = NodeType.BtnGroup_button;
                                            btnnode.Name = btn.LibButtonName;
                                            btnnode.Text = btn.LibButtonDisplayNm;
                                            btngroupNode.Nodes.Add(btnnode);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            #region 旧代码
            //if (_fm.FormGroups != null)
            //{
            //    #region 加载信息组节点及其字段子节点
            //    foreach (LibFormGroup fg in _fm.FormGroups)
            //    {
            //        #region 创建信息组节点
            //        LibTreeNode fmgroupNode = new LibTreeNode();
            //        fmgroupNode.NodeId = fg.FormGroupID;
            //        fmgroupNode.NodeType = NodeType.FormGroup;
            //        fmgroupNode.Name = fg.FormGroupName;
            //        fmgroupNode.Text = fg.FormGroupDisplayNm;
            //        fmNode.Nodes.Add(fmgroupNode);
            //        #endregion
            //        foreach (LibFormGroupField fd in fg.FmGroupFields)
            //        {
            //            #region 创建信息组字段节点
            //            LibTreeNode fmgroupfield = new LibTreeNode();
            //            fmgroupfield.NodeType = NodeType.FormGroup_Field;
            //            fmgroupfield.NodeId = fd.ID;
            //            fmgroupfield.Name = fd.Name;
            //            fmgroupfield.Text = fd.Name;
            //            fmgroupNode.Nodes.Add(fmgroupfield);
            //            #endregion
            //        }
            //    }
            //    #endregion
            //}
            //if (_fm.GridGroups != null)
            //{
            //    #region 加载表格组节点及其字段子节点
            //    foreach (LibGridGroup gd in _fm.GridGroups)
            //    {
            //        #region 创建表格组节点
            //        LibTreeNode gdgroupNode = new LibTreeNode();
            //        gdgroupNode.NodeId = gd.GridGroupID;
            //        gdgroupNode.NodeType = NodeType.GridGroup;
            //        gdgroupNode.Name = gd.GridGroupName;
            //        gdgroupNode.Text = gd.GridGroupDisplayNm;
            //        fmNode.Nodes.Add(gdgroupNode);
            //        #endregion
            //        foreach (LibGridGroupField fd in gd.GdGroupFields)
            //        {
            //            #region 创建表格组字段节点
            //            LibTreeNode gdgroupfield = new LibTreeNode();
            //            gdgroupfield.NodeType = NodeType.GridGroup_Field;
            //            gdgroupfield.NodeId = fd.ID;
            //            gdgroupfield.Name = fd.Name;
            //            gdgroupfield.Text = fd.Name;
            //            gdgroupNode.Nodes.Add(gdgroupfield);
            //            #endregion
            //        }
            //    }
            //    #endregion
            //}
            #endregion
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
                case NodeType.ButtonGroup:
                    if (_btngrouplst != null)
                    {
                        var exist = _btngrouplst.FirstOrDefault(i => i.Name == libnode.NodeId);
                        if (exist != null)
                        {
                            SetPanel2ControlsVisible(exist);
                        }
                        else
                        {
                            ButtonGroupProperty btngroup = new ButtonGroupProperty(libnode.NodeId);
                            btngroup.Dock = DockStyle.Fill;
                            this._btngrouplst.Add(btngroup);
                            this.splitContainer1.Panel2.Controls.Add(btngroup);
                            btngroup.SetPropertyValue(_fm.BtnGroups.FindFirst("BtnGroupID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(btngroup);
                        }
                    }
                    break;
                case NodeType.BtnGroup_button:
                    if (_btnlst != null)
                    {
                        var exist = _btnlst.FirstOrDefault(i => i.Name == libnode.NodeId);
                        if (exist != null)
                        {
                            SetPanel2ControlsVisible(exist);
                        }
                        else
                        {
                            ButtonProperty btn = new ButtonProperty(libnode.NodeId);
                            btn.Dock = DockStyle.Fill;
                            this._btnlst.Add(btn);
                            this.splitContainer1.Panel2.Controls.Add(btn);
                            LibButtonGroup btngroup = _fm.BtnGroups.FindFirst("BtnGroupID", ((LibTreeNode)libnode.Parent).NodeId);
                            btn.SetPropertyValue(btngroup.LibButtons.FindFirst("LibButtonID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(btn);
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
            foreach (ButtonGroupProperty btngroup in _btngrouplst)
            {
                btngroup.GetControlsValue();
            }
            foreach (ButtonProperty btn in _btnlst)
            {
                btn.GetControlsValue();
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
                else if (libnode.NodeType == NodeType.ButtonGroup)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip4;
                }
                else if (libnode.NodeType == NodeType.BtnGroup_button || 
                         libnode .NodeType==NodeType.FormGroup_Field || 
                         libnode .NodeType==NodeType.GridGroup_Field)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip_btn;
                }
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
            if (_fm.ModuleOrder == null) _fm.ModuleOrder = new LibCollection<ModuleOrder>();
            switch (e.ClickedItem.Name)
            {
                case "addFormGroup": //添加信息组
                    if (_fm.FormGroups == null) _fm.FormGroups = new LibCollection<LibFormGroup>();
                    AddNodeBindEntityToCtr<LibFormGroup, FormGroupProperty>(_formgrouplst, _fm.FormGroups, "FormGroupID", "FormGroupName", "FormGroupDisplayNm", "FormGroup", curentNode, NodeType.FormGroup, ModuleType.FormGroup);
                    #region
                    ////树节点
                    //LibTreeNode fmgroupNode = new LibTreeNode();
                    //fmgroupNode.NodeId = Guid.NewGuid().ToString();
                    //fmgroupNode.NodeType = NodeType.FormGroup;
                    //if (_fm.FormGroups == null) _fm.FormGroups = new LibCollection<LibFormGroup>();
                    //fmgroupNode.Name = string.Format("FormGroup{0}", _fm.FormGroups.Count + 1);
                    //fmgroupNode.Text = string.Format("{0}{1}", ReSourceManage.GetResource(NodeType.FormGroup), _fm.FormGroups.Count + 1);
                    //curentNode.Nodes.Add(fmgroupNode);

                    ////控件属性
                    //FormGroupProperty fgProperty = new FormGroupProperty(fmgroupNode.NodeId);
                    //fgProperty.Dock = DockStyle.Fill;
                    //_formgrouplst.Add(fgProperty);
                    //this.splitContainer1.Panel2.Controls.Add(fgProperty);

                    ////对应实体
                    //LibFormGroup libfg = new LibFormGroup();
                    //libfg.FormGroupID = fmgroupNode.NodeId;
                    //libfg.FormGroupName = fmgroupNode.Name;
                    //libfg.FormGroupDisplayNm = fmgroupNode.Text;
                    //_fm.FormGroups.Add(libfg);

                    //fgProperty.SetPropertyValue(libfg, fmgroupNode);
                    //_fm.ModuleOrder.Add(new ModuleOrder { moduleType = ModuleType.FormGroup, ID = libfg.FormGroupID });
                    #endregion
                    break;
                    
                case "addGridGroup"://添加表格组
                    if (_fm.GridGroups == null) _fm.GridGroups = new LibCollection<LibGridGroup>();
                    AddNodeBindEntityToCtr<LibGridGroup, GridGroupProperty>(_gridgrouplst, _fm.GridGroups, "GridGroupID", "GridGroupName", "GridGroupDisplayNm", "GridGroup", curentNode, NodeType.GridGroup, ModuleType.GridGroup);
                    #region
                    ////树节点
                    //LibTreeNode gridNode = new LibTreeNode();
                    //gridNode.NodeId = Guid.NewGuid().ToString();
                    //gridNode.NodeType = NodeType.GridGroup;
                    //if (_fm.GridGroups == null) _fm.GridGroups = new LibCollection<LibGridGroup>();
                    //gridNode.Name = string.Format("GridGroup{0}", _fm.GridGroups.Count + 1);
                    //gridNode.Text = string.Format("{0}{1}", ReSourceManage.GetResource(NodeType.GridGroup), _fm.GridGroups.Count + 1);
                    //curentNode.Nodes.Add(gridNode);

                    ////控件属性
                    //GridGroupProperty gridProperty = new GridGroupProperty(gridNode.NodeId);
                    //gridProperty.Dock = DockStyle.Fill;
                    //_gridgrouplst.Add(gridProperty);
                    //this.splitContainer1.Panel2.Controls.Add(gridProperty);

                    ////对应实体
                    //LibGridGroup libgrid = new LibGridGroup();
                    //libgrid.GridGroupID = gridNode.NodeId;
                    //libgrid.GridGroupName = gridNode.Name;
                    //libgrid.GridGroupDisplayNm = gridNode.Text;
                    //_fm.GridGroups.Add(libgrid);

                    //gridProperty.SetPropertyValue(libgrid, gridNode);
                    //_fm.ModuleOrder.Add(new ModuleOrder { moduleType = ModuleType.GridGroup, ID = libgrid.GridGroupID });
                    #endregion
                    break;
                case "addbuttongroup"://添加按钮组
                    if (_fm.BtnGroups == null) _fm.BtnGroups = new LibCollection<LibButtonGroup>();
                    AddNodeBindEntityToCtr<LibButtonGroup, ButtonGroupProperty>(_btngrouplst, _fm.BtnGroups, "BtnGroupID", "BtnGroupName", "BtnGroupDisplayNm", "BtnGroup", curentNode, NodeType.ButtonGroup, ModuleType.ButtonGroup);
                    #region 
                    ////树节点
                    //LibTreeNode btngroupNode = new LibTreeNode();
                    //btngroupNode.NodeId = Guid.NewGuid().ToString();
                    //btngroupNode.NodeType = NodeType.ButtonGroup;
                    //if (_fm.BtnGroups == null) _fm.BtnGroups = new LibCollection<LibButtonGroup>();
                    //btngroupNode.Name = string.Format("BtnGroup{0}", _fm.BtnGroups.Count + 1);
                    //btngroupNode.Text = string.Format("{0}{1}", ReSourceManage.GetResource(NodeType.ButtonGroup), _fm.BtnGroups.Count + 1);
                    //curentNode.Nodes.Add(btngroupNode);

                    ////控件属性
                    //ButtonGroupProperty btnProperty = new ButtonGroupProperty(btngroupNode.NodeId);
                    //btnProperty.Dock = DockStyle.Fill;
                    //_btngrouplst.Add(btnProperty);
                    //this.splitContainer1.Panel2.Controls.Add(btnProperty);

                    ////对应实体
                    //LibButtonGroup libbtngroup = new LibButtonGroup();
                    //libbtngroup.BtnGroupID = btngroupNode.NodeId;
                    //libbtngroup.BtnGroupName = btngroupNode.Name;
                    //libbtngroup.BtnGroupDisplayNm = btngroupNode.Text;
                    //_fm.BtnGroups.Add(libbtngroup);

                    //btnProperty.SetPropertyValue(libbtngroup, btngroupNode);
                    //_fm.ModuleOrder.Add(new ModuleOrder { moduleType = ModuleType.ButtonGroup, ID = libbtngroup.BtnGroupID });
                    #endregion
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
            LibFormGroup currentlibfmgroup = _fm.FormGroups.FindFirst("FormGroupID", curentNode.NodeId);
            switch (e.ClickedItem.Name)
            {
                case "addfmGroupFields"://添加字段
                    #region
                    if (currentlibfmgroup.FmGroupFields == null) currentlibfmgroup.FmGroupFields = new LibCollection<LibFormGroupField>();
                    LibDataSource ds = ModelDesignProject.GetDataSourceById(_fm.DSID);
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
                        curentNode.Nodes.Clear();
                        if (currentlibfmgroup.FmGroupFields != null && currentlibfmgroup.FmGroupFields.Count > 0)
                            currentlibfmgroup.FmGroupFields.RemoveAll();
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
                                    libfgfield.Width = 2;
                                    LibField libfield = ds.DefTables.FindFirst("TableName", deftb.Name).TableStruct.FindFirst("Name", tbstruct.Name).Fields.FindFirst("Name", f.Name);
                                    switch (libfield.FieldType)
                                    {
                                        case LibFieldType.Date:
                                            libfgfield.ElemType = ElementType.Date;
                                            break;
                                        case LibFieldType.DateTime:
                                            libfgfield.ElemType = ElementType.DateTime;
                                            break;
                                        case LibFieldType.Decimal :
                                        case  LibFieldType.Interger:
                                        case LibFieldType.Long:
                                            libfgfield.IsNumber = true;
                                            break;
                                        case LibFieldType.Text:
                                            libfgfield.ElemType = ElementType.Textarea;
                                            break;
                                    }
                                    libfgfield.IsAllowNull = !libfield.AllowNull;
                                    libfgfield.FieldLength = libfield.FieldLength;
                                    //if (libfield.SourceField != null && !string.IsNullOrEmpty (libfield.SourceField .FromDataSource ) 
                                    //    && !string.IsNullOrEmpty (libfield .SourceField .FromStructTableNm ) && !string.IsNullOrEmpty (libfield .SourceField .FromFieldNm ))
                                    if(libfield.SourceField !=null &&libfield.SourceField .Count >0)
                                    {
                                        libfgfield.ElemType = ElementType.Search;
                                    }
                                    if (libfield.Items != null && libfield.Items.Count > 0)
                                    {
                                        libfgfield.ElemType = ElementType.Select;
                                    }
                                    currentlibfmgroup.FmGroupFields.Add(libfgfield);

                                    fgfieldProperty.SetPropertyValue(libfgfield, fieldNode);

                                    #endregion
                                }
                            }
                        }
                        UpdateTabPageText();
                    }
                    #endregion
                    break;
                case "deletefmgroupfield"://删除
                    _fm.FormGroups.Remove(currentlibfmgroup);
                    _fm.ModuleOrder.Remove("ID", currentlibfmgroup.FormGroupID);
                    curentNode.Remove();
                    UpdateTabPageText();
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
            LibGridGroup currentlibgridgroup = _fm.GridGroups.FindFirst("GridGroupID", curentNode.NodeId);
            switch (e.ClickedItem.Name)
            {
                case "addgridField"://添加字段
                    #region
                   
                    if (currentlibgridgroup.GdGroupFields == null) currentlibgridgroup.GdGroupFields = new LibCollection<LibGridGroupField>();
                    LibDataSource ds = ModelDesignProject.GetDataSourceById(_fm.DSID);
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
                                            if (fld.SourceField != null)
                                            {
                                                foreach (LibFromSourceField sfd in fld.SourceField)
                                                {
                                                    if (sfd.RelateFieldNm == null) continue;
                                                    foreach (LibRelateField rfd in sfd.RelateFieldNm)
                                                    {
                                                        _node = new LibTreeNode();
                                                        _node.NodeType = NodeType.Field;
                                                        _node.Name = string.IsNullOrEmpty(rfd.AliasName) ? rfd.FieldNm : rfd.AliasName;
                                                        _node.Text = rfd.DisplayNm;
                                                        _node.Tag = 1;
                                                        dtstructnode.Nodes.Add(_node);
                                                    }
                                                }
                                            }
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
                        if (currentlibgridgroup.GdGroupFields  != null && currentlibgridgroup.GdGroupFields.Count > 0)
                            currentlibgridgroup.GdGroupFields.RemoveAll();
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
                                    libgdfield.IsFromSourceField = (f.Tag!=null &&Convert .ToInt32(f.Tag) == 1);
                                    currentlibgridgroup.GdGroupFields.Add(libgdfield);

                                    gdfieldProperty.SetPropertyValue(libgdfield, fieldNode);

                                    #endregion
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case "deletegridfield"://删除
                    _fm.GridGroups.Remove(currentlibgridgroup);
                    _fm.ModuleOrder.Remove("ID", currentlibgridgroup.GridGroupID);
                    curentNode.Remove();
                    UpdateTabPageText();
                    break;
            }
        }

        /// <summary>按钮组节点上的右键菜单</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip4_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode = (LibTreeNode)this.treeView1.SelectedNode;
            LibButtonGroup currentlibbtngroup = _fm.BtnGroups.FindFirst("BtnGroupID", curentNode.NodeId);
            switch (e.ClickedItem.Name)
            {
                case "addLibButton"://添加按钮
                    if (currentlibbtngroup.LibButtons == null) currentlibbtngroup.LibButtons = new LibCollection<LibButton>();
                    AddNodeBindEntityToCtr<LibButton, ButtonProperty>(_btnlst, currentlibbtngroup.LibButtons, "LibButtonID", "LibButtonName", "LibButtonDisplayNm", "Button", curentNode, NodeType.BtnGroup_button, null);
                    break;
                case "deletelibbutton": //删除
                    _fm.BtnGroups.Remove(currentlibbtngroup);
                    _fm.ModuleOrder.Remove("ID", currentlibbtngroup.BtnGroupID);
                    curentNode.Remove();
                    //UpdateTabPageText();
                    break;

            }
            UpdateTabPageText();
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

        private void AddNodeBindEntityToCtr<T,PropertyT>(List<PropertyT> propertylst, LibCollection<T> libCollection,string entiyid,string entiynm,string entiydisplaytext,string name, LibTreeNode curentNode, NodeType nodeType, ModuleType? moduleType)
            where PropertyT: BaseUserControl<T>
        {
            //树节点
            LibTreeNode Node = new LibTreeNode();
            Node.NodeId = Guid.NewGuid().ToString();
            Node.NodeType = nodeType;
            if (_fm.BtnGroups == null) _fm.BtnGroups = new LibCollection<LibButtonGroup>();
            Node.Name = string.Format("{1}{0}", libCollection.Count + 1,name);
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
            p.SetValue(entity, Node.NodeId,null);
            p= type.GetProperty(entiynm);
            p.SetValue(entity, Node.Name, null);
            p = type.GetProperty(entiydisplaytext);
            p.SetValue(entity, Node.Text, null);
            if (nodeType == NodeType.GridGroup)
            {
                string[] pnm = { "HasAddRowButton", "HasEditRowButton", "HasDeletRowButton" };
                foreach (string nm in pnm)
                {
                    p = type.GetProperty(nm);
                    if (p != null)
                        p.SetValue(entity, true, null);
                }
            }
            libCollection.Add(entity);

            propertyT.SetPropertyValue(entity, Node);
            if (moduleType != null)
                _fm.ModuleOrder.Add(new ModuleOrder { moduleType = moduleType, ID = Node.NodeId });
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            // 定义一个中间变量
            LibTreeNode treeNode;
            //判断拖动的是否为TreeNode类型，不是的话不予处理
            if (e.Data.GetDataPresent("BWYSDP.com.LibTreeNode", false))
            {
                // 拖放的目标节点
                LibTreeNode targetTreeNode;
                // 获取当前光标所处的坐标
                // 定义一个位置点的变量，保存当前光标所处的坐标点
                Point point = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                // 根据坐标点取得处于坐标点位置的节点
                targetTreeNode = (LibTreeNode)((TreeView)sender).GetNodeAt(point);
                // 获取被拖动的节点
                treeNode = (LibTreeNode)e.Data.GetData("BWYSDP.com.LibTreeNode");
                if (targetTreeNode == null) { MessageHandle.ShowMessage("无法拖曳至当前位置", true); return; }
                if (targetTreeNode.Parent != treeNode.Parent)
                {
                    MessageHandle.ShowMessage("只能移动相同父节点下的节点", true);
                    return;
                }
                int index = targetTreeNode.Parent.Nodes.IndexOf(targetTreeNode);
                LibTreeNode newnode = treeNode.Copy();
                targetTreeNode.Parent.Nodes.Insert(index, newnode);
                // 将被拖动的节点移除
                treeNode.Remove();
                if (newnode.NodeType == NodeType.FormGroup_Field)
                {
                    #region
                    LibFormGroup fmgp = _fm.FormGroups.FindFirst("FormGroupID", ((LibTreeNode)newnode.Parent).NodeId);
                    if (fmgp != null)
                    {
                        LibFormGroupField f = fmgp.FmGroupFields.FindFirst("ID", newnode.NodeId);
                        if (f != null)
                        {
                            LibFormGroupField newf = LibSysUtils.Copy(f);
                            if (newf != null)
                            {
                                LibFormGroupField targfield = fmgp.FmGroupFields.FindFirst("ID", targetTreeNode.NodeId);
                                if (targfield != null)
                                {
                                    int i = fmgp.FmGroupFields.IndexOf(targfield);
                                    fmgp.FmGroupFields.Insert(i, newf);
                                    fmgp.FmGroupFields.Remove(f);
                                }

                            }


                        }
                    }
                    #endregion 
                }
                else if (newnode.NodeType == NodeType.GridGroup_Field)
                {
                    #region
                    LibGridGroup gp = _fm.GridGroups.FindFirst("GridGroupID", ((LibTreeNode)newnode.Parent).NodeId);
                    if (gp != null)
                    {
                        LibGridGroupField f = gp.GdGroupFields.FindFirst("ID", newnode.NodeId);
                        if (f != null)
                        {
                            LibGridGroupField newf = LibSysUtils.Copy(f);
                            if (newf != null)
                            {
                                LibGridGroupField targfield = gp.GdGroupFields.FindFirst("ID", targetTreeNode.NodeId);
                                if (targfield != null)
                                {
                                    int i = gp.GdGroupFields.IndexOf(targfield);
                                    gp.GdGroupFields.Insert(i, newf);
                                    gp.GdGroupFields.Remove(f);
                                }

                            }


                        }
                    }
                    #endregion 
                }
                else if (newnode.NodeType == NodeType.FormGroup || newnode.NodeType == NodeType.GridGroup || newnode.NodeType==NodeType.ButtonGroup)
                {
                    ModuleOrder dragobj = _fm.ModuleOrder.FindFirst("ID", newnode.NodeId);
                    ModuleOrder targ= _fm.ModuleOrder.FindFirst("ID", targetTreeNode.NodeId);
                    //LibFormGroup formGroup = _fm.FormGroups.FindFirst("FormGroupID", newnode.NodeId);
                    //LibFormGroup targ = _fm.FormGroups.FindFirst("FormGroupID", targetTreeNode.NodeId);
                    if (dragobj != null && targ!=null)
                    {
                        int i= _fm.ModuleOrder.IndexOf(targ);
                        ModuleOrder module = LibSysUtils.Copy(dragobj);
                        _fm.ModuleOrder.Insert(i, module);
                        _fm.ModuleOrder.Remove(dragobj);

                    }
                }
                UpdateTabPageText();
                //}
            }
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        /// <summary>
        /// 按钮节点上的右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip_btn_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode = (LibTreeNode)this.treeView1.SelectedNode;
            switch (e.ClickedItem.Name)
            {
                case "deltelibbtn": //删除
                    if (curentNode.NodeType == NodeType.BtnGroup_button)
                    {
                        LibButtonGroup currentlibbtngroup = _fm.BtnGroups.FindFirst("BtnGroupID", ((LibTreeNode)curentNode.Parent).NodeId);
                        currentlibbtngroup.LibButtons.Remove("LibButtonID", curentNode.NodeId);
                        curentNode.Remove();
                    }
                    else if (curentNode.NodeType == NodeType.FormGroup_Field)
                    {
                        LibFormGroup currlibfmgp = _fm.FormGroups.FindFirst("FormGroupID", ((LibTreeNode)curentNode.Parent).NodeId);
                        currlibfmgp.FmGroupFields.Remove("ID", curentNode.NodeId);
                    }
                    else if (curentNode.NodeType == NodeType.GridGroup_Field)
                    {
                        LibGridGroup currgdp = _fm.GridGroups.FindFirst("GridGroupID", ((LibTreeNode)curentNode.Parent).NodeId);
                        currgdp.GdGroupFields.Remove("ID", curentNode.NodeId);
                    }
                    //var btnprop= _btnlst.FirstOrDefault(i => i.Name == curentNode.NodeId);
                    //_btnlst.Remove(btnprop);
                    break;
            }
            curentNode.Remove();
            UpdateTabPageText();
        }
    }
}
