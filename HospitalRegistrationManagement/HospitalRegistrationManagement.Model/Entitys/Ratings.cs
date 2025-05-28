using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace HospitalRegistrationManagement.Model.Entitys
{
    /// <summary>
    /// 就诊评价表
    /// </summary>
    public class Ratings
    {
        /// <summary>
        /// 评价唯一ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public long rating_id {  get; set; }
        /// <summary>
        /// 关联的挂号记录ID
        /// </summary>
        public long registration_id { get; set; }
        /// <summary>
        /// 评分(1-5分)
        /// </summary>
        public decimal score { get; set; }
        /// <summary>
        /// 文字评价
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime create_at { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool is_active { get; set; } = true;
    }
}
