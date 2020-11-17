using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineShopBackEnd.Model
{
   public class OrderDetailsModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public DateTime addedOn { get; set; }
        public int ProdQnty { get; set; }
        public string note { get; set; }
        public double price { get; set; }
    }
}
