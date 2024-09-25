﻿using Microsoft.EntityFrameworkCore;

namespace CandidatesApp.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateExperience> CandidatesExperience { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateExperience>(entity =>
            {
                entity.Property(e => e.Salary)
                      .HasColumnType("decimal(18,2)");
            });
        }
    }


}
