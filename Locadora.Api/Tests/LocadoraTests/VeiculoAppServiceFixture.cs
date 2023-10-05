﻿using Locadora.Api.Application.AutoMapper;
using Locadora.Api.Application.Interfaces;
using Locadora.Api.Application.Services;
using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Interfaces;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Locadora.Api.Tests.LocadoraTests;

[CollectionDefinition(nameof(VeiculoAppServiceCollection))]
public class VeiculoAppServiceCollection : ICollectionFixture<VeiculoAppServiceFixture>{ }
public class VeiculoAppServiceFixture
{
    private VeiculoAppService VeiculoAppService { get; set; }
    public AutoMocker Mocker { get; set; }

    public VeiculoAppService GetVeiculoAppService()
    {
        Mocker = new AutoMocker();

        Mocker.Use(AutoMapperConfiguration.RegisterMappings().CreateMapper());
        
        VeiculoAppService = Mocker.CreateInstance<VeiculoAppService>();

        return VeiculoAppService;
    }

    public void SetupObterVeiculos(IEnumerable<Veiculo> veiculos)
    {
        Mocker.GetMock<IVeiculoRepository>()
            .Setup(x => x.ObterVeiculos())
            .ReturnsAsync(veiculos);
    }
    
    public void SetupObterVeiculoPorPlaca(IEnumerable<Veiculo> veiculos)
    {
        Mocker.GetMock<IVeiculoRepository>()
            .Setup(x => x.ObterVeiculoPorPlaca(It.IsAny<string>()))
            .ReturnsAsync((string placa) =>
                veiculos.FirstOrDefault(x => x.Placa.ToLower().Equals(placa.ToLower())));
    }
}