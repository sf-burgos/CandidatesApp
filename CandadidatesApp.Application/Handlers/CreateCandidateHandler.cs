using AutoMapper;
using CandidatesApp.Application.Commands;
using CandidatesApp.Application.DTOs;
using CandidatesApp.Infrastructure.Data;
using CandidatesApp.Models;
using MediatR;

namespace CandidatesApp.Application.Handlers
{
    public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, CandidateDTO>
    {
        private readonly MyDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCandidateHandler(MyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CandidateDTO> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidateEntity = _mapper.Map<Candidate>(request);
            
            _dbContext.Candidates.Add(candidateEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var candidateDTO = _mapper.Map<CandidateDTO>(candidateEntity);

            return candidateDTO;
        }
    }
}
