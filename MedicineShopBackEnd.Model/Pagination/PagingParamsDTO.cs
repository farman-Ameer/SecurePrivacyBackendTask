using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineShopBackEnd.Model.Pagination
{
    public class PagingParamsDTO
    {
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
