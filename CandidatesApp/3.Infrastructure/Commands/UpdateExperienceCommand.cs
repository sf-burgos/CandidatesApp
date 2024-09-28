using CandidatesApp._2.Aplication.DTOs;
using MediatR;

namespace CandidatesApp._3.Infrastructure.Commands
{
    public class UpdateExperienceCommand : IRequest<ExperienceDto>
    {
        public int CandidateId { get; set; }
        public int ExperienceId { get; set; }
        public string Company { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
