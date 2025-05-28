using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Registration.Dto
{
    public class RegistrationRes
    {
        /// <summary>
        /// 挂号记录唯一ID
        /// </summary>
        public long registration_id { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public long patient_id { get; set; }
        /// <summary>
        /// 医生ID
        /// </summary>
        public long doctor_id { get; set; }
        /// <summary>
        /// 排班ID
        /// </summary>
        public long schedule_id { get; set; }
        /// <summary>
        /// 挂号状态(0="待就诊",1="已取消",2="已完成")
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime create_at { get; set; }
    }
}
