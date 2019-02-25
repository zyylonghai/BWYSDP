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
        public FormTemplate(LibTreeNode funcNode)
        {
            this._funNode = funcNode;
            InitializeComponent();
            _fmProperty = new FormPageProperty();
            _fmProperty.Dock = DockStyle.Fill;

            _formgrouplst = new List<FormGroupProperty>();
            _formgroupfieldlst = new List<FormGroupFieldProperty>();
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

            if (_fm != null)
            {
                 
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
            foreach (FormGroupProperty fg in _formgrouplst)
            {
                fg.GetControlsValue();
            }
            foreach (FormGroupFieldProperty fgfield in _formgroupfieldlst)
            {
                fgfield.GetControlsValue();
            }
            //foreach (DefFieldProperty field in _fieldPropertylst)
            //{
            //    field.GetControlsValue();
            //}
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
                //else if (libnode.NodeType == NodeType.TableStruct)
                //{
                //    libnode.ContextMenuStrip = this.contextMenuStrip3;
                //}
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
                    _fm.FormGroups.Add(libfg);

                    fgProperty.SetPropertyValue(libfg, fmgroupNode);
                    break;
                case "addGridGroup"://添加表格组


                    break;
            }
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
                           deftbnode.Text = string.Format("{0}({1})", deftb.DisplayName,deftb.TableName);
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
                                    libfgfield.Name = f.Name;
                                    libfgfield.FromTableNm = tbstruct.Name;
                                    libfgfield.FromDefTableNm = deftb.Name;
                                    libfgfield.DisplayName = f.Text;
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
    }
}
