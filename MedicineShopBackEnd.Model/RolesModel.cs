using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicineShopBackEnd.Model
{
   public class RolesModel
    {
        [Key]
        public int roleId { get; set; }
        public string roleName { get; set; }
    }
}
