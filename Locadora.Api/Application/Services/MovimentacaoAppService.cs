using AutoMapper;
using Locadora.Api.Application.Interfaces;
using Locadora.Api.Application.ViewModels.Movimentacao;
using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Interfaces;

namespace Locadora.Api.Application.Services;

public class MovimentacaoAppService : IMovimentacoesAppService
{
    private readonly IMapper _mapper;
    private readonly IMessageBus _bus;
    private readonly IMovimentacoesVeiculoRepository _repository;

    public MovimentacaoAppService(IMapper mapper, IMessageBus bus, IMovimentacoesVeiculoRepository repository)
    {
        _mapper = mapper;
        _bus = bus;
        _repository = repository;
    }

    public async Task InserirMovimentacao(InserirMovimentacaoRequest movimentacaoRequest)
    {
        var movimento = new MovimentacoesVeiculo(Guid.NewGuid(), movimentacaoRequest.Descricao, 
            movimentacaoRequest.MovimentacaoVeiculo, movimentacaoRequest.VeiculoId);
        await _repository.InserirMovimentacaoVeiculo(movimento);
    }
}