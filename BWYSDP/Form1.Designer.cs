namespace BWYSDP
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("数据源集");
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newDataSource = new System.Windows.Forms.ToolStripMenuItem();
            this.addDataSource = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolbtSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolbtCreateTableObj = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableStructTree = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDSPackage = new System.Windows.Forms.TextBox();
            this.txtDSdisplaytext = new System.Windows.Forms.TextBox();
            this.txtDSId = new System.Windows.Forms.TextBox();
            this.DataSourceId = new System.Windows.Forms.Label();
            this.txtDSName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataSourceName = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据源ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDataSource,
            this.addDataSource});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // newDataSource
            // 
            this.newDataSource.Name = "newDataSource";
            this.newDataSource.Size = new System.Drawing.Size(136, 22);
            this.newDataSource.Text = "新建数据源";
            // 
            // addDataSource
            // 
            this.addDataSource.Name = "addDataSource";
            this.addDataSource.Size = new System.Drawing.Size(136, 22);
            this.addDataSource.Text = "添加数据源";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(222, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(617, 384);
            this.panel2.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(200, 18);
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(617, 359);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbtSave,
            this.toolStripButton1,
            this.toolbtCreateTableObj});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(617, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolbtSave
            // 
            this.toolbtSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolbtSave.Image = ((System.Drawing.Image)(resources.GetObject("toolbtSave.Image")));
            this.toolbtSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtSave.Name = "toolbtSave";
            this.toolbtSave.Size = new System.Drawing.Size(36, 22);
            this.toolbtSave.Text = "保存";
            this.toolbtSave.Click += new System.EventHandler(this.toolbtSave_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton1.Text = "放弃";
            // 
            // toolbtCreateTableObj
            // 
            this.toolbtCreateTableObj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolbtCreateTableObj.Image = ((System.Drawing.Image)(resources.GetObject("toolbtCreateTableObj.Image")));
            this.toolbtCreateTableObj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtCreateTableObj.Name = "toolbtCreateTableObj";
            this.toolbtCreateTableObj.Size = new System.Drawing.Size(72, 22);
            this.toolbtCreateTableObj.Text = "创建表对象";
            this.toolbtCreateTableObj.Click += new System.EventHandler(this.toolbtCreateTableObj_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableStructTree);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 384);
            this.panel1.TabIndex = 5;
            // 
            // tableStructTree
            // 
            this.tableStructTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableStructTree.Location = new System.Drawing.Point(0, 0);
            this.tableStructTree.Name = "tableStructTree";
            treeNode2.ContextMenuStrip = this.contextMenuStrip1;
            treeNode2.Name = "dataSourcetree";
            treeNode2.Text = "数据源集";
            this.tableStructTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tableStructTree.Size = new System.Drawing.Size(222, 230);
            this.tableStructTree.TabIndex = 5;
            this.tableStructTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tableStructTree_NodeMouseClick);
            this.tableStructTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tableStructTree_NodeMouseDoubleClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtDSPackage);
            this.panel3.Controls.Add(this.txtDSdisplaytext);
            this.panel3.Controls.Add(this.txtDSId);
            this.panel3.Controls.Add(this.DataSourceId);
            this.panel3.Controls.Add(this.txtDSName);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.dataSourceName);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 230);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(222, 154);
            this.panel3.TabIndex = 0;
            // 
            // txtDSPackage
            // 
            this.txtDSPackage.Location = new System.Drawing.Point(81, 114);
            this.txtDSPackage.Name = "txtDSPackage";
            this.txtDSPackage.ReadOnly = true;
            this.txtDSPackage.Size = new System.Drawing.Size(100, 21);
            this.txtDSPackage.TabIndex = 4;
            // 
            // txtDSdisplaytext
            // 
            this.txtDSdisplaytext.Location = new System.Drawing.Point(79, 86);
            this.txtDSdisplaytext.Name = "txtDSdisplaytext";
            this.txtDSdisplaytext.ReadOnly = true;
            this.txtDSdisplaytext.Size = new System.Drawing.Size(100, 21);
            this.txtDSdisplaytext.TabIndex = 4;
            // 
            // txtDSId
            // 
            this.txtDSId.Location = new System.Drawing.Point(79, 19);
            this.txtDSId.Name = "txtDSId";
            this.txtDSId.ReadOnly = true;
            this.txtDSId.Size = new System.Drawing.Size(100, 21);
            this.txtDSId.TabIndex = 3;
            // 
            // DataSourceId
            // 
            this.DataSourceId.AutoSize = true;
            this.DataSourceId.Location = new System.Drawing.Point(12, 23);
            this.DataSourceId.Name = "DataSourceId";
            this.DataSourceId.Size = new System.Drawing.Size(53, 12);
            this.DataSourceId.TabIndex = 2;
            this.DataSourceId.Text = "数据源ID";
            // 
            // txtDSName
            // 
            this.txtDSName.Location = new System.Drawing.Point(81, 55);
            this.txtDSName.Name = "txtDSName";
            this.txtDSName.ReadOnly = true;
            this.txtDSName.Size = new System.Drawing.Size(100, 21);
            this.txtDSName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "所 属 包：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "显示名称";
            // 
            // dataSourceName
            // 
            this.dataSourceName.AutoSize = true;
            this.dataSourceName.Location = new System.Drawing.Point(10, 58);
            this.dataSourceName.Name = "dataSourceName";
            this.dataSourceName.Size = new System.Drawing.Size(65, 12);
            this.dataSourceName.TabIndex = 0;
            this.dataSourceName.Text = "数据源名称";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.新增ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(839, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem1.Text = "文件";
            // 
            // 新增ToolStripMenuItem
            // 
            this.新增ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据源ToolStripMenuItem});
            this.新增ToolStripMenuItem.Name = "新增ToolStripMenuItem";
            this.新增ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.新增ToolStripMenuItem.Text = "新增";
            // 
            // 数据源ToolStripMenuItem
            // 
            this.数据源ToolStripMenuItem.Name = "数据源ToolStripMenuItem";
            this.数据源ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.数据源ToolStripMenuItem.Text = "数据源";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(609, 333);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(609, 333);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 409);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newDataSource;
        private System.Windows.Forms.ToolStripMenuItem addDataSource;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 新增ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据源ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tableStructTree;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtDSId;
        private System.Windows.Forms.Label DataSourceId;
        private System.Windows.Forms.TextBox txtDSName;
        private System.Windows.Forms.Label dataSourceName;
        private System.Windows.Forms.TextBox txtDSdisplaytext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolbtSave;
        private System.Windows.Forms.TextBox txtDSPackage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolbtCreateTableObj;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

