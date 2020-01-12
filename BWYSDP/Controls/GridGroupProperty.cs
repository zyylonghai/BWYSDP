using BWYSDP.com;
using SDPCRL.COM.ModelManager.FormTemplate;
using SDPCRL.CORE;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BWYSDP.Controls
{
    public partial class GridGroupProperty : BaseUserControl<LibGridGroup>
    {
        private LibTreeNode _Node;
        public GridGroupProperty()
        {
            InitializeComponent();
            InitializeControls();
        }
        public GridGroupProperty(string name)
            :this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibGridGroup entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            _Node = node;
        }

        public override void TextAndBotton_Click(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            string ctrNm = ctl.Name.Replace(SysConstManage.BtnCtrlNmPrefix, "");
            Panel p = new Panel();
            p.AutoScroll = true;
            if (string.Compare(ctrNm, "grid_GdButtons") == 0) //自定义按钮
            {
                p.Name = "gridbuttonitems";
                Panel p2 = new Panel();
                p2.Name = "btnpanel";
                p2.Dock = DockStyle.Top;
                p2.Height = 50;
                Button addbtn = new Button();
                addbtn.Name = "_addbutton";
                addbtn.Width = 70;
                addbtn.Height = 25;
                addbtn.Location = new System.Drawing.Point(20, 15);
                addbtn.Text = "添加项";
                addbtn.Click += Addbtn_Click;
                p2.Controls.Add(addbtn);

                Button deletbtn = new Button();
                deletbtn.Name = "deletbutton";
                deletbtn.Width = 70;
                deletbtn.Height = 25;
                deletbtn.Location = new System.Drawing.Point(110, 15);
                deletbtn.Text = "删除项";
                p2.Controls.Add(deletbtn);

                ListBox listBox = new ListBox();
                listBox.Name = "_listbox";
                listBox.Dock = DockStyle.Left;
                listBox.Width = 200;
                listBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;




                GridButtonProperty keyValueProperty = new GridButtonProperty();
                keyValueProperty.Name = "_gridbuttonProperty";
                keyValueProperty.Dock = DockStyle.Fill;
                p.Controls.Add(keyValueProperty);

                p.Controls.Add(keyValueProperty);
                p.Controls.Add(listBox);
                p.Controls.Add(p2);

                if (this.entity.GdButtons != null)
                {
                    foreach (LibGridButton gridbtn in this.entity.GdButtons)
                    {
                        listBox.Items.Add(gridbtn);
                    }

                }

                DialogForm dialogForm = new DialogForm(p);
                dialogForm.Size = new Size(700, 488);


                DialogResult dialog = dialogForm.ShowDialog(this);
                if (dialog == DialogResult.OK)
                {
                    if (this.entity.GdButtons == null) this.entity.GdButtons = new LibCollection<LibGridButton>();
                    this.entity.GdButtons.RemoveAll();
                    foreach (LibGridButton item in listBox.Items)
                    {
                        //if (this.entity.Items.FindFirst("Key", item.Key) == null)
                        //{
                        this.entity.GdButtons.Add(item);
                        //}
                    }
                    #region 控件赋值
                    this.Controls[ctrNm].Text = string.Empty;
                    foreach (LibGridButton keyval in this.entity.GdButtons)
                    {
                        if (this.Controls[ctrNm].Text.Length != 0)
                        {
                            this.Controls[ctrNm].Text += ";";
                        }
                        this.Controls[ctrNm].Text += keyval.ToString();
                    }

                    #endregion
                }
            }
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            Control container = ctl.Parent.Parent;
            ListBox box = container.Controls["_listbox"] as ListBox;
            LibGridButton gridbtn = new LibGridButton();
            gridbtn .GridButtonID= Guid.NewGuid().ToString();
            gridbtn.GridButtonName = string.Format("GridButton{0}", box.Items.Count + 1);
            gridbtn .GridButtonDisplayNm = string.Format("GridButton{0}", box.Items.Count + 1);
            //keyValue.ID = Guid.NewGuid().ToString();
            //keyValue.Key = string.Format("itemkey{0}", box.Items.Count + 1);
            //keyValue.Value = string.Format("itemvalue{0}", box.Items.Count + 1);

            box.Items.Add(gridbtn);


            GridButtonProperty gridbtnProperty = container.Controls["_gridbuttonProperty"] as GridButtonProperty;
            gridbtnProperty.SetPropertyValue(gridbtn, null);


        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = sender as ListBox;
            Control container = listBox.Parent;
            GridButtonProperty gridbtnProperty = container.Controls["_gridbuttonProperty"] as GridButtonProperty;

            LibGridButton gridbtn = listBox.Items[listBox.SelectedIndex] as LibGridButton;
            gridbtnProperty.SetPropertyValue(gridbtn, null);
            //if (!string.IsNullOrEmpty(gridbtn.FromkeyValueID))
            //{
            //    keyValueProperty.Enabled = false;
            //}
            //else
            //{
            //    keyValueProperty.Enabled = true;
            //}
        }
    }
}
