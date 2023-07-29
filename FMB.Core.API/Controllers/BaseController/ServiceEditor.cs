using FMB.Core.Data.Models.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FMB.Core.API.Controllers.BaseController
{
    public class ServiceEditor<T> : IServiceEditor<T> where T : IEntity
    {

        // TODO: release when context to be add
        public Task Delete<TdentityType>(TdentityType id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T> Find<IdentityType>(IdentityType id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Find(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Include(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task Post(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Post(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task Put(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Put(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
