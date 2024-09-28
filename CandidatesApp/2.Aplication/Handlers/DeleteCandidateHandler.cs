using AutoMapper;
using CandidatesApp._2.Application.Exceptions;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;
using MediatR;

namespace CandidatesApp._2.Aplication.Handlers
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
