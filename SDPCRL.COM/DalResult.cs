using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    [Serializable ]
   public  class DalResult
   {
       #region 私有属性
       private List<string> _messageList = null;
       private List<ErrorMessage> _errormsglst = null;
       #endregion

       public object Value { get; set; }

       public List<string> Messagelist {
           get
           {
               if (_messageList == null)
                   _messageList = new List<string>();
               return _messageList;
           }
           set { _messageList = value; } 
       }
       public List<ErrorMessage> ErrorMsglst 
       {
           get 
           {
               if (_errormsglst == null)
               {
                   _errormsglst = new List<ErrorMessage>();
               }
               return _errormsglst;
           }
           set { _errormsglst = value; }
       }

    }
}
