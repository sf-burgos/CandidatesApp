using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;
using MediatR;

namespace CandidatesApp._2.Aplication.Handlers
{
    public class DeleteCandidateHandler : IRequestHandler<DeleteCandidateCommand, bool>
    {
        private readonly MyDbContext _context;

        public DeleteCandidateHandler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            // Buscar el candidato por ID
            var candidate = await _context.Candidates.FindAsync(request.Id);
            if (candidate == null)
            {

                return false;
            }


            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
