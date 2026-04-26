using System.Linq.Expressions;

namespace ServiceBooking.Core.Interfaces;

public interface IRepository<T> where T : class
{
      Task<T?> FindAsync(int id);
      void Add(T entity);
      void Update(T entity);
      void Delete(T entity);
      Task<IEnumerable<T>> FetchAsync(bool withNoTracking = false);
      Task<IEnumerable<T>> FetchAsync(bool withNoTracking = false, params Expression<Func<T, object>>[] args);
      Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, bool withNoTracking = false);
      Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, bool withNoTracking = false, params Expression<Func<T, object>>[] args);
}
