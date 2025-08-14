using Million.BackEnd.Domain.Common.Dtos;
using System.Linq.Expressions;

namespace Million.BackEnd.Domain.Common.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate, string includes = "", PaginationFilter? pagination = null, OrderByClausure<T>? orderBy = null);
        Task<int> Count();
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entity);
        void Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}
