using CandidatesApp._2.Aplication.DTOs;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
{
    public record DeleteCandidateCommand(int Id) : IRequest<string>;
}
