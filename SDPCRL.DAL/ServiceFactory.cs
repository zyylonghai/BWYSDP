using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.DAL.Service;

namespace SDPCRL.DAL
{
    public class ServiceFactory
    {
        public static IDataService DataService
        {
            get;
            //get
            //{
            //    return new DataService();
            //}
        }
    }
}
