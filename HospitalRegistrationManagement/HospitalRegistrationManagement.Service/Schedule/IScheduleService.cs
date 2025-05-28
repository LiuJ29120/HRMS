using HospitalRegistrationManagement.Service.Schedule.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Schedule
{
    public interface IScheduleService
    {
        public ScheduleRes CreateSchedule(ScheduleReq req,ref string msg);
        public List<ScheduleRes> GetScheduleAll();
        public ScheduleRes UpdateSchedule(ScheduleReq req,ref string msg);
        public bool DeleteSchedule(long id ,ref  string msg);
        public List<ScheduleRes> GetScheduleToday(DateTime date,ref  string msg);
    }
}
