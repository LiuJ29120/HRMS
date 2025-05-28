using AutoMapper;
using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model.Entitys;
using HospitalRegistrationManagement.Service.Department.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper mapper;
        public DepartmentService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public DepartmentRes CreateDepartment(DepartmentReq req, ref string msg)
        {
            var department = DbContext.db.Queryable<Departments>().First(p => p.name == req.name&&p.department_id==req.department_id && p.is_active);
            if (department != null)
            {
                msg = "科室已存在";
                return mapper.Map<DepartmentRes>(department);
            }
            else
            {
                try
                {
                    Departments departments = mapper.Map<Departments>(req);
                    bool departmentsSuccess = DbContext.db.Insertable(departments).ExecuteCommand() > 0;
                    if (departmentsSuccess)
                    {
                        department = DbContext.db.Queryable<Departments>().First(p => p.name == req.name && p.department_id == req.department_id && p.is_active);
                        DepartmentRes res = mapper.Map<DepartmentRes>(department);
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            return new DepartmentRes();
        }

        public bool DeleteDepartment(long id, ref string msg)
        {
            var department = DbContext.db.Queryable<Departments>().First(p => p.department_id == id && p.is_active);
            if (department == null)
            {
                msg = "找不到该科室";
            }
            else
            {
                try
                {
                    department.is_active = false;
                    bool departmentsSuccess = DbContext.db.Updateable(department).ExecuteCommand() > 0;
                    if (departmentsSuccess)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            return false;
        }

        public List<DepartmentRes> GetDepartmentAll()
        {
            var department = DbContext.db.Queryable<Departments>().Where(p => p.is_active).ToList();
            if (department != null)
            {
                return mapper.Map<List<DepartmentRes>>(department);
            }
            return new List<DepartmentRes>();
        }

        public DepartmentRes UpdateDepartment(DepartmentReq req, ref string msg)
        {
            var department = DbContext.db.Queryable<Departments>().First(p => p.department_id==req.department_id && p.is_active);
            if (department == null)
            {
                msg = "找不到该科室";
            }
            else
            {
                try
                {
                    Departments departments = mapper.Map<Departments>(req);
                    departments.department_id = department.department_id;
                    bool departmentsSuccess = DbContext.db.Updateable(departments).ExecuteCommand() > 0;
                    if (departmentsSuccess)
                    {
                        department = DbContext.db.Queryable<Departments>().First(p => p.name == req.name && p.is_active);
                        DepartmentRes res = mapper.Map<DepartmentRes>(department);
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            return new DepartmentRes();
        }

    }
}

