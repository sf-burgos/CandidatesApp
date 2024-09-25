namespace CandidatesApp.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }

        // Relation 1 to * CandidateExperience
        public ICollection<CandidateExperience> Experiences { get; set; }
    }
}
