using AutoMapper;
using Locadora.Api.Application.ViewModels;
using Locadora.Api.Domain.Entities;

namespace Locadora.Api.Application.AutoMapper;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Veiculo, VeiculoResponse>();
        CreateMap<Veiculo, InserirVeiculoRequest>();
        CreateMap<Veiculo, AtualizarVeiculoRequest>();
    }
}