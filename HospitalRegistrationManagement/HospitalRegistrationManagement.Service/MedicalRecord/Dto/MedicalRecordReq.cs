using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.MedicalRecord.Dto
{
    public class MedicalRecordReq
    {
        /// <summary>
        /// 关联的挂号记录ID
        /// </summary>
        public long registration_id { get; set; }
        /// <summary>
        /// 诊断结果
        /// </summary>
        public string diagnosis { get; set; }
        /// <summary>
        /// 处方信息
        /// </summary>
        public string prescription { get; set; }
    }
}
