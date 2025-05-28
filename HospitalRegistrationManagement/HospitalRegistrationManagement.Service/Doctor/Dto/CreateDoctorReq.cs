using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Doctor.Dto
{
    public class CreateDoctorReq
    {
        /// <summary>
        /// 所属科室ID
        /// </summary>
        public long department_id { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 职称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 医生简介
        /// </summary>
        public string bio { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
}
