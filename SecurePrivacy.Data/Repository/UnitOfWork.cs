using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SecurePrivacy.Data.IRepository;
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

namespace SecurePrivacy.Data.Repository
{
    public abstract class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {
        protected readonly SecurePrivacyDbContext Context;
        protected DbSet<TEntity> Entities;
        private readonly IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;
        public UnitOfWork(SecurePrivacyDbContext Context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUrlHelper urlHelper)
        {
            this.Context = Context;
            Entities = Context.Set<TEntity>();
            _configuration = configuration;
            this._httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
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
    }
}
