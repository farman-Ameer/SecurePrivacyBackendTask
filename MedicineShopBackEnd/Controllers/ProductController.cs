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
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [Authorize]
        [Route("AddProduct")]
        [HttpPost]
        public async Task<ApiResponseResult> AddProduct()
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                IFormFile file = null;
                if (Request.Form.Files.Count() > 0)
                    file = Request.Form.Files[0];

                var requestfields = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
                apiResponseResult = await productService.AddProduct(requestfields, file);
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
            }
            return apiResponseResult;
        }

        [Authorize]
        [Route("UpdateProduct")]
        [HttpPost]
        public async Task<ApiResponseResult> UpdateProduct()
        {
            IFormFile file = null;
            if (Request.Form.Files.Count() > 0)
                file = Request.Form.Files[0];

            var requestfields = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            return await productService.UpdateProduct(requestfields, file);
        }

        [Route("GetAllProducts")]
        [HttpGet]
        public async Task<ApiResponseResult> GetAllProducts(int? pageNumber = 1, int? pageSize = 10)
        {
            return await productService.GetAllProducts(pageNumber, pageSize);
        }

        [Route("GetProductsByCategoryId")]
        [HttpGet]
        public async Task<ApiResponseResult> GetProductsByCategoryId(int categoryId,int? pageNumber = 1, int? pageSize = 10)
        {
            return await productService.GetProductsByCategoryId(categoryId,pageNumber, pageSize);
        }

        [Authorize]
        [Route("DeleteProduct")]
        [HttpDelete]
        public async Task<ApiResponseResult> DeleteProduct(int productId)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                apiResponseResult = await productService.DeleteProduct(productId);
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
            }
            return apiResponseResult;
        }
        [Authorize]
        [Route("GetProductById")]
        [HttpGet]
        public async Task<ApiResponseResult> GetProductById(int productId)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                apiResponseResult = await productService.GetProductById(productId);
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
            }
            return apiResponseResult;
        }

        [Route("SearchProduct")]
        [HttpGet]
        public async Task<ApiResponseResult> GetProductsByCategoryId(string search,int categoryId, int? pageNumber = 1, int? pageSize = 10)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                apiResponseResult = await productService.SearchProduct(search, categoryId, pageNumber, pageSize);
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