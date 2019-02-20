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

namespace BWYSDP.Controls
{
    public partial class FormTemplate : UserControl
    {
        private LibTreeNode _funNode;
        private LibFormPage _fm = null;
        private FormPageProperty _fmProperty = null;
        private List<FormGroupProperty> _formgrouplst = null;
        public FormTemplate(LibTreeNode funcNode)
        {
            this._funNode = funcNode;
            InitializeComponent();
            _fmProperty = new FormPageProperty();
            _fmProperty.Dock = DockStyle.Fill;

            _formgrouplst = new List<FormGroupProperty>();
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
                case NodeType .FormPanel :
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
                case NodeType.FormGroup :
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

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                LibTreeNode libnode = (LibTreeNode)e.Node;
                if (libnode.NodeType == NodeType.FormPanel)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip1;
                }
                //else if (libnode.NodeType == NodeType.DefindTable)
                //{
                //    libnode.ContextMenuStrip = this.contextMenuStrip2;
                //}
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
                    fmgroupNode .NodeId =Guid .NewGuid ().ToString ();
                    fmgroupNode.NodeType = NodeType.FormGroup;
                    if (_fm.FormGroups == null) _fm.FormGroups = new LibCollection<LibFormGroup>();
                    fmgroupNode.Name =string .Format ("FormGroup{0}", _fm.FormGroups.Count + 1);
                    fmgroupNode.Text = string.Format("{0}{1}", ReSourceManage.GetResource(NodeType.FormGroup), _fm.FormGroups.Count + 1);
                    curentNode.Nodes.Add(fmgroupNode);

                    //控件属性
                    FormGroupProperty fgProperty = new FormGroupProperty(fmgroupNode.NodeId);
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
    }
}
