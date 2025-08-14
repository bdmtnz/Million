using ErrorOr;
using MediatR;

namespace Million.BackEnd.Application.Seeders.Commands.Create
{
    public record CreateSeederCommand : IRequest<ErrorOr<Success>>;
}
