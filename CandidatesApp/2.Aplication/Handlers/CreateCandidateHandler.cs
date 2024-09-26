using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;
using MediatR;

namespace CandidatesApp._2.Aplication.Handlers
{
    public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, CandidateDTO>
    {
        private readonly MyDbContext _dbContext;

        public CreateCandidateHandler(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CandidateDTO> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candiateItem = new Candidate
            {
                Name = request.Name,
                Surname = request.Surname,
                Birthdate = request.Birthday,
                Email = request.Email,
               
            };
            _dbContext.Candidates.Add(candiateItem);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CandidateDTO
            {
                Name = request.Name,
                Surname = request.Surname,
                Birthdate = request.Birthday,
                Email = request.Email
            };
        }
    }
}
