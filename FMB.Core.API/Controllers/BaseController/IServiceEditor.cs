using FMB.Core.Data.Models.BaseTypes;
using System.Linq.Expressions;

namespace FMB.Core.API.Controllers.BaseController
{
    public interface IServiceEditor<T> where T : IEntity
    {
        Task<IEnumerable<T>> Get();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression);
        Task Post(T entity);
        Task Post(IEnumerable<T> entities);
        Task Put(T entity);
        Task Put(IEnumerable<T> entities);
        Task Delete<TdentityType>(TdentityType id);
        Task DeleteRange(IEnumerable<T> entities);
        Task<T> Find<IdentityType>(IdentityType id);
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
        Task<T> Find(Expression<Func<T, bool>> expression);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
        IQueryable<T> Include(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}
