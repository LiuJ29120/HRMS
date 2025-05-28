using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace HospitalRegistrationManagement.Model.Entitys
{
    public class Users
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public long user_id { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 用户角色(0="管理员",1="患者",2="医生")
        /// </summary>
        public int role {  get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime create_at { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary
        [SugarColumn(IsNullable = true)]
        public bool is_active { get; set; } = true;
    }
}
