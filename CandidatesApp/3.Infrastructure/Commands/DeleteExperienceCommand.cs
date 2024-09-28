using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
{
    public record DeleteExperienceCommand(int CandidateId, int ExperienceId) : IRequest<string>;
}
