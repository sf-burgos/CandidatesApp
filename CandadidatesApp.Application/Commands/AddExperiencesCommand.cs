using CandidatesApp.Application.DTOs;
using MediatR;

namespace CandidatesApp.Application.Commands;

public record AddExperienceCommand(int CandidateId, ExperienceDto Experience) : IRequest<ExperienceDto>;
