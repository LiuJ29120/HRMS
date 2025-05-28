using HospitalRegistrationManagement.Model;
using HospitalRegistrationManagement.Service.Rating;
using HospitalRegistrationManagement.Service.Rating.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        public IRatingService ratingService;
        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }
        [HttpPost]
        public ApiResult CreateRation(RatingReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.comment) || req.registration_id < 1 || req.score < 1)
            {
                result.Msg = "参数不能为空";
            }
            else if (req.score > 5)
            {
                result.Msg = "请选择1-5范围内的评分";
            }
            else
            {
                string msg = string.Empty;
                var res = ratingService.CreateRating(req, ref msg);
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
        [HttpPost]
        public ApiResult UpdateRating(RatingReq req)
        {
            ApiResult result = new ApiResult();
            if (string.IsNullOrEmpty(req.comment) || req.registration_id < 1 || req.score < 1)
            {
                result.Msg = "参数不能为空";
            }
            else
            {
                string msg = string.Empty;
                var res = ratingService.UpdateRating(req, ref msg);
                if (!string.IsNullOrEmpty(msg))
                {
                    result.Msg = msg;
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
        [HttpGet]
        public ApiResult DeleteRating(long id)
        {
            ApiResult result = new ApiResult();
            if (id < 1)
            {
                result.Msg = "非法的传递参数";
            }
            else
            {
                string msg = string.Empty;
                var res = ratingService.DeleteRating(id, ref msg);
                if(!string.IsNullOrEmpty(msg))
                {
                    result.Msg=msg;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Msg = "已成功删除";
                    result.Result = res;
                }
            }
            return result ;
        }
        [HttpGet]
        public ApiResult GetRatingAll(long id)
        {
            ApiResult result = new ApiResult();
            if (id < 1)
            {
                result.Msg = "非法的传递参数";
            }
            else
            {
                string msg = string.Empty;
                var res = ratingService.GetRatingAll(id, ref msg);
                if (!string.IsNullOrEmpty(msg))
                {
                    result.Msg=msg;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Result = res;
                }
            }
            return result ;
        }
    }
}
