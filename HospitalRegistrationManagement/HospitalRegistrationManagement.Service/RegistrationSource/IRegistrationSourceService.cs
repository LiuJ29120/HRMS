using HospitalRegistrationManagement.Service.RegistrationSource.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.RegistrationSource
{
    public interface IRegistrationSourceService
    {
        public List<RegistrationSourceRes> GetRegistrationSourceAll();
        public RegistrationSourceRes CreateRegistrationSource(RegistrationSourceReq req,ref string msg);
        public RegistrationSourceRes UpdateRegistrationSource(RegistrationSourceReq req,ref string msg);
        public bool DeleteRegistrationSource(long id,ref string msg);
    }
}
