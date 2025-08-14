using ErrorOr;
using MediatR;
using Million.BackEnd.Contracts.Properties;

namespace Million.BackEnd.Application.Properties.Query.GetById
{
    public record GetpropertyByIdQuery(string Id) : IRequest<ErrorOr<PropertyResponse>>;
}
