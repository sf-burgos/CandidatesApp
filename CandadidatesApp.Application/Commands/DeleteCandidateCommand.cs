using MediatR;

namespace CandidatesApp.Application.Commands
{
    public record DeleteCandidateCommand(int Id) : IRequest<string>;
}
