using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.CORE;
using SDPCRL.CORE.FileUtils;
using SDPCRL.COM.ModelManager.FormTemplate;
using SDPCRL.COM.ModelManager.Reports;
using SDPCRL.COM.ModelManager.Trans;

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
        #region 旧代码
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

        public static LibDataSource GetDataSource(string dsNm)
        {
            return (LibDataSource)InternalInstanceModelSource(SysConstManage.DataSourceNm, dsNm);
        }
        public static LibFormPage GetFormSource(string fmId)
        {
            return (LibFormPage)InternalInstanceModelSource(SysConstManage.FormSourceNm, fmId);
        }

        public static LibPermissionSource GetLibPermission(string permissionid)
        {
            return (LibPermissionSource)InternalInstanceModelSource(SysConstManage.PermissionSourceNm, permissionid);
        }
        public static LibReportsSource GetLibReportSource(string reportid)
        {
            return (LibReportsSource)InternalInstanceModelSource(SysConstManage.ReportSourceNm, reportid);
        }
        public static LibTransSource GetTransSource(string transid)
        {
            return (LibTransSource)InternalInstanceModelSource(SysConstManage.TransSourceNm, transid);
        }
        public static LibKeyValueCollection GetKeyValues(string id)
        {
            return (LibKeyValueCollection)InternalInstanceModelSource(SysConstManage.KeyValues, id);
        }
        public static T GetModelBypath<T>(string rootpath, string modelId, string package)
        {
            FileOperation fileOperation = new FileOperation();
            fileOperation.Encoding = LibEncoding.UTF8;
            if (typeof(T) == typeof(LibDataSource))
            {
                fileOperation.FilePath = string.Format(@"{0}\Models\{1}\{2}\{3}.xml", rootpath, SysConstManage.DataSourceNm, package, modelId);
            }
            else if (typeof(T) == typeof(LibFormPage))
            {
                fileOperation.FilePath = string.Format(@"{0}\Models\{1}\{2}\{3}.xml", rootpath, SysConstManage.FormSourceNm, package, modelId);
            }
            else if (typeof(T) == typeof(LibPermissionSource))
            {
                fileOperation.FilePath = string.Format(@"{0}\Models\{1}\{2}\{3}.xml", rootpath, SysConstManage.PermissionSourceNm, package, modelId);
            }
            else if (typeof(T) == typeof(LibReportsSource))
            {
                fileOperation.FilePath = string.Format(@"{0}\Models\{1}\{2}\{3}.xml", rootpath, SysConstManage.ReportSourceNm, package, modelId);
            }
            string xmlstr = fileOperation.ReadFile();
            if (!string.IsNullOrEmpty(xmlstr))
            {
                return SerializerUtils.XMLDeSerialize<T>(xmlstr);
            }
            return default(T);

        }

        public static T GetModelBymodelId<T>(string rootpath, string modelId)
        {
            FileOperation fileOperation = new FileOperation();
            fileOperation.Encoding = LibEncoding.UTF8;
            rootpath = string.Format(@"{0}\Models", rootpath);
            if (typeof(T).Equals(typeof(LibDataSource)))
            {
                fileOperation.FilePath = string.Format(@"{0}\{1}", rootpath, SysConstManage.DataSourceNm);
            }
            else if (typeof(T).Equals(typeof(LibFormPage)))
            {
                fileOperation.FilePath = string.Format(@"{0}\{1}", rootpath, SysConstManage.FormSourceNm);
            }
            else
            {
                fileOperation.FilePath = string.Format(@"{0}\{1}", rootpath, SysConstManage.PermissionSourceNm);
            }
            string dsxml = fileOperation.SearchAndRead(string.Format("{0}.xml", modelId));
            if (string.IsNullOrEmpty(dsxml))
            {
                return default(T);
            }
            return SerializerUtils.XMLDeSerialize<T>(dsxml);

        }

        public static string[] GetAllDataSourceNm(string path)
        {
            return InternalSearchAllModel(path, SysConstManage.DataSourceNm);
        }

        public static string[] GetAllProgId(string path)
        {
            return InternalSearchAllModel(path, SysConstManage.FormSourceNm);
        }

        public static string[] GetAllKeyValuesNm(string path)
        {
            return InternalSearchAllModel(path, SysConstManage.KeyValues);
        }

        public static List<LibSysField> Sysfields {
            get {
                List<LibSysField> result = new List<LibSysField>();
                result.Add(new LibSysField {Name =SysConstManage .sysfld_creater,DisplayName= "创建者",FieldType=LibFieldType.String,FieldLength=30 });
                result.Add(new LibSysField { Name = SysConstManage.sysfld_createDT, DisplayName = "创建时间", FieldType = LibFieldType.DateTime, FieldLength = 0 });
                result.Add(new LibSysField { Name = SysConstManage.sysfld_lastmodifier, DisplayName = "最后修改者", FieldType = LibFieldType.String, FieldLength = 30 });
                result.Add(new LibSysField { Name = SysConstManage.sysfld_lastmodifyDT, DisplayName = "最后修改日期", FieldType = LibFieldType.DateTime, FieldLength = 0 });
                //Addsysfield(SysConstManage.sysfld_creater, "创建者", result);
                //Addsysfield(SysConstManage.sysfld_createDT, "创建时间", result);
                //Addsysfield(SysConstManage.sysfld_lastmodifier, "最后修改者", result);
                //Addsysfield(SysConstManage.sysfld_lastmodifyDT, "最后修改日期", result);
                return result;
            }
        }
        #endregion

        #region 私有函数

        #region 旧代码
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
            if (dsinfolist.DSInfoCollection != null && dsinfolist.DSInfoCollection.Count > 0)
            {
                DataSource ds = null;
                foreach (DSInfo info in dsinfolist.DSInfoCollection)
                {
                    ds = new DataSource();
                    ds.DSID = info.DSID;
                    ds.DataSourceName = info.Name;
                    //ds.DSDisplayText = info.DISPLAYTEXT;
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

        private static object InternalInstanceModelSource(string path, string modelSourceId)
        {
            FileOperation fileOperation = new FileOperation();
            fileOperation.FilePath = string.Format(@"{0}\{1}", SysConstManage.ModelPath, path);
            fileOperation.Encoding = LibEncoding.UTF8;
            string dsxml = fileOperation.SearchAndRead(string.Format("{0}.xml", modelSourceId));
            switch (path)
            {
                case SysConstManage.DataSourceNm:
                    if (string.IsNullOrEmpty(dsxml))
                    {

                        return new LibDataSource { DSID = modelSourceId };
                    }
                    return SerializerUtils.XMLDeSerialize<LibDataSource>(dsxml);
                case SysConstManage.FormSourceNm:
                    if (string.IsNullOrEmpty(dsxml))
                    {

                        return new LibFormPage { FormId = modelSourceId };
                    }
                    return SerializerUtils.XMLDeSerialize<LibFormPage>(dsxml);
                case SysConstManage .PermissionSourceNm :
                    if (string.IsNullOrEmpty(dsxml))
                    {
                        return new LibPermissionSource { PermissionID = modelSourceId };
                    }
                    return SerializerUtils.XMLDeSerialize<LibPermissionSource>(dsxml); ;
                case SysConstManage.KeyValues:
                    if (string.IsNullOrEmpty(dsxml))
                    {
                        return new LibKeyValueCollection { ID = modelSourceId };
                    }
                    return SerializerUtils.XMLDeSerialize<LibKeyValueCollection>(dsxml);
                case SysConstManage.ReportSourceNm:
                    if (string.IsNullOrEmpty(dsxml))
                    {

                        return new LibReportsSource { ReportId = modelSourceId };
                    }
                    return SerializerUtils.XMLDeSerialize<LibReportsSource>(dsxml);
                case SysConstManage.TransSourceNm:
                    if (string.IsNullOrEmpty(dsxml))
                    {

                        return new LibTransSource { TransId = modelSourceId };
                    }
                    return SerializerUtils.XMLDeSerialize<LibTransSource>(dsxml);
                default :

                    return null;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">模型所在的根目录,如果为空 则以SysConstManage.ModelPath为值</param>
        /// <param name="modeltype">数据源模型或排班模型或权限模型</param>
        /// <returns></returns>
        private static string[] InternalSearchAllModel(string path,string modeltype)
        {
            FileOperation fileOperation = new FileOperation();
            fileOperation.FilePath = string.Format(@"{0}\{1}", string.IsNullOrEmpty(path) ? SysConstManage.ModelPath : path, modeltype);
            fileOperation.Encoding = LibEncoding.UTF8;
            return fileOperation.SearchFileNm();
        }
        //private static void Addsysfield(string fieldnm,string displaynm,List<LibSysField> collections)
        //{
        //    LibSysField f = new LibSysField { Name = fieldnm, DisplayName = displaynm };
        //    collections.Add(f);
        //}
        #endregion
    }
}
