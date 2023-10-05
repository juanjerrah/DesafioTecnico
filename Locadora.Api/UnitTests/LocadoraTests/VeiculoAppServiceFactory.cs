using Locadora.Api.Application.ViewModels;
using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.UnitTests.LocadoraTests;

public class VeiculoAppServiceFactory
{
    public Veiculo GerarVeiculo(Guid? id = null, string? placa = null, ETiposVeiculos? tipoVeiculo = null, 
        EStatusVeiculo? statusVeiculo = null)
    {
        return new Veiculo(id ?? Guid.NewGuid(), placa ?? "qwe1234", tipoVeiculo ?? ETiposVeiculos.Suv, 
            statusVeiculo ?? EStatusVeiculo.Disponivel);
    }

    public VeiculoFiltroRequest GerarVeiculoFiltroRequest(Guid? veiculoId = null, string? placa = null, EStatusVeiculo? statusVeiculo = null, ETiposVeiculos? tiposVeiculos = null)
    {
        return new VeiculoFiltroRequest
        {
            VeiculoId = veiculoId,
            Placa = placa,
            StatusVeiculo = statusVeiculo,
            TipoVeiculo = tiposVeiculos,
        };
    }

    public InserirVeiculoRequest GerarInserirVeiculoRequest(string? placa, ETiposVeiculos? tiposVeiculos = null, 
        EStatusVeiculo? statusVeiculo = null)
    {
        return new InserirVeiculoRequest
        {
            Placa = placa,
            TipoDoVeiculo = tiposVeiculos,
            StatusDoVeiculo = statusVeiculo
        };
    }

    public MovimentacoesVeiculo GerarMovimentacoesVeiculo(Guid? id = null, string? descricao = null, 
        EMovimentacaoVeiculo? movimentacaoVeiculo = null, Guid? veiculoId = null)
    {
        return new MovimentacoesVeiculo(id ?? Guid.NewGuid(), descricao ?? "teste", movimentacaoVeiculo ?? 
            EMovimentacaoVeiculo.EntradaVeiculoNaBase, veiculoId ?? Guid.NewGuid());
    }
}