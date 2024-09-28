using CandidatesApp._2.Aplication.DTOs;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Queries
{
    public record GetExperiencesByCandidateIdQuery(int CandidateId) : IRequest<List<ExperienceDto>>;
}
