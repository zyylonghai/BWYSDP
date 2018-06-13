namespace BWYSDP
{
    partial class DSAdd
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
            this.txtDSDisplayText = new System.Windows.Forms.TextBox();
            this.btCance = new System.Windows.Forms.Button();
            this.btComfirm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDSPackage = new System.Windows.Forms.TextBox();
            this.txtDSName = new System.Windows.Forms.TextBox();
            this.txtDSID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDSDisplayText
            // 
            this.txtDSDisplayText.Location = new System.Drawing.Point(111, 127);
            this.txtDSDisplayText.Name = "txtDSDisplayText";
            this.txtDSDisplayText.Size = new System.Drawing.Size(100, 21);
            this.txtDSDisplayText.TabIndex = 5;
            // 
            // btCance
            // 
            this.btCance.Location = new System.Drawing.Point(146, 213);
            this.btCance.Name = "btCance";
            this.btCance.Size = new System.Drawing.Size(75, 23);
            this.btCance.TabIndex = 4;
            this.btCance.Text = "取消";
            this.btCance.UseVisualStyleBackColor = true;
            this.btCance.Click += new System.EventHandler(this.btCance_Click);
            // 
            // btComfirm
            // 
            this.btComfirm.Location = new System.Drawing.Point(31, 213);
            this.btComfirm.Name = "btComfirm";
            this.btComfirm.Size = new System.Drawing.Size(75, 23);
            this.btComfirm.TabIndex = 3;
            this.btComfirm.Text = "确定";
            this.btComfirm.UseVisualStyleBackColor = true;
            this.btComfirm.Click += new System.EventHandler(this.btComfirm_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "所  属  包：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "显示名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据源名称：";
            // 
            // txtDSPackage
            // 
            this.txtDSPackage.Location = new System.Drawing.Point(111, 165);
            this.txtDSPackage.Name = "txtDSPackage";
            this.txtDSPackage.Size = new System.Drawing.Size(100, 21);
            this.txtDSPackage.TabIndex = 1;
            // 
            // txtDSName
            // 
            this.txtDSName.Location = new System.Drawing.Point(111, 88);
            this.txtDSName.Name = "txtDSName";
            this.txtDSName.Size = new System.Drawing.Size(100, 21);
            this.txtDSName.TabIndex = 1;
            // 
            // txtDSID
            // 
            this.txtDSID.Location = new System.Drawing.Point(111, 37);
            this.txtDSID.Name = "txtDSID";
            this.txtDSID.Size = new System.Drawing.Size(100, 21);
            this.txtDSID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数 据 源 ID";
            // 
            // DSAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.txtDSDisplayText);
            this.Controls.Add(this.btCance);
            this.Controls.Add(this.btComfirm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDSPackage);
            this.Controls.Add(this.txtDSName);
            this.Controls.Add(this.txtDSID);
            this.Controls.Add(this.label1);
            this.Name = "DSAdd";
            this.Text = "DSAdd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDSID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDSName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDSPackage;
        private System.Windows.Forms.Button btComfirm;
        private System.Windows.Forms.Button btCance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDSDisplayText;
    }
}