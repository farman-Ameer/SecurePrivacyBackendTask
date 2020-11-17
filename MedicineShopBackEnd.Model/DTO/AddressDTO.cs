using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicineShopBackEnd.Model.DTO
{
   public class AddressDTO
    {
        //public int userID { get; set; }

        [Required]
        [StringLength(50)]
        public string zipCode { get; set; }

        [Required]
        [StringLength(50)]
        public string appartment { get; set; }

        [Required]
        [StringLength(50)]
        public string city { get; set; }

        [Required]
        [StringLength(50)]
        public string state { get; set; }

        [StringLength(50)]
        public string street { get; set; }
    }
}
