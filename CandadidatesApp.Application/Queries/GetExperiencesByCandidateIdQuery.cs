using CandidatesApp.Application.DTOs;
using MediatR;

namespace CandidatesApp.Application.Queries
{
    public record GetExperiencesByCandidateIdQuery(int CandidateId) : IRequest<List<ExperienceDto>>;
}
