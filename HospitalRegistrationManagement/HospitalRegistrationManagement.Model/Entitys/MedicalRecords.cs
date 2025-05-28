using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace HospitalRegistrationManagement.Model.Entitys
{
    /// <summary>
    /// 就诊记录表
    /// </summary>
    public class MedicalRecords
    {
        /// <summary>
        /// 就诊记录唯一ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long record_id { get; set; }
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
        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime visit_time { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool is_active { get; set; } = true;
    }
}
