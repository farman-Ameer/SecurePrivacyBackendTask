using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.Pagination;

namespace MedicineShopBackEnd.Data.IRepository
{
   public interface IProductRepository : IUnitOfWork<ProductModel>, IDisposable
    {
        PagedList<ProductModel> GetProducts(int? pageNumber = 1, int? pageSize = 10);
        PagedList<ProductModel> GetProductsByCategoryId(int categoryId, int? pageNumber = 1, int? pageSize = 10);
        Task<double> GetProductPrice(int productId);
        Task<ApiResponseResult> UpdateProduct(ProductModel product);
        Task<ApiResponseResult> DeleteProduct(int productId);
        PagedList<ProductModel> SearchProduct(string search, int? categoryId = 0, int? pageNumber = 1, int? pageSize = 10);
    }
}
