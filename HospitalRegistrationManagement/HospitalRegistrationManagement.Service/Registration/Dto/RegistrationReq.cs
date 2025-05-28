using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Registration.Dto
{
    public class RegistrationReq
    {
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
    }
}
