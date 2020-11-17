using MedicineShopBackEnd.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicineShopBackEnd.Services
{
   public interface IProductService
    {
        Task<ApiResponseResult> AddProduct(Dictionary<string, string> requestKeys, IFormFile file = null);
        Task<ApiResponseResult> GetAllProducts(int? pageNumber = 1, int? pageSize = 10);
        Task<ApiResponseResult> GetProductsByCategoryId(int categoryId, int? pageNumber = 1, int? pageSize = 10);
        Task<ApiResponseResult> UpdateProduct(Dictionary<string, string> requestKeys, IFormFile file = null);
        Task<ApiResponseResult> DeleteProduct(int productId);
        Task<ApiResponseResult> GetProductById(int productId);
        Task<ApiResponseResult> SearchProduct(string search, int? categoryId = 0, int? pageNumber = 1, int? pageSize = 10);
    }
}
