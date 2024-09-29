using AutoMapper;
using CandidatesApp.Application.DTOs;
using CandidatesApp.Application.Queries;
using CandidatesApp.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace CandidatesApp.Application.Handlers
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
