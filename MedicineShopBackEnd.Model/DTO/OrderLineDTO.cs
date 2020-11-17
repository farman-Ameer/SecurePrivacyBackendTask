using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineShopBackEnd.Model.DTO
{
    public class OrderLineDTO : ProductModel
    {
        public int quantity { get; set; }
        public string category { get; set; }
    }
}
