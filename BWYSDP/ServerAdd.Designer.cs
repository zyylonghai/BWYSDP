namespace BWYSDP
{
    partial class ServerAdd
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtpoint = new System.Windows.Forms.TextBox();
            this.combConnType = new System.Windows.Forms.ComboBox();
            this.btnsave = new System.Windows.Forms.Button();
            this.btncance = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.combaccountId = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "IP地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "端口：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "链接方式：";
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(95, 60);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(147, 21);
            this.txtServerName.TabIndex = 1;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(95, 97);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(147, 21);
            this.txtIPAddress.TabIndex = 1;
            // 
            // txtpoint
            // 
            this.txtpoint.Location = new System.Drawing.Point(95, 136);
            this.txtpoint.Name = "txtpoint";
            this.txtpoint.Size = new System.Drawing.Size(147, 21);
            this.txtpoint.TabIndex = 1;
            // 
            // combConnType
            // 
            this.combConnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combConnType.FormattingEnabled = true;
            this.combConnType.Items.AddRange(new object[] {
            "TCP",
            "HTTP"});
            this.combConnType.Location = new System.Drawing.Point(95, 23);
            this.combConnType.Name = "combConnType";
            this.combConnType.Size = new System.Drawing.Size(147, 20);
            this.combConnType.TabIndex = 2;
            // 
            // btnsave
            // 
            this.btnsave.Location = new System.Drawing.Point(46, 206);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(75, 23);
            this.btnsave.TabIndex = 3;
            this.btnsave.Text = "保存";
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btncance
            // 
            this.btncance.Location = new System.Drawing.Point(154, 206);
            this.btncance.Name = "btncance";
            this.btncance.Size = new System.Drawing.Size(75, 23);
            this.btncance.TabIndex = 3;
            this.btncance.Text = "取消";
            this.btncance.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "账套：";
            // 
            // combaccountId
            // 
            this.combaccountId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combaccountId.FormattingEnabled = true;
            this.combaccountId.Location = new System.Drawing.Point(95, 170);
            this.combaccountId.Name = "combaccountId";
            this.combaccountId.Size = new System.Drawing.Size(147, 20);
            this.combaccountId.TabIndex = 4;
            this.combaccountId.SelectedValueChanged += new System.EventHandler(this.combaccountId_SelectedValueChanged);
            this.combaccountId.Click += new System.EventHandler(this.combaccountId_Click);
            // 
            // ServerAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 259);
            this.Controls.Add(this.combaccountId);
            this.Controls.Add(this.btncance);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.combConnType);
            this.Controls.Add(this.txtpoint);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ServerAdd";
            this.Text = "ServerAdd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtpoint;
        private System.Windows.Forms.ComboBox combConnType;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btncance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combaccountId;
    }
}