using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BWYSDP
{
    public partial class FieldCollectionForm : LibFormBase 
    {
        public FieldCollectionForm(Panel p)
        {
            InitializeComponent();
            //p.Location = new System.Drawing.Point(0, 65);
            //p.TabIndex = 1;
            this.panel2.Controls.Add(p);
        }

        private void LoadData()
        {

        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
