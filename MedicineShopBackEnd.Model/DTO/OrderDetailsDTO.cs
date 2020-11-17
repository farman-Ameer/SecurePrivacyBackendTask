using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineShopBackEnd.Model.DTO
{
   public class OrderDetailsDTO
    {
        public UserDTO user { get; set; }
        public OrderDTO order { get; set; }
    }
}
