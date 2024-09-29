using AutoMapper;
using CandidatesApp.Application.DTOs;
using CandidatesApp.Application.Exceptions;
using CandidatesApp.Application.Queries;
using CandidatesApp.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CandidatesApp.Application.Handlers
{
    public class GetCandidateByIdHandler : IRequestHandler<GetCandidateByIdQuery, CandidateDTO>
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public GetCandidateByIdHandler(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CandidateDTO> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (candidate == null)
            {
                throw new CandidateNotFoundException(request.Id);
            }

            return _mapper.Map<CandidateDTO>(candidate);
        }
    }
}
