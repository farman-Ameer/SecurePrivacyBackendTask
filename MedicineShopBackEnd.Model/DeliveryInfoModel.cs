using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineShopBackEnd.Model
{
    public class DeliveryInfoModel
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}
