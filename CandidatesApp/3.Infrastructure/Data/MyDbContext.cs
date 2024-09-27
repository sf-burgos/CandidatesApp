using Microsoft.EntityFrameworkCore;

namespace CandidatesApp.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Experience> Experience { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Experience>(entity =>
            {
                entity.Property(e => e.Salary)
                      .HasColumnType("decimal(18,2)");
            });
        }

        internal async Task<Candidate> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        internal async Task UpdateAsync(Candidate candidate)
        {
            throw new NotImplementedException();
        }
    }


}
