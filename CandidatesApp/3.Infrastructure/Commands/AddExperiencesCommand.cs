using CandidatesApp._2.Aplication.DTOs;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
{
    public record AddExperienceCommand(int CandidateId, ExperienceDto Experience) : IRequest<ExperienceDto>;

}
