using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWYSDP.com;
using SDPCRL.COM.ModelManager;

namespace BWYSDP.Controls
{
    public partial class KeyValuesControl : UserControl
    {
        LibKeyValueCollection _keyvalus = null;
        private LibTreeNode _funNode=null;
        public KeyValuesControl()
        {
            InitializeComponent();
        }
        public KeyValuesControl(LibTreeNode funcNode)
            :this()
        {
            this._funNode = funcNode;
            KeyValueProperty keyValueProperty = new KeyValueProperty();
            keyValueProperty.Name = "_keyvalueProperty";
            keyValueProperty.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(keyValueProperty);
        }
        /// <summary>
        /// 新增项事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            LibKeyValue keyValue = new LibKeyValue();
            keyValue.ID = Guid.NewGuid().ToString();
            keyValue.Key = string.Format("itemkey{0}", this.listBox1.Items.Count + 1);
            keyValue.Value = string.Format("itemvalue{0}", this.listBox1.Items.Count + 1);

            this.listBox1.Items.Add(keyValue);
            this.listBox1.SelectedItem = keyValue;
            KeyValueProperty keyValueProperty = this.panel3.Controls["_keyvalueProperty"] as KeyValueProperty;
            keyValueProperty.SetPropertyValue(keyValue, null);
        }
        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = sender as ListBox;
            KeyValueProperty keyValueProperty = this.panel3.Controls["_keyvalueProperty"] as KeyValueProperty;

            LibKeyValue keyValue = listBox.Items[listBox.SelectedIndex] as LibKeyValue;
            keyValueProperty.SetPropertyValue(keyValue, null);
        }

        public void GetControlValueBindToKeyValue()
        {
            if (this._keyvalus.KeyValues == null) this._keyvalus.KeyValues = new SDPCRL.CORE.LibCollection<LibKeyValue>();
            LibKeyValue exist = null;
            foreach (LibKeyValue item in this.listBox1.Items)
            {
                exist= this._keyvalus.KeyValues.FindFirst("Key", item.Key);
                if (exist == null)
                    this._keyvalus.KeyValues.Add(item);
            }
        }

        private void KeyValuesControl_Load(object sender, EventArgs e)
        {
            this._keyvalus = ModelDesignProject.GetKeyvaluesByid(this._funNode.Name);
            this._keyvalus.Package = this._funNode.Package;
            if (this._keyvalus != null)
            {
                if (this._keyvalus.KeyValues != null)
                {
                    foreach (LibKeyValue item in this._keyvalus.KeyValues)
                    {
                        this.listBox1.Items.Add(item);
                    }
                }
            }
        }
    }
}
