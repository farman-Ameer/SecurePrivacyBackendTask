using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicineShopBackEnd.Model
{
   public class UserRoleModel
    {
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }
        public int roleId { get; set; }
    }
}
