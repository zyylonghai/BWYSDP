using BWYSDP.com;
using SDPCRL.COM.ModelManager;
using SDPCRL.CORE;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BWYSDP.Controls
{
    public partial class DefFieldProperty : BaseUserControl<LibField >
    {
        //private LibField _field;
        private LibTreeNode _fieldNode;
        
        public DefFieldProperty()
        {
            InitializeComponent();
            InitializeControls();
            //this.fd_txtFieldName.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            //this.fd_combFieldType.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            //this.fd_txtDisplayText.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            //this.fd_txtAliasName.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            //this.fd_combAllowNull.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            //this.fd_combActive.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
        }

        public DefFieldProperty(string name)
            :this()
        {
            this.Name = name;
        }
        public override void SetPropertyValue(LibField entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            if (entity.SysField)
            {
                this.Controls["fd_txtFieldName"].Enabled = false;
            }
            //this.entity = entity;
            this._fieldNode = node;
        }

        //public override void SetPropertyValue(TEntity entity, LibTreeNode node)
        //{
        //    base.SetPropertyValue(entity, node);
        //    this._field = entity as LibField;
        //    this._fieldNode = node;
        //}
        //private void InitializeControls()
        //{
        //    ModelDesignProject.InternalBindControls<LibField>(this);
        //}

        //void fd_PropertyTextBox_LostFocus(object sender, EventArgs e)
        //{
        //    SetPropertyValue(sender);
        //}

        ///// <summary>设置属性值</summary>
        ///// <param name="Field"></param>
        ///// <param name="node"></param>
        //public void SetPropertyValue(LibField field, LibTreeNode node)
        //{
        //    this._field = field;
        //    this._fieldNode = node;
        //    ModelDesignProject.DoSetPropertyValue<LibField>(this.Controls, field);

        //}


        #region 临时注释
        //public void GetControlsValue()
        //{
        //    ModelDesignProject.DoGetControlsValue(this.Controls, _field);
        //}

        private void DefFieldProperty_Load(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                if (string.Compare("fd_txtFieldName", item.Name) == 0 || string.Compare("fd_txtAliasName", item.Name) == 0)//字段名称控件
                {
                    item.KeyUp += new KeyEventHandler(item_KeyUp);
                }
            }
        }

        void item_KeyUp(object sender, KeyEventArgs e)
        {
            Control ctl = sender as Control;
            if (e.KeyCode == Keys.Enter)
            {
                foreach (LibTreeNode item in this._fieldNode.Parent.Nodes)
                {
                    if (item.NodeId!=this.entity .ID && string.Compare(ctl.Text.Trim(), item.Name, true) == 0)
                    {
                        ExceptionManager.ThrowError("不能有重复字段名");
                        //throw new LibExceptionBase("不能有重复字段名");
                    }
                }
                this._fieldNode.Name = string.Compare("fd_txtFieldName", ctl.Name) == 0 ? ctl.Text.Trim() : this._fieldNode.Name;
                //this._field.Name = string.Compare("fd_txtFieldName", ctl.Name) == 0 ? ctl.Text.Trim() : this._field.Name;
                //this._fieldNode.Text = string.Format("{0}({1})", _field.Name, string.Compare("fd_txtFieldName", ctl.Name) == 0 ? _field.DisplayName : ctl.Text);
                this.entity.Name = string.Compare("fd_txtFieldName", ctl.Name) == 0 ? ctl.Text.Trim() : this.entity.Name;
                this._fieldNode.Text = string.Format("{0}({1})", entity.Name, string.Compare("fd_txtFieldName", ctl.Name) == 0 ? entity.DisplayName : ctl.Text);
            }
        }

        public override void TextAndBotton_Click(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            string ctrNm = ctl.Name.Replace(SysConstManage.BtnCtrlNmPrefix, "");
            Panel p = new Panel();
            p.AutoScroll = true;
            if (string.Compare(ctrNm, "fd_SourceField") == 0)//来源字段集
            {
                p.Name = "fromsourceProperty";

                FromSourceControl fromSourceControl = null;

                if (this.entity.SourceField == null)
                {
                    this.entity.SourceField = new LibCollection<LibFromSourceField>();
                    fromSourceControl = new FromSourceControl();
                }
                else
                    fromSourceControl = new FromSourceControl(this.entity.SourceField.ToArray());
                fromSourceControl.Dock = DockStyle.Fill;
                p.Controls.Add(fromSourceControl);


                DialogForm dialogForm = new DialogForm(p);

                DialogResult dialog = dialogForm.ShowDialog(this);
                if (dialog == DialogResult.OK)
                {
                    SplitContainer ctr = fromSourceControl.Controls["splitContainer1"] as SplitContainer;
                    foreach (Control c in ctr.Panel2.Controls)
                    {
                        FromSourceProperty prop = c as FromSourceProperty;
                        prop.GetControlsValue();
                        
                    }
                    ListBox box = ctr.Panel1.Controls["listBox1"] as ListBox;
                    foreach (LibFromSourceField sfield in box.Items)
                    {
                        if (this.entity.SourceField.FindFirst("ID", sfield.ID) == null)
                            this.entity.SourceField.Add(sfield);
                    }
                }
                #region 控件赋值
                this.Controls[ctrNm].Text = string.Empty;
                foreach (LibFromSourceField item in this.entity.SourceField)
                {
                    if (!string.IsNullOrEmpty(this.Controls[ctrNm].Text))
                        this.Controls[ctrNm].Text += SysConstManage.Comma;
                    this.Controls[ctrNm].Text += item.ToString();
                }
                //this.Controls[ctrNm].Text = this.entity.SourceField.ToString();
                #endregion
            }
            else if (string.Compare(ctrNm, "fd_Items") == 0)//键值对集
            {
                p.Name = "keyvalueitems";
                Panel p2 = new Panel();
                p2.Name = "btnpanel";
                p2.Dock = DockStyle.Top;
                p2.Height = 50;
                Button addbtn = new Button();
                addbtn.Name = "_addkeyvalu";
                addbtn.Width = 70;
                addbtn.Height = 25;
                addbtn .Location= new System.Drawing.Point(20, 15);
                addbtn.Text = "添加项";
                addbtn.Click += Addbtn_Click;
                p2.Controls.Add(addbtn);

                Button deletbtn = new Button();
                deletbtn.Name = "deletkeyvalu";
                deletbtn.Width = 70;
                deletbtn.Height = 25;
                deletbtn.Location = new System.Drawing.Point(110, 15);
                deletbtn.Text = "删除项";
                p2.Controls.Add(deletbtn);

                Button frombtn = new Button();
                frombtn.Name = "fromkeyvaluid";
                frombtn.Width = 70;
                frombtn.Height = 25;
                frombtn.Location = new System.Drawing.Point(200, 15);
                frombtn.Text = "来源字典模型";
                frombtn.Click += Frombtn_Click;
                p2.Controls.Add(frombtn);

                ListBox listBox = new ListBox();
                listBox.Name = "_listbox";
                listBox.Dock = DockStyle.Left;
                listBox.Width = 200;
                listBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;




                KeyValueProperty keyValueProperty = new KeyValueProperty();
                keyValueProperty.Name = "_keyvalueProperty";
                keyValueProperty.Dock = DockStyle.Fill;
                p.Controls.Add(keyValueProperty);
                //Panel p3 = new Panel();
                //p3.Name = "keyvalueContains";
                //p3.Dock = DockStyle.Fill;

                p.Controls.Add(keyValueProperty);
                p.Controls.Add(listBox);
                p.Controls.Add(p2);

                if (this.entity.Items != null)
                {
                    foreach (LibKeyValue keyvalue in this.entity.Items)
                    {
                        listBox.Items.Add(keyvalue);
                    }

                }

                DialogForm dialogForm = new DialogForm(p);
                dialogForm.Size = new Size(700, 488);


                DialogResult dialog = dialogForm.ShowDialog(this);
                if (dialog == DialogResult.OK)
                {
                    if (this.entity.Items == null) this.entity.Items = new LibCollection<LibKeyValue>();
                    this.entity.Items.RemoveAll();
                    foreach (LibKeyValue item in listBox.Items)
                    {
                        //if (this.entity.Items.FindFirst("Key", item.Key) == null)
                        //{
                            this.entity.Items.Add(item);
                        //}
                    }
                    #region 控件赋值
                    this.Controls[ctrNm].Text = string.Empty;
                    foreach (LibKeyValue keyval in this.entity.Items)
                    {
                        if (this.Controls[ctrNm].Text.Length != 0)
                        {
                            this.Controls[ctrNm].Text += ";";
                        }
                        this.Controls[ctrNm].Text +=keyval.ToString();
                    }

                    #endregion
                }
            }
        }

        #region 键值对设计相关

        /// <summary>键值对窗体中的添加按钮事件</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Addbtn_Click(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            Control container = ctl.Parent.Parent;
            ListBox box = container.Controls["_listbox"] as ListBox;
            LibKeyValue keyValue = new LibKeyValue();
            keyValue.ID = Guid.NewGuid().ToString();
            keyValue.Key = string.Format("itemkey{0}", box.Items.Count + 1);
            keyValue.Value = string.Format("itemvalue{0}", box.Items.Count + 1);

            box.Items.Add(keyValue);

            //KeyValueProperty keyValueProperty = new KeyValueProperty();
            //keyValueProperty.Dock = DockStyle.Fill;
            KeyValueProperty keyValueProperty = container.Controls["_keyvalueProperty"] as KeyValueProperty;
            keyValueProperty.SetPropertyValue(keyValue, null);

            //container.Controls["keyvalueContains"].Controls.Add(keyValueProperty);
            
        }

        private void Frombtn_Click(object sender, EventArgs e)
        {
            string[] allkeyvalues = ModelManager.GetAllKeyValuesNm(string.Empty);
            Panel p = new Panel();
            p.Dock = DockStyle.Fill;
            p.Name = "pkeyvaluescollection";
            p.AutoScroll = true;
            ListBox listBox = new ListBox();
            listBox.Dock = DockStyle.Fill;
            listBox.Name = "keyvaluelist";
            if (allkeyvalues != null)
            {
                foreach (string kv in allkeyvalues)
                {
                    listBox.Items.Add(kv);
                }
            }
            p.Controls.Add(listBox);

            FieldCollectionForm fielsform = new FieldCollectionForm(p);
            DialogResult dialog = fielsform.ShowDialog(this);
            if (dialog == DialogResult.OK)
            {
                string nm = listBox.SelectedItem.ToString();
                LibKeyValueCollection obj = ModelManager.GetKeyValues(nm);

                Control ctl = sender as Control;
                Control container = ctl.Parent.Parent;
                ListBox box = container.Controls["_listbox"] as ListBox;
                if (obj != null && obj.KeyValues != null)
                {
                    box.Items.Clear();
                    foreach (LibKeyValue item in obj.KeyValues)
                    {
                        item.FromkeyValueID = nm;
                        box.Items.Add(item);
                    }
                }
            }
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = sender as ListBox;
            Control container = listBox.Parent; 
            KeyValueProperty keyValueProperty = container.Controls["_keyvalueProperty"] as KeyValueProperty;
            
            LibKeyValue keyValue = listBox.Items[listBox.SelectedIndex] as LibKeyValue;
            keyValueProperty.SetPropertyValue(keyValue, null);
            if (!string.IsNullOrEmpty(keyValue.FromkeyValueID))
            {
                keyValueProperty.Enabled = false;
            }
            else
            {
                keyValueProperty.Enabled = true;
            }
        }

        #endregion

        #endregion
        #region 旧代码
        ///// <summary>用于控件失去焦点后，进行对应对象的赋值</summary>
        ///// <param name="valueObj"></param>
        //private void SetPropertyValue(object valueObj)
        //{
        //    string propertyName = string.Empty;
        //    object propertyValue = null;
        //    switch (valueObj.GetType().Name)
        //    {
        //        case "TextBox":
        //            TextBox obj = (TextBox)valueObj;
        //            propertyName = obj.Name;
        //            propertyValue = obj.Text;
        //            break;
        //        case "ComboBox":
        //            ComboBox combobj = (ComboBox)valueObj;
        //            propertyName = combobj.Name;
        //            if (string.Compare(propertyName, "fd_combFieldType", true) == 0)//字段类型
        //            {
        //                propertyValue = EnumOperation.GetFieldTypeValue(combobj.Text);
        //            }
        //            else if (string.Compare(propertyName, "fd_combAllowNull", true) == 0 ||
        //                     string.Compare(propertyName, "fd_combActive", true) == 0)//允许为null,是否虚字段
        //            {
        //                propertyValue = LibSysUtils.ToBooLean(combobj.Text);
        //            }
        //            break;
        //    }
        //    if (_field != null)
        //    {
        //        PropertyInfo[] propertis = _field.GetType().GetProperties();
        //        foreach (PropertyInfo p in propertis)
        //        {
        //            object[] attrArray = p.GetCustomAttributes(typeof(LibXmlAttributeAttribute), true);
        //            if (attrArray.Length > 0)
        //            {
        //                LibXmlAttributeAttribute attr = attrArray[0] as LibXmlAttributeAttribute;
        //                if (string.Compare(attr.ControlNm, propertyName, false) == 0)
        //                {
        //                    p.SetValue(_field, propertyValue, null);
        //                    if (string.Compare(propertyName, "fd_txtFieldName", true) == 0)
        //                        _fieldNode.Name = propertyValue.ToString();//更新数节点的名称
        //                    else if (string.Compare(propertyName, "fd_txtDisplayText", true) == 0)
        //                        _fieldNode.Text = propertyValue.ToString(); //更新数节点的显示名称
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion
    }
}
