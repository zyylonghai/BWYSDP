using SDPCRL.CORE;
using SDPCRL.CORE.FileUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SDPCRL.COM;

namespace SDPCRL.DAL.COM
{
    public class DalAssemblyHelp
    {
        public DalAssemblyHelp()
        { }
        public static List<FuncAssemblyInfo> GetDalAssemblyInfos()
        {
            List<FuncAssemblyInfo> assemblyInfos = new List<FuncAssemblyInfo>();
            FuncAssemblyInfo info = null;
            FileOperation fileOperation = new FileOperation();
            fileOperation.FilePath = string.Format(@"{0}", SysConstManage.DALAssemblyPath);
            fileOperation.Encoding = LibEncoding.UTF8;
            List<LibFileInfo> fileInfos = fileOperation.SearchAllFileInfo();
            foreach (LibFileInfo f in fileInfos)
            {
                Assembly assembly = Assembly.LoadFrom(string.Format(@"{0}\{1}.dll", SysConstManage.DALAssemblyPath, f.FileName));
                Type[] types = assembly.GetTypes();
                foreach (Type t in types)
                {
                    object[] attrs = t.GetCustomAttributes(typeof(LibProgAttribute), true);
                    if (attrs.Length > 0)
                    {
                        LibProgAttribute progattr = (LibProgAttribute)attrs[0];
                        if (progattr!=null &&!string.IsNullOrEmpty(progattr.ProgId))
                        {
                            info = new FuncAssemblyInfo();
                            info.FuncID = progattr.ProgId;
                            info.AssemblyName = assembly.ManifestModule .Name.Split('.')[0];
                            info.TypeFullName = t.FullName;
                            assemblyInfos.Add(info);
                        }
                    }
                }
            }
            return assemblyInfos;
        }
    }

}
