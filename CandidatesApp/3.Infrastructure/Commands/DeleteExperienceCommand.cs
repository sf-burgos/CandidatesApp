using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
{
    public class DeleteExperienceCommand : IRequest<bool>
    {
        public int CandidateId { get; set; }
        public int ExperienceId { get; set; }

        public DeleteExperienceCommand(int candidateId, int experienceId)
        {
            CandidateId = candidateId;
            ExperienceId = experienceId;
        }
    }
}
