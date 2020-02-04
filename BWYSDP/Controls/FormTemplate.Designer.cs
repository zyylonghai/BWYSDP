namespace BWYSDP.Controls
{
    partial class FormTemplate
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFormGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.addGridGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.addbuttongroup = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addfmGroupFields = new System.Windows.Forms.ToolStripMenuItem();
            this.deletefmgroupfield = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addgridField = new System.Windows.Forms.ToolStripMenuItem();
            this.deletegridfield = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addLibButton = new System.Windows.Forms.ToolStripMenuItem();
            this.deletelibbutton = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_btn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deltelibbtn = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.contextMenuStrip4.SuspendLayout();
            this.contextMenuStrip_btn.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(332, 504);
            this.treeView1.TabIndex = 0;
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(999, 504);
            this.splitContainer1.SplitterDistance = 332;
            this.splitContainer1.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFormGroup,
            this.addGridGroup,
            this.addbuttongroup});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 70);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // addFormGroup
            // 
            this.addFormGroup.Name = "addFormGroup";
            this.addFormGroup.Size = new System.Drawing.Size(136, 22);
            this.addFormGroup.Text = "添加信息组";
            // 
            // addGridGroup
            // 
            this.addGridGroup.Name = "addGridGroup";
            this.addGridGroup.Size = new System.Drawing.Size(136, 22);
            this.addGridGroup.Text = "添加表格组";
            // 
            // addbuttongroup
            // 
            this.addbuttongroup.Name = "addbuttongroup";
            this.addbuttongroup.Size = new System.Drawing.Size(136, 22);
            this.addbuttongroup.Text = "添加按钮组";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addfmGroupFields,
            this.deletefmgroupfield});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 48);
            this.contextMenuStrip2.Text = "信息组右键菜单";
            this.contextMenuStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip2_ItemClicked);
            // 
            // addfmGroupFields
            // 
            this.addfmGroupFields.Name = "addfmGroupFields";
            this.addfmGroupFields.Size = new System.Drawing.Size(124, 22);
            this.addfmGroupFields.Text = "添加字段";
            // 
            // deletefmgroupfield
            // 
            this.deletefmgroupfield.Name = "deletefmgroupfield";
            this.deletefmgroupfield.Size = new System.Drawing.Size(124, 22);
            this.deletefmgroupfield.Text = "删除";
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addgridField,
            this.deletegridfield});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(125, 48);
            this.contextMenuStrip3.Text = "表格组右键菜单";
            this.contextMenuStrip3.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip3_ItemClicked);
            // 
            // addgridField
            // 
            this.addgridField.Name = "addgridField";
            this.addgridField.Size = new System.Drawing.Size(124, 22);
            this.addgridField.Text = "添加字段";
            // 
            // deletegridfield
            // 
            this.deletegridfield.Name = "deletegridfield";
            this.deletegridfield.Size = new System.Drawing.Size(124, 22);
            this.deletegridfield.Text = "删除";
            // 
            // contextMenuStrip4
            // 
            this.contextMenuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLibButton,
            this.deletelibbutton});
            this.contextMenuStrip4.Name = "contextMenuStrip4";
            this.contextMenuStrip4.Size = new System.Drawing.Size(125, 48);
            this.contextMenuStrip4.Text = "按钮组右键菜单";
            this.contextMenuStrip4.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip4_ItemClicked);
            // 
            // addLibButton
            // 
            this.addLibButton.Name = "addLibButton";
            this.addLibButton.Size = new System.Drawing.Size(124, 22);
            this.addLibButton.Text = "添加按钮";
            // 
            // deletelibbutton
            // 
            this.deletelibbutton.Name = "deletelibbutton";
            this.deletelibbutton.Size = new System.Drawing.Size(124, 22);
            this.deletelibbutton.Text = "删除";
            // 
            // contextMenuStrip_btn
            // 
            this.contextMenuStrip_btn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deltelibbtn});
            this.contextMenuStrip_btn.Name = "contextMenuStrip_btn";
            this.contextMenuStrip_btn.Size = new System.Drawing.Size(181, 48);
            this.contextMenuStrip_btn.Text = "按钮节点右键菜单";
            this.contextMenuStrip_btn.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_btn_ItemClicked);
            // 
            // deltelibbtn
            // 
            this.deltelibbtn.Name = "deltelibbtn";
            this.deltelibbtn.Size = new System.Drawing.Size(180, 22);
            this.deltelibbtn.Text = "删除";
            // 
            // FormTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormTemplate";
            this.Size = new System.Drawing.Size(999, 504);
            this.Load += new System.EventHandler(this.FormTemplate_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.contextMenuStrip4.ResumeLayout(false);
            this.contextMenuStrip_btn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addFormGroup;
        private System.Windows.Forms.ToolStripMenuItem addGridGroup;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem addfmGroupFields;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem addgridField;
        private System.Windows.Forms.ToolStripMenuItem deletefmgroupfield;
        private System.Windows.Forms.ToolStripMenuItem deletegridfield;
        private System.Windows.Forms.ToolStripMenuItem addbuttongroup;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;
        private System.Windows.Forms.ToolStripMenuItem addLibButton;
        private System.Windows.Forms.ToolStripMenuItem deletelibbutton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_btn;
        private System.Windows.Forms.ToolStripMenuItem deltelibbtn;
    }
}
