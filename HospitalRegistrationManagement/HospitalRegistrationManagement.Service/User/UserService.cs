using AutoMapper;
using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public UserRes GetUsers(UserReq req)
        {
            var user = DbContext.db.Queryable<Users>().First(p => p.username == req.username && p.password == req.password&&p.is_active);
            if (user != null)
            {
                return _mapper.Map<UserRes>(user);
            }
            return new UserRes();
        }

        public UserRes RegisterUser(RegisterReq req, ref string msg)
        {
            var user = DbContext.db.Queryable<Users>().First(p => p.username == req.username&&p.is_active);
            if (user != null)
            {
                msg = "账号已存在！";
                return _mapper.Map<UserRes>(user);
            }
            else
            {
                try
                {
                    Users users = _mapper.Map<Users>(req);
                    users.role = 1;
                    users.create_at = DateTime.Now;
                    bool usersSuccess = DbContext.db.Insertable(users).ExecuteCommand() > 0;
                    if (usersSuccess)
                    {
                        user = DbContext.db.Queryable<Users>().First(p => p.username == req.username && p.password == req.password&&p.is_active);
                        Patients patients = _mapper.Map<Patients>(req);
                        patients.user_id = user.user_id;
                        patients.created_at = DateTime.Now;
                        bool patientsSuccess = DbContext.db.Insertable(patients).ExecuteCommand() > 0;
                        if (patientsSuccess)
                        {
                            UserRes res = _mapper.Map<UserRes>(user);
                            res.name = patients.name;
                            return res;
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            return new UserRes();
        }

        public bool UpdatePassword(UserReq req, string pass,ref string msg)
        {
            var user = DbContext.db.Queryable<Users>().First(p=>p.username==req.username && p.password==req.password&&p.is_active);
            if (user != null)
            {
                user.password = pass;
                bool userSuccess = DbContext.db.Updateable(user).ExecuteCommand() > 0;
                if (userSuccess)
                {
                    return true;
                }
            }
            else
            {
                msg = "原密码错误";
            }
            return false;
        }
    }
}
