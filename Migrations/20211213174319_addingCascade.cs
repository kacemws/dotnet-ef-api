using Microsoft.EntityFrameworkCore.Migrations;

namespace API_2.Migrations
{
    public partial class addingCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_Quizzes_quizId",
                table: "QuizQuestions");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_Quizzes_quizId",
                table: "QuizQuestions",
                column: "quizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_Quizzes_quizId",
                table: "QuizQuestions");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_Quizzes_quizId",
                table: "QuizQuestions",
                column: "quizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
