using AutoMapper;
using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model.Entitys;
using HospitalRegistrationManagement.Service.RegistrationSource.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.RegistrationSource
{
    public class RegistrationSourceService : IRegistrationSourceService
    {
        private readonly IMapper mapper;
        public RegistrationSourceService(IMapper _mapper) { 
            mapper = _mapper;
        }
        public RegistrationSourceRes CreateRegistrationSource(RegistrationSourceReq req ,ref string msg)
        {
            var rs = DbContext.db.Queryable<RegistrationSources>().First(p => p.department_id == req.department_id && p.date.ToString("yyyy-MM-dd") == req.date.ToString("yyyy-MM-dd") && p.is_active);
            if (rs != null) {
                msg = "该科室已安排号源";
                return mapper.Map<RegistrationSourceRes>(rs);
            }
            else
            {
                try
                {
                    RegistrationSources sources = mapper.Map<RegistrationSources>(req);
                    sources.available_slots = sources.total_slots;
                    bool sourceSuccess = DbContext.db.Insertable(sources).ExecuteCommand() > 0;
                    if (sourceSuccess)
                    {
                        rs = DbContext.db.Queryable<RegistrationSources>().First(p =>p.source_id==req.source_id&&p.is_active);
                        RegistrationSourceRes res = mapper.Map<RegistrationSourceRes>(rs);
                        return res;
                    }
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return new RegistrationSourceRes();
        }

        public bool DeleteRegistrationSource(long id, ref string msg)
        {
            var rs = DbContext.db.Queryable<RegistrationSources>().First(p=>p.source_id == id);
            if (rs == null)
            {
                msg = "找不到该号源";
            }
            else
            {
                try
                {
                    rs.is_active = false;
                    bool rsSuccess = DbContext.db.Updateable(rs).ExecuteCommand() > 0;
                    if (rsSuccess)
                    {
                        return true;
                    }
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return false;
        }

        public List<RegistrationSourceRes> GetRegistrationSourceAll()
        {
            var res = DbContext.db.Queryable<RegistrationSources>().Where(p=>p.is_active).ToList();
            if (res != null) { 
                return mapper.Map<List<RegistrationSourceRes>>(res);
            }
            return new List<RegistrationSourceRes>();
        }

        public RegistrationSourceRes UpdateRegistrationSource(RegistrationSourceReq req, ref string msg)
        {
            var rs = DbContext.db.Queryable<RegistrationSources>().First(p => p.source_id==req.source_id&& p.is_active);
            if(rs == null)
            {
                msg = "该号源不存在";
            }
            else
            {
                try
                {
                    var rsTemp = mapper.Map<RegistrationSources>(req);
                    rsTemp.source_id = rs.source_id;
                    rsTemp.available_slots=rsTemp.total_slots-(rs.total_slots-rs.available_slots);
                    bool rsSuccess = DbContext.db.Updateable<RegistrationSources>(rsTemp).ExecuteCommand() > 0;
                    if (rsSuccess)
                    {
                        rs = DbContext.db.Queryable<RegistrationSources>().First(p => p.source_id == rsTemp.source_id);
                        RegistrationSourceRes res = mapper.Map<RegistrationSourceRes>(rs);
                        return res;
                    }
                }catch(Exception ex) { msg=ex.Message; }
            }
            return new RegistrationSourceRes();
        }
    }
}
