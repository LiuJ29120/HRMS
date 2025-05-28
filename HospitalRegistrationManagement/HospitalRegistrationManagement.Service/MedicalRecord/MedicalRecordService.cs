using AutoMapper;
using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model.Entitys;
using HospitalRegistrationManagement.Service.MedicalRecord.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.MedicalRecord
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMapper mapper;
        public MedicalRecordService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public MedicalRecordRes CreateMedicalRecord(MedicalRecordReq req, ref string msg)
        {
            var medicalRecord = DbContext.db.Queryable<MedicalRecords>().First(p => p.registration_id == req.registration_id && p.is_active);
            if (medicalRecord != null)
            {
                msg = "该号已就诊";
                return mapper.Map<MedicalRecordRes>(medicalRecord);
            }
            else
            {
                try
                {
                    MedicalRecords record = mapper.Map<MedicalRecords>(req);
                    record.visit_time = DateTime.Now;
                    bool recordSuccess = DbContext.db.Insertable(record).ExecuteCommand() > 0;
                    if (recordSuccess)
                    {
                        medicalRecord = DbContext.db.Queryable<MedicalRecords>().First(p => p.registration_id == req.registration_id && p.is_active);
                        MedicalRecordRes res = mapper.Map<MedicalRecordRes>(medicalRecord);
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            return new MedicalRecordRes();
        }

        public bool DeleteMedicalRecord(long id, ref string msg)
        {
            var mr = DbContext.db.Queryable<MedicalRecords>().First(p => p.record_id == id);
            if (mr == null)
            {
                msg = "找不到该记录";
            }
            else
            {
                try
                {
                    mr.is_active = false;
                    bool mrSuccess = DbContext.db.Updateable(mr).ExecuteCommand() > 0;
                    if (mrSuccess)
                    {
                        return true;
                    }
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return false;
        }

        public List<MedicalRecordRes> GetMedicalRecordByPid(long id,ref string msg)
        {
            var mr = DbContext.db.Queryable<MedicalRecords>().RightJoin<Registrations>((mr,r)=>mr.registration_id==r.registration_id).Where((mr,r)=>r.patient_id==id).ToList();
            if(mr != null)
            {
                return mapper.Map<List<MedicalRecordRes>>(mr);
            }
            return [];
        }

        public MedicalRecordRes UpdateMedicalRecord(MedicalRecordReq req, ref string msg)
        {
            var mr = DbContext.db.Queryable<MedicalRecords>().First(p => p.registration_id == req.registration_id && p.is_active);
            if(mr == null)
            {
                msg = "该记录不存在";
            }
            else
            {
                try
                {
                    var mrTemp = mapper.Map<MedicalRecords>(req);
                    mrTemp.record_id = mr.record_id;
                    mrTemp.visit_time = mr.visit_time;
                    bool mrSuccess = DbContext.db.Updateable(mrTemp).ExecuteCommand() > 0;
                    if (mrSuccess)
                    {
                        mr = DbContext.db.Queryable<MedicalRecords>().First(p => p.registration_id == req.registration_id && p.is_active);
                        MedicalRecordRes res = mapper.Map<MedicalRecordRes>(mr);
                        return res;
                    }
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return new MedicalRecordRes();
        }
    }
}
