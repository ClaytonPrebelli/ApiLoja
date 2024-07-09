using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class FotoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Fotos_FotoId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_FotoId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "FotoId",
                table: "Usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FotoId",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FotoFile = table.Column<byte[]>(type: "longblob", nullable: false),
                    FotoName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_FotoId",
                table: "Usuario",
                column: "FotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Fotos_FotoId",
                table: "Usuario",
                column: "FotoId",
                principalTable: "Fotos",
                principalColumn: "Id");
        }
    }
}
