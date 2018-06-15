﻿using System;
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
    }
}
