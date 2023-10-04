using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locadora.Api.Infra.Data.Migrations
{
    public partial class AjustesEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovimentacaoVeiculoId",
                table: "Veiculos");

            migrationBuilder.AddColumn<int>(
                name: "MovimentacaoVeiculo",
                table: "MovimentacoesVeiculos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovimentacaoVeiculo",
                table: "MovimentacoesVeiculos");

            migrationBuilder.AddColumn<Guid>(
                name: "MovimentacaoVeiculoId",
                table: "Veiculos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
