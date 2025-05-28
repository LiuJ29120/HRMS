using HospitalRegistrationManagement.Model;
using HospitalRegistrationManagement.Service.Department;
using HospitalRegistrationManagement.Service.Department.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        IDepartmentService departmentService;
        public DepartmentController(IDepartmentService _departmentService)
        {
            departmentService = _departmentService;
        }
        [HttpPost]
        public ApiResult CreateDepartment(DepartmentReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.name) || string.IsNullOrEmpty(req.description))
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res = departmentService.CreateDepartment(req, ref msg);
                if (!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Msg = "已成功创建";
                    result.Result = res;
                }
            }
            return result;
        }
        [HttpGet]
        public ApiResult GetDepartmentAll()
        {
            ApiResult result = new ApiResult();
            var res = departmentService.GetDepartmentAll();
            result.IsSuccess = true;
            result.Result = res;
            return result;
        }
        [HttpPost]
        public ApiResult UpdateDepartment(DepartmentReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.name) || string.IsNullOrEmpty(req.description))
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res = departmentService.UpdateDepartment(req, ref msg);
                if(!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Msg = "已成功修改";
                    result.Result = res;
                }
            }
            return result;
        }
        [HttpGet]
        public ApiResult DeleteDepartment(long id)
        {
            ApiResult result = new ApiResult();
            if(id < 1)
            {
                result.Msg = "该科室不存在";
            }
            else
            {
                string msg = string.Empty;
                var res = departmentService.DeleteDepartment(id,ref msg);
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
    }
}
