using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.Pagination;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicineShopBackEnd.Services
{
    public interface INotificationService
    {
        Task<ApiResponseResult> SaveNotification(NotificationModel notificationModel);
        Task<ApiResponseResult> GetNotifications(int? userId = 0, int? pageNumber = 1, int? pageSize = 10);
    }
}
