using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.IBussiness;
using SDPCRL.DAL.DBHelp;
using SDPCRL.DAL.IDBHelp;
using System.Data;

namespace SDPCRL.DAL.BUS
{
    class DataAccess:IDataAccess
    {
       private  static DBHelpFactory  _dbFactory;
       private ILibDBHelp  _dbHelp;
        public DataAccess()
        {
            if (_dbFactory == null)
            {
                _dbFactory = new DBHelpFactory();
            }
            _dbHelp = _dbFactory.GetDBHelp();
        }
        public DataAccess(string guid)
            :this()
        {
            _dbHelp = _dbFactory.GetDBHelp(guid);

        }

        public object ExecuteScalar(string commandText)
        {
            return _dbHelp.ExecuteScalar(commandText);
        }

        public DataRow GetDataRow(string commandText)
        {
            return _dbHelp.GetDataRow(commandText);
        }
    }
}
