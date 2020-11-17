using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.DTO;
using MedicineShopBackEnd.Model.Pagination;
namespace MedicineShopBackEnd.Data.IRepository
{
   public interface IOrdersRepository : IUnitOfWork<OrdersModel>, IDisposable
    {
        Task<int> SaveOrder(OrderShipmentDTO order);
        Task SaveOrderLines(int orderId, List<OrderLineDTO> Products);
        PagedList<OrderDTO> GetOrders(int? userId = 0, int? pageNumber = 1, int? pageSize = 10);
        Task<OrderDetailsDTO> GetOrdersDetails(int orderId);
       // PagedList<OrdersModel> GetOrders(int? pageNumber = 1, int? pageSize = 10);
    }
}
