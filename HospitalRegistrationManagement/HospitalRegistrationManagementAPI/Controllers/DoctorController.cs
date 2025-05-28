using HospitalRegistrationManagement.Model;
using HospitalRegistrationManagement.Service.Doctor;
using HospitalRegistrationManagement.Service.Doctor.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public IDoctorService doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }
        [HttpPost]
        public ApiResult CreateDoctor(CreateDoctorReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.name) || req.department_id < 1 || string.IsNullOrEmpty(req.bio) || string.IsNullOrEmpty(req.title) || string.IsNullOrEmpty(req.username) || string.IsNullOrEmpty(req.password))
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res = doctorService.CreateDoctor(req, ref msg);
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
        public ApiResult GetDoctorAll()
        {
            ApiResult result = new ApiResult();
            var res = doctorService.GetDoctorAll();
            result.IsSuccess = true;
            result.Result = res;
            return result;
        }
        [HttpGet]
        public ApiResult DeleteDoctor(long id)
        {
            ApiResult result=new ApiResult();
            if (id <1)
            {
                result.Msg = "该医生不存在";
            }
            else
            {
                string msg = string.Empty;
                var res = doctorService.DeteleDoctor(id, ref msg);
                if(!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess= true;
                    result.Msg = "已成功删除";
                    result.Result = res;
                }
            }
            return result;
        }
        [HttpGet]
        public ApiResult GetDoctor(long id)
        {
            ApiResult result = new ApiResult();
            if(id <1)
            {
                result.Msg = "该医生不存在";
            }
            else
            {
                string msg = string.Empty;
                var res = doctorService.GetDoctor(id, ref msg);
                if(!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess= true;
                    result.Result = res;
                }
            }
            return result;
        }
        [HttpPost]
        public ApiResult UpdateDoctor(DoctorReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.name) || req.department_id < 1 || string.IsNullOrEmpty(req.bio) || string.IsNullOrEmpty(req.title)||req.doctor_id<1||req.user_id<1||string.IsNullOrEmpty(req.avatar_url))
            {
                result.Msg = "参数不能为空";
            }
            else { 
                string msg = string.Empty; 
                var res = doctorService.UpdateDoctor(req,ref msg);
                if(!string.IsNullOrEmpty(msg))
                {
                    result.Msg= msg;
                }
                else
                {
                    result.IsSuccess= true;
                    result.Msg = "已成功修改";
                    result.Result = res;
                }
            }
            return result;
        }
    }
}
