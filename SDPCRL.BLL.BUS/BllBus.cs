﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.IBussiness;

namespace SDPCRL.BLL.BUS
{
    public class BllBus
    {
        #region 私有属性
        private IDALBus _dalBus 
        { 
            get 
            {
                return (IDALBus)Activator.GetObject(typeof(IDALBus), 
                    string.Format("{0}://{1}:{2}/{3}",ServerInfo.ConnectType ,ServerInfo.IPAddress ,ServerInfo.Point, ServerInfo.DalServerName));
            } 
        }
        #endregion

        #region 构造函数
        public BllBus()
        {

        }
        #endregion

        #region 受保护函数


        protected virtual object ExecuteDalMethod(string funcId, string method,params object[] param)
        {
            return _dalBus.ExecuteDalMethod(funcId, method, param);
        }

        #endregion
    }
}