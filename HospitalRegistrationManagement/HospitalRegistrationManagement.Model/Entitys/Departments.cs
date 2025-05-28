using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace HospitalRegistrationManagement.Model.Entitys
{
    /// <summary>
    /// 科室信息表
    /// </summary>
    public class Departments
    {
        /// <summary>
        /// 科室唯一ID
        /// </summary>
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public long department_id {  get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 科室描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(IsNullable =true)]
        public bool is_active { get; set; } = true;
    }
}
