﻿using AutoMapper;
using Locadora.Api.Application.ViewModels;
using Locadora.Api.Application.ViewModels.Movimentacao;
using Locadora.Api.Domain.Entities;

namespace Locadora.Api.Application.AutoMapper;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Veiculo, VeiculoResponse>()
            .AfterMap((x, y) =>
            {
                y.EStatusVeiculo = x.StatusVeiculo.ToString();
                y.ETipoVeiculo = x.TipoVeiculo.ToString();
            });
        CreateMap<MovimentacoesVeiculo, MovimentacaoResponse>()
            .AfterMap((x, y)=>
            {
                y.MovimentacaoVeiculo = x.MovimentacaoVeiculo.ToString();
            });
    }
}