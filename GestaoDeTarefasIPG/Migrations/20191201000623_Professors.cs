using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoDeTarefasIPG.Migrations
{
    public partial class Professors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Professor",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Email",
                table: "Professor",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
