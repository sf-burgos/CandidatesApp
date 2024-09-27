using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp._3.Infrastructure.Queries;
using CandidatesApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CandidatesApp._2.Aplication.Handlers
{
    public class GetExperiencesByCandidateIdHandler : IRequestHandler<GetExperiencesByCandidateIdQuery, List<ExperienceDto>>
    {
        private readonly MyDbContext _context; 
        public GetExperiencesByCandidateIdHandler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExperienceDto>> Handle(GetExperiencesByCandidateIdQuery request, CancellationToken cancellationToken)
        {
       
            var experiences = await _context.Experience
                .Where(e => e.CandidateId == request.CandidateId)
                .ToListAsync(cancellationToken);

            var experienceDtos = experiences.Select(exp => new ExperienceDto
            {
                Id = exp.Id,
                CandidateId = exp.CandidateId,
                Company = exp.Company,
                Job = exp.Job,
                Description = exp.Description,
                Salary = exp.Salary,
                BeginDate = exp.BeginDate,
                EndDate = exp.EndDate,
                InsertDate = exp.InsertDate,
                ModifyDate = exp.ModifyDate
            }).ToList();

            return experienceDtos;
        }
    }
}
