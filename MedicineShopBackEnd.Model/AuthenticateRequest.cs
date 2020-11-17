using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicineShopBackEnd.Model
{
  public class AuthenticateRequest
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
