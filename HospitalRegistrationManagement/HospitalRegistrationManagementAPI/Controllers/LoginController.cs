using HospitalRegistrationManagement.Model;
using HospitalRegistrationManagement.Model.Entitys;
using HospitalRegistrationManagement.Service;
using HospitalRegistrationManagement.Service.Patient;
using HospitalRegistrationManagement.Service.Patient.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IUserService _userService;
        public IPatientService _patientService;
        public LoginController(IUserService userService, IPatientService patientService)
        {
            _userService = userService;
            _patientService = patientService;
        }
        [HttpPost]
        public ApiResult LoginOn(UserReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.username) || string.IsNullOrEmpty(req.password))
            {
                result.Msg = "姓名或密码为空，请重新输入";
            }
            else
            {
                UserRes user = _userService.GetUsers(req);
                if (string.IsNullOrEmpty(user.username))
                {
                    result.Msg = "账号不存在";
                }
                else
                {
                    result.IsSuccess = true;
                    string msg = string.Empty;
                    PatientRes patientRes = _patientService.GetPatientByUid(user.user_id, ref msg);
                    user.name = patientRes.name;
                    user.gender = patientRes.gender;
                    result.Msg="欢迎您"+user.name;
                    result.Result = user;
                }
            }
            return result;
        }
        [HttpPost]
        public ApiResult Register(RegisterReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.username) || string.IsNullOrEmpty(req.password) || string.IsNullOrEmpty(req.phone) || string.IsNullOrEmpty(req.name) || req.gender < 1 || req.gender > 2)
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res = _userService.RegisterUser(req, ref msg);
                if (!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess = true;
                    PatientRes patientRes = _patientService.GetPatientByUid(res.user_id, ref msg);
                    res.gender = patientRes.gender;
                    result.Msg = "注册成功";
                    result.Result = res;
                }
            }
            return result;
        }
    }
}
