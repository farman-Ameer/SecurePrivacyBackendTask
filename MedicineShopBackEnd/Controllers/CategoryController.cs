using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Services;
using MedicineShopBackEnd.Services.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineShopBackEnd.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        [Authorize]
        [Route("AddCategory")]
        [HttpPost]
        public async Task<ApiResponseResult> AddCategory()
        {
            ApiResponseResult apiResponse = new ApiResponseResult();
            try
            {
                IFormFile file = null;
                if (Request.Form.Files.Count() > 0)
                    file = Request.Form.Files[0];

                var requestfields = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
                return await categoryService.AddCategory(requestfields, file);
            }
            catch (Exception exc)
            {
                throw exc;
            }
                
        }


        [Route("GetAllCategories", Name = "GetAllCategories")]
        [HttpGet]
        public async Task<ApiResponseResult> GetAllCategories(int? pageNumber = 1, int? pageSize = 10)
        {
          return await categoryService.GetAllCategoriesPaginated("GetAllCategories",pageNumber, pageSize);
        }



        [Authorize]
        [Route("UpdateCategory")]
        [HttpPost]
        public async Task<ApiResponseResult> UpdateCategory()
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                IFormFile file = null;
                if (Request.Form.Files.Count() > 0)
                    file = Request.Form.Files[0];

                var requestfields = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
                apiResponseResult =  await categoryService.UpdateCategory(requestfields, file);
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
            }
            return apiResponseResult;
        }

        [Authorize]
        [Route("DeleteCategory")]
        [HttpDelete]
        public async Task<ApiResponseResult> DeleteCategory(int categoryId)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                apiResponseResult = await categoryService.DeleteCategory(categoryId);
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
            }
            return apiResponseResult;
        }

        [Authorize]
        [Route("GetCategoryById")]
        [HttpGet]
        public async Task<ApiResponseResult> GetCategoryById(int categoryId)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                apiResponseResult = await categoryService.GetCategoryById(categoryId);
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