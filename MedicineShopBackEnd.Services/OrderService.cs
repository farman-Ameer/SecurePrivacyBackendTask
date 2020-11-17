using MedicineShopBackEnd.Data.Helpers;
using MedicineShopBackEnd.Data.IRepository;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicineShopBackEnd.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        private IOrdersRepository orderRepository;
        private IUserService _userService;
        public OrderService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment, IOrdersRepository _orderRepository, IUserService userService)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            orderRepository = _orderRepository;
            _userService = userService;
        }
        public async Task<ApiResponseResult> OrderShipment(OrderShipmentDTO order)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            int newOrderId;
            try
            {
                if (order.guestInfo.email != "" && order.guestInfo.email != null)
                {
                    apiResponseResult = await _userService.SaveGuestInfo(order.guestInfo, order.address);
                    if (apiResponseResult.ErrorCode != (int)Constant.Error.NO_ERROR)
                    {
                        return apiResponseResult;
                    }
                    order.userID = apiResponseResult.data.userId;
                }
                else
                {
                  //  await _userService.UpdateUserAddress(order.userID, order.address);
                }

                newOrderId = await orderRepository.SaveOrder(order);
                await orderRepository.SaveOrderLines(newOrderId, order.products);
                apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                apiResponseResult.message = "Order Sucessfully Saved";

            }
            catch (Exception exc)
            {
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
                apiResponseResult.message = exc.Message;
            }
            return apiResponseResult;
        }
        public async Task<ApiResponseResult> GetOrders(int? userId = 0, int? pageNumber = 1, int? pageSize = 10)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            paginationDTO<OrderDTO> paginatedorders = new paginationDTO<OrderDTO>();
            try
            {
                var model = orderRepository.GetOrders(userId,pageNumber, pageSize);
                paginatedorders.Paging = model.GetHeader();
                paginatedorders.Items = await model.List;
                apiResponseResult.data = paginatedorders;
                apiResponseResult.message = "Orders Get Successfully !";
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
        public async Task<ApiResponseResult> GetOrdersDetails(int orderId)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                apiResponseResult.data = await orderRepository.GetOrdersDetails(orderId);
                apiResponseResult.message = "Order Details Get Successfully";
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
