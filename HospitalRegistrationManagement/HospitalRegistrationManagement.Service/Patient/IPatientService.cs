using HospitalRegistrationManagement.Service.Patient.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Patient
{
    public interface IPatientService
    {
        public PatientRes GetPatientByUid(long id, ref string msg);
        public PatientRes GetPatientByPid(long id, ref string msg);
        public bool DeletePatient(long id,ref string msg);
        public PatientRes UpdatePatient(PatientReq  req,ref string msg);
    }
}
