using FMB.Core.Data.Data;
using FMB.Core.Data.Models.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FMB.Core.API.Controllers.BaseController
{
    public class ServiceEditor<T> : IServiceEditor<T> where T : IEntity
    {

        // TODO: release when context to be add (IdentityContext exemple)
        private readonly IdentityContext _identityContext;

        public ServiceEditor(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _identityContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression)
        {
            return await _identityContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task Post(T entity)
        {
            _identityContext.Set<T>().Add(entity);
            await _identityContext.SaveChangesAsync();
        }

        public async Task Post(IEnumerable<T> entities)
        {
            await _identityContext.Set<T>().AddRangeAsync(entities);
            await _identityContext.SaveChangesAsync();
        }

        public async Task Delete<TdentityType>(TdentityType id)
        {
            var entity = await _identityContext.Set<T>().FindAsync(id);
            entity.Archive = !entity.Archive;
            _identityContext.Entry(entity).State = EntityState.Modified;
            await _identityContext.SaveChangesAsync();
            
        }

        public async Task DeleteRange(IEnumerable<T> entities)
        {
            _identityContext.Set<T>().RemoveRange(entities);
            await _identityContext.SaveChangesAsync();
        }

        public async Task Put(T entity)
        {
            _identityContext.Set<T>().Update(entity);
            await _identityContext.SaveChangesAsync();
        }

        public async Task Put(IEnumerable<T> entities)
        {
            _identityContext.Set<T>().UpdateRange(entities);
            await _identityContext.SaveChangesAsync();
        }

        public async Task<T> Find<IdentityType>(IdentityType id)
        {
            return await _identityContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Find(Expression<Func<T, bool>> expression)
        {
            return await _identityContext.Set<T>().FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            DbSet<T> dbSet = _identityContext.Set<T>();

            IQueryable<T> query = null;
            foreach (var include in includes)
            {
                query = (query ?? dbSet).Include(include);
            }

            return (query ?? dbSet);
        }

        public IQueryable<T> Include(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> dbSet = _identityContext.Set<T>().Where(predicate);

            IQueryable<T> query = null;
            foreach (var include in includes)
            {
                query = (query ?? dbSet).Include(include);
            }

            return (query ?? dbSet);
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return await _identityContext.Set<T>().AnyAsync(predicate);
        }


    }
}
