using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;

namespace MedicineShopBackEnd.Model.Pagination
{
   public class PagedLinkBuilder
    {
        public Uri FirstPage { get; private set; }
        public Uri LastPage { get; private set; }
        public Uri NextPage { get; private set; }
        public Uri PreviousPage { get; private set; }

        public PagedLinkBuilder(IUrlHelper urlHelper, string routeName, object routeValues, int pageNo, int pageSize, long totalRecordCount)
        {
            try
            {
                // Determine total number of pages
                var pageCount = totalRecordCount > 0
                    ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                    : 0;

                // Create them page links 
                FirstPage = new Uri(urlHelper.Link(routeName, new RouteValueDictionary()
        {
            {"pageNo", 1},
            {"pageSize", pageSize}
        }));
                LastPage = new Uri(urlHelper.Link(routeName, new RouteValueDictionary()
        {
            {"pageNo", pageCount},
            {"pageSize", pageSize}
        }));
                if (pageNo > 1)
                {
                    PreviousPage = new Uri(urlHelper.Link(routeName, new RouteValueDictionary()
            {
                {"pageNo", pageNo - 1},
                {"pageSize", pageSize}
            }));
                }
                if (pageNo < pageCount)
                {
                    NextPage = new Uri(urlHelper.Link(routeName, new RouteValueDictionary()
            {
                {"pageNo", pageNo + 1},
                {"pageSize", pageSize}
            }));
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
