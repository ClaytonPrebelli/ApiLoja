using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class Cobrancas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasCobrancasModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoriaNome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasCobrancasModels", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CobrancasModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ref = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoriaCobrancaId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<double>(type: "double", nullable: false),
                    Pago = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CategoriasCobrancasId = table.Column<int>(type: "int", nullable: false),
                    UsuarioModelsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobrancasModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CobrancasModels_CategoriasCobrancasModels_CategoriasCobranca~",
                        column: x => x.CategoriasCobrancasId,
                        principalTable: "CategoriasCobrancasModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CobrancasModels_Usuario_UsuarioModelsId",
                        column: x => x.UsuarioModelsId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasModels_CategoriasCobrancasId",
                table: "CobrancasModels",
                column: "CategoriasCobrancasId");

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasModels_UsuarioModelsId",
                table: "CobrancasModels",
                column: "UsuarioModelsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CobrancasModels");

            migrationBuilder.DropTable(
                name: "CategoriasCobrancasModels");
        }
    }
}
