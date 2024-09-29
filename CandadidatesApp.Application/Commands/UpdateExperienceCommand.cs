using CandidatesApp.Application.DTOs;
using MediatR;

namespace CandidatesApp.Application.Commands
{
    public record UpdateExperienceCommand(
        int CandidateId,
        int ExperienceId,
        string Company,
        string Job,
        string Description,
        decimal Salary,
        DateTime BeginDate,
        DateTime? EndDate) : IRequest<ExperienceDto>;
}
