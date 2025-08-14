using MediatR;
using Microsoft.AspNetCore.Mvc;
using Million.BackEnd.Api.Controllers.Common;
using Million.BackEnd.Application.Properties.Query.Get;
using Million.BackEnd.Application.Properties.Query.GetById;
using Million.BackEnd.Domain.Common.Dtos;

namespace Million.BackEnd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController(IMediator _mediator) : ApiController
    {
        [HttpGet(Name = "GetProperties")]
        public async Task<IActionResult> Get(string? keyword, int? limit, int? offset, decimal? from, decimal? to)
        {
            var response = await _mediator.Send(
                new GetPropertyQuery(
                    keyword, 
                    new RangeFilter(from, to), 
                    new Domain.Common.Dtos.PaginationFilter(limit??0, offset??0)
                )
            );
            return response.Match(
                commissions => Ok(commissions),
                err => Problem(err)
            );
        }

        [HttpGet("{id}", Name = "GetPropertyById")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _mediator.Send(new GetpropertyByIdQuery(id));
            return response.Match(
                commissions => Ok(commissions),
                err => Problem(err)
            );
        }
    }
}
