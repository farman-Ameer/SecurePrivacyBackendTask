using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace SecurePrivacy.Data.IRepository
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
}
}
