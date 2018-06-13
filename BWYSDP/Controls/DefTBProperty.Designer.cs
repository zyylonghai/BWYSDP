namespace BWYSDP.Controls
{
    partial class DefTBProperty
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
            this.label1 = new System.Windows.Forms.Label();
            this.DefTB_txtDisplayName = new System.Windows.Forms.TextBox();
            this.DefTB_txtTableName = new System.Windows.Forms.TextBox();
            this.tableName = new System.Windows.Forms.Label();
            this.DefTB_txtID = new System.Windows.Forms.TextBox();
            this.tableID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "显示名称";
            // 
            // DefTB_txtDisplayName
            // 
            this.DefTB_txtDisplayName.Location = new System.Drawing.Point(87, 100);
            this.DefTB_txtDisplayName.Name = "DefTB_txtDisplayName";
            this.DefTB_txtDisplayName.Size = new System.Drawing.Size(162, 21);
            this.DefTB_txtDisplayName.TabIndex = 16;
            // 
            // DefTB_txtTableName
            // 
            this.DefTB_txtTableName.Location = new System.Drawing.Point(87, 61);
            this.DefTB_txtTableName.Name = "DefTB_txtTableName";
            this.DefTB_txtTableName.Size = new System.Drawing.Size(162, 21);
            this.DefTB_txtTableName.TabIndex = 15;
            // 
            // tableName
            // 
            this.tableName.AutoSize = true;
            this.tableName.Location = new System.Drawing.Point(25, 64);
            this.tableName.Name = "tableName";
            this.tableName.Size = new System.Drawing.Size(53, 12);
            this.tableName.TabIndex = 14;
            this.tableName.Text = "名    称";
            // 
            // DefTB_txtID
            // 
            this.DefTB_txtID.Location = new System.Drawing.Point(87, 22);
            this.DefTB_txtID.Name = "DefTB_txtID";
            this.DefTB_txtID.ReadOnly = true;
            this.DefTB_txtID.Size = new System.Drawing.Size(162, 21);
            this.DefTB_txtID.TabIndex = 13;
            // 
            // tableID
            // 
            this.tableID.AutoSize = true;
            this.tableID.Location = new System.Drawing.Point(25, 25);
            this.tableID.Name = "tableID";
            this.tableID.Size = new System.Drawing.Size(53, 12);
            this.tableID.TabIndex = 12;
            this.tableID.Text = "编    号";
            // 
            // DefTBProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DefTB_txtDisplayName);
            this.Controls.Add(this.DefTB_txtTableName);
            this.Controls.Add(this.tableName);
            this.Controls.Add(this.DefTB_txtID);
            this.Controls.Add(this.tableID);
            this.Name = "DefTBProperty";
            this.Size = new System.Drawing.Size(318, 324);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DefTB_txtDisplayName;
        private System.Windows.Forms.TextBox DefTB_txtTableName;
        private System.Windows.Forms.Label tableName;
        private System.Windows.Forms.TextBox DefTB_txtID;
        private System.Windows.Forms.Label tableID;
    }
}
