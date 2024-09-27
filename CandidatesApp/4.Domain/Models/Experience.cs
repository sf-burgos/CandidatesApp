namespace CandidatesApp.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string Company { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }

        // Relación * to 1 candidate
        public Candidate Candidate { get; set; }
    }
}
