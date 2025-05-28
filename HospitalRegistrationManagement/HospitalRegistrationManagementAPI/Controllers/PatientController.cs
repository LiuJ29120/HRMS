using HospitalRegistrationManagement.Model;
using HospitalRegistrationManagement.Service.Patient;
using HospitalRegistrationManagement.Service.Patient.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        public IPatientService patientService;
        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }
        [HttpGet]
        public ApiResult DeletePatient(long id)
        {
            ApiResult result = new ApiResult();
            if(id <1)
            {
                result.Msg = "找不到该患者";
            }
            else
            {
                string msg = string.Empty;
                var res=patientService.DeletePatient(id,ref msg);
                if (!string.IsNullOrEmpty(msg))
                {
                    result.Msg= msg;
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
        public ApiResult UpdatePatient(PatientReq req)
        {
            ApiResult result = new ApiResult();
            if(req.user_id<1||req.gender<0||req.gender>2||string.IsNullOrEmpty(req.name)||string.IsNullOrEmpty(req.phone))
            {
                result.Msg = "非法的输入参数";
            }
            else
            {
                string msg = string.Empty;
                var res = patientService.UpdatePatient(req,ref msg);
                if(!string.IsNullOrEmpty(msg))
                {
                    result.Msg= msg;
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
        [HttpGet]
        public ApiResult GetPatientByUid(long id)
        {
            ApiResult result = new ApiResult();
            if (id < 1)
            {
                result.Msg = "非法的传递参数";
            }
            else
            {
                string msg = string.Empty;
                var res = patientService.GetPatientByUid(id,ref msg);
                if(!string.IsNullOrEmpty(msg))
                {
                    result.Msg= msg;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Result= res;
                }
            }
            return result;
        }
        [HttpGet]
        public ApiResult GetPatientByPid(long id)
        {
            ApiResult result = new ApiResult();
            if (id < 1)
            {
                result.Msg = "非法的传递参数";
            }
            else
            {
                string msg = string.Empty;
                var res = patientService.GetPatientByPid(id, ref msg);
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
    }
}
