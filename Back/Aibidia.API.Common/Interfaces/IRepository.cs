using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aibidia.API.Common.Interfaces
{
    public interface IRepository
    {
        Task<bool> ExistsAsync();
    }

    public interface IRepository<TKey> : IRepository
    {
        Task<bool> ExistsAsync(TKey id);
    }

    public interface IRepository<TModel, TKey> : IRepository<TKey>
    {
        Task<bool> ExistsAsync(Expression<Func<TModel, bool>> whereClause);

        Task<TModel> GetAsync(TKey id);
        Task<TModel> GetFirstAsync(Expression<Func<TModel, bool>> whereClause);

        Task<List<TModel>> GetAllAsync();
        Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>> whereClause);

        Task<List<TModel>> GetAllSortedAsync(Expression<Func<TModel, TKey>> orderBy);
        Task<List<TModel>> GetAllSortedAsync(Expression<Func<TModel, bool>> whereClause, Expression<Func<TModel, TKey>> orderBy);

        void Add(TModel obj);
    }
}
