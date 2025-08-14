using ErrorOr;
using Mapster;
using MediatR;
using Million.BackEnd.Contracts.Properties;
using Million.BackEnd.Domain.Common.Contracts.Persistence;
using Million.BackEnd.Domain.PropertyAggregate;
using Million.BackEnd.Domain.PropertyAggregate.ValueObjects;

namespace Million.BackEnd.Application.Properties.Query.GetById
{
    public class GetpropertyByIdQueryHandler(IUnitOfWork _unit) : IRequestHandler<GetpropertyByIdQuery, ErrorOr<PropertyResponse>>
    {
        private readonly IGenericRepository<Property> _property = _unit.GenericRepository<Property>();

        public async Task<ErrorOr<PropertyResponse>> Handle(GetpropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _property.FirstOrDefaultAsync(p => p.Id == PropertyId.Create(request.Id));
            if (property is null)
            {
                return Error.NotFound(description: "Property not found");
            }

            return property.Adapt<PropertyResponse>();
        }
    }
}
