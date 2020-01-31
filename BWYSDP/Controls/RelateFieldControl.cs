using BWYSDP.com;
using SDPCRL.COM.ModelManager;
using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BWYSDP.Controls
{
    public partial class RelateFieldControl : UserControl
    {
        List<LibDataTableStruct> _tableStructs = null;
        List<LibRelateField> _relateFields = null;
        List<RelateFieldProperty> _rfieldPropertylist = null;
        public RelateFieldControl()
        {
            InitializeComponent();
            _rfieldPropertylist = new List<RelateFieldProperty>();
        }

        public RelateFieldControl(ListBox box,List<LibDataTableStruct> tableStructs, List<LibRelateField> relateFields)
            :this ()
        {
            this._tableStructs = tableStructs;
            this._relateFields = relateFields;
            //foreach (var item in box.Items)
            //{
                
            //    this.listBox1.Items.Add(item);
            //}
            this.listBox1.Items.AddRange(box.Items);
            if (_relateFields != null)
            {
                foreach (LibRelateField rField in _relateFields)
                {
                   var tb= tableStructs.FirstOrDefault(i => i.TableIndex == rField.FromTableIndex);
                    this.listBox2.Items.Add(string.Format("{0}{2}{1}", tb.Name, rField.FieldNm , SysConstManage.Point));
                }
            }

        }

        /// <summary>
        /// 选入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            LibRelateField relateField = null;
            string text = string.Empty;
            string tablenm = string.Empty;
            string fldnm = string.Empty;
            string[] array;
            foreach (var item in this.listBox1.SelectedItems)
            {
                this.listBox2.Items.Add(item);
                array = item.ToString().Split(SysConstManage.Point);
                tablenm = array[0];
                LibDataTableStruct tableStruct = _tableStructs.FirstOrDefault(i => i.Name == tablenm);
                LibField field = tableStruct.Fields.FindFirst("Name", array[1]);
                relateField = new LibRelateField();
                relateField.ID = Guid.NewGuid().ToString();
                relateField.FieldNm = field.Name;
                relateField.DisplayNm = field.DisplayName;
                relateField.AliasName = field.AliasName;
                relateField.FieldType = field.FieldType;
                relateField.FromTableIndex = tableStruct.TableIndex;
                if (_relateFields.FirstOrDefault(i => i.FromTableIndex == tableStruct.TableIndex && i.FieldNm == field.Name) == null)
                    _relateFields.Add(relateField);
                RelateFieldProperty property = (RelateFieldProperty)this.splitContainer1.Panel2.Controls[relateField.ID];
                if (property == null)
                {
                    property = new RelateFieldProperty(relateField.ID);
                    property.Dock = DockStyle.Fill;
                    property.SetPropertyValue(relateField, null);
                    _rfieldPropertylist.Add(property);
                    this.splitContainer1.Panel2.Controls.Add(property);

                }
            }
            for (int i = 0; i < this.listBox2.Items.Count; i++)
            {
                this.listBox1.Items.Remove(this.listBox2.Items[i]);
            }
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add(this.listBox2.SelectedItem);
            string[] array = this.listBox2.SelectedItem.ToString().Split(SysConstManage.Point);
            string tbnm = array[0];
            string fldnm = array[1];
            var table = _tableStructs.FirstOrDefault(i => i.Name == tbnm);
            if (table != null)
            {
                var o = _relateFields.FirstOrDefault(i => i.FromTableIndex == table.TableIndex && i.FieldNm == fldnm);
                if (o != null)
                {
                    this.splitContainer1.Panel2.Controls.Remove(this.splitContainer1.Panel2.Controls[o.ID]);
                    _relateFields.Remove(o);
                }
            }

            this.listBox2.Items.Remove(this.listBox2.SelectedItem);

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox2.SelectedItem == null) return;
            string[] array = this.listBox2.SelectedItem.ToString().Split(SysConstManage.Point);
            string tbnm = array[0];
            string fldnm = array[1];
            var table = _tableStructs.FirstOrDefault(i => i.Name == tbnm);
            if (table != null)
            {
                var o = _relateFields.FirstOrDefault(i => i.FromTableIndex == table.TableIndex && i.FieldNm == fldnm);
                if (o != null)
                {
                    RelateFieldProperty property =(RelateFieldProperty)this.splitContainer1.Panel2.Controls[o.ID];
                   //var exist= _rfieldPropertylist.FirstOrDefault(i => i.Name == o.ID);
                    if (property == null)
                    {
                        property = new RelateFieldProperty(o.ID);
                        property.Dock = DockStyle.Fill;
                        property.SetPropertyValue(o, null);
                        _rfieldPropertylist.Add(property);
                        this.splitContainer1.Panel2.Controls.Add(property);

                    }
                    ModelDesignProject.SetControlVisible((Control)property, this.splitContainer1.Panel2.Controls);
                }
            }
        }
    }
}
