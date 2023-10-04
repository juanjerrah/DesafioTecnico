using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locadora.Api.Infra.Data.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimentacoesVeiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    VeiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CascadeMode = table.Column<int>(type: "integer", nullable: false),
                    ClassLevelCascadeMode = table.Column<int>(type: "integer", nullable: false),
                    RuleLevelCascadeMode = table.Column<int>(type: "integer", nullable: false),
                    DateInc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateAlter = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacoesVeiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Placa = table.Column<string>(type: "text", nullable: false),
                    TipoVeiculo = table.Column<int>(type: "integer", nullable: false),
                    StatusVeiculo = table.Column<int>(type: "integer", nullable: false),
                    MovimentacaoVeiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    MovimentacoesVeiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CascadeMode = table.Column<int>(type: "integer", nullable: false),
                    ClassLevelCascadeMode = table.Column<int>(type: "integer", nullable: false),
                    RuleLevelCascadeMode = table.Column<int>(type: "integer", nullable: false),
                    DateInc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateAlter = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculos_MovimentacoesVeiculos_MovimentacoesVeiculoId",
                        column: x => x.MovimentacoesVeiculoId,
                        principalTable: "MovimentacoesVeiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_MovimentacoesVeiculoId",
                table: "Veiculos",
                column: "MovimentacoesVeiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "MovimentacoesVeiculos");
        }
    }
}
