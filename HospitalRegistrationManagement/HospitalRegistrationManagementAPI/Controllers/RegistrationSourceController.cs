using HospitalRegistrationManagement.Model;
using HospitalRegistrationManagement.Service.RegistrationSource;
using HospitalRegistrationManagement.Service.RegistrationSource.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationSourceController : ControllerBase
    {
        public IRegistrationSourceService rsService;
        public RegistrationSourceController(IRegistrationSourceService registrationSourceService)
        {
            rsService = registrationSourceService;
        }
        [HttpPost]
        public ApiResult CreateRegistrationSource(RegistrationSourceReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.date.ToString()) || req.department_id < 1 || req.total_slots < 0)
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res = rsService.CreateRegistrationSource(req, ref msg);
                if (!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Result = res;
                }
            }
            return result;
        }
        [HttpGet]
        public ApiResult GetRegistrationSourceAll()
        {
            ApiResult result = new ApiResult();
            string msg = string.Empty;
            var res = rsService.GetRegistrationSourceAll();
            result.IsSuccess = true;
            result.Result = res;
            return result;
        }
        [HttpGet]
        public ApiResult DeleteRegistrationSource(long id)
        {
            ApiResult result = new ApiResult();
            if(id < 1)
            {
                result.Msg = "非法的传递参数";
            }
            else
            {
                string msg = string.Empty ; 
                var res = rsService.DeleteRegistrationSource(id, ref msg);
                if(!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Msg = "删除成功";
                    result.Result = res;
                }
            }
            return result;
        }
        [HttpPost]
        public ApiResult UpdateRegistrationSource(RegistrationSourceReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.date.ToString()) || req.department_id < 1 || req.total_slots < 0)
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty ;
                var res = rsService.UpdateRegistrationSource(req, ref msg);
                if(!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Msg = "修改成功";
                    result.Result= res;
                }
            }
            return result;
        }
    }
}
