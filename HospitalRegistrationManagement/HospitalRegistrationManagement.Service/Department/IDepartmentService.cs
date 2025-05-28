using HospitalRegistrationManagement.Service.Department.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Department
{
    public interface IDepartmentService
    {
        public List<DepartmentRes> GetDepartmentAll();
        public DepartmentRes CreateDepartment(DepartmentReq req, ref string msg);
        public DepartmentRes UpdateDepartment(DepartmentReq req, ref string msg);
        public bool DeleteDepartment(long id,ref string msg);
    }
}
