using AutoMapper;
using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp._3.Infrastructure.Queries;
using CandidatesApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CandidatesApp._2.Aplication.Handlers
{
    public class GetAllCandidatesQueryHandler : IRequestHandler<GetAllCandidatesQuery, IEnumerable<CandidateDTO>>
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCandidatesQueryHandler(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CandidateDTO>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _context.Candidates.ToListAsync(cancellationToken);

            var candidateDTOs = _mapper.Map<IEnumerable<CandidateDTO>>(candidates);

            return candidateDTOs;
        }
    }
}
