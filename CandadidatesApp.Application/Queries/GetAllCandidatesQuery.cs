using CandidatesApp.Application.DTOs;
using MediatR;

namespace CandidatesApp.Application.Queries
{
    public record GetAllCandidatesQuery : IRequest<IEnumerable<CandidateDTO>>;
}