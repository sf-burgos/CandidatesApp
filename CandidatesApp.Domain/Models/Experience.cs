using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidatesApp.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CandidateId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Company { get; set; }
        [Required]
        [MaxLength(100)]
        public string Job { get; set; }
        [MaxLength(4000)]
        public string Description { get; set; }
        [Column(TypeName = "numeric(8, 2)")]
        public decimal Salary { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }

        // Relación * to 1 candidate
        [ForeignKey("CandidateId")]
        public Candidate Candidate { get; set; }
    }
}
