using AutoMapper;
using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model.Entitys;
using HospitalRegistrationManagement.Service.Patient.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Patient
{
    public class PatientService : IPatientService
    {
        private readonly IMapper mapper;
        public PatientService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public bool DeletePatient(long id, ref string msg)
        {
            var patient = DbContext.db.Queryable<Patients>().First(p=>p.user_id == id);
            var user = DbContext.db.Queryable<Users>().First(p => p.user_id == id);
            if (patient == null||user==null)
            {
                msg = "找不到该用户";
            }
            else
            {
                try
                {
                    patient.is_active = false;
                    bool patientSuccess = DbContext.db.Updateable(patient).ExecuteCommand() > 0;
                    user.is_active = false;
                    bool userSuccess = DbContext.db.Updateable(user).ExecuteCommand() > 0;
                    if (patientSuccess && userSuccess)
                    {
                        return true;
                    } 
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return false;
        }

        public PatientRes GetPatientByUid(long id,ref string msg)
        {
            var patient = DbContext.db.Queryable<Patients>().First(p => p.user_id == id&&p.is_active);
            if (patient != null)
            {
                return mapper.Map<PatientRes>(patient);
            }
            msg = "找不到信息";
            return new PatientRes();
        }
        public PatientRes GetPatientByPid(long id, ref string msg)
        {
            var patient = DbContext.db.Queryable<Patients>().First(p => p.patient_id == id && p.is_active);
            if (patient != null)
            {
                return mapper.Map<PatientRes>(patient);
            }
            msg = "找不到信息";
            return new PatientRes();
        }

        public PatientRes UpdatePatient(PatientReq req, ref string msg)
        {
            var patient = DbContext.db.Queryable<Patients>().First(p => p.user_id == req.user_id&&p.is_active);
            if (patient == null)
            {
                msg = "找不到该用户";
            }
            else
            {
                try
                {
                    var patientTemp = mapper.Map<Patients>(req);
                    patientTemp.patient_id = patient.patient_id;
                    patientTemp.created_at=patient.created_at;
                    bool patientSuccess = DbContext.db.Updateable(patientTemp).ExecuteCommand() > 0;
                    if(patientSuccess)
                    {
                        patient = DbContext.db.Queryable<Patients>().First(p => p.user_id == req.user_id && p.is_active);
                        PatientRes res = mapper.Map<PatientRes>(patient);
                        return res;
                    }
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return new PatientRes();
        }
    }
}
