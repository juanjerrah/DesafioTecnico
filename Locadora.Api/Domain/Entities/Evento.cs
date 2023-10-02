namespace Locadora.Api.Domain.Entities;

public class Evento : Entity<Evento>
{
    public string Descricao { get; private set; }
    public Guid VeiculoId { get; private set; }
    public IEnumerable<Veiculo> Veiculos { get; set; }
    
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
}