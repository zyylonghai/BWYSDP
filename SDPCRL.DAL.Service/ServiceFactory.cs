using SDPCRL.Services;

namespace SDPCRL.DAL.Service
{
    public class ServiceFactory
    {
        private static IService _dbservice;
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
    }
}
