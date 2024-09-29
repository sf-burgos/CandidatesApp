using CandidatesApp.Application.Commands;
using CandidatesApp.Application.Exceptions;
using CandidatesApp.Infrastructure.Data;
using MediatR;

namespace CandidatesApp.Application.Handlers
{
    public class DeleteCandidateHandler : IRequestHandler<DeleteCandidateCommand, string>
    {
        private readonly MyDbContext _context;

        public DeleteCandidateHandler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates.FindAsync(request.Id);

            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync(cancellationToken);

                return $"Candidate with ID {request.Id} has been successfully deleted.";
            }

            throw new CandidateNotFoundException(request.Id);
        }
    }
}
