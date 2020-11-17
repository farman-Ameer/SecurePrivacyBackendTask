using MedicineShopBackEnd.Model.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineShopBackEnd.Model.DTO
{
   public class paginationDTO<T>
    {
        public PagingHeader Paging { get; set; }
        public paging pagedLinks { get; set; }
        public List<T> Items { get; set; }
    }
}
