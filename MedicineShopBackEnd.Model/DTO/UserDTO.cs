using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineShopBackEnd.Model.DTO
{
   public class UserDTO
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userEmail { get; set; }
        public string userPhone { get; set; }
        public string userAddress { get; set; }
        public string userProfileImage { get; set; }
        public string userZipCode { get; set; }
        public UserRoleDTO Role { get; set; }
        //public List<UserRoleDTO> userRoles { get; set; }

    }
}
