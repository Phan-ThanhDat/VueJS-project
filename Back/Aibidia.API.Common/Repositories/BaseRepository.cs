using Aibidia.API.Common.Interfaces;
using Aibidia.API.Common.Models;
using Aibidia.API.Common.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aibidia.API.Common.Repositories
{
    public abstract class BaseRepository<TContext, TModel, TKey> : IRepository<TModel, TKey>
        where TModel : class
        where TContext : DbContext
    {
        protected virtual TContext Context => _contextProvider.GetCurrentDbContext();
        protected DbSet<TModel> DbSet => Context.Set<TModel>();
        protected virtual IQueryable<TModel> Query => DbSet;

        private IDbContextProvider<TContext> _contextProvider;

        protected abstract Expression<Func<TModel, bool>> KeyIs(TKey id);

        public BaseRepository(IDbContextProvider<TContext> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public virtual Task<bool> ExistsAsync()
        {
            return Query.AnyAsync();
        }

        public virtual Task<bool> ExistsAsync(TKey id)
        {
            return Query.Where(KeyIs(id)).AnyAsync();
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<TModel, bool>> whereClause)
        {
            return Query.Where(whereClause).AnyAsync();
        }

        public virtual Task<TModel> GetAsync(TKey id)
        {
            return Query.Where(KeyIs(id)).FirstOrDefaultAsync();
        }

        public virtual Task<TModel> GetFirstAsync(Expression<Func<TModel, bool>> whereClause)
        {
            return Query.Where(whereClause).FirstOrDefaultAsync();
        }

        public virtual Task<List<TModel>> GetAllAsync()
        {
            return Query.ToListAsync();
        }

        public virtual Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>> whereClause)
        {
            return Query.Where(whereClause).ToListAsync();
        }

        public virtual Task<List<TModel>> GetAllSortedAsync(Expression<Func<TModel, TKey>> orderBy)
        {
            return Query.OrderBy(orderBy).ToListAsync();
        }

        public virtual Task<List<TModel>> GetAllSortedAsync(Expression<Func<TModel, bool>> whereClause, Expression<Func<TModel, TKey>> orderBy)
        {
            return Query.Where(whereClause).OrderBy(orderBy).ToListAsync();
        }

        public virtual void Add(TModel obj) => DbSet.Add(obj);

        protected virtual void MarkDeleted(IDeletable obj)
        {
            obj.Deleted = true;
            obj.DeletedAt = DateTimeOffset.UtcNow;
        }
    }
}
