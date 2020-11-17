using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicineShopBackEnd.Model
{
   public class ProductModel
    {
        [Key]
        public int productId { get; set; }
        public string productName { get; set; }
        public string productDesc { get; set; }
        public string productImage { get; set; }
        public string productBrand { get; set; }
        public double productPrice { get; set; }
        public string productWeight { get; set; }
        public int categoryId { get; set; } = 0;
    }
}
