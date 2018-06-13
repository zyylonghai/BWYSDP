using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.BLL.DataAccess
{
    public class DataFactory
    {
        private IDataAccess _dataAccess;
        public IDataAccess DataAccess
        {
            get { return _dataAccess = new DataAccess(); }
        }
    }
}
