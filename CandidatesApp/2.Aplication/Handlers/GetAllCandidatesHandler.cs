
using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp._3.Infrastructure.Queries;
using CandidatesApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CandidatesApp._2.Aplication.Handlers
{
    public class GetAllCandidatesQueryHandler : IRequestHandler<GetAllCandidatesQuery, IEnumerable<CandidateDTO>>
    {
        private readonly MyDbContext _context;

        public GetAllCandidatesQueryHandler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CandidateDTO>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _context.Candidates.ToListAsync(cancellationToken);

            var candidateDTOs = candidates.Select(candidate => new CandidateDTO
            {
                Id = candidate.Id,
                Name = candidate.Name,
                Email = candidate.Email,
                Surname = candidate.Surname,
                Birthdate = candidate.Birthdate,
                InsertDate = candidate.InsertDate, 
                ModifyDate = candidate.ModifyDate,
            });

            return candidateDTOs;
        }
    }
}
