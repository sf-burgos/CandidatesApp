using CandidatesApp.Application.DTOs;
using MediatR;

namespace CandidatesApp.Application.Queries
{
    public record GetCandidateByIdQuery(int Id): IRequest<CandidateDTO>;
    
    
}
