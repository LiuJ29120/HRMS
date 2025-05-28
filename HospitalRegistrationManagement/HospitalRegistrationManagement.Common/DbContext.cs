using HospitalRegistrationManagement.Model.Entitys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Common
{
    public class DbContext
    {
        public static SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = "Server=LAPTOP-O9QSU5VS;Database=School;Persist Security Info=true;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=123456;",
            DbType = DbType.SqlServer,
            IsAutoCloseConnection = true,
        });
        public static void InitDataBase()
        {
            db.DbMaintenance.CreateDatabase();
            string nspace = "HospitalRegistrationManagement.Model.Entitys";
            Type[] types = Assembly.LoadFrom("bin/Debug/net8.0/HospitalRegistrationManagement.Model.dll").GetTypes().Where(p => p.Namespace == nspace).ToArray();
            db.CodeFirst.SetStringDefaultLength(200).InitTables(types);
            List<Users> flowers = new List<Users>()
            {
                //new Users()
                //{
                //    username= "admin",
                //    password= "123456",
                //    role=0,
                //    create_at= DateTime.Now,
                //}
            };
            //写入测试数据
            db.Insertable(flowers).ExecuteCommand();
        }
    }
}
