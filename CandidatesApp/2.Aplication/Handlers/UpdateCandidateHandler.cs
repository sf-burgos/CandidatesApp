using AutoMapper;
using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp._2.Application.Exceptions;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;
using MediatR;

namespace CandidatesApp._2.Aplication.Handlers
{
    public class UpdateCandidateHandler : IRequestHandler<UpdateCandidateCommand, CandidateDTO>
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCandidateHandler(MyDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<CandidateDTO> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates.FindAsync(request.Id);

            if (candidate == null)
            {
                throw new CandidateNotFoundException(request.Id);
            }

            _mapper.Map(request, candidate);
            candidate.ModifyDate = DateTime.UtcNow;

            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync(cancellationToken);

            var candidateDTO = _mapper.Map<CandidateDTO>(candidate);

            return candidateDTO;
        }
    }
}
