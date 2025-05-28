using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace HospitalRegistrationManagement.Model.Entitys
{
    /// <summary>
    /// 号源管理表
    /// </summary>
    public class RegistrationSources
    {
        /// <summary>
        /// 号源唯一ID
        /// </summary>
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public long source_id {  get; set; }
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
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool is_active { get; set; } = true;
    }
}
