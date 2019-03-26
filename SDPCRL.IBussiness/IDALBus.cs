using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.COM;

namespace SDPCRL.IBussiness
{
    public interface IDALBus
    {
        object ExecuteDalUpdate(string accountId, string funcId);
        object ExecuteDalMethod(string accountId, string funcId, string method, params object[] param);
        object ExecuteMethod(string accountId, string method, params object[] param);
        object ExecuteSysDalMethod(string funcId, string method, params object[] param);
        DalResult ExecuteDalMethod2(string accountId, string funcId, string method, params object[] param);

        object ExecuteSaveMethod(string accountId, string funcId, string method, LibTable[] param);
        object ServerConnectTest();
    }
}
