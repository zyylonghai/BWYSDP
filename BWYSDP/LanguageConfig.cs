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
                SDPCRL.COM.ModelManager.FormTemplate.LibFormPage fm = null;
                if (param.Length > 1)
                {
                    ds = param[0] as LibDataSource;
                    fm = param[1] as SDPCRL.COM.ModelManager.FormTemplate.LibFormPage;
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
                if (fm != null)
                {
                    DataTable dt = this.dataGridView1.DataSource as DataTable;
                    DataRow row = null;
                    if (dt != null)
                    {
                        row = dt.NewRow();
                        row["TableNm"] = string.Empty;
                        row["FieldNm"] = fm.FormId;
                        row[GetLanguageCol(dt)] = fm.FormName;
                        FilllanguageValue(languagedt, string.Empty, fm.FormId, row);
                        dt.Rows.Add(row);
                        foreach (SDPCRL.COM.ModelManager.FormTemplate.LibFormGroup fg in fm.FormGroups)
                        {
                            row = dt.NewRow();
                            row["TableNm"] = string.Empty;
                            row["FieldNm"] = fg.FormGroupName;
                            row[GetLanguageCol(dt)] = fg.FormGroupDisplayNm;
                            FilllanguageValue(languagedt, string.Empty, fg.FormGroupName, row);
                            dt.Rows.Add(row);
                        }
                        foreach (SDPCRL.COM.ModelManager.FormTemplate.LibGridGroup gg in fm.GridGroups)
                        {
                            row = dt.NewRow();
                            row["TableNm"] = string.Empty;
                            row["FieldNm"] = gg.GridGroupName;
                            row[GetLanguageCol(dt)] = gg.GridGroupDisplayNm;
                            FilllanguageValue(languagedt, string.Empty, gg.GridGroupName, row);
                            dt.Rows.Add(row);
                        }
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
            //DataRow[] drs = null;
            foreach (LibField f in dtstruct.Fields)
            {
                //if (languagedt != null) {
                //    drs = languagedt.Select(string.Format("TableNm='{0}' and FieldNm='{1}'",dtstruct .Name ,f.Name));
                //}
                row = dt.NewRow();
                row["TableNm"] = dtstruct.Name;
                row["FieldNm"] = f.Name;
                row[langcol] = f.DisplayName;
                FilllanguageValue(languagedt, dtstruct.Name, f.Name, row);
                //if (drs != null && drs.Length > 0)
                //{
                //    foreach (DataRow r in drs)
                //    {
                //        row[((Language)(Convert.ToInt32(r["LanguageId"]))).ToString()] = r["Vals"];
                //    }
                //}
                dt.Rows.Add(row);
            }
        }

        private DataColumn GetLanguageCol(DataTable dt)
        {
            DataColumn col = null;
            if (System.Globalization.CultureInfo.InstalledUICulture.Name.ToUpper() == "ZH-CN")
            {
                col = dt.Columns[Language.CHS.ToString()];
            }
            return col;
        }
        private void FilllanguageValue(DataTable langdt, string tbnm, string fieldnm,DataRow row)
        {
            DataRow[] drs = null;
            if (langdt != null)
            {
                drs = langdt.Select(string.Format("TableNm='{0}' and FieldNm='{1}'", tbnm, fieldnm));
                if (drs != null && drs.Length > 0)
                {
                    foreach (DataRow r in drs)
                    {
                        row[((Language)(Convert.ToInt32(r["LanguageId"]))).ToString()] = r["Vals"];
                    }
                }
            }
        }
    }
}
