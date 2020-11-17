using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineShopBackEnd.Model
{
   public class ApiResponseResult
    {
        public int ErrorCode;
        public string message;
        public dynamic data;
        public int statusCode;
        public bool status;

    }
}
