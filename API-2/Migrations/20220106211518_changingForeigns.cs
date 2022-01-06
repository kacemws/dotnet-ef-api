using Microsoft.EntityFrameworkCore.Migrations;

namespace API_2.Migrations
{
    public partial class changingForeigns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_questionId",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_questionId",
                table: "Answers",
                column: "questionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_questionId",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_questionId",
                table: "Answers",
                column: "questionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
