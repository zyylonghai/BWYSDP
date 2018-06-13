namespace InitialTool
{
    partial class DBConfig
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.combConType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.combDataBaseType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerAddr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtpwd = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDataBase = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "连接方式";
            // 
            // combConType
            // 
            this.combConType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combConType.FormattingEnabled = true;
            this.combConType.Items.AddRange(new object[] {
            "TCP",
            "HTTP"});
            this.combConType.Location = new System.Drawing.Point(91, 38);
            this.combConType.Name = "combConType";
            this.combConType.Size = new System.Drawing.Size(121, 20);
            this.combConType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据库种类";
            // 
            // combDataBaseType
            // 
            this.combDataBaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combDataBaseType.FormattingEnabled = true;
            this.combDataBaseType.Items.AddRange(new object[] {
            "SQL SERVER",
            "ORACLE"});
            this.combDataBaseType.Location = new System.Drawing.Point(91, 82);
            this.combDataBaseType.Name = "combDataBaseType";
            this.combDataBaseType.Size = new System.Drawing.Size(121, 20);
            this.combDataBaseType.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "服务器地址";
            // 
            // txtServerAddr
            // 
            this.txtServerAddr.Location = new System.Drawing.Point(91, 123);
            this.txtServerAddr.Name = "txtServerAddr";
            this.txtServerAddr.Size = new System.Drawing.Size(121, 21);
            this.txtServerAddr.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "用户名";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(91, 209);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(121, 21);
            this.txtUserId.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 258);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "密码";
            // 
            // txtpwd
            // 
            this.txtpwd.Location = new System.Drawing.Point(91, 255);
            this.txtpwd.Name = "txtpwd";
            this.txtpwd.Size = new System.Drawing.Size(121, 21);
            this.txtpwd.TabIndex = 9;
            this.txtpwd.UseSystemPasswordChar = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 323);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "测试链接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(128, 323);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "账套：";
            // 
            // txtDataBase
            // 
            this.txtDataBase.Location = new System.Drawing.Point(91, 164);
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.Size = new System.Drawing.Size(121, 21);
            this.txtDataBase.TabIndex = 13;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(224, 323);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 384);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtDataBase);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtpwd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtServerAddr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.combDataBaseType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.combConType);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "数据库配置工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combConType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combDataBaseType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerAddr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtpwd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDataBase;
        private System.Windows.Forms.Button button3;
    }
}

