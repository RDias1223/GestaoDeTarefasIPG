using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoDeTarefasIPG.Migrations
{
    public partial class funcionario1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Morada",
                table: "Funcionario");

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostal",
                table: "Funcionario",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data_Nascimento",
                table: "Funcionario",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Funcionario",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "Data_Nascimento",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Funcionario");

            migrationBuilder.AddColumn<string>(
                name: "Morada",
                table: "Funcionario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
