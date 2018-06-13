using SDPCRL.DAL.COM;
using SDPCRL.DAL.IDBHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPCRL.DAL.IDBHelp
{
    public interface IDBHelpFactory
    {
        //ILibDBHelp GetDBHelp(LibProviderType providerType);
        ILibDBHelp GetDBHelp();
        ILibDBHelp GetDBHelp(string guid);
    }
}
