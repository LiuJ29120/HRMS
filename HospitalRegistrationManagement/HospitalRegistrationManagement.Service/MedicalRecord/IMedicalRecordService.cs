using HospitalRegistrationManagement.Service.MedicalRecord.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.MedicalRecord
{
    public interface IMedicalRecordService
    {
        public MedicalRecordRes CreateMedicalRecord(MedicalRecordReq req,ref string msg);
        public bool DeleteMedicalRecord(long id,ref string msg);
        public MedicalRecordRes UpdateMedicalRecord(MedicalRecordReq req ,ref string msg);
        public List<MedicalRecordRes> GetMedicalRecordByPid(long id,ref string msg);
    }
}
