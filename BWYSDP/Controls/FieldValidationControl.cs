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
using SDPCRL.COM.ModelManager;

namespace BWYSDP.Controls
{
    public partial class FieldValidationControl : UserControl
    {
        LibFieldValidatExpress _entity;
        public FieldValidationControl()
        {
            InitializeComponent();
        }
        public FieldValidationControl(LibFieldValidatExpress entity)
            :this()
        {
            this._entity = entity;
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

        public void GetExpressValue()
        {
            if (this._entity == null) this._entity = new LibFieldValidatExpress();
            this._entity.Express = this.RtxbExpress.Text;
            this._entity.MsgCode = this.txtmsgcode.Text.Trim();
            this._entity.MsgParams = this.RtxbMsgParams.Text.Trim();
            //return this.RtxbExpress.Text;
        }

        public void SetExpressValue()
        {
            if (this._entity != null)
            {
                this.RtxbExpress.Text = _entity.Express;
                this.txtmsgcode.Text = _entity.MsgCode;
                this.RtxbMsgParams.Text = _entity.MsgParams;
            }

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
