using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class UpdateExperienceHandler : IRequestHandler<UpdateExperienceCommand, ExperienceDto>
{
    private readonly MyDbContext _context;

    public UpdateExperienceHandler(MyDbContext context)
    {
        _context = context;
    }

    public async Task<ExperienceDto> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        var experience = await GetExperienceById(request.CandidateId, request.ExperienceId);

        if (experience == null)
        {
            return null;
        }

        experience.Company = request.Company;
        experience.Job = request.Job;
        experience.Description = request.Description;
        experience.Salary = request.Salary;
        experience.BeginDate = request.BeginDate;
        experience.EndDate = request.EndDate;
        experience.ModifyDate = DateTime.Now;

        _context.Experience.Update(experience);
        await _context.SaveChangesAsync(cancellationToken);

        return new ExperienceDto
        {
            Id = experience.Id,
            CandidateId = experience.CandidateId,
            Company = experience.Company,
            Job = experience.Job,
            Description = experience.Description,
            Salary = experience.Salary,
            BeginDate = experience.BeginDate,
            EndDate = experience.EndDate,
            ModifyDate = DateTime.Now
        };
    }

    private async Task<Experience> GetExperienceById(int candidateId, int experienceId)
    {
        return await _context.Experience
                             .FirstOrDefaultAsync(e => e.CandidateId == candidateId && e.Id == experienceId);
    }
}


