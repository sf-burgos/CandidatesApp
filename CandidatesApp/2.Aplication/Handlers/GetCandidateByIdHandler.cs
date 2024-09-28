using AutoMapper;
using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CandidatesApp._2.Application.Exceptions;

namespace CandidatesApp._2.Aplication.Handlers
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
