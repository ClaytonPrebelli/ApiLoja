using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class AddFrequenciaFinanceiroEInstalacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobrancasModels_CategoriasCobrancasModels_CategoriasCobranca~",
                table: "CobrancasModels");

            migrationBuilder.DropForeignKey(
                name: "FK_CobrancasModels_Usuario_UsuarioModelsId",
                table: "CobrancasModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CobrancasModels",
                table: "CobrancasModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriasCobrancasModels",
                table: "CategoriasCobrancasModels");

            migrationBuilder.RenameTable(
                name: "CobrancasModels",
                newName: "Cobrancas");

            migrationBuilder.RenameTable(
                name: "CategoriasCobrancasModels",
                newName: "CategoriasCobrancas");

            migrationBuilder.RenameIndex(
                name: "IX_CobrancasModels_UsuarioModelsId",
                table: "Cobrancas",
                newName: "IX_Cobrancas_UsuarioModelsId");

            migrationBuilder.RenameIndex(
                name: "IX_CobrancasModels_CategoriasCobrancasId",
                table: "Cobrancas",
                newName: "IX_Cobrancas_CategoriasCobrancasId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInstalacao",
                table: "Usuario",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isMestreInstalado",
                table: "Usuario",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cobrancas",
                table: "Cobrancas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriasCobrancas",
                table: "CategoriasCobrancas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CategoriasFinanceiro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasFinanceiro", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Frequencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioModelsId = table.Column<int>(type: "int", nullable: false),
                    DataReuniao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Presente = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frequencia_Usuario_UsuarioModelsId",
                        column: x => x.UsuarioModelsId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Financeiro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoriaFinanceiroId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<double>(type: "double", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModelsId = table.Column<int>(type: "int", nullable: true),
                    Observacao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financeiro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financeiro_CategoriasFinanceiro_CategoriaFinanceiroId",
                        column: x => x.CategoriaFinanceiroId,
                        principalTable: "CategoriasFinanceiro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financeiro_Usuario_UsuarioModelsId",
                        column: x => x.UsuarioModelsId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "CategoriasCobrancas",
                columns: new[] { "Id", "CategoriaNome" },
                values: new object[,]
                {
                    { 1, "Mensalidade" },
                    { 2, "Mútua" },
                    { 3, "Joia" },
                    { 4, "Medalha" },
                    { 5, "Placets" },
                    { 6, "Outros" }
                });

            migrationBuilder.InsertData(
                table: "CategoriasFinanceiro",
                columns: new[] { "Id", "Nome", "Tipo" },
                values: new object[,]
                {
                    { 1, "Mensalidade", "Entrada" },
                    { 2, "Mútua", "Entrada" },
                    { 3, "Joia", "Entrada" },
                    { 4, "Medalha", "Entrada" },
                    { 5, "Aluguel", "Saida" },
                    { 6, "Capitação GOSP", "Saida" },
                    { 7, "Mútua", "Saida" },
                    { 8, "Insumos", "Saida" },
                    { 9, "Ágape", "Saida" },
                    { 10, "Medalha", "Saida" },
                    { 11, "Placets", "Saida" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Financeiro_CategoriaFinanceiroId",
                table: "Financeiro",
                column: "CategoriaFinanceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Financeiro_UsuarioModelsId",
                table: "Financeiro",
                column: "UsuarioModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Frequencia_UsuarioModelsId",
                table: "Frequencia",
                column: "UsuarioModelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cobrancas_CategoriasCobrancas_CategoriasCobrancasId",
                table: "Cobrancas",
                column: "CategoriasCobrancasId",
                principalTable: "CategoriasCobrancas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cobrancas_Usuario_UsuarioModelsId",
                table: "Cobrancas",
                column: "UsuarioModelsId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobrancas_CategoriasCobrancas_CategoriasCobrancasId",
                table: "Cobrancas");

            migrationBuilder.DropForeignKey(
                name: "FK_Cobrancas_Usuario_UsuarioModelsId",
                table: "Cobrancas");

            migrationBuilder.DropTable(
                name: "Financeiro");

            migrationBuilder.DropTable(
                name: "Frequencia");

            migrationBuilder.DropTable(
                name: "CategoriasFinanceiro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cobrancas",
                table: "Cobrancas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriasCobrancas",
                table: "CategoriasCobrancas");

            migrationBuilder.DeleteData(
                table: "CategoriasCobrancas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoriasCobrancas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CategoriasCobrancas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CategoriasCobrancas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CategoriasCobrancas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CategoriasCobrancas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "DataInstalacao",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "isMestreInstalado",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Cobrancas",
                newName: "CobrancasModels");

            migrationBuilder.RenameTable(
                name: "CategoriasCobrancas",
                newName: "CategoriasCobrancasModels");

            migrationBuilder.RenameIndex(
                name: "IX_Cobrancas_UsuarioModelsId",
                table: "CobrancasModels",
                newName: "IX_CobrancasModels_UsuarioModelsId");

            migrationBuilder.RenameIndex(
                name: "IX_Cobrancas_CategoriasCobrancasId",
                table: "CobrancasModels",
                newName: "IX_CobrancasModels_CategoriasCobrancasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CobrancasModels",
                table: "CobrancasModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriasCobrancasModels",
                table: "CategoriasCobrancasModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CobrancasModels_CategoriasCobrancasModels_CategoriasCobranca~",
                table: "CobrancasModels",
                column: "CategoriasCobrancasId",
                principalTable: "CategoriasCobrancasModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CobrancasModels_Usuario_UsuarioModelsId",
                table: "CobrancasModels",
                column: "UsuarioModelsId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }
    }
}
