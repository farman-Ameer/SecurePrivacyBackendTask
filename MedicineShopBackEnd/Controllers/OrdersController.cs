using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.DTO;
using MedicineShopBackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShopBackEnd.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrdersController(IOrderService _orderService)
        {
            orderService = _orderService;
        }


        [Route("OrderShipment")]
        [HttpPost]
        public async Task<ApiResponseResult> OrderShipment(OrderShipmentDTO orders)
        {
            return await orderService.OrderShipment(orders);
        }
        [Route("GetOrders")]
        [HttpGet]
        public async Task<ApiResponseResult> GetOrders(int? userId = 0, int? pageNumber = 1, int? pageSize = 10)
        {
            return await orderService.GetOrders(userId, pageNumber, pageSize);
        }

        [Route("GetOrderDetails")]
        [HttpGet]
        public async Task<ApiResponseResult> GetOrderDetails(int orderId)
        {
            return await orderService.GetOrdersDetails(orderId);
        }

    }
}