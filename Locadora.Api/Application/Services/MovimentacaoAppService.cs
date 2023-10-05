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

    public async Task<IEnumerable<MovimentacaoResponse>> ObterMovimentacoesDoVeiculo(Guid veiculoId)
    {
        if (veiculoId == Guid.Empty)
        {
            _bus.RaiseValidationError("O veículo é necessário", StatusCodes.Status400BadRequest);
            return Enumerable.Empty<MovimentacaoResponse>();
        }
        var movimentos = await _repository.ObterEventosVeiculo(veiculoId);
        var orderedMovements = movimentos.OrderByDescending(x => x.DateInc);

        return _mapper.Map<IEnumerable<MovimentacaoResponse>>(orderedMovements);
    }
}