using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicineShopBackEnd.Model
{
   public class NotificationModel
    {
        [Key]
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; } = 0; // Default value is 0, 0 means Notification is for Admin user 
        public int NotificationType { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
        
    }
}
