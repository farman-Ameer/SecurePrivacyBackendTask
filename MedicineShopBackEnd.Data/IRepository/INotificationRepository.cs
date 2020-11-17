using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineShopBackEnd.Data.IRepository
{
   public interface INotificationRepository : IUnitOfWork<NotificationModel>, IDisposable
    {
        PagedList<NotificationModel> GetNotifications(int? userId = 0, int? pageNumber = 1, int? pageSize = 10);
    }
}
