using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class UpdateSeedCategoriasFinanceiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobrancas_CategoriasCobrancas_CategoriasCobrancasId",
                table: "Cobrancas");

            migrationBuilder.DropIndex(
                name: "IX_Cobrancas_CategoriasCobrancasId",
                table: "Cobrancas");

            migrationBuilder.DeleteData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "CategoriasCobrancasId",
                table: "Cobrancas");

            migrationBuilder.UpdateData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Nome", "Tipo" },
                values: new object[] { "Arrecadação", "Entrada" });

            migrationBuilder.UpdateData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Nome", "Tipo" },
                values: new object[] { "Saldo Remanescente", "Entrada" });

            migrationBuilder.UpdateData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Nome", "Tipo" },
                values: new object[] { "Bolsa Beneficência", "Entrada" });

            migrationBuilder.UpdateData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 10,
                column: "Nome",
                value: "Aluguel");

            migrationBuilder.UpdateData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 11,
                column: "Nome",
                value: "Capitação GOSP");

            migrationBuilder.InsertData(
                table: "CategoriasFinanceiro",
                columns: new[] { "Id", "Nome", "Tipo" },
                values: new object[,]
                {
                    { 12, "Mútua", "Saida" },
                    { 13, "Insumos", "Saida" },
                    { 14, "Ágape", "Saida" },
                    { 15, "Medalha", "Saida" },
                    { 16, "Placets", "Saida" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cobrancas_CategoriaCobrancaId",
                table: "Cobrancas",
                column: "CategoriaCobrancaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cobrancas_CategoriasCobrancas_CategoriaCobrancaId",
                table: "Cobrancas",
                column: "CategoriaCobrancaId",
                principalTable: "CategoriasCobrancas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobrancas_CategoriasCobrancas_CategoriaCobrancaId",
                table: "Cobrancas");

            migrationBuilder.DropIndex(
                name: "IX_Cobrancas_CategoriaCobrancaId",
                table: "Cobrancas");

            migrationBuilder.DeleteData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.AddColumn<int>(
                name: "CategoriasCobrancasId",
                table: "Cobrancas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Nome", "Tipo" },
                values: new object[] { "Aluguel", "Saida" });

            migrationBuilder.UpdateData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Nome", "Tipo" },
                values: new object[] { "Capitação GOSP", "Saida" });

            migrationBuilder.UpdateData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Nome", "Tipo" },
                values: new object[] { "Mútua", "Saida" });

            migrationBuilder.UpdateData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 10,
                column: "Nome",
                value: "Medalha");

            migrationBuilder.UpdateData(
                table: "CategoriasFinanceiro",
                keyColumn: "Id",
                keyValue: 11,
                column: "Nome",
                value: "Placets");

            migrationBuilder.InsertData(
                table: "CategoriasFinanceiro",
                columns: new[] { "Id", "Nome", "Tipo" },
                values: new object[,]
                {
                    { 8, "Insumos", "Saida" },
                    { 9, "Ágape", "Saida" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cobrancas_CategoriasCobrancasId",
                table: "Cobrancas",
                column: "CategoriasCobrancasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cobrancas_CategoriasCobrancas_CategoriasCobrancasId",
                table: "Cobrancas",
                column: "CategoriasCobrancasId",
                principalTable: "CategoriasCobrancas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
