using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Model.Entitys
{
    /// <summary>
    /// 医生表
    /// </summary>
    public class Doctors
    {
        /// <summary>
        /// 医生唯一ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long doctor_id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long user_id { get; set; }
        /// <summary>
        /// 所属科室ID
        /// </summary>
        public long department_id { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 职称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 医生简介
        /// </summary>
        public string bio { get; set; }
        /// <summary>
        /// 头像URL
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string avatar_url { get; set; } = "0";
        /// <summary>
        /// 平均评分
        /// </summary>
        [SugarColumn(ColumnDataType = "decimal(18, 2)")]
        public decimal average_rating { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool is_active { get; set; } = true;
    }
}
