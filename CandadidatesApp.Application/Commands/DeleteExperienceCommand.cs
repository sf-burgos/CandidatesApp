using MediatR;

namespace CandidatesApp.Application.Commands
{
    public record DeleteExperienceCommand(int CandidateId, int ExperienceId) : IRequest<string>;
}
