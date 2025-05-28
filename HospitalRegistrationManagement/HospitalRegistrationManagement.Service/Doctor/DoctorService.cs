using AutoMapper;
using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model.Entitys;
using HospitalRegistrationManagement.Service.Doctor.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Doctor
{
    public class DoctorService : IDoctorService
    {
        private readonly IMapper mapper;
        public DoctorService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public DoctorRes CreateDoctor(CreateDoctorReq req, ref string msg)
        {
            var user = DbContext.db.Queryable<Users>().First(p => p.username == req.username && p.password == req.password && p.is_active);
            if (user != null)
            {
                if (user.role == 0)
                {
                    msg = "该账号为管理员账号";
                }
                else if (user.role == 1)
                {
                    msg = "该账号为患者账号";
                }
                else
                {
                    msg = "账号已存在";
                    var DoctorTemp = DbContext.db.Queryable<Doctors>().First(p => p.user_id == user.user_id && p.is_active);
                    return mapper.Map<DoctorRes>(DoctorTemp);

                }
            }
            else
            {
                var Doctor = DbContext.db.Queryable<Doctors>().First(p => p.name == req.name && p.department_id == req.department_id && p.bio == req.bio && p.is_active);
                if (Doctor != null)
                {
                    msg = "该医生已注册";
                    return mapper.Map<DoctorRes>(Doctor);
                }
                else
                {
                    try
                    {
                        Users users = mapper.Map<Users>(req);
                        users.role = 2;
                        users.create_at = DateTime.Now;
                        bool userSuccess = DbContext.db.Insertable(users).ExecuteCommand() > 0;
                        if (userSuccess)
                        {
                            Doctors doctors = mapper.Map<Doctors>(req);
                            doctors.average_rating = 0;
                            user = DbContext.db.Queryable<Users>().First(p => p.username == req.username && p.password == req.password && p.is_active);
                            doctors.user_id = user.user_id;
                            bool DoctorSuccess = DbContext.db.Insertable(doctors).ExecuteCommand() > 0;
                            if (DoctorSuccess)
                            {
                                Doctor = DbContext.db.Queryable<Doctors>().First(p => p.name == req.name && p.department_id == req.department_id && p.user_id == user.user_id && p.is_active);
                                DoctorRes res = mapper.Map<DoctorRes>(Doctor);
                                return res;
                            }
                            else
                            {
                                msg = "医生信息导入失败";
                            }
                        }
                        else
                        {
                            msg = "账号注册失败";
                        }

                    }
                    catch (Exception ex) { msg = ex.Message; }
                }
            }
            return new DoctorRes();
        }

        public bool DeteleDoctor(long id, ref string msg)
        {
            var doctor = DbContext.db.Queryable<Doctors>().First(p => p.doctor_id == id);
            if (doctor == null)
            {
                msg = "找不到该医生";
            }
            else
            {
                try
                {
                    doctor.is_active = false;
                    bool doctorSuccess = DbContext.db.Updateable(doctor).ExecuteCommand() > 0;
                    if (doctorSuccess)
                    {
                        return true;
                    }
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return false;
        }

        public DoctorRes GetDoctor(long id, ref string msg)
        {
            var doctor = DbContext.db.Queryable<Doctors>().First(p => p.user_id == id);
            if (doctor == null)
            {
                msg = "找不到该医生";
            }
            else
            {
                DoctorRes res = mapper.Map<DoctorRes>(doctor);
                return res;
            }
            return new DoctorRes();
        }

        public List<DoctorRes> GetDoctorAll()
        {
            var Doctor = DbContext.db.Queryable<Doctors>().Where(p => p.is_active).ToList();
            if (Doctor != null)
            {
                return mapper.Map<List<DoctorRes>>(Doctor);
            }
            return [];
        }

        public DoctorRes UpdateDoctor(DoctorReq req, ref string msg)
        {
            var Doctor = DbContext.db.Queryable<Doctors>().First(p => p.doctor_id == req.doctor_id && p.is_active);
            if (Doctor == null)
            {
                msg = "该医生不存在";
            }
            else
            {
                try
                {
                    var DoctorTemp = mapper.Map<Doctors>(req);
                    DoctorTemp.average_rating = Doctor.average_rating;
                    bool DoctorSuccess = DbContext.db.Updateable(DoctorTemp).ExecuteCommand() > 0;
                    if (DoctorSuccess)
                    {
                        Doctor = DbContext.db.Queryable<Doctors>().First(p => p.doctor_id == req.doctor_id);
                        DoctorRes res = mapper.Map<DoctorRes>(Doctor);
                        return res;
                    }
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return new DoctorRes();
        }
    }
}
