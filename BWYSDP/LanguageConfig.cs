﻿using SDPCRL.COM;
using SDPCRL.COM.ModelManager;
using SDPCRL.COM.ModelManager.FormTemplate;
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
        private DataColumn _currenlangcol = null;
        public DataColumn CurrentLangCol
        {
            get {
                if (_currenlangcol == null)
                {
                    DataTable dt = this.dataGridView1.DataSource as DataTable;
                    switch (System.Globalization.CultureInfo.InstalledUICulture.Name.ToUpper())
                    {
                        case "ZH-CN":
                            _currenlangcol = dt.Columns[Language.CHS.ToString()];
                            break;
                        case "":
                            break;
                    }
                }
                return _currenlangcol;
            }
        }
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
                SDPCRL.COM.ModelManager.LibKeyValueCollection keyValueCollection = null;
                if (param.Length > 1)
                {
                    ds = param[0] as LibDataSource;
                    fm = param[1] as SDPCRL.COM.ModelManager.FormTemplate.LibFormPage;
                    keyValueCollection = param[2] as SDPCRL.COM.ModelManager.LibKeyValueCollection;
                }
                if (keyValueCollection != null)
                {
                    this.textBox1.Text = keyValueCollection.ID;
                    languagedt = this.BllData.Getlanguagebydsid(keyValueCollection.ID);
                    if (keyValueCollection.KeyValues != null)
                    {
                        foreach (LibKeyValue kv in keyValueCollection.KeyValues)
                        {
                            AddDataGridRow(string.Empty, kv.Key.ToString(), kv.Value.ToString());
                        }
                    }

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
                        AddDataGridRow(string.Empty, fm.FormId, fm.FormName);
                        if (fm.FormGroups != null)
                        {
                            foreach (SDPCRL.COM.ModelManager.FormTemplate.LibFormGroup fg in fm.FormGroups)
                            {
                                AddDataGridRow(string.Empty, fg.FormGroupName, fg.FormGroupDisplayNm);
                            }
                        }
                        if (fm.GridGroups != null)
                        {
                            foreach (SDPCRL.COM.ModelManager.FormTemplate.LibGridGroup gg in fm.GridGroups)
                            {
                                AddDataGridRow(string.Empty, gg.GridGroupName, gg.GridGroupDisplayNm);
                                if (gg.GdButtons == null) continue;
                                foreach (LibGridButton item in gg.GdButtons)
                                {
                                    AddDataGridRow(string.Empty, item.GridButtonName, item.GridButtonDisplayNm);
                                }
                            }
                        }
                        if (fm.BtnGroups != null)
                            foreach (LibButtonGroup btngroup in fm.BtnGroups)
                            {
                                if (btngroup.LibButtons == null) continue;
                                //if(btngroup .LibButtons !=null )
                                //{
                                foreach (LibButton btn in btngroup.LibButtons)
                                {
                                    AddDataGridRow(string.Empty, btn.LibButtonName, btn.LibButtonDisplayNm);
                                }
                                //}
                                
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
            //DataRow row = null;
            DataTable dt = this.dataGridView1.DataSource as DataTable;
            //DataColumn langcol = null;
            if (isclear)
                dt.Rows.Clear();
            //if (System.Globalization.CultureInfo.InstalledUICulture.Name.ToUpper() == "ZH-CN")
            //{
            //    langcol = dt.Columns[Language.CHS.ToString()];
            //}
            //DataRow[] drs = null;
            foreach (LibField f in dtstruct.Fields)
            {
                if (f.Items != null)
                {
                    foreach (LibKeyValue item in f.Items)
                    {
                        if (string.IsNullOrEmpty(item.FromkeyValueID))
                        {
                            AddDataGridRow(dtstruct.Name, string.Format("{0}_{1}", f.Name, item.Key), item.Value.ToString ());
                            //row = dt.NewRow();
                            //row["TableNm"] = dtstruct.Name;
                            //row["FieldNm"] = string.Format("{0}_{1}", f.Name, item.Key);
                            //row[langcol] = item.Value;
                            //FilllanguageValue(languagedt, dtstruct.Name, row["FieldNm"].ToString (), row);
                            //dt.Rows.Add(row);
                        }
                    }
                }
                AddDataGridRow(dtstruct.Name, f.Name, f.DisplayName);
                //row = dt.NewRow();
                //row["TableNm"] = dtstruct.Name;
                //row["FieldNm"] = f.Name;
                //row[langcol] = f.DisplayName;
                //FilllanguageValue(languagedt, dtstruct.Name, f.Name, row);
                //dt.Rows.Add(row);
            }
        }
        private void AddDataGridRow(string tablenm,string fieldnm,string langvalue)
        {
            DataTable dt = this.dataGridView1.DataSource as DataTable;
            DataRow row = null;
            row = dt.NewRow();
            row["TableNm"] = tablenm;
            row["FieldNm"] = fieldnm;
            row[CurrentLangCol] = langvalue;
            FilllanguageValue(languagedt, tablenm, fieldnm, row);
            dt.Rows.Add(row);
        }

        //private DataColumn GetLanguageCol(DataTable dt)
        //{
        //    DataColumn col = null;
        //    if (System.Globalization.CultureInfo.InstalledUICulture.Name.ToUpper() == "ZH-CN")
        //    {
        //        col = dt.Columns[Language.CHS.ToString()];
        //    }
        //    return col;
        //}
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

        /// <summary>
        ///一键翻译
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btntranslation_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Show(this.btntranslation, 0 , this.btntranslation.Height);
        }

        /// <summary>查询</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox2.Text.Trim()))
            {
                languagedt = this.BllData.Getlanguagebydsid(this.textBox1.Text.Trim());
            }
            else
                languagedt = this.BllData.Getlanguage(this.textBox1.Text.Trim(), this.textBox2.Text.Trim());

            if (languagedt != null && languagedt.Rows != null)
            {
                DataTable dt = this.dataGridView1.DataSource as DataTable;
                DataRow row = null;
                dt.Rows.Clear();
                languagedt.DefaultView.Sort = "FieldNm";
                string prefieldnm = string.Empty;
                foreach (DataRowView r in languagedt.DefaultView)
                {
                    //DataRow[] rws = dt.Select(string.Format("FieldNm='{0}'", r["FieldNm"]));
                    if (r["FieldNm"].ToString() != prefieldnm)
                    {
                        row = dt.NewRow();
                        row["TableNm"] = r["TableNm"];
                        row["FieldNm"] = r["FieldNm"];
                        dt.Rows.Add(row);
                        prefieldnm = r["FieldNm"].ToString();
                    }

                    row[((Language)(Convert.ToInt32(r["LanguageId"]))).ToString()] = r["Vals"];
                }
            }
        }
    }
}
