using CandidatesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidatesApp.Infrastructure.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Experience> Experience { get; set; }
    }


}
