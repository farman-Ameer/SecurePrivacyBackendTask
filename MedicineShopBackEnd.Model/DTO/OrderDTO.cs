using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineShopBackEnd.Model.DTO
{
   public class OrderDTO : OrdersModel
    {
        public DeliveryInfoDTO deliveryInfo { get; set; }
        public List<OrderLineDTO> orderLines { get; set; }
        public string orderByUser;
    }
}
