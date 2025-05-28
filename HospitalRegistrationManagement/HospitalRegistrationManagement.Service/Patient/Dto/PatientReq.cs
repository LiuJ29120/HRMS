using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Patient.Dto
{
    public class PatientReq
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long user_id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 性别(1="男",2="女")
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string phone { get; set; }
    }
}
