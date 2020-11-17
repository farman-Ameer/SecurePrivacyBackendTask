using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace MedicineShopBackEnd.Model.Entities
{
    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userEmail { get; set; }
        public string userPhone { get; set; }
        public string userAddress { get; set; }
        public string userProfileImage { get; set; }
        public string userZipCode { get; set; }
        public string promoCode { get; set; }
        public DateTime joiningDate { get; set; }
        public int roleID { get; set; }
        public string street { get; set; }
        public string apartment { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public bool isActive { get; set; }
        public string contact { get; set; }

        //[JsonIgnore]
        public string userPassword { get; set; }
    }
}
