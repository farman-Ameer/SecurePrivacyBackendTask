using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineShopBackEnd.Data.Helpers
{
    public static class Constant
    {
        public enum Error { NO_ERROR, RECIEVE_DATA_NULL, ENTITY_EXCEPTION, EXCEPTION, USER_ALREADY_EXIST, INVALID_USER, EMAIL_INVALID, INCORRECT_CURRENT_PASSWORD, GUEST_ALREADY_EXIST, ORDER_NOT_EXIST, PROMO_CODE, INVALID_PERMISSIONS };
        public enum User { NO_USER, ADMIN_USER, AppUser, GUEST_USER, BOTH };
        public enum OrderStatus { NO_STATUS, Pending, InProgress, Delivered, Cancelled };
        public enum UserStatus { UNACTIVE, ACTIVE };
        public enum Status { UNACTIVE, ACTIVE };
        public enum NotificationType { USER_REGISTRATION, NEW_ORDER, ORDER_DELIVERED, ORDER_CANCELLED  };
    }
}
