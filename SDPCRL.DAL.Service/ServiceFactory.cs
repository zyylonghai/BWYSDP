using SDPCRL.Services;

namespace SDPCRL.DAL.Service
{
    public class ServiceFactory
    {
        private static IService _dbservice;
        private static IService _dalserver;
        public static IService DBService
        {
            get
            {
                if (_dbservice == null)
                {
                    _dbservice = new DBService();
                }
                return _dbservice;
            }
        }
        public static IService DALServer
        {
            get {
                if (_dalserver == null)
                    _dalserver = new DALServer();
                return _dalserver;
            }
        }
    }
}
