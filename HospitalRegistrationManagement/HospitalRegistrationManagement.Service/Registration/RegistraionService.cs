using AutoMapper;
using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model.Entitys;
using HospitalRegistrationManagement.Service.Registration.Dto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Registration
{
    public class RegistraionService : IRegistrationService
    {
        private readonly IMapper mapper;
        public RegistraionService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public RegistrationRes CreateRegistration(RegistrationReq req, ref string msg)
        {

            try
            {
                var registration = DbContext.db.Queryable<Registrations>().First(p => p.patient_id == req.patient_id && p.doctor_id == req.doctor_id && p.is_active && p.status == 0);
                if (registration != null)
                {
                    msg = "不允许重复挂号";
                    return new RegistrationRes();
                }
                var rs = DbContext.db.Queryable<RegistrationSources>().RightJoin<Doctors>((rs, d) => rs.department_id == d.department_id).Where((rs, d) => rs.is_active && d.is_active && d.doctor_id == req.doctor_id && rs.date.Date == DateTime.Now.Date).First();
                if (rs == null)
                {
                    msg = "该医生所属科室没有安排号源，请联系管理员";
                    return new RegistrationRes();
                }
                if (rs.available_slots <= 0)
                {
                    msg = "该医生所属科室号源已空";
                    return new RegistrationRes();
                }
                rs.available_slots--;
                bool rsSuccess = DbContext.db.Updateable(rs).ExecuteCommand() > 0;
                if (!rsSuccess)
                {
                    msg = "更新号源失败";
                    return new RegistrationRes();
                }
                Registrations registrations = mapper.Map<Registrations>(req);
                registrations.status = 0;
                registrations.create_at = DateTime.Now;
                bool registrationSuccess = DbContext.db.Insertable(registrations).ExecuteCommand() > 0;
                if (registrationSuccess)
                {
                    registration = DbContext.db.Queryable<Registrations>().First(p => p.patient_id == req.patient_id && p.doctor_id == req.doctor_id && p.is_active && p.status == 0);
                    RegistrationRes res = mapper.Map<RegistrationRes>(registration);
                    return res;
                }
                else
                {
                    msg = "挂号记录创建失败";
                    return new RegistrationRes();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return new RegistrationRes();
            }
        }

        public List<RegistrationRes> GetDoctorRegistration(long id)
        {
            var registration = DbContext.db.Queryable<Registrations>().Where(p => p.doctor_id == id && p.is_active && p.status == 0).ToList();
            List<RegistrationRes> res = mapper.Map<List<RegistrationRes>>(registration);
            return res;
        }

        public List<RegistrationRes> GetPatientRegistration(long id)
        {
            var registration = DbContext.db.Queryable<Registrations>().Where(p => p.patient_id == id && p.is_active).OrderBy(p=>p.create_at,OrderByType.Desc).ToList();
            List<RegistrationRes> res = mapper.Map<List<RegistrationRes>>(registration);
            return res;
        }

        public RegistrationRes OverRegistration(RegistrationReq req, ref string msg, int status)
        {
            var registration = DbContext.db.Queryable<Registrations>().First(p => p.patient_id == req.patient_id && p.doctor_id == req.doctor_id && p.is_active && p.status == 0);
            if (registration == null)
            {
                msg = "无法查询到该挂号记录";
                return mapper.Map<RegistrationRes>(registration);
            }
            else
            {
                try
                {
                    registration.status = status;
                    bool registrationSuccess = DbContext.db.Updateable(registration).ExecuteCommand() > 0;
                    bool rsSuccess = true;
                    if (status == 1)
                    {
                        var rs = DbContext.db.Queryable<RegistrationSources>().RightJoin<Doctors>((rs, d) => rs.department_id == d.department_id).RightJoin<Registrations>((rs, d, r) => d.doctor_id == r.doctor_id).Where((rs, d, r) => rs.is_active && r.doctor_id == req.doctor_id && rs.date.Date == DateTime.Now.Date).First();
                        rs.available_slots++;
                        rsSuccess = DbContext.db.Updateable(rs).ExecuteCommand() > 0;
                    }
                    if (registrationSuccess && rsSuccess)
                    {
                        registration = DbContext.db.Queryable<Registrations>().First(p => p.patient_id == req.patient_id && p.doctor_id == req.doctor_id && p.is_active && p.status == 0);
                        RegistrationRes res = mapper.Map<RegistrationRes>(registration);
                        return res;
                    }
                }
                catch (Exception e) { msg = e.Message; }
            }
            return new RegistrationRes();
        }
    }
}
