using AutoMapper;
using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model.Entitys;
using HospitalRegistrationManagement.Service.Schedule.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Schedule
{
    public class ScheduleService : IScheduleService
    {
        IMapper mapper;
        public ScheduleService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public ScheduleRes CreateSchedule(ScheduleReq req, ref string msg)
        {
            var schedule = DbContext.db.Queryable<Schedules>().First(p => p.doctor_id == req.doctor_id && p.date.ToString("yyyy-MM-dd") == req.date.ToString("yyyy-MM-dd") && p.shift_type == req.shift_type && p.is_active);
            if (schedule != null)
            {

                msg = "该医生已排此班";
                return mapper.Map<ScheduleRes>(schedule);
            }
            else
            {
                try
                {
                    Schedules schedules = mapper.Map<Schedules>(req);
                    bool scheduleSuccess = DbContext.db.Insertable(schedules).ExecuteCommand() > 0;
                    if (scheduleSuccess)
                    {
                        schedule = DbContext.db.Queryable<Schedules>().First(p => p.schedule_id == req.schedule_id && p.is_active);
                        ScheduleRes res = mapper.Map<ScheduleRes>(schedule);
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            return new ScheduleRes();
        }

        public bool DeleteSchedule(long id, ref string msg)
        {
            var schedule = DbContext.db.Queryable<Schedules>().First(p => p.schedule_id == id);
            if (schedule == null)
            {
                msg = "找不到该排班";
            }
            else
            {
                try
                {
                    schedule.is_active = false;
                    bool scheduleSucess = DbContext.db.Updateable(schedule).ExecuteCommand() > 0;
                    if (scheduleSucess)
                    {
                        return true;
                    }
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return false;
        }


        public List<ScheduleRes> GetScheduleAll()
        {
            var res = DbContext.db.Queryable<Schedules>().Where(p => p.is_active).ToList();
            if (res != null)
            {
                return mapper.Map<List<ScheduleRes>>(res);
            }
            return new List<ScheduleRes>();
        }

        public List<ScheduleRes> GetScheduleToday(DateTime date, ref string msg)
        {
            var res = DbContext.db.Queryable<Schedules>().Where(p => p.is_active && p.date.ToString("yyyy-MM-dd")==date.ToString("yyyy-MM-dd")).ToList();
            if (res != null)
            {
                return mapper.Map<List<ScheduleRes>>(res);
            }
            else
            {
                msg = "找不到今天的排班，请联系管理员";
            }
            return new List<ScheduleRes>();
        }

        public ScheduleRes UpdateSchedule(ScheduleReq req, ref string msg)
        {
            var schedule = DbContext.db.Queryable<Schedules>().First(p => p.schedule_id==req.schedule_id&& p.is_active);
            if (schedule == null)
            {
                msg = "该排班不存在";
            }
            else
            {
                try
                {
                    var scheduleTemp = mapper.Map<Schedules>(req);
                    scheduleTemp.schedule_id = schedule.schedule_id;
                    bool scheduleSuccess = DbContext.db.Updateable(scheduleTemp).ExecuteCommand() > 0;
                    if (scheduleSuccess)
                    {
                        schedule = DbContext.db.Queryable<Schedules>().First(p => p.schedule_id == scheduleTemp.schedule_id);
                        ScheduleRes res = mapper.Map<ScheduleRes>(schedule);
                        return res;
                    }
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return new ScheduleRes();
        }
    }
}
