using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.RegistrationSource.Dto
{
    public class RegistrationSourceRes
    {
        /// <summary>
        /// 号源唯一ID
        /// </summary>
        public long source_id { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public long department_id { get; set; }
        /// <summary>
        /// 号源日期
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// 剩余可挂号数
        /// </summary>
        public int available_slots { get; set; }
        /// <summary>
        /// 总号源数量
        /// </summary>
        public int total_slots { get; set; }
    }
}
