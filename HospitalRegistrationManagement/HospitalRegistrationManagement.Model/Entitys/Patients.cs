using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Model.Entitys
{
    /// <summary>
    /// 患者信息表
    /// </summary>
    public class Patients
    {
        /// <summary>
        /// 患者唯一ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long patient_id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long user_id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 性别(1="男",2="女")
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime created_at { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool is_active { get; set; } = true;
    }
}
