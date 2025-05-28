using HospitalRegistrationManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public ApiResult SetAvatar(IFormFile file)
        {
            ApiResult result = new ApiResult();
            if (file == null || file.Length == 0)
            {
                result.IsSuccess = false;
                result.Msg = "没有上传文件!";
            }

            // 获取文件扩展名
            var fileExtension = Path.GetExtension(file.FileName).TrimStart('.');

            // 生成唯一的文件名
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileNameWithoutExtension(file.FileName).CleanFileName() + "." + fileExtension;
            //文件保存路径
            var filePath = Path.Combine("D:\\Project\\graduation\\HospitalRegistrationManagementUI\\public\\image\\Avatar", uniqueFileName);
            // 检查文件夹是否存在，如果不存在则创建
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            //保存文件
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Msg = ex.Message;
            }
            var url = $"/public/image/Avatar/{uniqueFileName}";
            result.IsSuccess = true;
            result.Result = url;
            return result;
        }
    }

    // 扩展方法，用于清理文件名
    public static class StringExtensions
    {
        public static string CleanFileName(this string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), "_"));
        }
    }
}
