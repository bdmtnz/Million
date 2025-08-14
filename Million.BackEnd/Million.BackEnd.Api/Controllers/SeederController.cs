using MediatR;
using Microsoft.AspNetCore.Mvc;
using Million.BackEnd.Api.Controllers.Common;
using Million.BackEnd.Application.Seeders.Commands.Create;

namespace Million.BackEnd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeederController(IMediator _mediator) : ApiController
    {
        [HttpPatch(Name = "Create")]
        public async Task<IActionResult> Create()
        {
            var response = await _mediator.Send(new CreateSeederCommand());
            return response.Match(
                commissions => Ok(commissions),
                err => Problem(err)
            );
        }
    }
}
