using AutoMapper;
using HospitalRegistrationManagement.Model;
using HospitalRegistrationManagement.Service.MedicalRecord;
using HospitalRegistrationManagement.Service.MedicalRecord.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        public IMedicalRecordService mrService;
        public MedicalRecordController(IMedicalRecordService mrService)
        {
            this.mrService = mrService;
        }
        [HttpPost]
        public ApiResult CreateMedicalRecord(MedicalRecordReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.diagnosis) || string.IsNullOrEmpty(req.prescription) || req.registration_id < 1)
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res = mrService.CreateMedicalRecord(req,ref msg);
                if(!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Msg = "创建成功";
                    result.Result = res;
                }
            }
            return result;
        }
        [HttpGet]
        public ApiResult DeleteMedicalRecord(long id)
        {
            ApiResult result=new ApiResult();
            if (id < 1)
            {
                result.Msg = "该记录不存在";
            }
            else
            {
                string msg = string.Empty;
                var res = mrService.DeleteMedicalRecord(id,ref msg);
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
        public ApiResult GetMedicalRecordByPid(long id)
        {
            ApiResult result = new ApiResult();
            if (id < 1)
            {
                result.Msg = "该记录不存在";
            }
            else
            {
                string msg = string.Empty;
                var res = mrService.GetMedicalRecordByPid(id,ref msg);
                result.IsSuccess=true;
                result.Result= res;
            }
            return result ;
        }
        [HttpPost]
        public ApiResult UpdateMedicalRecord(MedicalRecordReq req)
        {
            ApiResult result=new ApiResult();
            if (string.IsNullOrEmpty(req.diagnosis) || string.IsNullOrEmpty(req.prescription) || req.registration_id < 1)
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res =mrService.UpdateMedicalRecord(req,ref msg);
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
    }
}
