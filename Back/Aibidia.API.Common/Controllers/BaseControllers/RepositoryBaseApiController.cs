using Aibidia.API.Common.Interfaces;
using PlatformOnline.Constants;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aibidia.API.Common.Controllers.BaseControllers
{
    public abstract class RepositoryBaseApiController<TRepository, TModel, TDto, TKey> : BaseApiController
         where TModel : class
         where TRepository : IRepository<TModel, TKey>, IProjectingRepository<TModel, TKey>
    {
        protected abstract Expression<Func<TModel, TDto>> AsDto { get; }
        protected abstract TKey GetKey(TModel model);

        protected TRepository Repository { get; }

        public RepositoryBaseApiController(TRepository repository)
        {
            Repository = repository;
        }

        protected virtual Task<TDto> FirstOrDefaultProjectedAsync(TKey id)
        {
            return Repository.GetAsAsync(AsDto, id);
        }

        protected virtual async Task<IHttpActionResult> GetByIdAsync(TKey id)
        {
            var obj = await Repository.GetAsAsync(AsDto, id);
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        protected virtual async Task<IHttpActionResult> GetFirstAsync(Expression<Func<TModel, bool>> whereClause)
        {
            var obj = await Repository.GetFirstAsAsync(AsDto, whereClause);
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        protected virtual async Task<IHttpActionResult> GetAllAsync()
        {
            var objs = await Repository.GetAllAsAsync(AsDto);
            return Ok(objs);
        }

        protected virtual async Task<IHttpActionResult> GetAllAsync(Expression<Func<TModel, bool>> whereClause)
        {
            var objs = await Repository.GetAllAsAsync(AsDto, whereClause);
            return Ok(objs);
        }

        protected virtual async Task<IHttpActionResult> AddAndSaveAsync(TModel obj, string endpoint = null, string routeName = null)
        {
            Repository.Add(obj);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Trace.TraceError($"Error saving a new {nameof(TModel)}.");
                throw;
            }

            if (String.IsNullOrWhiteSpace(routeName))
            {
                routeName = RouteConfigs.ApiRouteName;
            }

            TKey key = GetKey(obj);
            TDto created = await Repository.GetAsAsync(AsDto, key);

            return CreatedAtRoute(routeName, GetRouteObject(key, endpoint), created);
        }

        protected virtual async Task<IHttpActionResult> UpdateAsync(TModel obj)
        {
            TDto updated = await SaveAndReturnAsync(obj);

            return Ok(updated);
        }

        protected async Task<TDto> SaveAndReturnAsync(TModel obj)
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                Trace.TraceError($"Error saving a new {nameof(TModel)}.");
                throw;
            }

            TKey key = GetKey(obj);
            TDto dto = await FirstOrDefaultProjectedAsync(key);

            return dto;
        }

        protected virtual object GetRouteObject(TKey key, string controller = null)
        {
            if (String.IsNullOrWhiteSpace(controller))
            {
                return new { id = key };
            }

            return new { controller, id = key };
        }
    }
}