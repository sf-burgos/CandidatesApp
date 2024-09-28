using MediatR;
using CandidatesApp._2.Aplication.DTOs;

namespace CandidatesApp._3.Infrastructure.Queries
{
    public record GetAllCandidatesQuery : IRequest<IEnumerable<CandidateDTO>>;
}