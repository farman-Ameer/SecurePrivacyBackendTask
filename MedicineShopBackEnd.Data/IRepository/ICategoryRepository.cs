using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.Pagination;

namespace MedicineShopBackEnd.Data.IRepository
{
   public interface ICategoryRepository : IUnitOfWork<CategoryModel>, IDisposable
    {
        PagedList<CategoryModel> GetCategories(int? pageNumber = 1, int? pageSize = 10);
        Task<ApiResponseResult> UpdateCategory(CategoryModel category);
        Task<ApiResponseResult> DeleteCategory(int categoryId);
    }
}
