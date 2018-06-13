using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDPCRL.COM.Constant;
using System.IO;
using SDPCRL.COM;
using SDPCRL.COM.ModelManager;
using SDPCRL.CORE;
using BWYSDP.com;
using SDPCRL.CORE.FileUtils;
using System.Reflection;

namespace BWYSDP
{
    public partial class Form1 : LibFormBase
    {
        public Form1()
        {
            InitializeComponent();
            //this.tabControl1.Appearance = TabAppearance.FlatButtons;
            #region
            DBModelOperation dbmodel = new DBModelOperation();
            DataSource ds = new DataSource();
            ds.DataSourceName = "datasourcename";
            ds.DSID = 111111111;

            LibField field = new LibField();
            field.Name = "字段名称1";
            field.AliasName = "字段别名1";
            field.DisplayName = "字段显示名称1";

            LibField field2 = new LibField();
            field2.Name = "字段名称2";
            field2.AliasName = "字段别名2";
            field2.DisplayName = "字段显示名称2";


            DataTableStruct tablestruct = new DataTableStruct();
            tablestruct.Name = "数据表结构";

            tablestruct.Fields = new LibCollection<LibField>();
            tablestruct.Fields.Add(field);
            tablestruct.Fields.Add(field2);

            DefineTable deftb = new DefineTable();
            deftb.TableName = "zyy";
            deftb.ID = 123456;
            deftb.TableStruct = new LibCollection<DataTableStruct>();
            deftb.TableStruct.Add(tablestruct);
            //DefineTable deftb2 = new DefineTable();
            //deftb2.TableName = "Ylr";
            //deftb2.ID = "22222";
            //PropertyInfo[] propertis = deftb.GetType().GetProperties();
            //LibXmlAttributeAttribute attr = propertis[0].GetCustomAttributes(typeof(LibXmlAttributeAttribute), true)[0] as LibXmlAttributeAttribute;

            ds.DefTables = new LibCollection<DefineTable>();
            ds.DefTables.Add(deftb);
            //ds.DefTables.Add(deftb2);
            ds.DefTables.Guid = "................";
            SerializerUtils serial = new SerializerUtils();
            string str = serial.XMLSerialize(ds);
            //SerializerUtils.xmlserialzaition(ds, string.Format(@"{0}\Models\DataSource\{1}", Environment.CurrentDirectory, "text.xml"));
            //XMLOperation xml = new XMLOperation(string.Format(@"{0}\Models\DataSource\{1}", Environment.CurrentDirectory, "text.xml"));
            //ILibXMLNodeRead noderead = xml.NodeRead("/DataSource/DefTables/DefineTable");
            //while (!noderead.EOF)
            //{
            //   string test= noderead.InnerText;
            //   string TableName = noderead.Attributions["TableName"];
            //   noderead.ReadNext();
            //}
            //if (true) {
            //    ExceptionManager.ThrowError("sdfsd");
            //}
            //else {
            //    int a = 0;
            //    a = 88;
            //}
            #endregion
            //DAL.DBHelp.DataAccess.testsql();
            //DAL.DBHelp.DataAccess.testsql();
        }

        public void test()
        {
            DataContext tableNm = new DataContext();
            DataSource datasour = new DataSource();

        }

        public void GetDataSource()
        {

        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "addDataSource": //添加数据源
                    WakeUpForm<DSSearch>("DSSearch");
                    break;
                case "newDataSource": //新建数据源
                    WakeUpForm<DSAdd>("DSAdd", 1, 2);
                    break;
            }
        }
        protected override void DoSetParam(string tag, params object[] param)
        {
            base.DoSetParam(tag, param);
        }
        public override void DoFormAcceptMsg(string tag, Dictionary<object, object> agrs)
        {
            base.DoFormAcceptMsg(tag, agrs);
            if (string.Compare(tag, "DSInfo", true) == 0)
            {
                AddDS(agrs);
            }
        }

