using MedicineShopBackEnd.Model.DTO;
using MedicineShopBackEnd.Model.Pagination;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace MedicineShopBackEnd.Data.IRepository
{
   public interface IUnitOfWork<TEntity>
    {
          
        Task<List<TEntity>> GetAll(CancellationToken ct = default(CancellationToken));
        Task<int> Insert(TEntity entity);
        Task<int> Add(TEntity entity);
        Task<int> Update(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> where, CancellationToken ct = default(CancellationToken));
        Task<int> SaveChangesAsync();
        PagedList<TEntity> GetPaginatedData(Expression<Func<TEntity, bool>> where, int? pageNumber = 1, int? pageSize = 10);
        Paged<TEntity> GetPaginated(int? pageNumber = 1, int? pageSize = 10);
        Paged<TEntity> GetPaginated(Expression<Func<TEntity, bool>> where, int? pageNumber = 1, int? pageSize = 10);
        //paging pageLinks(string routname, int pageNo, int pageSize, int total);
    }
}
