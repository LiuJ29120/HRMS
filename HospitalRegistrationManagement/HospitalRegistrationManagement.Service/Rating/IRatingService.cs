using HospitalRegistrationManagement.Service.Rating.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Service.Rating
{
    public interface IRatingService
    {
        public RatingRes CreateRating(RatingReq req, ref string msg);
        public bool DeleteRating(long id ,ref string msg);
        public RatingRes UpdateRating(RatingReq req, ref string msg);
        public List<RatingRes> GetRatingAll(long id, ref string msg);
    }
}
