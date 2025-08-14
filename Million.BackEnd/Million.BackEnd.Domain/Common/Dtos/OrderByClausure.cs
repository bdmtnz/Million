using System.Linq.Expressions;

namespace Million.BackEnd.Domain.Common.Dtos
{
    public enum OrderByDirection
    {
        Asc,
        Desc
    }
    public record OrderByClausure<T>(Expression<Func<T, object>> Predicate, OrderByDirection Order = OrderByDirection.Asc);
}
