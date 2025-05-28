using AutoMapper;
using HospitalRegistrationManagement.Common;
using HospitalRegistrationManagement.Model.Entitys;
using HospitalRegistrationManagement.Service.Rating.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Rating
{
    public class RatingService : IRatingService
    {
        private readonly IMapper mapper;
        public RatingService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public RatingRes CreateRating(RatingReq req, ref string msg)
        {
            var rating = DbContext.db.Queryable<Ratings>().First(p => p.registration_id == req.registration_id && p.is_active);
            if (rating != null)
            {
                msg = "该记录已有评价";
                return mapper.Map<RatingRes>(rating);
            }
            try
            {
                Ratings ratings = mapper.Map<Ratings>(req);
                ratings.create_at = DateTime.Now;
                bool ratingSuccess = DbContext.db.Insertable(ratings).ExecuteCommand() > 0;
                var registration = DbContext.db.Queryable<Registrations>().First(p => p.registration_id == req.registration_id && p.is_active);
                registration.status = 3;
                bool registrationSuccess=DbContext.db.Updateable<Registrations>(registration).ExecuteCommand()>0;
                double ratingSum = (double)DbContext.db.Queryable<Ratings>().LeftJoin<Registrations>((ra, re) => ra.registration_id == re.registration_id).Where((ra, re) => re.doctor_id == registration.doctor_id&&ra.is_active).Avg((ra, re) => ra.score);
                var docTemp = DbContext.db.Queryable<Doctors>().First(p => p.doctor_id == registration.doctor_id&&p.is_active);
                docTemp.average_rating = (decimal)ratingSum;
                bool doctorSuccess = DbContext.db.Updateable<Doctors>(docTemp).ExecuteCommand() > 0;
                if (ratingSuccess && doctorSuccess && registrationSuccess)
                {
                    rating = DbContext.db.Queryable<Ratings>().First(p => p.registration_id == req.registration_id && p.is_active);
                    RatingRes res = mapper.Map<RatingRes>(rating);
                    return res;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return new RatingRes();
        }

        public bool DeleteRating(long id, ref string msg)
        {
            var rating = DbContext.db.Queryable<Ratings>().First(p => p.rating_id == id);
            if (rating == null)
            {
                msg = "找不到该评价";
            }
            else
            {
                try
                {
                    rating.is_active = false;
                    bool ratingSuccess = DbContext.db.Updateable(rating).ExecuteCommand() > 0;
                    if (ratingSuccess)
                    {
                        return true;
                    }
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return false;
        }

        public List<RatingRes> GetRatingAll(long id, ref string msg)
        {
            var rating = DbContext.db.Queryable<Ratings>().RightJoin<Registrations>((ra, re) => ra.registration_id == re.registration_id).Where((ra, re) => ra.is_active && re.patient_id == id).ToList();
            if (rating == null)
            {
                msg = "找不到该记录";
            }
            else
            {
                List<RatingRes> res = mapper.Map<List<RatingRes>>(rating);
                return res;
            }
            return [];
        }

        public RatingRes UpdateRating(RatingReq req, ref string msg)
        {
            var rating = DbContext.db.Queryable<Ratings>().First(p => p.registration_id == req.registration_id && p.is_active);
            if (rating == null)
            {
                msg = "该科室不存在";
            }
            else
            {
                try
                {
                    var ratingTemp = mapper.Map<Ratings>(req);
                    ratingTemp.rating_id = rating.rating_id;
                    ratingTemp.create_at = rating.create_at;
                    bool ratingSuccess = DbContext.db.Updateable<Ratings>(ratingTemp).ExecuteCommand() > 0;
                    if (ratingSuccess)
                    {
                        rating = DbContext.db.Queryable<Ratings>().First(p => p.registration_id == req.registration_id && p.is_active);
                        RatingRes res = mapper.Map<RatingRes>(rating);
                        return res;
                    }
                }
                catch (Exception ex) { msg = ex.Message; }
            }
            return new RatingRes();
        }
    }
}
