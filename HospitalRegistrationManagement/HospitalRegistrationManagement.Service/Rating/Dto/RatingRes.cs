using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Rating.Dto
{
    public class RatingRes
    {
        /// <summary>
        /// 评价唯一ID
        /// </summary>
        public long rating_id { get; set; }
        /// <summary>
        /// 关联的挂号记录ID
        /// </summary>
        public long registration_id { get; set; }
        /// <summary>
        /// 评分(1-5分)
        /// </summary>
        public double score { get; set; }
        /// <summary>
        /// 文字评价
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime create_at { get; set; }
    }
}
