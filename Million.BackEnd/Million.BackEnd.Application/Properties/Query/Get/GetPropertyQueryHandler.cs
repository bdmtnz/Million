using ErrorOr;
using MapsterMapper;
using MediatR;
using Million.BackEnd.Contracts.Common;
using Million.BackEnd.Contracts.Properties;
using Million.BackEnd.Domain.Common.Contracts.Persistence;
using Million.BackEnd.Domain.PropertyAggregate;
using MongoDB.Driver;

namespace Million.BackEnd.Application.Properties.Query.Get
{
    public class GetPropertyQueryHandler(IMapper _mapper, IUnitOfWork _unit) : IRequestHandler<GetPropertyQuery, ErrorOr<PaginationResponse<List<PropertyFilteredResponse>>>>
    {
        private readonly IGenericRepository<Property> _property = _unit.GenericRepository<Property>();

        public async Task<ErrorOr<PaginationResponse<List<PropertyFilteredResponse>>>> Handle(GetPropertyQuery request, CancellationToken cancellationToken)
        {
            var keyword = request.Keyword is null ? string.Empty : request.Keyword.ToLower().Replace(" ", "");

            FilterDefinition<Property> filter = (
                string.IsNullOrEmpty(keyword) ? 
                    Builders<Property>.Filter.Empty :
                    Builders<Property>.Filter.Or(
                        Builders<Property>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(keyword, "i")),
                        Builders<Property>.Filter.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(keyword, "i")),
                        Builders<Property>.Filter.Regex(p => p.Year, new MongoDB.Bson.BsonRegularExpression(keyword, "i"))
                    )
            );

            FilterDefinition<Property> range = (
                Builders<Property>.Filter.And(
                    Builders<Property>.Filter.Gte(p => p.Price, request.Range.From ?? 0),
                    request.Range.To is null ? 
                        Builders<Property>.Filter.Empty : 
                        Builders<Property>.Filter.Lte(p => p.Price, request.Range.To)
                )
            );

            var predicate = Builders<Property>.Filter.And(filter, range);

            SortDefinition<Property> orderBy = Builders<Property>.Sort.Descending(p => p.CreatedOnUtc);
            var properties = await _property.Where(predicate, request.Pagination, orderBy);
            List<PropertyFilteredResponse> data = properties.ToList().ConvertAll(_mapper.Map<PropertyFilteredResponse>);

            var total = await _property.Count(predicate);

            return new PaginationResponse<List<PropertyFilteredResponse>>(total, data);
        }
    }
}
