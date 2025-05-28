using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Model
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; } = false;
        public object Result { get; set; }
        public string Msg { get; set; }
    }
}
