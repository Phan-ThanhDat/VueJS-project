using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aibidia.API.Common.Interfaces
{
    public interface IProjectingRepository<TModel, TKey>
    {
        Task<TDto> GetAsAsync<TDto>(Expression<Func<TModel, TDto>> asDto, TKey id);
        Task<TDto> GetFirstAsAsync<TDto>(Expression<Func<TModel, TDto>> asDto, Expression<Func<TModel, bool>> whereClause);
        Task<List<TDto>> GetAllAsAsync<TDto>(Expression<Func<TModel, TDto>> asDto);
        Task<List<TDto>> GetAllAsAsync<TDto>(Expression<Func<TModel, TDto>> asDto, Expression<Func<TModel, bool>> whereClause);
        Task<List<TDto>> GetAllSortedAsAsync<TDto>(Expression<Func<TModel, TDto>> asDto, Expression<Func<TModel, TKey>> orderBy);
        Task<List<TDto>> GetAllSortedAsAsync<TDto>(Expression<Func<TModel, TDto>> asDto, Expression<Func<TModel, bool>> whereClause, Expression<Func<TModel, TKey>> orderBy);
    }
}
