using CandidatesApp._2.Aplication.DTOs;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
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
