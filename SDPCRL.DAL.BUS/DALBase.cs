using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.IBussiness;
using SDPCRL.CORE;
using SDPCRL.COM;

namespace SDPCRL.DAL.BUS
{
    public class DALBase
    {
        private IDataAccess _dataAccess;
        private List<string> _errorMsgList = null;
        #region 公开的属性
        public IDataAccess DataAccess
        {
            get
            {
                if (_dataAccess == null)
                {
                    if (string.IsNullOrEmpty(AccountID))
                        _dataAccess = new DataAccess();
                    else
                        _dataAccess = new DataAccess(AccountID);
                }
                return _dataAccess;
            }
        }

        public string AccountID { get; set; }
        #endregion

        #region 构造函数
        public DALBase()
        {
            _errorMsgList = new List<string>();
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

        #region 公开函数

        public virtual void Save(LibTable[] libtables)
        {
            #region 数据验证.

            #endregion
            BeforeUpdate();
            AfterUpdate();
        }
        public void AddErrorMessage(string msg)
        {
            this._errorMsgList.Add(msg);
        }

        public List<string> GetErrorMessage()
        {
            return _errorMsgList;
        }
        #endregion
    }
}
