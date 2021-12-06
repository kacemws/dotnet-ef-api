using System;
using Microsoft.EntityFrameworkCore;

namespace API_2
{
    public class AppDbContext : DbContext
    {
        public DbSet<Quiz> Quizzes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433; Database=QuizDB;User=sa; Password=1StrongPassword!");
        public AppDbContext()
        {
        }
    }
}
