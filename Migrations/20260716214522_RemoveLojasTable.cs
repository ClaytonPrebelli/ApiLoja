using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class RemoveLojasTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Lojas_LojaModelsId",
                table: "Documentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Lojas_LojaId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_LojaId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_LojaModelsId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "LojaId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "LojaId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "LojaModelsId",
                table: "Documentos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LojaId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LojaId",
                table: "Documentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LojaModelsId",
                table: "Documentos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativa = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataFundacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Endereco = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instagram = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeLoja = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroLoja = table.Column<int>(type: "int", nullable: false),
                    Oriente = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rito = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Veneravel = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_LojaModelsId",
                table: "Documentos",
                column: "LojaModelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Lojas_LojaModelsId",
                table: "Documentos",
                column: "LojaModelsId",
                principalTable: "Lojas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Lojas_LojaId",
                table: "Usuario",
                column: "LojaId",
                principalTable: "Lojas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
