using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace HospitalRegistrationManagement.Model.Entitys
{
    /// <summary>
    /// 挂号记录表
    /// </summary>
    public class Registrations
    {
        /// <summary>
        /// 挂号记录唯一ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public long registration_id {  get; set; }
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
        /// 挂号状态(0="待就诊",1="已取消",2="已完成",3="已评价")
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime create_at { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool is_active { get; set; } = true;
    }
}
