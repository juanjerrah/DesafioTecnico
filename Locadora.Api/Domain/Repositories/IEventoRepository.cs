using Locadora.Api.Domain.Entities;

namespace Locadora.Api.Domain.Repositories;

public interface IEventoRepository
{
    Task InserirEventoVeiculo(Evento evento);
    Task<IEnumerable<Evento>> ObterEventosVeiculo(Guid veiculoId);
}