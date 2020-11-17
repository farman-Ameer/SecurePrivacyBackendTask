using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicineShopBackEnd.Model
{
   public class CategoryModel
    {
        [Key]
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public string categoryImage { get; set; }
        public string categoryDesc { get; set; }
    }
}
