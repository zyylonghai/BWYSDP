using SDPCRL.COM.ModelManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BWYSDP.com
{
    public  class DSUtiles
    {
        public static void GetAllFieldsByDS(LibDataSource ds, TreeView tree,List<ExistField> existfields)
        {
            #region 收集数据源字段
            foreach (LibDefineTable deftb in ds.DefTables)
            {
                LibTreeNode deftbnode = new LibTreeNode();
                deftbnode.Name = deftb.TableName;
                deftbnode.Text = string.Format("{0}({1})", deftb.DisplayName, deftb.TableName);
                tree.Nodes.Add(deftbnode);
                LibTreeNode _node;
                if (deftb.TableStruct != null)
                {
                    foreach (LibDataTableStruct dtstruct in deftb.TableStruct)
                    {
                        LibTreeNode dtstructnode = new LibTreeNode();
                        dtstructnode.Name = dtstruct.Name;
                        dtstructnode.Text = string.Format("{0}({1})", dtstruct.DisplayName, dtstruct.Name);
                        dtstructnode.NodeId = dtstruct.TableIndex.ToString();
                        deftbnode.Nodes.Add(dtstructnode);
                        if (dtstruct.Fields != null)
                        {
                            foreach (LibField fld in dtstruct.Fields)
                            {
                                if (!fld.IsActive) continue;
                                _node = new LibTreeNode();
                                _node.Name = fld.Name;
                                _node.Text = fld.DisplayName;
                                _node.Tag = true;
                                _node.Checked = existfields.FirstOrDefault(i => i.Name == fld.Name && i.FromTableNm == dtstruct.Name) != null;
                                dtstructnode.Nodes.Add(_node);
                                if (fld.SourceField != null && fld.SourceField.Count > 0)
                                {
                                    foreach (LibFromSourceField fromfld in fld.SourceField)
                                    {
                                        if (fromfld.RelateFieldNm != null && fromfld.RelateFieldNm.Count > 0)
                                        {
                                            foreach (LibRelateField relateField in fromfld.RelateFieldNm)
                                            {
                                                if (relateField != null)
                                                {
                                                    _node = new LibTreeNode();
                                                    _node.Name = string.IsNullOrEmpty(relateField.AliasName) ? relateField.FieldNm : relateField.AliasName;
                                                    _node.Text = relateField.DisplayNm;
                                                    _node.Tag = false;
                                                    _node.Checked = existfields.FirstOrDefault(i => i.Name == _node.Name && i.FromTableNm == dtstruct.Name) != null;
                                                    dtstructnode.Nodes.Add(_node);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }
    }

    public class ExistField
    {
        public string Name { get; set; }
        public string FromTableNm { get; set; }
    }
}
