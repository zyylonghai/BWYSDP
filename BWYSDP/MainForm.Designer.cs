﻿namespace BWYSDP
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CreatClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateFuncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addfuncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtClassNm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDSPackage = new System.Windows.Forms.TextBox();
            this.txtFuncId = new System.Windows.Forms.TextBox();
            this.FuncName = new System.Windows.Forms.Label();
            this.txtbNodeType = new System.Windows.Forms.TextBox();
            this.txtFuncNm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FuncNm = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.libTabControl1 = new BWYSDP.Controls.LibTabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SaveBtn = new System.Windows.Forms.ToolStripButton();
            this.SaveAllBtn = new System.Windows.Forms.ToolStripButton();
            this.CreateTableStructBtn = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileupload_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ServerConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.多语言配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dal程序集加载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreatClassToolStripMenuItem,
            this.CreateFuncToolStripMenuItem,
            this.addfuncToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.RefreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 114);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // CreatClassToolStripMenuItem
            // 
            this.CreatClassToolStripMenuItem.Name = "CreatClassToolStripMenuItem";
            this.CreatClassToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CreatClassToolStripMenuItem.Text = "新建分类";
            // 
            // CreateFuncToolStripMenuItem
            // 
            this.CreateFuncToolStripMenuItem.Name = "CreateFuncToolStripMenuItem";
            this.CreateFuncToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CreateFuncToolStripMenuItem.Text = "新建功能";
            // 
            // addfuncToolStripMenuItem
            // 
            this.addfuncToolStripMenuItem.Name = "addfuncToolStripMenuItem";
            this.addfuncToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.addfuncToolStripMenuItem.Text = "添加功能";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deleteToolStripMenuItem.Text = "删除";
            // 
            // RefreshToolStripMenuItem
            // 
            this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.RefreshToolStripMenuItem.Text = "刷新";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 65);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1MinSize = 285;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2MinSize = 500;
            this.splitContainer1.Size = new System.Drawing.Size(960, 508);
            this.splitContainer1.SplitterDistance = 285;
            this.splitContainer1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(285, 508);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.treeView1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(285, 291);
            this.panel4.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(285, 291);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtClassNm);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtDSPackage);
            this.panel3.Controls.Add(this.txtFuncId);
            this.panel3.Controls.Add(this.FuncName);
            this.panel3.Controls.Add(this.txtbNodeType);
            this.panel3.Controls.Add(this.txtFuncNm);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.FuncNm);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 291);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(285, 217);
            this.panel3.TabIndex = 0;
            // 
            // txtClassNm
            // 
            this.txtClassNm.Location = new System.Drawing.Point(110, 19);
            this.txtClassNm.Name = "txtClassNm";
            this.txtClassNm.Size = new System.Drawing.Size(142, 21);
            this.txtClassNm.TabIndex = 22;
            this.txtClassNm.TextChanged += new System.EventHandler(this.txtClassNm_TextChanged);
            this.txtClassNm.Leave += new System.EventHandler(this.txtClassNm_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "分类名称";
            // 
            // txtDSPackage
            // 
            this.txtDSPackage.Location = new System.Drawing.Point(110, 146);
            this.txtDSPackage.Name = "txtDSPackage";
            this.txtDSPackage.ReadOnly = true;
            this.txtDSPackage.Size = new System.Drawing.Size(142, 21);
            this.txtDSPackage.TabIndex = 19;
            // 
            // txtFuncId
            // 
            this.txtFuncId.Location = new System.Drawing.Point(110, 48);
            this.txtFuncId.Name = "txtFuncId";
            this.txtFuncId.ReadOnly = true;
            this.txtFuncId.Size = new System.Drawing.Size(142, 21);
            this.txtFuncId.TabIndex = 18;
            // 
            // FuncName
            // 
            this.FuncName.AutoSize = true;
            this.FuncName.Location = new System.Drawing.Point(30, 51);
            this.FuncName.Name = "FuncName";
            this.FuncName.Size = new System.Drawing.Size(41, 12);
            this.FuncName.TabIndex = 17;
            this.FuncName.Text = "功能ID";
            // 
            // txtbNodeType
            // 
            this.txtbNodeType.Location = new System.Drawing.Point(110, 111);
            this.txtbNodeType.Name = "txtbNodeType";
            this.txtbNodeType.ReadOnly = true;
            this.txtbNodeType.Size = new System.Drawing.Size(142, 21);
            this.txtbNodeType.TabIndex = 16;
            // 
            // txtFuncNm
            // 
            this.txtFuncNm.Location = new System.Drawing.Point(110, 75);
            this.txtFuncNm.Name = "txtFuncNm";
            this.txtFuncNm.ReadOnly = true;
            this.txtFuncNm.Size = new System.Drawing.Size(142, 21);
            this.txtFuncNm.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "所 属 包：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "功能类型";
            // 
            // FuncNm
            // 
            this.FuncNm.AutoSize = true;
            this.FuncNm.Location = new System.Drawing.Point(30, 78);
            this.FuncNm.Name = "FuncNm";
            this.FuncNm.Size = new System.Drawing.Size(53, 12);
            this.FuncNm.TabIndex = 15;
            this.FuncNm.Text = "功能名称";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(671, 508);
            this.panel2.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.libTabControl1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(671, 508);
            this.panel5.TabIndex = 1;
            // 
            // libTabControl1
            // 
            this.libTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.libTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.libTabControl1.ItemSize = new System.Drawing.Size(250, 25);
            this.libTabControl1.Location = new System.Drawing.Point(0, 0);
            this.libTabControl1.Name = "libTabControl1";
            this.libTabControl1.Padding = new System.Drawing.Point(22, 11);
            this.libTabControl1.SelectedIndex = 0;
            this.libTabControl1.Size = new System.Drawing.Size(671, 508);
            this.libTabControl1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveBtn,
            this.SaveAllBtn,
            this.CreateTableStructBtn,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(960, 40);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SaveBtn
            // 
            this.SaveBtn.Image = ((System.Drawing.Image)(resources.GetObject("SaveBtn.Image")));
            this.SaveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(36, 37);
            this.SaveBtn.Text = "保存";
            this.SaveBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // SaveAllBtn
            // 
            this.SaveAllBtn.Image = ((System.Drawing.Image)(resources.GetObject("SaveAllBtn.Image")));
            this.SaveAllBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveAllBtn.Name = "SaveAllBtn";
            this.SaveAllBtn.Size = new System.Drawing.Size(60, 37);
            this.SaveAllBtn.Text = "全部保存";
            this.SaveAllBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // CreateTableStructBtn
            // 
            this.CreateTableStructBtn.Image = ((System.Drawing.Image)(resources.GetObject("CreateTableStructBtn.Image")));
            this.CreateTableStructBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateTableStructBtn.Name = "CreateTableStructBtn";
            this.CreateTableStructBtn.Size = new System.Drawing.Size(72, 37);
            this.CreateTableStructBtn.Text = "创建表结构";
            this.CreateTableStructBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.CreateTableStructBtn.Click += new System.EventHandler(this.CreateTableStructBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(960, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileupload_ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // fileupload_ToolStripMenuItem
            // 
            this.fileupload_ToolStripMenuItem.Name = "fileupload_ToolStripMenuItem";
            this.fileupload_ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.fileupload_ToolStripMenuItem.Text = "模型文件上传";
            this.fileupload_ToolStripMenuItem.Click += new System.EventHandler(this.fileupload_ToolStripMenuItem_Click);
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ServerConfigToolStripMenuItem,
            this.多语言配置ToolStripMenuItem,
            this.dal程序集加载ToolStripMenuItem});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // ServerConfigToolStripMenuItem
            // 
            this.ServerConfigToolStripMenuItem.Name = "ServerConfigToolStripMenuItem";
            this.ServerConfigToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ServerConfigToolStripMenuItem.Text = "服务配置";
            this.ServerConfigToolStripMenuItem.Click += new System.EventHandler(this.ServerConfigToolStripMenuItem_Click);
            // 
            // 多语言配置ToolStripMenuItem
            // 
            this.多语言配置ToolStripMenuItem.Name = "多语言配置ToolStripMenuItem";
            this.多语言配置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.多语言配置ToolStripMenuItem.Text = "多语言配置";
            this.多语言配置ToolStripMenuItem.Click += new System.EventHandler(this.多语言配置ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 573);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(960, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // dal程序集加载ToolStripMenuItem
            // 
            this.dal程序集加载ToolStripMenuItem.Name = "dal程序集加载ToolStripMenuItem";
            this.dal程序集加载ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dal程序集加载ToolStripMenuItem.Text = "Dal程序集加载";
            this.dal程序集加载ToolStripMenuItem.Click += new System.EventHandler(this.dal程序集加载ToolStripMenuItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 37);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 595);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SaveBtn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtDSPackage;
        private System.Windows.Forms.TextBox txtFuncId;
        private System.Windows.Forms.Label FuncName;
        private System.Windows.Forms.TextBox txtFuncNm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label FuncNm;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CreatClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateFuncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
        private System.Windows.Forms.TextBox txtClassNm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem addfuncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.TextBox txtbNodeType;
        private System.Windows.Forms.Label label1;
        private Controls.LibTabControl libTabControl1;
        private System.Windows.Forms.ToolStripButton CreateTableStructBtn;
        private System.Windows.Forms.ToolStripButton SaveAllBtn;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem ServerConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 多语言配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileupload_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dal程序集加载ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}