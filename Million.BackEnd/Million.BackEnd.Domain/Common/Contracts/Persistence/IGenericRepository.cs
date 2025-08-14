using Million.BackEnd.Domain.Common.Dtos;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Million.BackEnd.Domain.Common.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> Where(
            Expression<Func<T, bool>> predicate, 
            PaginationFilter? pagination = null, 
            OrderByClausure<T>? orderBy = null);
        Task<IEnumerable<T>> Where(
            FilterDefinition<T>? filter = null,
            PaginationFilter? pagination = null,
            SortDefinition<T>? orderBy = null);
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> predicate);
        Task<long> Count(FilterDefinition<T> filter);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entity);
        void Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}
