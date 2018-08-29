using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.IBussiness
{
    public interface IDALBus
    {
        object ExecuteDalUpdate(string funcId);
        object ExecuteDalMethod(string funcId, string method, params object[] param);
        object ExecuteDalMethod(string method, params object[] param);
        object ServerConnectTest();
    }
}
