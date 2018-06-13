namespace BWYSDP.Controls
{
    partial class DefFieldProperty
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
            this.fd_txtFieldName = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.fd_txtDisplayText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fd_combFieldType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fd_combAllowNull = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fd_relativeMast = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.fd_combActive = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.fd_txtAliasName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "字段名称：";
            // 
            // fd_txtFieldName
            // 
            this.fd_txtFieldName.Location = new System.Drawing.Point(105, 12);
            this.fd_txtFieldName.Name = "fd_txtFieldName";
            this.fd_txtFieldName.Size = new System.Drawing.Size(146, 21);
            this.fd_txtFieldName.TabIndex = 1;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(34, 56);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(65, 12);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "显示名称：";
            // 
            // fd_txtDisplayText
            // 
            this.fd_txtDisplayText.Location = new System.Drawing.Point(105, 52);
            this.fd_txtDisplayText.Name = "fd_txtDisplayText";
            this.fd_txtDisplayText.Size = new System.Drawing.Size(146, 21);
            this.fd_txtDisplayText.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "字段类型：";
            // 
            // fd_combFieldType
            // 
            this.fd_combFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fd_combFieldType.FormattingEnabled = true;
            this.fd_combFieldType.Items.AddRange(new object[] {
            "字符串型-String",
            "整型-Interger",
            "长整型-Long",
            "浮点型-Decimal",
            "小位数-Byte",
            "日期-Date",
            "日期时间-DateTime"});
            this.fd_combFieldType.Location = new System.Drawing.Point(105, 123);
            this.fd_combFieldType.Name = "fd_combFieldType";
            this.fd_combFieldType.Size = new System.Drawing.Size(146, 20);
            this.fd_combFieldType.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "允许NULL：";
            // 
            // fd_combAllowNull
            // 
            this.fd_combAllowNull.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fd_combAllowNull.FormattingEnabled = true;
            this.fd_combAllowNull.Items.AddRange(new object[] {
            "是",
            "否"});
            this.fd_combAllowNull.Location = new System.Drawing.Point(105, 165);
            this.fd_combAllowNull.Name = "fd_combAllowNull";
            this.fd_combAllowNull.Size = new System.Drawing.Size(146, 20);
            this.fd_combAllowNull.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "关联数据源：";
            // 
            // fd_relativeMast
            // 
            this.fd_relativeMast.Location = new System.Drawing.Point(105, 214);
            this.fd_relativeMast.Name = "fd_relativeMast";
            this.fd_relativeMast.Size = new System.Drawing.Size(117, 21);
            this.fd_relativeMast.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "是否虚字段：";
            // 
            // fd_combActive
            // 
            this.fd_combActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fd_combActive.FormattingEnabled = true;
            this.fd_combActive.Items.AddRange(new object[] {
            "是",
            "否"});
            this.fd_combActive.Location = new System.Drawing.Point(109, 256);
            this.fd_combActive.Name = "fd_combActive";
            this.fd_combActive.Size = new System.Drawing.Size(142, 20);
            this.fd_combActive.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(226, 214);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "选择";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "字段别名：";
            // 
            // fd_txtAliasName
            // 
            this.fd_txtAliasName.Location = new System.Drawing.Point(105, 88);
            this.fd_txtAliasName.Name = "fd_txtAliasName";
            this.fd_txtAliasName.Size = new System.Drawing.Size(146, 21);
            this.fd_txtAliasName.TabIndex = 3;
            // 
            // DefFieldProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.fd_relativeMast);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.fd_combActive);
            this.Controls.Add(this.fd_combAllowNull);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.fd_combFieldType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fd_txtAliasName);
            this.Controls.Add(this.fd_txtDisplayText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.fd_txtFieldName);
            this.Controls.Add(this.label1);
            this.Name = "DefFieldProperty";
            this.Size = new System.Drawing.Size(304, 489);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fd_txtFieldName;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.TextBox fd_txtDisplayText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox fd_combFieldType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox fd_combAllowNull;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox fd_relativeMast;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox fd_combActive;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox fd_txtAliasName;
    }
}
