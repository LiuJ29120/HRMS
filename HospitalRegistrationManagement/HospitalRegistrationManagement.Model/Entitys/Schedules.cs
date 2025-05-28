using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace HospitalRegistrationManagement.Model.Entitys
{
    /// <summary>
    /// 排班表
    /// </summary>
    public class Schedules
    {
        /// <summary>
        /// 排班唯一ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long schedule_id { get; set; }
        /// <summary>
        /// 医生ID
        /// </summary>
        public long doctor_id { get; set; }
        /// <summary>
        /// 排班日期
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// 班次类型(0="上午",1="下午")
        /// </summary>
        public int shift_type { get; set; }
        /// <summary>
        /// 诊室号
        /// </summary>
        public string room_number { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool is_active { get; set; } = true;
    }
}
