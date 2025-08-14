using ErrorOr;
using MediatR;
using Million.BackEnd.Contracts.Common;
using Million.BackEnd.Contracts.Properties;
using Million.BackEnd.Domain.Common.Dtos;

namespace Million.BackEnd.Application.Properties.Query.Get
{
    public record GetPropertyQuery(string? Keyword, RangeFilter Range, PaginationFilter Pagination) : IRequest<ErrorOr<PaginationResponse<List<PropertyFilteredResponse>>>>;
}
