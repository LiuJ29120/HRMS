using AutoMapper;
using HospitalRegistrationManagement.Model.Entitys;
using HospitalRegistrationManagement.Service.Department.Dto;
using HospitalRegistrationManagement.Service.Doctor.Dto;
using HospitalRegistrationManagement.Service.MedicalRecord.Dto;
using HospitalRegistrationManagement.Service.Patient.Dto;
using HospitalRegistrationManagement.Service.Rating.Dto;
using HospitalRegistrationManagement.Service.Registration.Dto;
using HospitalRegistrationManagement.Service.RegistrationSource.Dto;
using HospitalRegistrationManagement.Service.Schedule.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service
{
    //在构造函数配置映射关系
    public class AutoMapperConfigs : Profile
    {
        public AutoMapperConfigs()
        {
            //从A=>B 
            CreateMap<Users, UserRes>();
            CreateMap<RegisterReq, Users>();
            CreateMap<PatientReq, Patients>();
            CreateMap<Patients, PatientRes>();
            CreateMap<DepartmentReq, Departments>();
            CreateMap<Departments, DepartmentRes>();
            CreateMap<Doctors, DoctorRes>();
            CreateMap<CreateDoctorReq, Doctors>();
            CreateMap<DoctorReq, Doctors>();
            CreateMap<CreateDoctorReq, Users>();
            CreateMap<RegistrationSourceReq, RegistrationSources>();
            CreateMap<RegistrationSources, RegistrationSourceRes>();
            CreateMap<RegistrationReq, Registrations>();
            CreateMap<Registrations, RegistrationRes>();
            CreateMap<MedicalRecordReq, MedicalRecords>();
            CreateMap<MedicalRecords, MedicalRecordRes>();
            CreateMap<RatingReq, Ratings>();
            CreateMap<Ratings, RatingRes>();
            CreateMap<ScheduleReq, Schedules>();
            CreateMap<Schedules, ScheduleRes>();
        }
    }
}
