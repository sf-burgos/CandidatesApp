using CandidatesApp._2.Aplication.DTOs;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
{
    public record UpdateCandidateCommand(int Id, string Name, string Surname, DateTime Birthdate, string Email) : IRequest<CandidateDTO>;
}
