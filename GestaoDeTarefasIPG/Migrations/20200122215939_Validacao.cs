using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoDeTarefasIPG.Migrations
{
    public partial class Validacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Contato",
                table: "Professor",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Contato",
                table: "Professor",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
