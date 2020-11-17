using MedicineShopBackEnd.Data.IRepository;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.DTO;
using MedicineShopBackEnd.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicineShopBackEnd.Services
{
   public class CategoryService : ICategoryService
    {
        private readonly IHostingEnvironment    _hostingEnvironment;
        private IConfiguration                  _configuration;
        private IHttpContextAccessor            _httpContextAccessor;
        private ICategoryRepository             _categoryRepository;
        private IUserService                    _userService;
        public CategoryService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment, ICategoryRepository categoryRepository, IUserService userService)
        {
            _hostingEnvironment                 = hostingEnvironment;
            _configuration                      = configuration;
            _httpContextAccessor                = httpContextAccessor;
            _categoryRepository                 = categoryRepository;
            _userService                        = userService;
        }


        public async Task<bool> AddCategory(CategoryModel categoryModel)
        {
            try
            {
                if (categoryModel.categoryName == null || categoryModel.categoryName == "")
                    throw new Exception("Category Name is required");

                await _categoryRepository.Insert(categoryModel);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return true;
        }
        public async Task<ApiResponseResult> AddCategory(Dictionary<string, string> requestKeys, IFormFile file = null)
        {
            CategoryModel categoryModel             = new CategoryModel();
            ApiResponseResult apiResponseResult     = new ApiResponseResult();
            string fileName                         = "";

            try
            {
                if (!await _userService.IsUserHasRights())
                {
                    apiResponseResult.message       = "User has no permissions to perform this action";
                    apiResponseResult.ErrorCode     = (int)Constant.Error.INVALID_PERMISSIONS;
                    return apiResponseResult;
                }
               
                string projectRootPath = _hostingEnvironment.ContentRootPath;

                if (file != null)
                    fileName                        = Utility.FileUpload(file, projectRootPath);


                categoryModel.categoryName          = requestKeys["categoryName"];
                categoryModel.categoryImage         = fileName;
                categoryModel.categoryDesc          = requestKeys["categoryDesc"];

                await _categoryRepository.Insert(categoryModel);

                apiResponseResult.message           = "Category Saved Successfully";
                apiResponseResult.ErrorCode         = (int)Constant.Error.NO_ERROR;
                apiResponseResult.status            = true;
            }
            catch (Exception exc)
            {
                apiResponseResult.message           = exc.Message;
                apiResponseResult.ErrorCode         = (int)Constant.Error.EXCEPTION;
                apiResponseResult.status            = false;
            }
            return apiResponseResult;
        }
        public async Task<ApiResponseResult> GetAllCategoriesPaginated(string routname, int? pageNumber = 1, int? pageSize = 10)
        {
            ApiResponseResult apiResponseResult     = new ApiResponseResult();
            paginationDTO<CategoryModel> paginatedorders = new paginationDTO<CategoryModel>();
            try
            {

                var model = _categoryRepository.GetCategories(pageNumber, pageSize);
                paginatedorders.Paging = model.GetHeader();
                //paginatedorders.pagedLinks = _categoryRepository.pageLinks(routname, pageNumber.Value, pageSize.Value, paginatedorders.Paging.TotalItems);
                paginatedorders.Items = await model.List;

                apiResponseResult.data              = paginatedorders;
                apiResponseResult.message           = "Categories Get Successfully";
                apiResponseResult.ErrorCode         = (int)Constant.Error.NO_ERROR;
                apiResponseResult.status            = true;
            }
            catch (Exception exc)
            {
                apiResponseResult.message           = exc.Message;
                apiResponseResult.ErrorCode         = (int)Constant.Error.EXCEPTION;
                apiResponseResult.status            = false;
            }
            return apiResponseResult;
        }

        public async Task<ApiResponseResult> UpdateCategory(Dictionary<string, string> requestKeys, IFormFile file = null)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            CategoryModel categoryModel = new CategoryModel();
            string fileName = "";
            try
            {
                if (!await _userService.IsUserHasRights())
                {
                    apiResponseResult.message = "User has no permissions to perform this action";
                    apiResponseResult.ErrorCode = (int)Constant.Error.INVALID_PERMISSIONS;
                    return apiResponseResult;
                }

                if (file != null)
                {
                    string projectRootPath = _hostingEnvironment.ContentRootPath;
                    fileName = Utility.FileUpload(file, projectRootPath);
                }

                categoryModel.categoryId = Convert.ToInt32(requestKeys["categoryId"]);
                categoryModel.categoryDesc = requestKeys["categoryDesc"];
                categoryModel.categoryName = requestKeys["categoryName"];
                if (fileName != "") { categoryModel.categoryImage = fileName; }

                apiResponseResult = await _categoryRepository.UpdateCategory(categoryModel);
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
                if (!await _userService.IsUserHasRights())
                {
                    apiResponseResult.message = "User has no permissions to perform this action";
                    apiResponseResult.ErrorCode = (int)Constant.Error.INVALID_PERMISSIONS;
                    return apiResponseResult;
                }
                apiResponseResult = await _categoryRepository.DeleteCategory(categoryId);
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
            }
            return apiResponseResult;
        }

        public async Task<ApiResponseResult> GetCategoryById(int categoryId)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                if (!await _userService.IsUserHasRights())
                {
                    apiResponseResult.message = "User has no permissions to perform this action";
                    apiResponseResult.ErrorCode = (int)Constant.Error.INVALID_PERMISSIONS;
                    return apiResponseResult;
                }
                apiResponseResult.data = await _categoryRepository.Get(x=>x.categoryId == categoryId);
                apiResponseResult.message = "Category Get Successfully";
                apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;

            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
            }
            return apiResponseResult;
        }

    }
}
