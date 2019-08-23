using SDPCRL.COM;
using SDPCRL.COM.ModelManager;
using SDPCRL.CORE;
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
    public partial class LanguageConfig : LibFormBase
    {
        LibDataSource ds = null;
        DataTable languagedt = null;
        public LanguageConfig()
        {
            InitializeComponent();
        }

        protected override void DoSetParam(string tag, params object[] param)
        {
            base.DoSetParam(tag, param);
            if (LibSysUtils.Compare(tag, "language"))
            {
                if (param.Length > 0)
                {
                    ds = param[0] as LibDataSource;
                }
                if (ds == null) return;
                this.textBox1.Text = ds.DSID;
                this.comboBox1.Items.Add(string.Empty);
                languagedt = this.BllData.Getlanguagebydsid(ds.DSID);
                foreach (LibDefineTable deftb in ds.DefTables)
                {
                    foreach (LibDataTableStruct dt in deftb.TableStruct)
                    {
                        this.comboBox1.Items.Add(dt.Name);
                        FillDataGrid(dt, false);
                    }
                }
            }
        }

        private void LanguageConfig_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("Language_Field");
            DataColumn col = null;
            col = new DataColumn("TableNm");
            col.Caption = "表名";
            dt.Columns.Add(col);
            col = new DataColumn("FieldNm");
            col.Caption = "字段名";
            dt.Columns.Add(col);
            foreach (var item in Enum.GetValues(typeof(Language)))
            {
                col = new DataColumn(item.ToString());
                col.Caption = ReSourceManage.GetResource(item);
                dt.Columns.Add(col);
            }
            this.dataGridView1.DataSource = dt;
            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
            {
                c.HeaderText = dt.Columns[c.Name].Caption;
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            foreach (LibDefineTable item in ds.DefTables)
            {
                foreach (LibDataTableStruct dtstruct in item.TableStruct)
                {
                    if (LibSysUtils.IsNULLOrEmpty(cmb.SelectedItem))
                    {
                        FillDataGrid(dtstruct, false);
                    }
                    else if (dtstruct.Name == cmb.SelectedItem.ToString())
                    {
                        FillDataGrid(dtstruct, true);
                    }
                }
            }
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            BLL.BllDataBase bll = new BLL.BllDataBase();
            DataTable dt = this.dataGridView1.DataSource as DataTable;
            bll.Updatelanguage(dt, this.textBox1.Text.Trim ());
        }

        private void FillDataGrid(LibDataTableStruct dtstruct, bool isclear)
        {
            DataRow row = null;
            DataTable dt = this.dataGridView1.DataSource as DataTable;
            DataColumn langcol = null;
            if (isclear)
                dt.Rows.Clear();
            if (System.Globalization.CultureInfo.InstalledUICulture.Name.ToUpper() == "ZH-CN")
            {
                langcol = dt.Columns[Language.CHS.ToString()];
            }
            DataRow[] drs = null;
            foreach (LibField f in dtstruct.Fields)
            {
                if (languagedt != null) {
                    drs = languagedt.Select(string.Format("TableNm='{0}' and FieldNm='{1}'",dtstruct .Name ,f.Name));
                }
                row = dt.NewRow();
                row["TableNm"] = dtstruct.Name;
                row["FieldNm"] = f.Name;
                row[langcol] = f.DisplayName;
                if (drs != null && drs.Length > 0)
                {
                    foreach (DataRow r in drs)
                    {
                        row[((Language)(Convert.ToInt32(r["LanguageId"]))).ToString()] = r["Vals"];
                        //switch ((Language)(Convert .ToInt32(r["LanguageId"])))
                        //{
                        //    case Language.CHS:
                        //        row[Language.CHS.ToString()] = r["Vals"];
                        //        break;
                        //    case Language.CHS_F:
                        //        row[Language.CHS_F.ToString()] = r["Vals"];
                        //        break;
                        //    case Language.ENG:
                        //        row[Language.ENG.ToString()] = r["Vals"];
                        //        break;
                        //}
                    }
                }
                dt.Rows.Add(row);
            }
        }
    }
}
