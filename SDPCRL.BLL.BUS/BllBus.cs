using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.IBussiness;
using SDPCRL.COM;
using SDPCRL.CORE;

namespace SDPCRL.BLL.BUS
{
    public class BllBus
    {
        #region 私有属性
        private IDALBus _bus = null;
        private IDALBus _dalBus 
        { 
            get 
            {
                if(_bus==null)
                {
                    _bus = (IDALBus)Activator.GetObject(typeof(IDALBus),
                    string.Format("{0}://{1}:{2}/{3}", ServerInfo.ConnectType, ServerInfo.IPAddress, ServerInfo.Point, ServerInfo.DalServerName));
                }
                return _bus;
            } 
        }

        //private string _accoutId = string.Empty;
        #endregion

        #region 公开属性
        public string AccoutId { get; set; }
        #endregion

        #region 构造函数
        public BllBus()
        {

        }
        #endregion

        #region 受保护函数


        protected virtual object ExecuteDalMethod(LibClientInfo clientInfo, string funcId, string method, LibTable[] libTables, params object[] param)
        {
           SDPCRL .COM.DalResult result= _dalBus.ExecuteDalMethod2(clientInfo , AccoutId,funcId, method,libTables , param);
            if (result.ErrorMsglst != null && result.ErrorMsglst.Count > 0)
            {
                throw new LibExceptionBase(result.ErrorMsglst[0].Message, result.ErrorMsglst[0].Stack);
            }
           return result;

            //return _dalBus.ExecuteDalMethod(AccoutId,funcId, method, param);
        }

        protected object ExecuteSysDalMethod(int language, string funcId, string method, params object[] param)
        {
            return _dalBus.ExecuteSysDalMethod(language , funcId, method, param);
        }

        protected object ExecuteSaveMethod(LibClientInfo clientInfo, string funcId, string method, LibTable[] param)
        {
            return _dalBus.ExecuteSaveMethod(clientInfo,AccoutId, funcId, method, param);
        }

        #endregion
    }
}
