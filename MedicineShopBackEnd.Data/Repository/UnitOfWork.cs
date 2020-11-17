using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MedicineShopBackEnd.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Http;
using MedicineShopBackEnd.Model.Pagination;
using MedicineShopBackEnd.Model.DTO;

namespace MedicineShopBackEnd.Data.Repository
{
    public abstract class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {
        protected readonly MedicineShopDbContext Context;
        protected DbSet<TEntity> Entities;
        private readonly IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        //private readonly IUrlHelper _urlHelper;
        public UnitOfWork(MedicineShopDbContext Context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.Context = Context;
            Entities = Context.Set<TEntity>();
            _configuration = configuration;
            this._httpContextAccessor = httpContextAccessor;
            //_urlHelper = urlHelper;
        }
        public async Task<List<TEntity>> GetAll(CancellationToken ct = default(CancellationToken))
        {
            return await Entities.ToListAsync(ct);
        }
        public async Task<int> Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            string ip = string.Empty;

            Context.Entry(entity).State = EntityState.Added;
            var result = await Context.SaveChangesAsync();
            return result;
        }
        public async Task<int> Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            string ip = string.Empty;

            Context.Entry(entity).State = EntityState.Added;
            var result = await Context.AddAsync(entity);

            return 0;
        }
        public async Task<int> Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Context.Entry(entity).State = EntityState.Modified;
            var result = await Context.SaveChangesAsync();

            return result;
        }
        public async Task Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync();

        }
        public async virtual Task<TEntity> Get(Expression<Func<TEntity, bool>> where, CancellationToken ct = default(CancellationToken))
        {
            try
            {

                return await Entities.Where(where)?.FirstOrDefaultAsync(ct);
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }
        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }


        #region For Paginatin

        /// GetPaginatedData : This method returns all paginated data along with total count and pages Information. Use this method if you want to get data list from single table.
        /// GetPaginated : This method returns olny Total records and page information. No data Items will return. If your data contains join then use this method to get pages infromations and for getting data items from db use seperate call to get data. 


        public PagedList<TEntity> GetPaginatedData(Expression<Func<TEntity, bool>> where, int? pageNumber = 1, int? pageSize = 10)  // For Pagination along with data
        {
            PagingParamsDTO pagingParams = new PagingParamsDTO();
            pagingParams.PageNumber = pageNumber;
            pagingParams.PageSize = pageSize;
            //var query =  Entities.AsQueryable();
            var query = Entities.Where(where).AsQueryable();
            return new PagedList<TEntity>(
                query, pagingParams.PageNumber, pagingParams.PageSize);
        }


        public Paged<TEntity> GetPaginated(int? pageNumber = 1, int? pageSize = 10)  // For Pagination only No data
        {
            PagingParamsDTO pagingParams = new PagingParamsDTO();
            pagingParams.PageNumber = pageNumber;
            pagingParams.PageSize = pageSize;
            var query = Entities.AsQueryable();
            return new Paged<TEntity>(
                query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public Paged<TEntity> GetPaginated(Expression<Func<TEntity, bool>> where, int? pageNumber = 1, int? pageSize = 10)  // For Pagination only No data
        {
            PagingParamsDTO pagingParams = new PagingParamsDTO();
            pagingParams.PageNumber = pageNumber;
            pagingParams.PageSize = pageSize;
            //var query = Entities.AsQueryable();
            var query = Entities.Where(where).AsQueryable();
            return new Paged<TEntity>(
                query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        //public paging pageLinks(string routname, int pageNo, int pageSize, int total)
        //{
        //    var linkBuilder = new PagedLinkBuilder(_urlHelper, routname, new { PageNumber1 = pageNo, PageSize1 = pageSize }, pageNo, pageSize, total);
        //    paging objPageing = new paging();
        //    objPageing.First = linkBuilder.FirstPage.AbsoluteUri;

        //    if (linkBuilder.PreviousPage != null)
        //        objPageing.Previous = linkBuilder.PreviousPage.AbsoluteUri;

        //    if (linkBuilder.NextPage != null)
        //        objPageing.Next = linkBuilder.NextPage.AbsoluteUri;

        //    objPageing.Last = linkBuilder.LastPage.AbsoluteUri;

        //    return objPageing;

        //}


        #endregion
    }
}
