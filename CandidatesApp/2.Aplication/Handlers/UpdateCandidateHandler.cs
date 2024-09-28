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

        public UpdateCandidateHandler(MyDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<CandidateDTO> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
 
            var candidate = await _context.Candidates.FindAsync(request.Id);

            if (candidate == null)
            {
                throw new CandidateNotFoundException(request.Id); 
            }

            candidate.Name = request.Name;
            candidate.Surname = request.Surname;
            candidate.Birthdate = request.Birthdate;
            candidate.Email = request.Email;
            candidate.ModifyDate = DateTime.UtcNow;

            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync(cancellationToken);

            var candidateDTO = new CandidateDTO
            {
                Id = candidate.Id,
                Name = candidate.Name,
                Surname = candidate.Surname,
                Email = candidate.Email,
                Birthdate = candidate.Birthdate,
                ModifyDate = candidate.ModifyDate
            };

            return candidateDTO;
        }
    }
}
