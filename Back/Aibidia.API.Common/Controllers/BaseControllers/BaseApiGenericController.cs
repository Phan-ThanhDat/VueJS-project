using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using Aibidia.API.Common.Models.Interfaces;
using PlatformOnline.Constants;

namespace Aibidia.API.Common.Controllers.BaseControllers
{
    public abstract class BaseApiController<TModel, TDto, TKey> : BaseApiController where TModel : class
    {
        protected abstract DbSet<TModel> DbSet { get; }
        protected virtual IQueryable<TModel> Query => DbSet;
        protected abstract TKey GetKey(TModel model);

        protected abstract Expression<Func<TModel, bool>> KeyIs(TKey key);
        protected abstract Expression<Func<TModel, TDto>> AsDto { get; }

        protected virtual Task<bool> ExistsAsync(TKey key)
        {
            return Query.AnyAsync(KeyIs(key));
        }

        protected virtual Task<bool> ExistsAsync(Expression<Func<TModel, bool>> whereClause)
        {
            return Query.AnyAsync(whereClause);
        }

        protected virtual Task<TModel> FindAsync(TKey key)
        {
            return Query.FirstOrDefaultAsync(KeyIs(key));
        }

        protected virtual Task<TDto> FirstOrDefaultProjectedAsync(TKey id)
        {
            return Query.Where(KeyIs(id)).Select(AsDto).FirstOrDefaultAsync();
        }

        protected virtual async Task<IHttpActionResult> GetByIdAsync(TKey id)
        {
            var obj = await FirstOrDefaultProjectedAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        protected virtual async Task<IHttpActionResult> GetAllAsync()
        {
            var objs = await Query.Select(AsDto).ToListAsync();

            return Ok(objs);
        }
        protected virtual async Task<IHttpActionResult> GetAllSortedAsync(Expression<Func<TModel, TKey>> orderBy)
        {
            var objs = await Query.OrderBy(orderBy).Select(AsDto).ToListAsync();

            return Ok(objs);
        }

        protected virtual async Task<IHttpActionResult> GetAllAsync(Expression<Func<TModel, bool>> whereClause)
        {
            var objs = await Query.Where(whereClause).Select(AsDto).ToListAsync();

            return Ok(objs);
        }
        protected virtual async Task<IHttpActionResult> GetAllSorted(Expression<Func<TModel, bool>> whereClause, Expression<Func<TModel, TKey>> orderBy)
        {
            var objs = await Query.Where(whereClause).OrderBy(orderBy).Select(AsDto).ToListAsync();

            return Ok(objs);
        }

        protected virtual async Task<IHttpActionResult> AddAndSaveAsync(TModel obj, string endpoint = null, string routeName = null)
        {
            DbSet.Add(obj);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                Trace.TraceError($"Error saving a new {nameof(TModel)}.");
                throw;
            }

            if (String.IsNullOrWhiteSpace(routeName))
            {
                routeName = RouteConfigs.ApiRouteName;
            }

            TKey key = GetKey(obj);
            TDto created = await FirstOrDefaultProjectedAsync(key);

            return CreatedAtRoute(routeName, GetRouteObject(key, endpoint), created);
        }

        protected virtual async Task<IHttpActionResult> SaveAtRouteAsync(TModel obj, string endpoint = null, string routeName = null)
        {
            TDto created = await SaveAndReturnAsync(obj);
            TKey key = GetKey(obj);

            if (String.IsNullOrWhiteSpace(routeName))
            {
                routeName = RouteConfigs.ApiRouteName;
            }

            return CreatedAtRoute(routeName, GetRouteObject(key, endpoint), created);
        }

        protected virtual async Task<IHttpActionResult> UpdateAsync(TModel obj)
        {
            TDto updated = await SaveAndReturnAsync(obj);

            return Ok(updated);
        }

        protected virtual object GetRouteObject(TKey key, string controller = null)
        {
            if (String.IsNullOrWhiteSpace(controller))
            {
                return new { id = key };
            }

            return new { controller, id = key };
        }

        protected virtual async Task<IHttpActionResult> DeleteAndSaveAsync(IDeletable obj)
        {
            obj.Deleted = true;
            obj.DeletedAt = DateTimeOffset.UtcNow;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                Trace.TraceError("Error marking object as deleted in db.");
                throw;
            }

            return Ok();
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
    }
}