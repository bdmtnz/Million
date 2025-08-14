using ErrorOr;
using MapsterMapper;
using MediatR;
using Million.BackEnd.Contracts.Common;
using Million.BackEnd.Contracts.Properties;
using Million.BackEnd.Domain.Common.Contracts.Persistence;
using Million.BackEnd.Domain.Common.Dtos;
using Million.BackEnd.Domain.PropertyAggregate;
using System.Linq.Expressions;

namespace Million.BackEnd.Application.Properties.Query.Get
{
    public class GetPropertyQueryHandler(IMapper _mapper, IUnitOfWork _unit) : IRequestHandler<GetPropertyQuery, ErrorOr<PaginationResponse<List<PropertyResponse>>>>
    {
        private readonly IGenericRepository<Property> _property = _unit.GenericRepository<Property>();

        public async Task<ErrorOr<PaginationResponse<List<PropertyResponse>>>> Handle(GetPropertyQuery request, CancellationToken cancellationToken)
        {
            var keyword = request.Keyword is null ? string.Empty : request.Keyword.ToLower().Replace(" ", "");
            Expression<Func<Property, bool>> predicate =
                (m) =>
                    (string.IsNullOrEmpty(keyword) ? true : (
                        m.Name.ToLower().Contains(keyword) ||
                        m.Address.Contains(keyword) ||
                        m.Year.ToString().Contains(keyword)
                    )
                );

            var orderBy = new OrderByClausure<Property>(m => m.CreatedOnUtc, OrderByDirection.Desc);
            var properties = await _property.Where(predicate, request.Pagination, orderBy);
            List<PropertyResponse> data = properties.ToList().ConvertAll(_mapper.Map<PropertyResponse>);

            var total = await _property.Count(predicate);

            return new PaginationResponse<List<PropertyResponse>>(total, data);
        }
    }
}
