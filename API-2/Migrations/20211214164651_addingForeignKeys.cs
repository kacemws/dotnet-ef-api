using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_2.Migrations
{
    public partial class addingForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuizQuestions_QuizQuestionsId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "QuizQuestionsId",
                table: "Questions",
                newName: "quizQuestionsId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuizQuestionsId",
                table: "Questions",
                newName: "IX_Questions_quizQuestionsId");

            migrationBuilder.AlterColumn<Guid>(
                name: "quizQuestionsId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuizQuestions_quizQuestionsId",
                table: "Questions",
                column: "quizQuestionsId",
                principalTable: "QuizQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuizQuestions_quizQuestionsId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "quizQuestionsId",
                table: "Questions",
                newName: "QuizQuestionsId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_quizQuestionsId",
                table: "Questions",
                newName: "IX_Questions_QuizQuestionsId");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuizQuestionsId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuizQuestions_QuizQuestionsId",
                table: "Questions",
                column: "QuizQuestionsId",
                principalTable: "QuizQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
