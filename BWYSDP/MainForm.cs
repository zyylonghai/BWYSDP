using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWYSDP.com;

namespace BWYSDP
{
    public partial class MainForm : Form
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
                    libnode.ContextMenuStrip = this.contextMenuStrip1;
            }
            else
            {
 
            }
        }
        /// <summary>
        /// 快捷菜单 新建分类 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LibTreeNode node = new LibTreeNode(string.Format("新建分类{0}",index++));
            node.NodeType = NodeType.Class;
            node.Name = node.Text;
            node.OriginalName = node.Text;
            this.treeView1.SelectedNode.Nodes.Add(node);
            this.treeView1.SelectedNode = node;

            ModelDesignProject.AddXmlNode(node);
           
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
            }
            else
            {
                this.txtClassNm.Text = string.Empty;
                this.txtFuncId.Text = libnode.Name;
                this.txtFuncNm.Text = libnode.Text;
                this.txtDSPackage.Text = libnode.Package;
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
    }
}
