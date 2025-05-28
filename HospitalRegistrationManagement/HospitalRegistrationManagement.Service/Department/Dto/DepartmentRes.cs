using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Department.Dto
{
    public class DepartmentRes
    {
        /// <summary>
        /// 科室唯一ID
        /// </summary>
        public long department_id { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 科室描述
        /// </summary>
        public string description { get; set; }
    }
}
