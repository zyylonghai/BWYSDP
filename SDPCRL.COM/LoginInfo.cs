﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    [Serializable]
    public class LoginInfo
    {
        /// <summary>
        /// 1表示登录成功，2表示已登录，3表示密码错误,0表示登录失败，-1用户不存在
        /// </summary>
        public int loginResult { get; set; }
        public string UserNm { get; set; }

        /// <summary>
        /// 是否拥有管理者角色（即001角色）
        /// </summary>
        public bool HasAdminRole { get; set; }
    }
}
