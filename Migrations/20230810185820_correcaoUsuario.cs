using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class correcaoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Fotos_FotosId",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Status_StatusId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_FotosId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_StatusId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "FotosId",
                table: "Usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FotosId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_FotosId",
                table: "Usuario",
                column: "FotosId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_StatusId",
                table: "Usuario",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Fotos_FotosId",
                table: "Usuario",
                column: "FotosId",
                principalTable: "Fotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Status_StatusId",
                table: "Usuario",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
