﻿// <auto-generated />
using System;
using API_2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211214164651_addingForeignKeys")]
    partial class addingForeignKeys
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("API_2.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("valid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("API_2.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("quizQuestionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("quizQuestionsId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("API_2.Quiz", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("difficulty")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("EASY");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("numberOfPlays")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("numberOfVotes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("state")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("DRAFT");

                    b.HasKey("Id");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("API_2.QuizQuestions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("quizId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("quizId")
                        .IsUnique();

                    b.ToTable("QuizQuestions");
                });

            modelBuilder.Entity("API_2.Answer", b =>
                {
                    b.HasOne("API_2.Question", null)
                        .WithMany("answers")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("API_2.Question", b =>
                {
                    b.HasOne("API_2.QuizQuestions", "QuizQuestions")
                        .WithMany("questions")
                        .HasForeignKey("quizQuestionsId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("QuizQuestions");
                });

            modelBuilder.Entity("API_2.QuizQuestions", b =>
                {
                    b.HasOne("API_2.Quiz", "Quiz")
                        .WithOne("quizQuestions")
                        .HasForeignKey("API_2.QuizQuestions", "quizId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("API_2.Question", b =>
                {
                    b.Navigation("answers");
                });

            modelBuilder.Entity("API_2.Quiz", b =>
                {
                    b.Navigation("quizQuestions");
                });

            modelBuilder.Entity("API_2.QuizQuestions", b =>
                {
                    b.Navigation("questions");
                });
#pragma warning restore 612, 618
        }
    }
}
