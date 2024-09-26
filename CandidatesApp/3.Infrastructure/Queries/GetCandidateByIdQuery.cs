using CandidatesApp._2.Aplication.DTOs;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
{
    public record GetCandidateByIdQuery(int Id): IRequest<CandidateDTO>;
    
    
}
