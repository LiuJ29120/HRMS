using HospitalRegistrationManagement.Service.Tool.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Tool
{
    public interface IToolService
    {
        public List<DoctorReportRes> GetDoctorReport();
    }
}
