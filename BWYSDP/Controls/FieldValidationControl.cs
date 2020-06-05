using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BWYSDP.com;
using SDPCRL.COM;

namespace BWYSDP.Controls
{
    public partial class FieldValidationControl : UserControl
    {
        public FieldValidationControl()
        {
            InitializeComponent();
        }

        private void FieldValidationControl_Load(object sender, EventArgs e)
        {
            #region
            this.LstbFuncs.Items.Clear();
            List<LibdefFunc> funcs = ValidateExpressHelper.GetLibdefFuncs();
            this.LstbFuncs.Items.AddRange(funcs.ToArray());
            //this.LstbFuncs.Items.Add(new LibdefFunc { FuncNm = "Sum", FuncDesc = "求和" });
            //this.LstbFuncs.Items.Add(new LibdefFunc { FuncNm="Avg",FuncDesc="求平均值"});
            #endregion 
        }

        public string GetExpressValue()
        {
            return this.RtxbExpress.Text;
        }

        public void SetExpressValue(string express)
        {
            this.RtxbExpress.Text = express;
        }

        private void LstbFuncs_DoubleClick(object sender, EventArgs e)
        {
            LibdefFunc libdefFunc = this.LstbFuncs.SelectedItem as LibdefFunc;
            if (libdefFunc != null)
                this.RtxbExpress.Text += libdefFunc.FuncNm;
        }

        //private void LstbFuncs_DoubleClick(object sender, EventArgs e)
        //{
        //    LibdefFunc libdefFunc = this.LstbFuncs.SelectedItem as LibdefFunc;
        //    this.RtxbExpress.Text += libdefFunc.FuncNm;
        //}
    }
}
