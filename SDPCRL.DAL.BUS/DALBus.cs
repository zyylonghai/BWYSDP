using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.IBussiness;
using SDPCRL.DAL.COM;
using System.Data;
using System.Reflection;
using SDPCRL.CORE;

namespace SDPCRL.DAL.BUS
{
    public class DALBus : MarshalByRefObject,IDALBus
    {

        public object ExecuteDalUpdate(string funcId)
        {
            ReflectionOperate reflect = new ReflectionOperate(funcId);
            object obj=reflect.InstanceTarget();
            Type t = obj.GetType();
            MethodInfo method = t.GetMethod("GetAccount");
            object[] param ={
             };
            return  method.Invoke(obj,null);
        }


        public object ExecuteDalMethod(string funcId, string method, params object[] param)
        {
            ReflectionOperate reflect = new ReflectionOperate(funcId);
            object obj = reflect.InstanceTarget();
            Type t = obj.GetType();
            MethodInfo func = t.GetMethod(method);
            return func.Invoke(obj, param);
        }

        public object ExecuteDalMethod(string method, params object[] param)
        {
            throw new NotImplementedException();
        }


        public object ServerConnectTest()
        {
            return true;
        }
    }

    class ReflectionOperate
    {
        private string _funcId;
        private IDataAccess _dataAccess;
        private string _assemblyNm;
        private string _typeNm;
        public ReflectionOperate(string funcId)
        {
            this._funcId = funcId;
            _dataAccess = new DataAccess();
        }
        public object InstanceTarget()
        {
            GetAssemblyInfo();
            if (!string.IsNullOrEmpty(_assemblyNm))
            {
                Assembly assembly = Assembly.LoadFrom(string.Format(@"{0}\{1}.dll", SysConstManage.DALAssemblyPath, _assemblyNm));
                Type type = assembly.GetType(_typeNm);
                return Activator.CreateInstance(type);
            }
            return null;
        }

        private void GetAssemblyInfo()
        {
            SQLBuilder sqlbuilder = new SQLBuilder();
            string sql = sqlbuilder.GetSQL("FuncAssemblyInfo", new string[] { "AssemblyName", "TypeFullName" }, string.Format("FuncID='{0}'", _funcId));
            DataRow dr = _dataAccess.GetDataRow(sql);
            if (dr != null)
            {
                _assemblyNm = dr["AssemblyName"].ToString();
                _typeNm = dr["TypeFullName"].ToString();
            }
        }


        
    }
}
