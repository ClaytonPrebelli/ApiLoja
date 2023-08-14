using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class Fotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FotosId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FotoName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FotoFile = table.Column<byte[]>(type: "longblob", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_FotosId",
                table: "Usuario",
                column: "FotosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Fotos_FotosId",
                table: "Usuario",
                column: "FotosId",
                principalTable: "Fotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Fotos_FotosId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_FotosId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "FotosId",
                table: "Usuario");
        }
    }
}
