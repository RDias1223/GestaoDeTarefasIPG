using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoDeTarefasIPG.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    CargoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    CargoChefeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.CargoId);
                    table.ForeignKey(
                        name: "FK_Cargo_Cargo_CargoChefeId",
                        column: x => x.CargoChefeId,
                        principalTable: "Cargo",
                        principalColumn: "CargoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeOrganizacional",
                columns: table => new
                {
                    UnidadeOrganizacionalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Contacto = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeOrganizacional", x => x.UnidadeOrganizacionalID);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Endereco = table.Column<string>(maxLength: 100, nullable: false),
                    CodigoPostal = table.Column<string>(nullable: false),
                    Data_Nascimento = table.Column<DateTime>(nullable: false),
                    Contacto = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    CargoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.FuncionarioId);
                    table.ForeignKey(
                        name: "FK_Funcionario_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "CargoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    ServicoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Contacto = table.Column<string>(nullable: false),
                    UnidadeOrganizacionalID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.ServicoID);
                    table.ForeignKey(
                        name: "FK_Servico_UnidadeOrganizacional_UnidadeOrganizacionalID",
                        column: x => x.UnidadeOrganizacionalID,
                        principalTable: "UnidadeOrganizacional",
                        principalColumn: "UnidadeOrganizacionalID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarefa",
                columns: table => new
                {
                    TarefaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 1000, nullable: true),
                    FuncionarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefa", x => x.TarefaID);
                    table.ForeignKey(
                        name: "FK_Tarefa_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_CargoChefeId",
                table: "Cargo",
                column: "CargoChefeId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_CargoId",
                table: "Funcionario",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_UnidadeOrganizacionalID",
                table: "Servico",
                column: "UnidadeOrganizacionalID");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_FuncionarioId",
                table: "Tarefa",
                column: "FuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "Tarefa");

            migrationBuilder.DropTable(
                name: "UnidadeOrganizacional");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Cargo");
        }
    }
}
