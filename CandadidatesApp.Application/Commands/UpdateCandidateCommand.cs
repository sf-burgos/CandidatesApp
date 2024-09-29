using CandidatesApp.Application.DTOs;
using MediatR;

namespace CandidatesApp.Application.Commands
{
    public record UpdateCandidateCommand(int Id, string Name, string Surname, DateTime Birthdate, string Email) : IRequest<CandidateDTO>;
}
