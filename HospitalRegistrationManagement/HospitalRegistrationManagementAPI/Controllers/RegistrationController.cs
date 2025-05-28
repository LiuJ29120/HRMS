using HospitalRegistrationManagement.Model;
using HospitalRegistrationManagement.Service;
using HospitalRegistrationManagement.Service.Registration;
using HospitalRegistrationManagement.Service.Registration.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        public IRegistrationService registrationService;
        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }
        [HttpPost]
        public ApiResult CreateRegistration(RegistrationReq req)
        {
            ApiResult result = new ApiResult();
            if (req.patient_id < 1 || req.doctor_id < 1 || req.schedule_id < 1)
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res = registrationService.CreateRegistration(req,ref msg);
                if(!string.IsNullOrEmpty(msg))
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
        [HttpPost]
        public ApiResult OverRegistration(RegistrationReq req,int status)
        {
            var result = new ApiResult();
            if (req.patient_id < 1 || req.doctor_id < 1 || req.schedule_id < 1)
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res = registrationService.OverRegistration(req,ref msg,status);
                if (!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess = true;
                    if (status == 1)
                    {
                        result.Msg = "已成功取消挂号";
                    }else if(status == 2)
                    {
                        result.Msg = "已成功完成就诊";
                    }
                    result.Result = res;
                }
            }
            return result;
        }
        [HttpGet]
        public ApiResult GetDoctorRegistration(long id)
        {
            ApiResult result = new ApiResult();
            if (id < 1)
            {
                result.Msg = "非法的传递参数";
            }
            else
            {
                var res = registrationService.GetDoctorRegistration(id);
                result.IsSuccess= true;
                result.Result= res;
            }
            return result;
        }
        [HttpGet]
        public ApiResult GetPatientRegistration(long id)
        {
            ApiResult result = new ApiResult();
            if (id < 1)
            {
                result.Msg = "非法的传递参数";
            }
            else
            {
                var res = registrationService.GetPatientRegistration(id);
                result.IsSuccess = true;
                result.Result = res;
            }
            return result;
        }
    }
}
