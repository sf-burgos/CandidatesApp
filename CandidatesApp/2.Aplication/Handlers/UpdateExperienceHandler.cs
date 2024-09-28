using AutoMapper;
using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;
using CandidatesApp._2.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class UpdateExperienceHandler : IRequestHandler<UpdateExperienceCommand, ExperienceDto>
{
    private readonly MyDbContext _context;
    private readonly IMapper _mapper;

    public UpdateExperienceHandler(MyDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExperienceDto> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        var experience = await GetExperienceById(request.CandidateId, request.ExperienceId);

        if (experience != null)
        {
            _mapper.Map(request, experience);
            experience.ModifyDate = DateTime.Now;

            _context.Experience.Update(experience);
            await _context.SaveChangesAsync(cancellationToken);

            var experienceDto = _mapper.Map<ExperienceDto>(experience);

            return experienceDto;
        }

        throw new ExperienceNotFoundException(request.CandidateId, request.ExperienceId);
    }

    private async Task<Experience> GetExperienceById(int candidateId, int experienceId)
    {
        return await _context.Experience
                             .FirstOrDefaultAsync(e => e.CandidateId == candidateId && e.Id == experienceId);
    }
}