        /// <summary>创建数据源</summary>
        /// <param name="args"></param>
        private void AddDS(Dictionary<object, object> args)
        {
            TreeNode dsNode = null;
            if (args != null && args.Count > 0)
            {
                dsNode = new TreeNode();
                dsNode.Name = args["DSName"].ToString();
                dsNode.Text = args["DSDisplayText"].ToString();
            }
            if (dsNode != null)
            {
                this.tableStructTree.Nodes[0].Nodes.Add(dsNode);
                dsNode.Checked = true;
                int dsId = 0;
                Int32.TryParse(args["DSID"].ToString(), out dsId);
                this.tableStructTree.SelectedNode = dsNode;
                this.txtDSId.Text = dsId.ToString();
                this.txtDSName.Text = args["DSName"].ToString();
                this.txtDSdisplaytext.Text = args["DSDisplayText"].ToString();
                this.txtDSPackage.Text = args["DSPackage"].ToString();

                DataSource ds = new DataSource();
                ds.DSID = dsId;
                ds.DataSourceName = args["DSName"].ToString();
                ds.DSDisplayText = args["DSDisplayText"].ToString();
                ds.Package = args["DSPackage"].ToString();
                ModelDesignProject.CreateDS(ds);
            }
        }

        private void tableStructTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
                return;
            if (!this.tabControl1.TabPages.ContainsKey(e.Node.Name))
            {
                int dsId = 0;
                Int32.TryParse(this.txtDSId.Text.ToString(), out dsId);
                TabPage page = new TabPage();
                page.Name = e.Node.Name;
                page.Text = e.Node.Text;
                DefineTableControl defineTable = new DefineTableControl(dsId);
                defineTable.Dock = DockStyle.Fill;
                page.Controls.Add(defineTable);
                this.tabControl1.SelectedTab = page;
                this.tabControl1.TabPages.Add(page);
            }
            else
            {
                this.tabControl1.SelectedTab = this.tabControl1.TabPages[e.Node.Name];
            }
        }

        private void tableStructTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if (e.Node.Level != 0)
            //{
            //    DataSource dataSource = ModelDesignProject.GetDataSource(e.Node.Name.Trim());
            //    SetValue(dataSource);
            //}
            nodeSelected(e.Node);
        }

        private void nodeSelected(TreeNode node)
        {
            if (node.Level != 0)
            {
                DataSource dataSource = ModelDesignProject.GetDataSource(node.Name.Trim());
                SetValue(dataSource);
            }
        }

        private void toolbtSave_Click(object sender, EventArgs e)
        {
            ModelDesignProject.DoSaveDS(ModelDesignProject.GetDataSource(this.tabControl1.SelectedTab.Name).DSID);
            MessageBox.Show("保存成功");
        }

        /// <summary>初始化树结构</summary>
        private void InitialDefineTable()
        {
            List<DataSource> dsList = ModelDesignProject.GetDataSourceList();
            TreeNode dsNode = null;
            foreach (DataSource item in dsList)
            {
                dsNode = new TreeNode();
                dsNode.Name = item.DataSourceName;
                dsNode.Text = item.DSDisplayText;
                this.tableStructTree.Nodes[0].Nodes.Add(dsNode);
            }
        }

        private void SetValue(DataSource ds)
        {
            this.txtDSId.Text = ds.DSID.ToString();
            this.txtDSName.Text = ds.DataSourceName;
            this.txtDSdisplaytext.Text = ds.DSDisplayText;
            this.txtDSPackage.Text = ds.Package;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialDefineTable();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            foreach (TreeNode node in tableStructTree.Nodes["dataSourcetree"].Nodes)
            {
                if (e.TabPage != null && string.Compare(node.Name, e.TabPage.Name, true) == 0)
                {
                    tableStructTree.SelectedNode = node;
                    nodeSelected(node);
                }
            }
        }

        private void toolbtCreateTableObj_Click(object sender, EventArgs e)
        {
            ModelDesignProject.CreateTableObj(ModelDesignProject.GetDataSource(this.tabControl1.SelectedTab.Name));
            
        }

    }
}
