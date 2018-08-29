using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SDPCRL.IBussiness
{
   public interface IDataAccess
    {
       object ExecuteScalar(string commandText);
       DataRow GetDataRow(string commandText);
    }
}
