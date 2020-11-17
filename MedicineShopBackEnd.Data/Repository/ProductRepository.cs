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
   public class ProductRepository : UnitOfWork<ProductModel>, IProductRepository
    {
        private readonly MedicineShopDbContext _context;
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        //private readonly IUrlHelper _urlHelper;
        public ProductRepository(MedicineShopDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(context, configuration, httpContextAccessor)
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
        public PagedList<ProductModel> GetProducts(int? pageNumber = 1, int? pageSize = 10)
        {
            PagingParamsDTO pagingParams = new PagingParamsDTO();
            pagingParams.PageNumber = pageNumber;
            pagingParams.PageSize = pageSize;
            try
            {
                var query = _context.Products;
                return new PagedList<ProductModel>(
                        query, pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public PagedList<ProductModel> GetProductsByCategoryId(int categoryId,int? pageNumber = 1, int? pageSize = 10)
        {
            PagingParamsDTO pagingParams = new PagingParamsDTO();
            pagingParams.PageNumber = pageNumber;
            pagingParams.PageSize = pageSize;
            try
            {
                var query = _context.Products.Where(x=>x.categoryId == categoryId);
                return new PagedList<ProductModel>(
                        query, pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }


        public PagedList<ProductModel> SearchProduct(string search,int ? categoryId = 0, int? pageNumber = 1, int? pageSize = 10)
        {
            PagingParamsDTO pagingParams = new PagingParamsDTO();
            pagingParams.PageNumber = pageNumber;
            pagingParams.PageSize = pageSize;
            IQueryable<ProductModel> query;
            try
            {
                if(categoryId != 0 && categoryId != null)
                {
                    query = _context.Products.Where(x => (x.categoryId == categoryId) && (x.productName.Contains(search) || x.productBrand.Contains(search) || x.productDesc.Contains(search)));
                }
                else
                {
                    query = _context.Products.Where(x =>  (x.productName.Contains(search) || x.productBrand.Contains(search) || x.productDesc.Contains(search)));
                }
                return new PagedList<ProductModel>(
                        query, pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task<double> GetProductPrice(int productId)
        {
            try
            {
                var data = await _context.Products.Where(x => x.productId == productId).FirstOrDefaultAsync();
                return data.productPrice;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public async Task<ApiResponseResult> UpdateProduct(ProductModel product)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                var prod = await _context.Products.Where(x => x.productId == product.productId).FirstOrDefaultAsync();
                if(prod != null)
                {
                    prod.productBrand = product.productBrand;
                    prod.productDesc = product.productDesc;
                    if(product.productImage != null && product.productImage != "") { prod.productImage = product.productImage; }
                    prod.productName = product.productName;
                    prod.productPrice = product.productPrice;
                    prod.productWeight = product.productWeight;
                    prod.categoryId = product.categoryId;
                    await _context.SaveChangesAsync();
                    apiResponseResult.message = "Product Updated Successfully";
                    apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                }
                else
                {
                    apiResponseResult.message = "No Product found";
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
        public async Task<ApiResponseResult> DeleteProduct(int productId)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                var itemToRemove = _context.Products.SingleOrDefault(x => x.productId == productId); //returns a single item.
                if(itemToRemove != null)
                {
                    _context.Products.Remove(itemToRemove);
                    await _context.SaveChangesAsync();
                    apiResponseResult.message = "Product Deleted Successfully";
                    apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                }
                else
                {
                    apiResponseResult.message = "No Product found";
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
