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
        object ExecuteDalMethod(string accountId, int language, string funcId, string method, params object[] param);
        object ExecuteMethod(string accountId, string method, params object[] param);
        object ExecuteSysDalMethod(int language, string funcId, string method, params object[] param);
        object ExecuteLogDalMethod(int language, string funcId, string method, params object[] param);

        DalResult ExecuteDalMethod2(LibClientInfo clientInfo, string accountId, string funcId, string method,LibTable[] libTables, params object[] param);

        object ExecuteSaveMethod(LibClientInfo clientInfo, string accountId, string funcId, string method, LibTable[] param);
        object ServerConnectTest();
    }
}
