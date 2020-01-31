using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDPCRL.COM.ModelManager;
using BWYSDP.com;
using SDPCRL.CORE;

namespace BWYSDP.Controls
{
    public partial class FromSourceProperty : BaseUserControl<LibFromSourceField>
    {
        LibDataSource _ds = null;
        public FromSourceProperty()
        {
            InitializeComponent();
            InitializeControls();
        }
        public FromSourceProperty(string name)
            : this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibFromSourceField entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
        }

        public override void TextAndBotton_Click(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            string ctrNm = ctl.Name.Replace(SysConstManage.BtnCtrlNmPrefix, "");
            Panel p = new Panel();
            p.Dock = DockStyle.Fill;
            p.Name = "pfieldcollection";
            p.AutoScroll = true;
            ListBox listBox = new ListBox();
            listBox.Dock = DockStyle.Fill;
            p.Controls.Add(listBox);
            if (string.Compare(ctrNm, "fsfield_FromDataSource") == 0)//来源数据源
            {
                string[] dsarray = ModelManager.GetAllDataSourceNm(string.Empty);
                if (dsarray != null && dsarray.Length > 0)
                {
                    foreach (string item in dsarray)
                    {
                        listBox.Items.Add(item);
                    }
                }
                FieldCollectionForm fielsform = new FieldCollectionForm(p);
                DialogResult dialog = fielsform.ShowDialog(this);
                if (dialog == DialogResult.OK)
                {
                    if (this.Controls[ctrNm].Text.Trim() != listBox.SelectedItem.ToString())
                    {
                        foreach (Control c in this.Controls)
                        {
                            if (c.GetType().Equals(typeof(TextBox)))
                                c.Text = string.Empty;
                        }
                        this.Controls[ctrNm].Text = listBox.SelectedItem.ToString();
                        this.entity.FromDataSource = listBox.SelectedItem.ToString();
                        //this.Controls["fsfield_FromDefindTableNm"].Text = string.Empty;
                        //this.Controls["fsfield_FromStructTableNm"].Text = string.Empty;
                        //this.Controls["fsfield_FromFieldNm"].Text = string.Empty; 
                    }
                }
            }
            else if (string.Compare(ctrNm, "fsfield_FromDefindTableNm") == 0)//来源自定义表名
            {
                if (!string.IsNullOrEmpty(this.Controls["fsfield_FromDataSource"].Text))
                {
                    this._ds = ModelManager.GetDataSource(this.Controls["fsfield_FromDataSource"].Text);
                    if (this._ds != null && this._ds.DefTables != null)
                    {
                        foreach (LibDefineTable deftb in this._ds.DefTables)
                        {
                            listBox.Items.Add(deftb.TableName);
                        }
                    }
                }
                FieldCollectionForm fielsform = new FieldCollectionForm(p);
                DialogResult dialog = fielsform.ShowDialog(this);
                if (dialog == DialogResult.OK)
                {
                    this.Controls[ctrNm].Text = listBox.SelectedItem.ToString();
                    this.entity.FromDefindTableNm = listBox.SelectedItem.ToString();
                }
            }
            else if (string.Compare(ctrNm, "fsfield_FromStructTableNm") == 0)//来源数据表名
            {
                if (!string.IsNullOrEmpty(this.Controls["fsfield_FromDefindTableNm"].Text))
                {
                    if (_ds == null)
                        _ds = ModelManager.GetDataSource(this.Controls["fsfield_FromDataSource"].Text);
                    LibDefineTable deftb = this._ds.DefTables.FindFirst("TableName", this.Controls["fsfield_FromDefindTableNm"].Text.Trim());
                    if (deftb != null)
                    {
                        foreach (LibDataTableStruct dt in deftb.TableStruct)
                        {
                            listBox.Items.Add(dt.Name);
                        }
                    }

                }
                FieldCollectionForm fielsform = new FieldCollectionForm(p);
                DialogResult dialog = fielsform.ShowDialog(this);
                if (dialog == DialogResult.OK)
                {
                    this.Controls[ctrNm].Text = listBox.SelectedItem.ToString();
                    this.entity.FromStructTableNm = listBox.SelectedItem.ToString();
                    //foreach (LibDataTableStruct item in )
                }
            }
            else if (string.Compare(ctrNm, "fsfield_FromFieldNm") == 0 || string.Compare(ctrNm, "fsfield_FromFieldDesc") == 0)//来源字段
            {
                if (!string.IsNullOrEmpty(this.Controls["fsfield_FromStructTableNm"].Text))
                {
                    if (_ds == null)
                        _ds = ModelManager.GetDataSource(this.Controls["fsfield_FromDataSource"].Text);
                    LibDefineTable deftb = this._ds.DefTables.FindFirst("TableName", this.Controls["fsfield_FromDefindTableNm"].Text.Trim());
                    LibDataTableStruct dtstruct = deftb.TableStruct.FindFirst("Name", this.Controls["fsfield_FromStructTableNm"].Text.Trim());
                    if (dtstruct != null)
                    {
                        foreach (LibField f in dtstruct.Fields)
                        {
                            listBox.Items.Add(f.Name);
                        }
                    }
                }
                FieldCollectionForm fielsform = new FieldCollectionForm(p);
                DialogResult dialog = fielsform.ShowDialog(this);
                if (dialog == DialogResult.OK)
                {
                    this.Controls[ctrNm].Text = listBox.SelectedItem.ToString();
                    if (string.Compare(ctrNm, "fsfield_FromFieldNm") == 0)
                        this.entity.FromFieldNm = listBox.SelectedItem.ToString();
                    else 
                        this.entity .FromFieldDesc= listBox.SelectedItem.ToString();
                }
            }
            else if (string.Compare(ctrNm, "fsfield_RelateFieldNm") == 0)//关联字段
            {
                Panel p2 = new Panel();
                p2.AutoScroll = true;
                LibDataTableStruct dtstruct = null;
                List<LibDataTableStruct> list = new List<LibDataTableStruct>();
                //List<LibField> fields = new List<LibField>();
                if (!string.IsNullOrEmpty(this.Controls["fsfield_FromStructTableNm"].Text))
                {
                    if (_ds == null)
                        _ds = ModelManager.GetDataSource(this.Controls["fsfield_FromDataSource"].Text);
                    List<int> indexs =null;
                    foreach (LibDefineTable def in _ds.DefTables)
                    {
                        if (def.TableStruct == null) continue;
                        list.AddRange(def.TableStruct.ToArray());
                    }
                    LibDefineTable deftb = this._ds.DefTables.FindFirst("TableName", this.Controls["fsfield_FromDefindTableNm"].Text.Trim());
                    dtstruct = deftb.TableStruct.FindFirst("Name", this.Controls["fsfield_FromStructTableNm"].Text.Trim());
                    if (dtstruct != null)
                    {
                        listBox.SelectionMode = SelectionMode.MultiExtended;
                        foreach (LibField f in dtstruct.Fields)
                        {
                            if (this.entity .RelateFieldNm ==null ||(this.entity.RelateFieldNm != null && this.entity.RelateFieldNm.FirstOrDefault(i => i.FromTableIndex == dtstruct.TableIndex && i.FieldNm == f.Name) == null))
                            {
                                listBox.Items.Add(string.Format("{0}{2}{1}", dtstruct.Name, f.Name, SysConstManage.Point));
                            }
                        }
                        var relatetbs = list.Where(i => i.JoinTableIndex == dtstruct.TableIndex && i.TableIndex != dtstruct.TableIndex && i.Ignore).ToList();
                        while (relatetbs != null && relatetbs.Count > 0)
                        {
                            indexs = new List<int>();
                            foreach (LibDataTableStruct tb in relatetbs)
                            {
                                indexs.Add(tb.TableIndex);
                                foreach (LibField f in tb.Fields)
                                {
                                    if (this.entity .RelateFieldNm ==null ||(this.entity.RelateFieldNm != null && this.entity.RelateFieldNm.FirstOrDefault(i => i.FromTableIndex == tb.TableIndex && i.FieldNm == f.Name) == null))
                                    {
                                        listBox.Items.Add(string.Format("{0}{2}{1}", tb.Name, f.Name, SysConstManage.Point));
                                    }
                                }
                            }
                            relatetbs = list.Where(i => indexs.Contains(i.JoinTableIndex) && i.TableIndex != i.JoinTableIndex && i.Ignore).ToList();
                        }
                    }
                }
                if (this.entity.RelateFieldNm == null) this.entity.RelateFieldNm = new List<LibRelateField>();
                RelateFieldControl relateFieldControl = new RelateFieldControl(listBox, list,this.entity.RelateFieldNm);
                relateFieldControl.Dock = DockStyle.Fill;
                p2.Controls.Add(relateFieldControl);


                DialogForm dialogForm = new DialogForm(p2);

                DialogResult dialog = dialogForm.ShowDialog(this);
                //FieldCollectionForm fielsform = new FieldCollectionForm(p);
                //DialogResult dialog = fielsform.ShowDialog(this);
                if (dialog == DialogResult.OK)
                {
                    string text = string.Empty;
                    SplitContainer ctr = relateFieldControl.Controls["splitContainer1"] as SplitContainer;
                    foreach (Control c in ctr.Panel2.Controls)
                    {
                        RelateFieldProperty  prop = c as RelateFieldProperty;
                        prop.GetControlsValue();

                    }
                    foreach (LibRelateField field in this.entity.RelateFieldNm)
                    {
                        if (text.Length > 0) { text += SysConstManage.Comma; }
                        text += field.FieldNm;
                    }
                    //if (this.entity.RelateFieldNm == null) this.entity.RelateFieldNm = new List<LibRelateField>();
                    //this.entity.RelateFieldNm.Clear();
                    //LibRelateField relateField = null;
                    //string text = string.Empty;
                    //string tablenm = string.Empty;
                    //string fldnm = string.Empty;
                    //string[] array;
                    //foreach (var item in listBox.SelectedItems)
                    //{
                    //    array = item.ToString().Split(SysConstManage.Point);
                    //    tablenm = array[0];
                    //    LibDataTableStruct tableStruct = list.FirstOrDefault(i => i.Name == tablenm);
                    //    LibField field = tableStruct.Fields.FindFirst("Name", array[1]);
                    //    relateField = new LibRelateField();
                    //    relateField.ID = Guid.NewGuid().ToString();
                    //    relateField.FieldNm = field.Name;
                    //    relateField.DisplayNm = field.DisplayName;
                    //    relateField.AliasName = field.AliasName;
                    //    relateField.FieldType = field.FieldType;
                    //    relateField.FromTableIndex = tableStruct.TableIndex;
                    //    this.entity.RelateFieldNm.Add(relateField);
                    //    if (text.Length > 0) { text += SysConstManage.Comma; }
                    //    text += field.Name;
                    //}
                    this.Controls[ctrNm].Text = text;
                }

            }

        }
    }
}
