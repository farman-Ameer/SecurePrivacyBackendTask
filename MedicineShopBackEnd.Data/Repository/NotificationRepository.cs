using MedicineShopBackEnd.Data.IRepository;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicineShopBackEnd.Data.Repository
{
   public class NotificationRepository : UnitOfWork<NotificationModel>, INotificationRepository
    {
        private readonly MedicineShopDbContext _context;
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        private IUserRepository userRepository;
        public NotificationRepository(MedicineShopDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUserRepository _userRepository) : base(context, configuration, httpContextAccessor)
        {
            this._context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            userRepository = _userRepository;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public PagedList<NotificationModel> GetNotifications(int? userId = 0, int? pageNumber = 1, int? pageSize = 10)
        {
            PagingParamsDTO pagingParams = new PagingParamsDTO();
            pagingParams.PageNumber = pageNumber;
            pagingParams.PageSize = pageSize;
            try
            {
                var query = _context.Notifications.Where(x => x.UserId == userId);
                return new PagedList<NotificationModel>(
                     query, pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

    }
}
