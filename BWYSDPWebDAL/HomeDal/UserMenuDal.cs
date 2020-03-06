using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDPCRL.COM;

namespace HomeDal
{
    [LibProg("UserMenu")]
    public class UserMenuDal: HomeDal
    {
        public LibTableObj GetUserMenus(string userid)
        {
            LibTableObj tbobj = this.DSContext[0];
            this.DataAccess.FillTableObj(tbobj.Where(tbobj.Columns.UId + "={0}", userid));
            return tbobj;
        }
    }
}
