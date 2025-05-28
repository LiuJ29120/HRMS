using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model.Entitys;
using HospitalRegistrationManagement.Service.Tool.Dto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Tool
{
    public class ToolService : IToolService
    {
        public List<DoctorReportRes> GetDoctorReport()
        {
            var list = DbContext.db.Queryable<Doctors>().LeftJoin<Departments>((doc, dep) => doc.department_id == dep.department_id).LeftJoin<Registrations>((doc, dep, reg) => doc.doctor_id == reg.doctor_id).Where((doc, dep, reg) => reg.create_at.Date == DateTime.Now.Date && reg.is_active && (reg.status == 2 || reg.status == 3)).GroupBy((doc, dep, reg) => new { doc.doctor_id, DoctorName=doc.name, DepatementName=dep.name }).Select((doc,dep,reg) => new DoctorReportRes
            {
                DoctorName = doc.name,
                DepartmentName = dep.name,
                RegistrationNumber = SqlFunc.AggregateCount(reg.registration_id)
            }).ToList();
            return list;
        }
    }
}
