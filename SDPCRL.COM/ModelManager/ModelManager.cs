using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.CORE;
using SDPCRL.CORE.FileUtils;

namespace SDPCRL.COM.ModelManager
{
    public class ModelManager
    {
        /// <summary>枚举，查找数据源的依据</summary>
        private enum Mode
        {
            /// <summary>根据DSID</summary>
            ByDSID = 0,
            /// <summary>根据DSName </summary>
            ByDSName = 1
        }

        #region 公开函数

        public static DataSource DoGetDataSource(int dsId)
        {
            return DoGetDataSourceEx(dsId, Mode.ByDSID);
        }

        public static DataSource DoGetDataSource(string dsName)
        {
            return DoGetDataSourceEx(dsName, Mode.ByDSName);
        }
        public static bool DoIsExistId(int dsId)
        {
            return DoIsExistIdEx(dsId, Mode.ByDSID);
        }
        public static bool DoIsExistDSName(string dsName)
        {
            return DoIsExistIdEx(dsName, Mode.ByDSName);
        }

        public static List<DataSource> DoGetDataSourceListEx(ref DSList dsinfolist)
        {
            return DoGetDataSourceList(ref dsinfolist);
        }

        #endregion

        #region 私有函数

        private static DataSource DoGetDataSourceEx(object dsNmOrId, Mode mode)
        {
            DataSource datasource = null;
            XMLOperation xmlOperation = new XMLOperation(SysConstManage.DSListFile);
            ILibXMLNodeRead noderead = xmlOperation.NodeRead("/DSList/DSInfoCollection/DSInfo");
            string dsname = null;
            string package = null;
            while (!noderead.EOF)
            {
                if (mode == Mode.ByDSID)
                {
                    int id = LibSysUtils.ToInt32(noderead.Attributions["DSID"].ToString());
                    if (LibSysUtils.ToInt32(dsNmOrId) == id)
                    {
                        dsname = noderead.Attributions["Name"].ToString();
                        package = noderead.Attributions["PACKAGE"].ToString();
                        break;
                    }
                }
                else if (mode == Mode.ByDSName)
                {
                    dsname = noderead.Attributions["Name"].ToString();
                    if (string.Compare(dsname, dsNmOrId.ToString(), true) == 0)
                    {
                        package = noderead.Attributions["PACKAGE"].ToString().Trim();
                        break;
                    }
                }
                noderead.ReadNext();
            }
            InstanceDataSource(ref datasource, dsname, package);
            return datasource;
        }
        private static void InstanceDataSource(ref DataSource datasource, string dsname, string package)
        {
            FileOperation fileOperation = new FileOperation();
            fileOperation.FilePath = string.Format(@"{0}\{1}\{2}.xml", SysConstManage.DSFileRootPath, package, dsname);
            fileOperation.Encoding = LibEncoding.UTF8;
            string dsXML = fileOperation.ReadFile();
            datasource = SerializerUtils.XMLDeSerialize<DataSource>(dsXML);
        }

        private static bool DoIsExistIdEx(object dsNmOrId, Mode mode)
        {
            XMLOperation xmlOperation = new XMLOperation(SysConstManage.DSListFile);
            ILibXMLNodeRead noderead = xmlOperation.NodeRead("/DSList/DSInfoCollection/DSInfo");
            while (!noderead.EOF)
            {
                if (mode == Mode.ByDSID)
                {
                    int id = LibSysUtils.ToInt32(noderead.Attributions["DSID"].ToString());
                    if (id == LibSysUtils.ToInt32(dsNmOrId))
                    {
                        return true;
                    }
                }
                else if (mode == Mode.ByDSName)
                {
                    string dsName = noderead.InnerText.Trim();
                    if (string.Compare(dsNmOrId.ToString(), dsName, true) == 0)
                    {
                        return true;
                    }
                }
                noderead.ReadNext();
            }
            return false;
        }
        /// <summary>获取数据源列表 </summary>
        private static List<DataSource> DoGetDataSourceList(ref DSList dsinfolist)
        {
            List<DataSource> dsList = new List<DataSource>();
            FileOperation fileOperation = new FileOperation();
            fileOperation.FilePath = SysConstManage.DSListFile;
            fileOperation.Encoding = LibEncoding.UTF8;
            string dsListXML = fileOperation.ReadFile();
            dsinfolist = SerializerUtils.XMLDeSerialize<DSList>(dsListXML);
            if (dsinfolist.DSInfoCollection != null&&dsinfolist.DSInfoCollection .Count >0)
            {
                DataSource ds = null;
                foreach (DSInfo info in dsinfolist.DSInfoCollection)
                {
                    ds = new DataSource();
                    ds.DSID = info.DSID;
                    ds.DataSourceName = info.Name;
                    ds.DSDisplayText = info.DISPLAYTEXT;
                    ds.Package = info.PACKAGE;
                    dsList.Add(ds);
                }
            }
            #region 旧的做法
            //XMLOperation xmlOperation = new XMLOperation(SysConstManage.DSListFile);
            //ILibXMLNodeRead noderead = xmlOperation.NodeRead("/DATASOURCES/Name");
            //while (!noderead.EOF)
            //{
            //    DataSource ds = new DataSource();
            //    ds.DSID = LibSysUtils.ToInt32(noderead.Attributions["DSID"]);
            //    ds.DataSourceName = noderead.InnerText.Trim();
            //    ds.DSDisplayText = noderead.Attributions["DISPLAYTEXT"].ToString();
            //    ds.Package = noderead.Attributions["PACKAGE"].ToString();
            //    dsList.Add(ds);
            //    noderead.ReadNext();
            //}
            #endregion
            return dsList;
        }

        #endregion
    }
}
