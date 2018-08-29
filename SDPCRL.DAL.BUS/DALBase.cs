using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.IBussiness;

namespace SDPCRL.DAL.BUS
{
    public class DALBase
    {
        private IDataAccess _dataAccess;
        #region 公开的属性
        public IDataAccess DataAccess
        {
            get
            {
                if (_dataAccess == null)
                    _dataAccess = new DataAccess();
                return _dataAccess;
            }
        }
        #endregion

        #region 构造函数
        public DALBase()
        {

        }
        #endregion

        #region 受保护 虚拟函数
        protected virtual void BeforeUpdate()
        {

        }

        protected virtual void AfterUpdate()
        {

        }
        #endregion

        #region 私有函数
        #endregion
    }
}
