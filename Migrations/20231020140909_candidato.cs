using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    public partial class candidato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidatosModelsId",
                table: "Familiares",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CandidatosModelsId",
                table: "Documentos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FotosCandidato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FotoName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FotoFile = table.Column<byte[]>(type: "longblob", nullable: false),
                    CandidatoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotosCandidato", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CIM = table.Column<int>(type: "int", nullable: true),
                    CPF = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RG = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataExpedicao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Nascimento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AcreditaSerSupremo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Religiao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Naturalidade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nacionalidade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstadoCivil = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoSanguineo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CEP = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Profissao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Endereco = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TempoMoradia = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cidade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bairro = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pai = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaiMacom = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Renda = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Mae = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataSindicancia = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Motivos = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Vicios = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Aptidoes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContatoEmergencia = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FoneEmergencia = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FamiliaConcorda = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    FotoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidatos_FotosCandidato_FotoId",
                        column: x => x.FotoId,
                        principalTable: "FotosCandidato",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidatos_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Familiares_CandidatosModelsId",
                table: "Familiares",
                column: "CandidatosModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_CandidatosModelsId",
                table: "Documentos",
                column: "CandidatosModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_FotoId",
                table: "Candidatos",
                column: "FotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_StatusId",
                table: "Candidatos",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Candidatos_CandidatosModelsId",
                table: "Documentos",
                column: "CandidatosModelsId",
                principalTable: "Candidatos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Familiares_Candidatos_CandidatosModelsId",
                table: "Familiares",
                column: "CandidatosModelsId",
                principalTable: "Candidatos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Candidatos_CandidatosModelsId",
                table: "Documentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Familiares_Candidatos_CandidatosModelsId",
                table: "Familiares");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "FotosCandidato");

            migrationBuilder.DropIndex(
                name: "IX_Familiares_CandidatosModelsId",
                table: "Familiares");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_CandidatosModelsId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "CandidatosModelsId",
                table: "Familiares");

            migrationBuilder.DropColumn(
                name: "CandidatosModelsId",
                table: "Documentos");
        }
    }
}
