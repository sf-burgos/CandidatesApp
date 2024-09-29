using System.ComponentModel.DataAnnotations;

namespace CandidatesApp.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        public string Surname { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        [MaxLength(250)]
        public string Email { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }

        // Relation 1 to * CandidateExperience
        public ICollection<Experience> Experiences { get; set; }
    }
}
