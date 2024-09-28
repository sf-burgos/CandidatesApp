using AutoMapper;
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
        private readonly IMapper _mapper;

        public AddExperienceHandler(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienceDto> Handle(AddExperienceCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates.FindAsync(request.CandidateId);
            if (candidate == null)
            {
                return null; 
            }

            var experience = _mapper.Map<Experience>(request.Experience);
            experience.CandidateId = request.CandidateId;
            experience.InsertDate = DateTime.Now;

            await _context.Experience.AddAsync(experience);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ExperienceDto>(experience);
        }
    }
}
