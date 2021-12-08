using System;
using Microsoft.EntityFrameworkCore;

namespace API_2
{
    public class AppDbContext : DbContext
    {
        public DbSet<Quiz> Quizzes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>()
                .Property(q => q.difficulty)
                .HasConversion<string>();
        }
        
    }
}
