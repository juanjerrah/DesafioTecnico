using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Repositories;

namespace Locadora.Api.Infra.Data.Repositories;

public class EventoRepository : IEventoRepository
{
    public Task InserirEventoVeiculo(Evento evento)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Evento>> ObterEventosVeiculo(Guid veiculoId)
    {
        throw new NotImplementedException();
    }
}