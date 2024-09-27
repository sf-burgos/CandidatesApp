using CandidatesApp._2.Aplication.DTOs;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Queries
{
    public class GetExperiencesByCandidateIdQuery : IRequest<List<ExperienceDto>>
    {
        public int CandidateId { get; }

        public GetExperiencesByCandidateIdQuery(int candidateId)
        {
            CandidateId = candidateId;
        }
    }
}
