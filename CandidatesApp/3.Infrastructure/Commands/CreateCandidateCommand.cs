using CandidatesApp._2.Aplication.DTOs;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
{
    public record CreateCandidateCommand(string Name, string Surname, DateTime Birthday, string Email)
        : IRequest<CandidateDTO>;
}
