using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service
{
    public class RegisterReq
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string username {  get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string phone {  get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public int gender {  get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
}
