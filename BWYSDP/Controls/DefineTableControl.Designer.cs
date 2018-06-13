namespace BWYSDP
{
    partial class DefineTableControl
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("自定义数据表集");
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contextMenuStrip_defTB = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_TBStruct = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_fieldAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_fieldDelet = new System.Windows.Forms.ToolStripMenuItem();
            this.defFieldProperty1 = new BWYSDP.Controls.DefFieldProperty();
            this.tbStructProperty1 = new BWYSDP.Controls.TBStructProperty();
            this.defTBProperty1 = new BWYSDP.Controls.DefTBProperty();
            this.contextMenuStrip_field = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip_defTB.SuspendLayout();
            this.contextMenuStrip_TBStruct.SuspendLayout();
            this.contextMenuStrip_field.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 382);
            this.panel1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "defineTableCollection";
            treeNode1.Text = "自定义数据表集";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.Size = new System.Drawing.Size(200, 382);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.defFieldProperty1);
            this.panel2.Controls.Add(this.tbStructProperty1);
            this.panel2.Controls.Add(this.defTBProperty1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(200, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(453, 382);
            this.panel2.TabIndex = 1;
            // 
            // contextMenuStrip_defTB
            // 
            this.contextMenuStrip_defTB.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Add});
            this.contextMenuStrip_defTB.Name = "contextMenuStrip_defTB";
            this.contextMenuStrip_defTB.Size = new System.Drawing.Size(137, 26);
            // 
            // ToolStripMenuItem_Add
            // 
            this.ToolStripMenuItem_Add.Name = "ToolStripMenuItem_Add";
            this.ToolStripMenuItem_Add.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItem_Add.Text = "新建数据表";
            // 
            // contextMenuStrip_TBStruct
            // 
            this.contextMenuStrip_TBStruct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_fieldAdd,
            this.ToolStripMenuItem_fieldDelet});
            this.contextMenuStrip_TBStruct.Name = "contextMenuStrip_TBStruct";
            this.contextMenuStrip_TBStruct.Size = new System.Drawing.Size(125, 48);
            // 
            // ToolStripMenuItem_fieldAdd
            // 
            this.ToolStripMenuItem_fieldAdd.Name = "ToolStripMenuItem_fieldAdd";
            this.ToolStripMenuItem_fieldAdd.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_fieldAdd.Text = "新建字段";
            this.ToolStripMenuItem_fieldAdd.Click += new System.EventHandler(this.ToolStripMenuItem_fieldAdd_Click);
            // 
            // ToolStripMenuItem_fieldDelet
            // 
            this.ToolStripMenuItem_fieldDelet.Name = "ToolStripMenuItem_fieldDelet";
            this.ToolStripMenuItem_fieldDelet.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_fieldDelet.Text = "删除";
            // 
            // defFieldProperty1
            // 
            this.defFieldProperty1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defFieldProperty1.Location = new System.Drawing.Point(0, 0);
            this.defFieldProperty1.Name = "defFieldProperty1";
            this.defFieldProperty1.Size = new System.Drawing.Size(453, 382);
            this.defFieldProperty1.TabIndex = 2;
            // 
            // tbStructProperty1
            // 
            this.tbStructProperty1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbStructProperty1.Location = new System.Drawing.Point(0, 0);
            this.tbStructProperty1.Name = "tbStructProperty1";
            this.tbStructProperty1.Size = new System.Drawing.Size(453, 382);
            this.tbStructProperty1.TabIndex = 1;
            // 
            // defTBProperty1
            // 
            this.defTBProperty1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defTBProperty1.Location = new System.Drawing.Point(0, 0);
            this.defTBProperty1.Name = "defTBProperty1";
            this.defTBProperty1.Size = new System.Drawing.Size(453, 382);
            this.defTBProperty1.TabIndex = 0;
            // 
            // contextMenuStrip_field
            // 
            this.contextMenuStrip_field.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip_field.Name = "contextMenuStrip_field";
            this.contextMenuStrip_field.Size = new System.Drawing.Size(101, 26);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // DefineTableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DefineTableControl";
            this.Size = new System.Drawing.Size(653, 382);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip_defTB.ResumeLayout(false);
            this.contextMenuStrip_TBStruct.ResumeLayout(false);
            this.contextMenuStrip_field.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel2;
        private Controls.TBStructProperty tbStructProperty1;
        private Controls.DefTBProperty defTBProperty1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_defTB;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Add;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_TBStruct;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_fieldAdd;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_fieldDelet;
        private Controls.DefFieldProperty defFieldProperty1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_field;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;

    }
}
