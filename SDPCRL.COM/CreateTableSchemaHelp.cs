using SDPCRL.COM.ModelManager;
using SDPCRL.CORE.FileUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    public class CreateTableSchemaHelp
    {
        private string _root;
        public CreateTableSchemaHelp(string modelpath)
        {
            _root = modelpath;
        }

        public LibTable[] CreateTableSchema(string dsid,string package)
        {
            List<LibTable> dts = new List<LibTable>();
            LibTable dftb = null;
            DataTable dt = null;
            DataColumn col = null;
            int index = 0;
            FileOperation fileoperation = new FileOperation();
            LibDataSource data = ModelManager.ModelManager.GetModelBypath<LibDataSource>(_root, dsid, package);
            if (data != null)
            {
                if (data.DefTables == null) return null;
                foreach (LibDefineTable deftb in data.DefTables)
                {
                    if (deftb.TableStruct == null) continue;
                    dftb = new LibTable(deftb.TableName);
                    dftb.Tables = new DataTable[deftb.TableStruct.Count];
                    index = 0;
                    foreach (LibDataTableStruct tb in deftb.TableStruct)
                    {
                        if (tb.Fields == null) continue;
                        
                        dt = new DataTable(tb.Name);
                        dftb.Tables[index] = dt;
                        foreach (LibField f in tb.Fields)
                        {
                            col = new DataColumn(f.Name);
                            switch (f.FieldType)
                            {
                                case LibFieldType.Byte:
                                    col.DataType = typeof(byte);
                                    break;
                                case LibFieldType.Date:
                                    col.DataType = typeof(Date);
                                    break;
                                case LibFieldType.DateTime:
                                    col.DataType = typeof(DateTime);
                                    break;
                                case LibFieldType.Decimal:
                                    col.DataType = typeof(decimal);
                                    break;
                                case LibFieldType.Interger:
                                    col.DataType = typeof(Int32);
                                    break;
                                case LibFieldType.Long:
                                    col.DataType = typeof(long);
                                    break;
                                case LibFieldType.String:
                                    col.DataType = typeof(string);
                                    break;
                            }
                            dt.Columns.Add(col);
                        }
                        index++;
                    }
                    dts.Add(dftb);
                }
            }
            return dts.ToArray();
        }
    }

    public class Date
    {
        string value;
        public Date()
        {

        }
        public Date(string date)
        {
            value = date;
        }

        //public Date CurrentDate { get { return new Date { value = DateTime.Now.ToString("yyyy-MM-dd") }; } }

        public override string ToString()
        {
            return value;
        }
    }

    [Serializable]
    public class LibTable
    {
        /// <summary>
        /// 自定义表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 来源数据库表集
        /// </summary>
        public DataTable[] Tables { get; set; }

        public LibTable(string name)
        {
            this.Name = name;
        }
    }
}
