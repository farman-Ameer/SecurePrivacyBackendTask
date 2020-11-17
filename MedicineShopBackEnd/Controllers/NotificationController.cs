using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Services;
using MedicineShopBackEnd.Services.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShopBackEnd.Controllers
{
    [Route("api/Notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;
        public NotificationController(INotificationService _notificationService)
        {
            notificationService = _notificationService;
        }

        [Route("SaveNotification")]
        [HttpPost]
        public async Task<ApiResponseResult> SaveNotification(NotificationModel notificationModel)
        {
            return await notificationService.SaveNotification(notificationModel);
        }

        [Route("GetNotifications")]
        [HttpGet]
        public async Task<ApiResponseResult> GetNotifications(int? userId = 0, int? pageNumber = 1, int? pageSize = 10)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                apiResponseResult = await notificationService.GetNotifications(userId, pageNumber, pageSize);
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
                apiResponseResult.status = false;
            }
            return apiResponseResult;
        }
    }
}