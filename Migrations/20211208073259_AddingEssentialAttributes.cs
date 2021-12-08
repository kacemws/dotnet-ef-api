using Microsoft.EntityFrameworkCore.Migrations;

namespace API_2.Migrations
{
    public partial class AddingEssentialAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "difficulty",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "numberOfPlays",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "rating",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "difficulty",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "numberOfPlays",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "rating",
                table: "Quizzes");
        }
    }
}
