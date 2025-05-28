using HospitalRegistrationManagement.Model;
using HospitalRegistrationManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        public ApiResult UpdatePassword(UserReq req,string pass)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(req.username) || string.IsNullOrEmpty(req.password))
            {
                result.Msg = "非法的传递参数";
            }
            else
            {
                string msg=string.Empty;
                var res = userService.UpdatePassword(req, pass, ref msg);
                if (!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Msg = "修改成功";
                    result.Result = res;
                }
            }
            return result;
        }
    }
}
