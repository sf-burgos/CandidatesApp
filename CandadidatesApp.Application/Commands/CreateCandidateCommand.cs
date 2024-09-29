using CandidatesApp.Application.DTOs;
using MediatR;

namespace CandidatesApp.Application.Commands
{
    public record CreateCandidateCommand(string Name, string Surname, DateTime Birthday, string Email)
        : IRequest<CandidateDTO>;
}
