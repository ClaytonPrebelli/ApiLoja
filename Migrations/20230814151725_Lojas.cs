using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class Lojas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LojaId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeLoja = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroLoja = table.Column<int>(type: "int", nullable: false),
                    Veneravel = table.Column<int>(type: "int", nullable: false),
                    DataFundacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Ativa = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_LojaId",
                table: "Usuario",
                column: "LojaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Lojas_LojaId",
                table: "Usuario",
                column: "LojaId",
                principalTable: "Lojas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Lojas_LojaId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_LojaId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "LojaId",
                table: "Usuario");
        }
    }
}
