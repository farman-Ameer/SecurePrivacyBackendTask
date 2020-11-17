using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MedicineShopBackEnd.Model.Pagination
{
        public class PagedList<T>   // For Page Information along with data
        {
            public PagedList()
            {

            }
            public PagedList(IQueryable<T> source, int? pageNumber = 1, int? pageSize = 10)
            {
                this.TotalItems = source.Count();
                this.PageNumber = pageNumber.Value;
                this.PageSize = pageSize.Value;
                this.List = GetAsyncList(source, pageNumber, pageSize);
            }

            public int TotalItems { get; }
            public int PageNumber { get; }
            public int PageSize { get; }
            public Task<List<T>> List { get; }
            public int TotalPages =>
                  (int)Math.Ceiling(this.TotalItems / (double)this.PageSize);
            public bool HasPreviousPage => this.PageNumber > 1;
            public bool HasNextPage => this.PageNumber < this.TotalPages;
            public int NextPageNumber =>
                   this.HasNextPage ? this.PageNumber + 1 : this.TotalPages;
            public int PreviousPageNumber =>
                   this.HasPreviousPage ? this.PageNumber - 1 : 1;

            public PagingHeader GetHeader()
            {
                return new PagingHeader(
                     this.TotalItems, this.PageNumber,
                     this.PageSize, this.TotalPages);
            }

            // Follow this link : https://stackoverflow.com/questions/26676563/entity-framework-queryable-async

            public async Task<List<T>> GetAsyncList(IQueryable<T> source, int? pageNumber = 1, int? pageSize = 10)
            {
                try
                {
                    return await source
                                .Skip(pageSize.Value * (pageNumber.Value - 1))
                                .Take(pageSize.Value)
                                .ToListAsync();
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
        }
    
}
