using HospitalRegistrationManagement.Service.Doctor.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Doctor
{
    public interface IDoctorService
    {
        public DoctorRes CreateDoctor(CreateDoctorReq req, ref string msg);
        public List<DoctorRes> GetDoctorAll();
        public DoctorRes UpdateDoctor(DoctorReq req, ref string msg);
        public bool DeteleDoctor(long id , ref string msg);
        public DoctorRes GetDoctor(long id , ref string msg);
    }
}
