using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CandidatesApp._2.Application.Exceptions;

namespace CandidatesApp._2.Aplication.Handlers
{
    public class GetCandidateByIdHandler(MyDbContext context) : IRequestHandler<GetCandidateByIdQuery, CandidateDTO >
    {
        private readonly MyDbContext _context = context;

        public async Task<CandidateDTO> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            return candidate == null
                ? throw new CandidateNotFoundException(request.Id)
                : new CandidateDTO
            {
                Id = candidate.Id,
                Name = candidate.Name,
                Surname = candidate.Surname,
                Birthdate = candidate.Birthdate,
                Email = candidate.Email,
                ModifyDate = candidate.ModifyDate
            };
        }
    }
}
