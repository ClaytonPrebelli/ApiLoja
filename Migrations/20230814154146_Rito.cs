using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class Rito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rito",
                table: "Lojas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_StatusId",
                table: "Usuario",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Status_StatusId",
                table: "Usuario",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Status_StatusId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_StatusId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Rito",
                table: "Lojas");
        }
    }
}
