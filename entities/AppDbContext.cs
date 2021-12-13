using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_2
{
    public class AppDbContext : DbContext
    {
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestions> QuizQuestions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Keys */

            modelBuilder.Entity<QuizQuestions>()
                .HasOne(qst => qst.Quiz)
                .WithOne(qz => qz.quizQuestions)
                .HasForeignKey<QuizQuestions>(qst => qst.quizId);
            //.IsRequired();

            

            /* Keys */

            /* Enum conversion */

            modelBuilder.Entity<Quiz>()
                .Property(q => q.difficulty)
                .HasConversion( new EnumToStringConverter<Difficulty>());

            modelBuilder.Entity<Quiz>()
                .Property(q => q.state)
                .HasConversion(new EnumToStringConverter<QuizState>());

            modelBuilder.Entity<Question>()
                .Property(q => q.type)
                .HasConversion(new EnumToStringConverter<QuestionType>());

            /* Enum conversion */


            /* Default values */

            modelBuilder.Entity<Quiz>()
                .Property(q => q.state)
                .HasDefaultValue(QuizState.DRAFT);

            modelBuilder.Entity<Quiz>()
                .Property(q => q.rating)
                .HasDefaultValue(0);

            modelBuilder.Entity<Quiz>()
                .Property(q => q.numberOfVotes)
                .HasDefaultValue(0);

            modelBuilder.Entity<Quiz>()
                .Property(q => q.numberOfPlays)
                .HasDefaultValue(0);

            modelBuilder.Entity<Quiz>()
                .Property(q => q.difficulty)
                .HasDefaultValue(Difficulty.EASY);

            modelBuilder.Entity<Answer>()
                .Property(ans => ans.valid)
                .HasDefaultValue(false);

            /* Default values */
        }

    }
}
