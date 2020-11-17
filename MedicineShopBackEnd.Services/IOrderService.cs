using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicineShopBackEnd.Services
{
   public interface IOrderService
    {
        Task<ApiResponseResult> OrderShipment(OrderShipmentDTO order);
        Task<ApiResponseResult> GetOrders(int? userId = 0, int? pageNumber = 1, int? pageSize = 10);
        Task<ApiResponseResult> GetOrdersDetails(int orderId);
    }
}
