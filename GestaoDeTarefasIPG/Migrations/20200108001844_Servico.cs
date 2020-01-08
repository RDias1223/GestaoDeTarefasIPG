﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoDeTarefasIPG.Migrations
{
    public partial class Servico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Servico_UnidadeOrganizacionalID",
                table: "Servico",
                column: "UnidadeOrganizacionalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servico");
        }
    }
}
