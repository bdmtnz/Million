using Million.BackEnd.Domain.Common.Dtos;
using System.Linq.Expressions;

namespace Million.BackEnd.Domain.Common.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate, PaginationFilter? pagination = null, OrderByClausure<T>? orderBy = null, RangeFilter? range = null);
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entity);
        void Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}
