using CandidatesApp._2.Aplication.DTOs;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
{
    public class AddExperienceCommand : IRequest<ExperienceDto>
    {
        public int CandidateId { get; set; }
        public ExperienceDto Experience { get; set; }

        public AddExperienceCommand(int candidateId, ExperienceDto experience)
        {
            CandidateId = candidateId;
            Experience = experience;
        }
    }
}
