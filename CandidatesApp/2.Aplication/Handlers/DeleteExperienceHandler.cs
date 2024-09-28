using CandidatesApp._2.Application.Exceptions;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CandidatesApp._2.Aplication.Handlers
{
    public class DeleteExperienceHandler : IRequestHandler<DeleteExperienceCommand, bool>
    {
        private readonly MyDbContext _context;

        public DeleteExperienceHandler(MyDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates.FindAsync(request.CandidateId);

            if (candidate == null)
            {
                throw new CandidateNotFoundException(request.CandidateId); 
            }

            var experience = await _context.Experience
                .FirstOrDefaultAsync(e => e.Id == request.ExperienceId && e.CandidateId == request.CandidateId, cancellationToken);

            if (experience == null)
            {
                return false; 
            }

            _context.Experience.Remove(experience);
            await _context.SaveChangesAsync(cancellationToken);

            return true; 
        }
    }

}
