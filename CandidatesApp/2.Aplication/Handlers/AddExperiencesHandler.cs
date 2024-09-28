using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp._2.Application.Exceptions;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;
using MediatR;

namespace CandidatesApp._2.Aplication.Handlers
{
    public class AddExperienceHandler : IRequestHandler<AddExperienceCommand, ExperienceDto>
    {
        private readonly MyDbContext _context;

        public AddExperienceHandler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<ExperienceDto> Handle(AddExperienceCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates.FindAsync(request.CandidateId);
            if (candidate == null)
            {
                return null;
            }

            var experience = new Experience
            {
                CandidateId = request.CandidateId,
                Company = request.Experience.Company,
                Job = request.Experience.Job,
                Description = request.Experience.Description,
                Salary = request.Experience.Salary,
                BeginDate = request.Experience.BeginDate,
                EndDate = request.Experience.EndDate,
                InsertDate = DateTime.Now
            };

            await _context.Experience.AddAsync(experience);
            await _context.SaveChangesAsync();

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
                InsertDate = experience.InsertDate
            };
        }
    }

}
