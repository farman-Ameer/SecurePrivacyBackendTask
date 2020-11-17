using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicineShopBackEnd.Data.Helpers;
using MedicineShopBackEnd.Data.IRepository;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MedicineShopBackEnd.Data.Repository
{
   public class CategoryRepository : UnitOfWork<CategoryModel>, ICategoryRepository
    {
        private readonly MedicineShopDbContext _context;
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        //private readonly IUrlHelper _urlHelper;
        public CategoryRepository(MedicineShopDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(context, configuration, httpContextAccessor)
        {
            this._context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            //_urlHelper = urlHelper;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public PagedList<CategoryModel> GetCategories(int? pageNumber = 1, int? pageSize = 10)
        {
            PagingParamsDTO pagingParams = new PagingParamsDTO();
            pagingParams.PageNumber = pageNumber;
            pagingParams.PageSize = pageSize;
            try
            {
                var query = _context.Categories;
                return new PagedList<CategoryModel>(
                        query, pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task<ApiResponseResult> UpdateCategory(CategoryModel category)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                var _categpry = await _context.Categories.Where(x => x.categoryId == category.categoryId).FirstOrDefaultAsync();
                if (_categpry != null)
                {
                    _categpry.categoryDesc = category.categoryDesc;
                    _categpry.categoryName = category.categoryName;
                    if (category.categoryImage != null && category.categoryImage != "") { _categpry.categoryImage = category.categoryImage; }
                    await _context.SaveChangesAsync();
                    apiResponseResult.message = "Category Updated Successfully";
                    apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                }
                else
                {
                    apiResponseResult.message = "No Category found";
                    apiResponseResult.ErrorCode = (int)Constant.Error.RECIEVE_DATA_NULL;
                }

            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
            }
            return apiResponseResult;
        }

        public async Task<ApiResponseResult> DeleteCategory(int categoryId)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                var itemToRemove = _context.Categories.SingleOrDefault(x => x.categoryId == categoryId); //returns a single item.
                if (itemToRemove != null)
                {
                    _context.Categories.Remove(itemToRemove);
                    await _context.SaveChangesAsync();
                    apiResponseResult.message = "Category Deleted Successfully";
                    apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                }
                else
                {
                    apiResponseResult.message = "No Category found";
                    apiResponseResult.ErrorCode = (int)Constant.Error.RECIEVE_DATA_NULL;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return apiResponseResult;
        }

    }
}
