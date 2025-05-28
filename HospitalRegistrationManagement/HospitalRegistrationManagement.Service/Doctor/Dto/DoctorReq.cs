using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Doctor.Dto
{
    public class DoctorReq
    {
        /// <summary>
        /// 医生唯一ID
        /// </summary>
        public long doctor_id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long user_id { get; set; }
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
        /// 头像URL
        /// </summary>
        public string avatar_url { get; set; }
    }
}
