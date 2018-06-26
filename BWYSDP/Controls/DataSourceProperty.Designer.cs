namespace BWYSDP.Controls
{
    partial class DataSourceProperty
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
            this.ds_txtDSID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ds_txtDSNm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ds_txtPackage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据源ID：";
            // 
            // ds_txtDSID
            // 
            this.ds_txtDSID.Location = new System.Drawing.Point(98, 22);
            this.ds_txtDSID.Name = "ds_txtDSID";
            this.ds_txtDSID.Size = new System.Drawing.Size(158, 21);
            this.ds_txtDSID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "数据源名称：";
            // 
            // ds_txtDSNm
            // 
            this.ds_txtDSNm.Location = new System.Drawing.Point(98, 68);
            this.ds_txtDSNm.Name = "ds_txtDSNm";
            this.ds_txtDSNm.Size = new System.Drawing.Size(158, 21);
            this.ds_txtDSNm.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "所属包：";
            // 
            // ds_txtPackage
            // 
            this.ds_txtPackage.Location = new System.Drawing.Point(98, 120);
            this.ds_txtPackage.Name = "ds_txtPackage";
            this.ds_txtPackage.Size = new System.Drawing.Size(158, 21);
            this.ds_txtPackage.TabIndex = 1;
            // 
            // DataSourceProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ds_txtPackage);
            this.Controls.Add(this.ds_txtDSNm);
            this.Controls.Add(this.ds_txtDSID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DataSourceProperty";
            this.Size = new System.Drawing.Size(325, 375);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ds_txtDSID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ds_txtDSNm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ds_txtPackage;
    }
}
