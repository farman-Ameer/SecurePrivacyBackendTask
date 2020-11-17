using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicineShopBackEnd.Model
{
   public class OrdersModel
    {
        [Key]
        public int OrderId { get; set; }

        public int userID { get; set; }

        public int orderStatus { get; set; }

        public int? shopperId { get; set; }

        [StringLength(255)]
        public string deliveryInstructions { get; set; }

        public int? rating { get; set; }

        public decimal? subTotal { get; set; }

        public decimal? deliveryCharges { get; set; }

        public decimal? totalAmount { get; set; }

        [StringLength(50)]
        public string weightScale { get; set; }

        public decimal? totalTax { get; set; }

        public DateTime? insertedOn { get; set; }

        public string allProductNote { get; set; }

        public string address { get; set; }

        //public virtual User User { get; set; }

        //[JsonIgnore]
        //public virtual OrderStatu OrderStatu { get; set; }

        //public virtual Shopper Shopper { get; set; }


        //// [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //// public virtual ICollection<Rating> Ratings { get; set; }

        //public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
