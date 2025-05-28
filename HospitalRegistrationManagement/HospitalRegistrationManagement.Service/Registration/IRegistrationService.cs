using HospitalRegistrationManagement.Service.Registration.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Registration
{
    public interface IRegistrationService
    {
        public RegistrationRes CreateRegistration(RegistrationReq req,ref string msg);
        public RegistrationRes OverRegistration(RegistrationReq req,ref string msg,int status);
        public List<RegistrationRes> GetPatientRegistration(long id);
        public List<RegistrationRes> GetDoctorRegistration(long id);
    }
}
