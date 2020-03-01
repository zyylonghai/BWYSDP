using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWYSDP.com;
using SDPCRL.CORE;
using BWYSDP.Controls;

namespace BWYSDP
{
    public partial class MainForm : LibFormBase
    {
        public static int index = 1;
        public MainForm()
        {
            InitializeComponent();
            ModelDesignProject.InitialModelTree(this.treeView1);
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            ModelDesignProject.GetChildNode((LibTreeNode)e.Node);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                LibTreeNode libnode = (LibTreeNode)e.Node;
                if (libnode.NodeType == NodeType.Class)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip1;
                }
                this.treeView1.SelectedNode = libnode;
            }
            else
            {

            }
        }


        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            LibTreeNode node = (LibTreeNode)e.Node;
            if (node.NodeType != NodeType.Class && node.NodeType !=NodeType.Func)
            {
                #region  创建Tabpage
                string tabNm = string.Format("{0}{2}{1}", node.Name, node.NodeType.ToString(), SysConstManage.Underline);
                if (this.libTabControl1.TabPages.ContainsKey(tabNm))
                {
                    this.libTabControl1.SelectedTab = this.libTabControl1.TabPages[tabNm];
                    return;
                }
                TabPage page = new TabPage(string.Format("{0}({1})", node.Text, node.NodeType.ToString()));
                page.Name = tabNm;
                
                #endregion

                this.libTabControl1.TabPages.Add(page);
                this.libTabControl1.SelectedTab = page;
                switch (node.NodeType)
                {
                    case NodeType.DataModel:
                        DataSourceControl dsControl = new DataSourceControl(node);
                        dsControl.Dock = DockStyle.Fill;
                        page.Controls.Add(dsControl);
                        break;
                    case NodeType.FormModel:
                        FormTemplate fmControl = new FormTemplate(node);
                        fmControl.Dock = DockStyle.Fill;
                        page.Controls.Add(fmControl);
                        break;
                    case NodeType.PermissionModel:
                        PermissionProperty permissionctrl = new PermissionProperty(node.Name);
                        permissionctrl.Dock = DockStyle.Fill;
                        page.Controls.Add(permissionctrl);
                        SDPCRL.COM.ModelManager.LibPermissionSource libpermission = ModelDesignProject.GetLibPermissionById(node.Name);
                        libpermission.Package = node.Package;
                        permissionctrl.SetPropertyValue(libpermission, node);
                        break;
                    case NodeType.KeyValues:
                        KeyValuesControl keyvaluectrl = new KeyValuesControl(node);
                        keyvaluectrl.Dock = DockStyle.Fill;
                        page.Controls.Add(keyvaluectrl);
                        break;
                }
            }
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LibTreeNode libnode = (LibTreeNode)e.Node;
            if (libnode == null) return;
            if (libnode.NodeType == NodeType.Class)
            {
                this.txtClassNm.Text = libnode.Text;
                this.txtFuncId.Text = string.Empty;
                this.txtFuncNm.Text = string.Empty;
                this.txtDSPackage.Text = string.Empty;
                this.txtbNodeType.Text = string.Empty;
            }
            else
            {
                this.txtClassNm.Text = string.Empty;
                this.txtFuncId.Text = libnode.Name;
                this.txtFuncNm.Text = libnode.Text;
                this.txtDSPackage.Text = libnode.Package;
                this.txtbNodeType.Text = ReSourceManage.GetResource(libnode.NodeType);
            }
        }
        /// <summary>
        /// 分类名称 文本框 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtClassNm_TextChanged(object sender, EventArgs e)
        {
            LibTreeNode libNode = (LibTreeNode)this.treeView1.SelectedNode;
            if (libNode != null)
            {
                if (libNode.NodeType == NodeType.Class)
                {
                    libNode.Text = this.txtClassNm.Text;
                    libNode.Name = this.txtClassNm.Text;

                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void txtClassNm_Leave(object sender, EventArgs e)
        {
            LibTreeNode libNode = (LibTreeNode)this.treeView1.SelectedNode;
            if (libNode != null)
            {
                if (libNode.NodeType == NodeType.Class)
                {
                    ModelDesignProject.UpdateXmlNode(libNode);
                }
            }
        }
        /// <summary>
        /// 快捷菜单contextMenuStrip1 项点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode currentnode = (LibTreeNode)this.treeView1.SelectedNode;
            switch (e.ClickedItem.Name)
            {
                case "CreatClassToolStripMenuItem": //新建分类
                    if (currentnode != null)
                    {
                        LibTreeNode node = new LibTreeNode(string.Format("新建分类{0}", index++));
                        node.NodeType = NodeType.Class;
                        node.Name = node.Text;
                        node.OriginalName = node.Text;
                        this.treeView1.SelectedNode.Nodes.Add(node);
                        this.treeView1.SelectedNode = node;

                        ModelDesignProject.AddXmlNode(node);
                    }
                    else
                    {
                        MessageHandle.ShowMessage("未选中节点", true); 
                    }
                    break;
                case "CreateFuncToolStripMenuItem": //新建功能
                    //WakeUpForm<DSAdd>("DSAdd", 1, 2);
                    WakeUpForm<CreateFunc>("CreateFunc",2,1);
                    break;
                case "RefreshToolStripMenuItem"://刷新
                    ModelDesignProject.GetChildNode(currentnode);
                    this.treeView1.Refresh();
                    break;
                case "addfuncToolStripMenuItem"://添加功能
                    break;
                case "deleteToolStripMenuItem"://删除节点
                    if (currentnode != null)
                    {
                        ModelDesignProject.DeleteXmlNode(currentnode);
                        currentnode.Remove();
                    }
                    else
                    {
                        MessageHandle.ShowMessage("未选要删除的节点", true);
                    }
                    break;
            }
        }

        public override void DoFormAcceptMsg(string tag, Dictionary<object, object> agrs)
        {
            base.DoFormAcceptMsg(tag, agrs);
            if (string.Compare(tag, "NewFunc") == 0)//新建功能
            {
                if (agrs != null && agrs.Count() > 0)
                {
                    LibTreeNode funcNode = agrs["funcNode"] as LibTreeNode;
                    switch (funcNode.NodeType)
                    {
                        case NodeType.Func:
                            #region 
                            //数据源节点
                            LibTreeNode ds = new LibTreeNode();
                            funcNode.CopyTo(ds);
                            ds.NodeType = NodeType.DataModel;
                            ModelDesignProject.CreatModelFile(ds);

                            funcNode.Nodes.Add(ds);
                            //排版模型节点
                            LibTreeNode form = new LibTreeNode();
                            funcNode.CopyTo(form);
                            form.NodeType = NodeType.FormModel;
                            ModelDesignProject.CreatModelFile(form);
                            funcNode.Nodes.Add(form);
                            //权限模型节点
                            LibTreeNode permission = new LibTreeNode();
                            funcNode.CopyTo(permission);
                            permission.NodeType = NodeType.PermissionModel;
                            ModelDesignProject.CreatModelFile(permission);
                            funcNode.Nodes.Add(permission);

                            this.treeView1.SelectedNode.Nodes.Add(funcNode);
                            ModelDesignProject.AddXmlNode(funcNode);
                            this.treeView1.SelectedNode = funcNode;
                            #endregion
                            break;
                        case NodeType.SpectFunc:
                            break;
                        case NodeType.ReportFunc:
                            break;
                        case NodeType.DataModel:
                            break;
                        case NodeType.FormModel:
                            break;
                        case NodeType.PermissionModel:
                            break;
                        case NodeType.KeyValues:
                            ModelDesignProject.CreatModelFile(funcNode);
                            this.treeView1.SelectedNode.Nodes.Add(funcNode);
                            ModelDesignProject.AddXmlNode(funcNode);
                            this.treeView1.SelectedNode = funcNode;
                            break;
                    }
                }
            }
        }

        /// <summary>保存</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            TabPage page=this.libTabControl1.SelectedTab;
            if (page.Text.Contains(SysConstManage .Asterisk))
            {
                string[] nameAndtype = page.Name.Split(SysConstManage.Underline);
                NodeType ntype = LibSysUtils.ConvertToEnumType<NodeType>(nameAndtype[1]);
                switch (ntype)
                {
                    case NodeType.DataModel:
                        ((DataSourceControl)page.Controls[0]).GetControlValueBindToDS();
                        break;
                    case NodeType.FormModel:
                        ((FormTemplate)page.Controls[0]).GetControlValueBindToFM();
                        break;
                    case NodeType.PermissionModel:
                        ((PermissionProperty)page.Controls[0]).GetControlsValue();
                        break;
                    case NodeType.KeyValues:
                        ((KeyValuesControl)page.Controls[0]).GetControlValueBindToKeyValue();
                        break;
                }
                ModelDesignProject.SaveModel(nameAndtype[0], ntype);
                page.Text = page.Text.Replace(SysConstManage.Asterisk.ToString (),"");
            }
        }
        //服务配置
        private void ServerConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WakeUpForm<ServerConfig>("SetServer");
        }
        /// <summary> 创建表结构</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateTableStructBtn_Click(object sender, EventArgs e)
        {
            TabPage page = this.libTabControl1.SelectedTab;
            if (page.Text.Contains(SysConstManage.Asterisk))
            {
                MessageHandle.ShowMessage("模型有修改未保存", true);
                return;
            }
            string[] nameAndtype = page.Name.Split(SysConstManage.Underline);
            NodeType ntype = LibSysUtils.ConvertToEnumType<NodeType>(nameAndtype[1]);
            switch (ntype)
            {
                case NodeType.DataModel:
                    ((DataSourceControl)page.Controls[0]).CreateTableStructToDB();
                    break;
            }

        }

        private void 多语言配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SDPCRL.COM.ModelManager.LibDataSource ds = null;
            SDPCRL.COM.ModelManager.FormTemplate .LibFormPage fm = null;
            SDPCRL.COM.ModelManager.LibKeyValueCollection keyvaluecollection = null;
            TabPage page = this.libTabControl1.SelectedTab;
            if (page != null)
            {
                string[] nameAndtype = page.Name.Split(SysConstManage.Underline);
                NodeType ntype = LibSysUtils.ConvertToEnumType<NodeType>(nameAndtype[1]);
                switch (ntype)
                {
                    case NodeType.DataModel:
                        ds = ModelDesignProject.GetDataSourceById(nameAndtype[0]);
                        break;
                    case NodeType.FormModel:
                         fm = ModelDesignProject.GetFormSourceByFormId(nameAndtype[0]);
                        if (fm != null && !string.IsNullOrEmpty(fm.DSID))
                        {

                            ds = ModelDesignProject.GetDataSourceById(fm.DSID);
                        }
                        break;
                    case NodeType.KeyValues:
                        keyvaluecollection = ModelDesignProject.GetKeyvaluesByid(nameAndtype[0]);
                        break;
                }
            }
            WakeUpForm<LanguageConfig>("language", ds, fm, keyvaluecollection);
        }

        /// <summary>
        /// 模型文件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileupload_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WakeUpForm<FileUpload>("ftpupload");
        }

    }
}
