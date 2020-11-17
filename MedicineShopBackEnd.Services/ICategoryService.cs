using MedicineShopBackEnd.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicineShopBackEnd.Services
{
   public interface ICategoryService
    {
        Task<bool> AddCategory(CategoryModel categoryModel);
        Task<ApiResponseResult> AddCategory(Dictionary<string, string> requestKeys, IFormFile file = null);
        Task<ApiResponseResult> GetAllCategoriesPaginated(string routname, int? pageNumber = 1, int? pageSize = 10);
        Task<ApiResponseResult> UpdateCategory(Dictionary<string, string> requestKeys, IFormFile file = null);
        Task<ApiResponseResult> DeleteCategory(int categoryId);
        Task<ApiResponseResult> GetCategoryById(int categoryId);
    }
}
