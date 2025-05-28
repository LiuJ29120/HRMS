using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model;
using HospitalRegistrationManagement.Service.Tool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace HospitalRegistrationManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        public  IToolService _toolService;
        public ToolsController(IToolService toolService)
        {
            _toolService = toolService;
        }
        [HttpGet]
        public void InitDataBase()
        {
            DbContext.InitDataBase();
        }
        [HttpGet]
        public IActionResult DownloadReport()
        {
            // 创建内存中的Excel包
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // 添加表头
                worksheet.Cells[1, 1].Value = "医生姓名";
                worksheet.Cells[1, 2].Value = "科室名称";
                worksheet.Cells[1, 3].Value = "挂号数量";

                // 示例数据
                var data = _toolService.GetDoctorReport();

                int row = 2; // 从第二行开始，第一行为表头
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.DoctorName;
                    worksheet.Cells[row, 2].Value = item.DepartmentName;
                    worksheet.Cells[row, 3].Value = item.RegistrationNumber;
                    row++;
                }

                // 将Excel内容写入流
                var stream = new MemoryStream();
                package.SaveAs(stream);

                // 返回文件结果
                string fileName = "Report.xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;

                // 设置响应头，避免缓存
                Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                Response.Headers["Pragma"] = "no-cache";
                Response.Headers["Expires"] = "0";

                return File(stream.ToArray(), contentType, fileName);
            }
        }
    }
}
