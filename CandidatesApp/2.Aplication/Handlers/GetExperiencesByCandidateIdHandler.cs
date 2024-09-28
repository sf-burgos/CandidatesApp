using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetExperiencesByCandidateIdHandler(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ExperienceDto>> Handle(GetExperiencesByCandidateIdQuery request, CancellationToken cancellationToken)
        {
            var experiences = await _context.Experience
                .Where(e => e.CandidateId == request.CandidateId)
                .ToListAsync(cancellationToken);

            var experienceDtos = _mapper.Map<List<ExperienceDto>>(experiences);

            return experienceDtos;
        }
    }
}
