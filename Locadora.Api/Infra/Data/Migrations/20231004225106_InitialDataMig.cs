using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locadora.Api.Infra.Data.Migrations
{
    public partial class InitialDataMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Locadora");

            migrationBuilder.CreateTable(
                name: "Vei_Veiculo",
                schema: "Locadora",
                columns: table => new
                {
                    Vei_VeiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Vei_Placa = table.Column<string>(type: "text", nullable: false),
                    Vei_Tipo = table.Column<int>(type: "integer", nullable: false),
                    Vei_Status = table.Column<int>(type: "integer", nullable: false),
                    DateInc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateAlter = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vei_Veiculo", x => x.Vei_VeiculoId);
                });

            migrationBuilder.CreateTable(
                name: "Mov_MovimentacoesVeiculo",
                schema: "Locadora",
                columns: table => new
                {
                    Mov_MovimentacoesVeiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Mov_Descricao = table.Column<string>(type: "text", nullable: false),
                    MovimentacaoVeiculo = table.Column<int>(type: "integer", nullable: false),
                    Vei_VeiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateInc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateAlter = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mov_MovimentacoesVeiculoId", x => x.Mov_MovimentacoesVeiculoId);
                    table.ForeignKey(
                        name: "FK_Mov_MovimentacoesVeiculo_Vei_Veiculo_Vei_VeiculoId",
                        column: x => x.Vei_VeiculoId,
                        principalSchema: "Locadora",
                        principalTable: "Vei_Veiculo",
                        principalColumn: "Vei_VeiculoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mov_MovimentacoesVeiculo_Vei_VeiculoId",
                schema: "Locadora",
                table: "Mov_MovimentacoesVeiculo",
                column: "Vei_VeiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mov_MovimentacoesVeiculo",
                schema: "Locadora");

            migrationBuilder.DropTable(
                name: "Vei_Veiculo",
                schema: "Locadora");
        }
    }
}
