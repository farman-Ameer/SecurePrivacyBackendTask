using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicineShopBackEnd.Model.DTO
{
   public class OrderShipmentDTO
    {
        public int userID { get; set; }
        [Required]
        public Decimal subTotal { get; set; }
        [Required]
        public Decimal deliveryCharges { get; set; }

        [Required]
        public Decimal total { get; set; }

        public string allProductNote { get; set; }

        public String deliveryInstructions { get; set; }

        public GuestInfoDTO guestInfo { get; set; }
        public AddressDTO address { get; set; }
        public List<OrderLineDTO> products { get; set; }
        //public CreditCard creditCard { get; set; }
        //public DeliveryDateTime deliveryDateTime { get; set; }
        //public ICollection<ViewProduct> Products { get; set; }
    }
}
