using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoDeTarefasIPG.Migrations
{
    public partial class cargo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_Cargo_CargoChefeId",
                table: "Cargo");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Cargo_TempId",
                table: "Cargo");

            migrationBuilder.RenameColumn(
                name: "TempId",
                table: "Cargo",
                newName: "CargoId");

            migrationBuilder.AlterColumn<int>(
                name: "CargoChefeId",
                table: "Cargo",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Cargo",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargo",
                table: "Cargo",
                column: "CargoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargo",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Cargo");

            migrationBuilder.RenameColumn(
                name: "CargoId",
                table: "Cargo",
                newName: "TempId");

            migrationBuilder.AlterColumn<int>(
                name: "CargoChefeId",
                table: "Cargo",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Cargo_TempId",
                table: "Cargo",
                column: "TempId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_Cargo_CargoChefeId",
                table: "Cargo",
                column: "CargoChefeId",
                principalTable: "Cargo",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
