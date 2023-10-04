using System.ComponentModel.DataAnnotations;
using Locadora.Api.Domain.Entities.Enums;


namespace Locadora.Api.Application.ViewModels;

public class AtualizarVeiculoRequest
{
    public string Placa { get; set; }
    public EStatusVeiculo? StatusVeiculo { get; set; }
}