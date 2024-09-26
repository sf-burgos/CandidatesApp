using CandidatesApp._2.Aplication.DTOs;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
{
    public record UpdatedCandidateCommand(string Name, string Surname, DateTime Birthday, string Email) : IRequest<CandidateDTO>;
}
