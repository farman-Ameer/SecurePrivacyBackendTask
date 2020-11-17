using MedicineShopBackEnd.Data.Helpers;
using MedicineShopBackEnd.Data.IRepository;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.DTO;
using MedicineShopBackEnd.Model.Pagination;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicineShopBackEnd.Services
{
   public class NotificationService : INotificationService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        private INotificationRepository NotificationRepository;
        private IUserService _userService;
        public NotificationService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment, INotificationRepository _NotificationRepository, IUserService userService)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            NotificationRepository = _NotificationRepository;
            _userService = userService;
        }

        public async Task<ApiResponseResult> SaveNotification(NotificationModel notificationModel)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
              await NotificationRepository.Insert(notificationModel);
                apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                apiResponseResult.message = "Notification Saved Succesfully !";
            }
            catch (Exception exc)
            {
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
                apiResponseResult.message = exc.Message;
            }
            return apiResponseResult;
        }


       public async Task<ApiResponseResult> GetNotifications(int? userId = 0, int? pageNumber = 1, int? pageSize = 10)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            paginationDTO<NotificationModel> paginatedNotifications = new paginationDTO<NotificationModel>();
            try
            {
                var model = NotificationRepository.GetNotifications(userId, pageNumber, pageSize);
                paginatedNotifications.Paging = model.GetHeader();
                paginatedNotifications.Items = await model.List;
                apiResponseResult.data = paginatedNotifications;
                apiResponseResult.message = "Notifications Get Successfully !";
                apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                apiResponseResult.status = true;
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
