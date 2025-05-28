using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model;
using HospitalRegistrationManagement.Service.Schedule;
using HospitalRegistrationManagement.Service.Schedule.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        public IScheduleService scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }
        [HttpPost]
        public ApiResult CreateSchedule(ScheduleReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.room_number) || req.doctor_id < 1 || req.shift_type < 0 || req.shift_type > 1 || string.IsNullOrEmpty(req.date.ToString()))
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res = scheduleService.CreateSchedule(req, ref msg);
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
        public ApiResult DeleteSchedule(long id)
        {
            ApiResult result = new ApiResult();
            if (id < 1)
            {
                result.Msg = "非法的传递参数";
            }
            else
            {
                string msg = string.Empty;
                var res = scheduleService.DeleteSchedule(id, ref msg);
                if (!string.IsNullOrEmpty(msg))
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
        [HttpGet]
        public ApiResult GetScheduleAll()
        {
            ApiResult result = new ApiResult();
            var res = scheduleService.GetScheduleAll();
            result.IsSuccess = true;
            result.Result = res;
            return result;
        }
        [HttpGet]
        public ApiResult GetScheduleToday(DateTime date)
        {
            ApiResult result = new ApiResult();
            if (date < DateTime.MinValue)
            {
                result.Msg = "请输入合法的参数";
            }
            else
            {
                string msg = string.Empty;
                var res = scheduleService.GetScheduleToday(date, ref msg);
                if (!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Msg = "查找成功";
                    result.Result = res;
                }
            }
            return result;
        }
        [HttpPost]
        public ApiResult UpdateSchedule(ScheduleReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.room_number) || req.doctor_id < 1 || req.shift_type < 0 || req.shift_type > 1 || string.IsNullOrEmpty(req.date.ToString()))
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res = scheduleService.UpdateSchedule(req, ref msg);
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
