namespace CandidatesApp._2.Aplication.DTOs
{
    public class CandidateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }

        public ICollection<ExperienceDto> Experiences { get; set; }
    }
}
