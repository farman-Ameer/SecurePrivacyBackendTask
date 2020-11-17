using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MedicineShopBackEnd.Data.IRepository;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.DTO;
using MedicineShopBackEnd.Model.Entities;
using MedicineShopBackEnd.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MedicineShopBackEnd.Services
{
    public class ProductService : IProductService
    {
        private readonly IHostingEnvironment        _hostingEnvironment;
        private IConfiguration                      _configuration;
        private IHttpContextAccessor                _httpContextAccessor;
        private IProductRepository                  _productRepository;
        private IUserService                        _userService;
        public ProductService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment, IProductRepository productRepository, IUserService userService)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
            _userService = userService;
        }

        public async Task<ApiResponseResult> AddProduct(Dictionary<string, string> requestKeys, IFormFile file = null)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            ProductModel productModel           = new ProductModel();

            try
            {
                if(! await _userService.IsUserHasRights())
                {
                    apiResponseResult.message = "User has no permissions to perform this action";
                    apiResponseResult.ErrorCode = (int)Constant.Error.INVALID_PERMISSIONS;
                    return apiResponseResult;
                }

                string fileName = "";
                string projectRootPath = _hostingEnvironment.ContentRootPath;

                if (file != null)
                    fileName = Utility.FileUpload(file, projectRootPath);


                productModel.productBrand = requestKeys["productBrand"];
                productModel.productDesc = requestKeys["productDesc"];
                productModel.productImage = fileName;
                productModel.productName = requestKeys["productName"];
                productModel.productWeight = requestKeys["productWeight"];
                productModel.productPrice = (float) Convert.ToDouble(requestKeys["productPrice"]);
                if (requestKeys["categoryId"] != "undefined")
                {
                    productModel.categoryId = Convert.ToInt32(requestKeys["categoryId"] ?? "");
                }
                await _productRepository.Insert(productModel);

                apiResponseResult.message = "Product Saved Successfully";
                apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                apiResponseResult.status    = true;
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
                apiResponseResult.status = false;
            }
            return apiResponseResult;

        }
        public async Task<ApiResponseResult> GetAllProducts(int? pageNumber = 1, int? pageSize = 10)
        {
            ApiResponseResult apiResponseResult                 = new ApiResponseResult();
            paginationDTO<ProductModel> paginatedorders         = new paginationDTO<ProductModel>();
            try
            {
                var model                                       = _productRepository.GetProducts(pageNumber, pageSize);
                paginatedorders.Paging                          = model.GetHeader();
                //paginatedorders.pagedLinks = _categoryRepository.pageLinks(routname, pageNumber.Value, pageSize.Value, paginatedorders.Paging.TotalItems);
                paginatedorders.Items                           = await model.List;

                apiResponseResult.data                          = paginatedorders;

                //apiResponseResult.data          = _productRepository.GetProducts(pageNumber, pageSize);
                apiResponseResult.message                       = "Products Get Successfully";
                apiResponseResult.ErrorCode                     = (int)Constant.Error.NO_ERROR;
                apiResponseResult.status                        = true;
            }
            catch (Exception exc)
            {
                apiResponseResult.message                       = exc.Message;
                apiResponseResult.ErrorCode                     = (int)Constant.Error.EXCEPTION;
                apiResponseResult.status                        = false;
            }
            return apiResponseResult;
        }
        public async Task<ApiResponseResult> GetProductsByCategoryId(int categoryId,int? pageNumber = 1, int? pageSize = 10)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            paginationDTO<ProductModel> paginatedorders = new paginationDTO<ProductModel>();
            try
            {
                if(categoryId == 0)
                {
                    apiResponseResult.message = "Category Id is required";
                    apiResponseResult.ErrorCode = (int)Constant.Error.ENTITY_EXCEPTION;
                    apiResponseResult.status = false;
                    return apiResponseResult;
                }


                var model = _productRepository.GetProductsByCategoryId(categoryId,pageNumber, pageSize);
                paginatedorders.Paging = model.GetHeader();
                //paginatedorders.pagedLinks = _categoryRepository.pageLinks(routname, pageNumber.Value, pageSize.Value, paginatedorders.Paging.TotalItems);
                paginatedorders.Items = await model.List;

                if(paginatedorders.Items.Count == 0)
                {
                    apiResponseResult.message = "No product found";
                    apiResponseResult.ErrorCode = (int)Constant.Error.RECIEVE_DATA_NULL;
                    apiResponseResult.status = false;
                    return apiResponseResult;
                }

                apiResponseResult.data = paginatedorders;

                //apiResponseResult.data          = _productRepository.GetProducts(pageNumber, pageSize);
                apiResponseResult.message = "Products Get Successfully";
                apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                apiResponseResult.status = true;
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
                apiResponseResult.status = false;
            }
            return apiResponseResult;
        }
        public async Task<ApiResponseResult> UpdateProduct(Dictionary<string, string> requestKeys, IFormFile file = null)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            ProductModel productModel = new ProductModel();
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
                productModel.productId = Convert.ToInt32(requestKeys["productId"]);
                productModel.productBrand = requestKeys["productBrand"];
                productModel.productDesc = requestKeys["productDesc"];
                if(fileName != "") { productModel.productImage = fileName; }
                productModel.productName = requestKeys["productName"];
                productModel.productWeight = requestKeys["productWeight"];
                productModel.productPrice = (float)Convert.ToDouble(requestKeys["productPrice"]);
                if (requestKeys["categoryId"] != "undefined")
                {
                    productModel.categoryId = Convert.ToInt32(requestKeys["categoryId"] ?? "");
                }
                apiResponseResult = await _productRepository.UpdateProduct(productModel);
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
            }
            return apiResponseResult;
        }
        public async Task<ApiResponseResult> DeleteProduct(int productId)
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

                apiResponseResult = await _productRepository.DeleteProduct(productId);
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
            }
            return apiResponseResult;
        }
        public async Task<ApiResponseResult> GetProductById(int productId)
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
                apiResponseResult.data = await _productRepository.Get(x => x.productId == productId);
                apiResponseResult.message = "Product Get Successfully";
                apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;

            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
            }
            return apiResponseResult;
        }
        public async Task<ApiResponseResult> SearchProduct(string search, int? categoryId = 0, int? pageNumber = 1, int? pageSize = 10)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            paginationDTO<ProductModel> paginatedorders = new paginationDTO<ProductModel>();
            try
            {
                if (search == "" || search == null)
                {
                    apiResponseResult.message = "Search empty string";
                    apiResponseResult.ErrorCode = (int)Constant.Error.ENTITY_EXCEPTION;
                    apiResponseResult.status = false;
                    return apiResponseResult;
                }


                var model = _productRepository.SearchProduct(search,categoryId, pageNumber, pageSize);
                paginatedorders.Paging = model.GetHeader();
                //paginatedorders.pagedLinks = _categoryRepository.pageLinks(routname, pageNumber.Value, pageSize.Value, paginatedorders.Paging.TotalItems);
                paginatedorders.Items = await model.List;

                if (paginatedorders.Items.Count == 0)
                {
                    apiResponseResult.message = "No product found";
                    apiResponseResult.ErrorCode = (int)Constant.Error.RECIEVE_DATA_NULL;
                    apiResponseResult.status = false;
                    return apiResponseResult;
                }

                apiResponseResult.data = paginatedorders;

                //apiResponseResult.data          = _productRepository.GetProducts(pageNumber, pageSize);
                apiResponseResult.message = "Products Get Successfully";
                apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                apiResponseResult.status = true;
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
                apiResponseResult.status = false;
            }
            return apiResponseResult;
        }

    }
}
