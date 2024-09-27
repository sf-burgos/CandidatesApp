using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp.Models;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
{
    public class UpdateCandidateCommand : IRequest<CandidateDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
    }
}
