using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicineShopBackEnd.Model.Pagination
{
   public class Paged<T>
    {
        public Paged(IQueryable<T> source, int? pageNumber = 1, int? pageSize = 10)
        {
            this.TotalItems = source.Count();
            this.PageNumber = pageNumber.Value;
            this.PageSize = pageSize.Value;
        }
        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public List<T> List { get; }
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
    }
}
