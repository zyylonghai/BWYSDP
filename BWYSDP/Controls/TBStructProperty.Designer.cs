namespace BWYSDP.Controls
{
    partial class TBStructProperty
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbStruct_txtTableName = new System.Windows.Forms.TextBox();
            this.tbStruct_txtTableDisplayName = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btselect = new System.Windows.Forms.Button();
            this.tbStruct_combcreateTBStruct = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "表   名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "显示名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "创建结构：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "主   键：";
            // 
            // tbStruct_txtTableName
            // 
            this.tbStruct_txtTableName.Location = new System.Drawing.Point(90, 22);
            this.tbStruct_txtTableName.Name = "tbStruct_txtTableName";
            this.tbStruct_txtTableName.Size = new System.Drawing.Size(161, 21);
            this.tbStruct_txtTableName.TabIndex = 1;
            // 
            // tbStruct_txtTableDisplayName
            // 
            this.tbStruct_txtTableDisplayName.Location = new System.Drawing.Point(90, 58);
            this.tbStruct_txtTableDisplayName.Name = "tbStruct_txtTableDisplayName";
            this.tbStruct_txtTableDisplayName.Size = new System.Drawing.Size(161, 21);
            this.tbStruct_txtTableDisplayName.TabIndex = 1;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(90, 124);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(161, 21);
            this.textBox4.TabIndex = 1;
            // 
            // btselect
            // 
            this.btselect.Location = new System.Drawing.Point(257, 122);
            this.btselect.Name = "btselect";
            this.btselect.Size = new System.Drawing.Size(44, 23);
            this.btselect.TabIndex = 2;
            this.btselect.Text = "选择";
            this.btselect.UseVisualStyleBackColor = true;
            // 
            // tbStruct_combcreateTBStruct
            // 
            this.tbStruct_combcreateTBStruct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbStruct_combcreateTBStruct.FormattingEnabled = true;
            this.tbStruct_combcreateTBStruct.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tbStruct_combcreateTBStruct.Items.AddRange(new object[] {
            "是",
            "否"});
            this.tbStruct_combcreateTBStruct.Location = new System.Drawing.Point(90, 92);
            this.tbStruct_combcreateTBStruct.Name = "tbStruct_combcreateTBStruct";
            this.tbStruct_combcreateTBStruct.Size = new System.Drawing.Size(161, 20);
            this.tbStruct_combcreateTBStruct.TabIndex = 3;
            // 
            // TBStructProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbStruct_combcreateTBStruct);
            this.Controls.Add(this.btselect);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.tbStruct_txtTableDisplayName);
            this.Controls.Add(this.tbStruct_txtTableName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TBStructProperty";
            this.Size = new System.Drawing.Size(353, 324);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbStruct_txtTableName;
        private System.Windows.Forms.TextBox tbStruct_txtTableDisplayName;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btselect;
        private System.Windows.Forms.ComboBox tbStruct_combcreateTBStruct;
    }
}
