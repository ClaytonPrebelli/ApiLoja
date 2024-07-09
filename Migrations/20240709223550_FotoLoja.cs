using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class FotoLoja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lojas_FotosLojas_FotosId",
                table: "Lojas");

            migrationBuilder.DropTable(
                name: "FotosLojas");

            migrationBuilder.DropIndex(
                name: "IX_Lojas_FotosId",
                table: "Lojas");

            migrationBuilder.DropColumn(
                name: "FotosId",
                table: "Lojas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FotosId",
                table: "Lojas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FotosLojas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FotoFile = table.Column<byte[]>(type: "longblob", nullable: false),
                    FotoName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LojasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotosLojas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Lojas_FotosId",
                table: "Lojas",
                column: "FotosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lojas_FotosLojas_FotosId",
                table: "Lojas",
                column: "FotosId",
                principalTable: "FotosLojas",
                principalColumn: "Id");
        }
    }
}
