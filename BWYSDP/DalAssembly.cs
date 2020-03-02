using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDPCRL.COM;
using SDPCRL.CORE;

namespace BWYSDP
{
    public partial class DalAssembly : LibFormBase
    {
        public DalAssembly()
        {
            InitializeComponent();
        }

        protected override void DoSetParam(string tag, params object[] param)
        {
            base.DoSetParam(tag, param);
            if (tag == "dalassembly")
            {
                BLL.BllDataBase bll = new BLL.BllDataBase();
                List<FuncAssemblyInfo> result = bll.GetAssemblyinfos();
                if (result != null)
                {
                    DataTable dt = this.dataGridView1.DataSource as DataTable;
                    DataTable data = LibSysUtils.ToDataTable(result);
                    DataTableHelp dthelp = new DataTableHelp(data, dt);
                    dthelp.CopyStable();
                    //DataRow row = null;
                    //foreach (var item in result)
                    //{
                    //    row = dt.NewRow();
                    //    row["FuncID"] = item.FuncID;
                    //    row [""]
                    //}
                }
            }
        }

        private void DalAssembly_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("FuncAssemblyInfo");
            DataColumn col = null;
            col = new DataColumn("FuncID");
            col.Caption = "ProgId";
            dt.Columns.Add(col);
            col = new DataColumn("AssemblyName");
            col.Caption = "程序集名";
            dt.Columns.Add(col);
            col = new DataColumn("TypeFullName");
            col.Caption = "对象全名";
            dt.Columns.Add(col);
            this.dataGridView1.DataSource = dt;
            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
            {
                c.HeaderText = dt.Columns[c.Name].Caption;
            }
        }
    }
}
