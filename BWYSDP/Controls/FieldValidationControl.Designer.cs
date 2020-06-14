namespace BWYSDP.Controls
{
    partial class FieldValidationControl
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.RtxbExpress = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LstbFuncs = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtmsgcode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RtxbMsgParams = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.RtxbExpress);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(832, 519);
            this.splitContainer1.SplitterDistance = 448;
            this.splitContainer1.TabIndex = 0;
            // 
            // RtxbExpress
            // 
            this.RtxbExpress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RtxbExpress.Location = new System.Drawing.Point(0, 0);
            this.RtxbExpress.Name = "RtxbExpress";
            this.RtxbExpress.Size = new System.Drawing.Size(448, 519);
            this.RtxbExpress.TabIndex = 0;
            this.RtxbExpress.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LstbFuncs);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 252);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(380, 267);
            this.panel2.TabIndex = 3;
            // 
            // LstbFuncs
            // 
            this.LstbFuncs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstbFuncs.FormattingEnabled = true;
            this.LstbFuncs.ItemHeight = 12;
            this.LstbFuncs.Location = new System.Drawing.Point(0, 21);
            this.LstbFuncs.Name = "LstbFuncs";
            this.LstbFuncs.Size = new System.Drawing.Size(380, 246);
            this.LstbFuncs.TabIndex = 1;
            this.LstbFuncs.DoubleClick += new System.EventHandler(this.LstbFuncs_DoubleClick);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(380, 21);
            this.textBox1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RtxbMsgParams);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtmsgcode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 252);
            this.panel1.TabIndex = 2;
            // 
            // txtmsgcode
            // 
            this.txtmsgcode.Location = new System.Drawing.Point(17, 82);
            this.txtmsgcode.Name = "txtmsgcode";
            this.txtmsgcode.Size = new System.Drawing.Size(214, 21);
            this.txtmsgcode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "验证失败信息代码：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(92, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(139, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "有效性验证：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "信息参数：";
            // 
            // RtxbMsgParams
            // 
            this.RtxbMsgParams.Location = new System.Drawing.Point(17, 136);
            this.RtxbMsgParams.Name = "RtxbMsgParams";
            this.RtxbMsgParams.Size = new System.Drawing.Size(214, 96);
            this.RtxbMsgParams.TabIndex = 5;
            this.RtxbMsgParams.Text = "";
            // 
            // FieldValidationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FieldValidationControl";
            this.Size = new System.Drawing.Size(832, 519);
            this.Load += new System.EventHandler(this.FieldValidationControl_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox RtxbExpress;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox LstbFuncs;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtmsgcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox RtxbMsgParams;
        private System.Windows.Forms.Label label3;
    }
}
