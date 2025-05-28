using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service
{
    public class UserReq
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string username {  get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

    }
}
