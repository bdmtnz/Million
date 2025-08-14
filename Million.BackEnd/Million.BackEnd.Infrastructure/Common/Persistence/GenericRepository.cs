using Million.BackEnd.Domain.Common.Contracts.Persistence;
using Million.BackEnd.Domain.Common.Dtos;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace Million.BackEnd.Infrastructure.Common.Persistence
{
    public class GenericRepository<T>(IMongoDatabase context) : IGenericRepository<T>
        where T : class
    {
        private readonly IMongoCollection<T> _dbSet = context.GetCollection<T>(nameof(T));

        public async Task<T?> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var query = _dbSet.AsQueryable<T>();
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> Where(
            System.Linq.Expressions.Expression<Func<T, bool>> predicate,
            PaginationFilter? pagination = null,
            OrderByClausure<T>? orderBy = null)
        {
            IQueryable<T>? query = _dbSet.AsQueryable<T>().Where(predicate);

            if (orderBy is { Predicate: not null })
            {
                if (orderBy.Order == OrderByDirection.Asc)
                {
                    query = query.OrderBy(orderBy.Predicate);
                }
                else
                {
                    query = query.OrderByDescending(orderBy.Predicate);
                }
            }

            if (pagination is { Limit: > 0, Offset: > 0 })
            {
                var previousOffset = pagination.Offset - 1;
                query = query
                    .Skip(previousOffset * pagination.Limit)
                    .Take(pagination.Limit);
            }

            return (await query.ToListAsync()).ToImmutableList();
        }

        public async Task<IEnumerable<T>> Where(
            FilterDefinition<T>? filter = null,
            PaginationFilter? pagination = null,
            SortDefinition<T>? orderBy = null)
        {
            var query = _dbSet.Find(filter ?? Builders<T>.Filter.Empty);

            if (orderBy is not null)
            {
                query.Sort(orderBy);
            }

            if (pagination is not null)
            {
                var previousOffset = pagination.Offset - 1;
                query
                    .Sort(orderBy)
                    .Skip(previousOffset * pagination.Limit)
                    .Limit(pagination.Limit);
            }

            return await query.ToListAsync();
        }

        public Task Add(T entity)
            => _dbSet.InsertOneAsync(entity);

        public Task AddRange(IEnumerable<T> entities)
            => _dbSet.InsertManyAsync(entities);

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
            => _dbSet.DeleteOne(predicate);

        public async Task<int> Count()
        {
            return await _dbSet.AsQueryable<T>().CountAsync();
        }

        public Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsQueryable<T>().CountAsync(predicate);
        }

        public Task<long> Count(FilterDefinition<T> filter)
        {
            return _dbSet.CountDocumentsAsync(filter);
        }
    }
}
