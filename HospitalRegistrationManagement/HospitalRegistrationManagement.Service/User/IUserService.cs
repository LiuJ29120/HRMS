using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service
{
    public interface IUserService
    {
        UserRes GetUsers(UserReq req);
        UserRes RegisterUser(RegisterReq req,ref string msg);
        public bool UpdatePassword(UserReq req,string pass,ref string msg);
    }
}
