using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class renda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidatos_FotosCandidato_FotoId",
                table: "Candidatos");

            migrationBuilder.DropIndex(
                name: "IX_Candidatos_FotoId",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "FotoId",
                table: "Candidatos");

            migrationBuilder.AlterColumn<string>(
                name: "Renda",
                table: "Candidatos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Renda",
                table: "Candidatos",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "FotoId",
                table: "Candidatos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_FotoId",
                table: "Candidatos",
                column: "FotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidatos_FotosCandidato_FotoId",
                table: "Candidatos",
                column: "FotoId",
                principalTable: "FotosCandidato",
                principalColumn: "Id");
        }
    }
}
